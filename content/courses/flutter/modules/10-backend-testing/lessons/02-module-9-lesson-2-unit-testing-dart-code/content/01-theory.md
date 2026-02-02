---
type: "THEORY"
title: "The dart test Package"
---


Dart's built-in `test` package is the foundation for all testing in Dart and Flutter projects. It provides everything you need to write and run tests.

**Installation:**

The `test` package is a dev dependency, meaning it is only used during development and not included in your production build.

```yaml
# pubspec.yaml
dev_dependencies:
  test: ^1.24.0
```

**Project Structure:**

```
my_project/
|-- lib/
|   |-- src/
|       |-- calculator.dart
|       |-- user_service.dart
|-- test/
|   |-- calculator_test.dart
|   |-- user_service_test.dart
|-- pubspec.yaml
```

**Convention:** Test files should mirror your lib structure and end with `_test.dart`.

**Running Tests:**

```bash
# Run all tests
dart test

# Run a specific test file
dart test test/calculator_test.dart

# Run tests matching a name pattern
dart test --name "calculates discount"

# Run with verbose output
dart test --reporter expanded
```

