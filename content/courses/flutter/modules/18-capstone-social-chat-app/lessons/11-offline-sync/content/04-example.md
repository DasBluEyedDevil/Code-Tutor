---
type: "EXAMPLE"
title: "Sync Engine Implementation"
---


**SyncService for Queue Processing**

The SyncService manages the sync queue, processes items when online, handles failures with retries, and tracks sync status for UI feedback.



```dart
// lib/core/sync/sync_service.dart
import 'dart:async';
import 'dart:convert';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../database/app_database.dart';
import '../database/tables/posts_table.dart';
import '../database/tables/sync_queue_table.dart';
import '../network/connectivity_service.dart';
import '../providers/database_provider.dart';
import '../providers/api_client_provider.dart';
import '../../features/posts/data/image_upload_service.dart';

final syncServiceProvider = Provider<SyncService>((ref) {
  final service = SyncService(ref);
  ref.onDispose(() => service.dispose());
  return service;
});

final syncStatusProvider = StreamProvider<SyncState>((ref) {
  return ref.watch(syncServiceProvider).syncStateStream;
});

enum SyncStateType { idle, syncing, error, offline }

class SyncState {
  final SyncStateType type;
  final int pendingCount;
  final String? errorMessage;
  final double? progress;

  const SyncState({
    required this.type,
    this.pendingCount = 0,
    this.errorMessage,
    this.progress,
  });

  static const idle = SyncState(type: SyncStateType.idle);
  static const offline = SyncState(type: SyncStateType.offline);
}

class SyncService {
  final Ref _ref;
  Timer? _syncTimer;
  bool _isSyncing = false;
  StreamSubscription? _connectivitySubscription;

  final StreamController<SyncState> _stateController = StreamController.broadcast();
  Stream<SyncState> get syncStateStream => _stateController.stream;

  SyncService(this._ref) {
    _initialize();
  }

  AppDatabase get _db => _ref.read(databaseProvider);
  ConnectivityService get _connectivity => _ref.read(connectivityServiceProvider);

  void _initialize() {
    // Listen for connectivity changes
    _connectivitySubscription = _connectivity.onConnectivityChanged.listen((isOnline) {
      if (isOnline) {
        _stateController.add(const SyncState(type: SyncStateType.idle));
        triggerSync();
      } else {
        _stateController.add(SyncState.offline);
      }
    });

    // Periodic sync every 30 seconds when online
    _syncTimer = Timer.periodic(const Duration(seconds: 30), (_) {
      if (_connectivity.isOnline) {
        triggerSync();
      }
    });

    // Initial sync
    if (_connectivity.isOnline) {
      triggerSync();
    } else {
      _stateController.add(SyncState.offline);
    }
  }

  void triggerSync() {
    if (!_isSyncing && _connectivity.isOnline) {
      _processQueue();
    }
  }

  Future<void> _processQueue() async {
    if (_isSyncing) return;
    _isSyncing = true;

    try {
      final items = await _db.syncQueueDao.getNextBatch(limit: 10);

      if (items.isEmpty) {
        _stateController.add(SyncState.idle);
        _isSyncing = false;
        return;
      }

      _stateController.add(SyncState(
        type: SyncStateType.syncing,
        pendingCount: items.length,
        progress: 0,
      ));

      int processed = 0;
      for (final item in items) {
        try {
          await _processSyncItem(item);
          await _db.syncQueueDao.dequeue(item.id);
          processed++;

          _stateController.add(SyncState(
            type: SyncStateType.syncing,
            pendingCount: items.length - processed,
            progress: processed / items.length,
          ));
        } catch (e) {
          await _handleSyncError(item, e);
        }
      }

      // Check if more items to process
      final remaining = await _db.syncQueueDao.getNextBatch(limit: 1);
      if (remaining.isNotEmpty) {
        // Continue processing
        _isSyncing = false;
        _processQueue();
      } else {
        _stateController.add(SyncState.idle);
        _isSyncing = false;
      }
    } catch (e) {
      _stateController.add(SyncState(
        type: SyncStateType.error,
        errorMessage: e.toString(),
      ));
      _isSyncing = false;
    }
  }

  Future<void> _processSyncItem(SyncQueueData item) async {
    final payload = jsonDecode(item.payload) as Map<String, dynamic>;
    final apiClient = _ref.read(apiClientProvider);

    switch (item.entityType) {
      case SyncEntityType.post:
        await _syncPost(item, payload, apiClient);
        break;
      case SyncEntityType.message:
        await _syncMessage(item, payload, apiClient);
        break;
      case SyncEntityType.like:
        await _syncLike(item, payload, apiClient);
        break;
      case SyncEntityType.comment:
        await _syncComment(item, payload, apiClient);
        break;
      case SyncEntityType.follow:
        await _syncFollow(item, payload, apiClient);
        break;
    }
  }

  Future<void> _syncPost(
    SyncQueueData item,
    Map<String, dynamic> payload,
    ApiClient apiClient,
  ) async {
    switch (item.action) {
      case SyncAction.create:
        // Upload images first if present
        List<String>? imageUrls;
        final localPaths = payload['localImagePaths'] as List<dynamic>?;
        if (localPaths != null && localPaths.isNotEmpty) {
          final uploadService = _ref.read(imageUploadServiceProvider);
          imageUrls = await uploadService.uploadImages(
            localPaths.cast<String>(),
          );
        }

        // Create post on server
        final serverPost = await apiClient.posts.create(
          content: payload['content'] as String,
          imageUrls: imageUrls,
        );

        // Update local post with server ID and URLs
        await _db.postsDao.updateFromServer(
          item.entityId,
          LocalPostsCompanion(
            id: Value(serverPost.id),
            imageUrls: imageUrls != null
                ? Value(jsonEncode(imageUrls))
                : const Value.absent(),
            syncStatus: const Value(SyncStatus.synced),
            syncError: const Value(null),
          ),
        );
        break;

      case SyncAction.update:
        await apiClient.posts.update(
          id: item.entityId,
          content: payload['content'] as String?,
        );
        await _db.postsDao.updateSyncStatus(
          item.entityId,
          SyncStatus.synced,
        );
        break;

      case SyncAction.delete:
        await apiClient.posts.delete(id: item.entityId);
        await _db.postsDao.deletePost(item.entityId);
        break;
    }
  }

  Future<void> _syncMessage(
    SyncQueueData item,
    Map<String, dynamic> payload,
    ApiClient apiClient,
  ) async {
    switch (item.action) {
      case SyncAction.create:
        // Upload media if present
        String? mediaUrl;
        final localPath = payload['localMediaPath'] as String?;
        if (localPath != null) {
          final uploadService = _ref.read(imageUploadServiceProvider);
          final urls = await uploadService.uploadImages([localPath]);
          mediaUrl = urls.first;
        }

        final serverMessage = await apiClient.messages.send(
          conversationId: payload['conversationId'] as String,
          content: payload['content'] as String,
          mediaUrl: mediaUrl,
        );

        // Update local message with server data
        await _db.messagesDao.updateFromServer(
          item.entityId,
          serverMessage,
        );
        break;

      case SyncAction.update:
        // Messages typically aren't updated
        break;

      case SyncAction.delete:
        await apiClient.messages.delete(id: item.entityId);
        await _db.messagesDao.deleteMessage(item.entityId);
        break;
    }
  }

  Future<void> _syncLike(
    SyncQueueData item,
    Map<String, dynamic> payload,
    ApiClient apiClient,
  ) async {
    final postId = payload['postId'] as String;

    switch (item.action) {
      case SyncAction.create:
        await apiClient.posts.like(postId: postId);
        break;
      case SyncAction.delete:
        await apiClient.posts.unlike(postId: postId);
        break;
      default:
        break;
    }
  }

  Future<void> _syncComment(
    SyncQueueData item,
    Map<String, dynamic> payload,
    ApiClient apiClient,
  ) async {
    // Similar pattern to posts
  }

  Future<void> _syncFollow(
    SyncQueueData item,
    Map<String, dynamic> payload,
    ApiClient apiClient,
  ) async {
    final userId = payload['userId'] as String;

    switch (item.action) {
      case SyncAction.create:
        await apiClient.users.follow(userId: userId);
        break;
      case SyncAction.delete:
        await apiClient.users.unfollow(userId: userId);
        break;
      default:
        break;
    }
  }

  Future<void> _handleSyncError(SyncQueueData item, Object error) async {
    final isRetryable = _isRetryableError(error);

    if (isRetryable && item.retryCount < 5) {
      await _db.syncQueueDao.markRetry(item.id, error.toString());

      // Update entity sync status
      if (item.entityType == SyncEntityType.post) {
        await _db.postsDao.updateSyncStatus(
          item.entityId,
          SyncStatus.failed,
          errorMessage: error.toString(),
          retryCount: item.retryCount + 1,
        );
      }
    } else {
      // Max retries exceeded or non-retryable error
      await _db.syncQueueDao.dequeue(item.id);

      if (item.entityType == SyncEntityType.post) {
        await _db.postsDao.updateSyncStatus(
          item.entityId,
          SyncStatus.failed,
          errorMessage: 'Sync failed permanently: $error',
        );
      }
    }
  }

  bool _isRetryableError(Object error) {
    // Network errors are retryable
    if (error.toString().contains('SocketException') ||
        error.toString().contains('TimeoutException') ||
        error.toString().contains('Connection refused')) {
      return true;
    }
    // 5xx server errors are retryable
    if (error.toString().contains('500') ||
        error.toString().contains('502') ||
        error.toString().contains('503')) {
      return true;
    }
    return false;
  }

  // Manual retry for failed items
  Future<void> retryFailed() async {
    // Reset retry counts for failed posts
    final failedPosts = await _db.postsDao.getFailedPosts();
    for (final post in failedPosts) {
      await _db.postsDao.updateSyncStatus(
        post.id,
        SyncStatus.pending,
        retryCount: 0,
      );
    }
    triggerSync();
  }

  void dispose() {
    _syncTimer?.cancel();
    _connectivitySubscription?.cancel();
    _stateController.close();
  }
}

---

// lib/core/sync/sync_status_widget.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'sync_service.dart';

class SyncStatusWidget extends ConsumerWidget {
  const SyncStatusWidget({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final syncState = ref.watch(syncStatusProvider);

    return syncState.when(
      loading: () => const SizedBox.shrink(),
      error: (_, __) => const SizedBox.shrink(),
      data: (state) => _buildStatusIndicator(context, state),
    );
  }

  Widget _buildStatusIndicator(BuildContext context, SyncState state) {
    final theme = Theme.of(context);

    switch (state.type) {
      case SyncStateType.idle:
        return const SizedBox.shrink();

      case SyncStateType.syncing:
        return Container(
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
          decoration: BoxDecoration(
            color: theme.colorScheme.primaryContainer,
            borderRadius: BorderRadius.circular(16),
          ),
          child: Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              SizedBox(
                width: 16,
                height: 16,
                child: CircularProgressIndicator(
                  strokeWidth: 2,
                  value: state.progress,
                  color: theme.colorScheme.primary,
                ),
              ),
              const SizedBox(width: 8),
              Text(
                'Syncing ${state.pendingCount} items...',
                style: TextStyle(
                  fontSize: 12,
                  color: theme.colorScheme.onPrimaryContainer,
                ),
              ),
            ],
          ),
        );

      case SyncStateType.offline:
        return Container(
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
          decoration: BoxDecoration(
            color: theme.colorScheme.errorContainer,
            borderRadius: BorderRadius.circular(16),
          ),
          child: Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              Icon(
                Icons.cloud_off,
                size: 16,
                color: theme.colorScheme.onErrorContainer,
              ),
              const SizedBox(width: 8),
              Text(
                'Offline',
                style: TextStyle(
                  fontSize: 12,
                  color: theme.colorScheme.onErrorContainer,
                ),
              ),
            ],
          ),
        );

      case SyncStateType.error:
        return GestureDetector(
          onTap: () => ref.read(syncServiceProvider).retryFailed(),
          child: Container(
            padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
            decoration: BoxDecoration(
              color: theme.colorScheme.errorContainer,
              borderRadius: BorderRadius.circular(16),
            ),
            child: Row(
              mainAxisSize: MainAxisSize.min,
              children: [
                Icon(
                  Icons.error_outline,
                  size: 16,
                  color: theme.colorScheme.error,
                ),
                const SizedBox(width: 8),
                Text(
                  'Sync failed. Tap to retry',
                  style: TextStyle(
                    fontSize: 12,
                    color: theme.colorScheme.onErrorContainer,
                  ),
                ),
              ],
            ),
          ),
        );
    }
  }
}
```
