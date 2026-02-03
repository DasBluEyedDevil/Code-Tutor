---
type: "WARNING"
title: "ORM Misconceptions and Pitfalls"
---

## Watch Out For These Issues!

**ORMs do not eliminate SQL knowledge**: EF Core generates SQL for you, but debugging performance issues requires reading that SQL. Use `context.Database.Log` or `.ToQueryString()` to see generated queries. Do not treat the ORM as a black box!

**Not calling SaveChanges()**: The most common beginner mistake. Adding or modifying entities does NOTHING to the database until you call `context.SaveChanges()`. Changes live only in memory until that call. Forgetting it means your data silently disappears on restart.

**Tracking vs no-tracking confusion**: By default, EF Core tracks all queried entities. For read-only scenarios (displaying data), use `.AsNoTracking()` to avoid unnecessary memory overhead. For write scenarios, you need tracking -- do not add `.AsNoTracking()` to queries you plan to modify.

**Assuming ORM performance equals raw SQL**: ORMs add overhead. For simple CRUD, EF Core is excellent. For complex reporting queries or bulk operations, raw SQL via `context.Database.ExecuteSqlRaw()` or Dapper may be significantly faster. Know when to drop down to SQL.
