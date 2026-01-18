---
type: "EXAMPLE"
title: "Comparing Values"
---

```javascript
const myScore = 80;
const passingScore = 70;

// 1. Equality and Inequality
console.log(myScore === 80); // true (exact match)
console.log(myScore !== 100); // true (they are different)

// 2. Greater Than / Less Than
console.log(myScore > 100);  // false
console.log(myScore < 100);  // true

// 3. Inclusive Comparisons (Greater/Less than OR Equal)
console.log(myScore >= 80);  // true (it is exactly 80)
console.log(myScore >= 70);  // true (it is more than 70)

// 4. Using results in an 'if'
const isWinner = myScore > passingScore;

if (isWinner) {
    console.log("Congratulations, you passed!");
}
```

**Expected Output**:

```text
true
true
false
true
true
true
Congratulations, you passed!
```