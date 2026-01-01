// ========== DOMAIN LAYER ==========

data class CartItem(
    val id: String,
    val productId: String,
    val productName: String,
    val unitPrice: Double,
    val quantity: Int,
    val imageUrl: String?
) {
    val totalPrice: Double get() = unitPrice * quantity
}

data class Cart(
    val items: List<CartItem>,
    val discountCode: String? = null,
    val discountPercent: Double = 0.0
) {
    val subtotal: Double get() = items.sumOf { it.totalPrice }
    val discount: Double get() = subtotal * discountPercent
    val total: Double get() = subtotal - discount
    val itemCount: Int get() = items.sumOf { it.quantity }
}

interface CartRepository {
    fun observeCart(): Flow<Cart>
    suspend fun addItem(productId: String, quantity: Int = 1)
    suspend fun removeItem(itemId: String)
    suspend fun updateQuantity(itemId: String, quantity: Int)
    suspend fun applyDiscount(code: String): Result<Double>
    suspend fun clearCart()
    suspend fun syncWithBackend(): Result<Unit>
}

// ========== PRESENTATION LAYER ==========

data class CartUiState(
    val items: List<CartItemUiModel> = emptyList(),
    val subtotal: String = "$0.00",
    val discount: String? = null,
    val total: String = "$0.00",
    val itemCount: Int = 0,
    val isLoading: Boolean = true,
    val isSyncing: Boolean = false,
    val error: String? = null,
    val discountCode: String = "",
    val discountError: String? = null
)

data class CartItemUiModel(
    val id: String,
    val name: String,
    val price: String,
    val quantity: Int,
    val totalPrice: String,
    val imageUrl: String?
)

sealed interface CartAction {
    data class AddItem(val productId: String) : CartAction
    data class RemoveItem(val itemId: String) : CartAction
    data class UpdateQuantity(val itemId: String, val quantity: Int) : CartAction
    data class DiscountCodeChanged(val code: String) : CartAction
    data object ApplyDiscount : CartAction
    data object ClearCart : CartAction
    data object Sync : CartAction
    data object DismissError : CartAction
}

class CartViewModel(
    private val cartRepository: CartRepository,
    private val priceFormatter: PriceFormatter
) : BaseViewModel() {
    
    private val _state = MutableStateFlow(CartUiState())
    val state: StateFlow<CartUiState> = _state.asStateFlow()
    
    init {
        observeCart()
    }
    
    fun onAction(action: CartAction) {
        when (action) {
            is CartAction.AddItem -> addItem(action.productId)
            is CartAction.RemoveItem -> removeItem(action.itemId)
            is CartAction.UpdateQuantity -> updateQuantity(action.itemId, action.quantity)
            is CartAction.DiscountCodeChanged -> _state.update { it.copy(discountCode = action.code) }
            is CartAction.ApplyDiscount -> applyDiscount()
            is CartAction.ClearCart -> clearCart()
            is CartAction.Sync -> sync()
            is CartAction.DismissError -> _state.update { it.copy(error = null, discountError = null) }
        }
    }
    
    private fun observeCart() {
        scope.launch {
            cartRepository.observeCart()
                .catch { e -> _state.update { it.copy(error = e.message, isLoading = false) } }
                .collect { cart -> updateStateFromCart(cart) }
        }
    }
    
    private fun updateStateFromCart(cart: Cart) {
        _state.update { state ->
            state.copy(
                items = cart.items.map { it.toUiModel() },
                subtotal = priceFormatter.format(cart.subtotal),
                discount = if (cart.discountPercent > 0) "-${priceFormatter.format(cart.discount)}" else null,
                total = priceFormatter.format(cart.total),
                itemCount = cart.itemCount,
                isLoading = false
            )
        }
    }
    
    private fun addItem(productId: String) {
        scope.launch { cartRepository.addItem(productId) }
    }
    
    private fun removeItem(itemId: String) {
        scope.launch { cartRepository.removeItem(itemId) }
    }
    
    private fun updateQuantity(itemId: String, quantity: Int) {
        if (quantity <= 0) {
            removeItem(itemId)
        } else {
            scope.launch { cartRepository.updateQuantity(itemId, quantity) }
        }
    }
    
    private fun applyDiscount() {
        val code = _state.value.discountCode
        if (code.isBlank()) return
        
        scope.launch {
            cartRepository.applyDiscount(code)
                .onSuccess { percent ->
                    _state.update { it.copy(discountError = null) }
                }
                .onFailure { e ->
                    _state.update { it.copy(discountError = "Invalid code") }
                }
        }
    }
    
    private fun clearCart() {
        scope.launch { cartRepository.clearCart() }
    }
    
    private fun sync() {
        scope.launch {
            _state.update { it.copy(isSyncing = true) }
            cartRepository.syncWithBackend()
                .onFailure { e -> _state.update { it.copy(error = "Sync failed") } }
            _state.update { it.copy(isSyncing = false) }
        }
    }
    
    private fun CartItem.toUiModel() = CartItemUiModel(
        id = id,
        name = productName,
        price = priceFormatter.format(unitPrice),
        quantity = quantity,
        totalPrice = priceFormatter.format(totalPrice),
        imageUrl = imageUrl
    )
}