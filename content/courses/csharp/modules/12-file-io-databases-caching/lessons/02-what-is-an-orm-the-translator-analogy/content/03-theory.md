---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`ORM = Object-Relational Mapper`**: Bridges gap between OOP (objects, classes) and relational databases (tables, rows). Translates C# to SQL automatically!

**`Entity`**: C# class that maps to database table. Properties = table columns. Customer class → Customers table. One object = one row!

**`DbContext`**: The 'portal' to database. Contains DbSet<T> properties for each table. Tracks changes. Generates SQL. Your main EF class!

**`LINQ to SQL`**: Write LINQ queries on DbSet. ORM converts to SQL! .Where(), .Select(), .OrderBy() all become SELECT, WHERE, ORDER BY in SQL.