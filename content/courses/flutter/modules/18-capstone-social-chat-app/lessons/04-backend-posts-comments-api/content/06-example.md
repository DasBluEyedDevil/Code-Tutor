---
type: "EXAMPLE"
title: "Feed Implementation with Cursor Pagination"
---


**Implementing Efficient Feed Pagination**



```dart
// server/lib/src/endpoints/feed_endpoint.dart
import 'dart:convert';
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class FeedEndpoint extends Endpoint {
  static const int defaultPageSize = 20;
  static const int maxPageSize = 50;

  /// Get paginated feed with cursor
  Future<PaginatedFeed> getFeed(
    Session session, {
    String? cursor,
    int limit = defaultPageSize,
    String feedType = 'home',  // 'home', 'explore', 'following'
  }) async {
    final pageSize = limit.clamp(1, maxPageSize);
    
    // Decode cursor if provided
    FeedCursor? decodedCursor;
    if (cursor != null) {
      decodedCursor = _decodeCursor(cursor);
    }
    
    // Get current user (optional for explore feed)
    final userId = await session.auth.authenticatedUserId;
    UserProfile? currentUser;
    if (userId != null) {
      currentUser = await UserProfile.db.findFirstRow(
        session,
        where: (t) => t.userInfoId.equals(userId),
      );
    }
    
    // Build query based on feed type
    List<Post> posts;
    switch (feedType) {
      case 'following':
        posts = await _getFollowingFeed(
          session,
          currentUser,
          decodedCursor,
          pageSize,
        );
        break;
      case 'explore':
        posts = await _getExploreFeed(
          session,
          decodedCursor,
          pageSize,
        );
        break;
      case 'trending':
        posts = await _getTrendingFeed(
          session,
          decodedCursor,
          pageSize,
        );
        break;
      case 'home':
      default:
        posts = await _getHomeFeed(
          session,
          currentUser,
          decodedCursor,
          pageSize,
        );
    }
    
    // Generate next cursor
    String? nextCursor;
    if (posts.length == pageSize) {
      final lastPost = posts.last;
      nextCursor = _encodeCursor(FeedCursor(
        timestamp: lastPost.publishedAt ?? lastPost.createdAt,
        id: lastPost.id!,
      ));
    }
    
    // Enrich posts with additional data
    final enrichedPosts = await _enrichPosts(session, posts, currentUser);
    
    return PaginatedFeed(
      posts: enrichedPosts,
      nextCursor: nextCursor,
      hasMore: posts.length == pageSize,
    );
  }

  /// Home feed: Following + popular public posts
  Future<List<Post>> _getHomeFeed(
    Session session,
    UserProfile? currentUser,
    FeedCursor? cursor,
    int limit,
  ) async {
    if (currentUser == null) {
      // Fall back to explore for non-authenticated users
      return _getExploreFeed(session, cursor, limit);
    }
    
    // Get IDs of users being followed
    final following = await Follow.db.find(
      session,
      where: (t) => t.followerId.equals(currentUser.id!),
    );
    final followingIds = following.map((f) => f.followeeId).toList();
    
    // Include own posts and followed users' posts
    followingIds.add(currentUser.id!);
    
    // Build where clause
    var whereClause = Post.t.status.equals('published') &
        Post.t.authorId.inSet(followingIds.toSet());
    
    if (cursor != null) {
      // Composite cursor: timestamp + id for tie-breaking
      whereClause = whereClause &
          (Post.t.publishedAt.lessThan(cursor.timestamp) |
           (Post.t.publishedAt.equals(cursor.timestamp) & 
            Post.t.id.lessThan(cursor.id)));
    }
    
    return Post.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => t.publishedAt,
      orderDescending: true,
      limit: limit,
    );
  }

  /// Following feed: Only posts from followed users
  Future<List<Post>> _getFollowingFeed(
    Session session,
    UserProfile? currentUser,
    FeedCursor? cursor,
    int limit,
  ) async {
    if (currentUser == null) {
      return [];
    }
    
    final following = await Follow.db.find(
      session,
      where: (t) => t.followerId.equals(currentUser.id!),
    );
    
    if (following.isEmpty) {
      return [];
    }
    
    final followingIds = following.map((f) => f.followeeId).toSet();
    
    var whereClause = Post.t.status.equals('published') &
        Post.t.visibility.inSet({'public', 'followers'}) &
        Post.t.authorId.inSet(followingIds);
    
    if (cursor != null) {
      whereClause = whereClause &
          Post.t.publishedAt.lessThan(cursor.timestamp);
    }
    
    return Post.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => t.publishedAt,
      orderDescending: true,
      limit: limit,
    );
  }

  /// Explore feed: Popular public posts
  Future<List<Post>> _getExploreFeed(
    Session session,
    FeedCursor? cursor,
    int limit,
  ) async {
    var whereClause = Post.t.status.equals('published') &
        Post.t.visibility.equals('public');
    
    if (cursor != null) {
      whereClause = whereClause &
          Post.t.publishedAt.lessThan(cursor.timestamp);
    }
    
    return Post.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => t.publishedAt,
      orderDescending: true,
      limit: limit,
    );
  }

  /// Trending feed: Posts with high engagement recently
  Future<List<Post>> _getTrendingFeed(
    Session session,
    FeedCursor? cursor,
    int limit,
  ) async {
    final recentCutoff = DateTime.now().subtract(Duration(hours: 24));
    
    var whereClause = Post.t.status.equals('published') &
        Post.t.visibility.equals('public') &
        Post.t.publishedAt.greaterThan(recentCutoff);
    
    if (cursor != null) {
      // For trending, cursor is based on engagement score
      // Using likesCount as a simple proxy
      whereClause = whereClause &
          Post.t.likesCount.lessThan(cursor.id);  // Reusing id field for score
    }
    
    return Post.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => t.likesCount,
      orderDescending: true,
      limit: limit,
    );
  }

  /// Get posts by a specific user
  Future<PaginatedFeed> getUserPosts(
    Session session, {
    required int userId,
    String? cursor,
    int limit = defaultPageSize,
  }) async {
    final pageSize = limit.clamp(1, maxPageSize);
    
    // Get current user for visibility check
    final currentUserId = await session.auth.authenticatedUserId;
    UserProfile? currentUser;
    if (currentUserId != null) {
      currentUser = await UserProfile.db.findFirstRow(
        session,
        where: (t) => t.userInfoId.equals(currentUserId),
      );
    }
    
    // Determine visibility filter
    Set<String> visibleTypes = {'public'};
    if (currentUser != null) {
      if (currentUser.id == userId) {
        // Own posts - see everything
        visibleTypes = {'public', 'followers', 'private'};
      } else {
        // Check if following
        final isFollowing = await Follow.db.findFirstRow(
          session,
          where: (t) => t.followerId.equals(currentUser!.id!) &
                        t.followeeId.equals(userId),
        );
        if (isFollowing != null) {
          visibleTypes = {'public', 'followers'};
        }
      }
    }
    
    FeedCursor? decodedCursor;
    if (cursor != null) {
      decodedCursor = _decodeCursor(cursor);
    }
    
    var whereClause = Post.t.authorId.equals(userId) &
        Post.t.status.equals('published') &
        Post.t.visibility.inSet(visibleTypes);
    
    if (decodedCursor != null) {
      whereClause = whereClause &
          Post.t.publishedAt.lessThan(decodedCursor.timestamp);
    }
    
    final posts = await Post.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => t.publishedAt,
      orderDescending: true,
      limit: pageSize,
    );
    
    String? nextCursor;
    if (posts.length == pageSize) {
      final lastPost = posts.last;
      nextCursor = _encodeCursor(FeedCursor(
        timestamp: lastPost.publishedAt ?? lastPost.createdAt,
        id: lastPost.id!,
      ));
    }
    
    return PaginatedFeed(
      posts: await _enrichPosts(session, posts, currentUser),
      nextCursor: nextCursor,
      hasMore: posts.length == pageSize,
    );
  }

  // Cursor encoding/decoding
  
  String _encodeCursor(FeedCursor cursor) {
    final json = {
      't': cursor.timestamp.toIso8601String(),
      'i': cursor.id,
    };
    return base64Url.encode(utf8.encode(jsonEncode(json)));
  }
  
  FeedCursor? _decodeCursor(String encoded) {
    try {
      final json = jsonDecode(utf8.decode(base64Url.decode(encoded)));
      return FeedCursor(
        timestamp: DateTime.parse(json['t']),
        id: json['i'],
      );
    } catch (e) {
      return null;  // Invalid cursor, start from beginning
    }
  }
  
  Future<List<EnrichedPost>> _enrichPosts(
    Session session,
    List<Post> posts,
    UserProfile? currentUser,
  ) async {
    // Load authors, media, and like status in batch
    final enriched = <EnrichedPost>[];
    
    for (final post in posts) {
      // Get author
      final author = await UserProfile.db.findById(session, post.authorId);
      
      // Get media
      final media = await PostMedia.db.find(
        session,
        where: (t) => t.postId.equals(post.id!),
        orderBy: (t) => t.order,
      );
      
      // Check if current user liked
      bool isLiked = false;
      if (currentUser != null) {
        final like = await PostLike.db.findFirstRow(
          session,
          where: (t) => t.postId.equals(post.id!) & 
                        t.userId.equals(currentUser.id!),
        );
        isLiked = like != null;
      }
      
      enriched.add(EnrichedPost(
        post: post,
        author: author,
        media: media,
        isLikedByCurrentUser: isLiked,
      ));
    }
    
    return enriched;
  }
}

/// Cursor for pagination
class FeedCursor {
  final DateTime timestamp;
  final int id;
  
  FeedCursor({required this.timestamp, required this.id});
}
```
