---
type: "EXAMPLE"
title: "some() and every() - Test Conditions Across Array"
---

some() checks if AT LEAST ONE element passes the test (returns true/false). every() checks if ALL elements pass (returns true/false). Both use short-circuit evaluation - they stop as soon as they know the answer.

```javascript
// === some() - Does ANY element match? ===
let ages = [12, 15, 18, 21, 25];

// Is there at least one adult (18+)?
let hasAdult = ages.some(age => age >= 18);
console.log(hasAdult);  // true

// Short-circuit: stops at 18, doesn't check 21 or 25

// === every() - Do ALL elements match? ===
let allAdults = ages.every(age => age >= 18);
console.log(allAdults);  // false (12 and 15 are not adults)

// Short-circuit: stops at 12 (first failure)

// === Real-world: Form Validation ===
let formFields = [
  { name: 'email', value: 'test@example.com', valid: true },
  { name: 'password', value: '12345', valid: false },
  { name: 'username', value: 'johndoe', valid: true }
];

// Are ALL fields valid? (Must ALL be valid to submit)
let canSubmit = formFields.every(field => field.valid);
console.log('Can submit form:', canSubmit);  // false

// Is there ANY invalid field? (For showing error message)
let hasErrors = formFields.some(field => !field.valid);
console.log('Has errors:', hasErrors);  // true

// === Real-world: Permission Checking ===
let userPermissions = ['read', 'write', 'delete'];
let requiredPermissions = ['read', 'write'];

// Does user have ALL required permissions?
let hasAllPermissions = requiredPermissions.every(
  perm => userPermissions.includes(perm)
);
console.log('Authorized:', hasAllPermissions);  // true

// Does user have ANY admin permissions?
let adminPerms = ['admin', 'superuser'];
let isAdmin = adminPerms.some(
  perm => userPermissions.includes(perm)
);
console.log('Is admin:', isAdmin);  // false

// === Empty Array Behavior (Important!) ===
let empty = [];
console.log(empty.some(x => x > 0));   // false (no element to match)
console.log(empty.every(x => x > 0));  // true (vacuously true!)

// Why every([]) is true:
// 'All items in empty array pass' is technically true
// because there are no items to fail the test
```
