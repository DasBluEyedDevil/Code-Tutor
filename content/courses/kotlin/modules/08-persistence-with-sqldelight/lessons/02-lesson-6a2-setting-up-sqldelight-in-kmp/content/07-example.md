---
type: "EXAMPLE"
title: "Complete Setup Example"
---

Here's a complete working setup:

```kotlin
// ===== shared/build.gradle.kts =====
plugins {
    kotlin("multiplatform")
    id("com.android.library")
    id("app.cash.sqldelight")
}

kotlin {
    androidTarget()
    iosX64()
    iosArm64()
    iosSimulatorArm64()
    
    sourceSets {
        commonMain.dependencies {
            implementation("app.cash.sqldelight:coroutines-extensions:2.2.1")
        }
        androidMain.dependencies {
            implementation("app.cash.sqldelight:android-driver:2.2.1")
        }
        iosMain.dependencies {
            implementation("app.cash.sqldelight:native-driver:2.2.1")
        }
    }
}

sqldelight {
    databases {
        create("AppDatabase") {
            packageName.set("com.example.app.db")
            generateAsync.set(true)
        }
    }
}

// ===== commonMain/sqldelight/com/example/app/db/Note.sq =====
CREATE TABLE Note (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    content TEXT NOT NULL,
    created_at INTEGER NOT NULL
);

getAllNotes:
SELECT * FROM Note ORDER BY created_at DESC;

insertNote:
INSERT INTO Note(title, content, created_at)
VALUES (?, ?, ?);

// ===== After building, you can use: =====
val database = createDatabase(driverFactory)
val notes = database.noteQueries.getAllNotes().executeAsList()
```
