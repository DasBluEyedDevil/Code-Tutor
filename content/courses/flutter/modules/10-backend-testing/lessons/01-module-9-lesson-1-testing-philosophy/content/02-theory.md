---
type: "THEORY"
title: "The Test Pyramid"
---


The **Test Pyramid** is a fundamental concept that guides how to structure your testing strategy. It visualizes the ideal distribution of different test types.

```
                    /\
                   /  \
                  / E2E \           (Few, Slow, Expensive)
                 /--------\
                /          \
               / Integration \      (Some, Medium Speed)
              /--------------\
             /                \
            /   Unit Tests     \    (Many, Fast, Cheap)
           /--------------------\
```

**Unit Tests (Base of Pyramid)**:
- Test individual functions, classes, and methods in isolation
- Fast to run (milliseconds each)
- Easy to write and maintain
- Catch bugs at the source
- Should make up 70-80% of your tests

**Integration Tests (Middle)**:
- Test how components work together
- Database operations, API calls with real services
- Slower than unit tests (seconds each)
- More complex setup required
- Should make up 15-25% of your tests

**End-to-End Tests (Top)**:
- Test complete user workflows
- Full API request-response cycles
- Slowest to run (seconds to minutes)
- Most expensive to maintain
- Should make up 5-10% of your tests

**Why This Shape Matters**:
- Bugs caught by unit tests are cheapest to fix
- Bugs that slip to E2E tests are expensive to diagnose
- A strong base of unit tests provides confidence for refactoring

