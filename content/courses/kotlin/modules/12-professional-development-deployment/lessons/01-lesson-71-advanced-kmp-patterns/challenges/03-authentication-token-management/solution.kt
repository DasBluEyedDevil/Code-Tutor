import java.util.UUID

data class Session(val userId: Int, val token: String)

class SessionManager {
    private val sessions = mutableMapOf<String, Session>()
    
    fun createSession(userId: Int): String {
        val token = UUID.randomUUID().toString()
        sessions[token] = Session(userId, token)
        return token
    }
    
    fun validateToken(token: String): Int? {
        return sessions[token]?.userId
    }
    
    fun removeSession(token: String) {
        sessions.remove(token)
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