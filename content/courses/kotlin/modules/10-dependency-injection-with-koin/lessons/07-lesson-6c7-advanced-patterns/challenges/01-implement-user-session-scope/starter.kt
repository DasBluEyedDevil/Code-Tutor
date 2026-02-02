// Implement a session management system with these requirements:
// 1. UserSession holds user data (id, name, authToken)
// 2. SessionManager creates/destroys session scopes
// 3. UserPreferences is scoped to the session
// 4. When user logs out, all session data is cleared

data class UserSession(
    var userId: String = "",
    var userName: String = "",
    var authToken: String = ""
)

class UserPreferences(private val session: UserSession) {
    // Preferences tied to current user
    fun getUserTheme(): String = "light" // Would read from storage
}

class SessionManager {
    // TODO: Implement session management with Koin scopes
    
    fun login(userId: String, userName: String, token: String): Boolean {
        // Create session scope and initialize
        TODO()
    }
    
    fun logout() {
        // Close session scope, clear all session data
        TODO()
    }
    
    fun isLoggedIn(): Boolean {
        TODO()
    }
    
    fun getCurrentSession(): UserSession? {
        TODO()
    }
}

// TODO: Define the Koin module with session scope