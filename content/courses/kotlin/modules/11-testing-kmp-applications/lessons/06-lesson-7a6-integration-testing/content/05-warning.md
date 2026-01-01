---
type: "WARNING"
title: "Integration Test Best Practices"
---

### DO:

✅ **Use in-memory databases** for speed and isolation
```kotlin
JdbcSqliteDriver(JdbcSqliteDriver.IN_MEMORY)
```

✅ **Clean up after tests**
```kotlin
@AfterTest
fun teardown() {
    driver.close()
}
```

✅ **Test the happy path AND edge cases**
- Normal operation
- Empty data
- Error conditions
- Boundary conditions

✅ **Keep integration tests focused**
- Test one feature per test class
- Use descriptive test names

### DON'T:

❌ **Use production database**
- Tests are slow and flaky
- Data contamination between tests

❌ **Make real network calls**
- Tests become flaky (network issues)
- Tests are slow
- Can't test error scenarios

❌ **Share state between tests**
```kotlin
// ❌ Bad - shared state
companion object {
    val sharedDatabase = createDatabase()
}

// ✅ Good - fresh state per test
@BeforeTest
fun setup() {
    database = createInMemoryDatabase()
}
```