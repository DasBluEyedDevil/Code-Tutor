---
type: "EXAMPLE"
title: "Complete User Authentication Flow"
---

**Full registration and login implementation for the Finance Tracker:**

```python
from passlib.context import CryptContext
from datetime import datetime
import secrets
import re
from typing import Optional

# Password context
pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

class User:
    """User model for Finance Tracker"""
    def __init__(self, id: int, email: str, password_hash: str, 
                 created_at: datetime, last_login: Optional[datetime] = None):
        self.id = id
        self.email = email
        self.password_hash = password_hash
        self.created_at = created_at
        self.last_login = last_login

class AuthService:
    """Authentication service for Finance Tracker"""
    
    def __init__(self):
        # Simulated database
        self.users: dict[str, User] = {}
        self.user_counter = 0
    
    def validate_password_strength(self, password: str) -> tuple[bool, str]:
        """Validate password meets security requirements"""
        if len(password) < 12:
            return False, "Password must be at least 12 characters"
        if not re.search(r'[A-Z]', password):
            return False, "Password must contain an uppercase letter"
        if not re.search(r'[a-z]', password):
            return False, "Password must contain a lowercase letter"
        if not re.search(r'\d', password):
            return False, "Password must contain a digit"
        if not re.search(r'[!@#$%^&*(),.?":{}|<>]', password):
            return False, "Password must contain a special character"
        return True, "Password meets requirements"
    
    def register(self, email: str, password: str) -> tuple[bool, str]:
        """Register a new user"""
        # Check if email already exists
        if email.lower() in self.users:
            return False, "Email already registered"
        
        # Validate password strength
        is_valid, message = self.validate_password_strength(password)
        if not is_valid:
            return False, message
        
        # Hash the password
        password_hash = pwd_context.hash(password)
        
        # Create user
        self.user_counter += 1
        user = User(
            id=self.user_counter,
            email=email.lower(),
            password_hash=password_hash,
            created_at=datetime.now()
        )
        self.users[email.lower()] = user
        
        return True, f"User registered successfully with ID {user.id}"
    
    def login(self, email: str, password: str) -> tuple[bool, str]:
        """Authenticate a user"""
        # Find user
        user = self.users.get(email.lower())
        if not user:
            # Same message for security (don't reveal if email exists)
            return False, "Invalid email or password"
        
        # Verify password
        if not pwd_context.verify(password, user.password_hash):
            return False, "Invalid email or password"
        
        # Check if password hash needs upgrade
        if pwd_context.needs_update(user.password_hash):
            # Rehash with current settings
            user.password_hash = pwd_context.hash(password)
            # Would save to database here
        
        # Update last login
        user.last_login = datetime.now()
        
        return True, f"Login successful! Welcome back, user {user.id}"

# Test the authentication flow
auth = AuthService()

print("=" * 50)
print("FINANCE TRACKER AUTHENTICATION TEST")
print("=" * 50)

# Test registration
print("\n1. Registration Tests:")
result = auth.register("alice@example.com", "short")  # Too short
print(f"   Weak password: {result}")

result = auth.register("alice@example.com", "SecurePass123!@#")
print(f"   Strong password: {result}")

result = auth.register("alice@example.com", "AnotherPass123!")
print(f"   Duplicate email: {result}")

# Test login
print("\n2. Login Tests:")
result = auth.login("alice@example.com", "WrongPassword!")
print(f"   Wrong password: {result}")

result = auth.login("nobody@example.com", "SecurePass123!@#")
print(f"   Unknown email: {result}")

result = auth.login("alice@example.com", "SecurePass123!@#")
print(f"   Correct login: {result}")
```
