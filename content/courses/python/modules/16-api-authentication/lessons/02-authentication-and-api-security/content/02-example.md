---
type: "EXAMPLE"
title: "Code Example: API Key Authentication with FastAPI"
---

**Security implementation patterns with FastAPI:**

**1. API Key Authentication with Depends:**
```python
from fastapi.security import APIKeyHeader

api_key_header = APIKeyHeader(name='X-API-Key')

async def verify_api_key(api_key: str = Depends(api_key_header)):
    if api_key not in API_KEYS:
        raise HTTPException(status_code=401)
    return API_KEYS[api_key]
```

**2. Password Hashing:**
```python
# NEVER store plain passwords!
hashed = hash_password(password)  # Store this
if verify_password(stored, provided):
    # Password correct
```

**3. Rate Limiting:**
- Track requests per time window
- Return 429 when limit exceeded
- Use slowapi package for production

**4. CORS with FastAPI:**
```python
from fastapi.middleware.cors import CORSMiddleware
app.add_middleware(CORSMiddleware, allow_origins=['*'])
```

**5. Input Validation with Pydantic:**
- Automatic validation with type hints
- EmailStr for email validation
- Field() for constraints

```python
from fastapi import FastAPI, Depends, HTTPException, Header, Request
from fastapi.security import APIKeyHeader
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel, EmailStr, Field
import secrets
import hashlib
import os
from datetime import datetime, timedelta
from collections import defaultdict
import time

app = FastAPI()

# CORS configuration
app.add_middleware(
    CORSMiddleware,
    allow_origins=['*'],  # Configure for production
    allow_methods=['*'],
    allow_headers=['*'],
)

# Simulated API keys database
API_KEYS = {
    'dev-key-123': {'name': 'Development', 'permissions': ['read', 'write']},
    'readonly-456': {'name': 'ReadOnly', 'permissions': ['read']}
}

print("=== API Key Authentication ===")

# FastAPI's built-in API key header security
api_key_header = APIKeyHeader(name='X-API-Key', auto_error=False)

async def verify_api_key(api_key: str = Depends(api_key_header)):
    """Dependency to verify API key"""
    if not api_key:
        raise HTTPException(status_code=401, detail='API key required')
    if api_key not in API_KEYS:
        raise HTTPException(status_code=401, detail='Invalid API key')
    return API_KEYS[api_key]

def require_permission(permission: str):
    """Factory for permission-checking dependency"""
    async def check_permission(api_key_info: dict = Depends(verify_api_key)):
        if permission not in api_key_info['permissions']:
            raise HTTPException(
                status_code=403,
                detail=f'Permission denied: {permission} required'
            )
        return api_key_info
    return check_permission

@app.get('/api/public')
def public():
    """Public endpoint - no auth required"""
    return {'message': 'This is public'}

@app.get('/api/protected')
def protected(api_key_info: dict = Depends(verify_api_key)):
    """Protected endpoint - requires API key"""
    return {
        'message': 'You have access!',
        'key_name': api_key_info['name']
    }

@app.get('/api/admin')
def admin(api_key_info: dict = Depends(require_permission('write'))):
    """Admin endpoint - requires write permission"""
    return {
        'message': 'Admin access granted',
        'permissions': api_key_info['permissions']
    }

print("\n=== Password Hashing ===")

def hash_password(password: str) -> bytes:
    """Hash password with salt"""
    salt = os.urandom(32)
    pwdhash = hashlib.pbkdf2_hmac(
        'sha256',
        password.encode('utf-8'),
        salt,
        100000
    )
    return salt + pwdhash

def verify_password(stored_password: bytes, provided_password: str) -> bool:
    """Verify password against hash"""
    salt = stored_password[:32]
    stored_hash = stored_password[32:]
    pwdhash = hashlib.pbkdf2_hmac(
        'sha256',
        provided_password.encode('utf-8'),
        salt,
        100000
    )
    return pwdhash == stored_hash

# Demo password hashing
original_password = "MySecurePassword123!"
print(f"Original password: {original_password}")

hashed = hash_password(original_password)
print(f"Hashed password length: {len(hashed)} bytes")

if verify_password(hashed, original_password):
    print("Correct password verified")

if not verify_password(hashed, "WrongPassword"):
    print("Wrong password rejected")

print("\n=== Rate Limiting ===")

class RateLimiter:
    """Simple rate limiter"""
    
    def __init__(self, max_requests: int = 10, window: int = 60):
        self.max_requests = max_requests
        self.window = window
        self.requests = defaultdict(list)
    
    def is_allowed(self, key: str) -> bool:
        """Check if request is allowed"""
        now = time.time()
        self.requests[key] = [
            req_time for req_time in self.requests[key]
            if now - req_time < self.window
        ]
        if len(self.requests[key]) >= self.max_requests:
            return False
        self.requests[key].append(now)
        return True

rate_limiter = RateLimiter(max_requests=5, window=60)

async def check_rate_limit(request: Request):
    """Dependency for rate limiting"""
    client_ip = request.client.host if request.client else 'unknown'
    if not rate_limiter.is_allowed(client_ip):
        raise HTTPException(
            status_code=429,
            detail='Rate limit exceeded',
            headers={'Retry-After': '60'}
        )
    return True

@app.get('/api/limited')
def limited(_: bool = Depends(check_rate_limit)):
    """Rate limited endpoint"""
    return {'message': 'Request successful'}

print("\n=== Input Validation with Pydantic ===")

class UserCreate(BaseModel):
    """User creation with automatic validation"""
    email: EmailStr  # Automatically validates email format
    name: str = Field(..., min_length=1, max_length=100)
    
    class Config:
        str_strip_whitespace = True  # Auto-strip whitespace

@app.post('/api/users', status_code=201)
def create_user(
    user: UserCreate,
    api_key_info: dict = Depends(verify_api_key)
):
    """Create user - Pydantic validates automatically"""
    return {
        'message': 'User created',
        'user': {'name': user.name, 'email': user.email}
    }

if __name__ == '__main__':
    print("\n=== FastAPI Security Features ===")
    print("\nFeatures implemented:")
    print("  API key authentication with Depends()")
    print("  Permission-based access control")
    print("  Password hashing (PBKDF2)")
    print("  Rate limiting as dependency")
    print("  CORS middleware")
    print("  Input validation with Pydantic")
    print("\nRun with: uvicorn main:app --reload")
    print("API docs at: http://localhost:8000/docs")
```
