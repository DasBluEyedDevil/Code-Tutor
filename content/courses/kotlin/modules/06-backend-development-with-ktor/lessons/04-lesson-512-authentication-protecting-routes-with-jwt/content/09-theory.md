---
type: "THEORY"
title: "Solution Explanation"
---


### Authorization Levels

The solution implements three authorization levels:

**Level 1: Public Access** (no authentication)
- `GET /api/posts` - Anyone can list posts
- `GET /api/posts/:id` - Anyone can view a post

**Level 2: Authenticated Access** (requires valid token)
- `POST /api/posts` - Any authenticated user can create posts

**Level 3: Resource Ownership** (requires ownership or admin role)
- `PUT /api/posts/:id` - Only owner or admin
- `DELETE /api/posts/:id` - Only owner or admin

### The canModifyPost Function


This elegant function handles both:
- **Ownership**: `post.authorId == principal.userId`
- **Role override**: `principal.role == "ADMIN"`

Admins can modify any post, regular users can only modify their own.

---



```kotlin
private fun canModifyPost(post: Post, principal: UserPrincipal): Boolean {
    return post.authorId == principal.userId || principal.role == "ADMIN"
}
```
