---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Basic while loop - count to 5
let count = 0;

while (count < 5) {
  console.log('Count: ' + count);
  count++;  // IMPORTANT: Don't forget to update!
}

// Practical example: password attempts
let password = 'secret';
let userInput = 'wrong';
let attempts = 0;
let maxAttempts = 3;

while (userInput !== password && attempts < maxAttempts) {
  console.log('Attempt ' + (attempts + 1) + ': Incorrect password');
  // In a real app, you'd ask for input here
  // For this example, we'll just increment
  attempts++;
  if (attempts === 2) {
    userInput = 'secret';  // Correct on 3rd try
  }
}

if (userInput === password) {
  console.log('Access granted!');
} else {
  console.log('Too many failed attempts');
}

// Countdown example
let countdown = 5;
while (countdown > 0) {
  console.log(countdown);
  countdown--;
}
console.log('Liftoff!');
```
