---
type: "WARNING"
title: "Common Mistakes"
---

**1. Confusing find() with filter():**
```javascript
// find() returns ONE element (or undefined)
let result = arr.find(x => x > 5);  // Single value

// filter() returns an ARRAY of all matches
let results = arr.filter(x => x > 5);  // Array
```

**2. Forgetting undefined checks with find():**
```javascript
// DANGEROUS - will crash if not found!
let user = users.find(u => u.id === 999);
console.log(user.name);  // TypeError: Cannot read property 'name' of undefined

// SAFE - check first!
let user = users.find(u => u.id === 999);
if (user) {
  console.log(user.name);
} else {
  console.log('User not found');
}

// Or use optional chaining:
console.log(user?.name ?? 'Unknown');
```

**3. Using indexOf() with objects (reference equality):**
```javascript
let users = [{ name: 'Alice' }];
let search = { name: 'Alice' };

console.log(users.indexOf(search));  // -1 (different objects!)
console.log(users.includes(search)); // false

// Use find() for objects:
let found = users.find(u => u.name === 'Alice');  // Works!
```

**4. Expecting indexOf() to find NaN:**
```javascript
let arr = [1, NaN, 3];
console.log(arr.indexOf(NaN));   // -1 (can't find it!)
console.log(arr.includes(NaN));  // true (this works)

// Use includes() or find() for NaN
```

**5. Misunderstanding some() and every() with empty arrays:**
```javascript
let empty = [];
console.log(empty.some(x => x > 0));   // false - no element matches
console.log(empty.every(x => x > 0));  // true - 'all zero elements pass'

// Always consider the empty array case in your logic!
if (items.length > 0 && items.every(isValid)) {
  // Safe check
}
```

**6. Using at() for assignment (it's read-only):**
```javascript
arr.at(0) = 'new value';  // ERROR!
arr[0] = 'new value';     // Correct
```

**7. Browser compatibility:**
```javascript
// at() - ES2022 (most modern browsers)
// findLast/findLastIndex - ES2023 (check browser support)
// For older environments, use polyfills or alternatives
```