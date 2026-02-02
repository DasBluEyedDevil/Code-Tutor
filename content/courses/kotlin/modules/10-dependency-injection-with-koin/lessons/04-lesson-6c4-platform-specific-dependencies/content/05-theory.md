---
type: "THEORY"
title: "Pattern 3: Factory Providers"
---

For dependencies that need runtime configuration:

```kotlin
// commonMain
interface DatabaseDriverFactory {
    fun createDriver(): SqlDriver
}

// androidMain
class AndroidDatabaseDriverFactory(
    private val context: Context
) : DatabaseDriverFactory {
    override fun createDriver(): SqlDriver {
        return AndroidSqliteDriver(
            schema = AppDatabase.Schema,
            context = context,
            name = "notes.db"
        )
    }
}

// iosMain
class IOSDatabaseDriverFactory : DatabaseDriverFactory {
    override fun createDriver(): SqlDriver {
        return NativeSqliteDriver(
            schema = AppDatabase.Schema,
            name = "notes.db"
        )
    }
}
```

### In Modules

```kotlin
// commonMain
val commonModule = module {
    // Factory creates driver, database uses it
    single { get<DatabaseDriverFactory>().createDriver() }
    single { AppDatabase(get()) }
}

// androidMain
val platformModule = module {
    single<DatabaseDriverFactory> { AndroidDatabaseDriverFactory(get()) }
}

// iosMain  
val platformModule = module {
    single<DatabaseDriverFactory> { IOSDatabaseDriverFactory() }
}
```