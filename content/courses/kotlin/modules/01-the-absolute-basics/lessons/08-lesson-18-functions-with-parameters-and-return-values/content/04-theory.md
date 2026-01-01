---
type: "THEORY"
title: "Parameters: Giving Functions Input"
---


### Single Parameter

The simplest case—one input to customize the function:

```kotlin
fun greet(name: String) {
    println("Hello, $name!")
}

fun main() {
    greet("Alice") // Calls greet with argument "Alice"
    greet("Bob")   // Calls greet with argument "Bob"
}
```

**Output**:
```text
Hello, Alice!
Hello, Bob!
```

**Breaking it down**:
- `name: String` inside the parentheses defines a parameter named `name` of type `String`.
- When `greet("Alice")` is called, "Alice" is passed as an argument, and inside `greet`, the `name` variable holds "Alice".

### Multiple Parameters

Functions can accept multiple inputs:

```kotlin
fun addNumbers(num1: Int, num2: Int) {
    val sum = num1 + num2
    println("The sum of $num1 and $num2 is $sum")
}

fun main() {
    addNumbers(10, 20)
    addNumbers(5, 3)
}
```

**Output**:
```text
The sum of 10 and 20 is 30
The sum of 5 and 3 is 8
```

**Important**: Order matters!
- First argument (`10`) → first parameter (`num1`)
- Second argument (`20`) → second parameter (`num2`)

### Parameters with Different Types

You can mix and match any data types:

```kotlin
fun displayUserInfo(name: String, age: Int, isStudent: Boolean) {
    println("Name: $name, Age: $age, Student: $isStudent")
}

fun main() {
    displayUserInfo("Charlie", 30, true)
    displayUserInfo("Dana", 22, false)
}
```

**Output**:
```text
Name: Charlie, Age: 30, Student: true
Name: Dana, Age: 22, Student: false
```

### Practical Example: Calculation Function

```kotlin
fun calculateOrder(itemPrice: Double, quantity: Int, taxRate: Double) {
    val subtotal = itemPrice * quantity
    val taxAmount = subtotal * taxRate
    val total = subtotal + taxAmount

    println("Item Price: $itemPrice")
    println("Quantity: $quantity")
    println("Subtotal: $subtotal")
    println("Tax (${"%.1f".format(taxRate * 100)}%): $%.2f".format(taxAmount))
    println("Total: $%.2f".format(total))
}

fun main() {
    calculateOrder(19.99, 3, 0.08)
}
```

**Output**:
```text
Item Price: $19.99
Quantity: 3
Subtotal: $59.97
Tax (8.0%): $4.80
Total: $64.77
```
