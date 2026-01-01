---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// BASE CLASS
class Vehicle
{
    public string Brand;
    public int Year;
    
    public void Start()
    {
        Console.WriteLine(Brand + " vehicle starting...");
    }
    
    public void Stop()
    {
        Console.WriteLine("Vehicle stopping.");
    }
}

// DERIVED CLASS - inherits from Vehicle
class Car : Vehicle  // ':' means 'inherits from'
{
    public int Doors;  // NEW feature
    public string Model;
    
    public void OpenTrunk()
    {
        Console.WriteLine("Trunk opened!");
    }
}

class Motorcycle : Vehicle
{
    public bool HasSidecar;
    
    public void Wheelie()
    {
        Console.WriteLine("Doing a wheelie!");
    }
}

// Usage
Car myCar = new Car();
myCar.Brand = "Toyota";  // Inherited from Vehicle!
myCar.Year = 2020;       // Inherited from Vehicle!
myCar.Doors = 4;         // Car's own property
myCar.Start();           // Inherited method
myCar.OpenTrunk();       // Car's own method

Motorcycle bike = new Motorcycle();
bike.Brand = "Harley";   // Also inherited from Vehicle
bike.Start();            // Inherited
bike.Wheelie();          // Motorcycle's own method
```
