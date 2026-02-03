---
type: "WARNING"
title: "Common Pitfalls"
---

## Application and Infrastructure Layer Mistakes

**Generic Repository Anti-Pattern**: Creating a `IRepository<T>` with `GetAll()`, `GetById()`, `Add()`, `Update()`, `Delete()` for every entity hides the real query needs behind a generic facade. You end up pulling entire tables into memory or adding dozens of specialized methods that defeat the generic purpose. Define focused repository interfaces per aggregate root instead.

**Ignoring CancellationToken**: Every async repository and service method should accept `CancellationToken`. Without it, cancelled HTTP requests still execute expensive database queries and external API calls to completion. Pass `CancellationToken` from controller through Application to Infrastructure.

**Unit of Work Misuse**: Calling `SaveChangesAsync()` inside individual repository methods means each operation is a separate transaction. Cross-cutting operations (create order + deduct stock) need a shared Unit of Work. Call `SaveChangesAsync()` once in the handler after all operations, not inside each repository method.

**Registering Everything as Scoped**: Not all services need the same DI lifetime. Repositories and DbContext should be Scoped (per-request). Stateless utility services can be Singleton. Transient should be rare. Wrong lifetimes cause either memory leaks (Singleton holding Scoped) or concurrency bugs (Scoped used across threads).

**Missing Error Mapping**: Infrastructure exceptions (SqlException, HttpRequestException, SmtpException) shouldn't propagate to Application layer. Catch infrastructure-specific exceptions and throw domain-meaningful exceptions instead. The Application layer shouldn't need to know if you're using SQL Server or PostgreSQL to handle errors properly.
