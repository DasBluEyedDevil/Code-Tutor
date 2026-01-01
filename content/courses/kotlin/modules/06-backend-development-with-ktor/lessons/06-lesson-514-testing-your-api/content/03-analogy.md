---
type: "ANALOGY"
title: "The Concept"
---


### The Safety Net Analogy

Think of tests like a safety net for trapeze artists:

**Without Tests (No Safety Net)**:
- Every code change is scary
- Fear of breaking things prevents improvements
- Bugs discovered by users (embarrassing!)
- Hours spent manually testing after each change
- 😰 High stress, low confidence

**With Tests (Safety Net)**:
- Confident refactoring
- Catch bugs before deployment
- Automated validation (run tests in seconds)
- Documentation (tests show how code should work)
- ✅ Low stress, high confidence!

Tests are your safety net—they catch you when you fall.

### The Testing Pyramid


**Test Distribution**:
- **70%** Unit Tests: Fast, isolated, test individual functions
- **20%** Integration Tests: Test components working together
- **10%** End-to-End Tests: Test entire system from UI to database

We'll focus on unit and integration tests for backend APIs.

### Types of Tests for APIs

| Test Type | What It Tests | Example |
|-----------|---------------|---------|
| **Unit** | Single function/class in isolation | UserService.createUser() with mock repository |
| **Integration** | Multiple components together | POST /api/users endpoint with real database |
| **Contract** | API matches specification | Response has required fields |
| **Performance** | Speed and scalability | API handles 1000 req/sec |

---



```kotlin
          /\
         /  \        E2E Tests (Few)
        /____\       - Full system, slow, brittle
       /      \
      /        \     Integration Tests (Some)
     /__________\    - Multiple components, medium speed
    /            \
   /              \  Unit Tests (Many)
  /________________\ - Single component, fast, reliable
```
