---
type: "KEY_POINT"
title: "Key Takeaways"
---

**SQLDelight generates type-safe Kotlin APIs from SQL**, eliminating runtime query errors and manual mapping boilerplate. Write SQL in `.sq` files, get compile-time verified Kotlin functions.

**Unlike ORMs, SQLDelight doesn't hide SQL**—you write explicit SQL queries with full control over performance. This transparency prevents hidden N+1 queries and unnecessary joins that plague ORM-based apps.

**SQLDelight is truly multiplatform**, using SQLite on all targets (Android, iOS, JVM, Native) with platform-specific drivers but shared query logic. Your database code lives in commonMain.
