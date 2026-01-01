---
type: "EXAMPLE"
title: "The for...of Loop"
---

```javascript
// 1. Looping through a list of names (Array)
const guests = ['Alice', 'Bob', 'Charlie', 'Diana'];

console.log("Guest List:");
for (const guest of guests) {
    // In each lap, 'guest' becomes the next name in the list
    console.log(`- ${guest}`);
}

// 2. Calculating a total from a list
const prices = [10.99, 5.50, 20.00, 3.25];
let total = 0;

for (const price of prices) {
    total = total + price;
}
console.log(`Total price: $${total}`);

// 3. Looping through characters in a string
const word = "BUN";
console.log("Spelling it out:");
for (const char of word) {
    console.log(char);
}
```