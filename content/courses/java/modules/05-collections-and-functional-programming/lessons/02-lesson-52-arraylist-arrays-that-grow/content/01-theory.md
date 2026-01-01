---
type: "THEORY"
title: "The Problem: Fixed-Size Arrays"
---

Remember arrays?

int[] numbers = new int[5];  // Size is FIXED at 5

Problems:
1. What if you need to add a 6th element? Can't do it!
2. What if you need to remove an element? Have to shift manually
3. No built-in methods for common operations

Example:
You're building a to-do list app. How many tasks will users have?
- Could be 0
- Could be 5
- Could be 100

You CAN'T predict the size!

Solution: ArrayList - a dynamic, resizable array!