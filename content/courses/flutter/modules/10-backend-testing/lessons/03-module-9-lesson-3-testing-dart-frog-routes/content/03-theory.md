---
type: "THEORY"
title: "Setting Up Your Test Environment"
---

Before writing tests, you need to configure your Dart Frog project for testing.

First, ensure you have the test dependencies in your pubspec.yaml:

```yaml
dev_dependencies:
  test: ^1.24.0
  mocktail: ^1.0.0
  dart_frog_test: ^0.1.0
```

Create a test directory structure that mirrors your routes:

```
my_api/
  routes/
    index.dart
    users/
      index.dart
      [id].dart
  test/
    routes/
      index_test.dart
      users/
        index_test.dart
        user_id_test.dart
```

This structure keeps your tests organized and easy to locate.