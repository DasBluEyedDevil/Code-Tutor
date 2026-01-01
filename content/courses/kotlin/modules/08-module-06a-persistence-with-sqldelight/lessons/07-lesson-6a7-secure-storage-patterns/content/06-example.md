---
type: "EXAMPLE"
title: "Complete Secure Session Storage"
---

A production-ready session storage implementation:

```kotlin
// ===== commonMain =====
data class UserSession(
    val userId: String,
    val accessToken: String,
    val refreshToken: String,
    val expiresAt: Long
)

interface SessionManager {
    suspend fun saveSession(session: UserSession)
    suspend fun getSession(): UserSession?
    suspend fun clearSession()
    suspend fun isSessionValid(): Boolean
}

class SessionManagerImpl(
    private val secureStorage: SecureStorage
) : SessionManager {
    
    override suspend fun saveSession(session: UserSession) {
        secureStorage.saveString(KEY_USER_ID, session.userId)
        secureStorage.saveString(KEY_ACCESS_TOKEN, session.accessToken)
        secureStorage.saveString(KEY_REFRESH_TOKEN, session.refreshToken)
        secureStorage.saveString(KEY_EXPIRES_AT, session.expiresAt.toString())
    }
    
    override suspend fun getSession(): UserSession? {
        val userId = secureStorage.getString(KEY_USER_ID) ?: return null
        val accessToken = secureStorage.getString(KEY_ACCESS_TOKEN) ?: return null
        val refreshToken = secureStorage.getString(KEY_REFRESH_TOKEN) ?: return null
        val expiresAt = secureStorage.getString(KEY_EXPIRES_AT)?.toLongOrNull() ?: return null
        
        return UserSession(userId, accessToken, refreshToken, expiresAt)
    }
    
    override suspend fun clearSession() {
        secureStorage.remove(KEY_USER_ID)
        secureStorage.remove(KEY_ACCESS_TOKEN)
        secureStorage.remove(KEY_REFRESH_TOKEN)
        secureStorage.remove(KEY_EXPIRES_AT)
    }
    
    override suspend fun isSessionValid(): Boolean {
        val session = getSession() ?: return false
        return session.expiresAt > Clock.System.now().toEpochMilliseconds()
    }
    
    companion object {
        private const val KEY_USER_ID = "session_user_id"
        private const val KEY_ACCESS_TOKEN = "session_access_token"
        private const val KEY_REFRESH_TOKEN = "session_refresh_token"
        private const val KEY_EXPIRES_AT = "session_expires_at"
    }
}

// ===== Usage in Auth Repository =====
class AuthRepository(
    private val api: AuthApi,
    private val sessionManager: SessionManager
) {
    suspend fun login(email: String, password: String): Result<User> {
        return runCatching {
            val response = api.login(email, password)
            
            // Store session securely
            sessionManager.saveSession(
                UserSession(
                    userId = response.user.id,
                    accessToken = response.accessToken,
                    refreshToken = response.refreshToken,
                    expiresAt = response.expiresAt
                )
            )
            
            response.user
        }
    }
    
    suspend fun logout() {
        sessionManager.clearSession()
    }
    
    suspend fun isLoggedIn(): Boolean {
        return sessionManager.isSessionValid()
    }
}
```
