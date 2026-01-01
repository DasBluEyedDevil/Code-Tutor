# Temperature Class with Properties
# This solution demonstrates properties for encapsulation

class Temperature:
    """Temperature class with automatic unit conversion."""
    
    ABSOLUTE_ZERO = -273.15  # In Celsius
    
    def __init__(self, celsius=0):
        """Initialize temperature in Celsius."""
        self.celsius = celsius  # Uses the setter for validation
    
    @property
    def celsius(self):
        """Get temperature in Celsius."""
        return self._celsius
    
    @celsius.setter
    def celsius(self, value):
        """Set temperature in Celsius with validation."""
        if value < self.ABSOLUTE_ZERO:
            raise ValueError(f"Temperature cannot be below absolute zero ({self.ABSOLUTE_ZERO}C)")
        self._celsius = value
    
    @property
    def fahrenheit(self):
        """Get temperature in Fahrenheit (computed)."""
        return (self._celsius * 9/5) + 32
    
    @fahrenheit.setter
    def fahrenheit(self, value):
        """Set temperature via Fahrenheit."""
        self.celsius = (value - 32) * 5/9  # Converts and validates
    
    @property
    def kelvin(self):
        """Get temperature in Kelvin (read-only)."""
        return self._celsius + 273.15
    
    def __str__(self):
        return f"{self._celsius:.2f}C / {self.fahrenheit:.2f}F / {self.kelvin:.2f}K"

# Test the Temperature class
print("=== Temperature Class Demo ===")

# Create temperature object
temp = Temperature(25)  # 25 Celsius
print(f"\nInitial: {temp}")

# Read properties
print(f"\nCelsius: {temp.celsius}C")
print(f"Fahrenheit: {temp.fahrenheit}F")
print(f"Kelvin: {temp.kelvin}K")

# Modify via Celsius
temp.celsius = 100
print(f"\nAfter setting 100C: {temp}")

# Modify via Fahrenheit
temp.fahrenheit = 32
print(f"After setting 32F: {temp}")

# Test validation
print("\nTesting validation:")
try:
    temp.celsius = -300  # Below absolute zero
except ValueError as e:
    print(f"  Error: {e}")

# Create at absolute zero
print("\nAt absolute zero:")
cold = Temperature(-273.15)
print(f"  {cold}")