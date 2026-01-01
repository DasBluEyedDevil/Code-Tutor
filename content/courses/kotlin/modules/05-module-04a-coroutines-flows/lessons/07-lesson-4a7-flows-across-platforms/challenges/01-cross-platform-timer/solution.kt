import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

data class TimerState(
    val remainingSeconds: Int,
    val isRunning: Boolean,
    val isFinished: Boolean
)

class CountdownTimer(private val durationSeconds: Int) {
    private val scope = CoroutineScope(Dispatchers.Default + SupervisorJob())
    
    private val _state = MutableStateFlow(
        TimerState(
            remainingSeconds = durationSeconds,
            isRunning = false,
            isFinished = false
        )
    )
    val state: StateFlow<TimerState> = _state.asStateFlow()
    
    private var timerJob: Job? = null
    
    fun start() {
        if (_state.value.isFinished || _state.value.isRunning) return
        
        _state.update { it.copy(isRunning = true) }
        
        timerJob = scope.launch {
            while (_state.value.remainingSeconds > 0 && isActive) {
                delay(1000)
                _state.update { currentState ->
                    val newRemaining = currentState.remainingSeconds - 1
                    currentState.copy(
                        remainingSeconds = newRemaining,
                        isFinished = newRemaining <= 0,
                        isRunning = newRemaining > 0
                    )
                }
            }
        }
    }
    
    fun pause() {
        timerJob?.cancel()
        timerJob = null
        _state.update { it.copy(isRunning = false) }
    }
    
    fun reset() {
        timerJob?.cancel()
        timerJob = null
        _state.value = TimerState(
            remainingSeconds = durationSeconds,
            isRunning = false,
            isFinished = false
        )
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