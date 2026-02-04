---
type: "KEY_POINT"
title: "Key Takeaways"
---

**SQLDelight provides type-safe SQL for Kotlin Multiplatform** by generating Kotlin APIs from SQL schema files. Write SQL, get compile-time verified Kotlin functions—no room for typos or type mismatches.

**Define your schema in `.sq` files** with CREATE TABLE and custom queries. SQLDelight generates suspend functions for async operations and Flow<T> for reactive queries that update when data changes.

**Platform-specific drivers abstract database engines**—AndroidSqliteDriver for Android, NativeSqliteDriver for iOS. Your business logic stays in commonMain while platform code provides the driver implementation.
