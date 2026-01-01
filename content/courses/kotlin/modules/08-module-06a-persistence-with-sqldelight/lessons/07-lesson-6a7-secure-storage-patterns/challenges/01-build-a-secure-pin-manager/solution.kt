import kotlin.io.encoding.Base64
import kotlin.io.encoding.ExperimentalEncodingApi

class PinManagerImpl(
    private val secureStorage: SecureStorage
) : PinManager {
    
    private var failedAttempts = 0
    private var lockoutUntil: Long = 0
    
    companion object {
        const val MAX_ATTEMPTS = 5
        const val LOCKOUT_DURATION_MS = 30_000L
        private const val KEY_PIN_HASH = "pin_hash"
        private const val KEY_FAILED_ATTEMPTS = "failed_attempts"
        private const val KEY_LOCKOUT_UNTIL = "lockout_until"
    }
    
    override suspend fun setPin(pin: String): Result<Unit> = runCatching {
        require(pin.length >= 4) { "PIN must be at least 4 digits" }
        require(pin.all { it.isDigit() }) { "PIN must contain only digits" }
        
        val hash = hashPin(pin)
        secureStorage.saveString(KEY_PIN_HASH, hash)
        resetAttempts()
    }
    
    override suspend fun verifyPin(pin: String): Result<Boolean> = runCatching {
        // Check lockout
        loadState()
        val now = Clock.System.now().toEpochMilliseconds()
        if (lockoutUntil > now) {
            throw IllegalStateException("Locked out. Try again in ${(lockoutUntil - now) / 1000} seconds")
        }
        
        val storedHash = secureStorage.getString(KEY_PIN_HASH)
            ?: throw IllegalStateException("No PIN set")
        
        val inputHash = hashPin(pin)
        val isValid = storedHash == inputHash
        
        if (isValid) {
            resetAttempts()
        } else {
            failedAttempts++
            if (failedAttempts >= MAX_ATTEMPTS) {
                lockoutUntil = now + LOCKOUT_DURATION_MS
            }
            saveState()
        }
        
        isValid
    }
    
    override suspend fun hasPin(): Boolean {
        return secureStorage.getString(KEY_PIN_HASH) != null
    }
    
    override suspend fun clearPin() {
        secureStorage.remove(KEY_PIN_HASH)
        resetAttempts()
    }
    
    override fun getRemainingAttempts(): Int {
        return (MAX_ATTEMPTS - failedAttempts).coerceAtLeast(0)
    }
    
    @OptIn(ExperimentalEncodingApi::class)
    private fun hashPin(pin: String): String {
        // In production, use a proper crypto library
        // This is a simplified example
        val bytes = (pin + "salt_value").encodeToByteArray()
        return Base64.encode(bytes.sha256())
    }
    
    private suspend fun loadState() {
        failedAttempts = secureStorage.getString(KEY_FAILED_ATTEMPTS)?.toIntOrNull() ?: 0
        lockoutUntil = secureStorage.getString(KEY_LOCKOUT_UNTIL)?.toLongOrNull() ?: 0
    }
    
    private suspend fun saveState() {
        secureStorage.saveString(KEY_FAILED_ATTEMPTS, failedAttempts.toString())
        secureStorage.saveString(KEY_LOCKOUT_UNTIL, lockoutUntil.toString())
    }
    
    private suspend fun resetAttempts() {
        failedAttempts = 0
        lockoutUntil = 0
        saveState()
    }
}