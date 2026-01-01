---
type: "THEORY"
title: "Integration Testing for Dart Frog"
---

While unit tests mock dependencies, integration tests verify the entire request/response cycle. Integration tests catch issues that unit tests miss:

- Route configuration errors
- Middleware ordering problems
- Dependency injection issues
- Real database interactions

Dart Frog provides testing utilities that let you make real HTTP requests to your handlers.