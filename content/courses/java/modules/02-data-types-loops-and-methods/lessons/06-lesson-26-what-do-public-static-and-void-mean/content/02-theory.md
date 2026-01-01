---
type: "THEORY"
title: "Part 1: Understanding 'void' vs Return Types"
---

'void' is NOT a type like 'int' or 'String'. It means "returns NOTHING".

VOID METHOD (performs action, returns nothing):

public static void sayHello() {
    System.out.println("Hello!");
    // NO return statement
}

sayHello();  // Just does something, doesn't give back a value

RETURNING METHOD (calculates and returns value):

public static int add(int a, int b) {
    return a + b;  // MUST return an int
}

int result = add(5, 3);  // result gets the value 8

KEY DIFFERENCE:
- void = "do something" (print, save, modify)
- int/String/double/etc = "calculate and give me back a value"

Think of it like asking someone to do something:
- "Clean your room!" (void - just do it, no result)
- "What's 2 + 2?" (returns int - I need an answer back)