---
type: "THEORY"
title: "Phase 4: Annotations and Reflection (45 minutes)"
---


### Task Annotations


### Task Registry with Reflection


---



```kotlin
import kotlin.reflect.KClass
import kotlin.reflect.full.*

object TaskRegistry {
    private val tasks = mutableMapOf<String, KClass<out Task<*>>>()

    fun register(taskClass: KClass<out Task<*>>) {
        val annotation = taskClass.annotations.filterIsInstance<RegisteredTask>().firstOrNull()
            ?: throw IllegalArgumentException("Task must be annotated with @RegisteredTask")

        tasks[annotation.name] = taskClass
    }

    fun <T> create(name: String): Task<T>? {
        val taskClass = tasks[name] ?: return null

        // Find primary constructor
        val constructor = taskClass.constructors.firstOrNull() ?: return null

        // Create metadata from annotation
        val annotation = taskClass.annotations.filterIsInstance<RegisteredTask>().first()
        val metadata = TaskMetadata(
            name = annotation.name,
            priority = annotation.priority,
            retries = annotation.retries
        )

        // Call constructor with metadata
        val instance = if (constructor.parameters.isEmpty()) {
            constructor.call()
        } else {
            constructor.call(metadata)
        }

        @Suppress("UNCHECKED_CAST")
        return instance as? Task<T>
    }

    fun listTasks(): List<String> = tasks.keys.toList()

    fun getTaskInfo(name: String): TaskMetadata? {
        val taskClass = tasks[name] ?: return null
        val annotation = taskClass.annotations.filterIsInstance<RegisteredTask>().first()

        return TaskMetadata(
            name = annotation.name,
            priority = annotation.priority,
            retries = annotation.retries
        )
    }
}
```
