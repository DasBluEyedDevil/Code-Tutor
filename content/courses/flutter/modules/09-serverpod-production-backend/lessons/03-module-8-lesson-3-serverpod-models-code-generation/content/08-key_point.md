---
type: "KEY_POINT"
title: "Understanding Relations"
---

**Relations connect your models together.**

In the Post example above:
- `authorId: int` stores the actual foreign key value (the User's id)
- `author: User?, relation=userId` creates the relation

**Why Both Fields?**

1. **authorId** is stored in the database. It's the raw integer foreign key.

2. **author** is the actual User object. It's loaded when you explicitly request it.

**Loading Related Data:**

```dart
// Just get the post (author is null)
final post = await Post.db.findById(session, postId);
print(post?.authorId); // 42 (the integer)
print(post?.author);   // null (not loaded)

// Get post WITH author loaded
final postWithAuthor = await Post.db.findById(
  session,
  postId,
  include: Post.include(author: User.include()),
);
print(postWithAuthor?.author?.name); // "John Doe"
```

**Relation Types:**

- **One-to-Many**: One User has many Posts (shown above)
- **Many-to-One**: Many Posts belong to one User (shown above)
- **One-to-One**: One User has one Profile
- **Many-to-Many**: Posts have many Tags, Tags have many Posts

