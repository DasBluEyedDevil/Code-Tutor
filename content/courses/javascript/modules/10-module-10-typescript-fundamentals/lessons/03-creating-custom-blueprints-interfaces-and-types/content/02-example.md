---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// TypeScript 5.7 Interfaces and Type Aliases (2024-2025)

// INTERFACE - Blueprint for an object
interface User {
  id: number;
  username: string;
  email: string;
  isActive: boolean;
}

// Using the interface
let alice: User = {
  id: 1,
  username: 'alice',
  email: 'alice@example.com',
  isActive: true
};

console.log('User:', alice.username); // 'alice'

// This will cause an error (missing properties):
// let bob: User = {
//   id: 2,
//   username: 'bob'
//   // ERROR: Missing email and isActive!
// };

// OPTIONAL PROPERTIES - Sometimes a property might not exist
interface Product {
  id: number;
  name: string;
  price: number;
  description?: string;  // ? means optional
  inStock?: boolean;
}

let laptop: Product = {
  id: 101,
  name: 'Gaming Laptop',
  price: 1299.99
  // description and inStock are optional - no error!
};

let phone: Product = {
  id: 102,
  name: 'Smartphone',
  price: 799.99,
  description: 'Latest model with amazing camera',
  inStock: true
};

console.log('Laptop:', laptop.name);      // 'Gaming Laptop'
console.log('Phone stock:', phone.inStock); // true

// TYPE ALIASES - Alternative way to create custom types
type Point = {
  x: number;
  y: number;
};

let origin: Point = { x: 0, y: 0 };
let cursor: Point = { x: 150, y: 200 };

console.log('Cursor position:', cursor.x, cursor.y); // 150 200

// UNION TYPES - A value can be one of several types
type Status = 'pending' | 'approved' | 'rejected';

let orderStatus: Status = 'pending';
console.log('Order status:', orderStatus); // 'pending'

orderStatus = 'approved';  // OK
// orderStatus = 'cancelled'; // ERROR: Not one of the allowed values!

// INTERFACES WITH METHODS - Objects can have functions
interface Calculator {
  add(a: number, b: number): number;
  subtract(a: number, b: number): number;
}

let calc: Calculator = {
  add(a, b) {
    return a + b;
  },
  subtract(a, b) {
    return a - b;
  }
};

console.log('5 + 3 =', calc.add(5, 3));      // 8
console.log('10 - 4 =', calc.subtract(10, 4)); // 6
```
