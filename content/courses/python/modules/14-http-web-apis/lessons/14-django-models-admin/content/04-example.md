---
type: "EXAMPLE"
title: "Django Admin Setup"
---

**The Django Admin - Free CRUD Interface**

Django automatically generates an admin interface for your models.

**Basic Registration:**
```python
from django.contrib import admin
from .models import Category
admin.site.register(Category)
```

**Advanced Configuration with ModelAdmin:**

- **list_display** - Columns shown in list view
- **list_filter** - Sidebar filters
- **search_fields** - Searchable fields
- **date_hierarchy** - Date-based navigation
- **ordering** - Default sort order
- **readonly_fields** - Non-editable fields

**The `@admin.register` Decorator:**
```python
@admin.register(Category)
class CategoryAdmin(admin.ModelAdmin):
    list_display = ['name', 'description']
```
Cleaner than `admin.site.register(Category, CategoryAdmin)`.

**Setup Steps:**
1. Create superuser: `python manage.py createsuperuser`
2. Run server: `python manage.py runserver`
3. Visit: http://127.0.0.1:8000/admin/
4. Log in with superuser credentials

```python
# tracker/admin.py
from django.contrib import admin
from .models import Category, Transaction

@admin.register(Category)
class CategoryAdmin(admin.ModelAdmin):
    """Admin configuration for Category model."""
    list_display = ['name', 'description']
    search_fields = ['name']

@admin.register(Transaction)
class TransactionAdmin(admin.ModelAdmin):
    """Admin configuration for Transaction model."""
    list_display = ['user', 'amount', 'category', 'transaction_type', 'created_at']
    list_filter = ['transaction_type', 'category', 'created_at']
    search_fields = ['description']
    date_hierarchy = 'created_at'


print("=== Django Admin Configuration ===")

print("\nCategoryAdmin options:")
print("  list_display: Shows name and description in list")
print("  search_fields: Enables search by name")

print("\nTransactionAdmin options:")
print("  list_display: Shows user, amount, category, type, date")
print("  list_filter: Adds sidebar filters for type, category, date")
print("  search_fields: Enables search by description")
print("  date_hierarchy: Adds date-based drill-down navigation")

print("\n=== Admin Setup Commands ===")
print("  1. python manage.py createsuperuser")
print("     # Create admin login credentials")
print("")
print("  2. python manage.py runserver")
print("     # Start development server")
print("")
print("  3. Visit http://127.0.0.1:8000/admin/")
print("     # Access the admin interface")

print("\n=== What You Get ===")
print("  - Full CRUD interface (Create, Read, Update, Delete)")
print("  - Search, filter, and sort capabilities")
print("  - Pagination for large datasets")
print("  - Inline editing for related objects")
print("  - History tracking for changes")
print("  - Permission-based access control")
```
