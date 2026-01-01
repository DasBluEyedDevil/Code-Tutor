---
type: "THEORY"
title: "Backing Fields"
---


A **backing field** is the actual storage for a property. Kotlin generates it automatically when needed.

**When Kotlin generates a backing field**:
- Property has a default accessor (getter/setter)
- Property has a custom accessor that uses `field`

**When Kotlin does NOT generate a backing field**:
- Property only has a custom getter that doesn't use `field`


**Example: Tracking Property Changes**


---



```kotlin
class Product(name: String, price: Double) {
    var name: String = name
        set(value) {
            println("Name changed from '$field' to '$value'")
            field = value
        }

    var price: Double = price
        set(value) {
            require(value >= 0) { "Price cannot be negative" }
            println("Price changed from $$field to $$value")
            field = value
        }
}

fun main() {
    val product = Product("Laptop", 999.99)

    product.name = "Gaming Laptop"  // Name changed from 'Laptop' to 'Gaming Laptop'
    product.price = 1299.99          // Price changed from $999.99 to $1299.99
}
```
