---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`//`**: Two slashes create a 'comment' - notes for humans that the computer ignores. Use these to explain your code!

**`Console.WriteLine`**: This is like saying "Computer, speak!" It tells the computer to display text on the screen.

**`("Hello, World!")`**: The text inside quotes is the MESSAGE you want to display. It must be in quotes so C# knows it's text, not code.

**`;`**: The semicolon is like a period at the end of a sentence. It tells C#: "This instruction is complete!"

## Top-Level Statements in .NET

In modern C# (.NET 6+), you can write code directly without wrapping it in a class and Main method. This feature, called 'top-level statements', lets beginners start coding immediately:

```csharp
// No class needed! Just write code.
Console.WriteLine("Hello!");
```

Behind the scenes, C# creates the class structure for you. This is why our examples work without all the boilerplate code!
