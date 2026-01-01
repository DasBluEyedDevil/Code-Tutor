---
type: "THEORY"
title: "Solution 2"
---



**Tests**:

---



```kotlin
// src/androidTest/kotlin/com/example/ui/CartScreenTest.kt
class CartScreenTest {

    @get:Rule
    val composeTestRule = createComposeRule()

    @Test
    fun cartScreen_emptyCart_showsEmptyMessage() {
        composeTestRule.setContent {
            CartScreen(items = emptyList())
        }

        composeTestRule.onNodeWithTag("emptyMessage")
            .assertIsDisplayed()
            .assertTextEquals("Your cart is empty")
    }

    @Test
    fun cartScreen_withItems_displaysAllItems() {
        val items = listOf(
            CartItem("1", "Laptop", 999.99, 1),
            CartItem("2", "Mouse", 29.99, 2)
        )

        composeTestRule.setContent {
            CartScreen(items = items)
        }

        composeTestRule.onNodeWithTag("cartItem-1").assertIsDisplayed()
        composeTestRule.onNodeWithTag("cartItem-2").assertIsDisplayed()
        composeTestRule.onNodeWithText("Laptop").assertExists()
        composeTestRule.onNodeWithText("Mouse").assertExists()
    }

    @Test
    fun cartScreen_calculatesCorrectTotal() {
        val items = listOf(
            CartItem("1", "Laptop", 999.99, 1),
            CartItem("2", "Mouse", 29.99, 2)
        )

        composeTestRule.setContent {
            CartScreen(items = items)
        }

        // Total: 999.99 + (29.99 * 2) = 1059.97
        composeTestRule.onNodeWithTag("totalPrice")
            .assertTextContains("$1059.97")
    }

    @Test
    fun cartScreen_clickRemove_callsOnRemoveItem() {
        val items = listOf(CartItem("1", "Laptop", 999.99, 1))
        var removedId: String? = null

        composeTestRule.setContent {
            CartScreen(
                items = items,
                onRemoveItem = { id -> removedId = id }
            )
        }

        composeTestRule.onNodeWithTag("removeButton-1").performClick()

        assertEquals("1", removedId)
    }

    @Test
    fun cartScreen_clickCheckout_callsOnCheckout() {
        val items = listOf(CartItem("1", "Laptop", 999.99, 1))
        var checkoutCalled = false

        composeTestRule.setContent {
            CartScreen(
                items = items,
                onCheckout = { checkoutCalled = true }
            )
        }

        composeTestRule.onNodeWithTag("checkoutButton").performClick()

        assertTrue(checkoutCalled)
    }
}
```
