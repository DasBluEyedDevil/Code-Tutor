---
type: "EXAMPLE"
title: "TDD Workflow Commands"
---




```bash
# Watch mode - tests run on every save
flutter test --watch

# Run specific test file
flutter test test/features/cart/cart_notifier_test.dart

# Run tests matching pattern
flutter test --name 'addItem'

# Run with coverage report
flutter test --coverage
genhtml coverage/lcov.info -o coverage/html
open coverage/html/index.html
```
