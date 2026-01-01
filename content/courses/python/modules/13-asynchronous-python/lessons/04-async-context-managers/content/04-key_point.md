---
type: "KEY_POINT"
title: "Always Properly Close Async Resources"
---

**Best practices for async resources:**

1. **Always use `async with` when available:**
   ```python
   async with resource as r:
       await r.do_something()
   # Automatically cleaned up
   ```

2. **For HTTP clients, reuse connections:**
   ```python
   async with httpx.AsyncClient() as client:
       # Reuse this client for multiple requests
       r1 = await client.get(url1)
       r2 = await client.get(url2)
   ```

3. **Database connections should be pooled:**
   ```python
   async with database.pool.acquire() as conn:
       result = await conn.fetch(query)
   ```

**Why cleanup matters:**
- Unclosed connections leak memory
- Too many open files causes errors
- Database connections are limited
- Network sockets are limited