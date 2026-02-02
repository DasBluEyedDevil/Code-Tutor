---
type: "EXAMPLE"
title: "Object Literals and Access"
---

```javascript
// 1. Creating an Object
// We use curly braces { } and key:value pairs
const user = {
    username: 'CyberKnight',
    level: 42,
    isOnline: true,
    achievements: ['First Login', 'Bug Hunter']
};

// 2. Accessing Properties (Dot Notation)
// Most common and readable way
console.log(`Welcome, ${user.username}!`);
console.log(`Level: ${user.level}`);

// 3. Accessing Properties (Bracket Notation)
// Useful when the property name is stored in a variable
const propertyToLookUp = 'isOnline';
console.log(user[propertyToLookUp]); // true

// 4. Updating Properties
user.level = 43;
user.isOnline = false;
console.log(`${user.username} is now level ${user.level}`);

// 5. Adding New Properties
user.email = 'knight@example.com';
console.log(user.email);

// 6. Nested Objects
const laptop = {
    brand: 'Apple',
    specs: {
        cpu: 'M3',
        ram: '16GB'
    }
};
console.log(laptop.specs.cpu); // M3
```