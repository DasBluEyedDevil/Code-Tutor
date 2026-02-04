import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update

sealed interface CounterIntent {
    data object Increment : CounterIntent
    data object Decrement : CounterIntent
    data object Reset : CounterIntent
}

data class CounterState(
    val count: Int = 0,
    val history: List<String> = emptyList()
)

class CounterStore {
    private val _state = MutableStateFlow(CounterState())
    val state: StateFlow<CounterState> = _state.asStateFlow()

    fun processIntent(intent: CounterIntent) {
        _state.update { current ->
            when (intent) {
                is CounterIntent.Increment -> current.copy(
                    count = current.count + 1,
                    history = current.history + "Increment"
                )
                is CounterIntent.Decrement -> current.copy(
                    count = current.count - 1,
                    history = current.history + "Decrement"
                )
                is CounterIntent.Reset -> CounterState()
            }
        }
    }
}

fun main() {
    val store = CounterStore()
    println(store.state.value)

    store.processIntent(CounterIntent.Increment)
    println(store.state.value)

    store.processIntent(CounterIntent.Increment)
    store.processIntent(CounterIntent.Increment)
    println(store.state.value)

    store.processIntent(CounterIntent.Reset)
    println(store.state.value)
}
