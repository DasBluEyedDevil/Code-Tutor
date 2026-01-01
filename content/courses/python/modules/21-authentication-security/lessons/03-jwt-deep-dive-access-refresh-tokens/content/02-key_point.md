---
type: "KEY_POINT"
title: "JWTs Are NOT Encrypted"
---

**Critical understanding:** JWTs are **signed**, not encrypted. Anyone can decode and read the payload!

```python
import base64
token = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0In0.signature"
payload = base64.urlsafe_b64decode(token.split('.')[1] + '==')
print(payload)  # Anyone can read this!
```

**What signing provides:**
- **Integrity** - Payload cannot be modified without detection
- **Authentication** - Only the server with the secret can create valid tokens

**What signing does NOT provide:**
- **Confidentiality** - Anyone can read the payload

**Never put sensitive data in JWTs:** passwords, credit cards, SSNs, etc.