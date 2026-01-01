---
type: "THEORY"
title: "Complete Solution: TaskFlow System"
---


Here's the complete integrated solution:


---



```kotlin
import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*
import kotlin.reflect.KClass
import kotlin.reflect.full.*

// ========== Core Types ==========

sealed class TaskResult<out T> {
    data class Success<T>(val value: T) : TaskResult<T>()
    data class Failure(val error: Throwable) : TaskResult<Nothing>()
    object Cancelled : TaskResult<Nothing>()

    fun <R> map(transform: (T) -> R): TaskResult<R> = when (this) {
        is Success -> Success(transform(value))
        is Failure -> this
        is Cancelled -> this
    }

    fun getOrNull(): T? = (this as? Success)?.value
}

data class TaskMetadata(
    val name: String,
    val description: String = "",
    val priority: TaskPriority = TaskPriority.NORMAL,
    val retries: Int = 0,
    val timeout: Long = 0
)

enum class TaskPriority { LOW, NORMAL, HIGH, CRITICAL }
enum class TaskStatus { PENDING, RUNNING, COMPLETED, FAILED, CANCELLED }

// ========== Task Interface ==========

interface Task<T> {
    val metadata: TaskMetadata
    val status: StateFlow<TaskStatus>
    suspend fun execute(): TaskResult<T>
    fun cancel()
}

// ========== Example Tasks ==========

@RegisteredTask(name = "DataFetch", priority = TaskPriority.HIGH, retries = 3)
class DataFetchTask(override val metadata: TaskMetadata) : SimpleTask<String>(metadata) {
    override suspend fun run(): String {
        delay(1000)
        return "Fetched data at ${System.currentTimeMillis()}"
    }
}

@RegisteredTask(name = "DataProcess", priority = TaskPriority.NORMAL, retries = 2)
class DataProcessTask(override val metadata: TaskMetadata) : SimpleTask<String>(metadata) {
    override suspend fun run(): String {
        delay(500)
        return "Processed data"
    }
}

// ========== Main Demo ==========

fun main() = runBlocking {
    println("=== TaskFlow Demo ===\n")

    // 1. Simple Task with DSL
    println("1. Creating task with DSL:")
    val simpleTask = task<String> {
        name = "GreetingTask"
        description = "Generates a greeting"
        timeout = 5000

        action {
            delay(500)
            "Hello from TaskFlow!"
        }
    }

    val result1 = simpleTask.execute()
    println("Result: ${result1.getOrNull()}\n")

    // 2. Workflow Task
    println("2. Creating workflow:")
    val workflowTask = workflow<String> {
        name = "DataPipeline"
        description = "Fetch and process data"

        task("fetch") {
            delay(1000)
            "Raw Data"
        }

        task("transform") {
            delay(500)
            "Transformed"
        }

        finalize { results ->
            "Pipeline completed: $results"
        }
    }

    val result2 = workflowTask.execute()
    println("Workflow result: ${result2.getOrNull()}\n")

    // 3. Task Executor with monitoring
    println("3. Task Executor with monitoring:")
    val executor = TaskExecutor(maxConcurrentTasks = 2)

    launch {
        executor.events.collect { event ->
            when (event) {
                is TaskEvent.Started -> println("  ▶ Started: ${event.taskName}")
                is TaskEvent.Completed -> println("  ✅ Completed: ${event.taskName}")
                is TaskEvent.Failed -> println("  ❌ Failed: ${event.taskName}")
                is TaskEvent.Retrying -> println("  🔄 Retrying: ${event.taskName} (attempt ${event.attempt})")
                is TaskEvent.Cancelled -> println("  ⛔ Cancelled: ${event.taskName}")
            }
        }
    }

    val tasks = (1..5).map { i ->
        task<Int> {
            name = "Task-$i"
            retries = 2
            action {
                delay((500..1500).random().toLong())
                if (i == 3) throw Exception("Simulated failure")
                i * 10
            }
        }
    }

    val results = tasks.map { async { executor.execute(it) } }.awaitAll()

    println("\nResults:")
    results.forEach { result ->
        println("  ${result.getOrNull() ?: "Failed"}")
    }

    // 4. Task Registry with Reflection
    println("\n4. Task Registry:")
    TaskRegistry.register(DataFetchTask::class)
    TaskRegistry.register(DataProcessTask::class)

    println("Registered tasks: ${TaskRegistry.listTasks()}")

    val fetchTask = TaskRegistry.create<String>("DataFetch")
    if (fetchTask != null) {
        val result = executor.execute(fetchTask)
        println("Registry task result: ${result.getOrNull()}")
    }

    delay(1000)
    executor.shutdown()

    println("\n=== Demo Complete ===")
}
```
