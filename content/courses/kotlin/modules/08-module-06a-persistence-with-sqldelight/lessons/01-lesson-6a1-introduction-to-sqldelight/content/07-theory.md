---
type: "THEORY"
title: "Current Version and Ecosystem"
---

### SQLDelight 2.x (Current as of 2025)

SQLDelight 2.0 was released in 2023 with major improvements:

**New in 2.x:**
- Improved KMP support
- Better IDE integration
- Simplified driver setup
- Async/suspend function support
- Web target (SQLite WASM)

### Related Libraries

| Library | Purpose |
|---------|---------|
| `app.cash.sqldelight` | Core Gradle plugin |
| `native-driver` | iOS/macOS SQLite driver |
| `android-driver` | Android SQLite driver |
| `sqlite-driver` | JVM/Desktop SQLite driver |
| `web-worker-driver` | Web (WASM) SQLite driver |
| `coroutines-extensions` | Flow integration |

### What You'll Learn in This Module

1. **Setup**: Configure SQLDelight in a KMP project
2. **Schema**: Design tables and relationships
3. **Queries**: Write type-safe CRUD operations
4. **Migrations**: Handle schema changes safely
5. **Reactive**: Use Flow for automatic UI updates
6. **Platform**: Configure drivers for each platform
7. **Security**: Store sensitive data properly