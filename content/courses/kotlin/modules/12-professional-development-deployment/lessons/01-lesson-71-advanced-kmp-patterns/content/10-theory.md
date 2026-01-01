---
type: "THEORY"
title: "Platform-Specific Implementations"
---


### Example: Storage Layer

**Common Interface (commonMain)**:

**Android Implementation (androidMain)**:

**iOS Implementation (iosMain)**:

**Usage in Shared Code**:

---



```kotlin
class UserPreferences(private val storage: KeyValueStorage) {
    fun saveUsername(username: String) {
        storage.putString("username", username)
    }

    fun getUsername(): String? {
        return storage.getString("username")
    }

    fun logout() {
        storage.clear()
    }
}
```
