---
type: "THEORY"
title: "The Five Basic Arithmetic Operators"
---

1. ADDITION (+)
   int sum = 10 + 5;     // sum is 15
   int x = 7;
   int y = 3;
   int total = x + y;    // total is 10

2. SUBTRACTION (-)
   int difference = 10 - 5;  // 5
   int age = 30;
   int yearsAgo = age - 10;  // 20

3. MULTIPLICATION (*)
   int product = 10 * 5;     // 50
   int price = 20;
   int quantity = 3;
   int total = price * quantity;  // 60

4. DIVISION (/)
   int quotient = 10 / 5;    // 2
   ⚠️ WARNING: int division truncates decimals!
   int result = 10 / 3;      // 3 (not 3.333...)
   double result = 10.0 / 3; // 3.333...

5. MODULO (%) - Remainder after division
   int remainder = 10 % 3;   // 1 (10 ÷ 3 = 3 remainder 1)
   int rem = 17 % 5;         // 2 (17 ÷ 5 = 3 remainder 2)
   Use cases: Check if even (x % 2 == 0), cycle through values