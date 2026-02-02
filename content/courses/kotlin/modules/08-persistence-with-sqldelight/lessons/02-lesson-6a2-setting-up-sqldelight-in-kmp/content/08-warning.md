---
type: "WARNING"
title: "Common Setup Issues"
---

### Issue 1: "Cannot find database"
```
> Task :shared:generateCommonMainAppDatabaseInterface FAILED
```
**Solution**: Ensure `.sq` files are in the correct path:
`src/commonMain/sqldelight/<package-path>/`

### Issue 2: Wrong package import
```kotlin
import com.example.app.db.AppDatabase // Can't find
```
**Solution**: Run Gradle sync and build to generate code. Check `packageName` in sqldelight config.

### Issue 3: iOS driver crashes
```
FATAL: Unable to open database
```
**Solution**: On iOS, ensure you're not accessing the database from a background thread without proper configuration:
```kotlin
NativeSqliteDriver(
    schema = AppDatabase.Schema,
    name = "app.db",
    maxReaderConnections = 4  // For thread-safe access
)
```

### Issue 4: Missing driver for platform
```
Unresolved reference: AndroidSqliteDriver
```
**Solution**: Add the correct driver dependency in your sourceSet dependencies.