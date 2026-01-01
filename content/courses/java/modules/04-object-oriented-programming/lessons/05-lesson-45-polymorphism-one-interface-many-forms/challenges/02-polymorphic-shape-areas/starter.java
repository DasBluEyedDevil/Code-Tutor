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
    
    // Override getArea here
}

class Rectangle extends Shape {
    int width, height;
    
    public Rectangle(int width, int height) {
        this.width = width;
        this.height = height;
    }
    
    // Override getArea here
}