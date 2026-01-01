---
type: "THEORY"
title: "Solution 2: Shopping Cart with Unique Items"
---



**Solution Code**:

```kotlin
fun addItem(cart: MutableMap<String, Int>, item: String, quantity: Int) {
    val currentQuantity = cart.getOrDefault(item, 0)
    cart[item] = currentQuantity + quantity
    println("Added $quantity x $item")
}

fun removeItem(cart: MutableMap<String, Int>, item: String) {
    if (cart.containsKey(item)) {
        cart.remove(item)
        println("Removed $item from cart")
    }
}

fun updateQuantity(cart: MutableMap<String, Int>, item: String, newQuantity: Int) {
    if (cart.containsKey(item)) {
        cart[item] = newQuantity
        println("Updated $item quantity to $newQuantity")
    }
}

fun displayCart(cart: Map<String, Int>) {
    println("\n=== Shopping Cart ===")
    cart.forEach { (item, qty) -> println("  $item: $qty") }
    println("Total items: ${cart.values.sum()}")
}

fun main() {
    val cart = mutableMapOf<String, Int>()
    
    addItem(cart, "Apple", 5)
    addItem(cart, "Banana", 3)
    addItem(cart, "Orange", 4)
    displayCart(cart)
    
    addItem(cart, "Apple", 2)
    displayCart(cart)
    
    updateQuantity(cart, "Banana", 6)
    displayCart(cart)
    
    removeItem(cart, "Orange")
    displayCart(cart)
}
```

**Sample Output**:

```text
Added 5 x Apple
Added 3 x Banana
Added 4 x Orange

=== Shopping Cart ===
  Apple: 5
  Banana: 3
  Orange: 4
Total items: 12

Added 2 x Apple

=== Shopping Cart ===
  Apple: 7
  Banana: 3
  Orange: 4
Total items: 14

Updated Banana quantity to 6

=== Shopping Cart ===
  Apple: 7
  Banana: 6
  Orange: 4
Total items: 17

Removed Orange from cart

=== Shopping Cart ===
  Apple: 7
  Banana: 6
Total items: 13
```
