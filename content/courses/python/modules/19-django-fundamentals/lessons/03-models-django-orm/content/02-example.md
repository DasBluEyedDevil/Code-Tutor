---
type: "EXAMPLE"
title: "Finance Tracker Models"
---

Complete model definitions for our finance tracker:

**Expected Output:**
```
Models defined: Category, Account, Transaction
Relationships: Transaction -> Category, Account -> User
```

```python
from django.db import models
from django.contrib.auth.models import User
from decimal import Decimal

class Category(models.Model):
    """Budget category for transactions (Food, Rent, Salary, etc.)"""
    
    class CategoryType(models.TextChoices):
        INCOME = 'INC', 'Income'
        EXPENSE = 'EXP', 'Expense'
    
    name = models.CharField(max_length=50)
    type = models.CharField(
        max_length=3,
        choices=CategoryType.choices,
        default=CategoryType.EXPENSE
    )
    icon = models.CharField(max_length=50, blank=True)  # emoji or icon name
    color = models.CharField(max_length=7, default='#6B7280')  # hex color
    
    class Meta:
        verbose_name_plural = 'Categories'
        ordering = ['name']
    
    def __str__(self):
        return f"{self.icon} {self.name}" if self.icon else self.name


class Account(models.Model):
    """Bank account or wallet."""
    
    user = models.ForeignKey(
        User,
        on_delete=models.CASCADE,  # Delete accounts when user deleted
        related_name='accounts'
    )
    name = models.CharField(max_length=100)
    balance = models.DecimalField(
        max_digits=12,
        decimal_places=2,
        default=Decimal('0.00')
    )
    is_active = models.BooleanField(default=True)
    created_at = models.DateTimeField(auto_now_add=True)
    
    class Meta:
        unique_together = ['user', 'name']  # No duplicate account names per user
    
    def __str__(self):
        return f"{self.name} ({self.balance:,.2f})"


class Transaction(models.Model):
    """Individual income or expense."""
    
    account = models.ForeignKey(
        Account,
        on_delete=models.CASCADE,
        related_name='transactions'
    )
    category = models.ForeignKey(
        Category,
        on_delete=models.PROTECT,  # Prevent deleting used categories
        related_name='transactions'
    )
    amount = models.DecimalField(max_digits=10, decimal_places=2)
    description = models.CharField(max_length=200)
    date = models.DateField()
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)
    
    class Meta:
        ordering = ['-date', '-created_at']  # Newest first
        indexes = [
            models.Index(fields=['date']),  # Speed up date queries
            models.Index(fields=['account', 'date']),
        ]
    
    def __str__(self):
        return f"{self.date}: {self.description} ({self.amount})"
    
    @property
    def is_expense(self):
        return self.category.type == Category.CategoryType.EXPENSE

print("Models defined: Category, Account, Transaction")
print("Relationships: Transaction -> Category, Account -> User")
```
