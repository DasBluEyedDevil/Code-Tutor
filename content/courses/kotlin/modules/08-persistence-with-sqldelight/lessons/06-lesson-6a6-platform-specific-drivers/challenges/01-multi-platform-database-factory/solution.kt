// ===== commonMain =====
import app.cash.sqldelight.db.SqlDriver

expect class DatabaseDriverFactory {
    fun createDriver(): SqlDriver
}

// ===== androidMain =====
import android.content.Context
import app.cash.sqldelight.driver.android.AndroidSqliteDriver
import androidx.sqlite.db.SupportSQLiteDatabase

actual class DatabaseDriverFactory(private val context: Context) {
    actual fun createDriver(): SqlDriver {
        return AndroidSqliteDriver(
            schema = AppDatabase.Schema,
            context = context,
            name = "tasks.db",
            callback = object : AndroidSqliteDriver.Callback(AppDatabase.Schema) {
                override fun onOpen(db: SupportSQLiteDatabase) {
                    db.execSQL("PRAGMA foreign_keys=ON;")
                }
            }
        )
    }
}

// ===== iosMain =====
import app.cash.sqldelight.driver.native.NativeSqliteDriver

actual class DatabaseDriverFactory {
    actual fun createDriver(): SqlDriver {
        return NativeSqliteDriver(
            schema = AppDatabase.Schema,
            name = "tasks.db",
            maxReaderConnections = 4
        )
    }
}

// ===== desktopMain =====
import app.cash.sqldelight.driver.jdbc.sqlite.JdbcSqliteDriver
import java.io.File

actual class DatabaseDriverFactory {
    actual fun createDriver(): SqlDriver {
        val dbFile = File(getAppDataPath(), "tasks.db")
        val isNew = !dbFile.exists()
        
        val driver = JdbcSqliteDriver("jdbc:sqlite:${dbFile.absolutePath}")
        
        if (isNew) {
            AppDatabase.Schema.create(driver)
        }
        
        return driver
    }
    
    private fun getAppDataPath(): File {
        val home = System.getProperty("user.home")
        val os = System.getProperty("os.name").lowercase()
        
        val path = when {
            "win" in os -> File(home, "AppData/Local/TasksApp")
            "mac" in os -> File(home, "Library/Application Support/TasksApp")
            else -> File(home, ".tasksapp")
        }
        
        path.mkdirs()
        return path
    }
}