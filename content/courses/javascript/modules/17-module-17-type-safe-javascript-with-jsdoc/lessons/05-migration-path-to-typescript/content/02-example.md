---
type: "EXAMPLE"
title: "JSDoc to TypeScript - Side by Side"
---

The concepts are identical, only the syntax changes:

```typescript
// JSDoc (JavaScript)
/**
 * @typedef {Object} User
 * @property {number} id
 * @property {string} name
 */

/**
 * @param {User} user
 * @returns {string}
 */
function greet(user) {
  return `Hello, ${user.name}!`;
}

// TypeScript
interface User {
  id: number;
  name: string;
}

function greet(user: User): string {
  return `Hello, ${user.name}!`;
}

// Same types, same behavior, different syntax!
```
