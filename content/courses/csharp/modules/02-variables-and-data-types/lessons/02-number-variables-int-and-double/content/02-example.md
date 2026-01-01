---
type: "EXAMPLE"
title: "Code Example"
---

Here are the most common numeric types you'll use. Notice that `int` is for whole numbers and `double` is for decimals. For financial calculations involving money, C# has a special `decimal` type that provides better precision - crucial when every penny counts!

```csharp
// Integer (whole numbers only)
int playerScore = 1500;
int lives = 3;

// Double (decimal numbers - good for scientific calculations)
double temperature = -5.5;
double distance = 3.14159;

// Decimal (precise decimals - ALWAYS use for money!)
decimal price = 19.99m;      // Note the 'm' suffix!
decimal accountBalance = 1234.56m;

// Display them
Console.WriteLine("Score: " + playerScore);
Console.WriteLine("Price: $" + price);
Console.WriteLine("Temperature: " + temperature + "C");

// You can do math with them!
int totalScore = playerScore + 500;
Console.WriteLine("New score: " + totalScore);
```
