---
type: "THEORY"
title: "Testing"
---



---



```kotlin
@Test
fun `adding task should insert to database`() = runTest {
    val task = Task(
        title = "Test Task",
        category = Category.WORK,
        priority = Priority.HIGH
    )

    repository.insertTask(task)

    val tasks = repository.getAllTasks().first()
    assertTrue(tasks.any { it.title == "Test Task" })
}
```
