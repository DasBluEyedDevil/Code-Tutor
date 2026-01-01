---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
class Player
{
    private string name;
    private int score;
    
    // Without 'this' - confusing!
    public Player(string n, int s)
    {
        name = n;  // Works, but parameter names are weird
        score = s;
    }
    
    // With 'this' - much clearer!
    public Player(string name, int score)
    {
        this.name = name;   // this.name = field, name = parameter
        this.score = score;
    }
    
    public void DisplayInfo()
    {
        // 'this' is optional here but makes it clear
        Console.WriteLine("Name: " + this.name);
        Console.WriteLine("Score: " + this.score);
    }
    
    public Player Clone()
    {
        // Return a new player with same values
        return new Player(this.name, this.score);
    }
    
    public void CompareWith(Player other)
    {
        if (this.score > other.score)
            Console.WriteLine(this.name + " wins!");
        else
            Console.WriteLine(other.name + " wins!");
    }
}

// Usage
Player p1 = new Player("Alice", 100);
Player p2 = new Player("Bob", 150);
p1.CompareWith(p2);  // Inside CompareWith, 'this' refers to p1
```
