---
type: "THEORY"
title: "Solution 2"
---


**Common Declaration (commonMain)**:

**Android Implementation (androidMain)**:

**iOS Implementation (iosMain)**:

**JVM Implementation (jvmMain)**:

**Usage (commonMain)**:

---



```kotlin
class UserRepository {
    suspend fun getUser(id: String): User? {
        Logger.debug("UserRepository", "Fetching user: $id")

        return try {
            val user = api.getUser(id)
            Logger.info("UserRepository", "User fetched successfully")
            user
        } catch (e: Exception) {
            Logger.error("UserRepository", "Failed to fetch user", e)
            null
        }
    }
}
```
