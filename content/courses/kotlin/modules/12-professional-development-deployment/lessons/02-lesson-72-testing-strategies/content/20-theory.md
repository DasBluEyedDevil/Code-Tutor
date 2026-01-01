---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: A) 70% unit, 20% integration, 10% E2E**

The testing pyramid recommends:
- **Most**: Unit tests (fast, isolated)
- **Some**: Integration tests
- **Few**: E2E tests (slow, brittle)

---

**Question 2: B) Defines the behavior of a mock**


Tells the mock: "When getUser(1) is called, return this user"

---

**Question 3: C) Use `runTest`**


`runTest` provides a coroutine scope for testing.

---

**Question 4: B) Finds a composable with testTag("button")**


Test tags help locate composables in tests.

---

**Question 5: B) Write a failing test**

TDD cycle:
1. **Red**: Write failing test
2. **Green**: Write minimal code to pass
3. **Refactor**: Improve code

---



```kotlin
Button(modifier = Modifier.testTag("button")) { }

composeTestRule.onNodeWithTag("button").performClick()
```
