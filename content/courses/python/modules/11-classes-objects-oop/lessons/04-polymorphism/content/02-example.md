---
type: "EXAMPLE"
title: "Code Example: Polymorphism in Action"
---

**Polymorphism demonstrated:**

**1. Method Overriding:**
- Each shape implements `area()` and `perimeter()` differently
- Same method names, different calculations

**2. Uniform Interface:**
- `print_shape_info()` works with any shape
- Doesn't care about specific type
- Just calls `area()` and `perimeter()`

**3. Duck Typing:**
- `Square` doesn't inherit from `Shape`
- But has required methods
- Works perfectly with polymorphic functions!

**Key insight:** Code to interfaces, not implementations

```python
# Base class with common interface
class Shape:
    def __init__(self, name):
        self.name = name
    
    def area(self):
        raise NotImplementedError("Subclass must implement area()")
    
    def perimeter(self):
        raise NotImplementedError("Subclass must implement perimeter()")
    
    def describe(self):
        return f"{self.name}: Area = {self.area()}, Perimeter = {self.perimeter()}"

# Different shapes, same interface
class Circle(Shape):
    def __init__(self, radius):
        super().__init__("Circle")
        self.radius = radius
    
    def area(self):
        return 3.14159 * self.radius ** 2
    
    def perimeter(self):
        return 2 * 3.14159 * self.radius

class Rectangle(Shape):
    def __init__(self, width, height):
        super().__init__("Rectangle")
        self.width = width
        self.height = height
    
    def area(self):
        return self.width * self.height
    
    def perimeter(self):
        return 2 * (self.width + self.height)

class Triangle(Shape):
    def __init__(self, base, height, side1, side2, side3):
        super().__init__("Triangle")
        self.base = base
        self.height = height
        self.sides = [side1, side2, side3]
    
    def area(self):
        return 0.5 * self.base * self.height
    
    def perimeter(self):
        return sum(self.sides)

# Polymorphic function - works with any Shape
def print_shape_info(shape):
    """Works with any object that has area() and perimeter()"""
    print(f"{shape.name}:")
    print(f"  Area: {shape.area():.2f}")
    print(f"  Perimeter: {shape.perimeter():.2f}")
    print()

def calculate_total_area(shapes):
    """Works with list of any shapes"""
    total = sum(shape.area() for shape in shapes)
    return total

# Create different shapes
print("=== Creating Shapes ===")
circle = Circle(5)
rectangle = Rectangle(4, 6)
triangle = Triangle(base=3, height=4, side1=3, side2=4, side3=5)

# Polymorphism: same method, different behavior
print("\n=== Polymorphism: Each shape implements methods differently ===")
print_shape_info(circle)
print_shape_info(rectangle)
print_shape_info(triangle)

# Polymorphism: treating different types uniformly
print("=== Treating Different Shapes Uniformly ===")
shapes = [circle, rectangle, triangle]

for shape in shapes:
    print(shape.describe())

print(f"\nTotal area of all shapes: {calculate_total_area(shapes):.2f}")

# Duck typing: if it has the methods, it works!
print("\n=== Duck Typing ===")

class Square:  # Doesn't inherit from Shape!
    def __init__(self, side):
        self.name = "Square"
        self.side = side
    
    def area(self):
        return self.side ** 2
    
    def perimeter(self):
        return 4 * self.side

square = Square(5)
print_shape_info(square)  # Works! Has area() and perimeter()

# Add to shapes list
shapes.append(square)
print(f"Total area including square: {calculate_total_area(shapes):.2f}")
```
