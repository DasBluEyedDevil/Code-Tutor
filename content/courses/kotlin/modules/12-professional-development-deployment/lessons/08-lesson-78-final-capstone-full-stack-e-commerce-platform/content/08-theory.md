---
type: "THEORY"
title: "Phase 4: Testing (2-3 hours)"
---


### Backend Tests


### Android Tests


---



```kotlin
// app/src/test/kotlin/com/shopkotlin/CartViewModelTest.kt
package com.shopkotlin

import app.cash.turbine.test
import com.shopkotlin.data.repository.CartRepository
import com.shopkotlin.models.*
import com.shopkotlin.ui.screens.cart.CartViewModel
import io.mockk.*
import kotlinx.coroutines.flow.flowOf
import kotlinx.coroutines.test.*
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class CartViewModelTest {

    private val cartRepository = mockk<CartRepository>()
    private val viewModel = CartViewModel(cartRepository)

    @Test
    fun `cart items should be loaded on init`() = runTest {
        // Arrange
        val cartItems = listOf(
            CartItem(product = mockk(), quantity = 2)
        )

        coEvery { cartRepository.getCartItems() } returns flowOf(cartItems)

        // Act
        viewModel.cartItems.test {
            val items = awaitItem()

            // Assert
            assertEquals(cartItems, items)
        }
    }

    @Test
    fun `updateQuantity should call repository`() = runTest {
        coEvery { cartRepository.updateQuantity(any(), any()) } just Runs

        viewModel.updateQuantity("product1", 5)

        coVerify { cartRepository.updateQuantity("product1", 5) }
    }

    @Test
    fun `totalAmount should sum all items`() = runTest {
        val product1 = mockk<Product> {
            every { price } returns 10.0
        }
        val product2 = mockk<Product> {
            every { price } returns 20.0
        }

        val cartItems = listOf(
            CartItem(product1, 2), // 20.0
            CartItem(product2, 3)  // 60.0
        )

        coEvery { cartRepository.getCartItems() } returns flowOf(cartItems)

        viewModel.totalAmount.test {
            val total = awaitItem()
            assertEquals(80.0, total, 0.01)
        }
    }
}
```
