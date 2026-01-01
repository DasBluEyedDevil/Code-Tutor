---
type: "EXAMPLE"
title: "Nested Destructuring"
---

When you have objects within objects or arrays within arrays, you can destructure multiple levels deep. This is especially useful for API responses with nested data.

```javascript
// === NESTED OBJECT DESTRUCTURING ===
let user = {
  id: 1,
  name: 'Alice',
  address: {
    street: '123 Main St',
    city: 'New York',
    country: 'USA',
    zip: '10001'
  },
  contacts: {
    email: 'alice@example.com',
    phone: '555-1234'
  }
};

// Extract nested properties
let {
  name,
  address: { city, country },
  contacts: { email }
} = user;

console.log(name);     // 'Alice'
console.log(city);     // 'New York'
console.log(country);  // 'USA'
console.log(email);    // 'alice@example.com'

// Note: 'address' and 'contacts' are NOT created as variables!
// Only the nested properties (city, country, email) are extracted
// To also get the parent object:
let {
  address,                    // Gets the whole address object
  address: { zip }            // Also extracts zip from it
} = user;
console.log(address);         // { street: '123 Main St', city: 'New York', ... }
console.log(zip);             // '10001'

// === NESTED ARRAY DESTRUCTURING ===
let matrix = [
  [1, 2, 3],
  [4, 5, 6],
  [7, 8, 9]
];

// Get specific elements from nested arrays
let [[a, b], [, d, e], [g]] = matrix;
console.log(a, b);    // 1 2 (from first row)
console.log(d, e);    // 5 6 (from second row, skipped 4)
console.log(g);       // 7 (from third row)

// === MIXED: Objects containing Arrays ===
let order = {
  id: 'ORD-001',
  items: ['Laptop', 'Mouse', 'Keyboard'],
  customer: {
    name: 'Bob',
    email: 'bob@shop.com'
  }
};

let {
  id: orderId,
  items: [firstItem, secondItem],
  customer: { name: customerName }
} = order;

console.log(orderId);       // 'ORD-001'
console.log(firstItem);     // 'Laptop'
console.log(secondItem);    // 'Mouse'
console.log(customerName);  // 'Bob'

// === SAFE NESTED DESTRUCTURING with Defaults ===
// What if nested object might not exist?
let incompleteUser = {
  name: 'Charlie'
  // No address property!
};

// This would crash: let { address: { city } } = incompleteUser;
// TypeError: Cannot destructure property 'city' of undefined

// Safe way: provide default empty object
let { address: { city: userCity } = {} } = incompleteUser;
console.log(userCity);  // undefined (but no crash!)

// Even safer: default for the nested property too
let { address: { city: safeCity = 'Unknown' } = {} } = incompleteUser;
console.log(safeCity);  // 'Unknown'

// === PRACTICAL: API Response Handling ===
let apiResponse = {
  data: {
    user: {
      profile: {
        firstName: 'Diana',
        lastName: 'Smith'
      }
    }
  },
  status: 200
};

let {
  data: {
    user: {
      profile: { firstName, lastName }
    }
  },
  status
} = apiResponse;

console.log(`${firstName} ${lastName}`);  // 'Diana Smith'
console.log(status);                       // 200
```
