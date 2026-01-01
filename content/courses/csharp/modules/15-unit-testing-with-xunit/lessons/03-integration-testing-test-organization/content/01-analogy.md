---
type: "ANALOGY"
title: "Understanding the Concept"
---

Unit tests check INDIVIDUAL parts work. But do they work TOGETHER?

Imagine testing a bicycle:
• UNIT TESTS: Test brake pads, gears, wheels separately
• INTEGRATION TESTS: Test brakes + wheels together - does braking actually stop the bike?

TYPES OF TESTS:
1. UNIT TESTS (70-80%)
   - Test one class in isolation
   - Fast, run in milliseconds
   - Mock all dependencies

2. INTEGRATION TESTS (15-20%)
   - Test multiple components together
   - May use real database (in-memory)
   - Slower but more realistic

3. END-TO-END TESTS (5-10%)
   - Test entire application
   - Real browser, real API calls
   - Slowest but highest confidence

TEST PYRAMID:
   /\\    E2E (few)
  /  \\
 /____\\  Integration (some)
/______\\  Unit (many)

Think: 'Unit tests for speed, integration tests for confidence, E2E tests for critical paths!'