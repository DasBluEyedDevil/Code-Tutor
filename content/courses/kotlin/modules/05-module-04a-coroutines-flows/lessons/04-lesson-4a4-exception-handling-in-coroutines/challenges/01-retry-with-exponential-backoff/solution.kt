import kotlinx.coroutines.*

var attemptCount = 0

suspend fun unreliableOperation(): String {
    attemptCount++
    if (attemptCount < 3) {
        throw Exception("Temporary failure (attempt $attemptCount)")
    }
    return "Success on attempt $attemptCount"
}

suspend fun <T> retryWithBackoff(
    maxRetries: Int = 3,
    initialDelayMs: Long = 100,
    block: suspend () -> T
): T {
    var currentDelay = initialDelayMs
    var lastException: Exception? = null
    
    repeat(maxRetries) { attempt ->
        try {
            return block()
        } catch (e: CancellationException) {
            throw e // Never retry cancellation
        } catch (e: Exception) {
            lastException = e
            println("Attempt ${attempt + 1} failed: ${e.message}")
            
            if (attempt < maxRetries - 1) {
                println("Waiting ${currentDelay}ms before retry...")
                delay(currentDelay)
                currentDelay *= 2 // Exponential backoff
            }
        }
    }
    
    throw lastException ?: Exception("Retry failed")
}

fun main() = runBlocking {
    attemptCount = 0
    
    try {
        val result = retryWithBackoff(maxRetries = 5) {
            unreliableOperation()
        }
        println(result)
    } catch (e: Exception) {
        println("All retries failed: ${e.message}")
    }
}