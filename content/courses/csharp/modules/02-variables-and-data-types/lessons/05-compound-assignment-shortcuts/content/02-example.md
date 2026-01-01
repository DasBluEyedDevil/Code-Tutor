---
type: "EXAMPLE"
title: "Code Example"
---

Compound assignment operators combine an operation with assignment in one step. They're shorter, clearer, and less error-prone than writing the variable name twice. The increment (++) and decrement (--) operators are especially common - you'll see them everywhere in real code!

```csharp
// Start with a score
int score = 100;
Console.WriteLine($"Starting score: {score}");

// Add 50 points (the long way)
score = score + 50;
Console.WriteLine($"After earning 50: {score}");

// Add 25 points (the shortcut way!)
score += 25;
Console.WriteLine($"After earning 25 more: {score}");

// Multiply score by 2 (bonus!)
score *= 2;
Console.WriteLine($"After 2x bonus: {score}");

// Lives example with decrement
int lives = 3;
lives--;  // Lost a life! Same as lives = lives - 1
Console.WriteLine($"Lives remaining: {lives}");

// All compound operators:
// +=  -=  *=  /=  %=  (and more!)
int coins = 10;
coins += 5;   // Now 15
coins -= 3;   // Now 12
coins *= 2;   // Now 24
coins /= 4;   // Now 6
coins %= 4;   // Now 2 (6 % 4 = 2)
```
