---
type: "THEORY"
title: "Post and Comment Models"
---


**Social Feed Data Structure**

Posts and comments form the social feed aspect of our app. Key requirements:

**Post Model**
- Rich content (text, images, links)
- Author reference
- Engagement metrics (likes, comments, shares)
- Visibility settings
- Edit history tracking

**Comment Model**
- Nested replies (optional)
- Author reference
- Post reference
- Reactions/likes
- Moderation support

**Content Types**

Posts can contain different types of content:

| Type | Description | Storage |
|------|-------------|--------|
| `text` | Plain text content | `content` field |
| `image` | Single/multiple images | `mediaUrls` array |
| `link` | URL with preview | `linkPreview` object |
| `poll` | Interactive poll | `pollData` JSON |

**Engagement Tracking**

We track engagement through separate tables to enable:
- Counting likes/reactions efficiently
- Tracking who liked what
- Supporting multiple reaction types
- Preventing duplicate reactions

