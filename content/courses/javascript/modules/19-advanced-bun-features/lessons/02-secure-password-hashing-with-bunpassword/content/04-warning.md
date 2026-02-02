---
type: "WARNING"
title: "Security Best Practices"
---

1. **Never store plain passwords** - Always hash before storing

2. **Never log passwords** - Even during debugging
```javascript
// WRONG
console.log('User password:', password);

// RIGHT
console.log('Login attempt for:', email);
```

3. **Use HTTPS** - Passwords should only travel encrypted

4. **Add rate limiting** - Prevent brute force attacks
```javascript
const attempts = new Map();
if (attempts.get(email) > 5) {
  throw new Error('Too many attempts');
}
```