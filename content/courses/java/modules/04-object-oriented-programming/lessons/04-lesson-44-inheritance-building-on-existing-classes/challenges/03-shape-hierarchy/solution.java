// Solution: Shape Hierarchy
// This demonstrates inheritance with constructors and methods

class Shape {
    String color;
    
    // Constructor to set color
    public Shape(String color) {
        this.color = color;
    }
}

class Rectangle extends Shape {
    int width;
    int height;
    
    // Constructor calls super to set color, then sets dimensions
    public Rectangle(String color, int width, int height) {
        super(color);  // Call Shape constructor
        this.width = width;
        this.height = height;
    }
    
    // Calculate and return the area
    public int getArea() {
        return width * height;
    }
}