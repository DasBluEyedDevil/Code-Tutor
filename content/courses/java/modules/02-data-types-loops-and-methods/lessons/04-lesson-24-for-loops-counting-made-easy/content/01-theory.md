---
type: "THEORY"
title: "The Problem: While Loops for Counting Feel Clunky"
---

You learned to count with a while loop:

int i = 1;
while (i <= 10) {
    System.out.println(i);
    i++;
}

This works, but notice the pattern:
1. INITIALIZE a counter (int i = 1)
2. CHECK a condition (i <= 10)
3. UPDATE the counter (i++)

These three pieces are scattered across 4 lines! When you're just COUNTING, this feels messy.

Java provides a cleaner syntax for this exact pattern: the FOR LOOP.

The same code with a for loop:

for (int i = 1; i <= 10; i++) {
    System.out.println(i);
}

All three pieces (init, condition, update) are on ONE line!