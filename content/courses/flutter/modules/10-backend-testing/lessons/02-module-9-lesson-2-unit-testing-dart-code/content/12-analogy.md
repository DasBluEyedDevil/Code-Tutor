---
type: "ANALOGY"
title: "The Calculator Warranty Test"
---

Unit testing a Dart function is like testing a calculator before it leaves the factory. You punch in "2 + 2" and check that the screen shows "4." You punch in "0 / 0" and check that it shows an error instead of crashing. You do not test whether the calculator's case is the right color or whether the buttons feel good -- those are different tests. You are only testing whether the math engine produces correct outputs for given inputs.

In Dart, each unit test is one of these button presses. You call a function with specific arguments (the input), and you assert that it returns the expected result (the output). If `calculateTotal(items: [10, 20, 30])` does not return `60`, the test fails and you know exactly which function broke and what it returned instead.

**The "unit" in unit testing means isolation.** You test one function, one method, one class at a time. If the function depends on a database or network call, you replace those with mocks -- like testing the calculator's math engine without plugging it into a wall outlet. This isolation is what makes unit tests fast (milliseconds per test) and precise (failure points directly to the broken function).
