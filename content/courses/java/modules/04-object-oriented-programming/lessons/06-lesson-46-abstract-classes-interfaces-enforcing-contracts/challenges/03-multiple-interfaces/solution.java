// Solution: Multiple Interfaces
// This demonstrates implementing multiple interfaces

interface Drivable {
    String drive();
}

interface Refuelable {
    String refuel();
}

class Car implements Drivable, Refuelable {
    // Implement drive() from Drivable interface
    @Override
    public String drive() {
        return "Driving car";
    }
    
    // Implement refuel() from Refuelable interface
    @Override
    public String refuel() {
        return "Refueling car";
    }
}