interface PinManager {
    // Save a new PIN (store hash, not plain text)
    suspend fun setPin(pin: String): Result<Unit>
    
    // Verify PIN attempt (return false if too many attempts)
    suspend fun verifyPin(pin: String): Result<Boolean>
    
    // Check if PIN is set
    suspend fun hasPin(): Boolean
    
    // Clear PIN and reset attempts
    suspend fun clearPin()
    
    // Get remaining attempts before lockout
    fun getRemainingAttempts(): Int
}

class PinManagerImpl(
    private val secureStorage: SecureStorage
) : PinManager {
    
    companion object {
        const val MAX_ATTEMPTS = 5
        const val LOCKOUT_DURATION_MS = 30_000L // 30 seconds
    }
    
    // TODO: Implement all methods
    // - Hash PIN before storing
    // - Track failed attempts
    // - Implement temporary lockout after MAX_ATTEMPTS
    // - Store attempt count and lockout timestamp
}