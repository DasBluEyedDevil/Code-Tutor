---
type: "EXAMPLE"
title: "Post Protocol Definition"
---


**Post and Related Models**

Define the post model with support for rich content:



```yaml
# server/lib/src/protocol/post.yaml
class: Post
table: posts
fields:
  # Author reference
  authorId: int, relation(parent=user_profiles)
  
  # Content
  content: String
  contentType: PostContentType
  mediaUrls: List<String>?
  linkPreview: LinkPreview?, relation(optional)
  
  # Engagement counts (denormalized for performance)
  likeCount: int
  commentCount: int
  shareCount: int
  
  # Visibility
  visibility: PostVisibility
  
  # State
  isEdited: bool
  isDeleted: bool
  isPinned: bool
  
  # Timestamps
  createdAt: DateTime
  updatedAt: DateTime?

indexes:
  post_author_idx:
    fields: authorId
  post_created_idx:
    fields: createdAt
  post_feed_idx:
    fields: visibility, isDeleted, createdAt

---

# server/lib/src/protocol/post_content_type.yaml
enum: PostContentType
values:
  - text
  - image
  - video
  - link
  - poll

---

# server/lib/src/protocol/post_visibility.yaml
enum: PostVisibility
values:
  - public      # Everyone can see
  - friends     # Only friends
  - private     # Only author

---

# server/lib/src/protocol/link_preview.yaml
class: LinkPreview
table: link_previews
fields:
  url: String
  title: String?
  description: String?
  imageUrl: String?
  siteName: String?
  fetchedAt: DateTime
```
