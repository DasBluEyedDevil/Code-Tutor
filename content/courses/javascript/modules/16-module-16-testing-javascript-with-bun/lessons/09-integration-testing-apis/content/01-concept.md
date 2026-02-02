---
type: "THEORY"
title: "Unit vs Integration Tests"
---

Understanding when to use unit tests versus integration tests is fundamental to building a robust test suite. Unit tests verify individual functions in isolation - they're fast, focused, and numerous. Integration tests verify that multiple components work together correctly - they're slower but catch different types of bugs.

The Testing Pyramid guides test distribution:

**Unit Tests (Base)**: Test pure functions, business logic, utilities. Run in milliseconds. You should have hundreds of these.

**Integration Tests (Middle)**: Test API endpoints, database operations, service interactions. Run in seconds. You should have dozens of these.

**End-to-End Tests (Top)**: Test complete user workflows through the UI. Run in minutes. You should have few of these.

**Cost-Benefit Tradeoffs:**
- Unit tests are cheap to write and run but miss integration issues
- Integration tests catch real-world bugs but are slower and more complex
- E2E tests catch everything but are brittle and expensive to maintain

**What Integration Tests Actually Test:**
- Real HTTP request/response cycles
- Database connections and queries
- Authentication and authorization flows
- Error handling across system boundaries
- Request validation and serialization

A healthy codebase follows the pyramid: many unit tests, some integration tests, few E2E tests. Integration tests fill the gap between isolated function testing and full system testing.