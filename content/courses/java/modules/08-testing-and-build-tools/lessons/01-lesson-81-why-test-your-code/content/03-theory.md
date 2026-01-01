---
type: "THEORY"
title: "JUnit - The Testing Framework"
---

JUnit is Java's most popular testing framework.

Example test:

@Test
public void testAdd() {
    int result = Calculator.add(2, 3);
    assertEquals(5, result);
}

If add(2,3) returns 5, test PASSES ✓
If it returns anything else, test FAILS ✗