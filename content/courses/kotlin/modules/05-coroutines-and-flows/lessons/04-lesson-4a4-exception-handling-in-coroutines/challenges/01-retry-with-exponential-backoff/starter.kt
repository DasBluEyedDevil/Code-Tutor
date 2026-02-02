import kotlinx.coroutines.*

var attemptCount = 0

suspend fun unreliableOperation(): String {
    attemptCount++
    if (attemptCount < 3) {
        throw Exception("Temporary failure (attempt $attemptCount)")
    }
    return "Success on attempt $attemptCount"
}

// TODO: Implement retry with exponential backoff
// - Retry up to maxRetries times
// - Wait initialDelayMs between first and second attempt
// - Double the delay for each subsequent attempt
// - Return the result if successful
// - Throw the last exception if all retries fail
suspend fun <T> retryWithBackoff(
    maxRetries: Int = 3,
    initialDelayMs: Long = 100,
    block: suspend () -> T
): T {
    // Your code here
    return block()
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