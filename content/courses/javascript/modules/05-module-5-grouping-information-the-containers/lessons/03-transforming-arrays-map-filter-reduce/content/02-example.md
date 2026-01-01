---
type: "EXAMPLE"
title: "map() - Transform Every Element"
---

The map() method creates a NEW array by applying a transformation function to every element. The original array is never modified. The new array always has the same number of elements as the original - map transforms but never adds or removes items.

```javascript
// map() transforms EVERY element and returns a NEW array
// Original array is NEVER modified

// Example 1: Double every number
let numbers = [1, 2, 3, 4, 5];
let doubled = numbers.map(function(num) {
  return num * 2;
});
console.log(doubled);   // [2, 4, 6, 8, 10]
console.log(numbers);   // [1, 2, 3, 4, 5] - UNCHANGED!

// Example 2: Square every number (arrow function shorthand)
let squared = numbers.map(num => num * num);
console.log(squared);   // [1, 4, 9, 16, 25]

// Example 3: Extract property from objects
let users = [
  { name: 'Alice', age: 25, email: 'alice@example.com' },
  { name: 'Bob', age: 30, email: 'bob@example.com' },
  { name: 'Charlie', age: 35, email: 'charlie@example.com' }
];

let names = users.map(user => user.name);
console.log(names);   // ['Alice', 'Bob', 'Charlie']

let emails = users.map(user => user.email);
console.log(emails);  // ['alice@example.com', 'bob@example.com', 'charlie@example.com']

// Example 4: Format data for display
let prices = [19.99, 45.50, 99.00, 12.75];
let formatted = prices.map(price => '$' + price.toFixed(2));
console.log(formatted);  // ['$19.99', '$45.50', '$99.00', '$12.75']

// Example 5: Calculate with tax
let taxRate = 0.08;  // 8% tax
let withTax = prices.map(price => {
  let tax = price * taxRate;
  let total = price + tax;
  return Math.round(total * 100) / 100;  // Round to 2 decimal places
});
console.log(withTax);  // [21.59, 49.14, 106.92, 13.77]

// Example 6: Transform objects into new objects
let products = [
  { name: 'Laptop', price: 1000 },
  { name: 'Mouse', price: 25 },
  { name: 'Keyboard', price: 75 }
];

let productCards = products.map(product => {
  return {
    title: product.name.toUpperCase(),
    displayPrice: '$' + product.price.toFixed(2),
    originalProduct: product
  };
});
console.log(productCards);
// [
//   { title: 'LAPTOP', displayPrice: '$1000.00', originalProduct: {...} },
//   { title: 'MOUSE', displayPrice: '$25.00', originalProduct: {...} },
//   { title: 'KEYBOARD', displayPrice: '$75.00', originalProduct: {...} }
// ]

// Verify original array is unchanged
console.log(products[0].name);  // 'Laptop' (not 'LAPTOP')
```
