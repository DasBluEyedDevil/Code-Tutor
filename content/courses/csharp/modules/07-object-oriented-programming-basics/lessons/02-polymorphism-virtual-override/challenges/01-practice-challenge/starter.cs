class Shape
{
    public virtual double CalculateArea()
    {
        return 0;
    }
    
    public virtual void Display()
    {
        Console.WriteLine("Generic shape");
    }
}

class Circle : Shape
{
    public double Radius;
    // Override methods
}

class Rectangle : Shape
{
    public double Width;
    public double Height;
    // Override methods
}

// Create shapes polymorphically