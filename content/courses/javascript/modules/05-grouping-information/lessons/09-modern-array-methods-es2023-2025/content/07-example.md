---
type: "EXAMPLE"
title: "Map.groupBy() - Grouping with Complex Keys"
---

Map.groupBy() works like Object.groupBy() but returns a Map instead of an object. Use it when you need non-string keys (booleans, objects, numbers) since Map preserves key types while objects convert everything to strings.

```javascript
// Map.groupBy() is like Object.groupBy() but returns a Map
// Why use Map? Keys can be ANY type (not just strings)!

let products = [
  { name: 'Apple', category: 'Fruit', inStock: true },
  { name: 'Banana', category: 'Fruit', inStock: false },
  { name: 'Carrot', category: 'Vegetable', inStock: true },
  { name: 'Milk', category: 'Dairy', inStock: true }
];

// Object.groupBy - Keys become strings
let objGrouped = Object.groupBy(products, p => p.inStock);
console.log(objGrouped['true']);   // Works (string key)
console.log(objGrouped[true]);     // undefined! Boolean converted to string

// Map.groupBy - Keys keep their original type
let mapGrouped = Map.groupBy(products, p => p.inStock);
console.log(mapGrouped.get(true));   // Products that are in stock
console.log(mapGrouped.get(false));  // Products that are out of stock

// Group by object reference (impossible with Object.groupBy!)
let categoryA = { name: 'Fruits' };
let categoryB = { name: 'Vegetables' };

let items = [
  { product: 'Apple', cat: categoryA },
  { product: 'Banana', cat: categoryA },
  { product: 'Carrot', cat: categoryB }
];

let byCategory = Map.groupBy(items, item => item.cat);
console.log(byCategory.get(categoryA));  // Apple and Banana
console.log(byCategory.get(categoryB));  // Carrot

// Practical: Group DOM elements by their tag
// (In browser) const elements = document.querySelectorAll('*');
// const byTag = Map.groupBy([...elements], el => el.tagName);
// console.log(byTag.get('DIV'));  // All div elements

// Iterate over Map groups
for (let [key, items] of mapGrouped) {
  console.log(`In stock: ${key}`, items.map(p => p.name));
}
// In stock: true ['Apple', 'Carrot', 'Milk']
// In stock: false ['Banana']

// Convert Map to Object if needed
let asObject = Object.fromEntries(mapGrouped);
console.log(asObject);  // { true: [...], false: [...] }
```
