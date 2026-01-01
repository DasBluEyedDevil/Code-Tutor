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
    throw Exception("News API is down")
}

suspend fun fetchStocks(): String {
    delay(600)
    return "AAPL: $150, GOOGL: $140"
}

suspend fun loadDashboard(): DashboardState = supervisorScope {
    val weatherDeferred = async { runCatching { fetchWeather() } }
    val newsDeferred = async { runCatching { fetchNews() } }
    val stocksDeferred = async { runCatching { fetchStocks() } }
    
    val weatherResult = weatherDeferred.await()
    val newsResult = newsDeferred.await()
    val stocksResult = stocksDeferred.await()
    
    val errors = mutableListOf<String>()
    
    weatherResult.exceptionOrNull()?.let {
        errors.add("Weather: ${it.message}")
    }
    newsResult.exceptionOrNull()?.let {
        errors.add("News: ${it.message}")
    }
    stocksResult.exceptionOrNull()?.let {
        errors.add("Stocks: ${it.message}")
    }
    
    DashboardState(
        weather = weatherResult.getOrNull(),
        news = newsResult.getOrNull(),
        stocks = stocksResult.getOrNull(),
        errors = errors
    )
}

fun main() = runBlocking {
    val state = loadDashboard()
    println("Weather: ${state.weather ?: "Failed to load"}")
    println("News: ${state.news ?: "Failed to load"}")
    println("Stocks: ${state.stocks ?: "Failed to load"}")
    println("Errors: ${state.errors}")
}