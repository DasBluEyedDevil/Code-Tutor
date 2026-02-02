---
type: "KEY_POINT"
title: "TestSession vs Production Session"
---

TestSession provides the same interface as a production Session but with testing features:

- **Isolated Database**: Each test can use a clean database state
- **Controllable Time**: You can manipulate session timestamps for time-sensitive tests
- **Mock Authentication**: Easily simulate authenticated and unauthenticated users
- **Transaction Control**: Wrap tests in transactions that roll back automatically

Always use TestSession in your tests, never create production Sessions directly.