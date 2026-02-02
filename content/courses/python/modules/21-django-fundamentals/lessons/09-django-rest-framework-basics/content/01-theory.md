---
type: "THEORY"
title: "Introduction to Django REST Framework"
---

Django REST Framework (DRF) is a powerful toolkit for building Web APIs with Django. It extends Django's capabilities to easily create RESTful APIs.

**Why Use DRF Instead of Plain Django?**

- **Serialization** - Convert models to JSON/XML automatically
- **Authentication** - Token, Session, OAuth2 support
- **Permissions** - Fine-grained API access control
- **Browsable API** - Interactive documentation/testing
- **Throttling** - Rate limiting built-in
- **Pagination** - Automatic result pagination
- **ViewSets & Routers** - Less boilerplate code

**Core Concepts:**

1. **Serializers** - Convert between Python objects and JSON
   - Like Django Forms but for APIs
   - Handle validation and nested data

2. **Views/ViewSets** - Handle HTTP requests
   - APIView (function-based and class-based)
   - ViewSets (CRUD with minimal code)

3. **Routers** - Automatic URL configuration
   - Maps ViewSets to URLs automatically

4. **Authentication/Permissions**
   - Who can access (authentication)
   - What they can do (permissions)

**Installation:**
```bash
pip install djangorestframework

# settings.py
INSTALLED_APPS = [
    ...
    'rest_framework',
]
```