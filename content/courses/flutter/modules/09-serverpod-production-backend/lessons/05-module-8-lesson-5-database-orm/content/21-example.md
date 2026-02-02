---
type: "EXAMPLE"
title: "One-to-Many Relationship"
---

Here is how to define a one-to-many relationship between User and Post:



```yaml
# lib/src/protocol/user.yaml
class: User
table: users
fields:
  email: String
  username: String
  # Optional: Add a relation to access posts from user
  posts: List<Post>?, relation(name: user_posts)

# lib/src/protocol/post.yaml
class: Post
table: posts
fields:
  title: String
  content: String
  createdAt: DateTime
  isPublished: bool
  
  # Foreign key to User
  authorId: int
  author: User?, relation(field: authorId, name: user_posts)

# Querying with relations:

# Get a post with its author
final post = await Post.db.findById(
  session,
  postId,
  include: Post.include(
    author: User.include(),
  ),
);
print('Post by: ${post?.author?.username}');

# Get all posts by a user
final posts = await Post.db.find(
  session,
  where: (t) => t.authorId.equals(userId),
  orderBy: (t) => t.createdAt,
  orderDescending: true,
);

# Get a user with all their posts
final user = await User.db.findById(
  session,
  userId,
  include: User.include(
    posts: Post.includeList(),
  ),
);
print('User has ${user?.posts?.length} posts');
```
