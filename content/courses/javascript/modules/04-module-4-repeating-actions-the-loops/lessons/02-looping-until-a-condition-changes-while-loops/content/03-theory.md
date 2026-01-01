---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Structure of a while loop:

while (condition) {
       │
       └─ Keep looping while this is true
  // Code to repeat
  // MUST update something that affects the condition!
}

How it works:
1. Check the condition
2. If true: run the loop body, then go back to step 1
3. If false: exit the loop

Critical difference from for loop:
- for loop: Use when you KNOW how many times to loop
  for (let i = 0; i < 10; i++) { }  // Exactly 10 times

- while loop: Use when you DON'T know how many times
  while (notFullYet) { }  // Until it's full (who knows how long?)

**WARNING**: You MUST change something in the loop that affects the condition, or you'll create an infinite loop!

Good (will eventually stop):
let x = 0;
while (x < 5) {
  console.log(x);
  x++;  // x changes, will eventually reach 5
}

BAD (infinite loop!):
let x = 0;
while (x < 5) {
  console.log(x);  // x never changes!
  // Loop runs forever!
}

Common while loop patterns:
- Keep trying until success
- Process until data runs out
- Wait for a condition to change