---
type: "EXAMPLE"
title: "The While and Do-While Loops"
---

```javascript
// 1. A basic while loop
let health = 100;

while (health > 0) {
    console.log(`Still fighting! Health: ${health}`);
    // Simulate taking damage
    health = health - 25;
}
console.log("Game Over!");

// 2. Looping with a Flag (Boolean)
let isSearching = true;
let attempts = 0;

while (isSearching) {
    attempts++;
    console.log(`Search attempt #${attempts}...`);
    
    if (attempts === 3) {
        console.log("Found the item!");
        isSearching = false; // This breaks the loop next time it checks
    }
}

// 3. The Do-While Loop
// This runs at least ONCE, even if the condition is already false
let energy = 0;
do {
    console.log("Applying emergency battery boost...");
    energy = energy + 10;
} while (energy < 0);
```