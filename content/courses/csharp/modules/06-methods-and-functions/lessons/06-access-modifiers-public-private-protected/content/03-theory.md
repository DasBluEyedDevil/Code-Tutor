---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`private`**: Only accessible within the same class. Fields should almost ALWAYS be private! Access them through public properties or methods.

**`public`**: Accessible from anywhere. Use for methods and properties that form your class's PUBLIC INTERFACE - what users interact with.

**`protected`**: Accessible within the class AND derived classes (inheritance - next module!). Useful for base classes that child classes need to access.

**`Default (no modifier)`**: In C#, if you don't specify, it defaults to PRIVATE for class members. Always specify explicitly for clarity!

**`Encapsulation best practice`**: PRIVATE fields + PUBLIC properties/methods = encapsulation! Control how data is accessed and modified. Never expose fields directly!