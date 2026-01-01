---
type: "THEORY"
title: "Algorithm Options"
---

Bun supports multiple algorithms:

```javascript
// Argon2id (default, recommended)
await Bun.password.hash(password, { algorithm: 'argon2id' });

// Argon2d (faster, less memory-hard)
await Bun.password.hash(password, { algorithm: 'argon2d' });

// Bcrypt (widely compatible)
await Bun.password.hash(password, { algorithm: 'bcrypt', cost: 12 });
```

**Which to choose:**
- **argon2id**: Best security, recommended for new projects
- **bcrypt**: Use if you need compatibility with existing systems