---
type: "THEORY"
title: "Parameters: Passing Data to Functions"
---


### Single Parameter
Parameters allow you to pass data into a function.

```kotlin
fun greetUser(name: String) {
    println("Hello, $name!")
}

fun main() {
    greetUser("Alice")
    greetUser("Bob")
}
```

**Parameter Structure**: `name: String` (name of parameter followed by its type).

### Multiple Parameters
You can pass multiple pieces of data by separating them with commas.

```kotlin
fun printSum(a: Int, b: Int) {
    val result = a + b
    println("The sum of $a and $b is $result")
}
```

### Parameters with Different Types
Functions can take any combination of types.

```kotlin
fun printReceipt(item: String, price: Double, quantity: Int) {
    val subtotal = price * quantity
    val tax = subtotal * 0.08
    val total = subtotal + tax
    
    println("Item: $item")
    println("Subtotal: $$subtotal")
    println("Tax: $$tax")
    println("Total: $$total")
}
```

**Output** (when called with `printReceipt("Widget", 19.99, 3)`):

```kotlin
Item: Widget
Subtotal: $59.97
Tax: $4.7976
Total: $64.7676
```
