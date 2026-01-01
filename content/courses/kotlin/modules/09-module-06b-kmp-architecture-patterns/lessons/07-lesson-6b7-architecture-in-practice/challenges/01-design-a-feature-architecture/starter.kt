// Shopping Cart Feature Requirements:
// - Display list of cart items (product name, quantity, price)
// - Add item to cart
// - Remove item from cart
// - Update item quantity
// - Calculate total price
// - Apply discount code
// - Clear cart
// - Persist cart locally
// - Sync with backend when online

// Design your architecture:

// 1. Domain Models
// data class CartItem(...)
// data class Cart(...)

// 2. Repository Interface
// interface CartRepository { ... }

// 3. ViewModel State
// data class CartUiState(...)

// 4. ViewModel Actions/Intents
// sealed interface CartAction { ... }

// 5. ViewModel Implementation (skeleton)
// class CartViewModel(...) { ... }