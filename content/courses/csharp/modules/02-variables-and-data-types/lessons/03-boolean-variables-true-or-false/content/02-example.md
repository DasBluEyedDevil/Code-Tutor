---
type: "EXAMPLE"
title: "Code Example"
---

Booleans are perfect for tracking states and conditions in your program. Notice the naming convention - boolean variables typically start with 'is', 'has', 'can', or 'should' because they answer yes/no questions. This makes your code read almost like English!

```csharp
// Boolean variables - only true or false!
bool isPlayerAlive = true;
bool hasKey = false;
bool canJump = true;
bool shouldSaveGame = false;

// Display them
Console.WriteLine("Player alive: " + isPlayerAlive);
Console.WriteLine("Has key: " + hasKey);

// You can change them!
isPlayerAlive = false;
Console.WriteLine("Player alive now: " + isPlayerAlive);

// Booleans from comparisons (preview of what's coming!)
int score = 85;
bool isPassing = score >= 60;  // true!
Console.WriteLine("Passing: " + isPassing);
```
