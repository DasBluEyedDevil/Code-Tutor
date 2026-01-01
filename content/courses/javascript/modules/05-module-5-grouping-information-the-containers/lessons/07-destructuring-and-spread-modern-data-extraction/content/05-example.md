---
type: "EXAMPLE"
title: "Spread Operator with Objects"
---

The spread operator (...) 'spreads out' the properties of an object. It's commonly used for copying objects and merging multiple objects together. Important: spread creates a SHALLOW copy, not a deep copy.

```javascript
// === COPYING OBJECTS ===
let original = { name: 'Alice', age: 28 };

// Shallow copy using spread
let copy = { ...original };
console.log(copy);  // { name: 'Alice', age: 28 }

// Proof it's a copy, not a reference
copy.name = 'Bob';
console.log(original.name);  // 'Alice' (unchanged!)
console.log(copy.name);      // 'Bob'

// === MERGING OBJECTS ===
let defaults = { theme: 'light', language: 'en', fontSize: 14 };
let userPrefs = { theme: 'dark', fontSize: 18 };

// Merge: userPrefs overrides defaults
let settings = { ...defaults, ...userPrefs };
console.log(settings);
// { theme: 'dark', language: 'en', fontSize: 18 }

// ORDER MATTERS! Later spreads override earlier ones
let result1 = { ...defaults, ...userPrefs };  // userPrefs wins
let result2 = { ...userPrefs, ...defaults };  // defaults wins
console.log(result1.theme);  // 'dark' (from userPrefs)
console.log(result2.theme);  // 'light' (from defaults)

// === ADDING/OVERRIDING PROPERTIES ===
let person = { name: 'Charlie', age: 30 };

// Add new properties
let personWithEmail = { ...person, email: 'charlie@test.com' };
console.log(personWithEmail);
// { name: 'Charlie', age: 30, email: 'charlie@test.com' }

// Override existing property
let olderPerson = { ...person, age: 31 };
console.log(olderPerson);  // { name: 'Charlie', age: 31 }

// Combined: copy, override, and add
let updatedPerson = {
  ...person,
  age: 31,           // Override
  city: 'Boston'     // Add new
};
console.log(updatedPerson);
// { name: 'Charlie', age: 31, city: 'Boston' }

// === SHALLOW COPY WARNING ===
// Spread only copies one level deep!
let user = {
  name: 'Diana',
  address: { city: 'NYC', zip: '10001' }  // Nested object
};

let userCopy = { ...user };
userCopy.name = 'Eve';              // OK - doesn't affect original
userCopy.address.city = 'Boston';   // PROBLEM - affects original!

console.log(user.name);          // 'Diana' (unchanged)
console.log(user.address.city);  // 'Boston' (CHANGED!)

// The nested 'address' object is the SAME reference in both!
// For deep copies, you need:
let deepCopy = JSON.parse(JSON.stringify(user));
// Or use structuredClone() in modern environments:
// let deepCopy = structuredClone(user);

// === PRACTICAL: Immutable Updates (React Pattern) ===
let state = {
  user: { name: 'Frank', score: 100 },
  settings: { theme: 'dark' }
};

// Update score without mutating original state
let newState = {
  ...state,
  user: {
    ...state.user,
    score: state.user.score + 10
  }
};

console.log(state.user.score);     // 100 (unchanged)
console.log(newState.user.score);  // 110

// === PRACTICAL: Removing Properties (Rest Pattern) ===
let fullUser = { id: 1, name: 'Grace', password: 'secret123', email: 'g@test.com' };

// Remove password before sending to client
let { password, ...safeUser } = fullUser;
console.log(safeUser);  // { id: 1, name: 'Grace', email: 'g@test.com' }
// password is extracted but not used - effectively removed
```
