---
type: "WARNING"
title: "Migration and Bulk Operation Pitfalls"
---

## Watch Out For These Issues!

**Running migrations on production without backup**: `dotnet ef database update` modifies your database schema directly. Always back up your production database before applying migrations! Use `dotnet ef migrations script` to generate SQL scripts first, review them, then apply manually in production environments.

**Migration ordering matters**: Migrations are applied in the order they were created (by timestamp prefix). If two developers create migrations independently, merge conflicts can produce invalid schema states. Always run `dotnet ef migrations add` from the latest codebase after merging.

**ExecuteUpdate/ExecuteDelete bypass change tracking**: Bulk operations execute immediately against the database -- they do NOT go through SaveChanges()! Any entities already tracked in memory become stale. If you mix bulk operations with tracked entities in the same DbContext, call `context.ChangeTracker.Clear()` after bulk operations.

**Data loss from ExecuteDelete**: `ExecuteDelete()` permanently removes rows. There is no undo, no soft-delete, no recycle bin. Double-check your `.Where()` filter before calling ExecuteDelete, especially in production. Consider logging which rows will be affected first.
