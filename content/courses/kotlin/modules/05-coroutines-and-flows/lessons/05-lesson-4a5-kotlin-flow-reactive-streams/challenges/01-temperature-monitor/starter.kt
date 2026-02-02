import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

// Simulates temperature sensor readings (some may be invalid: -999)
fun temperatureSensor(): Flow<Int> = flow {
    val readings = listOf(20, 22, -999, 25, 30, -999, 35, 40, 38, 42)
    for (temp in readings) {
        delay(200)
        emit(temp)
    }
}

// TODO: Implement a flow that:
// 1. Filters out invalid readings (-999)
// 2. Converts Celsius to Fahrenheit: F = C * 9/5 + 32
// 3. Emits a Pair of (fahrenheit, isAlert) where isAlert is true if temp > 100°F
fun monitorTemperature(): Flow<Pair<Int, Boolean>> {
    // Your code here
    return flowOf(Pair(0, false))
}

fun main() = runBlocking {
    monitorTemperature().collect { (temp, isAlert) ->
        val status = if (isAlert) "🚨 ALERT!" else "OK"
        println("Temp: $temp°F - $status")
    }
}