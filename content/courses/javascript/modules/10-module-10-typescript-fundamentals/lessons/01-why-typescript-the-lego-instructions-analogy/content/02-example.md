---
type: "EXAMPLE"
title: "The TypeScript Difference"
---

```typescript
// 1. JavaScript behavior (The mystery)
function calculateTotal(price, quantity) {
    return price * quantity;
}

// In JS, this runs but gives NaN (Not a Number)
// You might not find the bug for days!
console.log(calculateTotal("19.99", "lots")); 

// 2. TypeScript behavior (The safety)
// We add Type Annotations: price: number, quantity: number
function calculateTotalTS(price: number, quantity: number): number {
    return price * quantity;
}

// TypeScript will show a RED SQUIGGLY line here!
// Error: Argument of type 'string' is not assignable to parameter of type 'number'.
// calculateTotalTS("19.99", "lots");

// 3. Catching property typos
const user = {
    name: "Alice",
    age: 30
};

// In JS, this gives 'undefined'
console.log(user.username); 

// In TS, this is an immediate error
// Error: Property 'username' does not exist on type '{ name: string; age: number; }'
// console.log(user.username);
```