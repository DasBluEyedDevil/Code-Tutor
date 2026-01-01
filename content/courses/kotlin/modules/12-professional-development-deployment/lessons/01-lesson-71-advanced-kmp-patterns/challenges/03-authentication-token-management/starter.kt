import java.util.UUID

data class Session(val userId: Int, val token: String)

class SessionManager {
    private val sessions = mutableMapOf<String, Session>()
    
    fun createSession(userId: Int): String {
        // Generate token and store session
    }
    
    fun validateToken(token: String): Int? {
        // Return userId if valid, null otherwise
    }
    
    fun removeSession(token: String) {
        // Remove session
    }
}

fun main() {
    val manager = SessionManager()
    val token = manager.createSession(1)
    println("Token created: $token")
    println("User ID: ${manager.validateToken(token)}")
    println("Invalid token: ${manager.validateToken("invalid")}")
    manager.removeSession(token)
    println("After logout: ${manager.validateToken(token)}")
}