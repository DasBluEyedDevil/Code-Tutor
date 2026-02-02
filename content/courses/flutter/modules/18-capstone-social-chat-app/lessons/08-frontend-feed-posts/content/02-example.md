---
type: "EXAMPLE"
title: "Feed Provider and State"
---


**Implementing Feed State Management with Riverpod**

We'll create a robust feed state system that handles pagination, caching, and optimistic updates for a smooth user experience.



```dart
// lib/features/feed/domain/feed_state.dart
import 'package:freezed_annotation/freezed_annotation.dart';
import '../../../shared/models/post.dart';

part 'feed_state.freezed.dart';

/// Represents the current state of the feed
@freezed
class FeedState with _$FeedState {
  const FeedState._();

  const factory FeedState({
    @Default([]) List<Post> posts,
    @Default(false) bool isLoading,
    @Default(false) bool isLoadingMore,
    @Default(true) bool hasMore,
    String? error,
    DateTime? lastRefresh,
    @Default(1) int currentPage,
    @Default(20) int pageSize,
  }) = _FeedState;

  /// Check if feed is empty and not loading
  bool get isEmpty => posts.isEmpty && !isLoading;

  /// Check if this is the initial load
  bool get isInitialLoad => posts.isEmpty && isLoading;

  /// Check if feed is stale (older than 5 minutes)
  bool get isStale {
    if (lastRefresh == null) return true;
    return DateTime.now().difference(lastRefresh!).inMinutes > 5;
  }
}

---

// lib/shared/models/post.dart
import 'package:freezed_annotation/freezed_annotation.dart';
import 'user.dart';

part 'post.freezed.dart';
part 'post.g.dart';

@freezed
class Post with _$Post {
  const Post._();

  const factory Post({
    required String id,
    required User author,
    required String content,
    @Default([]) List<String> imageUrls,
    required DateTime createdAt,
    @Default(0) int likeCount,
    @Default(0) int commentCount,
    @Default(false) bool isLikedByMe,
    @Default(false) bool isBookmarked,
  }) = _Post;

  factory Post.fromJson(Map<String, dynamic> json) => _$PostFromJson(json);

  /// Format the timestamp for display
  String get formattedTime {
    final now = DateTime.now();
    final diff = now.difference(createdAt);

    if (diff.inMinutes < 1) return 'Just now';
    if (diff.inMinutes < 60) return '${diff.inMinutes}m';
    if (diff.inHours < 24) return '${diff.inHours}h';
    if (diff.inDays < 7) return '${diff.inDays}d';
    return '${createdAt.day}/${createdAt.month}/${createdAt.year}';
  }
}

---

// lib/features/feed/data/feed_repository.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../core/providers/serverpod_client_provider.dart';
import '../../../shared/models/post.dart';

class FeedRepository {
  final Ref _ref;

  FeedRepository(this._ref);

  /// Fetch posts with pagination
  Future<FeedPage> fetchFeed({
    required int page,
    required int pageSize,
  }) async {
    try {
      final client = _ref.read(serverpodClientProvider);
      final response = await client.posts.getFeed(
        page: page,
        limit: pageSize,
      );

      return FeedPage(
        posts: response.posts.map((p) => Post.fromJson(p.toJson())).toList(),
        hasMore: response.hasMore,
        totalCount: response.totalCount,
      );
    } catch (e) {
      throw FeedException('Failed to load feed: $e');
    }
  }

  /// Like or unlike a post
  Future<Post> toggleLike(Post post) async {
    try {
      final client = _ref.read(serverpodClientProvider);
      final response = await client.posts.toggleLike(postId: post.id);

      return post.copyWith(
        isLikedByMe: response.isLiked,
        likeCount: response.likeCount,
      );
    } catch (e) {
      throw FeedException('Failed to toggle like: $e');
    }
  }

  /// Create a new post
  Future<Post> createPost({
    required String content,
    List<String>? imageUrls,
  }) async {
    try {
      final client = _ref.read(serverpodClientProvider);
      final response = await client.posts.create(
        content: content,
        imageUrls: imageUrls ?? [],
      );

      return Post.fromJson(response.toJson());
    } catch (e) {
      throw FeedException('Failed to create post: $e');
    }
  }

  /// Bookmark or unbookmark a post
  Future<Post> toggleBookmark(Post post) async {
    try {
      final client = _ref.read(serverpodClientProvider);
      await client.posts.toggleBookmark(postId: post.id);

      return post.copyWith(isBookmarked: !post.isBookmarked);
    } catch (e) {
      throw FeedException('Failed to toggle bookmark: $e');
    }
  }
}

class FeedPage {
  final List<Post> posts;
  final bool hasMore;
  final int totalCount;

  const FeedPage({
    required this.posts,
    required this.hasMore,
    required this.totalCount,
  });
}

class FeedException implements Exception {
  final String message;
  FeedException(this.message);

  @override
  String toString() => message;
}

---

// lib/features/feed/providers/feed_provider.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../data/feed_repository.dart';
import '../domain/feed_state.dart';
import '../../../shared/models/post.dart';

/// Provider for feed repository
final feedRepositoryProvider = Provider<FeedRepository>((ref) {
  return FeedRepository(ref);
});

/// Main feed state notifier
class FeedNotifier extends Notifier<FeedState> {
  @override
  FeedState build() {
    // Initial load
    Future.microtask(() => loadInitial());
    return const FeedState(isLoading: true);
  }

  FeedRepository get _repository => ref.read(feedRepositoryProvider);

  /// Load initial feed
  Future<void> loadInitial() async {
    state = state.copyWith(isLoading: true, error: null);

    try {
      final page = await _repository.fetchFeed(
        page: 1,
        pageSize: state.pageSize,
      );

      state = state.copyWith(
        posts: page.posts,
        hasMore: page.hasMore,
        isLoading: false,
        currentPage: 1,
        lastRefresh: DateTime.now(),
      );
    } catch (e) {
      state = state.copyWith(
        isLoading: false,
        error: e.toString(),
      );
    }
  }

  /// Load more posts (pagination)
  Future<void> loadMore() async {
    // Prevent duplicate loads
    if (state.isLoadingMore || !state.hasMore) return;

    state = state.copyWith(isLoadingMore: true);

    try {
      final nextPage = state.currentPage + 1;
      final page = await _repository.fetchFeed(
        page: nextPage,
        pageSize: state.pageSize,
      );

      // Deduplicate posts
      final existingIds = state.posts.map((p) => p.id).toSet();
      final newPosts = page.posts.where((p) => !existingIds.contains(p.id)).toList();

      state = state.copyWith(
        posts: [...state.posts, ...newPosts],
        hasMore: page.hasMore,
        isLoadingMore: false,
        currentPage: nextPage,
      );
    } catch (e) {
      state = state.copyWith(
        isLoadingMore: false,
        error: e.toString(),
      );
    }
  }

  /// Refresh feed (pull-to-refresh)
  Future<void> refresh() async {
    try {
      final page = await _repository.fetchFeed(
        page: 1,
        pageSize: state.pageSize,
      );

      state = state.copyWith(
        posts: page.posts,
        hasMore: page.hasMore,
        currentPage: 1,
        lastRefresh: DateTime.now(),
        error: null,
      );
    } catch (e) {
      state = state.copyWith(error: e.toString());
    }
  }

  /// Toggle like with optimistic update
  Future<void> toggleLike(Post post) async {
    // Optimistic update
    final optimisticPost = post.copyWith(
      isLikedByMe: !post.isLikedByMe,
      likeCount: post.isLikedByMe ? post.likeCount - 1 : post.likeCount + 1,
    );
    _updatePost(optimisticPost);

    try {
      final updatedPost = await _repository.toggleLike(post);
      _updatePost(updatedPost);
    } catch (e) {
      // Revert on failure
      _updatePost(post);
      state = state.copyWith(error: 'Failed to like post');
    }
  }

  /// Toggle bookmark with optimistic update
  Future<void> toggleBookmark(Post post) async {
    // Optimistic update
    final optimisticPost = post.copyWith(isBookmarked: !post.isBookmarked);
    _updatePost(optimisticPost);

    try {
      final updatedPost = await _repository.toggleBookmark(post);
      _updatePost(updatedPost);
    } catch (e) {
      // Revert on failure
      _updatePost(post);
      state = state.copyWith(error: 'Failed to bookmark post');
    }
  }

  /// Add a new post to the top of the feed
  void addPost(Post post) {
    state = state.copyWith(
      posts: [post, ...state.posts],
    );
  }

  /// Update a post in the list
  void _updatePost(Post updatedPost) {
    final index = state.posts.indexWhere((p) => p.id == updatedPost.id);
    if (index == -1) return;

    final newPosts = [...state.posts];
    newPosts[index] = updatedPost;
    state = state.copyWith(posts: newPosts);
  }

  /// Clear any error state
  void clearError() {
    state = state.copyWith(error: null);
  }
}

/// Provider for feed state
final feedProvider = NotifierProvider<FeedNotifier, FeedState>(() {
  return FeedNotifier();
});

/// Convenience provider for posts list
final postsProvider = Provider<List<Post>>((ref) {
  return ref.watch(feedProvider).posts;
});

/// Provider for checking if feed is loading
final isFeedLoadingProvider = Provider<bool>((ref) {
  final feed = ref.watch(feedProvider);
  return feed.isLoading || feed.isLoadingMore;
});
```
