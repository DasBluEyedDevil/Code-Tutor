---
type: "EXAMPLE"
title: "The Logic Chain"
---

```javascript
let score = 85;

console.log(`Your score is: ${score}`);

if (score >= 90) {
    console.log("Grade: A");
} else if (score >= 80) {
    // This runs if score < 90 AND score >= 80
    console.log("Grade: B");
} else if (score >= 70) {
    // This runs if score < 80 AND score >= 70
    console.log("Grade: C");
} else {
    // This runs if NONE of the above are true
    console.log("Grade: F");
}

// 2. The importance of order
let age = 25;
if (age > 21) {
    console.log("You can enter the club.");
} else if (age > 100) {
    // This code will NEVER run, because 101 is already > 21!
    console.log("You get a free drink for being a century old.");
}
```

**Expected Output**:

```text
Your score is: 85
Grade: B
You can enter the club.
```