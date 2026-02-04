---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Each platform requires a specific SQLite driver**: `AndroidSqliteDriver` for Android, `NativeSqliteDriver` for iOS/Native, `JdbcSqliteDriver` for JVM. Provide these via dependency injection or expect/actual declarations.

**Driver initialization includes schema creation** via `Database.Schema.create(driver)`. This happens once on first launch; subsequent launches open the existing database file.

**In-memory databases use special names** like `:memory:` or empty strings depending on the driver. Use in-memory databases for testing to ensure tests start with clean state and run fast.
