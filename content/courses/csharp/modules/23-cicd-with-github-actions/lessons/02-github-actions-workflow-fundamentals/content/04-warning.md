---
type: "WARNING"
title: "Common Workflow Mistakes"
---

## Exposing Secrets in Logs

One of the most dangerous mistakes is accidentally printing secrets to workflow logs. GitHub automatically masks secrets you reference with `${{ secrets.NAME }}`, but secrets passed through environment variables or written to files can leak if echoed carelessly.

**The Problem:**
```yaml
- name: Debug connection
  run: echo "Connecting to $DATABASE_URL"  # DANGER: Prints password!
  env:
    DATABASE_URL: "postgres://user:${{ secrets.DB_PASSWORD }}@host/db"
```

**The Fix:** Never echo variables that contain secrets. Use `add-mask` for dynamically generated secrets:
```yaml
- run: echo "::add-mask::$GENERATED_TOKEN"
```

## Not Caching Dependencies

Without caching, every workflow run downloads all dependencies fresh. For a .NET project with many NuGet packages, this adds minutes to every build.

**Before (no caching):** 4-5 minutes for restore
**After (with caching):** 10-20 seconds for cache hit

```yaml
- uses: actions/cache@v4
  with:
    path: ~/.nuget/packages
    key: ${{ runner.os }}-nuget-${{ hashFiles('**/*.csproj') }}
```

The cache key includes a hash of project files, so the cache invalidates when dependencies change. Always add caching for package managers (NuGet, npm, pip, etc.).

## Tests Modifying Shared State

Integration tests that modify a shared database without cleanup cause intermittent failures. Test A creates data, Test B expects an empty database, and the order of execution determines success.

**The Problem:**
```csharp
[Fact]
public async Task CreateOrder_SavesOrder()
{
    var order = new Order { ... };
    await _dbContext.Orders.AddAsync(order);  // Leaves data behind!
    await _dbContext.SaveChangesAsync();
}
```

**The Fix:** Use database transactions that rollback after each test, or use a fresh database per test run:
```csharp
public class IntegrationTestBase : IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        await _dbContext.Database.BeginTransactionAsync();
    }
    
    public async Task DisposeAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }
}
```

## Ignoring Failing Builds

When builds fail frequently, teams develop 'alarm fatigue' and stop paying attention. Failed main branch builds become normal, and real problems slip through.

**Prevention strategies:**
1. Treat failed builds as P1 incidents requiring immediate attention
2. Use branch protection rules requiring passing CI before merge
3. Add notifications to team channels for failures
4. Track and fix flaky tests immediately
5. Review build history weekly to identify patterns

## Running Expensive Jobs Unnecessarily

Running all tests on every documentation change wastes compute resources and delays feedback.

**The Fix:** Use path filters to skip irrelevant workflows:
```yaml
on:
  push:
    paths-ignore:
      - '**.md'
      - 'docs/**'
      - '.github/**'
```

Or use conditional job execution:
```yaml
jobs:
  check-changes:
    outputs:
      should-test: ${{ steps.filter.outputs.src }}
    steps:
      - uses: dorny/paths-filter@v3
        id: filter
        with:
          filters: |
            src:
              - 'src/**'
              - 'tests/**'
  
  test:
    needs: check-changes
    if: needs.check-changes.outputs.should-test == 'true'
```