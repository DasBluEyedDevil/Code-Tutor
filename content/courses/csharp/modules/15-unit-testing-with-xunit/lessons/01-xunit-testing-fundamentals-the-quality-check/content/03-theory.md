---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`[Fact]`**: Marks a method as a test. Method must be public, void (or Task for async), with no parameters. xUnit discovers and runs all [Fact] methods.

**`[Theory] + [InlineData]`**: Parameterized tests! Run the same test with different inputs. Each [InlineData] creates a separate test case. Great for testing multiple scenarios efficiently.

**`Assert.Equal(expected, actual)`**: Core assertion. IMPORTANT: expected comes FIRST! 'Assert.Equal(5, result)' not 'Assert.Equal(result, 5)'. Wrong order = confusing failure messages.

**`Assert.Throws<TException>(() => code)`**: Verifies that code throws a specific exception. If no exception or wrong type, test fails. Use lambda to wrap the throwing code.

**`Test naming: Method_Scenario_Expected`**: Convention: 'Add_TwoPositiveNumbers_ReturnsSum'. Clear names document behavior and make failures obvious.