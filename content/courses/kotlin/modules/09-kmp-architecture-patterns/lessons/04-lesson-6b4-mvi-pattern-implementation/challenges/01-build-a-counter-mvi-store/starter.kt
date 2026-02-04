import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update

// TODO: Define CounterIntent sealed interface with:
//   - Increment (data object)
//   - Decrement (data object)
//   - Reset (data object)

// TODO: Define CounterState data class with:
//   - count: Int (default 0)
//   - history: List<String> (default emptyList)

// TODO: Implement CounterStore
class CounterStore {
    // 1. Create MutableStateFlow<CounterState> for internal state
    // 2. Expose as StateFlow<CounterState>
    // 3. Implement processIntent(intent: CounterIntent)
    //    - Use when to match intent type
    //    - Update state with _state.update { }
    //    - Increment: count + 1, add "Increment" to history
    //    - Decrement: count - 1, add "Decrement" to history
    //    - Reset: count = 0, clear history
}

fun main() {
    val store = CounterStore()
    println(store.state.value) // CounterState(count=0, history=[])

    store.processIntent(CounterIntent.Increment)
    println(store.state.value) // CounterState(count=1, history=[Increment])

    store.processIntent(CounterIntent.Increment)
    store.processIntent(CounterIntent.Increment)
    println(store.state.value) // CounterState(count=3, history=[Increment, Increment, Increment])

    store.processIntent(CounterIntent.Reset)
    println(store.state.value) // CounterState(count=0, history=[])
}
