---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
class Player
{
    // STATIC - shared by all players
    public static int TotalPlayers = 0;
    public static int MaxScore = 0;
    
    // INSTANCE - each player has their own
    public string Name;
    public int Score;
    
    public Player(string name)
    {
        Name = name;
        Score = 0;
        TotalPlayers++;  // Increment shared counter
    }
    
    // INSTANCE method - works with specific player
    public void AddPoints(int points)
    {
        Score += points;
        
        // Update static MaxScore if this player beat it
        if (Score > MaxScore)
            MaxScore = Score;
    }
    
    // STATIC method - doesn't need a specific player
    public static void DisplayStats()
    {
        Console.WriteLine("Total Players: " + TotalPlayers);
        Console.WriteLine("Highest Score: " + MaxScore);
    }
}

// Usage
Player p1 = new Player("Alice");
Player p2 = new Player("Bob");

// Access static through class name
Console.WriteLine("Players: " + Player.TotalPlayers);  // 2

p1.AddPoints(100);
p2.AddPoints(150);

// Static method called through class name
Player.DisplayStats();  // Total: 2, Max: 150

// Each instance has own Score
Console.WriteLine(p1.Score);  // 100
Console.WriteLine(p2.Score);  // 150
```
