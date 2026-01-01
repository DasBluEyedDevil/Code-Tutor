---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`using var context = new DbContext();`**: Modern C# 8+ using declaration. DbContext implements IDisposable. The 'using var' ensures disposal at end of scope (closes connection, releases resources). Cleaner than the old braces style!

**`context.Entry(entity).State`**: Check entity state: Unchanged, Added, Modified, Deleted, Detached. EF tracks state automatically when you modify objects!

**`DbSet operations`**: Add(), Remove(), Find(key), ToList(), Where(), etc. DbSet implements IQueryable<T> - full LINQ support!

**`context.SaveChanges()`**: Persists ALL tracked changes. Batches INSERT, UPDATE, DELETE into single transaction. Call once after all changes!