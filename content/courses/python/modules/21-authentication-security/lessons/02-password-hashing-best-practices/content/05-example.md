---
type: "EXAMPLE"
title: "Password Hashing with passlib"
---

**passlib** is the gold standard for password hashing in Python:

**Installation:**
```bash
pip install passlib[bcrypt]
```

**Expected Output:**
```
Hashed password: $2b$12$...
Password verification: True
Wrong password check: False
```

```python
from passlib.context import CryptContext

# Create a password context with bcrypt as the primary scheme
pwd_context = CryptContext(
    schemes=["bcrypt"],
    deprecated="auto"  # Automatically upgrade old hashes
)

def hash_password(password: str) -> str:
    """Hash a password for secure storage"""
    return pwd_context.hash(password)

def verify_password(plain_password: str, hashed_password: str) -> bool:
    """Verify a password against its hash"""
    return pwd_context.verify(plain_password, hashed_password)

# Example usage
password = "MySecureP@ssw0rd!"

# Hash the password (would be done during registration)
hashed = hash_password(password)
print(f"Hashed password: {hashed}")

# Verify the password (would be done during login)
is_valid = verify_password(password, hashed)
print(f"Password verification: {is_valid}")

# Wrong password check
is_valid_wrong = verify_password("WrongPassword", hashed)
print(f"Wrong password check: {is_valid_wrong}")
```
