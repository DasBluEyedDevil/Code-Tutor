---
type: "THEORY"
title: "Android Driver"
---

### AndroidSqliteDriver

```kotlin
// androidMain/kotlin/Database.android.kt
import android.content.Context
import app.cash.sqldelight.driver.android.AndroidSqliteDriver

class AndroidDatabaseDriverFactory(private val context: Context) {
    fun createDriver(): SqlDriver {
        return AndroidSqliteDriver(
            schema = AppDatabase.Schema,
            context = context,
            name = "app.db"
        )
    }
}
```

### Configuration Options

```kotlin
AndroidSqliteDriver(
    schema = AppDatabase.Schema,
    context = context,
    name = "app.db",           // null for in-memory
    factory = SupportSQLiteOpenHelper.Factory(), // Custom factory
    callback = object : AndroidSqliteDriver.Callback(AppDatabase.Schema) {
        override fun onOpen(db: SupportSQLiteDatabase) {
            // Enable foreign keys
            db.execSQL("PRAGMA foreign_keys=ON;")
        }
    }
)
```

### WAL Mode (Performance)

```kotlin
callback = object : AndroidSqliteDriver.Callback(AppDatabase.Schema) {
    override fun onConfigure(db: SupportSQLiteDatabase) {
        // Write-Ahead Logging for better performance
        db.enableWriteAheadLogging()
    }
}
```