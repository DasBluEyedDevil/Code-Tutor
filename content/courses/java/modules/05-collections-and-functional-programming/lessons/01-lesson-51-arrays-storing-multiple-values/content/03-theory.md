---
type: "THEORY"
title: "Array Syntax"
---

Creating an array:

// Method 1: Declare size, fill later
int[] numbers = new int[5];
numbers[0] = 10;
numbers[1] = 20;

// Method 2: Declare with values
int[] numbers = {10, 20, 30, 40, 50};

Accessing elements:
IO.println(numbers[0]);  // 10
IO.println(numbers[4]);  // 50

Array length:
numbers.length  // 5

Looping through array:
for (int i = 0; i < numbers.length; i++) {
    IO.println(numbers[i]);
}