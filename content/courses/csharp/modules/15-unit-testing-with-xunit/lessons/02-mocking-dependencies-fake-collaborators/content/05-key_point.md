---
type: "KEY_POINT"
title: "Mocking with Moq"
---

## Key Takeaways

- **`new Mock<IService>()` creates a fake implementation** -- configure return values with `.Setup(x => x.Method()).Returns(value)`. Pass `mock.Object` to the class under test.

- **`mock.Verify()` checks method calls** -- `mock.Verify(x => x.Save(), Times.Once)` asserts that `Save` was called exactly once. Use `Times.Never` to assert a method was not called.

- **Mock interfaces, not concrete classes** -- design your code with interfaces so dependencies can be replaced with mocks. This is why dependency injection and programming to interfaces matter.
