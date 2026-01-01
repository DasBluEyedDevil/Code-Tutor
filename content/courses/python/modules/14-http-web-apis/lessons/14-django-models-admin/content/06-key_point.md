---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Django Models:**
- Models are Python classes that map to database tables
- Use `models.CharField`, `DecimalField`, `ForeignKey`, etc. for columns
- `__str__` method defines how objects display in admin
- `class Meta` configures model behavior (ordering, plural name, etc.)

**Migrations:**
- `makemigrations` detects model changes and creates migration files
- `migrate` applies changes to the database
- `sqlmigrate` shows the generated SQL
- Always commit migration files to version control

**Admin Interface:**
- Register models with `@admin.register(Model)` decorator
- `ModelAdmin` class customizes the admin display
- `list_display`, `list_filter`, `search_fields` for list view
- Free CRUD interface with minimal code

**ORM Queries:**
- `Model.objects.all()` gets all records
- `filter()` and `exclude()` for WHERE clauses
- Double-underscore syntax for lookups: `__gte`, `__contains`, `__isnull`
- Chain methods: `.filter().order_by().limit()`
- Use `aggregate()` and `annotate()` for SQL aggregations