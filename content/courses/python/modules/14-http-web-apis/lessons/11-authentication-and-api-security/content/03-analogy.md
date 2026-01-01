---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**FastAPI API Key Authentication Pattern:**

```python
from fastapi.security import APIKeyHeader

api_key_header = APIKeyHeader(name='X-API-Key')

async def verify_api_key(api_key: str = Depends(api_key_header)):
    if api_key not in VALID_KEYS:
        raise HTTPException(status_code=401)
    return VALID_KEYS[api_key]

@app.get('/protected')
def protected(key_info = Depends(verify_api_key)):
    return {'data': 'secret'}
```

**Password Hashing:**

```python
import hashlib
import os

# Hash password with salt
salt = os.urandom(32)
hashed = hashlib.pbkdf2_hmac('sha256', password.encode(), salt, 100000)
stored = salt + hashed  # Store this

# Verify password
salt = stored[:32]
stored_hash = stored[32:]
test_hash = hashlib.pbkdf2_hmac('sha256', provided.encode(), salt, 100000)
if test_hash == stored_hash:
    # Password correct
```

**Rate Limiting as Dependency:**

```python
async def check_rate_limit(request: Request):
    client_ip = request.client.host
    if not rate_limiter.is_allowed(client_ip):
        raise HTTPException(status_code=429)
    return True

@app.get('/limited')
def limited(_: bool = Depends(check_rate_limit)):
    return {'message': 'OK'}
```

**CORS with FastAPI Middleware:**

```python
from fastapi.middleware.cors import CORSMiddleware

app.add_middleware(
    CORSMiddleware,
    allow_origins=['https://yourdomain.com'],
    allow_methods=['*'],
    allow_headers=['*'],
)
```