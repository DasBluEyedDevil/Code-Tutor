---
type: "THEORY"
title: "Testing Strategy for Dart Frog"
---


**Dart Frog** provides built-in testing support that makes it easy to test your routes and middleware.

**Project Structure for Testing:**

```
my_api/
|-- routes/
|   |-- index.dart
|   |-- users/
|       |-- index.dart
|       |-- [id].dart
|-- test/
|   |-- routes/
|       |-- index_test.dart
|       |-- users/
|           |-- index_test.dart
|           |-- id_test.dart
|-- pubspec.yaml
```

**Key Testing Principles for Dart Frog:**

1. **Test Routes in Isolation**: Use `RequestContext` mocks to test route handlers without running a server

2. **Test Middleware Separately**: Middleware should be testable independently of routes

3. **Use dart_frog_test Package**: Provides utilities for creating test requests and contexts

4. **Test HTTP Methods**: Verify each route handles GET, POST, PUT, DELETE correctly

5. **Test Error Responses**: Ensure proper error codes and messages for invalid requests

**Setup for Dart Frog Testing:**

```yaml
# pubspec.yaml
dev_dependencies:
  test: ^1.24.0
  mocktail: ^1.0.0
```

