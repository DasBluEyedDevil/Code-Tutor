---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Koin works identically in commonMain and platform code**—define shared modules for business logic, platform modules for platform-specific implementations. The same `get()` syntax works everywhere.

**Use expect/actual for platform-specific factories**: define `expect fun platformModule(): Module` in commonMain, implement in each platform with platform-specific classes (AndroidSqliteDriver, NativeSqliteDriver).

**ViewModels can be injected via Koin in commonMain**, making them accessible from Compose (Android) and SwiftUI wrappers (iOS). Koin's lifecycle awareness ensures proper cleanup.
