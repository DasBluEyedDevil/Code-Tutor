---
type: "WARNING"
title: "Async ORM Gotcha: Iteration"
---

**Do NOT use list() or iteration directly on async querysets!**

**Wrong - This will raise SynchronousOnlyOperation:**
```python
async def bad_view(request):
    # This tries to iterate synchronously!
    transactions = list(Transaction.objects.all())  # ERROR!
```

**Right - Use aall() for async iteration:**
```python
async def good_view(request):
    # Option 1: Async for loop
    async for tx in Transaction.objects.all():
        process(tx)
    
    # Option 2: Collect into list
    transactions = []
    async for tx in Transaction.objects.all():
        transactions.append(tx)
    
    # Option 3: List comprehension (Django 5.2+)
    transactions = [tx async for tx in Transaction.objects.all()]
```

**Why This Happens:**
Async views run in an async context where sync database access is blocked by default to prevent accidental blocking.