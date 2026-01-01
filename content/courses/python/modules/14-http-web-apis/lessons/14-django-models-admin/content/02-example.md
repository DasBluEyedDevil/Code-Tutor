---
type: "EXAMPLE"
title: "Creating Models"
---

**Building a Finance Tracker with Django Models**

Let's create models for tracking personal finances:

**Key Patterns:**

1. **Model Meta Class** - Configure model behavior
```python
class Meta:
    verbose_name_plural = "categories"  # Admin display name
```

2. **`__str__` Method** - Human-readable representation
```python
def __str__(self):
    return self.name  # Shows in admin and shell
```

3. **Choices for Fixed Options**
```python
TRANSACTION_TYPES = [
    ('income', 'Income'),
    ('expense', 'Expense'),
]
```

4. **ForeignKey Relationships**
```python
user = models.ForeignKey(User, on_delete=models.CASCADE)
# CASCADE: Delete transactions if user is deleted
# SET_NULL: Set to null if related object deleted
# PROTECT: Prevent deletion if related objects exist
```

5. **Auto Timestamps**
```python
created_at = models.DateTimeField(auto_now_add=True)  # Set on create
updated_at = models.DateTimeField(auto_now=True)      # Update on save
```

```python
# tracker/models.py
from django.db import models
from django.contrib.auth.models import User

class Category(models.Model):
    """Expense/Income category for organizing transactions."""
    name = models.CharField(max_length=50)
    description = models.TextField(blank=True)
    
    class Meta:
        verbose_name_plural = "categories"
    
    def __str__(self):
        return self.name

class Transaction(models.Model):
    """Financial transaction record."""
    TRANSACTION_TYPES = [
        ('income', 'Income'),
        ('expense', 'Expense'),
    ]
    
    user = models.ForeignKey(User, on_delete=models.CASCADE)
    amount = models.DecimalField(max_digits=10, decimal_places=2)
    category = models.ForeignKey(Category, on_delete=models.SET_NULL, null=True)
    transaction_type = models.CharField(max_length=10, choices=TRANSACTION_TYPES)
    description = models.TextField(blank=True)
    created_at = models.DateTimeField(auto_now_add=True)
    
    def __str__(self):
        return f"{self.transaction_type}: ${self.amount}"


# Example usage in Django shell:
print("=== Django Models Example ===")
print("\nModel definitions created:")
print("  - Category: name, description")
print("  - Transaction: user, amount, category, type, description, created_at")

print("\nField types used:")
print("  - CharField: Short text with max length")
print("  - TextField: Long text (blank=True for optional)")
print("  - DecimalField: Precise decimal numbers (for money)")
print("  - ForeignKey: Relationship to another model")
print("  - DateTimeField: Timestamps (auto_now_add for creation)")

print("\nRelationship behaviors:")
print("  - CASCADE: Delete related objects when parent deleted")
print("  - SET_NULL: Set to null when related object deleted")
print("  - PROTECT: Prevent deletion if related objects exist")
```
