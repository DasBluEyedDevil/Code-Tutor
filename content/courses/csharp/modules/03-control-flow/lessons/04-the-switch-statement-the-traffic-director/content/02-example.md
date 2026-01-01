---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates both switch statements and the modern switch expression syntax.

```csharp
// === SWITCH STATEMENT (classic) ===
int dayNumber = 3;

switch (dayNumber)
{
    case 1:
        Console.WriteLine("Monday");
        break;
    case 2:
        Console.WriteLine("Tuesday");
        break;
    case 3:
        Console.WriteLine("Wednesday");
        break;
    case 6:
    case 7:
        Console.WriteLine("Weekend!");
        break;
    default:
        Console.WriteLine("Invalid day");
        break;
}

// === SWITCH EXPRESSION (modern C# 8+) ===
// Much cleaner! Returns a value directly.
string dayName = dayNumber switch
{
    1 => "Monday",
    2 => "Tuesday",
    3 => "Wednesday",
    4 => "Thursday",
    5 => "Friday",
    6 or 7 => "Weekend!",   // 'or' pattern
    _ => "Invalid day"       // _ is the default
};
Console.WriteLine(dayName);

// === PATTERN MATCHING in switch expressions ===
int score = 85;
string grade = score switch
{
    >= 90 => "A",
    >= 80 and < 90 => "B",   // relational + logical patterns
    >= 70 and < 80 => "C",
    >= 60 => "D",
    _ => "F"
};
Console.WriteLine($"Grade: {grade}");
```
