interface Drivable {
    String drive();
}

interface Refuelable {
    String refuel();
}

class Car implements Drivable, Refuelable {
    // Your code here
}