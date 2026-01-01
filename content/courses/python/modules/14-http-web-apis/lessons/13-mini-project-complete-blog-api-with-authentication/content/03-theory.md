---
type: "THEORY"
title: "Understanding the Authentication Flow"
---

**How OAuth2 + JWT token authentication works in FastAPI:**

**1. Registration Flow:**
```
Client                          Server
  |                               |
  |-- POST /register ------------>|
  |   {username, email, pass}     |
  |                               |-- Validate with Pydantic
  |                               |-- Hash password (bcrypt)
  |                               |-- Store user
  |<------------ 201 Created -----|   
  |   {message: 'registered'}     |
```

**2. Login Flow (OAuth2PasswordRequestForm):**
```
Client                          Server
  |                               |
  |-- POST /login --------------->|
  |   (form data: username, pwd)  |
  |                               |-- Verify credentials
  |                               |-- Create JWT token
  |<------------ 200 OK ---------|   
  |   {access_token, token_type}  |
```

**3. Authenticated Request (Depends):**
```
Client                          Server
  |                               |
  |-- POST /posts --------------->|
  |   Header: Bearer <jwt>        |
  |   {title, content}            |
  |                               |-- Depends(get_current_user)
  |                               |-- Decode JWT, get user
  |                               |-- Create post
  |<------------ 201 Created -----|   
  |   {post: {...}}               |
```

**FastAPI security features:**
- **Pydantic validation** - Automatic input validation
- **OAuth2PasswordBearer** - Standard OAuth2 flow
- **Depends()** - Dependency injection for auth
- **HTTPException** - Proper error responses
- **Auto-generated docs** - /docs shows auth requirements

**Authorization levels:**
- **Public**: Anyone can access (GET /api/posts)
- **Authenticated**: Requires Depends(get_current_user)
- **Owner only**: Check current_user == resource.author