---
type: "EXAMPLE"
title: "Like System Implementation"
---


**Complete Like/Unlike Implementation**



```dart
// server/lib/src/endpoints/like_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';
import '../services/notification_service.dart';

class LikeEndpoint extends Endpoint {
  final NotificationService _notifications = NotificationService();

  /// Like or unlike a post (toggle)
  Future<LikeResult> togglePostLike(
    Session session, {
    required int postId,
    String reactionType = 'like',
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw LikeException(
        code: LikeErrorCode.unauthenticated,
        message: 'Please log in to like posts',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw LikeException(
        code: LikeErrorCode.userNotFound,
        message: 'User not found',
      );
    }
    
    // Validate post exists
    final post = await Post.db.findById(session, postId);
    if (post == null || post.status == 'deleted') {
      throw LikeException(
        code: LikeErrorCode.postNotFound,
        message: 'Post not found',
      );
    }
    
    // Check for existing like
    final existingLike = await PostLike.db.findFirstRow(
      session,
      where: (t) => t.postId.equals(postId) & t.userId.equals(user.id!),
    );
    
    if (existingLike != null) {
      // Unlike or change reaction
      if (existingLike.reactionType == reactionType) {
        // Same reaction - unlike (remove)
        await PostLike.db.deleteRow(session, existingLike);
        
        // Decrement count atomically
        await _decrementLikeCount(session, postId);
        
        return LikeResult(
          isLiked: false,
          reactionType: null,
          newCount: post.likesCount - 1,
        );
      } else {
        // Different reaction - update
        await PostLike.db.updateRow(
          session,
          existingLike.copyWith(
            reactionType: reactionType,
          ),
        );
        
        return LikeResult(
          isLiked: true,
          reactionType: reactionType,
          newCount: post.likesCount,
        );
      }
    } else {
      // New like
      await PostLike.db.insertRow(
        session,
        PostLike(
          postId: postId,
          userId: user.id!,
          reactionType: reactionType,
          createdAt: DateTime.now(),
        ),
      );
      
      // Increment count atomically
      await _incrementLikeCount(session, postId);
      
      // Notify post author (async)
      if (post.authorId != user.id) {
        _notifications.notifyLike(
          session,
          recipientId: post.authorId,
          likerId: user.id!,
          postId: postId,
          reactionType: reactionType,
        );
      }
      
      return LikeResult(
        isLiked: true,
        reactionType: reactionType,
        newCount: post.likesCount + 1,
      );
    }
  }

  /// Like or unlike a comment (toggle)
  Future<LikeResult> toggleCommentLike(
    Session session, {
    required int commentId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw LikeException(
        code: LikeErrorCode.unauthenticated,
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw LikeException(
        code: LikeErrorCode.userNotFound,
        message: 'User not found',
      );
    }
    
    final comment = await Comment.db.findById(session, commentId);
    if (comment == null || comment.isDeleted) {
      throw LikeException(
        code: LikeErrorCode.commentNotFound,
        message: 'Comment not found',
      );
    }
    
    final existingLike = await CommentLike.db.findFirstRow(
      session,
      where: (t) => t.commentId.equals(commentId) & t.userId.equals(user.id!),
    );
    
    if (existingLike != null) {
      // Unlike
      await CommentLike.db.deleteRow(session, existingLike);
      
      await Comment.db.updateRow(
        session,
        comment.copyWith(
          likesCount: (comment.likesCount - 1).clamp(0, comment.likesCount),
        ),
      );
      
      return LikeResult(
        isLiked: false,
        reactionType: null,
        newCount: comment.likesCount - 1,
      );
    } else {
      // Like
      await CommentLike.db.insertRow(
        session,
        CommentLike(
          commentId: commentId,
          userId: user.id!,
          createdAt: DateTime.now(),
        ),
      );
      
      await Comment.db.updateRow(
        session,
        comment.copyWith(likesCount: comment.likesCount + 1),
      );
      
      // Notify comment author
      if (comment.authorId != user.id) {
        _notifications.notifyCommentLike(
          session,
          recipientId: comment.authorId,
          likerId: user.id!,
          commentId: commentId,
        );
      }
      
      return LikeResult(
        isLiked: true,
        reactionType: 'like',
        newCount: comment.likesCount + 1,
      );
    }
  }

  /// Get users who liked a post
  Future<PaginatedLikers> getPostLikers(
    Session session, {
    required int postId,
    String? cursor,
    int limit = 20,
  }) async {
    DateTime? cursorTime;
    if (cursor != null) {
      cursorTime = DateTime.tryParse(cursor);
    }
    
    var whereClause = PostLike.t.postId.equals(postId);
    if (cursorTime != null) {
      whereClause = whereClause & PostLike.t.createdAt.lessThan(cursorTime);
    }
    
    final likes = await PostLike.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => t.createdAt,
      orderDescending: true,
      limit: limit,
    );
    
    // Get user profiles
    final likers = <LikerInfo>[];
    for (final like in likes) {
      final user = await UserProfile.db.findById(session, like.userId);
      if (user != null && !user.isDeleted) {
        likers.add(LikerInfo(
          user: user,
          reactionType: like.reactionType,
          likedAt: like.createdAt,
        ));
      }
    }
    
    String? nextCursor;
    if (likes.length == limit) {
      nextCursor = likes.last.createdAt.toIso8601String();
    }
    
    return PaginatedLikers(
      likers: likers,
      nextCursor: nextCursor,
      hasMore: likes.length == limit,
    );
  }

  /// Get reaction counts by type for a post
  Future<Map<String, int>> getReactionCounts(
    Session session, {
    required int postId,
  }) async {
    final likes = await PostLike.db.find(
      session,
      where: (t) => t.postId.equals(postId),
    );
    
    final counts = <String, int>{};
    for (final like in likes) {
      counts[like.reactionType] = (counts[like.reactionType] ?? 0) + 1;
    }
    
    return counts;
  }

  /// Check if current user liked a list of posts
  Future<Map<int, LikeStatus>> batchCheckLiked(
    Session session, {
    required List<int> postIds,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      return {for (final id in postIds) id: LikeStatus(isLiked: false)};
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      return {for (final id in postIds) id: LikeStatus(isLiked: false)};
    }
    
    final likes = await PostLike.db.find(
      session,
      where: (t) => t.postId.inSet(postIds.toSet()) & 
                    t.userId.equals(user.id!),
    );
    
    final likeMap = <int, LikeStatus>{};
    for (final id in postIds) {
      final like = likes.firstWhere(
        (l) => l.postId == id,
        orElse: () => PostLike(
          postId: id,
          userId: 0,
          reactionType: '',
          createdAt: DateTime.now(),
        ),
      );
      
      likeMap[id] = LikeStatus(
        isLiked: like.userId == user.id,
        reactionType: like.userId == user.id ? like.reactionType : null,
      );
    }
    
    return likeMap;
  }

  // Atomic count updates using raw SQL
  
  Future<void> _incrementLikeCount(Session session, int postId) async {
    await session.db.unsafeExecute(
      'UPDATE posts SET likes_count = likes_count + 1 WHERE id = @postId',
      parameters: QueryParameters.named({'postId': postId}),
    );
  }
  
  Future<void> _decrementLikeCount(Session session, int postId) async {
    await session.db.unsafeExecute(
      'UPDATE posts SET likes_count = GREATEST(likes_count - 1, 0) WHERE id = @postId',
      parameters: QueryParameters.named({'postId': postId}),
    );
  }
}

/// Like operation result
class LikeResult {
  final bool isLiked;
  final String? reactionType;
  final int newCount;
  
  LikeResult({
    required this.isLiked,
    this.reactionType,
    required this.newCount,
  });
}

/// Like status for a single post
class LikeStatus {
  final bool isLiked;
  final String? reactionType;
  
  LikeStatus({required this.isLiked, this.reactionType});
}
```
