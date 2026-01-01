import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

data class TimerState(
    val remainingSeconds: Int,
    val isRunning: Boolean,
    val isFinished: Boolean
)

class CountdownTimer(private val durationSeconds: Int) {
    private val scope = CoroutineScope(Dispatchers.Default + SupervisorJob())
    
    // TODO: Create _state MutableStateFlow
    // TODO: Create state StateFlow
    
    private var timerJob: Job? = null
    
    // TODO: Implement start()
    fun start() {
        // Start the countdown
        // Update state every second
        // Set isFinished when done
    }
    
    // TODO: Implement pause()
    fun pause() {
        // Stop the countdown but keep remaining time
    }
    
    // TODO: Implement reset()
    fun reset() {
        // Reset to initial duration
    }
    
    fun onCleared() = scope.cancel()
}

fun main() = runBlocking {
    val timer = CountdownTimer(5)
    
    launch {
        timer.state.collect { state ->
            println("Time: ${state.remainingSeconds}s | Running: ${state.isRunning} | Finished: ${state.isFinished}")
        }
    }
    
    delay(100)
    println("Starting...")
    timer.start()
    delay(2500)
    println("Pausing...")
    timer.pause()
    delay(1000)
    println("Resuming...")
    timer.start()
    delay(4000)
    
    timer.onCleared()
}