---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`System.IO namespace`**: File operations: File.ReadAllText(), File.WriteAllText(), File.Exists(), Directory.CreateDirectory(). Essential for working with files!

**`StringBuilder`**: From System.Text. Use when concatenating strings in loops! String concat creates new objects (slow). StringBuilder modifies in-place (fast).

**`DateTime`**: From System. DateTime.Now (current), DateTime.Today (date only). Arithmetic: subtract dates to get TimeSpan. Format with .ToString("yyyy-MM-dd").

**`DateOnly and TimeOnly`**: .NET 6+ added DateOnly (just date, no time) and TimeOnly (just time, no date). Cleaner than DateTime when you only need one!

**`Math class`**: Static methods: Math.Sqrt(), Math.Pow(), Math.Round(), Math.Abs(), Math.Min(), Math.Max(). From System namespace.

**`Path.Combine()`**: Cross-platform path building: `Path.Combine("folder", "file.txt")` works on both Windows and Linux/Mac!