---
type: "THEORY"
title: "KMP Abstraction for Secure Storage"
---

### Common Interface

```kotlin
// commonMain
interface SecureStorage {
    suspend fun saveString(key: String, value: String)
    suspend fun getString(key: String): String?
    suspend fun remove(key: String)
    suspend fun clear()
}

// Token-specific wrapper
class TokenStorage(private val secureStorage: SecureStorage) {
    suspend fun saveAuthToken(token: String) {
        secureStorage.saveString(KEY_AUTH_TOKEN, token)
    }
    
    suspend fun getAuthToken(): String? {
        return secureStorage.getString(KEY_AUTH_TOKEN)
    }
    
    suspend fun clearAuthToken() {
        secureStorage.remove(KEY_AUTH_TOKEN)
    }
    
    companion object {
        private const val KEY_AUTH_TOKEN = "auth_token"
    }
}
```

### Using Multiplatform Settings Library

A simpler approach using `multiplatform-settings`:

```kotlin
// build.gradle.kts
implementation("com.russhwolf:multiplatform-settings:1.1.1")
implementation("com.russhwolf:multiplatform-settings-no-arg:1.1.1")

// For encrypted storage on Android:
implementation("com.russhwolf:multiplatform-settings-datastore:1.1.1")
```

```kotlin
import com.russhwolf.settings.Settings

class SettingsStorage(private val settings: Settings) : SecureStorage {
    override suspend fun saveString(key: String, value: String) {
        settings.putString(key, value)
    }
    
    override suspend fun getString(key: String): String? {
        return settings.getStringOrNull(key)
    }
    
    override suspend fun remove(key: String) {
        settings.remove(key)
    }
    
    override suspend fun clear() {
        settings.clear()
    }
}
```