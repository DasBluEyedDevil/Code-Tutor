---
type: "THEORY"
title: "Testing Your Implementation"
---



---



```kotlin
import kotlinx.coroutines.test.*
import kotlin.test.*

class TaskFlowTests {
    @Test
    fun testSimpleTaskSuccess() = runTest {
        val task = task<Int> {
            name = "Test"
            action { 42 }
        }

        val result = task.execute()
        assertTrue(result is TaskResult.Success)
        assertEquals(42, result.getOrNull())
    }

    @Test
    fun testTaskRetry() = runTest {
        var attempts = 0
        val task = task<Int> {
            name = "RetryTest"
            retries = 2
            action {
                attempts++
                if (attempts < 3) throw Exception("Fail")
                42
            }
        }

        val executor = TaskExecutor()
        val result = executor.execute(task)

        assertEquals(3, attempts)
        assertTrue(result is TaskResult.Success)
    }

    @Test
    fun testWorkflow() = runTest {
        val workflow = workflow<Int> {
            name = "TestWorkflow"

            task("step1") { 10 }
            task("step2") { 20 }

            finalize { results ->
                (results[0] as Int) + (results[1] as Int)
            }
        }

        val result = workflow.execute()
        assertEquals(30, result.getOrNull())
    }
}
```
