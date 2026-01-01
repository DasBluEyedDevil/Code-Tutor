---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues!

**Test Isolation Failure**: Tests that share state (static fields, singletons) cause flaky results. Each test must be independent - create fresh instances in each test method.

**Assert.Equal with Floating Point**: `Assert.Equal(0.1 + 0.2, 0.3)` FAILS due to floating-point precision! Use `Assert.Equal(expected, actual, precision)` for decimals/doubles.

**Async Tests Without Task Return**: `async void` tests appear to pass even when they fail! Always use `async Task` for async tests, never `async void`.

**Theory Without Enough Test Cases**: One [InlineData] doesn't prove correctness. Include edge cases: zero, negative, max values, null, empty strings.

**xUnit v3 Migration Note**: xUnit v3 introduces `TheoryDataRow<T>` for strongly-typed theory data and `Assert.Equivalent` for deep object comparison. When upgrading, review new assertion options.