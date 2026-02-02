---
type: "EXAMPLE"
title: "Advanced passlib Configuration"
---

**Production-ready configuration with automatic hash upgrades:**

```python
from passlib.context import CryptContext

# Production configuration with multiple schemes
pwd_context = CryptContext(
    # Primary scheme (used for new hashes)
    schemes=["argon2", "bcrypt"],
    
    # Automatically re-hash old passwords on verification
    deprecated="auto",
    
    # Argon2 configuration
    argon2__rounds=4,              # Time cost
    argon2__memory_cost=65536,     # Memory in KB (64 MB)
    argon2__parallelism=2,         # CPU threads
    
    # bcrypt fallback configuration  
    bcrypt__rounds=12              # Cost factor
)

class PasswordService:
    """Production password service with hash upgrade support"""
    
    def __init__(self, context: CryptContext):
        self.context = context
    
    def hash(self, password: str) -> str:
        """Hash a new password"""
        return self.context.hash(password)
    
    def verify_and_update(self, password: str, hash: str) -> tuple[bool, str | None]:
        """
        Verify password and return updated hash if needed.
        
        Returns:
            (is_valid, new_hash)
            - is_valid: True if password matches
            - new_hash: Updated hash if old scheme was used, else None
        """
        # Verify and check if hash needs updating
        is_valid, new_hash = self.context.verify_and_update(password, hash)
        return is_valid, new_hash
    
    def needs_rehash(self, hash: str) -> bool:
        """Check if hash uses deprecated scheme or settings"""
        return self.context.needs_update(hash)

# Usage example
service = PasswordService(pwd_context)

# Simulate an old bcrypt hash from before we switched to Argon2
old_bcrypt_hash = service.context.hash("MyPassword123")
print(f"Hash algorithm detected: {service.context.identify(old_bcrypt_hash)}")

# During login, verify and get updated hash if needed
is_valid, new_hash = service.verify_and_update("MyPassword123", old_bcrypt_hash)
print(f"Password valid: {is_valid}")
print(f"Hash needs update: {new_hash is not None}")

if new_hash:
    print(f"New hash algorithm: {service.context.identify(new_hash)}")
    print("Would save new_hash to database for seamless upgrade")
```
