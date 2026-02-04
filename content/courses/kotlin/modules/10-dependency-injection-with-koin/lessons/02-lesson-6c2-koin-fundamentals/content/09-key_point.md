---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Koin modules define dependency graphs using `single` for singletons and `factory` for new instances**. Write `single { DatabaseRepository(get()) }` where `get()` resolves dependencies automatically.

**Koin is runtime-based with compile-time safety via Koin Annotations** (optional). It offers a middle ground between pure reflection DI (Dagger/Hilt) and manual factories.

**Start Koin with `startKoin { modules(appModule) }` at application startup**. Koin validates the module graph on start, catching missing dependencies before they cause runtime crashes.
