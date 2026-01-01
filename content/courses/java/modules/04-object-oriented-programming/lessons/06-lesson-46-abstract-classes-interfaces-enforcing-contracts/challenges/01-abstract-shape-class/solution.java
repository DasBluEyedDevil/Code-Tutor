// Solution: Abstract Shape Class
// This demonstrates abstract classes with required method implementations

abstract class Shape {
    // Protected field accessible to subclasses
    String color;
    
    // Constructor
    public Shape(String color) {
        this.color = color;
    }
    
    // Abstract method - must be implemented by subclasses
    public abstract double getArea();
}

class Circle extends Shape {
    double radius;
    
    // Constructor calls super to set color and sets radius
    public Circle(String color, double radius) {
        super(color);  // Call Shape constructor
        this.radius = radius;
    }
    
    // Implement the abstract method
    @Override
    public double getArea() {
        return Math.PI * radius * radius;
    }
}