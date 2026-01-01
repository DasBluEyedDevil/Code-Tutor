---
type: "THEORY"
title: "Repository with Room"
---



---



```kotlin
class TaskRepository(private val taskDao: TaskDao) {
    val allTasks: Flow<List<Task>> = taskDao.getAllTasks()

    fun getTask(taskId: Int): Flow<Task?> = taskDao.getTask(taskId)

    fun getActiveTasks(): Flow<List<Task>> = taskDao.getTasksByStatus(false)

    fun getCompletedTasks(): Flow<List<Task>> = taskDao.getTasksByStatus(true)

    suspend fun insertTask(task: Task): Long {
        return taskDao.insertTask(task)
    }

    suspend fun updateTask(task: Task) {
        taskDao.updateTask(task)
    }

    suspend fun deleteTask(task: Task) {
        taskDao.deleteTask(task)
    }

    suspend fun toggleTaskStatus(taskId: Int, isCompleted: Boolean) {
        taskDao.updateTaskStatus(taskId, isCompleted)
    }
}
```
