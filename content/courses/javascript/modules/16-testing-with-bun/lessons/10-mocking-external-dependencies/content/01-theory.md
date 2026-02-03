---
type: "THEORY"
title: "Why Mock?"
---

Mocking isolates your code from external systems like databases, APIs, and file systems. This provides several critical benefits for testing:

**Isolation**: Tests verify YOUR code, not third-party services. When a test fails, you know the bug is in your code, not in Stripe's API being slow.

**Speed**: Real API calls take 100-500ms. Mocked calls take <1ms. A test suite with 100 API tests goes from 50 seconds to under 1 second.

**Determinism**: Real services return different data, have rate limits, and occasionally fail. Mocks return exactly what you configure, every single time.

**Edge Case Testing**: How do you test what happens when the payment API returns a fraud alert? With mocks, you just configure that response.

**Test Doubles Terminology:**

- **Mock**: Replacement with pre-programmed responses that verifies it was called correctly
- **Stub**: Replacement that returns canned data (no verification)
- **Spy**: Wraps real implementation but records calls for verification
- **Fake**: Working implementation with shortcuts (like in-memory database)

In practice, modern frameworks blur these lines. Bun and Vitest use 'mock' for all test doubles.