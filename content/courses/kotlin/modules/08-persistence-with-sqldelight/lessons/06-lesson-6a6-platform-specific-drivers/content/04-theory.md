---
type: "THEORY"
title: "Desktop (JVM) Driver"
---

### SqliteDriver for JVM

```kotlin
// jvmMain/kotlin/Database.jvm.kt (or desktopMain)
import app.cash.sqldelight.driver.jdbc.sqlite.JdbcSqliteDriver

class JvmDatabaseDriverFactory {
    fun createDriver(): SqlDriver {
        // File-based database
        val driver = JdbcSqliteDriver("jdbc:sqlite:app.db")
        AppDatabase.Schema.create(driver)
        return driver
    }
}

// In-memory database (for testing)
class TestDatabaseDriverFactory {
    fun createDriver(): SqlDriver {
        val driver = JdbcSqliteDriver(JdbcSqliteDriver.IN_MEMORY)
        AppDatabase.Schema.create(driver)
        return driver
    }
}
```

### User Data Directory

```kotlin
import java.io.File

fun getDatabasePath(): String {
    val userHome = System.getProperty("user.home")
    val appDir = File(userHome, ".myapp")
    appDir.mkdirs()
    return "jdbc:sqlite:${appDir.absolutePath}/app.db"
}
```