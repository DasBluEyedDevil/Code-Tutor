---
type: "THEORY"
title: "Solution"
---


### Complete Refresh Token System






### Test the Refresh Flow

**1. Login**:

Response includes both tokens:

**2. Use Access Token** (we'll implement this in next lesson)

**3. When Access Token Expires, Refresh**:

Response: New tokens!

**4. Logout**:

---



```bash
curl -X POST http://localhost:8080/api/auth/logout \
  -H "Content-Type: application/json" \
  -d '{"refreshToken": "x9y8z7..."}'
```
