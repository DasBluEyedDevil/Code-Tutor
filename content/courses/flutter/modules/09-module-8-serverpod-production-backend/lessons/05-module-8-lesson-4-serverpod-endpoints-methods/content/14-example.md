---
type: "EXAMPLE"
title: "Complete Real-World Endpoint"
---

Here's a production-ready endpoint with all best practices applied.


```dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

/// Endpoint for blog post operations.
///
/// Handles creating, reading, updating, and deleting blog posts.
/// Some operations require authentication.
class PostEndpoint extends Endpoint {

  // === PUBLIC METHODS (No auth required) ===

  /// Get a published post by ID.
  /// Returns null if post doesn't exist or is not published.
  Future<Post?> getPost(Session session, int postId) async {
    final post = await Post.db.findById(
      session,
      postId,
      include: Post.include(
        author: User.include(),  // Include author data
      ),
    );

    // Only return published posts publicly
    if (post == null || !post.isPublished) {
      return null;
    }

    // Increment view count (fire and forget)
    _incrementViewCount(session, postId);

    return post;
  }

  /// List published posts with pagination.
  Future<List<Post>> listPosts(
    Session session, {
    int limit = 20,
    int offset = 0,
    String? tag,
  }) async {
    return await Post.db.find(
      session,
      where: (t) {
        var condition = t.isPublished.equals(true);
        if (tag != null) {
          // Filter by tag if provided
          condition = condition & t.tags.like('%$tag%');
        }
        return condition;
      },
      limit: limit,
      offset: offset,
      orderBy: (t) => t.publishedAt,
      orderDescending: true,
      include: Post.include(author: User.include()),
    );
  }

  /// Search posts by title or content.
  Future<List<Post>> searchPosts(
    Session session,
    String query,
  ) async {
    if (query.length < 3) {
      throw ArgumentError('Search query must be at least 3 characters');
    }

    return await Post.db.find(
      session,
      where: (t) =>
        t.isPublished.equals(true) & (
          t.title.ilike('%$query%') |
          t.content.ilike('%$query%')
        ),
      limit: 50,
      orderBy: (t) => t.publishedAt,
      orderDescending: true,
    );
  }

  // === AUTHENTICATED METHODS ===

  /// Create a new post. Requires authentication.
  Future<Post> createPost(Session session, Post post) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw NotAuthenticatedException();
    }

    // Validate
    if (post.title.trim().isEmpty) {
      throw ArgumentError('Title cannot be empty');
    }
    if (post.content.trim().length < 100) {
      throw ArgumentError('Content must be at least 100 characters');
    }

    // Set author and timestamps
    final now = DateTime.now();
    final postToCreate = post.copyWith(
      authorId: userId,
      createdAt: now,
      publishedAt: post.isPublished ? now : null,
      viewCount: 0,
    );

    final created = await Post.db.insertRow(session, postToCreate);
    session.log('Post created: ${created.id} by user $userId');

    return created;
  }

  /// Update a post. Only the author can update their own posts.
  Future<Post> updatePost(Session session, Post post) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw NotAuthenticatedException();
    }

    // Verify ownership
    final existingPost = await Post.db.findById(session, post.id!);
    if (existingPost == null) {
      throw PostNotFoundException(postId: post.id!);
    }
    if (existingPost.authorId != userId) {
      throw UnauthorizedException('You can only edit your own posts');
    }

    // Handle publish state change
    Post postToUpdate = post;
    if (!existingPost.isPublished && post.isPublished) {
      // First time publishing
      postToUpdate = post.copyWith(publishedAt: DateTime.now());
    }

    return await Post.db.updateRow(session, postToUpdate);
  }

  /// Delete a post. Only the author can delete their own posts.
  Future<bool> deletePost(Session session, int postId) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw NotAuthenticatedException();
    }

    final post = await Post.db.findById(session, postId);
    if (post == null) {
      return false;  // Already deleted or never existed
    }

    if (post.authorId != userId) {
      throw UnauthorizedException('You can only delete your own posts');
    }

    final deleted = await Post.db.deleteRow(session, post);
    session.log('Post deleted: $postId by user $userId');

    return deleted;
  }

  /// Get posts by the current user (including drafts).
  Future<List<Post>> getMyPosts(Session session) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw NotAuthenticatedException();
    }

    return await Post.db.find(
      session,
      where: (t) => t.authorId.equals(userId),
      orderBy: (t) => t.createdAt,
      orderDescending: true,
    );
  }

  // === PRIVATE HELPERS ===

  /// Increment view count without blocking the response.
  void _incrementViewCount(Session session, int postId) {
    // Run async without awaiting
    Post.db.findById(session, postId).then((post) {
      if (post != null) {
        Post.db.updateRow(
          session,
          post.copyWith(viewCount: post.viewCount + 1),
        );
      }
    });
  }
}
```
