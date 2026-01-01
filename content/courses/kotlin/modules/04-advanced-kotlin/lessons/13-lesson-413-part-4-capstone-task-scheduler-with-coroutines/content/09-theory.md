---
type: "THEORY"
title: "Phase 5: DSL Configuration (60 minutes)"
---


### Task DSL


### Workflow DSL


---



```kotlin
@TaskFlowDsl
class WorkflowBuilder<T> {
    var name: String = ""
    var description: String = ""

    private val tasks = mutableListOf<Task<*>>()
    private var finalTask: (suspend (List<Any?>) -> T)? = null

    fun <R> task(name: String, action: suspend () -> R) {
        val task = task<R> {
            this.name = name
            action(action)
        }
        tasks.add(task)
    }

    fun finalize(action: suspend (List<Any?>) -> T) {
        finalTask = action
    }

    fun build(): WorkflowTask<T> {
        val metadata = TaskMetadata(name, description)
        return WorkflowTask(metadata, tasks, finalTask!!)
    }
}

class WorkflowTask<T>(
    override val metadata: TaskMetadata,
    private val tasks: List<Task<*>>,
    private val finalizer: suspend (List<Any?>) -> T
) : Task<T> {
    private val _status = MutableStateFlow(TaskStatus.PENDING)
    override val status: StateFlow<TaskStatus> = _status

    override suspend fun execute(): TaskResult<T> {
        _status.value = TaskStatus.RUNNING

        return try {
            val results = tasks.map { task ->
                when (val result = task.execute()) {
                    is TaskResult.Success -> result.value
                    is TaskResult.Failure -> throw result.error
                    is TaskResult.Cancelled -> throw CancellationException("Subtask cancelled")
                }
            }

            val finalResult = finalizer(results)
            _status.value = TaskStatus.COMPLETED
            TaskResult.Success(finalResult)
        } catch (e: CancellationException) {
            _status.value = TaskStatus.CANCELLED
            TaskResult.Cancelled
        } catch (e: Exception) {
            _status.value = TaskStatus.FAILED
            TaskResult.Failure(e)
        }
    }

    override fun cancel() {
        tasks.forEach { it.cancel() }
        _status.value = TaskStatus.CANCELLED
    }
}

fun <T> workflow(block: WorkflowBuilder<T>.() -> Unit): WorkflowTask<T> {
    return WorkflowBuilder<T>().apply(block).build()
}
```
