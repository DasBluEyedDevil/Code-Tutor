---
type: "EXAMPLE"
title: "Hash and Verify Passwords"
---

Bun.password provides secure bcrypt/argon2 hashing with zero dependencies:

```javascript
// Hash a password (use when user signs up)
const hash = await Bun.password.hash('mySecretPassword');
// '$argon2id$v=19$m=65536,t=2,p=1$...' - secure hash

// Store 'hash' in your database, never the plain password!

// Verify a password (use when user logs in)
const isValid = await Bun.password.verify('mySecretPassword', hash);
console.log(isValid);  // true

const isWrong = await Bun.password.verify('wrongPassword', hash);
console.log(isWrong);  // false

// Complete signup/login example
async function signup(email, password) {
  const hash = await Bun.password.hash(password);
  db.prepare('INSERT INTO users (email, password_hash) VALUES (?, ?)')
    .run(email, hash);
}

async function login(email, password) {
  const user = db.prepare('SELECT * FROM users WHERE email = ?').get(email);
  if (!user) return null;
  
  const valid = await Bun.password.verify(password, user.password_hash);
  return valid ? user : null;
}
```
