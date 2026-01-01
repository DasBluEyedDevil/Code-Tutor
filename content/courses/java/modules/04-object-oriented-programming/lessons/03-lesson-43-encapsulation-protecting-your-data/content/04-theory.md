---
type: "THEORY"
title: "Getters and Setters: Controlled Access"
---

Instead of public fields, use private fields + public methods:

class Car {
    private int speed;  // Hidden from outside
    
    // GETTER - Read the value
    public int getSpeed() {
        return speed;
    }
    
    // SETTER - Change the value (with validation!)
    public void setSpeed(int newSpeed) {
        if (newSpeed >= 0 && newSpeed <= 200) {
            speed = newSpeed;
        } else {
            System.out.println("Invalid speed!");
        }
    }
}

Now:
Car myCar = new Car();
myCar.speed = -500;  // COMPILE ERROR! speed is private
myCar.setSpeed(-500);  // Prints "Invalid speed!", doesn't change
myCar.setSpeed(60);  // Works! Sets speed to 60
System.out.println(myCar.getSpeed());  // 60