import org.koin.core.component.KoinComponent
import org.koin.core.component.inject
import org.koin.core.qualifier.named
import org.koin.core.scope.Scope
import org.koin.dsl.module
import org.koin.mp.KoinPlatform.getKoin

// Session data class
data class UserSession(
    var userId: String = "",
    var userName: String = "",
    var authToken: String = ""
)

// User preferences tied to session
class UserPreferences(private val session: UserSession) {
    fun getUserTheme(): String = "light"
    fun getUserId(): String = session.userId
}

// Define session scope name
val USER_SESSION_SCOPE = named("userSession")

// Koin module with session scope
val sessionModule = module {
    // Session scope - all these are created per session
    scope(USER_SESSION_SCOPE) {
        scoped { UserSession() }
        scoped { UserPreferences(get()) }
        // Add more session-scoped dependencies here
        // scoped { CartRepository(get()) }
        // scoped { WishlistRepository(get()) }
    }
}

// Session manager
class SessionManager : KoinComponent {
    private var sessionScope: Scope? = null
    
    fun login(userId: String, userName: String, token: String): Boolean {
        return try {
            // Close any existing session
            logout()
            
            // Create new session scope with unique ID
            sessionScope = getKoin().createScope(
                scopeId = "session_$userId",
                qualifier = USER_SESSION_SCOPE
            )
            
            // Initialize session data
            val session = sessionScope!!.get<UserSession>()
            session.userId = userId
            session.userName = userName
            session.authToken = token
            
            true
        } catch (e: Exception) {
            println("Login failed: ${e.message}")
            false
        }
    }
    
    fun logout() {
        sessionScope?.let { scope ->
            // Optional: Clear sensitive data before closing
            try {
                val session = scope.get<UserSession>()
                session.authToken = ""  // Clear token
            } catch (_: Exception) {}
            
            // Close scope - destroys all scoped instances
            scope.close()
        }
        sessionScope = null
    }
    
    fun isLoggedIn(): Boolean {
        return sessionScope?.closed == false &&
               getCurrentSession()?.authToken?.isNotBlank() == true
    }
    
    fun getCurrentSession(): UserSession? {
        return try {
            sessionScope?.get<UserSession>()
        } catch (e: Exception) {
            null
        }
    }
    
    // Get any session-scoped dependency
    inline fun <reified T: Any> getSessionDependency(): T? {
        return try {
            sessionScope?.get<T>()
        } catch (e: Exception) {
            null
        }
    }
}

// Usage example:
fun example() {
    // Include sessionModule in startKoin
    // startKoin { modules(sessionModule, ...) }
    
    val sessionManager = SessionManager()
    
    // Login
    sessionManager.login("user123", "John Doe", "auth_token_xyz")
    
    // Access session
    val session = sessionManager.getCurrentSession()
    println("Logged in as: ${session?.userName}")
    
    // Get session-scoped dependencies
    val prefs = sessionManager.getSessionDependency<UserPreferences>()
    println("Theme: ${prefs?.getUserTheme()}")
    
    // Logout - all session data destroyed
    sessionManager.logout()
    println("Is logged in: ${sessionManager.isLoggedIn()}") // false
}