---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`public int Add(int a, int b)`**: Method signature: access modifier (public), return type (int), name (Add), parameters (int a, int b). This method takes two ints and returns an int.

**`return a + b;`**: The 'return' keyword sends a value back to the caller. The type MUST match the method's return type (int here). return exits the method immediately!

**`public void DisplayResult(int result)`**: 'void' means 'returns nothing'. Use void for methods that DO something but don't need to send a value back. No return statement needed (or use 'return;' to exit early).

**`Method overloading`**: You can have multiple methods with the SAME NAME but DIFFERENT parameters: Add(int, int), Add(double, double), Add(int, int, int). C# picks the right one based on arguments!