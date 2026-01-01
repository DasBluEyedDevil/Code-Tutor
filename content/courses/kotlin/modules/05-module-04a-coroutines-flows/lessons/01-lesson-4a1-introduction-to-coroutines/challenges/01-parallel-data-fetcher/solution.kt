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

suspend fun fetchAllUserData(): Triple<String, String, String> = coroutineScope {
    // Launch all three fetches in parallel
    val profileDeferred = async { fetchUserProfile() }
    val settingsDeferred = async { fetchUserSettings() }
    val notificationsDeferred = async { fetchUserNotifications() }
    
    // Wait for all results and combine
    Triple(
        profileDeferred.await(),
        settingsDeferred.await(),
        notificationsDeferred.await()
    )
}

fun main() = runBlocking {
    val time = measureTimeMillis {
        val result = fetchAllUserData()
        println("Fetched: $result")
    }
    println("Time: ${time}ms") // Should be ~1000-1100ms
}