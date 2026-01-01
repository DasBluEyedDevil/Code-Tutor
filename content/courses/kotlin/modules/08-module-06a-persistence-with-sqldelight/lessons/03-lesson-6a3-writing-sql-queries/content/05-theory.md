---
type: "THEORY"
title: "Type Adapters for Custom Types"
---

SQLite only supports basic types. For custom Kotlin types, use adapters:

### Common Scenarios

```kotlin
// Enum to string
enum class Priority { LOW, MEDIUM, HIGH }

// In your schema:
// priority TEXT NOT NULL -- stored as "LOW", "MEDIUM", or "HIGH"

// Adapter
val priorityAdapter = object : ColumnAdapter<Priority, String> {
    override fun decode(databaseValue: String): Priority = 
        Priority.valueOf(databaseValue)
    override fun encode(value: Priority): String = 
        value.name
}
```

### Registering Adapters

```kotlin
fun createDatabase(driver: SqlDriver): AppDatabase {
    return AppDatabase(
        driver = driver,
        NoteAdapter = Note.Adapter(
            priorityAdapter = priorityAdapter
        )
    )
}
```

### Date/Time with kotlinx-datetime

```kotlin
import kotlinx.datetime.Instant

val instantAdapter = object : ColumnAdapter<Instant, Long> {
    override fun decode(databaseValue: Long): Instant = 
        Instant.fromEpochMilliseconds(databaseValue)
    override fun encode(value: Instant): Long = 
        value.toEpochMilliseconds()
}
```