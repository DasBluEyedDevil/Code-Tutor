---
type: "KEY_POINT"
title: "The Testing Pattern"
---

Every Dart Frog route test follows this pattern:

1. **Arrange**: Create a MockRequestContext and configure it with the data your handler needs
2. **Act**: Call your route handler function with the mock context
3. **Assert**: Verify the response status code, headers, and body match expectations

This is the Arrange-Act-Assert pattern, and it keeps your tests clean and readable.