---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`class Car : Vehicle`**: The colon ':' means 'inherits from'. Car is the DERIVED class, Vehicle is the BASE class. Car gets ALL of Vehicle's members automatically!

**`Inherited members`**: Car automatically has Brand, Year, Start(), Stop() even though you didn't write them! They come from Vehicle. You can use them immediately on Car objects.

**`Adding new members`**: Derived classes can ADD new fields, properties, and methods. Car adds Doors and OpenTrunk(). Base class (Vehicle) doesn't have these!

**`Single inheritance`**: In C#, a class can only inherit from ONE base class! Can't do 'class Car : Vehicle, Machine'. But you can chain: Vehicle → Car → SportsCar.