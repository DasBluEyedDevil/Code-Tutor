---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`\n`**: The backslash-n creates a newline (line break). It's called an 'escape sequence' – special characters that do something instead of displaying.

**`Multiple WriteLine vs. \n`**: Both methods work! Multiple WriteLine is clearer for beginners. Using \n is more compact but harder to read at first.

## Common Escape Sequences

| Sequence | Meaning |
|----------|--------|
| `\n` | Newline (line break) |
| `\t` | Tab (horizontal indent) |
| `\r` | Carriage return |
| `\\` | Literal backslash |
| `\"` | Literal double quote |
| `\'` | Literal single quote |
| `\0` | Null character |
| `\e` | Escape character (C# 13+) |

**C# 13 Feature**: The `\e` escape sequence is new in C# 13! It represents the escape character (Unicode 0x1B), commonly used for ANSI terminal colors:

```csharp
// C# 13: \e escape sequence for terminal colors
Console.WriteLine("\e[32mGreen text\e[0m");
Console.WriteLine("\e[1;31mBold red\e[0m");
```

Note: ANSI colors may not work in all terminals.

## Write vs. WriteLine

**`Console.Write()`**: Outputs text WITHOUT moving to a new line.
**`Console.WriteLine()`**: Outputs text AND moves to a new line.

```csharp
Console.Write("Hello ");
Console.Write("World");  // Output: Hello World (same line)

Console.WriteLine("Hello");
Console.WriteLine("World"); // Output on separate lines
```