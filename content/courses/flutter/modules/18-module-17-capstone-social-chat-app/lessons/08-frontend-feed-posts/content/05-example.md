---
type: "EXAMPLE"
title: "Create Post Screen"
---


**Building the Create Post Screen**

The create post screen provides a clean composition UI with image picker integration, posting with loading state, and navigation back to the feed with the new post.



```dart
// lib/features/feed/presentation/screens/create_post_screen.dart
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:image_picker/image_picker.dart';
import '../../providers/feed_provider.dart';
import '../../providers/create_post_provider.dart';

class CreatePostScreen extends ConsumerStatefulWidget {
  const CreatePostScreen({super.key});

  @override
  ConsumerState<CreatePostScreen> createState() => _CreatePostScreenState();
}

class _CreatePostScreenState extends ConsumerState<CreatePostScreen> {
  final _contentController = TextEditingController();
  final _focusNode = FocusNode();
  final _imagePicker = ImagePicker();
  final List<XFile> _selectedImages = [];
  bool _isPosting = false;

  @override
  void initState() {
    super.initState();
    // Auto-focus the text field
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _focusNode.requestFocus();
    });
  }

  @override
  void dispose() {
    _contentController.dispose();
    _focusNode.dispose();
    super.dispose();
  }

  bool get _canPost =>
      _contentController.text.trim().isNotEmpty || _selectedImages.isNotEmpty;

  Future<void> _pickImages() async {
    if (_selectedImages.length >= 4) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Maximum 4 images allowed')),
      );
      return;
    }

    final remaining = 4 - _selectedImages.length;

    final images = await _imagePicker.pickMultiImage(
      maxWidth: 1920,
      maxHeight: 1920,
      imageQuality: 85,
    );

    if (images.isNotEmpty) {
      setState(() {
        _selectedImages.addAll(images.take(remaining));
      });
    }
  }

  Future<void> _takePhoto() async {
    if (_selectedImages.length >= 4) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Maximum 4 images allowed')),
      );
      return;
    }

    final image = await _imagePicker.pickImage(
      source: ImageSource.camera,
      maxWidth: 1920,
      maxHeight: 1920,
      imageQuality: 85,
    );

    if (image != null) {
      setState(() {
        _selectedImages.add(image);
      });
    }
  }

  void _removeImage(int index) {
    setState(() {
      _selectedImages.removeAt(index);
    });
  }

  Future<void> _createPost() async {
    if (!_canPost || _isPosting) return;

    setState(() => _isPosting = true);

    try {
      // Upload images first if any
      List<String>? imageUrls;
      if (_selectedImages.isNotEmpty) {
        imageUrls = await ref.read(createPostProvider.notifier).uploadImages(
          _selectedImages.map((f) => File(f.path)).toList(),
        );
      }

      // Create the post
      final post = await ref.read(createPostProvider.notifier).createPost(
        content: _contentController.text.trim(),
        imageUrls: imageUrls,
      );

      // Add to feed
      ref.read(feedProvider.notifier).addPost(post);

      if (mounted) {
        Navigator.of(context).pop();
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Post created successfully!')),
        );
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Failed to create post: $e')),
        );
      }
    } finally {
      if (mounted) {
        setState(() => _isPosting = false);
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final user = ref.watch(currentUserProvider);

    return Scaffold(
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.close),
          onPressed: _isPosting ? null : () => _confirmClose(context),
        ),
        title: const Text('Create Post'),
        actions: [
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 8),
            child: FilledButton(
              onPressed: _canPost && !_isPosting ? _createPost : null,
              child: _isPosting
                  ? const SizedBox(
                      width: 20,
                      height: 20,
                      child: CircularProgressIndicator(
                        strokeWidth: 2,
                        color: Colors.white,
                      ),
                    )
                  : const Text('Post'),
            ),
          ),
        ],
      ),
      body: Column(
        children: [
          // Main content area
          Expanded(
            child: SingleChildScrollView(
              padding: const EdgeInsets.all(16),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  // User info row
                  Row(
                    children: [
                      CircleAvatar(
                        radius: 24,
                        backgroundColor: theme.colorScheme.primaryContainer,
                        backgroundImage: user?.imageUrl != null
                            ? NetworkImage(user!.imageUrl!)
                            : null,
                        child: user?.imageUrl == null
                            ? Text(
                                user?.userName?[0].toUpperCase() ?? '?',
                                style: TextStyle(
                                  color: theme.colorScheme.onPrimaryContainer,
                                  fontWeight: FontWeight.bold,
                                ),
                              )
                            : null,
                      ),
                      const SizedBox(width: 12),
                      Text(
                        user?.userName ?? 'User',
                        style: theme.textTheme.titleMedium?.copyWith(
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 16),

                  // Content text field
                  TextField(
                    controller: _contentController,
                    focusNode: _focusNode,
                    enabled: !_isPosting,
                    maxLines: null,
                    minLines: 5,
                    maxLength: 500,
                    decoration: const InputDecoration(
                      hintText: "What's on your mind?",
                      border: InputBorder.none,
                      counterText: '',
                    ),
                    style: theme.textTheme.bodyLarge,
                    onChanged: (_) => setState(() {}),
                  ),

                  // Selected images preview
                  if (_selectedImages.isNotEmpty) ...[
                    const SizedBox(height: 16),
                    _buildImagePreview(),
                  ],
                ],
              ),
            ),
          ),

          // Character count
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16),
            child: Align(
              alignment: Alignment.centerRight,
              child: Text(
                '${_contentController.text.length}/500',
                style: theme.textTheme.bodySmall?.copyWith(
                  color: _contentController.text.length > 450
                      ? theme.colorScheme.error
                      : theme.colorScheme.onSurfaceVariant,
                ),
              ),
            ),
          ),

          // Bottom toolbar
          Container(
            decoration: BoxDecoration(
              border: Border(
                top: BorderSide(color: theme.dividerColor),
              ),
            ),
            padding: const EdgeInsets.all(8),
            child: Row(
              children: [
                IconButton(
                  icon: const Icon(Icons.image_outlined),
                  onPressed: _isPosting ? null : _pickImages,
                  tooltip: 'Add images',
                ),
                IconButton(
                  icon: const Icon(Icons.camera_alt_outlined),
                  onPressed: _isPosting ? null : _takePhoto,
                  tooltip: 'Take photo',
                ),
                const Spacer(),
                Text(
                  '${_selectedImages.length}/4 images',
                  style: theme.textTheme.bodySmall?.copyWith(
                    color: theme.colorScheme.onSurfaceVariant,
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildImagePreview() {
    return SizedBox(
      height: 120,
      child: ListView.separated(
        scrollDirection: Axis.horizontal,
        itemCount: _selectedImages.length,
        separatorBuilder: (_, __) => const SizedBox(width: 8),
        itemBuilder: (context, index) {
          final image = _selectedImages[index];
          return Stack(
            children: [
              ClipRRect(
                borderRadius: BorderRadius.circular(8),
                child: Image.file(
                  File(image.path),
                  width: 120,
                  height: 120,
                  fit: BoxFit.cover,
                ),
              ),
              Positioned(
                top: 4,
                right: 4,
                child: GestureDetector(
                  onTap: _isPosting ? null : () => _removeImage(index),
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
        },
      ),
    );
  }

  void _confirmClose(BuildContext context) {
    if (_contentController.text.isEmpty && _selectedImages.isEmpty) {
      Navigator.of(context).pop();
      return;
    }

    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Discard Post?'),
        content: const Text(
          'You have unsaved changes. Are you sure you want to discard this post?',
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Keep Editing'),
          ),
          FilledButton(
            onPressed: () {
              Navigator.pop(context);
              Navigator.pop(context);
            },
            child: const Text('Discard'),
          ),
        ],
      ),
    );
  }
}

---

// lib/features/feed/providers/create_post_provider.dart
import 'dart:io';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../data/feed_repository.dart';
import '../../../shared/models/post.dart';
import '../../../core/services/upload_service.dart';

class CreatePostNotifier extends Notifier<CreatePostState> {
  @override
  CreatePostState build() {
    return const CreatePostState();
  }

  /// Upload images and return URLs
  Future<List<String>> uploadImages(List<File> images) async {
    state = state.copyWith(isUploading: true, uploadProgress: 0);

    try {
      final uploadService = ref.read(uploadServiceProvider);
      final urls = <String>[];

      for (int i = 0; i < images.length; i++) {
        final url = await uploadService.uploadImage(images[i]);
        urls.add(url);
        state = state.copyWith(
          uploadProgress: (i + 1) / images.length,
        );
      }

      state = state.copyWith(isUploading: false, uploadProgress: 1);
      return urls;
    } catch (e) {
      state = state.copyWith(isUploading: false, error: e.toString());
      rethrow;
    }
  }

  /// Create a post
  Future<Post> createPost({
    required String content,
    List<String>? imageUrls,
  }) async {
    state = state.copyWith(isPosting: true, error: null);

    try {
      final repository = ref.read(feedRepositoryProvider);
      final post = await repository.createPost(
        content: content,
        imageUrls: imageUrls,
      );

      state = state.copyWith(isPosting: false);
      return post;
    } catch (e) {
      state = state.copyWith(isPosting: false, error: e.toString());
      rethrow;
    }
  }
}

class CreatePostState {
  final bool isUploading;
  final bool isPosting;
  final double uploadProgress;
  final String? error;

  const CreatePostState({
    this.isUploading = false,
    this.isPosting = false,
    this.uploadProgress = 0,
    this.error,
  });

  CreatePostState copyWith({
    bool? isUploading,
    bool? isPosting,
    double? uploadProgress,
    String? error,
  }) {
    return CreatePostState(
      isUploading: isUploading ?? this.isUploading,
      isPosting: isPosting ?? this.isPosting,
      uploadProgress: uploadProgress ?? this.uploadProgress,
      error: error,
    );
  }

  bool get isLoading => isUploading || isPosting;
}

final createPostProvider =
    NotifierProvider<CreatePostNotifier, CreatePostState>(() {
  return CreatePostNotifier();
});

// Provider reference for current user
final currentUserProvider = Provider((ref) {
  // This would come from your auth state
  return ref.watch(authProvider).valueOrNull?.userInfo;
});
```
