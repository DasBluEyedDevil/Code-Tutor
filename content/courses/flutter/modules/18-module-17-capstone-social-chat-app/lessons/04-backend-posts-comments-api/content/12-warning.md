---
type: "WARNING"
title: "Common API Mistakes"
---


**1. N+1 Query Problem**

Problem: Loading related data in a loop.

```dart
// BAD - N+1 queries
for (final post in posts) {
  final author = await UserProfile.db.findById(session, post.authorId);
  // This runs 1 query per post!
}

// GOOD - Batch load
final authorIds = posts.map((p) => p.authorId).toSet();
final authors = await UserProfile.db.find(
  session,
  where: (t) => t.id.inSet(authorIds),
);
final authorMap = {for (final a in authors) a.id: a};
```

**2. Race Condition in Counts**

Problem: Reading and updating count separately.

```dart
// BAD - Race condition
final post = await Post.db.findById(session, postId);
await Post.db.updateRow(
  session,
  post.copyWith(likesCount: post.likesCount + 1),
);

// GOOD - Atomic update
await session.db.unsafeExecute(
  'UPDATE posts SET likes_count = likes_count + 1 WHERE id = @id',
  parameters: QueryParameters.named({'id': postId}),
);
```

**3. Offset Pagination at Scale**

Problem: Using OFFSET for large datasets.

```dart
// BAD - Slow at high offsets
final posts = await Post.db.find(
  session,
  offset: 10000,  // Database scans 10,000+ rows!
  limit: 20,
);

// GOOD - Cursor pagination
final posts = await Post.db.find(
  session,
  where: (t) => t.createdAt.lessThan(cursorTime),
  limit: 20,
);
```

**4. Missing Visibility Checks**

Problem: Not enforcing visibility rules consistently.

```dart
// BAD - Anyone can see any post
final post = await Post.db.findById(session, postId);
return post;

// GOOD - Check visibility
final post = await Post.db.findById(session, postId);
if (!await _canViewPost(session, post)) {
  throw AccessDeniedException();
}
return post;
```

**5. Hard Delete Breaking References**

Problem: Deleting records that are referenced elsewhere.

```dart
// BAD - Breaks comment threading
await Comment.db.deleteRow(session, comment);

// GOOD - Soft delete
await Comment.db.updateRow(
  session,
  comment.copyWith(
    isDeleted: true,
    content: '[deleted]',
  ),
);
```

