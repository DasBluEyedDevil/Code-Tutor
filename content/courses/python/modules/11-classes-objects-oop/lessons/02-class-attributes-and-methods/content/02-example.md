---
type: "EXAMPLE"
title: "Code Example: Instance vs Class Attributes"
---

**Key differences:**

**Instance Attributes:**
- Defined with `self.attribute` in `__init__`
- Unique to each object
- Access with `self.attribute` or `obj.attribute`

**Class Attributes:**
- Defined directly in class body
- Shared by all instances
- Access with `ClassName.attribute` or `cls.attribute`

**Method Types:**
- `def method(self):` - Instance method
- `@classmethod def method(cls):` - Class method
- `@staticmethod def method():` - Static method

```python
class Car:
    # Class attributes (shared by all cars)
    manufacturer = "Toyota"
    warranty_years = 3
    total_cars = 0  # Track how many cars created
    
    def __init__(self, model, color, price):
        # Instance attributes (unique to each car)
        self.model = model
        self.color = color
        self.price = price
        self.mileage = 0
        
        # Increment class attribute
        Car.total_cars += 1
    
    # Instance method (works with specific car)
    def drive(self, miles):
        self.mileage += miles
        return f"{self.color} {self.model} drove {miles} miles. Total: {self.mileage}"
    
    def info(self):
        return f"{Car.manufacturer} {self.model} ({self.color}) - ${self.price} - {self.mileage} miles"
    
    # Class method (works with the class)
    @classmethod
    def from_string(cls, car_string):
        """Alternative constructor from string 'Model,Color,Price'"""
        model, color, price = car_string.split(',')
        return cls(model, color, float(price))
    
    @classmethod
    def get_total_cars(cls):
        return f"Total {cls.manufacturer} cars created: {cls.total_cars}"
    
    # Static method (utility function)
    @staticmethod
    def is_luxury(price):
        """Check if price indicates luxury car"""
        return price > 50000

# Create cars normally
print("=== Creating Cars ===")
car1 = Car("Camry", "Blue", 28000)
car2 = Car("Corolla", "Red", 23000)
car3 = Car("Avalon", "Black", 42000)

print(car1.info())
print(car2.info())
print(car3.info())

print("\n=== Class Attributes (Shared) ===")
print(f"Manufacturer: {Car.manufacturer}")
print(f"Warranty: {Car.warranty_years} years")
print(Car.get_total_cars())

print("\n=== Instance Attributes (Unique) ===")
print(f"Car 1 color: {car1.color}")
print(f"Car 2 model: {car2.model}")
print(f"Car 3 price: ${car3.price}")

print("\n=== Using Instance Method ===")
print(car1.drive(100))
print(car1.drive(50))
print(car2.drive(200))

print("\n=== Using Class Method (Alternative Constructor) ===")
car4 = Car.from_string("RAV4,Silver,32000")
print(car4.info())
print(Car.get_total_cars())

print("\n=== Using Static Method ===")
print(f"Is $28,000 luxury? {Car.is_luxury(28000)}")
print(f"Is $42,000 luxury? {Car.is_luxury(42000)}")
print(f"Is car3 luxury? {Car.is_luxury(car3.price)}")
```
