---
type: "EXAMPLE"
title: "Comment Protocol Definition"
---


**Comment Model with Threading**

Support for nested replies and reactions:



```yaml
# server/lib/src/protocol/comment.yaml
class: Comment
table: comments
fields:
  # References
  postId: int, relation(parent=posts)
  authorId: int, relation(parent=user_profiles)
  
  # For nested replies
  parentCommentId: int?, relation(parent=comments, optional)
  
  # Content
  content: String
  
  # Engagement (denormalized)
  likeCount: int
  replyCount: int
  
  # State
  isEdited: bool
  isDeleted: bool
  
  # Timestamps
  createdAt: DateTime
  updatedAt: DateTime?

indexes:
  comment_post_idx:
    fields: postId, createdAt
  comment_author_idx:
    fields: authorId
  comment_parent_idx:
    fields: parentCommentId

---

# server/lib/src/protocol/post_like.yaml
class: PostLike
table: post_likes
fields:
  postId: int, relation(parent=posts)
  userId: int, relation(parent=user_profiles)
  reactionType: ReactionType
  createdAt: DateTime

indexes:
  post_like_unique_idx:
    fields: postId, userId
    unique: true
  post_like_post_idx:
    fields: postId

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

# server/lib/src/protocol/reaction_type.yaml
enum: ReactionType
values:
  - like
  - love
  - laugh
  - wow
  - sad
  - angry
```
