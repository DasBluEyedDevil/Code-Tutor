---
type: "THEORY"
title: "Function-Based Views vs Class-Based Views"
---

**Two Ways to Handle Requests in Django**

Django provides two paradigms for writing views: function-based views (FBVs) and class-based views (CBVs).

**Function-Based Views (FBVs)**
- Simple and explicit
- Easy to understand and debug
- Good for unique, one-off logic
- Full control over request/response flow

**Class-Based Views (CBVs)**
- Reusable through inheritance
- DRY (Don't Repeat Yourself)
- Built-in handling for common patterns
- Mixins for composable behavior

**When to Use Each:**

| Use FBVs When | Use CBVs When |
|---------------|---------------|
| Simple, unique logic | Standard CRUD operations |
| Learning Django | Need inheritance/mixins |
| One-off endpoints | Repeating patterns |
| Complex conditional logic | Generic list/detail views |

**The Django Philosophy:**
Start with FBVs to understand the flow, then graduate to CBVs for common patterns. Many Django developers use a mix of both based on the situation.