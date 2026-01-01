---
type: "THEORY"
title: "The Problem: Anyone Can Mess With Your Objects"
---

Remember our Car class?

class Car {
    String model;
    int speed;
}

What if someone does this?

Car myCar = new Car();
myCar.speed = -500;  // Negative speed?!
myCar.speed = 999999;  // Impossible speed!

Right now, ANYONE can directly change our object's fields to nonsensical values.
We need CONTROL over how our data gets accessed and modified.

This is where ENCAPSULATION comes in.