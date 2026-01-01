---
type: "EXAMPLE"
title: "Breaking and Continuing"
---

```javascript
// 1. Using 'break' to exit early
console.log("Searching for the magic number (7)...");

for (let i = 1; i <= 10; i++) {
    console.log(`Checking number: ${i}`);
    
    if (i === 7) {
        console.log("Found it! Stopping the search.");
        break; // The loop ends immediately
    }
}

// 2. Using 'continue' to skip iterations
console.log("\nPrinting only odd numbers:");

for (let i = 1; i <= 10; i++) {
    // If it's an even number, skip it
    if (i % 2 === 0) {
        continue; // Jumps directly to the next i++
    }
    
    console.log(`Odd number: ${i}`);
}

// 3. Nested loop breaking (Advanced)
// Break only affects the loop it is DIRECTLY inside
for (let i = 1; i <= 3; i++) {
    for (let j = 1; j <= 3; j++) {
        if (i === 2) break; // This only stops the 'j' loop
        console.log(`i: ${i}, j: ${j}`);
    }
}
```