---
type: "THEORY"
title: "Solution 3: Function Builder"
---



**Explanation**:
- `createGreeter` is a factory function that returns functions
- Based on style parameter, it returns different greeting implementations
- Each returned function has the same signature: `(String) -> String`
- This demonstrates functions returning functions—powerful abstraction!

---



```kotlin
fun createGreeter(style: String): (String) -> String {
    return when (style) {
        "formal" -> { name -> "Good day, $name. How may I assist you?" }
        "casual" -> { name -> "Hey $name! What's up?" }
        "enthusiastic" -> { name -> "OH WOW! Hi $name!!! So great to see you!!!" }
        else -> { name -> "Hello, $name." }
    }
}

fun main() {
    val formalGreeter = createGreeter("formal")
    val casualGreeter = createGreeter("casual")
    val enthusiasticGreeter = createGreeter("enthusiastic")

    val person = "Alice"

    println(formalGreeter(person))
    // Output: Good day, Alice. How may I assist you?

    println(casualGreeter(person))
    // Output: Hey Alice! What's up?

    println(enthusiasticGreeter(person))
    // Output: OH WOW! Hi Alice!!! So great to see you!!!

    // Can also create and use immediately
    println(createGreeter("unknown")(person))
    // Output: Hello, Alice.
}
```
