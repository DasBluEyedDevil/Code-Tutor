---
type: "EXAMPLE"
title: "Async Authentication (Django 5.2)"
---

**Async Auth Functions in Django 5.2**

Django 5.2 provides async versions of authentication functions:

| Sync | Async |
|------|-------|
| `authenticate()` | `aauthenticate()` |
| `login()` | `alogin()` |
| `logout()` | `alogout()` |
| `get_user()` | `aget_user()` |

**Async Decorators:**
- Use `@login_required` with async views - it handles both
- For async-specific, use `@sync_to_async` wrapper

**Request User in Async Views:**
```python
async def my_view(request):
    # request.user works but may block
    # For fully async, use:
    user = await aget_user(request)
```

```python
from django.http import JsonResponse
from django.contrib.auth import (
    aauthenticate, 
    alogin, 
    alogout,
)
from django.views.decorators.http import require_http_methods
import json


@require_http_methods(["POST"])
async def async_login_view(request):
    """Async login endpoint."""
    data = json.loads(request.body)
    
    # Async authentication
    user = await aauthenticate(
        request,
        username=data.get("username"),
        password=data.get("password")
    )
    
    if user is not None:
        # Async login
        await alogin(request, user)
        return JsonResponse({
            "status": "success",
            "user": user.username
        })
    
    return JsonResponse(
        {"error": "Invalid credentials"},
        status=401
    )


@require_http_methods(["POST"])
async def async_logout_view(request):
    """Async logout endpoint."""
    await alogout(request)
    return JsonResponse({"status": "logged_out"})


# Using with login_required decorator
from django.contrib.auth.decorators import login_required

@login_required
async def protected_async_view(request):
    """Protected async view - login_required works with async."""
    return JsonResponse({
        "message": f"Hello, {request.user.username}!",
        "authenticated": True
    })


print("=== Django 5.2 Async Authentication ===")

print("\nAsync Auth Functions:")
print("  await aauthenticate(request, username, password)")
print("  await alogin(request, user)")
print("  await alogout(request)")

print("\nDecorators Work with Async:")
print("  @login_required")
print("  async def my_view(request):")
print("      ...")

print("\nMigration Tip:")
print("  Replace authenticate() with aauthenticate()")
print("  Replace login() with alogin()")
print("  Add 'await' before calls")
```
