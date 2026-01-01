---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ABSTRACT CLASS - can't be instantiated
abstract class Shape
{
    public string Color;  // Regular property
    
    // ABSTRACT method - no implementation!
    public abstract double CalculateArea();
    
    // ABSTRACT method
    public abstract double CalculatePerimeter();
    
    // Regular (concrete) method
    public void Display()
    {
        Console.WriteLine(Color + " shape with area: " + CalculateArea());
    }
}

// Derived class MUST implement abstract methods
class Circle : Shape
{
    public double Radius;
    
    // MUST override abstract methods
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public override double CalculatePerimeter()
    {
        return 2 * Math.PI * Radius;
    }
}

class Rectangle : Shape
{
    public double Width, Height;
    
    public override double CalculateArea()
    {
        return Width * Height;
    }
    
    public override double CalculatePerimeter()
    {
        return 2 * (Width + Height);
    }
}

// Usage
// Shape s = new Shape();  // ERROR! Can't instantiate abstract class
Shape circle = new Circle() { Radius = 5, Color = "Red" };
circle.Display();  // Inherited method works!
```
