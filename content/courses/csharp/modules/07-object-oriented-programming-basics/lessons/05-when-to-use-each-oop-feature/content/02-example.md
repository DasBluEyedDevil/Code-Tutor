---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// Example: Game character system

// INTERFACE - contract for anything that can attack
interface IAttacker
{
    void Attack();
    int GetDamage();
}

// ABSTRACT CLASS - template for all characters
abstract class Character
{
    public string Name;
    public int Health = 100;
    
    // Concrete method (shared by all)
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine(Name + " took " + damage + " damage. Health: " + Health);
    }
    
    // Abstract method (each character moves differently)
    public abstract void Move();
}

// DERIVED CLASS implementing abstract and interface
class Warrior : Character, IAttacker
{
    public int WeaponDamage = 20;
    
    public override void Move()
    {
        Console.WriteLine(Name + " marches forward");
    }
    
    public void Attack()
    {
        Console.WriteLine(Name + " swings sword!");
    }
    
    public int GetDamage()
    {
        return WeaponDamage;
    }
}

class Mage : Character, IAttacker
{
    public int SpellPower = 30;
    
    public override void Move()
    {
        Console.WriteLine(Name + " teleports");
    }
    
    public void Attack()
    {
        Console.WriteLine(Name + " casts fireball!");
    }
    
    public int GetDamage()
    {
        return SpellPower;
    }
}

// Polymorphism in action
Character[] party = { new Warrior { Name = "Thor" }, new Mage { Name = "Gandalf" } };
foreach (Character c in party)
{
    c.Move();  // Each moves differently!
}

IAttacker[] attackers = { new Warrior(), new Mage() };
foreach (IAttacker a in attackers)
{
    a.Attack();  // Each attacks differently!
}
```
