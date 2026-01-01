---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) 70% unit, 20% integration, 10% E2E**

The testing pyramid recommends:
- **Most**: Unit tests (fast, cheap, isolated)
- **Some**: Integration tests (medium speed, test combinations)
- **Few**: E2E tests (slow, expensive, brittle)

---

**Question 2: B) Arrange, Act, Assert**


---

**Question 3: B) They provide test isolation and don't require database setup**

Mock repositories:
- No database needed (tests run in memory)
- Fast execution (no I/O overhead)
- Complete control (easily simulate edge cases)
- Isolated (one test doesn't affect another)

---

**Question 4: C) 401 Unauthorized**

HTTP status codes in authentication:
- **401 Unauthorized**: Missing or invalid credentials/token
- **403 Forbidden**: Authenticated but not authorized (valid token, insufficient permissions)

---

**Question 5: C) Increases confidence that code works correctly**

Test coverage shows which code paths are tested:
- 80%+ coverage = most code is verified
- Low coverage = many code paths untested (likely bugs)
- Confidence to refactor and add features

Note: 100% coverage doesn't guarantee bug-free code, but it helps!

---



```kotlin
@Test
fun `example test`() {
    // Arrange - Set up test data and dependencies
    val user = createTestUser()

    // Act - Perform the action being tested
    val result = userService.deleteUser(user.id)

    // Assert - Verify the outcome
    assertTrue(result.isSuccess)
}
```
