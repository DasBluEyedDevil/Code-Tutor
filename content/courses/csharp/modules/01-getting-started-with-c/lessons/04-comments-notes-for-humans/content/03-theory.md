---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`// Single-line comment`**: Everything after // on that line is ignored. Quick notes go here!

**`/* Multi-line comment */`**: Everything between /* and */ is ignored, even across multiple lines. Good for long explanations.

**`Why use comments?`**: Comments explain your thinking: "This loop finds the highest score" is better than just reading the code!

## XML Documentation Comments (Preview)

Professional C# developers use special `///` comments for documentation:

```csharp
/// <summary>
/// Displays a welcome message to the user.
/// </summary>
void ShowWelcome()
{
    Console.WriteLine("Welcome!");
}
```

These generate automatic documentation and show tooltips in your IDE. You'll learn more about these when we cover methods!

## Code Regions

C# also has `#region` directives to organize code into collapsible sections:

```csharp
#region Initialization Code
Console.WriteLine("Starting...");
Console.WriteLine("Loading...");
#endregion
```

Regions help organize large files but aren't true comments - they're preprocessor directives.