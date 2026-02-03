---
type: "THEORY"
title: "Parameters and Return Values"
---

PARAMETERS: Inputs to the method

public static void greet(String name) {
    IO.println("Hello, " + name + "!");
}

greet("Alice");  // Prints: Hello, Alice!
greet("Bob");    // Prints: Hello, Bob!

'name' is a parameter—a variable that gets its value when you call the method.

RETURN VALUES: Outputs from the method

public static int square(int number) {
    return number * number;
}

int result = square(5);  // result is 25
int x = square(10);      // x is 100

The 'return' keyword sends the value back to wherever the method was called.

Think of it like:
- Parameters = ingredients you give to a chef
- Return value = the dish the chef gives back