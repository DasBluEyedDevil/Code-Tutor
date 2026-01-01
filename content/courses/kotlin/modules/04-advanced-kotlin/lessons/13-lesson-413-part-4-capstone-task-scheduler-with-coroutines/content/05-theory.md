---
type: "THEORY"
title: "Phase 1: Core Task System (60 minutes)"
---


Let's start by building the core task system with generics.

### Task Result Types


### Task Metadata


### Base Task Interface


### Simple Task Implementation


Wait, let me fix this implementation:


---



```kotlin
abstract class SimpleTask<T>(override val metadata: TaskMetadata) : Task<T> {
    private val _status = MutableStateFlow(TaskStatus.PENDING)
    override val status: StateFlow<TaskStatus> = _status

    private var job: Job? = null

    protected abstract suspend fun run(): T

    override suspend fun execute(): TaskResult<T> {
        _status.value = TaskStatus.RUNNING

        return try {
            val result = if (metadata.timeout > 0) {
                withTimeout(metadata.timeout) { run() }
            } else {
                run()
            }

            _status.value = TaskStatus.COMPLETED
            TaskResult.Success(result)
        } catch (e: CancellationException) {
            _status.value = TaskStatus.CANCELLED
            TaskResult.Cancelled
        } catch (e: Exception) {
            _status.value = TaskStatus.FAILED
            TaskResult.Failure(e)
        }
    }

    override fun cancel() {
        job?.cancel()
        _status.value = TaskStatus.CANCELLED
    }
}
```
