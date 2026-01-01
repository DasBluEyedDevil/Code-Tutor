---
type: "EXAMPLE"
title: "Quick Django Setup"
---

**Getting Started with Django:**

```bash
# Install Django
uv add django

# Create project
django-admin startproject finance_project
cd finance_project

# Create app
python manage.py startapp tracker

# Run development server
python manage.py runserver
```

**After Running These Commands:**

1. Visit http://127.0.0.1:8000/ to see the welcome page
2. Visit http://127.0.0.1:8000/admin/ for the admin interface

**Next Steps:**

```python
# 1. Add app to settings.py
INSTALLED_APPS = [
    ...
    'tracker',
]

# 2. Create a model (tracker/models.py)
from django.db import models

class Expense(models.Model):
    description = models.CharField(max_length=200)
    amount = models.DecimalField(max_digits=10, decimal_places=2)
    date = models.DateField(auto_now_add=True)

# 3. Create migrations
python manage.py makemigrations
python manage.py migrate

# 4. Create superuser for admin
python manage.py createsuperuser

# 5. Register in admin (tracker/admin.py)
from django.contrib import admin
from .models import Expense
admin.site.register(Expense)
```

**You Now Have:**
- A running web server
- Admin interface with login
- Database with your Expense model
- Full CRUD via admin panel