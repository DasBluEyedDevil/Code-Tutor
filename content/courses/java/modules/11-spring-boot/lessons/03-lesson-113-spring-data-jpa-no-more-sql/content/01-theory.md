---
type: "THEORY"
title: "The Problem: Writing SQL is Repetitive"
---

Every entity needs similar code:

findAll(), findById(), save(), deleteById()...

Writing JDBC code:
- PreparedStatement for every query
- ResultSet parsing
- Connection management
- Hundreds of lines of boilerplate!

Spring Data JPA eliminates this:
- Auto-generates common queries
- Maps Java objects to database tables
- Handles connections automatically
- You write interfaces, Spring implements them!