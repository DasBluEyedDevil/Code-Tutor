---
type: "EXAMPLE"
title: "Repository Pattern for Offline"
---


**Repository That Reads Local First**

The offline-capable repository reads from local database first, fetches from network when online, saves to local cache, and detects connectivity changes to trigger syncs.



```dart
// lib/core/network/connectivity_service.dart
import 'dart:async';
import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final connectivityServiceProvider = Provider<ConnectivityService>((ref) {
  return ConnectivityService();
});

final isOnlineProvider = StreamProvider<bool>((ref) {
  return ref.watch(connectivityServiceProvider).onConnectivityChanged;
});

class ConnectivityService {
  final Connectivity _connectivity = Connectivity();
  final StreamController<bool> _onlineController = StreamController.broadcast();

  ConnectivityService() {
    _connectivity.onConnectivityChanged.listen(_updateConnectionStatus);
    _checkInitialConnection();
  }

  Stream<bool> get onConnectivityChanged => _onlineController.stream;

  bool _isOnline = true;
  bool get isOnline => _isOnline;

  Future<void> _checkInitialConnection() async {
    final result = await _connectivity.checkConnectivity();
    _updateConnectionStatus(result);
  }

  void _updateConnectionStatus(List<ConnectivityResult> results) {
    final wasOnline = _isOnline;
    _isOnline = results.isNotEmpty && !results.contains(ConnectivityResult.none);
    
    if (_isOnline != wasOnline) {
      _onlineController.add(_isOnline);
    }
  }

  void dispose() {
    _onlineController.close();
  }
}

---

// lib/features/posts/data/offline_posts_repository.dart
import 'dart:convert';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:uuid/uuid.dart';
import '../../../core/database/app_database.dart';
import '../../../core/database/tables/posts_table.dart';
import '../../../core/database/tables/sync_queue_table.dart';
import '../../../core/network/connectivity_service.dart';
import '../../../core/providers/database_provider.dart';
import '../../../core/providers/api_client_provider.dart';
import '../domain/post.dart';

final offlinePostsRepositoryProvider = Provider<OfflinePostsRepository>((ref) {
  return OfflinePostsRepository(ref);
});

class OfflinePostsRepository {
  final Ref _ref;
  final _uuid = const Uuid();

  OfflinePostsRepository(this._ref);

  AppDatabase get _db => _ref.read(databaseProvider);
  bool get _isOnline => _ref.read(connectivityServiceProvider).isOnline;

  // Watch posts - always returns local data with reactive updates
  Stream<List<Post>> watchPosts() {
    return _db.postsDao.watchAllPosts().map((localPosts) {
      return localPosts.map(_localPostToPost).toList();
    });
  }

  // Watch user posts
  Stream<List<Post>> watchUserPosts(String userId) {
    return _db.postsDao.watchUserPosts(userId).map((localPosts) {
      return localPosts.map(_localPostToPost).toList();
    });
  }

  // Fetch posts from server and update local cache
  Future<void> refreshPosts() async {
    if (!_isOnline) return;

    try {
      final apiClient = _ref.read(apiClientProvider);
      final serverPosts = await apiClient.posts.getFeed();

      // Convert to local format and upsert
      final localPosts = serverPosts.map((p) => _postToLocalCompanion(
        p,
        syncStatus: SyncStatus.synced,
      )).toList();

      await _db.postsDao.upsertPosts(localPosts);
    } catch (e) {
      // Silently fail - local data is still available
      rethrow;
    }
  }

  // Create post - works offline
  Future<Post> createPost({
    required String content,
    List<String>? localImagePaths,
  }) async {
    final localId = _uuid.v4();
    final now = DateTime.now();
    final currentUserId = _ref.read(currentUserIdProvider);

    // Create local post immediately
    final localPost = LocalPostsCompanion.insert(
      id: localId, // Use local ID as primary key initially
      localId: Value(localId),
      userId: currentUserId,
      content: content,
      localImagePaths: localImagePaths != null
          ? Value(jsonEncode(localImagePaths))
          : const Value.absent(),
      createdAt: now,
      updatedAt: now,
      localModifiedAt: Value(now),
      syncStatus: Value(SyncStatus.pending),
    );

    await _db.postsDao.upsertPost(localPost);

    // Add to sync queue
    await _db.syncQueueDao.enqueue(SyncQueueCompanion.insert(
      entityType: SyncEntityType.post,
      entityId: localId,
      action: SyncAction.create,
      payload: jsonEncode({
        'content': content,
        'localImagePaths': localImagePaths,
      }),
      createdAt: now,
      priority: Value(10), // High priority for new content
    ));

    // Try to sync immediately if online
    if (_isOnline) {
      _ref.read(syncServiceProvider).triggerSync();
    }

    // Return the local post
    final created = await _db.postsDao.getPost(localId);
    return _localPostToPost(created!);
  }

  // Update post - works offline
  Future<void> updatePost(String id, {String? content}) async {
    final now = DateTime.now();

    // Update locally first
    await _db.postsDao.upsertPost(LocalPostsCompanion(
      id: Value(id),
      content: content != null ? Value(content) : const Value.absent(),
      updatedAt: Value(now),
      localModifiedAt: Value(now),
      syncStatus: const Value(SyncStatus.pending),
    ));

    // Check if already has pending action
    final hasPending = await _db.syncQueueDao.hasPendingAction(
      SyncEntityType.post,
      id,
    );

    if (!hasPending) {
      // Add to sync queue
      await _db.syncQueueDao.enqueue(SyncQueueCompanion.insert(
        entityType: SyncEntityType.post,
        entityId: id,
        action: SyncAction.update,
        payload: jsonEncode({'content': content}),
        createdAt: now,
      ));
    }

    if (_isOnline) {
      _ref.read(syncServiceProvider).triggerSync();
    }
  }

  // Delete post - works offline
  Future<void> deletePost(String id) async {
    // Mark as pending delete (don't actually delete until synced)
    await _db.postsDao.updateSyncStatus(id, SyncStatus.pending);

    // Clear any existing pending actions for this post
    await _db.syncQueueDao.clearEntityActions(SyncEntityType.post, id);

    // Add delete action to queue
    await _db.syncQueueDao.enqueue(SyncQueueCompanion.insert(
      entityType: SyncEntityType.post,
      entityId: id,
      action: SyncAction.delete,
      payload: jsonEncode({'id': id}),
      createdAt: DateTime.now(),
      priority: Value(5), // Medium priority
    ));

    if (_isOnline) {
      _ref.read(syncServiceProvider).triggerSync();
    }
  }

  // Like post - works offline with optimistic update
  Future<void> toggleLike(String postId) async {
    final post = await _db.postsDao.getPost(postId);
    if (post == null) return;

    final isLiked = !post.isLikedByMe;
    final likesCount = post.likesCount + (isLiked ? 1 : -1);

    // Update locally immediately (optimistic)
    await _db.postsDao.upsertPost(LocalPostsCompanion(
      id: Value(postId),
      isLikedByMe: Value(isLiked),
      likesCount: Value(likesCount),
      localModifiedAt: Value(DateTime.now()),
    ));

    // Add to sync queue
    await _db.syncQueueDao.enqueue(SyncQueueCompanion.insert(
      entityType: SyncEntityType.like,
      entityId: postId,
      action: isLiked ? SyncAction.create : SyncAction.delete,
      payload: jsonEncode({'postId': postId}),
      createdAt: DateTime.now(),
      priority: Value(1), // Low priority for likes
    ));

    if (_isOnline) {
      _ref.read(syncServiceProvider).triggerSync();
    }
  }

  // Convert local post to domain model
  Post _localPostToPost(LocalPost local) {
    return Post(
      id: local.id,
      localId: local.localId,
      userId: local.userId,
      content: local.content,
      imageUrls: local.imageUrls != null
          ? List<String>.from(jsonDecode(local.imageUrls!))
          : [],
      localImagePaths: local.localImagePaths != null
          ? List<String>.from(jsonDecode(local.localImagePaths!))
          : null,
      likesCount: local.likesCount,
      commentsCount: local.commentsCount,
      isLikedByMe: local.isLikedByMe,
      createdAt: local.createdAt,
      updatedAt: local.updatedAt,
      syncStatus: local.syncStatus,
      syncError: local.syncError,
    );
  }

  // Convert domain model to local companion
  LocalPostsCompanion _postToLocalCompanion(
    Post post, {
    SyncStatus syncStatus = SyncStatus.synced,
  }) {
    return LocalPostsCompanion.insert(
      id: post.id,
      localId: Value(post.localId),
      userId: post.userId,
      content: post.content,
      imageUrls: post.imageUrls.isNotEmpty
          ? Value(jsonEncode(post.imageUrls))
          : const Value.absent(),
      likesCount: Value(post.likesCount),
      commentsCount: Value(post.commentsCount),
      isLikedByMe: Value(post.isLikedByMe),
      createdAt: post.createdAt,
      updatedAt: post.updatedAt,
      syncStatus: Value(syncStatus),
    );
  }
}

---

// lib/features/posts/domain/post.dart
import 'package:freezed_annotation/freezed_annotation.dart';
import '../../../core/database/tables/posts_table.dart';

part 'post.freezed.dart';
part 'post.g.dart';

@freezed
class Post with _$Post {
  const Post._();

  const factory Post({
    required String id,
    String? localId,
    required String userId,
    required String content,
    @Default([]) List<String> imageUrls,
    List<String>? localImagePaths,
    @Default(0) int likesCount,
    @Default(0) int commentsCount,
    @Default(false) bool isLikedByMe,
    required DateTime createdAt,
    required DateTime updatedAt,
    @Default(SyncStatus.synced) SyncStatus syncStatus,
    String? syncError,
  }) = _Post;

  factory Post.fromJson(Map<String, dynamic> json) => _$PostFromJson(json);

  bool get isPending => syncStatus == SyncStatus.pending;
  bool get isSyncing => syncStatus == SyncStatus.syncing;
  bool get isFailed => syncStatus == SyncStatus.failed;
  bool get hasLocalImages => localImagePaths != null && localImagePaths!.isNotEmpty;
  bool get isLocalOnly => localId != null && id == localId;
}
```
