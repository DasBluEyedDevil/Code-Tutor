---
type: "WARNING"
title: "Blog API Security Checklist"
---

**Complete Authentication Pitfalls:**

❌ **NEVER trust client-side user ID**

```python
# WRONG: User can modify their own ID!
@app.put("/posts/{post_id}")
async def update_post(post_id: int, user_id: int, content: str):
    post = await get_post(post_id)
    if post.user_id == user_id:  # User sends fake ID!
        post.content = content

# RIGHT: Get user from token
@app.put("/posts/{post_id}")
async def update_post(
    post_id: int,
    content: str,
    current_user = Depends(get_current_user)
):
    post = await get_post(post_id)
    if post.user_id != current_user.id:
        raise HTTPException(status_code=403, detail="Not your post")
    post.content = content
```

❌ **NEVER skip authorization on sensitive endpoints**

```python
# WRONG: Only authentication, no authorization
@app.delete("/posts/{post_id}")
async def delete_post(post_id: int, user = Depends(get_current_user)):
    await db.delete(post_id)  # Any logged-in user can delete ANY post!

# RIGHT: Verify ownership
@app.delete("/posts/{post_id}")
async def delete_post(post_id: int, user = Depends(get_current_user)):
    post = await get_post(post_id)
    if post.user_id != user.id:
        raise HTTPException(403, "Cannot delete others' posts")
    await db.delete(post_id)
```

❌ **NEVER expose stack traces in production**

```python
# WRONG: Leaks internal details
@app.exception_handler(Exception)
async def error_handler(request, exc):
    return JSONResponse({"error": str(exc)})  # Shows SQL errors!

# RIGHT: Generic error in production
@app.exception_handler(Exception)
async def error_handler(request, exc):
    logger.error(f"Error: {exc}")  # Log for debugging
    return JSONResponse(
        status_code=500,
        content={"error": "Internal server error"}  # Safe message
    )
```

❌ **NEVER forget rate limiting**

```python
# WRONG: Brute force possible
@app.post("/login")
async def login(credentials: Credentials):
    # Attacker can try millions of passwords!

# RIGHT: Rate limit login attempts
from slowapi import Limiter
limiter = Limiter(key_func=get_remote_address)

@app.post("/login")
@limiter.limit("5/minute")
async def login(request: Request, credentials: Credentials):
    # Maximum 5 attempts per minute per IP
```

**Production Security Checklist:**
- [ ] User identity from token, never from client
- [ ] Authorization checks on every sensitive endpoint
- [ ] Generic error messages (no stack traces)
- [ ] Rate limiting on auth endpoints
- [ ] HTTPS only
- [ ] CORS configured properly
- [ ] Input validation on all fields
- [ ] SQL injection prevention (use ORM)
- [ ] XSS prevention (escape output)
