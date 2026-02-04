---
type: "THEORY"
title: "Client-Side Cache: SQLDelight"
---

The client uses SQLDelight for offline-first local storage. You write plain SQL in `.sq` files and SQLDelight generates type-safe Kotlin code. Platform-specific drivers are provided via `expect`/`actual`.

### SQL Schema (.sq file)

```sql
-- composeApp/src/commonMain/sqldelight/com/taskflow/app/db/TaskFlowDatabase.sq

CREATE TABLE IF NOT EXISTS TaskEntity (
    id TEXT NOT NULL PRIMARY KEY,
    title TEXT NOT NULL,
    description TEXT NOT NULL DEFAULT '',
    priority TEXT NOT NULL DEFAULT 'MEDIUM',
    status TEXT NOT NULL DEFAULT 'TODO',
    category TEXT NOT NULL DEFAULT '',
    dueDate TEXT,
    userId TEXT NOT NULL DEFAULT '',
    createdAt TEXT NOT NULL DEFAULT '',
    updatedAt TEXT NOT NULL DEFAULT '',
    synced INTEGER NOT NULL DEFAULT 0
);

-- Queries

selectAll:
SELECT * FROM TaskEntity
ORDER BY
    CASE priority
        WHEN 'URGENT' THEN 0
        WHEN 'HIGH' THEN 1
        WHEN 'MEDIUM' THEN 2
        WHEN 'LOW' THEN 3
    END,
    createdAt DESC;

selectById:
SELECT * FROM TaskEntity WHERE id = ?;

selectUnsynced:
SELECT * FROM TaskEntity WHERE synced = 0;

insert:
INSERT OR REPLACE INTO TaskEntity(id, title, description, priority, status, category, dueDate, userId, createdAt, updatedAt, synced)
VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);

markSynced:
UPDATE TaskEntity SET synced = 1 WHERE id = ?;

deleteById:
DELETE FROM TaskEntity WHERE id = ?;

deleteAll:
DELETE FROM TaskEntity;
```

The `synced` column tracks whether a local change has been pushed to the server. This enables the offline-first pattern.

### Platform Drivers (expect/actual)

```kotlin
// composeApp/src/commonMain/kotlin/com/taskflow/app/data/local/DriverFactory.kt
package com.taskflow.app.data.local

import app.cash.sqldelight.db.SqlDriver

expect class DriverFactory {
    fun createDriver(): SqlDriver
}
```

```kotlin
// composeApp/src/desktopMain/kotlin/com/taskflow/app/DatabaseDriverFactory.kt
package com.taskflow.app.data.local

import app.cash.sqldelight.db.SqlDriver
import app.cash.sqldelight.driver.jdbc.sqlite.JdbcSqliteDriver
import com.taskflow.app.db.TaskFlowDatabase

actual class DriverFactory {
    actual fun createDriver(): SqlDriver {
        val driver = JdbcSqliteDriver(JdbcSqliteDriver.IN_MEMORY)
        TaskFlowDatabase.Schema.create(driver)
        return driver
    }
}
```

```kotlin
// composeApp/src/androidMain/kotlin/com/taskflow/app/DatabaseDriverFactory.kt
package com.taskflow.app.data.local

import android.content.Context
import app.cash.sqldelight.db.SqlDriver
import app.cash.sqldelight.driver.android.AndroidSqliteDriver
import com.taskflow.app.db.TaskFlowDatabase

actual class DriverFactory(private val context: Context) {
    actual fun createDriver(): SqlDriver {
        return AndroidSqliteDriver(TaskFlowDatabase.Schema, context, "taskflow.db")
    }
}
```

### SyncManager

The SyncManager coordinates between the local cache and the remote server.

```kotlin
// composeApp/src/commonMain/kotlin/com/taskflow/app/data/repository/SyncManager.kt
package com.taskflow.app.data.repository

import com.taskflow.app.data.local.DriverFactory
import com.taskflow.app.data.remote.TaskFlowApi
import com.taskflow.app.db.TaskFlowDatabase
import com.taskflow.shared.model.Task
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class SyncManager(
    driverFactory: DriverFactory,
    private val api: TaskFlowApi,
    private val scope: CoroutineScope = CoroutineScope(Dispatchers.Default)
) {
    private val database = TaskFlowDatabase(driverFactory.createDriver())
    private val queries = database.taskFlowDatabaseQueries

    fun getAllTasks(): List<Task> {
        return queries.selectAll().executeAsList().map { entity ->
            Task(
                id = entity.id,
                title = entity.title,
                description = entity.description,
                priority = enumValueOf(entity.priority),
                status = enumValueOf(entity.status),
                category = entity.category,
                dueDate = entity.dueDate,
                userId = entity.userId,
                createdAt = entity.createdAt,
                updatedAt = entity.updatedAt
            )
        }
    }

    fun cacheTask(task: Task, synced: Boolean = true) {
        queries.insert(
            id = task.id,
            title = task.title,
            description = task.description,
            priority = task.priority.name,
            status = task.status.name,
            category = task.category,
            dueDate = task.dueDate,
            userId = task.userId,
            createdAt = task.createdAt,
            updatedAt = task.updatedAt,
            synced = if (synced) 1L else 0L
        )
    }

    fun syncWithServer() {
        scope.launch {
            // Push unsynced local changes
            val unsynced = queries.selectUnsynced().executeAsList()
            for (entity in unsynced) {
                try {
                    // Push to server (implementation depends on change type)
                    queries.markSynced(entity.id)
                } catch (_: Exception) {
                    // Network error -- retry on next sync
                }
            }

            // Pull latest from server
            try {
                val remoteTasks = api.getAllTasks()
                for (task in remoteTasks) {
                    cacheTask(task, synced = true)
                }
            } catch (_: Exception) {
                // Offline -- use cached data
            }
        }
    }
}
```

---

