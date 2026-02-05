---
type: "WARNING"
title: "Don't Try to Use FastAPI Patterns in Django"
---

**Common Mistakes When Transitioning:**

❌ **Don't fight Django's conventions**

```python
# WRONG: Trying to use FastAPI patterns in Django
class UserView(View):
    async def post(self, request):
        data = json.loads(request.body)
        user = User(**data)  # No validation!
        user.save()
        return JsonResponse({"id": user.id})

# RIGHT: Embrace Django REST Framework
class UserViewSet(viewsets.ModelViewSet):
    queryset = User.objects.all()
    serializer_class = UserSerializer
    # DRF handles validation, serialization, CRUD, permissions
```

❌ **Don't ignore Django's built-in auth**

```python
# WRONG: Building auth from scratch like in FastAPI
from passlib.context import CryptContext
# ... 50 lines of custom auth code

# RIGHT: Use Django's auth
from django.contrib.auth import authenticate, login
user = authenticate(request, username=username, password=password)
if user:
    login(request, user)
```

❌ **Don't skip the admin interface**

You spent weeks building CRUD views in FastAPI. In Django:

```python
# admin.py - 3 lines!
from django.contrib import admin
from .models import BlogPost

admin.site.register(BlogPost)
# Now you have full CRUD at /admin/
```

**Migration Pitfalls:**

| FastAPI Habit | Django Reality |
|---------------|----------------|
| `async def` everywhere | Django views are sync by default (async opt-in) |
| Explicit dependency injection | Use middleware, decorators, mixins |
| Pydantic for everything | Django models + DRF serializers |
| SQLAlchemy sessions | Django ORM is simpler (no explicit sessions) |

**The Rule:**

When in Rome, do as the Romans do. When in Django, follow Django conventions.

Fighting the framework creates more work, not less.
