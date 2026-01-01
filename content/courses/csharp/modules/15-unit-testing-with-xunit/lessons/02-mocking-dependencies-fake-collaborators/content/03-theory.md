---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`new Mock<IInterface>()`**: Creates a mock object that implements the interface. All methods return default values unless you Setup() them.

**`mock.Setup(x => x.Method(params)).Returns(value)`**: Configure what the mock returns when a method is called. Use It.IsAny<T>() for 'any argument of type T'.

**`mock.Object`**: Gets the actual mock instance to inject into your class. This is what you pass to constructors.

**`mock.Verify(x => x.Method(), Times.Once)`**: Verify that a method was called. Times options: Once, Never, Exactly(n), AtLeast(n), AtMost(n). Fails if condition not met.

**`It.IsAny<T>()`**: Match any argument of type T. Use in Setup or Verify. Example: 'It.IsAny<string>()' matches any string.

**`It.Is<T>(predicate)`**: Match arguments that satisfy a condition. Example: 'It.Is<int>(x => x > 0)' matches positive integers.