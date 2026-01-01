---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) Authentication verifies identity, authorization verifies permissions**

- **Authentication**: "Who are you?" (prove identity with username/password)
- **Authorization**: "Are you allowed to do this?" (check permissions/roles)

Example: Alice authenticates (proves she's Alice), then authorization checks if Alice can delete posts.

---

**Question 2: C) From `call.principal<UserPrincipal>()`**

After successful authentication, Ktor stores the user information in the principal:


---

**Question 3: B) 403 Forbidden**

HTTP status code meanings:
- **401 Unauthorized**: Not authenticated (no token or invalid token)
- **403 Forbidden**: Authenticated but not authorized (valid token but insufficient permissions)
- **404 Not Found**: Resource doesn't exist
- **500 Internal Server Error**: Server bug

---

**Question 4: C) The owner OR admins**

The canModifyPost function implements:

This allows:
- Owner to modify their own posts
- Admins to modify any post (moderator pattern)

---

**Question 5: C) The challenge function is called, typically returning 401 Unauthorized**

The authentication flow:
1. Request arrives without token (or invalid token)
2. `validate { }` function returns null
3. `challenge { }` function is called
4. Returns 401 Unauthorized with error message

---



```kotlin
return post.authorId == principal.userId || principal.role == "ADMIN"
```
