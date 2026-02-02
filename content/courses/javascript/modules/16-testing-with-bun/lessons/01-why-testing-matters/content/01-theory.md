---
type: "THEORY"
title: "The Test Pyramid"
---

Testing is like quality control in manufacturing. Before shipping a product, you check it works.

The Test Pyramid has three levels:

1. **Unit Tests** (base) - Test individual functions in isolation. Fast, many of these.
2. **Integration Tests** (middle) - Test how components work together. Slower, fewer.
3. **End-to-End Tests** (top) - Test full user workflows. Slowest, fewest.

Why this shape? Unit tests are cheap and fast. E2E tests are expensive and slow. A healthy codebase has many unit tests, some integration tests, and few E2E tests.

Without tests, every change is a gamble. With tests, you refactor with confidence.