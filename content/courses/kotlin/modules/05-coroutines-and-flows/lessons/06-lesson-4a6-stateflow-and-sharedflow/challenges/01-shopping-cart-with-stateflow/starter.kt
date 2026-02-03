import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

data class CartItem(val id: Int, val name: String, val price: Double, val quantity: Int)

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
    // TODO: Create _state and state
    // TODO: Create _events and events
    
    private var nextId = 1
    
    // TODO: Implement addItem
    fun addItem(name: String, price: Double) {
        // Add item to cart
        // Emit ItemAdded event
    }
    
    // TODO: Implement removeItem
    fun removeItem(id: Int) {
        // Remove item from cart
        // Emit ItemRemoved event
    }
    
    // TODO: Implement clearCart
    fun clearCart() {
        // Clear all items
        // Emit CartCleared event
    }
    
    private fun calculateTotal(items: List<CartItem>): Double {
        return items.sumOf { it.price * it.quantity }
    }
}

fun main() = runBlocking {
    // Test your implementation
}