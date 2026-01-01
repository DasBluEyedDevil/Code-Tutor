---
type: "EXAMPLE"
title: "union() - Combining Sets"
---

The union() method combines two Sets into one, returning all unique items from both. It's like creating a guest list for people invited to either party.

```javascript
// union() returns all unique items from BOTH sets
// Like 'everyone invited to EITHER party'

let frontend = new Set(['Alice', 'Bob', 'Charlie']);
let backend = new Set(['Charlie', 'Diana', 'Eve']);

let allDevs = frontend.union(backend);
console.log(allDevs);
// Set { 'Alice', 'Bob', 'Charlie', 'Diana', 'Eve' }
// Notice: Charlie appears only once (no duplicates)

// Practical: Combine permission sets
let adminPerms = new Set(['read', 'write', 'delete', 'admin']);
let userPerms = new Set(['read', 'write']);

let combinedPerms = adminPerms.union(userPerms);
console.log(combinedPerms);
// Set { 'read', 'write', 'delete', 'admin' }

// Works with any iterable
let setA = new Set([1, 2, 3]);
let setB = new Set([3, 4, 5]);
console.log(setA.union(setB));  // Set { 1, 2, 3, 4, 5 }

// OLD WAY (before ES2025):
let oldUnion = new Set([...setA, ...setB]);
console.log(oldUnion);  // Same result, but more verbose
```
