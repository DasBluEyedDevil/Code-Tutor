---
type: "THEORY"
title: "Phase 2: Task Executor with Coroutines (60 minutes)"
---


### Task Executor


---



```kotlin
import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

class TaskExecutor(
    private val dispatcher: CoroutineDispatcher = Dispatchers.Default,
    private val maxConcurrentTasks: Int = 4
) {
    private val scope = CoroutineScope(dispatcher + SupervisorJob())
    private val _events = MutableSharedFlow<TaskEvent>()
    val events: SharedFlow<TaskEvent> = _events

    private val activeTasks = MutableStateFlow(0)

    suspend fun <T> execute(task: Task<T>): TaskResult<T> {
        return withContext(dispatcher) {
            // Wait if max concurrent tasks reached
            while (activeTasks.value >= maxConcurrentTasks) {
                delay(100)
            }

            activeTasks.value++
            _events.emit(TaskEvent.Started(task.metadata.name))

            try {
                val result = executeWithRetry(task)

                when (result) {
                    is TaskResult.Success -> _events.emit(TaskEvent.Completed(task.metadata.name))
                    is TaskResult.Failure -> _events.emit(TaskEvent.Failed(task.metadata.name, result.error))
                    is TaskResult.Cancelled -> _events.emit(TaskEvent.Cancelled(task.metadata.name))
                }

                result
            } finally {
                activeTasks.value--
            }
        }
    }

    private suspend fun <T> executeWithRetry(task: Task<T>): TaskResult<T> {
        var lastError: Throwable? = null
        var attempt = 0
        val maxAttempts = task.metadata.retries + 1

        while (attempt < maxAttempts) {
            val result = task.execute()

            when (result) {
                is TaskResult.Success -> return result
                is TaskResult.Cancelled -> return result
                is TaskResult.Failure -> {
                    lastError = result.error
                    attempt++

                    if (attempt < maxAttempts) {
                        val delayMs = (100 * (1 shl attempt)).toLong()
                        _events.emit(TaskEvent.Retrying(task.metadata.name, attempt, delayMs))
                        delay(delayMs)
                    }
                }
            }
        }

        return TaskResult.Failure(lastError ?: Exception("Unknown error"))
    }

    fun shutdown() {
        scope.cancel()
    }
}

sealed class TaskEvent {
    data class Started(val taskName: String) : TaskEvent()
    data class Completed(val taskName: String) : TaskEvent()
    data class Failed(val taskName: String, val error: Throwable) : TaskEvent()
    data class Cancelled(val taskName: String) : TaskEvent()
    data class Retrying(val taskName: String, val attempt: Int, val delayMs: Long) : TaskEvent()
}
```
