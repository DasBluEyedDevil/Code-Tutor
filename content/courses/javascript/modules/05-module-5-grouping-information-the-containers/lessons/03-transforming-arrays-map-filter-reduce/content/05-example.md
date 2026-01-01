---
type: "EXAMPLE"
title: "Chaining Methods Together"
---

One of the most powerful aspects of map, filter, and reduce is that you can CHAIN them together. Since map and filter both return arrays, you can call another method on the result. This lets you build data transformation pipelines that are readable and expressive.

```javascript
// Chaining: Connect multiple array methods into a pipeline
// Each method returns an array (or value), which feeds into the next

// Example 1: Get names of adult users
let users = [
  { name: 'Alice', age: 25, active: true },
  { name: 'Bob', age: 17, active: true },
  { name: 'Charlie', age: 30, active: false },
  { name: 'Diana', age: 22, active: true },
  { name: 'Eve', age: 15, active: true }
];

let adultNames = users
  .filter(user => user.age >= 18)      // Keep users 18+
  .map(user => user.name);             // Extract just names

console.log(adultNames);  // ['Alice', 'Charlie', 'Diana']

// Example 2: Active adult names (multiple filters, then map)
let activeAdultNames = users
  .filter(user => user.age >= 18)       // Keep adults
  .filter(user => user.active)          // Keep active ones
  .map(user => user.name);              // Get names

console.log(activeAdultNames);  // ['Alice', 'Diana']

// Example 3: Calculate total price of in-stock items
let products = [
  { name: 'Laptop', price: 1000, inStock: true },
  { name: 'Phone', price: 800, inStock: false },
  { name: 'Tablet', price: 500, inStock: true },
  { name: 'Watch', price: 300, inStock: true },
  { name: 'Headphones', price: 150, inStock: false }
];

let inStockTotal = products
  .filter(product => product.inStock)           // Only in-stock items
  .map(product => product.price)                // Get prices
  .reduce((sum, price) => sum + price, 0);      // Sum them up

console.log('In-stock total: $' + inStockTotal);  // In-stock total: $1800

// Example 4: Complex data transformation
let orders = [
  { id: 1, items: ['apple', 'banana'], total: 15.00, status: 'completed' },
  { id: 2, items: ['orange'], total: 8.50, status: 'pending' },
  { id: 3, items: ['apple', 'cherry', 'grape'], total: 22.00, status: 'completed' },
  { id: 4, items: ['banana', 'mango'], total: 12.00, status: 'cancelled' }
];

// Get formatted list of completed order totals
let completedOrderSummary = orders
  .filter(order => order.status === 'completed')  // Only completed
  .map(order => ({                                // Transform to summary
    orderId: order.id,
    itemCount: order.items.length,
    formattedTotal: '$' + order.total.toFixed(2)
  }));

console.log(completedOrderSummary);
// [
//   { orderId: 1, itemCount: 2, formattedTotal: '$15.00' },
//   { orderId: 3, itemCount: 3, formattedTotal: '$22.00' }
// ]

// Example 5: Process and reduce in one chain
let transactions = [
  { type: 'deposit', amount: 100 },
  { type: 'withdrawal', amount: 30 },
  { type: 'deposit', amount: 50 },
  { type: 'withdrawal', amount: 20 },
  { type: 'deposit', amount: 200 }
];

let balance = transactions
  .map(t => t.type === 'deposit' ? t.amount : -t.amount)  // Convert to +/-
  .reduce((total, amount) => total + amount, 0);          // Sum it up

console.log('Balance: $' + balance);  // Balance: $300

// Example 6: Find top 3 scorers
let players = [
  { name: 'Alice', score: 850 },
  { name: 'Bob', score: 920 },
  { name: 'Charlie', score: 780 },
  { name: 'Diana', score: 1050 },
  { name: 'Eve', score: 890 }
];

let top3 = [...players]                              // Copy array (sort mutates!)
  .sort((a, b) => b.score - a.score)                 // Sort descending by score
  .slice(0, 3)                                       // Take first 3
  .map(p => `${p.name}: ${p.score}`);                // Format as strings

console.log('Top 3:', top3);
// Top 3: ['Diana: 1050', 'Bob: 920', 'Eve: 890']
```
