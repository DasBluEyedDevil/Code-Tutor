---
type: "EXAMPLE"
title: "Protecting Views with Decorators"
---

Django provides decorators and mixins to restrict access to views:

**Expected Output:**
```
Public page: Anyone can access
Dashboard: Logged in users only
Admin panel: Staff only
Manage users: Permission required
```

```python
from functools import wraps
from typing import Callable, List, Optional
from dataclasses import dataclass

@dataclass
class Request:
    """Simulated Django request object."""
    user: any
    path: str
    method: str = "GET"

class PermissionDenied(Exception):
    """403 Forbidden response."""
    pass

class Redirect:
    """Simulated redirect response."""
    def __init__(self, url: str):
        self.url = url
    def __repr__(self):
        return f"Redirect to {self.url}"


# =========================
# Decorators (Function-Based Views)
# =========================

def login_required(view_func: Callable) -> Callable:
    """
    Decorator that requires user to be authenticated.
    Redirects to login page if not authenticated.
    """
    @wraps(view_func)
    def wrapper(request: Request, *args, **kwargs):
        if not request.user.is_authenticated:
            return Redirect(f"/login/?next={request.path}")
        return view_func(request, *args, **kwargs)
    return wrapper

def staff_member_required(view_func: Callable) -> Callable:
    """
    Decorator that requires user to be staff.
    """
    @wraps(view_func)
    def wrapper(request: Request, *args, **kwargs):
        if not request.user.is_authenticated:
            return Redirect("/login/")
        if not request.user.is_staff:
            raise PermissionDenied("Staff access required")
        return view_func(request, *args, **kwargs)
    return wrapper

def permission_required(perm: str) -> Callable:
    """
    Decorator factory that requires specific permission.
    """
    def decorator(view_func: Callable) -> Callable:
        @wraps(view_func)
        def wrapper(request: Request, *args, **kwargs):
            if not request.user.is_authenticated:
                return Redirect("/login/")
            if not has_permission(request.user, perm):
                raise PermissionDenied(f"Permission '{perm}' required")
            return view_func(request, *args, **kwargs)
        return wrapper
    return decorator

def has_permission(user, perm: str) -> bool:
    """Check if user has permission (simplified)."""
    if user.is_superuser:
        return True
    return perm in getattr(user, 'permissions', [])


# =========================
# Example Views
# =========================

def public_page(request: Request) -> str:
    """No authentication required."""
    return "Public page: Anyone can access"

@login_required
def dashboard(request: Request) -> str:
    """Requires login."""
    return f"Dashboard: Welcome, {request.user.username}!"

@staff_member_required
def admin_panel(request: Request) -> str:
    """Requires staff status."""
    return "Admin panel: Staff only"

@permission_required('can_manage_users')
def manage_users(request: Request) -> str:
    """Requires specific permission."""
    return "Manage users: Permission required"


# =========================
# Demo
# =========================

# Create test users
@dataclass
class TestUser:
    username: str
    is_authenticated: bool = True
    is_staff: bool = False
    is_superuser: bool = False
    permissions: List[str] = None
    
    def __post_init__(self):
        if self.permissions is None:
            self.permissions = []

anon = TestUser(username="", is_authenticated=False)
regular = TestUser(username="alice")
staff = TestUser(username="bob", is_staff=True)
admin = TestUser(username="admin", is_superuser=True)

print("=== View Access Control Demo ===")

# Test public page
req = Request(user=anon, path="/")
print(f"\n1. {public_page(req)}")

# Test dashboard
print("\n2. Dashboard Access:")
for user in [anon, regular]:
    req = Request(user=user, path="/dashboard/")
    result = dashboard(req)
    if isinstance(result, Redirect):
        print(f"   {user.username or 'Anonymous'}: {result}")
    else:
        print(f"   {user.username}: {result}")

# Test admin panel
print("\n3. Admin Panel Access:")
for user in [regular, staff]:
    req = Request(user=user, path="/admin/")
    try:
        result = admin_panel(req)
        print(f"   {user.username}: {result}")
    except PermissionDenied as e:
        print(f"   {user.username}: Denied - {e}")

# Test permission-based access
print("\n4. Permission-Based Access:")
user_manager = TestUser(username="manager", permissions=['can_manage_users'])
for user in [regular, user_manager, admin]:
    req = Request(user=user, path="/users/")
    try:
        result = manage_users(req)
        print(f"   {user.username}: Access granted")
    except PermissionDenied as e:
        print(f"   {user.username}: Denied")

print("\nPublic page: Anyone can access")
print("Dashboard: Logged in users only")
print("Admin panel: Staff only")
print("Manage users: Permission required")
```
