---
type: "THEORY"
title: "Django ORM Fundamentals"
---

The Django ORM (Object-Relational Mapper) lets you define database tables as Python classes and query them without writing SQL.

**Model = Database Table**
```python
class Transaction(models.Model):  # Creates 'transactions_transaction' table
    amount = models.DecimalField()  # Creates 'amount' column
    description = models.CharField()  # Creates 'description' column
```

**Common Field Types:**
- `CharField(max_length=N)` - Short text (VARCHAR)
- `TextField()` - Long text (TEXT)
- `IntegerField()` - Whole numbers
- `DecimalField(max_digits, decimal_places)` - Money/precision
- `FloatField()` - Floating point (avoid for money!)
- `BooleanField()` - True/False
- `DateField()` / `DateTimeField()` - Dates and times
- `EmailField()` - Validated email
- `ForeignKey()` - Many-to-one relationship
- `ManyToManyField()` - Many-to-many relationship
- `OneToOneField()` - One-to-one relationship

**Field Options:**
- `null=True` - Allows NULL in database
- `blank=True` - Allows empty in forms
- `default=value` - Default value
- `unique=True` - No duplicates allowed
- `choices=[(val, label)]` - Dropdown options
- `auto_now_add=True` - Set once on creation
- `auto_now=True` - Update on every save