---
type: "THEORY"
title: "iOS Native Driver"
---

### NativeSqliteDriver

```kotlin
// iosMain/kotlin/Database.ios.kt
import app.cash.sqldelight.driver.native.NativeSqliteDriver

class IosDatabaseDriverFactory {
    fun createDriver(): SqlDriver {
        return NativeSqliteDriver(
            schema = AppDatabase.Schema,
            name = "app.db"
        )
    }
}
```

### Multi-Threaded Access

iOS requires special configuration for thread-safe access:

```kotlin
NativeSqliteDriver(
    schema = AppDatabase.Schema,
    name = "app.db",
    maxReaderConnections = 4  // Enable connection pool
)
```

### Database Location

By default, the database is stored in the app's Documents directory. For different locations:

```kotlin
// In-memory database
NativeSqliteDriver(
    schema = AppDatabase.Schema,
    name = null  // null = in-memory
)

// Custom path (requires platform-specific code)
import platform.Foundation.*

fun getDatabasePath(): String {
    val paths = NSSearchPathForDirectoriesInDomains(
        NSApplicationSupportDirectory,
        NSUserDomainMask,
        true
    )
    return (paths.first() as String) + "/app.db"
}
```