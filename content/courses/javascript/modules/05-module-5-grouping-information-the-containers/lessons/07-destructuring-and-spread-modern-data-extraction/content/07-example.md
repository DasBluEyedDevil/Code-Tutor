---
type: "EXAMPLE"
title: "Rest Pattern in Destructuring"
---

The rest pattern (...) collects 'the rest' of the properties or elements that weren't explicitly destructured. It's the opposite of spread - instead of spreading out, it gathers up.

```javascript
// === REST WITH OBJECTS ===
let user = {
  id: 1,
  name: 'Alice',
  email: 'alice@test.com',
  role: 'admin',
  lastLogin: '2024-01-15'
};

// Extract specific properties, collect the rest
let { id, name, ...otherProps } = user;
console.log(id);          // 1
console.log(name);        // 'Alice'
console.log(otherProps);  // { email: 'alice@test.com', role: 'admin', lastLogin: '2024-01-15' }

// PRACTICAL: Omit sensitive fields
let userData = {
  username: 'bob',
  password: 'secret',
  token: 'abc123',
  profile: { name: 'Bob Smith' }
};

// Remove password and token, keep the rest
let { password, token, ...safeData } = userData;
console.log(safeData);  // { username: 'bob', profile: { name: 'Bob Smith' } }

// === REST WITH ARRAYS ===
let numbers = [1, 2, 3, 4, 5, 6, 7];

// Get first two, collect rest
let [first, second, ...rest] = numbers;
console.log(first);   // 1
console.log(second);  // 2
console.log(rest);    // [3, 4, 5, 6, 7]

// Get first and last (using rest creatively)
let scores = [95, 87, 92, 78, 85];
let [highest, ...middle] = scores;
let lowest = middle.pop();  // Get last from rest
console.log(highest);  // 95
console.log(lowest);   // 85
console.log(middle);   // [87, 92, 78]

// === REST MUST BE LAST ===
// These are SYNTAX ERRORS:
// let { ...rest, name } = obj;     // Error!
// let [...rest, last] = array;     // Error!
// Rest pattern MUST be the last element

// === COMBINING REST AND DEFAULTS ===
let config = { debug: true };

let { debug = false, verbose = false, ...options } = config;
console.log(debug);    // true (from object)
console.log(verbose);  // false (default)
console.log(options);  // {} (nothing else left)

// === PRACTICAL: Function Options Pattern ===
function createUser({ name, email, ...options }) {
  console.log('Creating user:', name);
  console.log('Email:', email);
  console.log('Additional options:', options);
  
  return {
    name,
    email,
    createdAt: new Date(),
    ...options  // Include all extra options
  };
}

let newUser = createUser({
  name: 'Charlie',
  email: 'c@test.com',
  role: 'editor',
  department: 'Marketing',
  verified: true
});

console.log(newUser);
// { name: 'Charlie', email: 'c@test.com', createdAt: Date, role: 'editor', department: 'Marketing', verified: true }
```
