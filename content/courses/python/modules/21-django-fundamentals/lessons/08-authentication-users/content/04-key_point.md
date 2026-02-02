---
type: "KEY_POINT"
title: "Custom User Model"
---

**Always Use a Custom User Model in New Projects!**

Django strongly recommends creating a custom user model before your first migration:

```python
# accounts/models.py
from django.contrib.auth.models import AbstractUser

class User(AbstractUser):
    # Add custom fields
    phone = models.CharField(max_length=20, blank=True)
    avatar = models.ImageField(upload_to='avatars/', blank=True)
    
    # Or extend with a Profile model for more flexibility
```

```python
# settings.py
AUTH_USER_MODEL = 'accounts.User'  # app_name.ModelName
```

**Why Custom User Model?**
- Changing user model later requires complex migrations
- Add fields like phone, avatar, preferences easily
- Use email as username (common requirement)
- Add custom authentication methods

**AbstractUser vs AbstractBaseUser:**
- `AbstractUser` - Includes all default fields, easy to extend
- `AbstractBaseUser` - Bare minimum, full control but more work

**Referencing the User Model:**
```python
# BAD: Hardcoded import
from django.contrib.auth.models import User

# GOOD: Works with custom user model
from django.contrib.auth import get_user_model
User = get_user_model()

# GOOD: In ForeignKey
from django.conf import settings
owner = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.CASCADE)
```