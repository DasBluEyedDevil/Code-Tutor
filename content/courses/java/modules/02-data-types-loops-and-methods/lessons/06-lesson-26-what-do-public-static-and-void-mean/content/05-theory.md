---
type: "THEORY"
title: "Part 3: Understanding 'static'"
---

'static' means the method belongs to the CLASS, not to individual objects.

STATIC METHOD (class-level, no object needed):

public static int add(int a, int b) {
    return a + b;
}

// Call directly on class:
int result = MathUtils.add(5, 3);  // No object needed!

NON-STATIC METHOD (instance-level, needs an object):

public class Dog {
    String name;
    
    public void bark() {  // NO 'static'
        System.out.println(name + " says Woof!");
    }
}

// Must create object first:
Dog myDog = new Dog();
myDog.name = "Buddy";
myDog.bark();  // "Buddy says Woof!"

WHEN TO USE STATIC:
✓ Utility methods (Math.sqrt, Integer.parseInt)
✓ Methods that don't need object data
✓ main() method (entry point)

WHEN TO USE NON-STATIC:
✓ Methods that work with object data
✓ Behavior specific to an instance
✓ Most methods in classes you create