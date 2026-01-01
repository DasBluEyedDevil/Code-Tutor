---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`UseInMemoryDatabase(name)`**: EF Core in-memory provider for testing. Fast, no external DB needed. Use unique name (Guid) per test to ensure isolation.

**`IDisposable`**: Implement Dispose() for cleanup. xUnit calls Dispose() after each test method. Perfect for cleaning up DbContext, connections, files.

**`[Collection(name)]`**: Share fixtures across test classes. Tests in same collection don't run in parallel. Useful for shared database state.

**`IClassFixture<T>`**: Shared setup/teardown for all tests in a class. Create expensive resources once, share across tests. Fixture disposed after all tests complete.

**`IAsyncLifetime`**: Async setup/teardown. Use InitializeAsync() and DisposeAsync() for async operations like database seeding.

**`[Trait(name, value)]`**: Categorize tests. Example: [Trait("Category", "Integration")]. Filter in test runner: 'dotnet test --filter Category=Integration'.