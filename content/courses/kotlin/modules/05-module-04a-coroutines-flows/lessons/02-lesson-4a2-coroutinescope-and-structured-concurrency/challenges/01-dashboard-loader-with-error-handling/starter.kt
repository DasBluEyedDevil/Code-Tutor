import kotlinx.coroutines.*

data class DashboardState(
    val weather: String? = null,
    val news: String? = null,
    val stocks: String? = null,
    val errors: List<String> = emptyList()
)

suspend fun fetchWeather(): String {
    delay(500)
    return "Sunny, 72°F"
}

suspend fun fetchNews(): String {
    delay(800)
    throw Exception("News API is down") // Simulated failure!
}

suspend fun fetchStocks(): String {
    delay(600)
    return "AAPL: $150, GOOGL: $140"
}

// TODO: Implement this function
// It should fetch all three in parallel
// If one fails, the others should still return their data
// Collect any errors in the errors list
suspend fun loadDashboard(): DashboardState {
    // Your code here
    return DashboardState()
}

fun main() = runBlocking {
    val state = loadDashboard()
    println("Weather: ${state.weather ?: "Failed to load"}")
    println("News: ${state.news ?: "Failed to load"}")
    println("Stocks: ${state.stocks ?: "Failed to load"}")
    println("Errors: ${state.errors}")
}