---
type: "THEORY"
title: "Creating the Database Factory"
---

### Step 4: Platform-Specific Database Creation

Use expect/actual to create the database on each platform:

**In commonMain:**
```kotlin
// Database.kt
expect class DatabaseDriverFactory {
    fun createDriver(): SqlDriver
}

fun createDatabase(driverFactory: DatabaseDriverFactory): AppDatabase {
    return AppDatabase(driverFactory.createDriver())
}
```

**In androidMain:**
```kotlin
// Database.android.kt
import android.content.Context
import app.cash.sqldelight.driver.android.AndroidSqliteDriver

actual class DatabaseDriverFactory(private val context: Context) {
    actual fun createDriver(): SqlDriver {
        return AndroidSqliteDriver(
            schema = AppDatabase.Schema,
            context = context,
            name = "app.db"
        )
    }
}
```

**In iosMain:**
```kotlin
// Database.ios.kt
import app.cash.sqldelight.driver.native.NativeSqliteDriver

actual class DatabaseDriverFactory {
    actual fun createDriver(): SqlDriver {
        return NativeSqliteDriver(
            schema = AppDatabase.Schema,
            name = "app.db"
        )
    }
}
```