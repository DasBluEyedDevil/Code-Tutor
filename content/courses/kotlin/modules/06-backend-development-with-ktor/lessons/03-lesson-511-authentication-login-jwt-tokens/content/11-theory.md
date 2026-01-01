---
type: "THEORY"
title: "Checkpoint Quiz"
---


### Question 1
What are the three parts of a JWT?

A) Username, Password, Signature
B) Header, Body, Footer
C) Header, Payload, Signature
D) Key, Value, Hash

### Question 2
Why use refresh tokens instead of just making access tokens long-lived?

A) Refresh tokens look cooler
B) Short access tokens limit exposure if stolen; refresh tokens enable revocation
C) It's required by OAuth 2.0 specification
D) Refresh tokens are faster to verify

### Question 3
Why should error messages for "wrong password" and "email not found" be identical?

A) It's easier to code
B) It prevents attackers from enumerating valid email addresses
C) It confuses users
D) It's required by GDPR

### Question 4
What claim in a JWT identifies the user?

A) `uid`
B) `user`
C) `sub` (subject)
D) `id`

### Question 5
Why hash refresh tokens before storing them in the database?

A) To make them look random
B) To save database space
C) To protect users if database is breached (like password hashing)
D) It's not necessary, just a best practice

---

