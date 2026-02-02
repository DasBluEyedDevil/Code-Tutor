---
type: "EXAMPLE"
title: "The For Loop in Action"
---

```javascript
// 1. A basic loop (Counting up)
// Start at 0; run as long as i < 5; add 1 each time
for (let i = 0; i < 5; i++) {
    console.log(`Lap number: ${i}`);
}

// 2. Counting down
console.log("Blast off in...");
for (let i = 5; i > 0; i--) {
    console.log(i);
}
console.log("LIFTOFF!");

// 3. Skipping numbers (Step by 2)
for (let i = 0; i <= 10; i = i + 2) {
    console.log(`Even number: ${i}`);
}

// 4. Using the loop variable in a calculation
for (let i = 1; i <= 5; i++) {
    console.log(`${i} squared is: ${i * i}`);
}
```