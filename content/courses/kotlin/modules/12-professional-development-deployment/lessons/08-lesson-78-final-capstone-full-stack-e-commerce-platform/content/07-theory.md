---
type: "THEORY"
title: "Phase 3: Android App Development (4-6 hours)"
---


### 3.1 API Client


### 3.2 Product Screen


### 3.3 Cart ViewModel


---



```kotlin
// app/src/main/kotlin/com/shopkotlin/ui/screens/cart/CartViewModel.kt
package com.shopkotlin.ui.screens.cart

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.shopkotlin.data.repository.CartRepository
import com.shopkotlin.models.CartItem
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch

class CartViewModel(
    private val cartRepository: CartRepository
) : ViewModel() {

    private val _cartItems = MutableStateFlow<List<CartItem>>(emptyList())
    val cartItems: StateFlow<List<CartItem>> = _cartItems.asStateFlow()

    val totalAmount: StateFlow<Double> = cartItems.map { items ->
        items.sumOf { it.product.price * it.quantity }
    }.stateIn(viewModelScope, SharingStarted.WhileSubscribed(5000), 0.0)

    init {
        loadCart()
    }

    private fun loadCart() {
        viewModelScope.launch {
            cartRepository.getCartItems().collect { items ->
                _cartItems.value = items
            }
        }
    }

    fun updateQuantity(productId: String, quantity: Int) {
        viewModelScope.launch {
            if (quantity <= 0) {
                cartRepository.removeFromCart(productId)
            } else {
                cartRepository.updateQuantity(productId, quantity)
            }
        }
    }

    fun removeItem(productId: String) {
        viewModelScope.launch {
            cartRepository.removeFromCart(productId)
        }
    }

    fun clearCart() {
        viewModelScope.launch {
            cartRepository.clearCart()
        }
    }
}
```
