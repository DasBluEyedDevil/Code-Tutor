---
type: "EXAMPLE"
title: "Feed Screen with Infinite Scroll"
---


**Building the Feed Screen with ListView.builder and Scroll Detection**

The feed screen implements infinite scroll, pull-to-refresh, loading indicators, empty states, and error handling for a polished user experience.



```dart
// lib/features/feed/presentation/screens/feed_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../providers/feed_provider.dart';
import '../widgets/post_card.dart';
import '../widgets/post_shimmer.dart';
import '../widgets/feed_empty_state.dart';
import '../widgets/feed_error_state.dart';

class FeedScreen extends ConsumerStatefulWidget {
  const FeedScreen({super.key});

  @override
  ConsumerState<FeedScreen> createState() => _FeedScreenState();
}

class _FeedScreenState extends ConsumerState<FeedScreen> {
  final _scrollController = ScrollController();

  @override
  void initState() {
    super.initState();
    _scrollController.addListener(_onScroll);
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }

  void _onScroll() {
    // Load more when user is near the bottom (200 pixels threshold)
    if (_scrollController.position.pixels >=
        _scrollController.position.maxScrollExtent - 200) {
      ref.read(feedProvider.notifier).loadMore();
    }
  }

  Future<void> _onRefresh() async {
    await ref.read(feedProvider.notifier).refresh();
  }

  void _navigateToCreatePost() {
    Navigator.of(context).pushNamed('/create-post');
  }

  @override
  Widget build(BuildContext context) {
    final feedState = ref.watch(feedProvider);
    final theme = Theme.of(context);

    // Listen for errors
    ref.listen<String?>(
      feedProvider.select((s) => s.error),
      (previous, error) {
        if (error != null && previous != error) {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text(error),
              action: SnackBarAction(
                label: 'Retry',
                onPressed: () {
                  ref.read(feedProvider.notifier).clearError();
                  if (feedState.posts.isEmpty) {
                    ref.read(feedProvider.notifier).loadInitial();
                  }
                },
              ),
            ),
          );
        }
      },
    );

    return Scaffold(
      appBar: AppBar(
        title: const Text('Feed'),
        actions: [
          IconButton(
            icon: const Icon(Icons.add_box_outlined),
            onPressed: _navigateToCreatePost,
            tooltip: 'Create Post',
          ),
        ],
      ),
      body: _buildBody(feedState, theme),
    );
  }

  Widget _buildBody(FeedState feedState, ThemeData theme) {
    // Initial loading state
    if (feedState.isInitialLoad) {
      return const PostShimmerList();
    }

    // Error state with no posts
    if (feedState.error != null && feedState.posts.isEmpty) {
      return FeedErrorState(
        message: feedState.error!,
        onRetry: () => ref.read(feedProvider.notifier).loadInitial(),
      );
    }

    // Empty state
    if (feedState.isEmpty) {
      return FeedEmptyState(
        onRefresh: _onRefresh,
        onCreatePost: _navigateToCreatePost,
      );
    }

    // Feed with posts
    return RefreshIndicator(
      onRefresh: _onRefresh,
      child: ListView.builder(
        controller: _scrollController,
        physics: const AlwaysScrollableScrollPhysics(),
        padding: const EdgeInsets.symmetric(vertical: 8),
        itemCount: feedState.posts.length + (feedState.hasMore ? 1 : 0),
        itemBuilder: (context, index) {
          // Loading indicator at the bottom
          if (index == feedState.posts.length) {
            return _buildLoadingIndicator(feedState);
          }

          final post = feedState.posts[index];
          return PostCard(
            key: ValueKey(post.id),
            post: post,
            onLike: () => ref.read(feedProvider.notifier).toggleLike(post),
            onBookmark: () => ref.read(feedProvider.notifier).toggleBookmark(post),
            onComment: () => _navigateToComments(post.id),
            onShare: () => _sharePost(post),
            onTap: () => _navigateToPostDetail(post.id),
          );
        },
      ),
    );
  }

  Widget _buildLoadingIndicator(FeedState feedState) {
    if (feedState.isLoadingMore) {
      return const Padding(
        padding: EdgeInsets.all(16),
        child: Center(
          child: CircularProgressIndicator(),
        ),
      );
    }

    if (feedState.error != null) {
      return Padding(
        padding: const EdgeInsets.all(16),
        child: Center(
          child: Column(
            children: [
              Text(
                'Failed to load more',
                style: Theme.of(context).textTheme.bodyMedium,
              ),
              const SizedBox(height: 8),
              TextButton(
                onPressed: () => ref.read(feedProvider.notifier).loadMore(),
                child: const Text('Retry'),
              ),
            ],
          ),
        ),
      );
    }

    return const SizedBox(height: 16);
  }

  void _navigateToPostDetail(String postId) {
    Navigator.of(context).pushNamed('/post/$postId');
  }

  void _navigateToComments(String postId) {
    Navigator.of(context).pushNamed('/post/$postId/comments');
  }

  void _sharePost(Post post) {
    // Implement share functionality
  }
}

---

// lib/features/feed/presentation/widgets/post_shimmer.dart
import 'package:flutter/material.dart';
import 'package:shimmer/shimmer.dart';

class PostShimmerList extends StatelessWidget {
  const PostShimmerList({super.key});

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      padding: const EdgeInsets.symmetric(vertical: 8),
      itemCount: 5,
      itemBuilder: (context, index) => const PostShimmer(),
    );
  }
}

class PostShimmer extends StatelessWidget {
  const PostShimmer({super.key});

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final isDark = theme.brightness == Brightness.dark;

    return Shimmer.fromColors(
      baseColor: isDark ? Colors.grey[800]! : Colors.grey[300]!,
      highlightColor: isDark ? Colors.grey[700]! : Colors.grey[100]!,
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // Header with avatar and name
            Row(
              children: [
                Container(
                  width: 40,
                  height: 40,
                  decoration: const BoxDecoration(
                    color: Colors.white,
                    shape: BoxShape.circle,
                  ),
                ),
                const SizedBox(width: 12),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Container(
                      width: 120,
                      height: 14,
                      color: Colors.white,
                    ),
                    const SizedBox(height: 4),
                    Container(
                      width: 60,
                      height: 12,
                      color: Colors.white,
                    ),
                  ],
                ),
              ],
            ),
            const SizedBox(height: 12),
            // Content lines
            Container(
              width: double.infinity,
              height: 14,
              color: Colors.white,
            ),
            const SizedBox(height: 8),
            Container(
              width: MediaQuery.of(context).size.width * 0.7,
              height: 14,
              color: Colors.white,
            ),
            const SizedBox(height: 16),
            // Action buttons
            Row(
              children: [
                Container(width: 60, height: 24, color: Colors.white),
                const SizedBox(width: 16),
                Container(width: 60, height: 24, color: Colors.white),
                const SizedBox(width: 16),
                Container(width: 60, height: 24, color: Colors.white),
              ],
            ),
            const SizedBox(height: 8),
            const Divider(),
          ],
        ),
      ),
    );
  }
}

---

// lib/features/feed/presentation/widgets/feed_empty_state.dart
import 'package:flutter/material.dart';

class FeedEmptyState extends StatelessWidget {
  final VoidCallback onRefresh;
  final VoidCallback onCreatePost;

  const FeedEmptyState({
    super.key,
    required this.onRefresh,
    required this.onCreatePost,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Center(
      child: Padding(
        padding: const EdgeInsets.all(32),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(
              Icons.dynamic_feed_outlined,
              size: 80,
              color: theme.colorScheme.onSurfaceVariant.withValues(alpha: 0.5),
            ),
            const SizedBox(height: 24),
            Text(
              'Your Feed is Empty',
              style: theme.textTheme.headlineSmall?.copyWith(
                fontWeight: FontWeight.bold,
              ),
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: 8),
            Text(
              'Follow some people or create your first post to get started!',
              style: theme.textTheme.bodyMedium?.copyWith(
                color: theme.colorScheme.onSurfaceVariant,
              ),
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: 32),
            FilledButton.icon(
              onPressed: onCreatePost,
              icon: const Icon(Icons.add),
              label: const Text('Create Your First Post'),
            ),
            const SizedBox(height: 16),
            OutlinedButton.icon(
              onPressed: onRefresh,
              icon: const Icon(Icons.refresh),
              label: const Text('Refresh'),
            ),
          ],
        ),
      ),
    );
  }
}

---

// lib/features/feed/presentation/widgets/feed_error_state.dart
import 'package:flutter/material.dart';

class FeedErrorState extends StatelessWidget {
  final String message;
  final VoidCallback onRetry;

  const FeedErrorState({
    super.key,
    required this.message,
    required this.onRetry,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Center(
      child: Padding(
        padding: const EdgeInsets.all(32),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(
              Icons.error_outline,
              size: 64,
              color: theme.colorScheme.error,
            ),
            const SizedBox(height: 24),
            Text(
              'Something went wrong',
              style: theme.textTheme.titleLarge?.copyWith(
                fontWeight: FontWeight.bold,
              ),
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: 8),
            Text(
              message,
              style: theme.textTheme.bodyMedium?.copyWith(
                color: theme.colorScheme.onSurfaceVariant,
              ),
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: 24),
            FilledButton.icon(
              onPressed: onRetry,
              icon: const Icon(Icons.refresh),
              label: const Text('Try Again'),
            ),
          ],
        ),
      ),
    );
  }
}
```
