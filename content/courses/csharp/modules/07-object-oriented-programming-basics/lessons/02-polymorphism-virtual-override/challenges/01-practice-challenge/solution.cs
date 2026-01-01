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
    
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public override void Display()
    {
        Console.WriteLine("Circle with radius " + Radius);
    }
}

class Rectangle : Shape
{
    public double Width;
    public double Height;
    
    public override double CalculateArea()
    {
        return Width * Height;
    }
    
    public override void Display()
    {
        Console.WriteLine("Rectangle " + Width + "x" + Height);
    }
}

Shape[] shapes = new Shape[2];
shapes[0] = new Circle() { Radius = 5 };
shapes[1] = new Rectangle() { Width = 4, Height = 6 };

foreach (Shape shape in shapes)
{
    shape.Display();
    Console.WriteLine("Area: " + shape.CalculateArea());
}