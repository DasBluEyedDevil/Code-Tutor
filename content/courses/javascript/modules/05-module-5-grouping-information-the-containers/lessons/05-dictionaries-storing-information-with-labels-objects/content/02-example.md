---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Creating an object
let person = {
  name: 'Alice',
  age: 25,
  city: 'New York',
  isStudent: false
};

// Accessing properties (two ways)
console.log(person.name);      // Alice (dot notation)
console.log(person['age']);    // 25 (bracket notation)

// Changing a property
person.age = 26;
console.log(person.age);  // 26

// Adding a new property
person.email = 'alice@example.com';
console.log(person.email);  // alice@example.com

// Deleting a property
delete person.isStudent;
console.log(person.isStudent);  // undefined

// Object with different types
let product = {
  name: 'Laptop',
  price: 999.99,
  inStock: true,
  specs: ['16GB RAM', '512GB SSD'],  // Array inside object!
  manufacturer: {
    name: 'Dell',
    country: 'USA'
  }  // Object inside object!
};

console.log(product.specs[0]);  // 16GB RAM
console.log(product.manufacturer.name);  // Dell

// Empty object
let empty = {};
```
