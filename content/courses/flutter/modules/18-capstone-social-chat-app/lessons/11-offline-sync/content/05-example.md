---
type: "EXAMPLE"
title: "Optimistic UI with Rollback"
---


**Optimistic Updates with Failure Rollback**

This pattern updates the UI immediately with local changes, syncs in the background, and rolls back on failure while notifying the user of sync status.



```dart
// lib/features/posts/providers/optimistic_post_provider.dart
import 'dart:async';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../core/database/tables/posts_table.dart';
import '../data/offline_posts_repository.dart';
import '../domain/post.dart';

final postsProvider = StreamNotifierProvider<PostsNotifier, List<Post>>(() {
  return PostsNotifier();
});

class PostsNotifier extends StreamNotifier<List<Post>> {
  @override
  Stream<List<Post>> build() {
    // Watch local database for reactive updates
    return ref.watch(offlinePostsRepositoryProvider).watchPosts();
  }

  Future<void> refresh() async {
    await ref.read(offlinePostsRepositoryProvider).refreshPosts();
  }

  Future<void> createPost({
    required String content,
    List<String>? imagePaths,
  }) async {
    await ref.read(offlinePostsRepositoryProvider).createPost(
      content: content,
      localImagePaths: imagePaths,
    );
  }

  Future<void> toggleLike(String postId) async {
    await ref.read(offlinePostsRepositoryProvider).toggleLike(postId);
  }

  Future<void> deletePost(String postId) async {
    await ref.read(offlinePostsRepositoryProvider).deletePost(postId);
  }
}

---

// lib/features/posts/presentation/widgets/post_card.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'dart:io';
import '../../domain/post.dart';
import '../../providers/optimistic_post_provider.dart';
import '../../../../core/database/tables/posts_table.dart';

class PostCard extends ConsumerWidget {
  final Post post;

  const PostCard({super.key, required this.post});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final theme = Theme.of(context);

    return Card(
      margin: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // Sync status indicator
          if (post.syncStatus != SyncStatus.synced)
            _buildSyncStatusBanner(context, post),

          // Post content
          Padding(
            padding: const EdgeInsets.all(16),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // Author info
                _buildAuthorRow(context),
                const SizedBox(height: 12),

                // Content
                Text(
                  post.content,
                  style: theme.textTheme.bodyLarge,
                ),
                const SizedBox(height: 12),

                // Images - show local or remote
                if (post.imageUrls.isNotEmpty || post.hasLocalImages)
                  _buildImages(context, post),

                const SizedBox(height: 12),

                // Actions
                _buildActionsRow(context, ref),
              ],
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildSyncStatusBanner(BuildContext context, Post post) {
    final theme = Theme.of(context);

    Color backgroundColor;
    IconData icon;
    String message;

    switch (post.syncStatus) {
      case SyncStatus.pending:
        backgroundColor = theme.colorScheme.primaryContainer;
        icon = Icons.cloud_upload_outlined;
        message = 'Waiting to sync...';
        break;
      case SyncStatus.syncing:
        backgroundColor = theme.colorScheme.primaryContainer;
        icon = Icons.sync;
        message = 'Syncing...';
        break;
      case SyncStatus.failed:
        backgroundColor = theme.colorScheme.errorContainer;
        icon = Icons.error_outline;
        message = post.syncError ?? 'Sync failed';
        break;
      case SyncStatus.conflict:
        backgroundColor = theme.colorScheme.tertiaryContainer;
        icon = Icons.warning_amber;
        message = 'Conflict detected';
        break;
      default:
        return const SizedBox.shrink();
    }

    return Container(
      width: double.infinity,
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      color: backgroundColor,
      child: Row(
        children: [
          if (post.syncStatus == SyncStatus.syncing)
            SizedBox(
              width: 16,
              height: 16,
              child: CircularProgressIndicator(
                strokeWidth: 2,
                color: theme.colorScheme.primary,
              ),
            )
          else
            Icon(icon, size: 16),
          const SizedBox(width: 8),
          Expanded(
            child: Text(
              message,
              style: theme.textTheme.bodySmall,
              maxLines: 1,
              overflow: TextOverflow.ellipsis,
            ),
          ),
          if (post.syncStatus == SyncStatus.failed)
            TextButton(
              onPressed: () => _retrySync(context),
              child: const Text('Retry'),
            ),
        ],
      ),
    );
  }

  Widget _buildImages(BuildContext context, Post post) {
    // Prefer server URLs, fall back to local paths
    final images = post.imageUrls.isNotEmpty
        ? post.imageUrls
            .map((url) => _NetworkImage(url: url))
            .toList()
        : post.localImagePaths!
            .map((path) => _LocalImage(path: path))
            .toList();

    if (images.length == 1) {
      return ClipRRect(
        borderRadius: BorderRadius.circular(8),
        child: AspectRatio(
          aspectRatio: 16 / 9,
          child: images.first,
        ),
      );
    }

    return GridView.count(
      shrinkWrap: true,
      physics: const NeverScrollableScrollPhysics(),
      crossAxisCount: 2,
      mainAxisSpacing: 4,
      crossAxisSpacing: 4,
      children: images.take(4).toList(),
    );
  }

  Widget _buildAuthorRow(BuildContext context) {
    return Row(
      children: [
        const CircleAvatar(
          radius: 20,
          child: Icon(Icons.person),
        ),
        const SizedBox(width: 12),
        Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              'Username',
              style: Theme.of(context).textTheme.titleSmall,
            ),
            Text(
              _formatTime(post.createdAt),
              style: Theme.of(context).textTheme.bodySmall,
            ),
          ],
        ),
        const Spacer(),
        if (post.isLocalOnly)
          Chip(
            label: const Text('Draft'),
            visualDensity: VisualDensity.compact,
            backgroundColor: Theme.of(context).colorScheme.secondaryContainer,
          ),
      ],
    );
  }

  Widget _buildActionsRow(BuildContext context, WidgetRef ref) {
    final theme = Theme.of(context);

    return Row(
      children: [
        // Like button with optimistic update
        _ActionButton(
          icon: post.isLikedByMe ? Icons.favorite : Icons.favorite_border,
          color: post.isLikedByMe ? Colors.red : null,
          label: post.likesCount.toString(),
          onTap: () => ref.read(postsProvider.notifier).toggleLike(post.id),
        ),
        const SizedBox(width: 16),
        _ActionButton(
          icon: Icons.chat_bubble_outline,
          label: post.commentsCount.toString(),
          onTap: () => _openComments(context),
        ),
        const SizedBox(width: 16),
        _ActionButton(
          icon: Icons.share_outlined,
          label: 'Share',
          onTap: () => _sharePost(context),
        ),
      ],
    );
  }

  String _formatTime(DateTime dateTime) {
    final now = DateTime.now();
    final difference = now.difference(dateTime);

    if (difference.inMinutes < 1) return 'Just now';
    if (difference.inHours < 1) return '${difference.inMinutes}m';
    if (difference.inDays < 1) return '${difference.inHours}h';
    if (difference.inDays < 7) return '${difference.inDays}d';
    return '${dateTime.month}/${dateTime.day}';
  }

  void _retrySync(BuildContext context) {
    // Trigger retry for this specific post
  }

  void _openComments(BuildContext context) {
    Navigator.of(context).pushNamed('/post/${post.id}/comments');
  }

  void _sharePost(BuildContext context) {
    // Share functionality
  }
}

class _ActionButton extends StatelessWidget {
  final IconData icon;
  final String label;
  final VoidCallback onTap;
  final Color? color;

  const _ActionButton({
    required this.icon,
    required this.label,
    required this.onTap,
    this.color,
  });

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      borderRadius: BorderRadius.circular(8),
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
        child: Row(
          children: [
            Icon(icon, size: 20, color: color),
            const SizedBox(width: 4),
            Text(label, style: TextStyle(color: color)),
          ],
        ),
      ),
    );
  }
}

class _NetworkImage extends StatelessWidget {
  final String url;

  const _NetworkImage({required this.url});

  @override
  Widget build(BuildContext context) {
    return Image.network(
      url,
      fit: BoxFit.cover,
      loadingBuilder: (context, child, loadingProgress) {
        if (loadingProgress == null) return child;
        return Container(
          color: Theme.of(context).colorScheme.surfaceContainerHighest,
          child: const Center(child: CircularProgressIndicator()),
        );
      },
      errorBuilder: (context, error, stack) {
        return Container(
          color: Theme.of(context).colorScheme.errorContainer,
          child: const Icon(Icons.broken_image),
        );
      },
    );
  }
}

class _LocalImage extends StatelessWidget {
  final String path;

  const _LocalImage({required this.path});

  @override
  Widget build(BuildContext context) {
    return Stack(
      fit: StackFit.expand,
      children: [
        Image.file(
          File(path),
          fit: BoxFit.cover,
        ),
        // Overlay to indicate pending upload
        Container(
          color: Colors.black26,
          child: const Center(
            child: Icon(
              Icons.cloud_upload,
              color: Colors.white,
              size: 32,
            ),
          ),
        ),
      ],
    );
  }
}

---

// lib/features/posts/presentation/screens/create_post_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:image_picker/image_picker.dart';
import 'dart:io';
import '../../providers/optimistic_post_provider.dart';
import '../../../../core/network/connectivity_service.dart';

class CreatePostScreen extends ConsumerStatefulWidget {
  const CreatePostScreen({super.key});

  @override
  ConsumerState<CreatePostScreen> createState() => _CreatePostScreenState();
}

class _CreatePostScreenState extends ConsumerState<CreatePostScreen> {
  final _contentController = TextEditingController();
  final _imagePicker = ImagePicker();
  final List<String> _selectedImages = [];
  bool _isPosting = false;

  @override
  Widget build(BuildContext context) {
    final isOnline = ref.watch(isOnlineProvider).valueOrNull ?? true;

    return Scaffold(
      appBar: AppBar(
        title: const Text('Create Post'),
        actions: [
          TextButton(
            onPressed: _canPost() ? _createPost : null,
            child: _isPosting
                ? const SizedBox(
                    width: 20,
                    height: 20,
                    child: CircularProgressIndicator(strokeWidth: 2),
                  )
                : const Text('Post'),
          ),
        ],
      ),
      body: Column(
        children: [
          // Offline banner
          if (!isOnline)
            Container(
              width: double.infinity,
              padding: const EdgeInsets.all(12),
              color: Theme.of(context).colorScheme.tertiaryContainer,
              child: Row(
                children: [
                  Icon(
                    Icons.cloud_off,
                    size: 20,
                    color: Theme.of(context).colorScheme.onTertiaryContainer,
                  ),
                  const SizedBox(width: 8),
                  Expanded(
                    child: Text(
                      'You\'re offline. Your post will be uploaded when you reconnect.',
                      style: TextStyle(
                        color: Theme.of(context).colorScheme.onTertiaryContainer,
                      ),
                    ),
                  ),
                ],
              ),
            ),

          Expanded(
            child: SingleChildScrollView(
              padding: const EdgeInsets.all(16),
              child: Column(
                children: [
                  TextField(
                    controller: _contentController,
                    maxLines: null,
                    minLines: 5,
                    decoration: const InputDecoration(
                      hintText: 'What\'s on your mind?',
                      border: InputBorder.none,
                    ),
                    onChanged: (_) => setState(() {}),
                  ),
                  const SizedBox(height: 16),
                  if (_selectedImages.isNotEmpty) _buildImagePreview(),
                ],
              ),
            ),
          ),

          // Bottom toolbar
          Container(
            padding: const EdgeInsets.all(8),
            decoration: BoxDecoration(
              border: Border(
                top: BorderSide(
                  color: Theme.of(context).dividerColor,
                ),
              ),
            ),
            child: Row(
              children: [
                IconButton(
                  icon: const Icon(Icons.photo_library),
                  onPressed: _pickImages,
                  tooltip: 'Add photos',
                ),
                IconButton(
                  icon: const Icon(Icons.camera_alt),
                  onPressed: _takePhoto,
                  tooltip: 'Take photo',
                ),
                const Spacer(),
                Text(
                  '${_contentController.text.length}/500',
                  style: Theme.of(context).textTheme.bodySmall,
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildImagePreview() {
    return GridView.count(
      shrinkWrap: true,
      physics: const NeverScrollableScrollPhysics(),
      crossAxisCount: 3,
      mainAxisSpacing: 8,
      crossAxisSpacing: 8,
      children: _selectedImages.map((path) {
        return Stack(
          children: [
            ClipRRect(
              borderRadius: BorderRadius.circular(8),
              child: Image.file(
                File(path),
                fit: BoxFit.cover,
                width: double.infinity,
                height: double.infinity,
              ),
            ),
            Positioned(
              top: 4,
              right: 4,
              child: GestureDetector(
                onTap: () => _removeImage(path),
                child: Container(
                  padding: const EdgeInsets.all(4),
                  decoration: const BoxDecoration(
                    color: Colors.black54,
                    shape: BoxShape.circle,
                  ),
                  child: const Icon(
                    Icons.close,
                    size: 16,
                    color: Colors.white,
                  ),
                ),
              ),
            ),
          ],
        );
      }).toList(),
    );
  }

  bool _canPost() {
    return !_isPosting &&
        (_contentController.text.trim().isNotEmpty ||
            _selectedImages.isNotEmpty);
  }

  Future<void> _pickImages() async {
    final images = await _imagePicker.pickMultiImage(
      maxWidth: 1080,
      imageQuality: 85,
    );

    setState(() {
      _selectedImages.addAll(images.map((img) => img.path));
    });
  }

  Future<void> _takePhoto() async {
    final image = await _imagePicker.pickImage(
      source: ImageSource.camera,
      maxWidth: 1080,
      imageQuality: 85,
    );

    if (image != null) {
      setState(() {
        _selectedImages.add(image.path);
      });
    }
  }

  void _removeImage(String path) {
    setState(() {
      _selectedImages.remove(path);
    });
  }

  Future<void> _createPost() async {
    if (!_canPost()) return;

    setState(() => _isPosting = true);

    try {
      await ref.read(postsProvider.notifier).createPost(
            content: _contentController.text.trim(),
            imagePaths: _selectedImages.isNotEmpty ? _selectedImages : null,
          );

      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text(
              ref.read(isOnlineProvider).valueOrNull ?? true
                  ? 'Post created!'
                  : 'Post saved. Will upload when online.',
            ),
          ),
        );
        Navigator.of(context).pop();
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('Failed to create post: $e'),
            backgroundColor: Theme.of(context).colorScheme.error,
          ),
        );
      }
    } finally {
      if (mounted) {
        setState(() => _isPosting = false);
      }
    }
  }

  @override
  void dispose() {
    _contentController.dispose();
    super.dispose();
  }
}
```
