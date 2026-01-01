---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues!

**Moq Strict vs Loose Mode**: Default is Loose (unsetup methods return defaults). `new Mock<T>(MockBehavior.Strict)` throws on unconfigured calls - useful for catching missing setups but requires more work.

**NSubstitute Alternative**: Consider NSubstitute for cleaner syntax: `var mock = Substitute.For<IService>(); mock.Method().Returns(value);` - no .Object needed, more readable.

**Mocking Concrete Classes**: Moq can only mock virtual methods on classes. Prefer interfaces for dependencies. If you must mock classes, ensure methods are virtual.

**Callback vs Returns for Side Effects**: Use `.Callback()` when you need to capture arguments or perform side effects: `mock.Setup(x => x.Save(It.IsAny<User>())).Callback<User>(u => savedUser = u);`

**Async Method Mocking**: For async methods, use `.ReturnsAsync(value)` not `.Returns(Task.FromResult(value))`. The async helper is cleaner and handles edge cases better.