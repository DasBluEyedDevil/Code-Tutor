---
type: "THEORY"
title: "OAuth2 in FastAPI"
---

**OAuth2 is the Industry Standard for Authorization**

OAuth2 is a protocol that allows applications to securely authorize users without handling passwords directly. FastAPI has excellent built-in OAuth2 support.

**Key Concepts:**

1. **OAuth2 is a Standard Protocol**
   - Industry-standard for authorization
   - Used by Google, GitHub, Facebook, etc.
   - Separates authentication from authorization

2. **FastAPI's Built-in OAuth2 Support**
   - `OAuth2PasswordBearer` - Token-based authentication
   - `OAuth2PasswordRequestForm` - Standard login form
   - Automatic OpenAPI documentation

3. **Password Flow (Resource Owner Password Flow)**
   - Best for your own first-party applications
   - User sends username/password directly
   - Server returns access token
   - Simple but only use with trusted clients

4. **Scopes for Permission Control**
   - Define what actions a token can perform
   - Examples: `read`, `write`, `admin`
   - Fine-grained access control
   - Token can have multiple scopes

**OAuth2 Flow:**
```
1. Client sends credentials to /token endpoint
2. Server validates credentials
3. Server returns access_token (+ optional refresh_token)
4. Client includes token in Authorization header
5. Server validates token on each request
```