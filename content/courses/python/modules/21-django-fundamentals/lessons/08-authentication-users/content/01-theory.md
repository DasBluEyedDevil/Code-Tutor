---
type: "THEORY"
title: "Django Authentication System"
---

Django provides a complete authentication system out of the box, handling user registration, login, logout, password management, and permissions.

**The User Model:**

Django's built-in `User` model includes:
- `username` - Unique identifier
- `password` - Hashed password (never stored in plaintext)
- `email` - Email address
- `first_name` / `last_name` - Name fields
- `is_active` - Account enabled/disabled
- `is_staff` - Can access admin site
- `is_superuser` - Full permissions
- `date_joined` - Registration timestamp

**Authentication vs Authorization:**
- **Authentication** - "Who are you?" (login, verify identity)
- **Authorization** - "What can you do?" (permissions, access control)

**Key Components:**

1. **User Model** - `django.contrib.auth.models.User`
2. **Authentication Backends** - Verify credentials
3. **Middleware** - Attach user to request
4. **Decorators/Mixins** - Protect views
5. **Permissions** - Fine-grained access control

**The Request.User Object:**
```python
def my_view(request):
    if request.user.is_authenticated:
        # User is logged in
        print(request.user.username)
    else:
        # Anonymous user
        pass
```