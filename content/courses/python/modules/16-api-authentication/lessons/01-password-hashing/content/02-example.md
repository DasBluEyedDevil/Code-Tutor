---
type: "EXAMPLE"
title: "Using passlib with bcrypt"
---

**passlib: The Recommended Library**

`passlib` provides a clean, secure interface for password hashing. It handles all the complexity of salting, hashing, and verification.

**Installation:**
```bash
uv add "passlib[bcrypt]"
# or: pip install "passlib[bcrypt]"
```

**Key Concepts:**
- `CryptContext`: Manages hashing schemes and configuration
- `hash()`: Creates a secure hash from a password
- `verify()`: Checks if a password matches a hash
- `deprecated="auto"`: Automatically handles scheme upgrades

```python
# uv add "passlib[bcrypt]"

from passlib.context import CryptContext

# Configure password hashing context
# - schemes: List of allowed hashing algorithms
# - deprecated: Automatically rehash old schemes
pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

def hash_password(password: str) -> str:
    """Hash a password for storage.
    
    This creates a secure bcrypt hash with automatic salt.
    Each call produces a different hash for the same password!
    """
    return pwd_context.hash(password)

def verify_password(plain_password: str, hashed_password: str) -> bool:
    """Verify a password against its hash.
    
    Returns True if the password matches, False otherwise.
    Timing-safe comparison prevents timing attacks.
    """
    return pwd_context.verify(plain_password, hashed_password)

# Demonstration
print("=== Password Hashing Demo ===")
print()

# Hash a password
password = "secretpassword123"
hashed = hash_password(password)

print(f"Original password: {password}")
print(f"Hashed password:   {hashed}")
print(f"Hash length:       {len(hashed)} characters")
print()

# Notice: Same password produces different hashes (due to salt)
print("Same password, different hashes (salt makes each unique):")
for i in range(3):
    h = hash_password(password)
    print(f"  Hash {i+1}: {h[:30]}...")
print()

# Verify passwords
print("Password verification:")
print(f"  Correct password: {verify_password('secretpassword123', hashed)}")
print(f"  Wrong password:   {verify_password('wrongpassword', hashed)}")
print(f"  Empty password:   {verify_password('', hashed)}")
```
