---
type: "THEORY"
title: "🛤️ Path Parameters: Identifying Resources"
---


### When to Use Path Parameters

Use path parameters for:
- ✅ Resource identifiers (IDs, usernames, slugs)
- ✅ Required hierarchical relationships
- ✅ Data that identifies **which** resource

**Examples:**

### Single Path Parameter


### Multiple Path Parameters


### Optional Path Parameters


The `?` makes the parameter optional:
- `/tasks` → Returns all tasks
- `/tasks/high` → Returns only high-priority tasks

---



```kotlin
get("/tasks/{priority?}") {
    val priority = call.parameters["priority"]

    val tasks = if (priority != null) {
        TaskStorage.getByPriority(priority)
    } else {
        TaskStorage.getAll()
    }

    call.respond(tasks)
}
```
