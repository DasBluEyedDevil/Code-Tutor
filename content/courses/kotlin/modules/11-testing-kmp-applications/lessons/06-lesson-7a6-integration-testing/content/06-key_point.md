---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Integration tests verify multiple components working together**—repository + database, ViewModel + use cases + repository. They catch integration bugs that unit tests miss.

**Use in-memory databases for integration tests** to keep tests fast and isolated. SQLDelight's in-memory driver creates fresh databases per test, ensuring test independence.

**Integration tests belong in platform test source sets** (androidTest, iosTest) when they require platform-specific infrastructure. Keep pure Kotlin integration tests in commonTest when possible.
