---
type: "THEORY"
title: "Return Values: Getting Results Back"
---


So far, our functions only **do** things (print output). But what if you want a function to **calculate** something and give you the result to use elsewhere?

**That's where return values come in!**

### The Return Statement
Use the `return` keyword to send a value back from a function.

```kotlin
fun add(num1: Int, num2: Int): Int {
    return num1 + num2 // Returns the sum of num1 and num2
}

fun main() {
    val sum = add(5, 3) // 'sum' will be 8
    println("5 + 3 = $sum")
}
```

**Output**:
```text
5 + 3 = 8
```

**Anatomy of a Return Function**:
- `add(num1: Int, num2: Int): Int` - The `: Int` after the parameters specifies the **return type**.
- `return num1 + num2` - The `return` keyword sends the value back.

### Return Types Explained
The return type tells you what kind of value the function will give back:

```kotlin
fun getGreeting(name: String): String { // Returns a String
    return "Hello, $name!"
}

fun isEven(number: Int): Boolean { // Returns a Boolean
    return number % 2 == 0
}

fun getPi(): Double { // Returns a Double
    return 3.14159
}
```

---

### Using Return Values

Once a function returns a value, you can use it in many ways:

#### 1. Store in a Variable
```kotlin
val result = add(10, 5) // result = 15
```

#### 2. Use Directly in Expressions
```kotlin
println("Total: ${add(7, 8)}") // Prints "Total: 15"
```

#### 3. Print Directly
```kotlin
println(getGreeting("World")) // Prints "Hello, World!"
```

#### 4. Use in Conditions
```kotlin
if (isEven(4)) { // True
    println("It's even!")
}
```

#### 5. Chain Function Calls
```kotlin
val message = getGreeting("Kotlin").uppercase() // "HELLO, KOTLIN!"
```

---

### Functions with Early Return

A function can have multiple return statements:

```kotlin
fun getGrade(score: Int): String {
    if (score >= 90) {
        return "A"
    } else if (score >= 80) {
        return "B"
    } else if (score >= 70) {
        return "C"
    }
    return "F" // Default return if no other condition met
}

fun main() {
    println("Score 95: ${getGrade(95)}") // A
    println("Score 72: ${getGrade(72)}") // C
}
```

**How it works**:
- When a return is executed, the function immediately exits
- No code after the return runs
- Very useful for handling different cases

---

### Void Functions (Unit Type)

What about functions that don't return anything meaningful?
These functions are said to return `Unit`. `Unit` is Kotlin's type for "no meaningful return value." It's like `void` in other languages, but in Kotlin, you usually just omit the `: Unit` part, as it's the default.

---



```kotlin
fun printWelcome(name: String): Unit {
    println("Welcome, $name!")
    // No return statement needed
}

// Unit can be omitted (it's the default)
fun printGoodbye(name: String) {
    println("Goodbye, $name!")
}

fun main() {
    printWelcome("Alice")  // Welcome, Alice!
    printGoodbye("Bob")    // Goodbye, Bob!
}
```
