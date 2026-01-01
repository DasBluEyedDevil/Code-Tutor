---
type: "EXAMPLE"
title: "Setting Up DRF"
---

**Installing and Configuring Django REST Framework**

**1. Install the Package:**
```bash
uv add djangorestframework
# or: pip install djangorestframework
```

**2. Add to INSTALLED_APPS:**
DRF must be registered in your Django settings.

**3. Configure Default Settings:**
- `DEFAULT_PERMISSION_CLASSES` - Who can access your API
- `DEFAULT_AUTHENTICATION_CLASSES` - How users authenticate
- `DEFAULT_PAGINATION_CLASS` - How to paginate results

**Common Permission Classes:**
- `AllowAny` - No authentication required
- `IsAuthenticated` - Must be logged in
- `IsAuthenticatedOrReadOnly` - Read for anyone, write needs auth
- `IsAdminUser` - Only Django admin users

**Common Authentication Classes:**
- `SessionAuthentication` - Use Django sessions
- `TokenAuthentication` - API tokens
- `BasicAuthentication` - HTTP Basic Auth

```python
# Install
# uv add djangorestframework

# settings.py
INSTALLED_APPS = [
    'django.contrib.admin',
    'django.contrib.auth',
    'django.contrib.contenttypes',
    'django.contrib.sessions',
    'django.contrib.messages',
    'django.contrib.staticfiles',
    'rest_framework',  # Add this
    'tracker',          # Your app
]

REST_FRAMEWORK = {
    'DEFAULT_PERMISSION_CLASSES': [
        'rest_framework.permissions.IsAuthenticated',
    ],
    'DEFAULT_AUTHENTICATION_CLASSES': [
        'rest_framework.authentication.SessionAuthentication',
        'rest_framework.authentication.TokenAuthentication',
    ],
    'DEFAULT_PAGINATION_CLASS': 'rest_framework.pagination.PageNumberPagination',
    'PAGE_SIZE': 20,
}


print("=== Django REST Framework Setup ===")

print("\n1. Installation:")
print("   uv add djangorestframework")
print("   # or: pip install djangorestframework")

print("\n2. Add to INSTALLED_APPS:")
print("   'rest_framework',")

print("\n3. Common Settings:")
print("   - DEFAULT_PERMISSION_CLASSES: Who can access")
print("   - DEFAULT_AUTHENTICATION_CLASSES: How to authenticate")
print("   - DEFAULT_PAGINATION_CLASS: How to paginate")

print("\n4. Permission Options:")
print("   - AllowAny: Public access")
print("   - IsAuthenticated: Login required")
print("   - IsAuthenticatedOrReadOnly: Read public, write protected")
print("   - IsAdminUser: Admin only")
```
