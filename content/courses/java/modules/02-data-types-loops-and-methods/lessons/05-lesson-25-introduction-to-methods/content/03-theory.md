---
type: "THEORY"
title: "Method Syntax - The Basics"
---

A method has several parts:

public static RETURN_TYPE methodName(PARAMETERS) {
    // Code to run
    return result;  // If return type isn't void
}

Let's break it down:

1. 'public static' - For now, always write these (you'll understand later)
2. RETURN_TYPE - What the method gives back (int, double, String, or 'void' for nothing)
3. methodName - What you call it (use camelCase: calculateSum, printMessage)
4. PARAMETERS - Inputs in parentheses (like variables: int x, String name)
5. CODE BLOCK - What the method does
6. 'return' - Sends the result back (not needed if return type is 'void')

Example 1: Method that returns nothing (void)

public static void sayHello() {
    IO.println("Hello!");
}

Calling it:
sayHello();  // Prints: Hello!

Example 2: Method that returns a value

public static int addNumbers(int a, int b) {
    int sum = a + b;
    return sum;
}

Calling it:
int result = addNumbers(5, 3);  // result is 8