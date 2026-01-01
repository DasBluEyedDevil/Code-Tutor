---
type: "EXAMPLE"
title: "Chaining Dependencies"
---

**Dependencies can depend on other dependencies:**

This creates a chain where each dependency builds on the previous one.

**Common Pattern: Authentication Chain**
```
get_settings -> verify_token -> get_current_user
```

**Benefits:**
- Each step has a single responsibility
- Easy to test each step independently
- Reuse intermediate dependencies
- Clear error handling at each level

```python
from fastapi import FastAPI, Depends, HTTPException

app = FastAPI()

# Level 1: Basic settings
def get_settings():
    """Provide application settings"""
    return {"api_key": "secret123", "admin_emails": ["admin@example.com"]}

# Level 2: Depends on settings
def verify_api_key(settings = Depends(get_settings)):
    """Verify API key (simulated - real app would check header)"""
    # In real app: get key from header and compare
    # For demo, we just return True
    if settings["api_key"]:
        return True
    raise HTTPException(status_code=401, detail="Invalid API key")

# Level 3: Depends on verification
def get_current_user(verified = Depends(verify_api_key)):
    """Get user only if API key is valid"""
    if not verified:
        raise HTTPException(status_code=401, detail="Not authenticated")
    return {"user_id": 1, "name": "Alice", "role": "admin"}

# Level 4: Depends on current user
def require_admin(user = Depends(get_current_user)):
    """Check if user is admin"""
    if user["role"] != "admin":
        raise HTTPException(status_code=403, detail="Admin required")
    return user

@app.get("/me")
async def read_user(user = Depends(get_current_user)):
    """Get current user info"""
    return user

@app.get("/admin/dashboard")
async def admin_dashboard(admin = Depends(require_admin)):
    """Admin-only endpoint"""
    return {"message": f"Welcome, {admin['name']}!", "admin_panel": True}

# Demonstration
print("=== Chaining Dependencies ===")
print("\nDependency chain:")
print("  get_settings()")
print("      |")
print("      v")
print("  verify_api_key(settings)")
print("      |")
print("      v")
print("  get_current_user(verified)")
print("      |")
print("      v")
print("  require_admin(user)")

print("\nWhen /admin/dashboard is called:")
print("  1. get_settings() returns config")
print("  2. verify_api_key() checks the key")
print("  3. get_current_user() returns user")
print("  4. require_admin() checks admin role")
print("  5. Endpoint runs with admin user")

print("\nSimulating the chain:")
settings = get_settings()
print(f"  Settings: {settings['api_key'][:6]}...")
verified = verify_api_key(settings)
print(f"  Verified: {verified}")
user = get_current_user(verified)
print(f"  User: {user['name']} ({user['role']})")
admin = require_admin(user)
print(f"  Admin check passed: {admin['name']}")
```
