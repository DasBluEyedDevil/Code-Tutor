---
type: "THEORY"
title: "Why Mock?"
---

**Mocking** replaces real objects with fake ones during tests.

**When to mock:**
- External APIs (don't hit real servers in tests)
- Databases (faster, no side effects)
- Time/random (make tests deterministic)
- Expensive operations (fast tests)

**When NOT to mock:**
- Your own code (test the real thing)
- Simple data structures (just use them)
- When integration tests are more valuable

**Python's mocking tools:**
- `unittest.mock` (standard library)
- `pytest-mock` (pytest plugin, cleaner API)