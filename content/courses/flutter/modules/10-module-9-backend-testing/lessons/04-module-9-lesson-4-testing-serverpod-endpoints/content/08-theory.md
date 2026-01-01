---
type: "THEORY"
title: "Testing with a Test Database"
---

Integration tests that use a real database catch issues that mocks miss:

- ORM mapping errors
- Database constraint violations
- Query performance issues
- Transaction behavior
- Migration problems

Serverpod makes database integration testing straightforward with built-in utilities.