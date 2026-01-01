// Solution: Polymorphic Shape Areas
// This demonstrates polymorphism with different shape calculations

class Shape {
    public double getArea() {
        return 0.0;
    }
}

class Circle extends Shape {
    double radius;
    
    public Circle(double radius) {
        this.radius = radius;
    }
    
    // Override getArea to calculate circle area: PI * r^2
    @Override
    public double getArea() {
        return Math.PI * radius * radius;
    }
}

class Rectangle extends Shape {
    int width, height;
    
    public Rectangle(int width, int height) {
        this.width = width;
        this.height = height;
    }
    
    // Override getArea to calculate rectangle area: width * height
    @Override
    public double getArea() {
        return width * height;
    }
}