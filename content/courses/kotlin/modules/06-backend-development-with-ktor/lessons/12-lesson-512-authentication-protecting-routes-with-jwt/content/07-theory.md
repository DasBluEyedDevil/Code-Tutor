---
type: "THEORY"
title: "Exercise: Resource Ownership Authorization"
---


Implement authorization that allows users to only modify their own resources.

### Scenario

You have a blog API where users can create posts. Requirements:
- Any authenticated user can create posts
- Users can only edit/delete their own posts
- Admins can edit/delete any post

### Requirements

1. **Create Post Model**:
   - id, title, content, authorId, createdAt

2. **Implement Authorization Logic**:
   ```kotlin
   fun canModifyPost(post: Post, principal: UserPrincipal): Boolean {
       // User can modify if they own the post OR they're an admin
       return post.authorId == principal.userId || principal.role == "ADMIN"
   }
   ```

3. **Apply to Routes**:
   - `PUT /api/posts/:id` - Check ownership before updating
   - `DELETE /api/posts/:id` - Check ownership before deleting

4. **Error Handling**:
   - Return 403 Forbidden if user doesn't own the post and isn't admin
   - Return 404 Not Found if post doesn't exist

### Starter Code


---



```kotlin
@Serializable
data class Post(
    val id: Int,
    val title: String,
    val content: String,
    val authorId: Int,
    val authorName: String,
    val createdAt: String
)

@Serializable
data class CreatePostRequest(
    val title: String,
    val content: String
)

// TODO: Implement canModifyPost authorization
// TODO: Implement update and delete with ownership checks
```
