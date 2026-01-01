---
type: "KEY_POINT"
title: "Tests are Like Scientific Experiments"
---

HYPOTHESIS (What you expect):
"When I add 2 + 3, I should get 5"

EXPERIMENT (The test):
@Test
public void testAddition() {
    int result = add(2, 3);
    assertEquals(5, result);  // Verify hypothesis
}

RESULT:
✓ Test passes → Hypothesis confirmed
✗ Test fails → Something's wrong, investigate!

Just like science: tests help you understand if your code "theory" is correct.