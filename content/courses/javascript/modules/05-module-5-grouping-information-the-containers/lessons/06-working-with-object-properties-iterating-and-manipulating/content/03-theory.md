---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Three key Object methods:

1. **Object.keys(obj)**
   - Returns array of property names
   - {a: 1, b: 2} → ['a', 'b']
   - Use when you need property names

2. **Object.values(obj)**
   - Returns array of property values
   - {a: 1, b: 2} → [1, 2]
   - Use when you only need values

3. **Object.entries(obj)**
   - Returns array of [key, value] pairs
   - {a: 1, b: 2} → [['a', 1], ['b', 2]]
   - Use when you need both keys and values

Loop patterns:

// Pattern 1: Loop through keys
for (let key of Object.keys(obj)) {
  console.log(key);          // Property name
  console.log(obj[key]);     // Property value
}

// Pattern 2: Loop through values
for (let value of Object.values(obj)) {
  console.log(value);  // Just the values
}

// Pattern 3: Loop through entries (destructuring)
for (let [key, value] of Object.entries(obj)) {
  console.log(key, value);  // Both at once
}

// Pattern 4: for...in loop (older)
for (let key in obj) {
  console.log(key, obj[key]);
}

Destructuring in loops:
// This:
for (let entry of Object.entries(obj)) {
  let key = entry[0];
  let value = entry[1];
}

// Can be written as:
for (let [key, value] of Object.entries(obj)) {
  // key and value are extracted automatically!
}

Counting properties:
Object.keys(obj).length  // Number of properties