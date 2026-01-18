---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Forward slash vs. backslash**: Escape sequences use BACKSLASH `\`, not forward slash `/`!
```csharp
Console.WriteLine("Line1/nLine2");  // Wrong: prints literally /n
Console.WriteLine("Line1\nLine2"); // Correct: creates new line
```

**The \x escape sequence trap**: Before C# 13's `\e`, you might see `\x1b` for the escape character. But `\x` is dangerous:
```csharp
string bad = "\x1b2F";  // BUG: Reads as single char U+1B2F!
string good = "\e2F";   // C# 13: Correct - ESC + '2' + 'F'
```

**Escape sequences only work in strings**: They must be inside quotes!
```csharp
\n  // ERROR: Not in a string!
"\n" // Correct: Inside quotes
```

**Raw string literals ignore escapes**: If you use raw strings (triple quotes), escape sequences are literal:
```csharp
Console.WriteLine("""Line1\nLine2"""); // Prints: Line1\nLine2 (literally!)
```
