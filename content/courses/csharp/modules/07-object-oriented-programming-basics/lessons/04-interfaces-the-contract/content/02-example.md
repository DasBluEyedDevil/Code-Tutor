---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// INTERFACE definition
interface IDrawable
{
    void Draw();  // No implementation!
    void Erase();
}

interface IResizable
{
    void Resize(int width, int height);
}

// Class implementing ONE interface
class Button : IDrawable
{
    public void Draw()
    {
        Console.WriteLine("Drawing button");
    }
    
    public void Erase()
    {
        Console.WriteLine("Erasing button");
    }
}

// Class implementing MULTIPLE interfaces
class Image : IDrawable, IResizable
{
    public void Draw()
    {
        Console.WriteLine("Drawing image");
    }
    
    public void Erase()
    {
        Console.WriteLine("Erasing image");
    }
    
    public void Resize(int width, int height)
    {
        Console.WriteLine("Resizing to " + width + "x" + height);
    }
}

// Polymorphism with interfaces
IDrawable[] drawable = { new Button(), new Image() };
foreach (IDrawable item in drawable)
{
    item.Draw();  // Each draws differently!
}
```
