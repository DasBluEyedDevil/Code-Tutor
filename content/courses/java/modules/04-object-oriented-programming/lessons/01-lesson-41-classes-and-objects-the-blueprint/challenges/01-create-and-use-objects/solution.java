// Solution: Create and Use Objects
// This demonstrates creating and using objects

// Define the Car class with fields and methods
class Car {
    String brand;
    int year;
    
    // Method that uses the brand field
    void honk() {
        IO.println(brand + " goes beep!");
    }
}

public class Solution {
    public static void main(String[] args) {
        // Step 1: Create a Car object
        Car myCar = new Car();
        
        // Step 2: Set the brand field
        myCar.brand = "Toyota";
        
        // Step 3: Set the year field
        myCar.year = 2020;
        
        // Step 4: Call the honk method
        myCar.honk();
    }
}