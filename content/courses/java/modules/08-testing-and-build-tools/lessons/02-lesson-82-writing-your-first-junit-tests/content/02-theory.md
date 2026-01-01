---
type: "THEORY"
title: "Common Assertion Methods"
---

JUnit provides assertion methods to verify results:

EQUALITY:
assertEquals(expected, actual);
assertEquals(5, calculator.add(2, 3));

BOOLEAN:
assertTrue(condition);
assertTrue(list.isEmpty());
assertFalse(condition);
assertFalse(list.contains("X"));

NULL CHECKS:
assertNull(object);
assertNotNull(object);

EXCEPTIONS:
assertThrows(IllegalArgumentException.class, () -> {
    calculator.divide(10, 0);
});

ARRAYS:
assertArrayEquals(expectedArray, actualArray);

CUSTOM MESSAGE:
assertEquals(5, result, "Addition failed");
// If fails, shows: "Addition failed"