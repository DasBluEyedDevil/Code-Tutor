---
type: "KEY_POINT"
title: "OAuth2 with FastAPI Takeaways"
---

**Core Concepts:**

1. **OAuth2 is the Standard** - Industry-wide authorization protocol
   - FastAPI has built-in OAuth2 support
   - Automatic OpenAPI documentation
   - Works with any OAuth2 client

2. **OAuth2PasswordBearer** - Token-based auth
   ```python
   oauth2_scheme = OAuth2PasswordBearer(tokenUrl="token")
   ```

3. **Token Endpoint Pattern:**
   ```python
   @app.post("/token")
   async def login(form_data: OAuth2PasswordRequestForm = Depends()):
       user = authenticate_user(form_data.username, form_data.password)
       token = create_access_token(data={"sub": user.email})
       return {"access_token": token, "token_type": "bearer"}
   ```

4. **Scopes for Permissions:**
   - Define: `scopes={"read": "Read access", "admin": "Admin access"}`
   - Protect: `Security(get_current_user, scopes=["admin"])`
   - Check in token validation

5. **Refresh Tokens:**
   - Short access tokens (15-60 min)
   - Long refresh tokens (days/weeks)
   - Exchange refresh for new access token

**Security Best Practices:**
- [ ] Use HTTPS in production
- [ ] Store tokens securely (httpOnly cookies or secure storage)
- [ ] Short access token expiration
- [ ] Validate refresh token type
- [ ] Consider token revocation for refresh tokens
- [ ] Use scopes for fine-grained access control