---
type: "EXAMPLE"
title: "Subset, Superset, and Disjoint Checks"
---

These methods let you compare Sets: isSubsetOf() checks if all items exist in another Set, isSupersetOf() checks if a Set contains all items of another, and isDisjointFrom() checks if Sets have no common items.

```javascript
// isSubsetOf() - Check if ALL items exist in another set
let basic = new Set(['read']);
let standard = new Set(['read', 'write']);
let premium = new Set(['read', 'write', 'delete', 'admin']);

console.log(basic.isSubsetOf(standard));    // true - 'read' is in standard
console.log(standard.isSubsetOf(premium));  // true - 'read', 'write' in premium
console.log(premium.isSubsetOf(basic));     // false - premium has more items

// isSupersetOf() - Check if set CONTAINS all items of another
console.log(premium.isSupersetOf(standard));  // true
console.log(premium.isSupersetOf(basic));     // true
console.log(basic.isSupersetOf(premium));     // false

// isDisjointFrom() - Check if sets have NO common items
let cats = new Set(['whiskers', 'mittens', 'shadow']);
let dogs = new Set(['buddy', 'max', 'bella']);
let mixed = new Set(['whiskers', 'buddy']);

console.log(cats.isDisjointFrom(dogs));   // true - no overlap
console.log(cats.isDisjointFrom(mixed));  // false - 'whiskers' in both

// Practical: Permission validation
let requiredPerms = new Set(['read', 'write']);
let userPerms = new Set(['read', 'write', 'comment']);

if (requiredPerms.isSubsetOf(userPerms)) {
  console.log('Access granted!');
} else {
  let missing = requiredPerms.difference(userPerms);
  console.log('Missing permissions:', [...missing]);
}
```
