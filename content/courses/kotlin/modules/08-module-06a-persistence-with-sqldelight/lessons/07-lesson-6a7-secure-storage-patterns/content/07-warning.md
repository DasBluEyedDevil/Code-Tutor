---
type: "WARNING"
title: "Security Best Practices"
---

### DO:

✅ Use platform-native secure storage (Keychain, EncryptedSharedPrefs)
✅ Clear sensitive data on logout
✅ Use short-lived access tokens
✅ Implement token refresh logic
✅ Validate data before storage
✅ Handle storage errors gracefully

### DON'T:

❌ Store passwords locally (even encrypted)
❌ Log sensitive data
❌ Use hardcoded encryption keys
❌ Store API secrets in code
❌ Trust data from local storage without validation
❌ Keep sessions alive indefinitely

### Testing Security

```kotlin
// Use in-memory storage for tests
class FakeSecureStorage : SecureStorage {
    private val store = mutableMapOf<String, String>()
    
    override suspend fun saveString(key: String, value: String) {
        store[key] = value
    }
    
    override suspend fun getString(key: String): String? = store[key]
    override suspend fun remove(key: String) { store.remove(key) }
    override suspend fun clear() { store.clear() }
}
```