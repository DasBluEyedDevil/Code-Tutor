---
type: "THEORY"
title: "Reflection Basics"
---


Reflection allows inspecting and manipulating code at runtime.

### Getting Class References


### KClass - Class Metadata


### KProperty - Property Reflection


### KFunction - Function Reflection


---



```kotlin
import kotlin.reflect.full.*

class Calculator {
    fun add(a: Int, b: Int): Int = a + b

    fun multiply(a: Int, b: Int, c: Int = 1): Int = a * b * c
}

fun main() {
    val calc = Calculator()
    val kClass = Calculator::class

    val addFunction = kClass.memberFunctions.find { it.name == "add" }!!

    println("Function: ${addFunction.name}")
    println("Parameters: ${addFunction.parameters.map { it.name }}")
    println("Return type: ${addFunction.returnType}")

    // Call function
    val result = addFunction.call(calc, 5, 3)
    println("Result: $result")  // 8

    // Call with named parameters
    val multiplyFunction = kClass.memberFunctions.find { it.name == "multiply" }!!
    val result2 = multiplyFunction.callBy(
        mapOf(
            multiplyFunction.parameters[0] to calc,  // instance
            multiplyFunction.parameters[1] to 2,      // a
            multiplyFunction.parameters[2] to 3       // b (c uses default)
        )
    )
    println("Multiply result: $result2")  // 6
}
```
