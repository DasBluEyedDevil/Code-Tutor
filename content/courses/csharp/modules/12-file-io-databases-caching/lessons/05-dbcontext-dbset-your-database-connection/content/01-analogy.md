---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine a library:

LIBRARY BUILDING = DbContext
• The entire library facility
• Entry point to all resources
• Handles check-in/check-out (SaveChanges)
• Tracks what you borrowed (Change Tracking)

BOOKSHELVES = DbSet<T>
• Fiction shelf = DbSet<FictionBook>
• Science shelf = DbSet<ScienceBook>
• Each shelf (DbSet) contains books (entities) of one type

DbContext responsibilities:
• CONNECTION management
• CHANGE TRACKING (remembers what you modified)
• QUERY TRANSLATION (LINQ → SQL)
• TRANSACTION management
• CACHING (reduce database trips)

DbSet<T> is a COLLECTION that:
• Represents a table
• Queryable with LINQ
• Track additions/removals

Think: DbContext = 'Your database session', DbSet = 'A specific table'!