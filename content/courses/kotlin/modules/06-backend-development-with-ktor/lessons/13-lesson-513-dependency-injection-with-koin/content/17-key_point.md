---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Dependency injection separates object construction from usage**, enabling testability and flexible architecture. Koin provides compile-time safe DI for Kotlin Multiplatform without annotation processing or code generation.

**Define modules with `single` for singletons and `factory` for new instances**. Organize dependencies by layer (data, domain, presentation) to maintain clear separation of concerns and dependency directions.

**Inject dependencies via constructor parameters rather than property injection**. Constructor injection makes dependencies explicit, enables immutability, and fails fast at app startup if configuration is invalid.
