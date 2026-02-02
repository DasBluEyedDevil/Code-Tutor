---
type: "THEORY"
title: "Why Serverpod Testing is Different"
---

Serverpod testing differs from Dart Frog testing in several key ways:

1. **Built-in Test Utilities**: Serverpod provides TestSession and other utilities designed specifically for testing endpoints

2. **Database Integration**: Serverpod tests can run against a real test database, catching ORM issues that mocks would miss

3. **Code Generation**: Your test setup can leverage Serverpod's generated client code for type-safe assertions

4. **Session-Based Auth**: Testing authenticated endpoints requires proper session handling, not just header mocking

5. **Streaming Support**: Serverpod's real-time features need specialized testing approaches

These differences make Serverpod tests more integration-focused, which catches more real-world bugs.