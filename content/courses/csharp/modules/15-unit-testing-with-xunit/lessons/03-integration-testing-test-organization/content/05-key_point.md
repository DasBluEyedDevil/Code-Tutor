---
type: "KEY_POINT"
title: "Integration Testing and Organization"
---

## Key Takeaways

- **`UseInMemoryDatabase` isolates tests** -- use a unique database name (Guid) per test to prevent state leaking between tests. In-memory databases are fast and require no external infrastructure.

- **`IClassFixture<T>` shares expensive setup** -- create a database or HTTP client once, share across all tests in the class. The fixture is disposed after all tests complete.

- **Categorize with `[Trait]`** -- `[Trait("Category", "Integration")]` lets you run subsets: `dotnet test --filter Category=Integration`. Keep fast unit tests separate from slow integration tests.
