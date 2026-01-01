---
type: "THEORY"
title: "Exercises"
---


### Exercise 1: JSON Builder (Medium)

Create a type-safe JSON builder DSL.

**Requirements**:
- Support objects and arrays
- Support primitives (string, number, boolean, null)
- Nested structures
- Pretty-print output

**Solution**:


### Exercise 2: SQL Query Builder (Hard)

Create a type-safe SQL query builder.

**Requirements**:
- SELECT with columns
- FROM with table
- WHERE with conditions
- ORDER BY
- LIMIT

**Solution**:


### Exercise 3: Test DSL (Hard)

Create a test framework DSL similar to Kotest or Spek.

**Requirements**:
- describe/it blocks
- Nested contexts
- Assertions
- Setup/teardown hooks

**Solution**:


---



```kotlin
@DslMarker
annotation class TestMarker

@TestMarker
class TestSuite(val name: String) {
    private val specs = mutableListOf<Spec>()
    private var beforeEach: (() -> Unit)? = null
    private var afterEach: (() -> Unit)? = null

    fun describe(description: String, action: Context.() -> Unit) {
        specs.add(Context(description).apply(action))
    }

    fun beforeEach(action: () -> Unit) {
        beforeEach = action
    }

    fun afterEach(action: () -> Unit) {
        afterEach = action
    }

    fun run() {
        println("Test Suite: $name\n")
        var passed = 0
        var failed = 0

        specs.forEach { spec ->
            val results = spec.run(beforeEach, afterEach)
            passed += results.first
            failed += results.second
        }

        println("\n${passed} passed, $failed failed")
    }
}

@TestMarker
sealed class Spec {
    abstract fun run(beforeEach: (() -> Unit)?, afterEach: (() -> Unit)?): Pair<Int, Int>
}

@TestMarker
class Context(private val description: String) : Spec() {
    private val tests = mutableListOf<Test>()
    private val subContexts = mutableListOf<Context>()

    fun it(description: String, action: () -> Unit) {
        tests.add(Test(description, action))
    }

    fun describe(description: String, action: Context.() -> Unit) {
        subContexts.add(Context(description).apply(action))
    }

    override fun run(beforeEach: (() -> Unit)?, afterEach: (() -> Unit)?): Pair<Int, Int> {
        println("  $description")
        var passed = 0
        var failed = 0

        tests.forEach { test ->
            val result = test.run(beforeEach, afterEach)
            if (result.first == 1) passed++ else failed++
        }

        subContexts.forEach { context ->
            val results = context.run(beforeEach, afterEach)
            passed += results.first
            failed += results.second
        }

        return Pair(passed, failed)
    }
}

@TestMarker
class Test(private val description: String, private val action: () -> Unit) : Spec() {
    override fun run(beforeEach: (() -> Unit)?, afterEach: (() -> Unit)?): Pair<Int, Int> {
        return try {
            beforeEach?.invoke()
            action()
            afterEach?.invoke()

            println("    ✅ $description")
            Pair(1, 0)
        } catch (e: AssertionError) {
            println("    ❌ $description: ${e.message}")
            Pair(0, 1)
        }
    }
}

fun testSuite(name: String, action: TestSuite.() -> Unit): TestSuite {
    return TestSuite(name).apply(action)
}

fun assertEquals(expected: Any?, actual: Any?) {
    if (expected != actual) {
        throw AssertionError("Expected $expected but got $actual")
    }
}

fun main() {
    val suite = testSuite("Calculator Tests") {
        beforeEach {
            println("      [Setup]")
        }

        afterEach {
            println("      [Teardown]")
        }

        describe("Addition") {
            it("should add positive numbers") {
                assertEquals(5, 2 + 3)
            }

            it("should add negative numbers") {
                assertEquals(-5, -2 + -3)
            }
        }

        describe("Multiplication") {
            it("should multiply numbers") {
                assertEquals(6, 2 * 3)
            }

            it("should fail example") {
                assertEquals(10, 2 * 3)  // This will fail
            }

            describe("Edge cases") {
                it("should handle zero") {
                    assertEquals(0, 0 * 100)
                }
            }
        }
    }

    suite.run()
}
```
