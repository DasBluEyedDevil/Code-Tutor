---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// Traditional constructor
class Player
{
    public string Name;
    public int Score;
    public int Health;
    
    // Constructor - same name as class, no return type!
    public Player(string name, int score, int health)
    {
        Name = name;
        Score = score;
        Health = health;
        Console.WriteLine("Player created: " + Name);
    }
    
    public void Display()
    {
        Console.WriteLine(Name + ": " + Score + " points, " + Health + " HP");
    }
}

// C# 12 Primary Constructor - parameters in class declaration!
class Enemy(string name, int damage)
{
    public string Name { get; } = name;
    public int Damage { get; } = damage;
    
    public void Attack() => Console.WriteLine($"{Name} attacks for {Damage} damage!");
}

// Creating objects
Player alice = new Player("Alice", 100, 80);
alice.Display();

Enemy goblin = new Enemy("Goblin", 15);
goblin.Attack();
```
