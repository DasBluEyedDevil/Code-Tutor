---
type: "EXAMPLE"
title: "Post CRUD Implementation"
---


**Complete Post Endpoint Implementation**



```dart
// server/lib/src/endpoints/post_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';
import '../services/content_moderator.dart';
import '../services/media_service.dart';
import '../services/notification_service.dart';

class PostEndpoint extends Endpoint {
  final ContentModerator _moderator = ContentModerator();
  final MediaService _mediaService = MediaService();
  final NotificationService _notifications = NotificationService();

  /// Create a new post
  Future<Post> createPost(
    Session session, {
    required String content,
    String contentType = 'text',
    String visibility = 'public',
    bool allowComments = true,
    List<MediaUpload>? mediaUploads,
    String? locationName,
    double? latitude,
    double? longitude,
  }) async {
    // 1. Authenticate
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw PostException(
        code: PostErrorCode.unauthenticated,
        message: 'Please log in to create a post',
      );
    }
    
    // Get user profile
    final author = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (author == null) {
      throw PostException(
        code: PostErrorCode.userNotFound,
        message: 'User profile not found',
      );
    }
    
    // 2. Validate content
    _validateContent(content);
    _validateVisibility(visibility);
    
    // 3. Check for moderation issues
    final moderationResult = await _moderator.checkContent(content);
    if (moderationResult.isBlocked) {
      throw PostException(
        code: PostErrorCode.contentBlocked,
        message: 'Your post contains prohibited content',
      );
    }
    
    // 4. Create the post
    final now = DateTime.now();
    final post = Post(
      authorId: author.id!,
      content: content,
      contentType: contentType,
      visibility: visibility,
      status: 'published',
      likesCount: 0,
      commentsCount: 0,
      sharesCount: 0,
      viewsCount: 0,
      isEdited: false,
      isPinned: false,
      allowComments: allowComments,
      locationName: locationName,
      latitude: latitude,
      longitude: longitude,
      publishedAt: now,
      createdAt: now,
    );
    
    final savedPost = await Post.db.insertRow(session, post);
    
    // 5. Process and save media
    if (mediaUploads != null && mediaUploads.isNotEmpty) {
      await _processMediaUploads(
        session,
        savedPost.id!,
        mediaUploads,
      );
    }
    
    // 6. Notify followers (async - don't await)
    _notifications.notifyNewPost(session, author.id!, savedPost.id!);
    
    // 7. Return with author info
    return _enrichPost(session, savedPost);
  }

  /// Get a single post by ID
  Future<Post?> getPost(
    Session session, {
    required int postId,
  }) async {
    final post = await Post.db.findById(session, postId);
    
    if (post == null || post.status == 'deleted') {
      return null;
    }
    
    // Check visibility permissions
    final canView = await _canViewPost(session, post);
    if (!canView) {
      throw PostException(
        code: PostErrorCode.accessDenied,
        message: 'You do not have permission to view this post',
      );
    }
    
    // Increment view count
    await Post.db.updateRow(
      session,
      post.copyWith(viewsCount: post.viewsCount + 1),
    );
    
    return _enrichPost(session, post);
  }

  /// Update a post
  Future<Post> updatePost(
    Session session, {
    required int postId,
    String? content,
    String? visibility,
    bool? allowComments,
    String? locationName,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw PostException(
        code: PostErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    final post = await Post.db.findById(session, postId);
    if (post == null || post.status == 'deleted') {
      throw PostException(
        code: PostErrorCode.postNotFound,
        message: 'Post not found',
      );
    }
    
    // Verify ownership
    final author = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (author == null || author.id != post.authorId) {
      throw PostException(
        code: PostErrorCode.accessDenied,
        message: 'You can only edit your own posts',
      );
    }
    
    // Validate new content if provided
    if (content != null) {
      _validateContent(content);
      final moderationResult = await _moderator.checkContent(content);
      if (moderationResult.isBlocked) {
        throw PostException(
          code: PostErrorCode.contentBlocked,
          message: 'Your post contains prohibited content',
        );
      }
    }
    
    if (visibility != null) {
      _validateVisibility(visibility);
    }
    
    // Update the post
    final updated = post.copyWith(
      content: content ?? post.content,
      visibility: visibility ?? post.visibility,
      allowComments: allowComments ?? post.allowComments,
      locationName: locationName ?? post.locationName,
      isEdited: true,
      updatedAt: DateTime.now(),
    );
    
    return Post.db.updateRow(session, updated);
  }

  /// Delete a post (soft delete)
  Future<bool> deletePost(
    Session session, {
    required int postId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw PostException(
        code: PostErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    final post = await Post.db.findById(session, postId);
    if (post == null) {
      return true;  // Already deleted
    }
    
    // Verify ownership (or moderator role)
    final author = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    final isModerator = await _isModerator(session, userId);
    
    if (author?.id != post.authorId && !isModerator) {
      throw PostException(
        code: PostErrorCode.accessDenied,
        message: 'You can only delete your own posts',
      );
    }
    
    // Soft delete
    await Post.db.updateRow(
      session,
      post.copyWith(
        status: 'deleted',
        deletedAt: DateTime.now(),
        updatedAt: DateTime.now(),
      ),
    );
    
    return true;
  }

  // Helper methods
  
  void _validateContent(String content) {
    if (content.trim().isEmpty) {
      throw PostException(
        code: PostErrorCode.invalidContent,
        message: 'Post content cannot be empty',
      );
    }
    
    if (content.length > 5000) {
      throw PostException(
        code: PostErrorCode.invalidContent,
        message: 'Post content cannot exceed 5000 characters',
      );
    }
  }
  
  void _validateVisibility(String visibility) {
    const validVisibilities = ['public', 'followers', 'private'];
    if (!validVisibilities.contains(visibility)) {
      throw PostException(
        code: PostErrorCode.invalidVisibility,
        message: 'Invalid visibility setting',
      );
    }
  }
  
  Future<bool> _canViewPost(Session session, Post post) async {
    // Public posts are always viewable
    if (post.visibility == 'public') {
      return true;
    }
    
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      return false;
    }
    
    final viewer = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    // Author can always view their posts
    if (viewer?.id == post.authorId) {
      return true;
    }
    
    // Private posts only visible to author
    if (post.visibility == 'private') {
      return false;
    }
    
    // Followers-only: check if viewer follows author
    if (post.visibility == 'followers' && viewer != null) {
      final isFollowing = await _isFollowing(session, viewer.id!, post.authorId);
      return isFollowing;
    }
    
    return false;
  }
  
  Future<bool> _isFollowing(Session session, int followerId, int followeeId) async {
    final follow = await Follow.db.findFirstRow(
      session,
      where: (t) => t.followerId.equals(followerId) & 
                    t.followeeId.equals(followeeId),
    );
    return follow != null;
  }
  
  Future<bool> _isModerator(Session session, int userId) async {
    // TODO: Implement moderator role check
    return false;
  }
  
  Future<void> _processMediaUploads(
    Session session,
    int postId,
    List<MediaUpload> uploads,
  ) async {
    for (var i = 0; i < uploads.length; i++) {
      final upload = uploads[i];
      
      // Process and store media
      final mediaInfo = await _mediaService.processUpload(upload);
      
      await PostMedia.db.insertRow(
        session,
        PostMedia(
          postId: postId,
          mediaType: mediaInfo.type,
          url: mediaInfo.url,
          thumbnailUrl: mediaInfo.thumbnailUrl,
          width: mediaInfo.width,
          height: mediaInfo.height,
          durationSeconds: mediaInfo.duration,
          mimeType: mediaInfo.mimeType,
          sizeBytes: mediaInfo.size,
          order: i,
          altText: upload.altText,
          createdAt: DateTime.now(),
        ),
      );
    }
  }
  
  Future<Post> _enrichPost(Session session, Post post) async {
    // Load author info
    // Load media
    // Check if current user liked
    // This would use Serverpod's include feature
    return post;
  }
}
```
