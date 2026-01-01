---
type: "ANALOGY"
title: "Understanding the Concept"
---

There are TWO ways to build a database app:

DATABASE-FIRST (old way):
1. Create database tables in SQL
2. Generate C# classes from database
3. Hope they stay in sync!

CODE-FIRST (modern way):
1. Write C# classes
2. EF Core generates database from classes!
3. Classes are source of truth

Code-First is like building a house from blueprints:
• Blueprint = C# class
• House = Database table
• Change blueprint → Rebuild house automatically!

BENEFITS:
✅ Version control - Classes in Git, database changes tracked!
✅ Refactoring - Rename property? Database updates!
✅ Portable - Same classes work with SQL Server, PostgreSQL, etc.
✅ Testable - Use in-memory database for tests

Think: Code-First = 'Your C# code IS the database schema. Database generated from code!'