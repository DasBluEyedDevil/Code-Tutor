---
type: "EXAMPLE"
title: "Post Model Definition"
---


**Defining the Post Protocol**

Create the post model with all necessary fields and relationships:



```yaml
# server/lib/src/protocol/post.yaml
class: Post
table: posts
fields:
  # Author relationship
  authorId: int, relation(parent=user_profiles)
  
  # Content
  content: String
  contentType: String  # 'text', 'image', 'video', 'poll'
  
  # Visibility and status
  visibility: String  # 'public', 'followers', 'private'
  status: String  # 'draft', 'published', 'archived', 'deleted'
  
  # Denormalized counts for performance
  likesCount: int
  commentsCount: int
  sharesCount: int
  viewsCount: int
  
  # Metadata
  isEdited: bool
  isPinned: bool
  allowComments: bool
  
  # Location (optional)
  locationName: String?
  latitude: double?
  longitude: double?
  
  # Timestamps
  publishedAt: DateTime?
  createdAt: DateTime
  updatedAt: DateTime?
  deletedAt: DateTime?

indexes:
  post_author_idx:
    fields: authorId, status, createdAt
  post_feed_idx:
    fields: status, visibility, publishedAt
  post_trending_idx:
    fields: status, likesCount, publishedAt

---

# server/lib/src/protocol/post_media.yaml
class: PostMedia
table: post_media
fields:
  postId: int, relation(parent=posts)
  
  # Media info
  mediaType: String  # 'image', 'video', 'gif'
  url: String
  thumbnailUrl: String?
  
  # Dimensions
  width: int?
  height: int?
  durationSeconds: int?  # For videos
  
  # File info
  mimeType: String?
  sizeBytes: int?
  
  # Ordering
  order: int
  
  # Accessibility
  altText: String?
  
  createdAt: DateTime

indexes:
  post_media_post_idx:
    fields: postId, order

---

# server/lib/src/protocol/post_like.yaml
class: PostLike
table: post_likes
fields:
  postId: int, relation(parent=posts)
  userId: int, relation(parent=user_profiles)
  reactionType: String  # 'like', 'love', 'laugh', 'wow', 'sad', 'angry'
  createdAt: DateTime

indexes:
  post_like_unique_idx:
    fields: postId, userId
    unique: true
  post_like_user_idx:
    fields: userId, createdAt
```
