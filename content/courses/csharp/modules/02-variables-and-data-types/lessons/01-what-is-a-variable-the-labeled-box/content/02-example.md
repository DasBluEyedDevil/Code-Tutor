---
type: "EXAMPLE"
title: "Code Example"
---

Let's walk through creating, assigning, and using variables step by step. Notice how we first declare the variable (create the box), then assign a value (put something in), and finally use it (look inside the box). You can also use the `var` keyword when the type is obvious from the right side - C# figures out the type automatically!

```csharp
// Create a variable (declare it)
string playerName;

// Put something in the box (assign a value)
playerName = "Alex";

// Look at what's in the box (use it)
Console.WriteLine("Player name: " + playerName);

// Put something new in the box (reassign)
playerName = "Jordan";
Console.WriteLine("New player: " + playerName);

// Modern C#: Declare and initialize in one line
var score = 100;  // C# knows this is an int!
var message = "Hello";  // C# knows this is a string!
Console.WriteLine(message + ", your score is " + score);
```
