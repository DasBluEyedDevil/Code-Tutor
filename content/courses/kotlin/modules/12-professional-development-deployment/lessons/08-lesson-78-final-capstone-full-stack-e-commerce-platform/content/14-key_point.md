---
type: "KEY_POINT"
title: "Capstone Key Points: What Makes This a KMP Project"
---

### How TaskFlow Combines Every Course Module

This capstone is not a standalone exercise -- it is a demonstration of every skill you have built across the entire Kotlin course. Here is how each technology connects:

**Shared Code (the KMP advantage)**
- `@Serializable` data classes in `shared/` compile to JVM, Android, Desktop, and iOS
- The server and client deserialize the exact same `Task` class -- no mapping code needed
- This eliminates an entire class of bugs (mismatched field names, wrong types, forgotten fields)

**Server (Ktor + Exposed)**
- Extension functions on `Route` for clean route declarations (Module 06)
- Exposed DSL tables with type-safe column definitions (Module 06)
- Koin dependency injection wiring DAOs and config (Module 10)
- JWT authentication with bcrypt password hashing (Module 06, 12)

**Client (Compose Multiplatform + SQLDelight)**
- `@Composable` functions shared across Android and Desktop (Module 07)
- Material 3 components: `Scaffold`, `Card`, `FloatingActionButton` (Module 07)
- `StateFlow` collected as Compose state via `collectAsState()` (Module 05, 07)
- SQLDelight `.sq` files generate type-safe Kotlin from plain SQL (Module 08)
- `expect`/`actual` for platform-specific database drivers (Module 09)

**Architecture Patterns**
- Offline-first: Read local first, sync server in background (Module 08, 09)
- Repository pattern: Single data source for the UI layer (Module 09)
- ViewModel pattern: Business logic separated from UI (Module 07)
- Koin DI: Constructor injection everywhere, no service locator anti-pattern (Module 10)

### Alternative Capstone Ideas

If you want a different challenge after completing TaskFlow, consider:

- **Recipe Manager**: Categories, ingredients, step-by-step instructions with images
- **Expense Tracker**: Categories, charts, monthly summaries, CSV export
- **Note-Taking App**: Markdown support, tags, full-text search via SQLDelight FTS

All of these follow the same architecture: `shared/` + `server/` + `composeApp/`, Ktor + Exposed + H2, CMP + SQLDelight + Koin.

---

