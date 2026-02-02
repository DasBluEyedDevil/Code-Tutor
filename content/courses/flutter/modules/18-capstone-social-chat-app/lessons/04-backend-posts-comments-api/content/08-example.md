---
type: "EXAMPLE"
title: "Comments Implementation"
---


**Complete Comments System**



```dart
# server/lib/src/protocol/comment.yaml
class: Comment
table: comments
fields:
  # Relationships
  postId: int, relation(parent=posts)
  authorId: int, relation(parent=user_profiles)
  parentId: int?  # Null for top-level comments
  
  # Content
  content: String
  
  # Threading
  depth: int  # 1 = top-level, 2 = reply, 3 = max nested
  replyCount: int  # Denormalized for performance
  
  # Engagement
  likesCount: int
  
  # Status
  isEdited: bool
  isDeleted: bool  # Soft delete - show "deleted" placeholder
  
  # Timestamps
  createdAt: DateTime
  updatedAt: DateTime?
  deletedAt: DateTime?

indexes:
  comment_post_idx:
    fields: postId, parentId, createdAt
  comment_author_idx:
    fields: authorId, createdAt
  comment_parent_idx:
    fields: parentId

---

# server/lib/src/protocol/comment_like.yaml
class: CommentLike
table: comment_likes
fields:
  commentId: int, relation(parent=comments)
  userId: int, relation(parent=user_profiles)
  createdAt: DateTime

indexes:
  comment_like_unique_idx:
    fields: commentId, userId
    unique: true

---

// server/lib/src/endpoints/comment_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';
import '../services/content_moderator.dart';
import '../services/notification_service.dart';

class CommentEndpoint extends Endpoint {
  static const int maxDepth = 3;
  static const int maxCommentLength = 2000;
  static const int defaultPageSize = 20;
  
  final ContentModerator _moderator = ContentModerator();
  final NotificationService _notifications = NotificationService();

  /// Add a comment to a post
  Future<Comment> addComment(
    Session session, {
    required int postId,
    required String content,
    int? parentId,
  }) async {
    // 1. Authenticate
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw CommentException(
        code: CommentErrorCode.unauthenticated,
        message: 'Please log in to comment',
      );
    }
    
    final author = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (author == null) {
      throw CommentException(
        code: CommentErrorCode.userNotFound,
        message: 'User not found',
      );
    }
    
    // 2. Validate post exists and allows comments
    final post = await Post.db.findById(session, postId);
    if (post == null || post.status == 'deleted') {
      throw CommentException(
        code: CommentErrorCode.postNotFound,
        message: 'Post not found',
      );
    }
    
    if (!post.allowComments) {
      throw CommentException(
        code: CommentErrorCode.commentsDisabled,
        message: 'Comments are disabled for this post',
      );
    }
    
    // 3. Validate content
    _validateContent(content);
    
    final moderationResult = await _moderator.checkContent(content);
    if (moderationResult.isBlocked) {
      throw CommentException(
        code: CommentErrorCode.contentBlocked,
        message: 'Your comment contains prohibited content',
      );
    }
    
    // 4. Handle threading
    int depth = 1;
    Comment? parentComment;
    
    if (parentId != null) {
      parentComment = await Comment.db.findById(session, parentId);
      
      if (parentComment == null || parentComment.isDeleted) {
        throw CommentException(
          code: CommentErrorCode.parentNotFound,
          message: 'Parent comment not found',
        );
      }
      
      if (parentComment.postId != postId) {
        throw CommentException(
          code: CommentErrorCode.invalidParent,
          message: 'Parent comment does not belong to this post',
        );
      }
      
      depth = parentComment.depth + 1;
      
      if (depth > maxDepth) {
        // Reply to the parent's parent instead (flatten at max depth)
        parentId = parentComment.parentId;
        depth = maxDepth;
      }
    }
    
    // 5. Create comment
    final comment = Comment(
      postId: postId,
      authorId: author.id!,
      parentId: parentId,
      content: content,
      depth: depth,
      replyCount: 0,
      likesCount: 0,
      isEdited: false,
      isDeleted: false,
      createdAt: DateTime.now(),
    );
    
    final savedComment = await Comment.db.insertRow(session, comment);
    
    // 6. Update counts
    await Post.db.updateRow(
      session,
      post.copyWith(commentsCount: post.commentsCount + 1),
    );
    
    if (parentComment != null) {
      await Comment.db.updateRow(
        session,
        parentComment.copyWith(
          replyCount: parentComment.replyCount + 1,
        ),
      );
    }
    
    // 7. Send notifications
    await _sendCommentNotifications(
      session,
      post,
      savedComment,
      author,
      parentComment,
    );
    
    return savedComment;
  }

  /// Get comments for a post with threading
  Future<CommentThread> getComments(
    Session session, {
    required int postId,
    String? cursor,
    int limit = defaultPageSize,
    String sortBy = 'newest',  // 'newest', 'oldest', 'top'
  }) async {
    final pageSize = limit.clamp(1, 50);
    
    // Get current user for like status
    final userId = await session.auth.authenticatedUserId;
    UserProfile? currentUser;
    if (userId != null) {
      currentUser = await UserProfile.db.findFirstRow(
        session,
        where: (t) => t.userInfoId.equals(userId),
      );
    }
    
    // Parse cursor
    DateTime? cursorTime;
    if (cursor != null) {
      cursorTime = DateTime.tryParse(cursor);
    }
    
    // Build query for top-level comments
    var whereClause = Comment.t.postId.equals(postId) &
        Comment.t.parentId.equals(null) &
        Comment.t.isDeleted.equals(false);
    
    if (cursorTime != null) {
      if (sortBy == 'oldest') {
        whereClause = whereClause & 
            Comment.t.createdAt.greaterThan(cursorTime);
      } else {
        whereClause = whereClause & 
            Comment.t.createdAt.lessThan(cursorTime);
      }
    }
    
    // Determine sort order
    final orderDescending = sortBy != 'oldest';
    final orderByField = sortBy == 'top' 
        ? Comment.t.likesCount 
        : Comment.t.createdAt;
    
    final topLevelComments = await Comment.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => orderByField,
      orderDescending: orderDescending,
      limit: pageSize,
    );
    
    // Build comment tree
    final commentTree = <CommentNode>[];
    
    for (final comment in topLevelComments) {
      final node = await _buildCommentNode(
        session,
        comment,
        currentUser,
        loadReplies: true,
        maxReplies: 3,  // Load first 3 replies
      );
      commentTree.add(node);
    }
    
    // Generate next cursor
    String? nextCursor;
    if (topLevelComments.length == pageSize) {
      final lastComment = topLevelComments.last;
      nextCursor = lastComment.createdAt.toIso8601String();
    }
    
    return CommentThread(
      comments: commentTree,
      nextCursor: nextCursor,
      hasMore: topLevelComments.length == pageSize,
      totalCount: await _getCommentCount(session, postId),
    );
  }

  /// Get replies to a comment
  Future<List<CommentNode>> getReplies(
    Session session, {
    required int commentId,
    String? cursor,
    int limit = 10,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    UserProfile? currentUser;
    if (userId != null) {
      currentUser = await UserProfile.db.findFirstRow(
        session,
        where: (t) => t.userInfoId.equals(userId),
      );
    }
    
    DateTime? cursorTime;
    if (cursor != null) {
      cursorTime = DateTime.tryParse(cursor);
    }
    
    var whereClause = Comment.t.parentId.equals(commentId) &
        Comment.t.isDeleted.equals(false);
    
    if (cursorTime != null) {
      whereClause = whereClause & 
          Comment.t.createdAt.greaterThan(cursorTime);
    }
    
    final replies = await Comment.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => t.createdAt,
      limit: limit,
    );
    
    final nodes = <CommentNode>[];
    for (final reply in replies) {
      nodes.add(await _buildCommentNode(
        session,
        reply,
        currentUser,
        loadReplies: reply.depth < maxDepth,
        maxReplies: 2,
      ));
    }
    
    return nodes;
  }

  /// Edit a comment
  Future<Comment> editComment(
    Session session, {
    required int commentId,
    required String content,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw CommentException(
        code: CommentErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    final comment = await Comment.db.findById(session, commentId);
    if (comment == null || comment.isDeleted) {
      throw CommentException(
        code: CommentErrorCode.commentNotFound,
        message: 'Comment not found',
      );
    }
    
    // Verify ownership
    final author = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (author?.id != comment.authorId) {
      throw CommentException(
        code: CommentErrorCode.accessDenied,
        message: 'You can only edit your own comments',
      );
    }
    
    // Check edit time limit (e.g., 15 minutes)
    final editWindow = Duration(minutes: 15);
    if (DateTime.now().difference(comment.createdAt) > editWindow) {
      throw CommentException(
        code: CommentErrorCode.editWindowExpired,
        message: 'Comments can only be edited within 15 minutes',
      );
    }
    
    // Validate content
    _validateContent(content);
    
    return Comment.db.updateRow(
      session,
      comment.copyWith(
        content: content,
        isEdited: true,
        updatedAt: DateTime.now(),
      ),
    );
  }

  /// Delete a comment (soft delete)
  Future<bool> deleteComment(
    Session session, {
    required int commentId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw CommentException(
        code: CommentErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    final comment = await Comment.db.findById(session, commentId);
    if (comment == null) {
      return true;  // Already deleted
    }
    
    // Verify ownership or moderator
    final author = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (author?.id != comment.authorId) {
      throw CommentException(
        code: CommentErrorCode.accessDenied,
        message: 'You can only delete your own comments',
      );
    }
    
    // Soft delete - keeps threading intact
    await Comment.db.updateRow(
      session,
      comment.copyWith(
        isDeleted: true,
        content: '[deleted]',
        deletedAt: DateTime.now(),
      ),
    );
    
    // Update post comment count
    final post = await Post.db.findById(session, comment.postId);
    if (post != null) {
      await Post.db.updateRow(
        session,
        post.copyWith(
          commentsCount: (post.commentsCount - 1).clamp(0, post.commentsCount),
        ),
      );
    }
    
    return true;
  }

  // Helper methods
  
  void _validateContent(String content) {
    if (content.trim().isEmpty) {
      throw CommentException(
        code: CommentErrorCode.invalidContent,
        message: 'Comment cannot be empty',
      );
    }
    
    if (content.length > maxCommentLength) {
      throw CommentException(
        code: CommentErrorCode.invalidContent,
        message: 'Comment cannot exceed $maxCommentLength characters',
      );
    }
  }
  
  Future<CommentNode> _buildCommentNode(
    Session session,
    Comment comment,
    UserProfile? currentUser, {
    bool loadReplies = false,
    int maxReplies = 3,
  }) async {
    // Get author
    final author = await UserProfile.db.findById(session, comment.authorId);
    
    // Check if liked
    bool isLiked = false;
    if (currentUser != null) {
      final like = await CommentLike.db.findFirstRow(
        session,
        where: (t) => t.commentId.equals(comment.id!) &
                      t.userId.equals(currentUser.id!),
      );
      isLiked = like != null;
    }
    
    // Load replies if needed
    List<CommentNode> replies = [];
    if (loadReplies && comment.replyCount > 0) {
      final replyComments = await Comment.db.find(
        session,
        where: (t) => t.parentId.equals(comment.id!) &
                      t.isDeleted.equals(false),
        orderBy: (t) => t.createdAt,
        limit: maxReplies,
      );
      
      for (final reply in replyComments) {
        replies.add(await _buildCommentNode(
          session,
          reply,
          currentUser,
          loadReplies: reply.depth < maxDepth,
          maxReplies: 2,
        ));
      }
    }
    
    return CommentNode(
      comment: comment,
      author: author,
      isLikedByCurrentUser: isLiked,
      replies: replies,
      hasMoreReplies: comment.replyCount > replies.length,
    );
  }
  
  Future<int> _getCommentCount(Session session, int postId) async {
    return Comment.db.count(
      session,
      where: (t) => t.postId.equals(postId) & t.isDeleted.equals(false),
    );
  }
  
  Future<void> _sendCommentNotifications(
    Session session,
    Post post,
    Comment comment,
    UserProfile author,
    Comment? parentComment,
  ) async {
    // Notify post author (if not self)
    if (post.authorId != author.id) {
      await _notifications.notifyComment(
        session,
        recipientId: post.authorId,
        commenterId: author.id!,
        postId: post.id!,
        commentPreview: _truncate(comment.content, 100),
      );
    }
    
    // Notify parent comment author (if reply and not self)
    if (parentComment != null && parentComment.authorId != author.id) {
      await _notifications.notifyReply(
        session,
        recipientId: parentComment.authorId,
        replierId: author.id!,
        postId: post.id!,
        commentId: parentComment.id!,
        replyPreview: _truncate(comment.content, 100),
      );
    }
  }
  
  String _truncate(String text, int maxLength) {
    if (text.length <= maxLength) return text;
    return '${text.substring(0, maxLength - 3)}...';
  }
}
```
