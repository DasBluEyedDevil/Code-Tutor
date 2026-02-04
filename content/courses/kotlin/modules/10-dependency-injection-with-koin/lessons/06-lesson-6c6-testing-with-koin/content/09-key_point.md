---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Replace production modules with test modules using `loadKoinModules` in test setup**. Define test doubles (fakes, mocks) for repositories and services, letting business logic tests stay fast and isolated.

**Koin's `koinInject()` in tests retrieves dependencies from the test container**. Combined with `declareMock<T>()` for inline mocking, this makes writing tests with proper isolation straightforward.

**Test modules mirror production structure**—if production has `dataModule` and `domainModule`, tests have `testDataModule` and `testDomainModule`. This ensures test coverage matches real dependency graphs.
