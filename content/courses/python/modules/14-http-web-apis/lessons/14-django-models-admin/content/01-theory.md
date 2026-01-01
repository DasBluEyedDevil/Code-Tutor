---
type: "THEORY"
title: "Django ORM"
---

**Django ORM - Simpler Database Operations**

Django's ORM (Object-Relational Mapper) lets you work with databases using Python classes instead of raw SQL.

**Key Concepts:**

**1. Models = Python Classes That Map to Database Tables**
```python
class Category(models.Model):
    name = models.CharField(max_length=50)
```
This creates a `category` table with an auto-generated `id` and a `name` column.

**2. Simpler Than SQLAlchemy (More Opinionated)**

| SQLAlchemy | Django |
|------------|--------|
| Requires explicit session management | Automatic connection handling |
| Multiple query syntaxes | One consistent API |
| Flexible but complex | Convention over configuration |

Django makes decisions for you - less flexibility, but faster development.

**3. Migrations Handled Automatically**

```bash
python manage.py makemigrations  # Detect model changes
python manage.py migrate          # Apply to database
```

Django generates migration files automatically when you change your models.

**Common Field Types:**
- `CharField(max_length=N)` - Short text
- `TextField()` - Long text
- `IntegerField()` - Whole numbers
- `DecimalField(max_digits, decimal_places)` - Money/precise decimals
- `BooleanField()` - True/False
- `DateTimeField()` - Date and time
- `ForeignKey(Model, on_delete=...)` - Relationships