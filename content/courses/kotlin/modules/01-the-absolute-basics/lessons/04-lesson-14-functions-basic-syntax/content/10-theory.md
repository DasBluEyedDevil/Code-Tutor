---
type: "THEORY"
title: "Extension Functions"
---


Kotlin allows you to add new functions to existing classes without having to inherit from them. These are called **Extension Functions**.

### Basic Extension Function
You define an extension function by prefixing the class name to the function name.

```kotlin
fun String.addExclamation(): String {
    return this + "!"
}

fun main() {
    val greeting = "Hello"
    println(greeting.addExclamation()) // Output: Hello!
}
```

In extension functions, `this` refers to the object the function is called on.

### More Extension Examples
Extension functions are incredibly useful for adding utility methods to standard library types like `Int`, `Double`, or `List`.

### Why Extension Functions?
They make code more readable and allow you to "extend" classes that you don't own (like those in the Kotlin Standard Library or 3rd-party libraries).

---



```kotlin
// Without extension
val doubled = multiplyBy2(number)
val formatted = formatAsCurrency(price)

// With extension
val doubled = number.double()
val formatted = price.asCurrency()
```
