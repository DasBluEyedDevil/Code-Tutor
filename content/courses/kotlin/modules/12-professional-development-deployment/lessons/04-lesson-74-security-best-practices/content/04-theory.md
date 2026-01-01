---
type: "THEORY"
title: "Secure Coding Principles"
---


### Principle 1: Defense in Depth

Never rely on a single security measure.

❌ **Bad** (Single layer):

✅ **Good** (Multiple layers):

### Principle 2: Least Privilege

Grant minimum permissions necessary.

❌ **Bad** (Admin for everyone):

✅ **Good** (Minimal permissions):

### Principle 3: Fail Securely

When errors occur, fail in a secure state.

❌ **Bad** (Fails open):

✅ **Good** (Fails closed):

---



```kotlin
fun checkAccess(userId: String, resourceId: String): Boolean {
    return try {
        val user = userService.getUser(userId) ?: return false
        val resource = resourceService.getResource(resourceId) ?: return false
        user.hasAccessTo(resource)
    } catch (e: Exception) {
        logger.error("Access check failed", e)
        // ✅ Error = deny access
        false
    }
}
```
