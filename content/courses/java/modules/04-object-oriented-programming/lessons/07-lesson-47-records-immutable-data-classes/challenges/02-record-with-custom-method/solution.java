// Solution: Record with Custom Method
// Define the Rectangle record with custom method
record Rectangle(int width, int height) {
    int area() {
        return width * height;
    }
}

void main() {
    // Create a Rectangle
    var rect = new Rectangle(5, 10);
    
    // Print the area
    println(rect.area());
}