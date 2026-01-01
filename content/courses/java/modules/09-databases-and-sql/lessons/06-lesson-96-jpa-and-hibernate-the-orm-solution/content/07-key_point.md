---
type: "KEY_POINT"
title: "JPA vs JDBC - The Comparison"
---

JDBC (Manual Labor):
- Write SQL for every operation
- Manually map ResultSet to objects
- Handle connections, statements, exceptions
- ~50 lines for a simple CRUD operation

JPA (Automation):
- Annotations define the mapping once
- CRUD is 1-2 lines of code
- Relationships handled automatically
- Type-safe queries with JPQL

WHEN TO USE EACH:
JPA: 90% of applications - simpler, faster development
JDBC: Complex queries, performance-critical code, legacy systems

BUT: Understanding JDBC helps you debug JPA issues!
That's why we learned JDBC first.