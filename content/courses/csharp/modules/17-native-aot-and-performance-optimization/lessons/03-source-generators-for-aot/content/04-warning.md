---
type: "WARNING"
title: "Common Pitfalls"
---

## Source Generator Gotchas

**Must Be Partial**: Forgetting the `partial` keyword is the #1 mistake. Without it, the source generator cannot add its implementation and you get cryptic compiler errors.

**Nested Types Not Automatically Included**: If your class has nested types or collections, you must explicitly register each one with `[JsonSerializable(typeof(List<MyType>))]`. Missing registrations cause runtime failures in AOT.

**Regex Complexity Limits**: Extremely complex regex patterns may cause source generator build failures or generate slow code. Keep patterns reasonably simple or break them into multiple smaller patterns.

**IDE Support Varies**: Some IDEs may not show generated code in IntelliSense immediately. Rebuild the project if auto-complete for generated methods doesn't appear.

**Source Generator Ordering**: When using multiple source generators, order can matter. If one generator's output is input to another, you may need explicit build dependencies.

**LoggerMessage Performance**: While [LoggerMessage] is faster than string interpolation, the performance gain is only significant at high log volumes. Don't over-optimize simple applications.