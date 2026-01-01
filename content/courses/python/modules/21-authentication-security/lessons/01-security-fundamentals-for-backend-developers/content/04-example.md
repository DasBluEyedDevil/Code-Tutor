---
type: "EXAMPLE"
title: "Broken Access Control in Action"
---

**The Vulnerability:**
A user can access another user's transactions by changing the URL.

**Expected Behavior (Secure):**
```
# User 1 requests their transactions
GET /api/transactions?user_id=1  # Returns user 1's transactions

# User 1 tries to access user 2's data
GET /api/transactions?user_id=2  # Should return 403 Forbidden
```

**The vulnerability allows:**
```
# User 1 can see user 2's private financial data!
GET /api/transactions?user_id=2  # Returns user 2's transactions (BAD!)
```

```python
# BAD: Broken Access Control - DO NOT USE
async def get_transactions_insecure(user_id: int, db):
    """Vulnerable: Uses user_id from request without verification"""
    # Anyone can access any user's transactions!
    query = "SELECT * FROM transactions WHERE user_id = $1"
    return await db.fetch(query, user_id)

# GOOD: Proper Access Control
async def get_transactions_secure(requested_user_id: int, 
                                   current_user_id: int, 
                                   current_user_role: str,
                                   db):
    """Secure: Verifies the current user has permission"""
    # Check if user is accessing their own data or is an admin
    if requested_user_id != current_user_id and current_user_role != 'admin':
        raise PermissionError("Access denied: Cannot view other users' transactions")
    
    query = "SELECT * FROM transactions WHERE user_id = $1"
    return await db.fetch(query, requested_user_id)

# EVEN BETTER: Always use current user from session
async def get_my_transactions(current_user_id: int, db):
    """Best: User ID comes from authenticated session, not request"""
    query = "SELECT * FROM transactions WHERE user_id = $1"
    return await db.fetch(query, current_user_id)

# Example usage
print("Secure API design: /api/my/transactions")
print("The user_id comes from the authenticated session,")
print("not from request parameters that can be manipulated.")
```
