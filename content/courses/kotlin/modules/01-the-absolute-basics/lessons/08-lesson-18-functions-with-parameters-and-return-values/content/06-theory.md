---
type: "THEORY"
title: "Single-Expression Functions"
---


When a function is simple and returns a single expression, Kotlin has a shortcut:

### Traditional Way vs. Shortcut


All three versions do exactly the same thing, but the single-expression version is more concise!

---

### More Single-Expression Examples


**When to use single-expression functions**:
- ✅ Function body is one simple expression
- ✅ Makes code more readable and concise
- ❌ Don't use if the logic is complex or needs multiple lines

---



```kotlin
// Math operations
fun square(x: Int) = x * x
fun cube(x: Int) = x * x * x
fun double(x: Int) = x * 2

// Boolean checks
fun isEven(n: Int) = n % 2 == 0
fun isPositive(n: Int) = n > 0
fun isAdult(age: Int) = age >= 18

// String operations
fun greet(name: String) = "Hello, $name!"
fun shout(text: String) = text.uppercase() + "!"

// Conditional expressions
fun max(a: Int, b: Int) = if (a > b) a else b
fun min(a: Int, b: Int) = if (a < b) a else b
fun absoluteValue(n: Int) = if (n >= 0) n else -n

fun main() {
    println(square(5))           // 25
    println(isEven(4))           // true
    println(greet("Alice"))      // Hello, Alice!
    println(max(10, 20))         // 20
    println(absoluteValue(-7))   // 7
}
```
