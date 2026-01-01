---
type: "EXAMPLE"
title: "Optional Chaining (?.) Examples"
---

Optional chaining (?.) lets you safely access nested properties without checking each level for null or undefined. If any part of the chain is null or undefined, it returns undefined instead of throwing an error.

```javascript
// The problem: accessing nested properties that might not exist
let user = {
  name: 'Alice',
  address: {
    city: 'New York'
  }
};

// Old way - tedious and error-prone
let city;
if (user && user.address && user.address.city) {
  city = user.address.city;
}
console.log(city);  // 'New York'

// With Optional Chaining - clean and safe!
let city2 = user?.address?.city;
console.log(city2);  // 'New York'

// When property doesn't exist
let user2 = { name: 'Bob' };  // No address!
let city3 = user2?.address?.city;
console.log(city3);  // undefined (no error!)

// Without ?. this would crash:
// let city4 = user2.address.city;  // ERROR: Cannot read 'city' of undefined

// Works with methods too!
let calculator = {
  add: (a, b) => a + b
};
console.log(calculator.add?.(2, 3));       // 5
console.log(calculator.subtract?.(5, 2));  // undefined (method doesn't exist)

// Works with arrays!
let users = [{ name: 'Alice' }, { name: 'Bob' }];
console.log(users?.[0]?.name);  // 'Alice'
console.log(users?.[5]?.name);  // undefined (index 5 doesn't exist)

let noUsers = null;
console.log(noUsers?.[0]?.name);  // undefined (noUsers is null)
```
