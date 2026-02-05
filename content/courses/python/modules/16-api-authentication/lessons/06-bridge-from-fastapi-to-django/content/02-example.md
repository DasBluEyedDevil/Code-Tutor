---
type: "EXAMPLE"
title: "The Same Endpoint in FastAPI vs Django"
---

**Seeing is believing. Here's the same "create user" endpoint in both frameworks.**

**FastAPI Approach (explicit):**

```python
# main.py
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, EmailStr
from passlib.context import CryptContext

app = FastAPI()
pwd_context = CryptContext(schemes=["bcrypt"])

class UserCreate(BaseModel):
    email: EmailStr
    password: str

@app.post("/users")
async def create_user(user: UserCreate):
    # You handle everything explicitly
    hashed = pwd_context.hash(user.password)
    # You choose: SQLAlchemy, Tortoise, raw SQL...
    db_user = await save_user(user.email, hashed)
    return {"id": db_user.id, "email": db_user.email}
```

**Django Approach (convention-based):**

```python
# models.py
from django.db import models
from django.contrib.auth.models import AbstractUser

class User(AbstractUser):
    # Django provides: username, password, email, is_active, date_joined...
    # Just add your custom fields
    bio = models.TextField(blank=True)

# views.py
from rest_framework import generics
from rest_framework.permissions import AllowAny

class CreateUserView(generics.CreateAPIView):
    # Django REST Framework handles:
    # - Validation
    # - Password hashing
    # - Database save
    # - JSON serialization
    serializer_class = UserSerializer
    permission_classes = [AllowAny]
```

**What Django Does Automatically:**

- Password hashing (secure by default)
- Database schema from models (`python manage.py migrate`)
- Admin interface (just add `admin.site.register(User)`)
- CSRF protection on forms
- Session-based auth (in addition to token auth)

**The Tradeoff:**

| FastAPI | Django |
|---------|--------|
| ~20 lines to create user endpoint | ~10 lines (if you follow conventions) |
| Full control over every detail | Less control, but faster to build |
| You choose all dependencies | Dependencies are chosen for you |
| Async by default | Async is opt-in |

**Both Are Valid Choices:**
- FastAPI: When you need control and async performance
- Django: When you need speed and convention
