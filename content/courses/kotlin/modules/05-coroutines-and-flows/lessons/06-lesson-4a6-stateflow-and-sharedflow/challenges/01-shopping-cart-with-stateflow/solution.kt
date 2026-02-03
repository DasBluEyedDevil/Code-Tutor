import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

data class CartItem(val id: Int, val name: String, val price: Double, val quantity: Int = 1)

data class CartState(
    val items: List<CartItem> = emptyList(),
    val total: Double = 0.0
)

sealed class CartEvent {
    data class ItemAdded(val name: String) : CartEvent()
    data class ItemRemoved(val name: String) : CartEvent()
    data object CartCleared : CartEvent()
}

class ShoppingCartViewModel {
    private val scope = CoroutineScope(Dispatchers.Default + SupervisorJob())
    
    private val _state = MutableStateFlow(CartState())
    val state: StateFlow<CartState> = _state.asStateFlow()
    
    private val _events = MutableSharedFlow<CartEvent>()
    val events: SharedFlow<CartEvent> = _events.asSharedFlow()
    
    private var nextId = 1
    
    fun addItem(name: String, price: Double) {
        val newItem = CartItem(nextId++, name, price)
        _state.update { currentState ->
            val newItems = currentState.items + newItem
            CartState(
                items = newItems,
                total = calculateTotal(newItems)
            )
        }
        scope.launch {
            _events.emit(CartEvent.ItemAdded(name))
        }
    }
    
    fun removeItem(id: Int) {
        val itemName = _state.value.items.find { it.id == id }?.name ?: return
        _state.update { currentState ->
            val newItems = currentState.items.filter { it.id != id }
            CartState(
                items = newItems,
                total = calculateTotal(newItems)
            )
        }
        scope.launch {
            _events.emit(CartEvent.ItemRemoved(itemName))
        }
    }
    
    fun clearCart() {
        _state.value = CartState()
        scope.launch {
            _events.emit(CartEvent.CartCleared)
        }
    }
    
    private fun calculateTotal(items: List<CartItem>): Double {
        return items.sumOf { it.price * it.quantity }
    }
    
    fun onCleared() = scope.cancel()
}

fun main() = runBlocking {
    val cart = ShoppingCartViewModel()
    
    launch {
        cart.state.collect { state ->
            println("Cart: ${state.items.map { it.name }} Total: $${state.total}")
        }
    }
    
    launch {
        cart.events.collect { event ->
            println("Event: $event")
        }
    }
    
    delay(100)
    cart.addItem("Apple", 1.50)
    delay(100)
    cart.addItem("Banana", 0.75)
    delay(100)
    cart.addItem("Orange", 2.00)
    delay(100)
    cart.removeItem(2)
    delay(100)
    cart.clearCart()
    delay(200)
    
    cart.onCleared()
}