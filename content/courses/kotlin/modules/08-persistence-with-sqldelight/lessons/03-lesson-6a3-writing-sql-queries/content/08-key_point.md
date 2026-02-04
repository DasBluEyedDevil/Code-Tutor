---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Define queries in `.sq` files with named labels** like `selectAll:`, `insert:`, `updateById:`. SQLDelight generates suspend functions for execute queries and regular functions for select queries.

**Use query parameters with `:paramName` syntax**—SQLDelight generates type-safe function parameters. Write `SELECT * FROM user WHERE id = :userId` to get `selectByUserId(userId: Long)`.

**Queries return generated data classes** matching selected columns. Select `id, name, email` and SQLDelight creates a result class with exactly those properties—no more manual mapping or reflection.
