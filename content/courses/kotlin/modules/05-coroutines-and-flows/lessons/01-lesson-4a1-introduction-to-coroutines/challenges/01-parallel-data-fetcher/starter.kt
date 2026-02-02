import kotlinx.coroutines.*
import kotlin.system.measureTimeMillis

suspend fun fetchUserProfile(): String {
    delay(1000)
    return "Profile Data"
}

suspend fun fetchUserSettings(): String {
    delay(800)
    return "Settings Data"
}

suspend fun fetchUserNotifications(): String {
    delay(600)
    return "Notifications"
}

// TODO: Implement this function to fetch all three in parallel
// It should complete in ~1000ms, not ~2400ms
suspend fun fetchAllUserData(): Triple<String, String, String> {
    // Your code here
    return Triple("", "", "")
}

fun main() = runBlocking {
    val time = measureTimeMillis {
        val result = fetchAllUserData()
        println("Fetched: $result")
    }
    println("Time: ${time}ms")
}