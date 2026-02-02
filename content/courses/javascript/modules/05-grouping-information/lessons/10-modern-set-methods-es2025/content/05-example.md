---
type: "EXAMPLE"
title: "difference() - Finding Unique Items"
---

The difference() method returns items that are in the first Set but NOT in the second. The order matters - A.difference(B) gives different results than B.difference(A).

```javascript
// difference() returns items in the first set but NOT in the second
// Like 'people only invited to MY party, not yours'

let myFriends = new Set(['Alice', 'Bob', 'Charlie', 'Diana']);
let yourFriends = new Set(['Charlie', 'Diana', 'Eve', 'Frank']);

let onlyMyFriends = myFriends.difference(yourFriends);
console.log(onlyMyFriends);  // Set { 'Alice', 'Bob' }

let onlyYourFriends = yourFriends.difference(myFriends);
console.log(onlyYourFriends);  // Set { 'Eve', 'Frank' }
// Order matters! A.difference(B) !== B.difference(A)

// Practical: Find missing items
let required = new Set(['name', 'email', 'password', 'age']);
let provided = new Set(['name', 'email']);

let missing = required.difference(provided);
console.log(missing);  // Set { 'password', 'age' }
console.log('Missing fields:', [...missing].join(', '));
// 'Missing fields: password, age'

// Find items to remove from cart
let cartBefore = new Set(['apple', 'banana', 'cherry', 'date']);
let cartAfter = new Set(['apple', 'cherry']);

let removed = cartBefore.difference(cartAfter);
console.log('Removed:', removed);  // Set { 'banana', 'date' }
```
