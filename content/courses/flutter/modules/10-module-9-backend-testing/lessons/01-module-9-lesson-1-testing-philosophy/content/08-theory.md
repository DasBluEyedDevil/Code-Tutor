---
type: "THEORY"
title: "Testing Strategy for Serverpod"
---


**Serverpod** has a more structured approach to testing due to its full-featured nature.

**Serverpod Testing Layers:**

1. **Endpoint Tests**: Test your Serverpod endpoints with mock sessions
2. **Model Tests**: Validate serialization and business logic in models
3. **Database Tests**: Test repository operations with test databases
4. **Integration Tests**: Test full request-response cycles

**Serverpod Test Structure:**

```
my_server/
|-- lib/
|   |-- src/
|       |-- endpoints/
|           |-- user_endpoint.dart
|-- test/
|   |-- endpoints/
|       |-- user_endpoint_test.dart
|   |-- integration/
|       |-- user_flow_test.dart
|-- pubspec.yaml
```

**Key Differences from Dart Frog:**

- Serverpod provides `TestSession` for mocking database and authentication
- Built-in support for testing with PostgreSQL test databases
- Automatic cleanup of test data between tests
- Strong typing through generated code means fewer runtime errors to test for

**Setup for Serverpod Testing:**

```yaml
# pubspec.yaml
dev_dependencies:
  test: ^1.24.0
  mocktail: ^1.0.0
  serverpod_test: ^2.0.0
```

