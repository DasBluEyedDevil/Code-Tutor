---
type: "KEY_POINT"
title: "Test Naming Conventions"
---

GOOD TEST NAMES describe WHAT is being tested:

❌ Bad names:
test1()
testStuff()
myTest()

✓ Good names:
testAddPositiveNumbers()
testDivideByZeroThrowsException()
testEmptyListReturnsZero()

PATTERN:
test[MethodName][Scenario][ExpectedResult]

Examples:
testCalculateDiscountWithValidCouponReturnsReducedPrice()
testGetUserWithInvalidIdReturnsNull()
testSortEmptyArrayReturnsEmptyArray()

Good names make it OBVIOUS what broke when a test fails!