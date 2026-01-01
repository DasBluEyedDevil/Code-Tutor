// Solution: Vehicle Start Methods
// This demonstrates polymorphism with vehicle start methods

class Vehicle {
    // Base start method
    public String start() {
        return "Starting vehicle";
    }
}

class Car extends Vehicle {
    // Override start for Car behavior
    @Override
    public String start() {
        return "Turning key, engine starts";
    }
}

class Motorcycle extends Vehicle {
    // Override start for Motorcycle behavior
    @Override
    public String start() {
        return "Kick-starting engine";
    }
}