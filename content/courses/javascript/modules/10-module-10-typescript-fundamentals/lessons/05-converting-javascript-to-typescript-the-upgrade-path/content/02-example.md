---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// BEFORE: Plain JavaScript (works but no type safety)
function calculateDiscount(price, discountPercent) {
  if (discountPercent < 0 || discountPercent > 100) {
    return 'Invalid discount';
  }
  let discount = price * (discountPercent / 100);
  return price - discount;
}

let product = {
  name: 'Laptop',
  price: 1000,
  category: 'Electronics'
};

let discountedPrice = calculateDiscount(product.price, 20);
console.log('Original JavaScript:', discountedPrice); // 800

// Problems with the JavaScript version:
// calculateDiscount('abc', 'xyz')  // No error until runtime!
// calculateDiscount(1000, 150)     // Accepts invalid discount percent

// AFTER: TypeScript (type-safe)
interface Product {
  name: string;
  price: number;
  category: string;
}

function calculateDiscountTyped(
  price: number, 
  discountPercent: number
): number | string {
  if (discountPercent < 0 || discountPercent > 100) {
    return 'Invalid discount';
  }
  let discount: number = price * (discountPercent / 100);
  return price - discount;
}

let typedProduct: Product = {
  name: 'Laptop',
  price: 1000,
  category: 'Electronics'
};

let typedDiscountedPrice = calculateDiscountTyped(typedProduct.price, 20);
console.log('TypeScript version:', typedDiscountedPrice); // 800

// These will now cause COMPILE-TIME errors:
// calculateDiscountTyped('abc', 'xyz')  // ERROR: string is not assignable to number
// calculateDiscountTyped(1000, 150)     // Still runs, but you can add validation

// MIGRATION STRATEGY: Step-by-step conversion

// Step 1: Rename .js files to .ts (start simple)
// app.js → app.ts

// Step 2: Add return types to functions
function greet(name: string): string {
  return `Hello, ${name}!`;
}

// Step 3: Add parameter types
function add(a: number, b: number): number {
  return a + b;
}

// Step 4: Create interfaces for complex objects
interface User {
  id: number;
  name: string;
  email: string;
}

// Step 5: Update variable declarations
let users: User[] = [
  { id: 1, name: 'Alice', email: 'alice@example.com' },
  { id: 2, name: 'Bob', email: 'bob@example.com' }
];

// Step 6: Enable strict mode in tsconfig.json (gradually)
// Start with "strict": false, then enable incrementally

console.log('Migration complete!');
console.log('User count:', users.length); // 2
```
