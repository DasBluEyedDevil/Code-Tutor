// Solution: Create Vehicle Hierarchy
// This demonstrates inheritance with extends and super

class Vehicle {
    String brand;
    int year;
    
    // Constructor
    public Vehicle(String brand, int year) {
        this.brand = brand;
        this.year = year;
    }
    
    // Method to get vehicle info
    public String getInfo() {
        return brand + " (" + year + ")";
    }
}

class Car extends Vehicle {
    int doors;
    
    // Constructor calls super to initialize parent fields
    public Car(String brand, int year, int doors) {
        super(brand, year);  // Call Vehicle constructor
        this.doors = doors;
    }
    // Car inherits getInfo() from Vehicle automatically
}