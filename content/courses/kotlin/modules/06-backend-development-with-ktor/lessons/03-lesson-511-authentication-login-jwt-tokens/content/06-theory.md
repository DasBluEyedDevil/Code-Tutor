---
type: "THEORY"
title: "Testing Login"
---


### Test 1: Successful Login

First, register a user:

Now login:

Response (200 OK):

### Test 2: Wrong Password


Response (401 Unauthorized):

### Test 3: Non-existent Email


Response (401 Unauthorized):

Notice: **Same error message** as wrong password! Security best practice.

### Test 4: Decode the JWT Token

Copy the token from the login response and decode it at [jwt.io](https://jwt.io):

**Header**:

**Payload**:

**Verify Signature**: Paste the secret `your-256-bit-secret-change-this-in-production` to verify the signature is valid.

---



```json
{
  "iss": "http://localhost:8080",
  "aud": "http://localhost:8080/api",
  "sub": "1",
  "email": "alice@example.com",
  "iat": 1705315200,
  "exp": 1705318800
}
```
