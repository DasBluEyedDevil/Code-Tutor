---
type: "THEORY"
title: "Why Mock?"
---

Mocks replace real dependencies with controlled fakes.

When to mock:
- API calls (don't hit real servers in tests)
- Databases (tests should be fast and isolated)
- Time/dates (make tests deterministic)
- External services (payment, email)

When NOT to mock:
- The code you're actually testing
- Simple utility functions
- When real behavior is fast and reliable

Bun provides `mock()` and `spyOn()` from 'bun:test' - no external packages needed!

Over-mocking makes tests brittle and less valuable. Mock at the boundaries, not everywhere.