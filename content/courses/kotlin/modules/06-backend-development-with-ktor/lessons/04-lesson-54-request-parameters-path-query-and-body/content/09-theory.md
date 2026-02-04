---
type: "THEORY"
title: "🎯 Exercise: Build a Task Filter API"
---


Create a comprehensive task filtering API using all parameter types:

### Requirements

**1. GET /tasks/{status}** - Path parameter for status
- `status` can be: "pending", "completed", "archived"
- Example: `/tasks/pending`

**2. Add query parameters for additional filters:**
- `priority`: "low", "medium", "high"
- `assignedTo`: username
- `sort`: "date", "priority", "title"
- `order`: "asc", "desc"

**3. POST /tasks/search** - Advanced search with body
- Body should accept:
  ```json
  {
    "title": "search term",
    "tags": ["urgent", "bug"],
    "dueDateRange": {
      "start": "2024-01-01",
      "end": "2024-12-31"
    }
  }
  ```

### Starter Code


---



```kotlin
@Serializable
data class Task(
    val id: Int,
    val title: String,
    val status: String,
    val priority: String,
    val assignedTo: String?,
    val tags: List<String>,
    val dueDate: String?
)

object TaskStorage {
    private val tasks = mutableListOf(
        Task(1, "Fix bug", "pending", "high", "alice", listOf("bug", "urgent"), "2024-12-01"),
        Task(2, "Write docs", "completed", "medium", "bob", listOf("docs"), "2024-11-15"),
        Task(3, "Review PR", "pending", "medium", "alice", listOf("review"), "2024-11-20"),
        Task(4, "Deploy", "archived", "low", null, listOf("deploy"), null)
    )

    fun getAll() = tasks.toList()
    fun getByStatus(status: String) = tasks.filter { it.status == status }
}

// TODO: Implement the routes!
```
