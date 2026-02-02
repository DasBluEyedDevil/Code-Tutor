import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

fun temperatureSensor(): Flow<Int> = flow {
    val readings = listOf(20, 22, -999, 25, 30, -999, 35, 40, 38, 42)
    for (temp in readings) {
        delay(200)
        emit(temp)
    }
}

fun monitorTemperature(): Flow<Pair<Int, Boolean>> = temperatureSensor()
    .filter { it != -999 } // Remove invalid readings
    .map { celsius ->
        val fahrenheit = celsius * 9 / 5 + 32
        val isAlert = fahrenheit > 100
        Pair(fahrenheit, isAlert)
    }
    .onEach { (temp, isAlert) ->
        if (isAlert) {
            println("⚠️ High temperature detected: $temp°F")
        }
    }

fun main() = runBlocking {
    monitorTemperature().collect { (temp, isAlert) ->
        val status = if (isAlert) "🚨 ALERT!" else "OK"
        println("Temp: $temp°F - $status")
    }
}