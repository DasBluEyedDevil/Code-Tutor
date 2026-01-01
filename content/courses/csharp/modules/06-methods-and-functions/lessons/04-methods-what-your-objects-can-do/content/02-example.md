---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
class Calculator
{
    // Method with parameters and return value
    public int Add(int a, int b)
    {
        return a + b;
    }
    
    // Method with no return (void)
    public void DisplayResult(int result)
    {
        Console.WriteLine("Result: " + result);
    }
    
    // Method with multiple parameters
    public double Average(double x, double y, double z)
    {
        return (x + y + z) / 3;
    }
}

class Player
{
    public string Name;
    public int Health = 100;
    public int Score = 0;
    
    // Method that modifies object state
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine(Name + " took " + damage + " damage! Health: " + Health);
    }
    
    // Method with return value
    public bool IsAlive()
    {
        return Health > 0;
    }
}

// Usage
Calculator calc = new Calculator();
int sum = calc.Add(5, 3);
calc.DisplayResult(sum);

Player player = new Player();
player.Name = "Hero";
player.TakeDamage(30);
if (player.IsAlive())
    Console.WriteLine("Still fighting!");
```
