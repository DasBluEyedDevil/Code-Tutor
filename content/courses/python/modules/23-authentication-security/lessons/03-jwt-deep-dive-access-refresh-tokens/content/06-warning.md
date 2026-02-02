---
type: "WARNING"
title: "JWT Security Pitfalls"
---

**Common JWT mistakes that lead to security breaches:**

**1. Algorithm Confusion Attack:**
```python
# NEVER allow 'none' algorithm
jwt.decode(token, options={"verify_signature": False})  # DANGEROUS!

# ALWAYS specify allowed algorithms
jwt.decode(token, SECRET_KEY, algorithms=["HS256"])  # Safe
```

**2. Weak Secret Keys:**
```python
# BAD: Short, predictable keys
SECRET = "secret"  # Can be brute-forced!

# GOOD: Long, random keys (256+ bits)
import secrets
SECRET = secrets.token_hex(32)  # 256 bits
```

**3. Not Validating Claims:**
```python
# BAD: Trusting all claims without validation
user_id = payload["sub"]  # What if attacker changed this?

# GOOD: Validate critical claims
if payload["iss"] != "finance-tracker":
    raise InvalidTokenError("Invalid issuer")
```

**4. Sensitive Data in Payload:**
```python
# NEVER put sensitive data in JWT
payload = {
    "password": "secret123",  # NEVER!
    "ssn": "123-45-6789"      # NEVER!
}
```