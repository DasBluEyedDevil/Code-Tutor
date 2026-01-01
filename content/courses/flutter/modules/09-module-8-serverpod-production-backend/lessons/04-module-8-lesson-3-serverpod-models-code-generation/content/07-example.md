---
type: "EXAMPLE"
title: "Complex Model with Relations"
---

Real applications have related data. Let's define a Post model that belongs to a User.

**File: my_project_server/protocol/post.yaml**


```yaml
# protocol/post.yaml
# A Post model with a relation to User

class: Post
table: posts
fields:
  title: String
  # The post title, required.

  content: String
  # The post body, required.

  authorId: int
  # Foreign key to the users table.
  # This stores the id of the User who wrote the post.

  author: User?, relation=userId
  # This creates a relation to the User model.
  # The ? makes it optional (not always loaded).
  # relation=userId means it uses authorId as the foreign key.
  # When you fetch a Post, you can optionally include the author.

  publishedAt: DateTime?
  # When the post was published. Null if still a draft.

  isPublished: bool, default='false'
  # Whether the post is visible to the public.

  viewCount: int, default='0'
  # How many times the post has been viewed.

  tags: List<String>?
  # Optional list of tags for categorization.

indexes:
  # Database indexes for faster queries
  post_author_idx:
    fields: authorId
    # Index on authorId for fast lookups by author

  post_published_idx:
    fields: isPublished, publishedAt
    # Composite index for finding published posts by date
```
