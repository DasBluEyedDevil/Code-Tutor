---
type: "KEY_POINT"
title: "ORM Bridges Objects and Tables"
---

## Key Takeaways

- **An ORM maps C# classes to database tables** -- your `Product` class becomes the `Products` table. Properties become columns. One object instance equals one row.

- **Write LINQ, get SQL** -- `context.Products.Where(p => p.Price > 10)` generates `SELECT * FROM Products WHERE Price > 10`. You work with objects, the ORM handles SQL translation.

- **`DbContext` is your database session** -- it tracks changes, generates SQL, and manages the connection. Register it with DI using `AddDbContext<T>()` in ASP.NET Core.
