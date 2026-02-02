// Solution: Create a Sealed Shape Hierarchy

sealed interface Shape permits Circle, Square {}

record Circle(double radius) implements Shape {}
record Square(double side) implements Shape {}

double calculateArea(Shape shape) {
    return switch (shape) {
        case Circle(double r) -> Math.PI * r * r;
        case Square(double s) -> s * s;
    };
}

void main() {
    Shape circle = new Circle(5);
    // Format to 2 decimal places
    IO.println(String.format("%.2f", calculateArea(circle)));
}