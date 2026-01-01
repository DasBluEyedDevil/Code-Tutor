---
type: "EXAMPLE"
title: "Higher-Order Functions"
---


Functions that take or return other functions:



```kotlin
// Function that takes a function
fun <T> List<T>.customFilter(predicate: (T) -> Boolean): List<T> =
    buildList {
        for (item in this@customFilter) {
            if (predicate(item)) add(item)
        }
    }

// Function that returns a function
fun multiplyBy(factor: Int): (Int) -> Int = { it * factor }

val double = multiplyBy(2)
val triple = multiplyBy(3)

println(double(5))  // 10
println(triple(5))  // 15

// Currying - transforming multi-arg function to chain of single-arg functions
fun add(a: Int): (Int) -> Int = { b -> a + b }

val add5 = add(5)
println(add5(3))  // 8
println(add5(10)) // 15

// Partial application
fun greet(greeting: String, name: String): String = "$greeting, $name!"

val sayHello: (String) -> String = { name -> greet("Hello", name) }
val sayGoodbye: (String) -> String = { name -> greet("Goodbye", name) }

println(sayHello("World"))  // "Hello, World!"
```
