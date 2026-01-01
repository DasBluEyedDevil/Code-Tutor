---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These!

**^0 is out of bounds**: Unlike regular indexing, ^0 means 'one past the end' - it will throw IndexOutOfRangeException! Use ^1 for the last element.

**Requires .NET 9 / C# 13**: This feature requires the C# 13 compiler, which comes with .NET 9 SDK. Older projects won't support this syntax in object initializers.

**Collection must have Length property**: The ^ operator requires a Length or Count property to calculate the actual index. Custom collections need to implement this.

**Not all collections support it**: While arrays and List<T> work great, some custom collections might not support index initialization. Check documentation!

**Readability trade-off**: While ^1, ^2 is convenient, using too many from-end indexes can make code harder to follow. Use comments when the intent isn't obvious.