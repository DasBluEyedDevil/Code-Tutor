---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// AND operator (&&) - ALL must be true
let hasTicket = true;
let hasTime = true;
let isShowing = true;

if (hasTicket && hasTime && isShowing) {
  console.log('You can watch the movie!');  // All true, this runs
}

let age = 25;
let hasLicense = true;

if (age >= 16 && hasLicense) {
  console.log('You can drive');  // Both true, this runs
}

// OR operator (||) - AT LEAST ONE must be true
let isWeekend = true;
let isHoliday = false;

if (isWeekend || isHoliday) {
  console.log('You can sleep in!');  // One is true, this runs
}

let temperature = 95;
if (temperature > 90 || temperature < 32) {
  console.log('Extreme weather!');  // First condition true, runs
}

// NOT operator (!) - flips true/false
let isRaining = false;

if (!isRaining) {
  console.log('No umbrella needed');  // !false = true, runs
}

// Combining operators
let hour = 14;
let isWeekday = true;

if ((hour >= 9 && hour <= 17) && isWeekday) {
  console.log('Office is open');
}
```
