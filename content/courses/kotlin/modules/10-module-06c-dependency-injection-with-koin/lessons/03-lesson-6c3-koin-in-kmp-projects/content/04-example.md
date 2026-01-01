---
type: "EXAMPLE"
title: "Platform Modules"
---

Each platform provides its own implementations:

```kotlin
// ===== androidMain/kotlin/di/PlatformModule.android.kt =====
import android.content.Context
import app.cash.sqldelight.driver.android.AndroidSqliteDriver
import org.koin.dsl.module

val platformModule = module {
    // Android-specific database driver
    single {
        val context = get<Context>()
        AndroidSqliteDriver(
            schema = AppDatabase.Schema,
            context = context,
            name = "notes.db"
        )
    }
    
    // Database using Android driver
    single { AppDatabase(get()) }
    
    // Platform-specific utilities
    single<PlatformLogger> { AndroidLogger() }
    single<FileManager> { AndroidFileManager(get()) }
}

// ===== iosMain/kotlin/di/PlatformModule.ios.kt =====
import app.cash.sqldelight.driver.native.NativeSqliteDriver
import org.koin.dsl.module

val platformModule = module {
    // iOS-specific database driver
    single {
        NativeSqliteDriver(
            schema = AppDatabase.Schema,
            name = "notes.db"
        )
    }
    
    // Database using native driver
    single { AppDatabase(get()) }
    
    // Platform-specific utilities
    single<PlatformLogger> { IOSLogger() }
    single<FileManager> { IOSFileManager() }
}
```
