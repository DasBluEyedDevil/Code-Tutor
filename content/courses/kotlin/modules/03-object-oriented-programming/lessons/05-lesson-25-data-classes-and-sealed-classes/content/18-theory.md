---
type: "THEORY"
title: "Solution: Task Management"
---



---



```kotlin
sealed class TaskState {
    object Todo : TaskState() {
        override fun toString() = "TODO"
    }

    data class InProgress(val assignee: String, val startedAt: Long = System.currentTimeMillis()) : TaskState() {
        override fun toString() = "IN_PROGRESS (Assignee: $assignee)"
    }

    data class Completed(val completedBy: String, val completedAt: Long = System.currentTimeMillis()) : TaskState() {
        override fun toString() = "COMPLETED (By: $completedBy)"
    }

    data class Cancelled(val reason: String) : TaskState() {
        override fun toString() = "CANCELLED (Reason: $reason)"
    }
}

data class Task(
    val id: Int,
    val title: String,
    val description: String,
    val state: TaskState = TaskState.Todo,
    val history: List<TaskState> = listOf(TaskState.Todo)
) {
    fun startWork(assignee: String): Task {
        require(state is TaskState.Todo) { "Task must be in TODO state to start" }
        val newState = TaskState.InProgress(assignee)
        return copy(state = newState, history = history + newState)
    }

    fun complete(completedBy: String): Task {
        require(state is TaskState.InProgress) { "Task must be in progress to complete" }
        val newState = TaskState.Completed(completedBy)
        return copy(state = newState, history = history + newState)
    }

    fun cancel(reason: String): Task {
        require(state !is TaskState.Completed) { "Cannot cancel completed task" }
        val newState = TaskState.Cancelled(reason)
        return copy(state = newState, history = history + newState)
    }

    fun displayTask() {
        println("\n=== Task #$id ===")
        println("Title: $title")
        println("Description: $description")
        println("Current State: $state")
        println("\nState History:")
        history.forEachIndexed { index, state ->
            println("  ${index + 1}. $state")
        }
        println("================\n")
    }

    fun getStatusEmoji(): String = when (state) {
        is TaskState.Todo -> "📝"
        is TaskState.InProgress -> "🔄"
        is TaskState.Completed -> "✅"
        is TaskState.Cancelled -> "❌"
    }
}

class TaskManager {
    private val tasks = mutableMapOf<Int, Task>()
    private var nextId = 1

    fun createTask(title: String, description: String): Task {
        val task = Task(nextId++, title, description)
        tasks[task.id] = task
        println("Created task: ${task.getStatusEmoji()} ${task.title}")
        return task
    }

    fun updateTask(task: Task) {
        tasks[task.id] = task
        println("Updated task: ${task.getStatusEmoji()} ${task.title} -> ${task.state}")
    }

    fun listTasks() {
        println("\n=== All Tasks ===")
        tasks.values.forEach { task ->
            println("${task.getStatusEmoji()} #${task.id}: ${task.title} [${task.state}]")
        }
        println("=================\n")
    }
}

fun main() {
    val manager = TaskManager()

    // Create tasks
    var task1 = manager.createTask("Implement login", "Add JWT authentication")
    var task2 = manager.createTask("Fix bug #123", "Null pointer exception in profile")
    var task3 = manager.createTask("Write tests", "Unit tests for payment module")

    manager.listTasks()

    // Start working on tasks
    task1 = task1.startWork("Alice")
    manager.updateTask(task1)

    task2 = task2.startWork("Bob")
    manager.updateTask(task2)

    manager.listTasks()

    // Complete a task
    task1 = task1.complete("Alice")
    manager.updateTask(task1)

    // Cancel a task
    task3 = task3.cancel("Requirements changed")
    manager.updateTask(task3)

    manager.listTasks()

    // Display full history
    task1.displayTask()
}
```
