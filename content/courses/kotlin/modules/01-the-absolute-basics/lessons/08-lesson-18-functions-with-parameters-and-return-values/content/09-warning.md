---
type: "WARNING"
title: "Common Pitfalls and Best Practices"
---


### Common Mistakes

#### Mistake 1: Wrong Number of Arguments
Calling a function with too few or too many arguments will result in a compile-time error.
```kotlin
fun greet(name: String) = println("Hello, $name!")
// greet() // ❌ Error: Not enough arguments
// greet("Alice", "Bob") // ❌ Error: Too many arguments
```

---

#### Mistake 2: Wrong Argument Type
Passing an argument of the wrong data type will cause a compile-time error.
```kotlin
fun printNumber(num: Int) = println("Number: $num")
// printNumber("five") // ❌ Error: Type mismatch
```

---

#### Mistake 3: Wrong Argument Order
If you don't use named arguments, the order of arguments must match the order of parameters.
```kotlin
fun createUser(name: String, age: Int) = println("User: $name, Age: $age")
// createUser(25, "Bob") // ❌ Error: Type mismatch (age assigned to name)
createUser("Bob", 25) // ✅ Correct
```

---

#### Mistake 4: Forgetting Return Statement
If a function has a declared return type other than `Unit`, it must have a `return` statement that is reachable in all code paths.
```kotlin
fun getValue(condition: Boolean): Int {
    if (condition) {
        return 10
    }
    // ❌ Error: This function must return a value
}
```

---

#### Mistake 5: Incorrect Return Type
The value returned must match the declared return type.
```kotlin
fun getText(): String {
    return 123 // ❌ Error: Type mismatch (Int instead of String)
}
```

---

### Best Practices

#### 1. Use Descriptive Parameter Names
Choose parameter names that clearly indicate their purpose and what type of data they are expected to hold.
```kotlin
// fun calculate(a: Double, b: Double) // ❌ Unclear
fun calculateArea(width: Double, height: Double) // ✅ Clear
```

---

#### 2. Keep Functions Focused (Single Responsibility)
Each function should ideally perform one specific task. This makes functions easier to understand, test, and reuse.
```kotlin
// ❌ Does too much
fun processOrderAndSendEmail() { /* ... */ }
// ✅ Better
fun processOrder() { /* ... */ }
fun sendOrderConfirmationEmail() { /* ... */ }
```

---

#### 3. Use Default Parameters for Optional Values
Avoid creating multiple overloaded functions by using default parameter values where appropriate.
```kotlin
// ❌ Overload hell
// fun log(message: String)
// fun log(message: String, level: String)
// ✅ Better with default
fun log(message: String, level: String = "INFO") { println("[$level] $message") }
```

---

#### 4. Use Single-Expression Functions for Simple Logic
For concise functions that compute and return a single value, prefer the single-expression syntax.
```kotlin
fun square(x: Int) = x * x
```

---

#### 5. Validate Input Parameters
If a function relies on certain conditions for its parameters (e.g., a number must be positive, a string cannot be empty), add checks at the beginning of the function.
This makes your functions more robust.

---



```kotlin
fun divide(a: Double, b: Double): Double {
    if (b == 0.0) {
        println("Error: Cannot divide by zero!")
        return 0.0
    }
    return a / b
}

fun createUser(name: String, age: Int) {
    if (name.isBlank()) {
        println("Error: Name cannot be empty!")
        return
    }
    if (age < 0 || age > 150) {
        println("Error: Invalid age!")
        return
    }
    println("User created: $name, age $age")
}
```
