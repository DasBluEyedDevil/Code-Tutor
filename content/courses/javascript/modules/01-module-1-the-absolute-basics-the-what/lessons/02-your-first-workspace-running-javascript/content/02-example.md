---
type: "EXAMPLE"
title: "The Code Environment"
---

JavaScript runs from top to bottom. Every line is executed in order, allowing you to perform calculations and display multiple messages in a sequence.

```javascript
// 1. Performing Calculations
// The computer calculates the result first, then logs it.
console.log(10 + 20); // Result: 30

// 2. Sequential Execution
console.log('Step 1: Gathering ingredients');
console.log('Step 2: Mixing the batter');
console.log('Step 3: Baking the cake');

// 3. Combining Text and Math (Template Literals)
// We use backticks (`) and ${...} to insert calculations into text.
console.log(`Total score: ${500 + 45}`);

// Note: The calculation inside ${...} happens BEFORE it gets displayed.
```

### Expected Output

```
30
Step 1: Gathering ingredients
Step 2: Mixing the batter
Step 3: Baking the cake
Total score: 545
```
