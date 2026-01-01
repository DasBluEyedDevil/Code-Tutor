---
type: "THEORY"
title: "Reading Annotations at Runtime"
---



### Finding Annotated Members


---



```kotlin
import kotlin.reflect.full.*

@Target(AnnotationTarget.FUNCTION)
@Retention(AnnotationRetention.RUNTIME)
annotation class Test

class TestSuite {
    @Test
    fun test1() = println("Running test 1")

    @Test
    fun test2() = println("Running test 2")

    fun helper() = println("Helper function")
}

fun main() {
    val testSuite = TestSuite()
    val kClass = TestSuite::class

    val testFunctions = kClass.memberFunctions.filter { function ->
        function.annotations.any { it is Test }
    }

    println("Running ${testFunctions.size} tests:")
    testFunctions.forEach { function ->
        function.call(testSuite)
    }
}
// Output:
// Running 2 tests:
// Running test 1
// Running test 2
```
