---
type: "EXAMPLE"
title: "Many-to-Many Relationship"
---

Many-to-many relationships require a junction table. Here is Post and Tag:



```yaml
# lib/src/protocol/tag.yaml
class: Tag
table: tags
fields:
  name: String
  slug: String

# lib/src/protocol/post.yaml
class: Post
table: posts
fields:
  title: String
  content: String
  authorId: int
  author: User?, relation(field: authorId)

# lib/src/protocol/post_tag.yaml (junction table)
class: PostTag
table: post_tags
fields:
  postId: int
  post: Post?, relation(field: postId)
  
  tagId: int
  tag: Tag?, relation(field: tagId)

# Querying many-to-many:

# Get all tags for a post
final postTags = await PostTag.db.find(
  session,
  where: (t) => t.postId.equals(postId),
  include: PostTag.include(
    tag: Tag.include(),
  ),
);
final tags = postTags.map((pt) => pt.tag).whereType<Tag>().toList();

# Get all posts with a specific tag
final postTags = await PostTag.db.find(
  session,
  where: (t) => t.tagId.equals(tagId),
  include: PostTag.include(
    post: Post.include(),
  ),
);
final posts = postTags.map((pt) => pt.post).whereType<Post>().toList();

# Add a tag to a post
final postTag = PostTag(
  postId: postId,
  tagId: tagId,
);
await PostTag.db.insertRow(session, postTag);

# Remove a tag from a post
await PostTag.db.deleteWhere(
  session,
  where: (t) => t.postId.equals(postId) & t.tagId.equals(tagId),
);
```
