---
type: "EXAMPLE"
title: "intersection() - Finding Common Items"
---

The intersection() method returns only the items that exist in BOTH Sets. It's like finding people who were invited to both parties - the overlap in a Venn diagram.

```javascript
// intersection() returns items that exist in BOTH sets
// Like 'people invited to BOTH parties'

let frontend = new Set(['Alice', 'Bob', 'Charlie']);
let backend = new Set(['Charlie', 'Diana', 'Eve']);

let fullStack = frontend.intersection(backend);
console.log(fullStack);  // Set { 'Charlie' }
// Only Charlie is in both teams!

// Practical: Find common interests
let aliceHobbies = new Set(['reading', 'gaming', 'hiking', 'cooking']);
let bobHobbies = new Set(['gaming', 'music', 'hiking', 'sports']);

let commonHobbies = aliceHobbies.intersection(bobHobbies);
console.log(commonHobbies);  // Set { 'gaming', 'hiking' }

// Find users with both permissions
let canRead = new Set(['user1', 'user2', 'user3', 'admin']);
let canWrite = new Set(['user2', 'admin', 'editor']);

let canReadAndWrite = canRead.intersection(canWrite);
console.log(canReadAndWrite);  // Set { 'user2', 'admin' }

// OLD WAY (before ES2025):
let oldIntersection = new Set(
  [...aliceHobbies].filter(x => bobHobbies.has(x))
);
// New way is much cleaner!
```
