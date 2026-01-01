# Vehicle Hierarchy
# This solution demonstrates inheritance and method overriding

class Vehicle:
    """Base class for all vehicles."""
    
    def __init__(self, brand, year):
        """Initialize vehicle with brand and year."""
        self.brand = brand
        self.year = year
        self.is_running = False
    
    def start(self):
        """Start the vehicle."""
        self.is_running = True
        return f"{self.brand} is starting..."
    
    def stop(self):
        """Stop the vehicle."""
        self.is_running = False
        return f"{self.brand} has stopped."
    
    def info(self):
        """Return basic vehicle info."""
        return f"{self.year} {self.brand}"

class Car(Vehicle):
    """Car class - inherits from Vehicle."""
    
    def __init__(self, brand, year, num_doors):
        """Initialize car with doors."""
        super().__init__(brand, year)
        self.num_doors = num_doors
    
    def info(self):
        """Override: Car-specific info."""
        base_info = super().info()
        return f"{base_info} - {self.num_doors}-door car"
    
    def honk(self):
        """Car-specific method."""
        return "Beep beep!"

class Motorcycle(Vehicle):
    """Motorcycle class - inherits from Vehicle."""
    
    def __init__(self, brand, year, has_sidecar=False):
        """Initialize motorcycle with sidecar option."""
        super().__init__(brand, year)
        self.has_sidecar = has_sidecar
    
    def info(self):
        """Override: Motorcycle-specific info."""
        base_info = super().info()
        sidecar_text = "with sidecar" if self.has_sidecar else "no sidecar"
        return f"{base_info} - motorcycle ({sidecar_text})"
    
    def wheelie(self):
        """Motorcycle-specific method."""
        if self.has_sidecar:
            return "Can't wheelie with a sidecar!"
        return "Doing a wheelie!"

# Test the vehicle hierarchy
print("=== Vehicle Hierarchy Demo ===")

# Create vehicles
my_car = Car("Toyota", 2022, 4)
my_bike = Motorcycle("Harley", 2021, has_sidecar=True)
old_bike = Motorcycle("Honda", 2020)

# Test info (polymorphism)
print("\nVehicle Info:")
for vehicle in [my_car, my_bike, old_bike]:
    print(f"  - {vehicle.info()}")

# Test start/stop
print(f"\n{my_car.start()}")
print(f"{my_car.stop()}")

# Test specific methods
print(f"\nCar honk: {my_car.honk()}")
print(f"Bike with sidecar: {my_bike.wheelie()}")
print(f"Regular bike: {old_bike.wheelie()}")