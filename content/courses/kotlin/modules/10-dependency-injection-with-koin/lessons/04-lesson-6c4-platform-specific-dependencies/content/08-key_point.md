---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Platform-specific dependencies are resolved via expect/actual interfaces** and registered in platform modules. Common code depends on interfaces, platforms provide implementations—this is the KMP DI pattern.

**Qualifiers distinguish multiple implementations of the same type**—`single(named("prod")) { ProdDatabase() }` vs `single(named("test")) { TestDatabase() }`. Inject with `get(named("prod"))`.

**Context parameters (Activity, Context, UIViewController) are injected from platform modules**, never referenced directly in common code. Pass them during platform initialization: `startKoin { androidContext(application) }`.
