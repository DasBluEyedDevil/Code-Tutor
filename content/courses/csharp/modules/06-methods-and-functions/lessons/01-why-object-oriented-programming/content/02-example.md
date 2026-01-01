---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// WITHOUT OOP - messy!
string playerName = "Alice";
int playerScore = 100;
int playerHealth = 80;

void DisplayPlayer()
{
    Console.WriteLine(playerName + ": " + playerScore + " points, " + playerHealth + " HP");
}

// WITH OOP - clean and organized!
class Player
{
    public string Name;
    public int Score;
    public int Health;
    
    public void Display()
    {
        Console.WriteLine(Name + ": " + Score + " points, " + Health + " HP");
    }
}

// Creating objects from the class
Player alice = new Player();
alice.Name = "Alice";
alice.Score = 100;
alice.Health = 80;
alice.Display();

Player bob = new Player();
bob.Name = "Bob";
bob.Score = 150;
bob.Health = 60;
bob.Display();
```
