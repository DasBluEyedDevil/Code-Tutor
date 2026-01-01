---
type: "THEORY"
title: "Exercise: Complete Test Suite"
---


Write a complete test suite for the Post API.

### Requirements

1. **Unit Tests for PostService**:
   - Test create post
   - Test update post with ownership check
   - Test delete post with ownership check
   - Test get posts by user

2. **Integration Tests for Post Routes**:
   - Test POST /api/posts (create post)
   - Test GET /api/posts (get all posts)
   - Test PUT /api/posts/:id (update post - owner only)
   - Test DELETE /api/posts/:id (delete post - owner only)
   - Test authorization (user can't modify others' posts)
   - Test admin can modify any post

3. **Test Coverage**:
   - Aim for 80%+ coverage on services
   - Test all error paths (validation, not found, forbidden)

---

