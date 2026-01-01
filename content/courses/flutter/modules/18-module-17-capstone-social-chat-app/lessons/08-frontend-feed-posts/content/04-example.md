---
type: "EXAMPLE"
title: "Post Card Widget"
---


**Building the Post Card Component**

The PostCard widget displays user avatar, content, images, interaction counts, and action buttons with proper timestamp formatting and visual feedback.



```dart
// lib/features/feed/presentation/widgets/post_card.dart
import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';
import '../../../../shared/models/post.dart';
import 'post_images.dart';

class PostCard extends StatelessWidget {
  final Post post;
  final VoidCallback onLike;
  final VoidCallback onComment;
  final VoidCallback onBookmark;
  final VoidCallback onShare;
  final VoidCallback onTap;

  const PostCard({
    super.key,
    required this.post,
    required this.onLike,
    required this.onComment,
    required this.onBookmark,
    required this.onShare,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Card(
      margin: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      clipBehavior: Clip.antiAlias,
      child: InkWell(
        onTap: onTap,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // Header: Avatar, name, time, and menu
            _buildHeader(context, theme),

            // Content
            if (post.content.isNotEmpty)
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16),
                child: Text(
                  post.content,
                  style: theme.textTheme.bodyLarge,
                  maxLines: 10,
                  overflow: TextOverflow.ellipsis,
                ),
              ),

            // Images
            if (post.imageUrls.isNotEmpty) ...[
              const SizedBox(height: 12),
              PostImages(imageUrls: post.imageUrls),
            ],

            const SizedBox(height: 8),

            // Stats row
            _buildStatsRow(context, theme),

            const Divider(height: 1),

            // Action buttons
            _buildActionButtons(context, theme),
          ],
        ),
      ),
    );
  }

  Widget _buildHeader(BuildContext context, ThemeData theme) {
    return Padding(
      padding: const EdgeInsets.all(16),
      child: Row(
        children: [
          // Avatar
          GestureDetector(
            onTap: () => _navigateToProfile(context),
            child: CircleAvatar(
              radius: 20,
              backgroundColor: theme.colorScheme.primaryContainer,
              backgroundImage: post.author.avatarUrl != null
                  ? CachedNetworkImageProvider(post.author.avatarUrl!)
                  : null,
              child: post.author.avatarUrl == null
                  ? Text(
                      post.author.displayName[0].toUpperCase(),
                      style: TextStyle(
                        color: theme.colorScheme.onPrimaryContainer,
                        fontWeight: FontWeight.bold,
                      ),
                    )
                  : null,
            ),
          ),
          const SizedBox(width: 12),

          // Name and time
          Expanded(
            child: GestureDetector(
              onTap: () => _navigateToProfile(context),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    post.author.displayName,
                    style: theme.textTheme.titleSmall?.copyWith(
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  Text(
                    post.formattedTime,
                    style: theme.textTheme.bodySmall?.copyWith(
                      color: theme.colorScheme.onSurfaceVariant,
                    ),
                  ),
                ],
              ),
            ),
          ),

          // More options menu
          PopupMenuButton<String>(
            icon: const Icon(Icons.more_horiz),
            onSelected: (value) => _handleMenuAction(context, value),
            itemBuilder: (context) => [
              const PopupMenuItem(
                value: 'share',
                child: ListTile(
                  leading: Icon(Icons.share),
                  title: Text('Share'),
                  contentPadding: EdgeInsets.zero,
                ),
              ),
              const PopupMenuItem(
                value: 'report',
                child: ListTile(
                  leading: Icon(Icons.flag_outlined),
                  title: Text('Report'),
                  contentPadding: EdgeInsets.zero,
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }

  Widget _buildStatsRow(BuildContext context, ThemeData theme) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      child: Row(
        children: [
          if (post.likeCount > 0) ...[
            Icon(
              Icons.favorite,
              size: 16,
              color: theme.colorScheme.error,
            ),
            const SizedBox(width: 4),
            Text(
              _formatCount(post.likeCount),
              style: theme.textTheme.bodySmall?.copyWith(
                color: theme.colorScheme.onSurfaceVariant,
              ),
            ),
            const SizedBox(width: 16),
          ],
          if (post.commentCount > 0)
            Text(
              '${_formatCount(post.commentCount)} comments',
              style: theme.textTheme.bodySmall?.copyWith(
                color: theme.colorScheme.onSurfaceVariant,
              ),
            ),
        ],
      ),
    );
  }

  Widget _buildActionButtons(BuildContext context, ThemeData theme) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceAround,
        children: [
          // Like button
          _ActionButton(
            icon: post.isLikedByMe ? Icons.favorite : Icons.favorite_border,
            label: 'Like',
            isActive: post.isLikedByMe,
            activeColor: theme.colorScheme.error,
            onTap: onLike,
          ),

          // Comment button
          _ActionButton(
            icon: Icons.chat_bubble_outline,
            label: 'Comment',
            onTap: onComment,
          ),

          // Bookmark button
          _ActionButton(
            icon: post.isBookmarked ? Icons.bookmark : Icons.bookmark_border,
            label: 'Save',
            isActive: post.isBookmarked,
            activeColor: theme.colorScheme.primary,
            onTap: onBookmark,
          ),

          // Share button
          _ActionButton(
            icon: Icons.share_outlined,
            label: 'Share',
            onTap: onShare,
          ),
        ],
      ),
    );
  }

  String _formatCount(int count) {
    if (count >= 1000000) {
      return '${(count / 1000000).toStringAsFixed(1)}M';
    } else if (count >= 1000) {
      return '${(count / 1000).toStringAsFixed(1)}K';
    }
    return count.toString();
  }

  void _navigateToProfile(BuildContext context) {
    Navigator.of(context).pushNamed('/user/${post.author.id}');
  }

  void _handleMenuAction(BuildContext context, String action) {
    switch (action) {
      case 'share':
        onShare();
        break;
      case 'report':
        _showReportDialog(context);
        break;
    }
  }

  void _showReportDialog(BuildContext context) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Report Post'),
        content: const Text('Are you sure you want to report this post?'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
          FilledButton(
            onPressed: () {
              Navigator.pop(context);
              ScaffoldMessenger.of(context).showSnackBar(
                const SnackBar(content: Text('Post reported')),
              );
            },
            child: const Text('Report'),
          ),
        ],
      ),
    );
  }
}

class _ActionButton extends StatelessWidget {
  final IconData icon;
  final String label;
  final bool isActive;
  final Color? activeColor;
  final VoidCallback onTap;

  const _ActionButton({
    required this.icon,
    required this.label,
    this.isActive = false,
    this.activeColor,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final color = isActive
        ? (activeColor ?? theme.colorScheme.primary)
        : theme.colorScheme.onSurfaceVariant;

    return InkWell(
      onTap: onTap,
      borderRadius: BorderRadius.circular(8),
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
        child: Row(
          mainAxisSize: MainAxisSize.min,
          children: [
            Icon(icon, size: 20, color: color),
            const SizedBox(width: 4),
            Text(
              label,
              style: theme.textTheme.labelMedium?.copyWith(color: color),
            ),
          ],
        ),
      ),
    );
  }
}

---

// lib/features/feed/presentation/widgets/post_images.dart
import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';

class PostImages extends StatelessWidget {
  final List<String> imageUrls;

  const PostImages({super.key, required this.imageUrls});

  @override
  Widget build(BuildContext context) {
    if (imageUrls.isEmpty) return const SizedBox.shrink();

    if (imageUrls.length == 1) {
      return _buildSingleImage(context);
    }

    return _buildImageGrid(context);
  }

  Widget _buildSingleImage(BuildContext context) {
    return GestureDetector(
      onTap: () => _openImageViewer(context, 0),
      child: ConstrainedBox(
        constraints: const BoxConstraints(maxHeight: 400),
        child: CachedNetworkImage(
          imageUrl: imageUrls.first,
          width: double.infinity,
          fit: BoxFit.cover,
          placeholder: (context, url) => Container(
            height: 200,
            color: Theme.of(context).colorScheme.surfaceContainerHighest,
            child: const Center(child: CircularProgressIndicator()),
          ),
          errorWidget: (context, url, error) => Container(
            height: 200,
            color: Theme.of(context).colorScheme.errorContainer,
            child: Icon(
              Icons.broken_image,
              color: Theme.of(context).colorScheme.onErrorContainer,
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildImageGrid(BuildContext context) {
    final displayCount = imageUrls.length.clamp(1, 4);
    final remaining = imageUrls.length - 4;

    return LayoutBuilder(
      builder: (context, constraints) {
        final width = constraints.maxWidth;
        final halfWidth = (width - 4) / 2;

        return SizedBox(
          height: displayCount <= 2 ? 200 : 300,
          child: Row(
            children: [
              // Left column (or single image for 2 images)
              Expanded(
                child: displayCount == 2
                    ? _buildGridImage(context, 0, halfWidth, 200)
                    : Column(
                        children: [
                          Expanded(
                            child: _buildGridImage(context, 0, width, null),
                          ),
                          if (displayCount > 2) ...[
                            const SizedBox(height: 4),
                            Expanded(
                              child: _buildGridImage(context, 2, width, null),
                            ),
                          ],
                        ],
                      ),
              ),

              if (displayCount >= 2) ...[
                const SizedBox(width: 4),
                // Right column
                Expanded(
                  child: Column(
                    children: [
                      Expanded(
                        child: _buildGridImage(context, 1, width, null),
                      ),
                      if (displayCount > 3) ...[
                        const SizedBox(height: 4),
                        Expanded(
                          child: Stack(
                            fit: StackFit.expand,
                            children: [
                              _buildGridImage(context, 3, width, null),
                              if (remaining > 0)
                                Container(
                                  color: Colors.black54,
                                  child: Center(
                                    child: Text(
                                      '+$remaining',
                                      style: const TextStyle(
                                        color: Colors.white,
                                        fontSize: 24,
                                        fontWeight: FontWeight.bold,
                                      ),
                                    ),
                                  ),
                                ),
                            ],
                          ),
                        ),
                      ],
                    ],
                  ),
                ),
              ],
            ],
          ),
        );
      },
    );
  }

  Widget _buildGridImage(
    BuildContext context,
    int index,
    double width,
    double? height,
  ) {
    return GestureDetector(
      onTap: () => _openImageViewer(context, index),
      child: CachedNetworkImage(
        imageUrl: imageUrls[index],
        width: width,
        height: height,
        fit: BoxFit.cover,
        placeholder: (context, url) => Container(
          color: Theme.of(context).colorScheme.surfaceContainerHighest,
          child: const Center(
            child: CircularProgressIndicator(strokeWidth: 2),
          ),
        ),
        errorWidget: (context, url, error) => Container(
          color: Theme.of(context).colorScheme.errorContainer,
          child: const Icon(Icons.broken_image),
        ),
      ),
    );
  }

  void _openImageViewer(BuildContext context, int initialIndex) {
    Navigator.of(context).push(
      MaterialPageRoute(
        builder: (context) => ImageViewerScreen(
          imageUrls: imageUrls,
          initialIndex: initialIndex,
        ),
      ),
    );
  }
}

---

// lib/features/feed/presentation/screens/image_viewer_screen.dart
import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';
import 'package:photo_view/photo_view.dart';
import 'package:photo_view/photo_view_gallery.dart';

class ImageViewerScreen extends StatefulWidget {
  final List<String> imageUrls;
  final int initialIndex;

  const ImageViewerScreen({
    super.key,
    required this.imageUrls,
    this.initialIndex = 0,
  });

  @override
  State<ImageViewerScreen> createState() => _ImageViewerScreenState();
}

class _ImageViewerScreenState extends State<ImageViewerScreen> {
  late int _currentIndex;
  late PageController _pageController;

  @override
  void initState() {
    super.initState();
    _currentIndex = widget.initialIndex;
    _pageController = PageController(initialPage: widget.initialIndex);
  }

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.black,
      appBar: AppBar(
        backgroundColor: Colors.black,
        foregroundColor: Colors.white,
        title: widget.imageUrls.length > 1
            ? Text('${_currentIndex + 1} / ${widget.imageUrls.length}')
            : null,
      ),
      body: PhotoViewGallery.builder(
        pageController: _pageController,
        itemCount: widget.imageUrls.length,
        onPageChanged: (index) => setState(() => _currentIndex = index),
        builder: (context, index) {
          return PhotoViewGalleryPageOptions(
            imageProvider: CachedNetworkImageProvider(
              widget.imageUrls[index],
            ),
            minScale: PhotoViewComputedScale.contained,
            maxScale: PhotoViewComputedScale.covered * 3,
          );
        },
        loadingBuilder: (context, event) => const Center(
          child: CircularProgressIndicator(color: Colors.white),
        ),
      ),
    );
  }
}
```
