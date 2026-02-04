---
type: "KEY_POINT"
title: "Key Takeaways"
---

**SQLDelight Gradle plugin processes `.sq` files during compilation**, generating Kotlin database APIs before your code compiles. Schema files go in `src/commonMain/sqldelight/` organized by package.

**Platform-specific drivers are provided via expect/actual**—AndroidSqliteDriver for Android, NativeSqliteDriver for iOS. The driver is the only platform-specific part; all query logic stays in commonMain.

**Configure SQLDelight schema module and package name in `build.gradle.kts`** to control generated code location. Multiple databases are supported by defining multiple schema configurations.
