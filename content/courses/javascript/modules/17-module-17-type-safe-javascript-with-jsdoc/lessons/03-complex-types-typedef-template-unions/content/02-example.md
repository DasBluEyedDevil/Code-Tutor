---
type: "EXAMPLE"
title: "Reusable Types with @typedef"
---

Define complex types once and reuse them throughout your codebase:

```javascript
/**
 * @typedef {Object} User
 * @property {number} id - Unique identifier
 * @property {string} name - Display name
 * @property {string} email - Email address
 * @property {'admin'|'user'|'guest'} role - User role
 */

/**
 * @typedef {Object} ApiResponse
 * @property {boolean} success
 * @property {T} [data] - Response data (when success)
 * @property {string} [error] - Error message (when failed)
 * @template T
 */

/**
 * @param {number} id
 * @returns {Promise<ApiResponse<User>>}
 */
async function fetchUser(id) {
  const res = await fetch(`/api/users/${id}`);
  if (!res.ok) {
    return { success: false, error: 'User not found' };
  }
  return { success: true, data: await res.json() };
}
```
