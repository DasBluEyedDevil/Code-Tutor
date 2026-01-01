---
type: "EXAMPLE"
title: "Practical Examples: Real-World Use Cases"
---


### Example 1: Form Validation


### Example 2: Event Handling


### Example 3: Strategy Pattern with Functions


---



```kotlin
class PriceCalculator {
    fun calculatePrice(
        basePrice: Double,
        quantity: Int,
        discountStrategy: (Double, Int) -> Double
    ): Double {
        return discountStrategy(basePrice, quantity)
    }
}

// Different discount strategies
val noDiscount = { price: Double, qty: Int -> price * qty }
val bulkDiscount = { price: Double, qty: Int ->
    if (qty >= 10) price * qty * 0.9 else price * qty
}
val loyaltyDiscount = { price: Double, qty: Int -> price * qty * 0.85 }

val calculator = PriceCalculator()

println(calculator.calculatePrice(100.0, 5, noDiscount))        // 500.0
println(calculator.calculatePrice(100.0, 15, bulkDiscount))     // 1350.0
println(calculator.calculatePrice(100.0, 5, loyaltyDiscount))   // 425.0
```
