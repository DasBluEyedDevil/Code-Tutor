---
type: "WARNING"
title: "Common ORM Pitfalls"
---

**1. N+1 Query Problem:**
```python
# BAD: 1 query + N queries for accounts
for t in Transaction.objects.all():
    print(t.account.name)  # Hits DB each time!

# GOOD: 2 queries total with select_related
for t in Transaction.objects.select_related('account').all():
    print(t.account.name)  # Already loaded
```

**2. Using FloatField for Money:**
```python
# BAD: Floating point errors
amount = models.FloatField()  # 0.1 + 0.2 != 0.3

# GOOD: Exact decimal representation
amount = models.DecimalField(max_digits=10, decimal_places=2)
```

**3. Missing on_delete:**
```python
# What happens when related object is deleted?
account = models.ForeignKey(Account, on_delete=models.CASCADE)  # Delete related
category = models.ForeignKey(Category, on_delete=models.PROTECT)  # Prevent deletion
user = models.ForeignKey(User, on_delete=models.SET_NULL, null=True)  # Set to NULL
```