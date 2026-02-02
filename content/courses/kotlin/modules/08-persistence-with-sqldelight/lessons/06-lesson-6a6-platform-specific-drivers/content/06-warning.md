---
type: "WARNING"
title: "Platform-Specific Gotchas"
---

### Android

**Context requirement:**
```kotlin
// ❌ Can't create without Context
val factory = DatabaseDriverFactory() // Compile error

// ✅ Pass Context (usually from Application)
class MyApp : Application() {
    val database by lazy {
        createDatabase(DatabaseDriverFactory(this))
    }
}
```

### iOS

**Thread safety:**
```kotlin
// ❌ Single connection - crashes on concurrent access
NativeSqliteDriver(schema, "db")

// ✅ Enable connection pool
NativeSqliteDriver(schema, "db", maxReaderConnections = 4)
```

**Background access:**
iOS can suspend apps. Long transactions may be interrupted.

### Desktop

**Driver lifecycle:**
```kotlin
// ❌ Creating driver doesn't create schema!
val driver = JdbcSqliteDriver("jdbc:sqlite:app.db")

// ✅ Always create/migrate schema
val driver = JdbcSqliteDriver("jdbc:sqlite:app.db")
AppDatabase.Schema.create(driver) // For new database
```

**File path:**
```kotlin
// ❌ Relative path - unpredictable location
"jdbc:sqlite:app.db"

// ✅ Absolute path in user directory
"jdbc:sqlite:${System.getProperty("user.home")}/.myapp/app.db"
```