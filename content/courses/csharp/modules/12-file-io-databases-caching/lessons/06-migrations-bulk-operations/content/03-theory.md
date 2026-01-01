---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`ExecuteUpdate()`**: EF Core 7+ feature. Updates multiple rows with single SQL! SetProperty(prop, value). Returns number of affected rows. WAY faster than load-modify-save!

**`ExecuteDelete()`**: EF Core 7+ feature. Deletes multiple rows with single SQL! No loading into memory. Executes immediately (not on SaveChanges!).

**`Migrations add`**: dotnet ef migrations add Name - Creates migration file with Up() and Down() methods. Snapshot of schema changes.

**`database update`**: dotnet ef database update - Applies pending migrations to database. Updates schema to match code!