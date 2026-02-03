---
type: "WARNING"
title: "Common For Loop Pitfalls"
---

Watch out for these common mistakes:

1. OFF-BY-ONE ERRORS:
// WRONG: Starts at 1, misses index 0
for (int i = 1; i < array.length; i++)

// WRONG: Uses <=, goes past last index
for (int i = 0; i <= array.length; i++)

// CORRECT: Start at 0, use < length
for (int i = 0; i < array.length; i++)

2. INFINITE LOOPS:
// WRONG: Forgot to increment!
for (int i = 0; i < 10; ) { }

// WRONG: Incrementing wrong direction
for (int i = 10; i > 0; i++) { }

3. MODIFYING LOOP VARIABLE:
// DANGEROUS: Changing i inside loop
for (int i = 0; i < 10; i++) {
    i = i + 2;  // Skips iterations unexpectedly
}

4. STRING CONCATENATION IN LOOPS:
// SLOW: Creates new String objects each time
String result = "";
for (int i = 0; i < 1000; i++) {
    result += i;  // Use StringBuilder instead!
}

5. SCOPE OF LOOP VARIABLE:
// i only exists inside the loop
for (int i = 0; i < 5; i++) { }
IO.println(i);  // ERROR: i not defined here