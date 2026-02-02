---
type: "EXAMPLE"
title: "Complete Multi-Platform Setup"
---

A complete expect/actual implementation:

```kotlin
// ===== commonMain/kotlin/DatabaseFactory.kt =====
import app.cash.sqldelight.db.SqlDriver

expect class DatabaseDriverFactory {
    fun createDriver(): SqlDriver
}

fun createDatabase(driverFactory: DatabaseDriverFactory): AppDatabase {
    val driver = driverFactory.createDriver()
    return AppDatabase(driver)
}

// ===== androidMain/kotlin/DatabaseFactory.android.kt =====
import android.content.Context
import app.cash.sqldelight.driver.android.AndroidSqliteDriver

actual class DatabaseDriverFactory(private val context: Context) {
    actual fun createDriver(): SqlDriver {
        return AndroidSqliteDriver(
            schema = AppDatabase.Schema,
            context = context,
            name = "notes.db"
        ).also { driver ->
            driver.execute(null, "PRAGMA foreign_keys=ON;", 0)
        }
    }
}

// ===== iosMain/kotlin/DatabaseFactory.ios.kt =====
import app.cash.sqldelight.driver.native.NativeSqliteDriver

actual class DatabaseDriverFactory {
    actual fun createDriver(): SqlDriver {
        return NativeSqliteDriver(
            schema = AppDatabase.Schema,
            name = "notes.db",
            maxReaderConnections = 4
        )
    }
}

// ===== desktopMain/kotlin/DatabaseFactory.desktop.kt =====
import app.cash.sqldelight.driver.jdbc.sqlite.JdbcSqliteDriver
import java.io.File

actual class DatabaseDriverFactory {
    actual fun createDriver(): SqlDriver {
        val dbPath = getAppDataPath() + "/notes.db"
        val driver = JdbcSqliteDriver("jdbc:sqlite:$dbPath")
        
        // Create schema if database is new
        if (!File(dbPath).exists()) {
            AppDatabase.Schema.create(driver)
        } else {
            // Run migrations if needed
            val currentVersion = driver.getCurrentVersion()
            if (currentVersion < AppDatabase.Schema.version) {
                AppDatabase.Schema.migrate(
                    driver, currentVersion, AppDatabase.Schema.version
                )
            }
        }
        
        return driver
    }
    
    private fun getAppDataPath(): String {
        val os = System.getProperty("os.name").lowercase()
        val home = System.getProperty("user.home")
        return when {
            os.contains("win") -> "$home\\AppData\\Local\\NotesApp"
            os.contains("mac") -> "$home/Library/Application Support/NotesApp"
            else -> "$home/.notesapp"
        }.also { File(it).mkdirs() }
    }
}
```
