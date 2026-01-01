---
type: "KEY_POINT"
title: "The include Parameter"
---

By default, related objects are NOT loaded to save database queries. Use the `include` parameter to fetch related data.

**Without include:**

```dart
final post = await Post.db.findById(session, 1);
print(post?.author); // null - not loaded!
```

**With include:**

```dart
final post = await Post.db.findById(
  session,
  1,
  include: Post.include(
    author: User.include(),
  ),
);
print(post?.author?.username); // 'john' - loaded!
```

**Nested includes:**

```dart
// Load post with author and author's profile
final post = await Post.db.findById(
  session,
  1,
  include: Post.include(
    author: User.include(
      profile: UserProfile.include(),
    ),
  ),
);
print(post?.author?.profile?.bio);
```

**Include lists (for one-to-many):**

```dart
// Load user with all their posts
final user = await User.db.findById(
  session,
  userId,
  include: User.include(
    posts: Post.includeList(),
  ),
);
for (final post in user?.posts ?? []) {
  print(post.title);
}
```

**Warning:** Avoid deep nesting and large lists. Each include adds database queries. For complex data needs, consider multiple targeted queries instead.

