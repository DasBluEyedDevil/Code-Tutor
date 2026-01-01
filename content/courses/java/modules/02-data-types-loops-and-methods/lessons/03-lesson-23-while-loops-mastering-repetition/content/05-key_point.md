---
type: "KEY_POINT"
title: "When to Use While Loops"
---

Use a while loop when:

✓ You DON'T know how many times to repeat in advance
   - "Keep asking until user enters valid input"
   - "Keep running game until player loses all lives"

✓ The repetition depends on a condition
   - while (userIsLoggedIn) { ... }
   - while (hasMoreData) { ... }

Example patterns:

// Sum numbers from 1 to 10
int sum = 0;
int n = 1;
while (n <= 10) {
    sum += n;  // Add n to sum
    n++;       // Move to next number
}
// sum is now 55

// Countdown
int countdown = 5;
while (countdown > 0) {
    System.out.println(countdown);
    countdown--;
}
System.out.println("Blast off!");
// Prints: 5 4 3 2 1 Blast off!