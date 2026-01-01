---
type: "EXAMPLE"
title: "Django 5.2 Async ORM Methods"
---

**New Async ORM Methods in Django 5.2**

Django 5.2 introduces truly async database operations. These methods perform the actual database query asynchronously.

**Async QuerySet Methods:**

| Sync | Async | Returns |
|------|-------|--------|
| `.all()` + iterate | `.aall()` | Async generator |
| `.get()` | `.aget()` | Single object |
| `.first()` | `.afirst()` | First object or None |
| `.last()` | `.alast()` | Last object or None |
| `.count()` | `.acount()` | Integer |
| `.exists()` | `.aexists()` | Boolean |
| `.create()` | `.acreate()` | Created object |
| `.update()` | `.aupdate()` | Rows affected |
| `.delete()` | `.adelete()` | Tuple (count, details) |
| `.get_or_create()` | `.aget_or_create()` | (obj, created) |
| `.update_or_create()` | `.aupdate_or_create()` | (obj, created) |

**Important:** Filter chains are still sync - only the terminal methods are async:
```python
# Filtering is sync (builds query)
queryset = Transaction.objects.filter(user=user)

# Terminal method is async (executes query)
transactions = await queryset.aall()
```

**Database Backend Support:**
- PostgreSQL: Full async support via asyncpg/psycopg3
- SQLite: Runs in thread pool (aiosqlite)
- MySQL: Limited async support

```python
# models.py
from django.db import models
from django.contrib.auth.models import User

class Transaction(models.Model):
    user = models.ForeignKey(User, on_delete=models.CASCADE)
    amount = models.DecimalField(max_digits=10, decimal_places=2)
    description = models.CharField(max_length=200)
    category = models.CharField(max_length=50)
    created_at = models.DateTimeField(auto_now_add=True)
    
    def to_dict(self):
        return {
            "id": self.id,
            "amount": str(self.amount),
            "description": self.description,
            "category": self.category,
        }


# views.py - Django 5.2 Async ORM
from django.http import JsonResponse

async def get_transactions(request):
    """Fetch user's transactions using async ORM."""
    # aall() returns an async iterable
    transactions = []
    async for tx in Transaction.objects.filter(
        user=request.user
    ).order_by('-created_at')[:50]:
        transactions.append(tx.to_dict())
    
    return JsonResponse({"transactions": transactions})


async def get_transaction_stats(request):
    """Get transaction statistics using async methods."""
    user_txs = Transaction.objects.filter(user=request.user)
    
    # Async count and existence check
    total_count = await user_txs.acount()
    has_transactions = await user_txs.aexists()
    
    # Async first/last
    latest = await user_txs.order_by('-created_at').afirst()
    oldest = await user_txs.order_by('created_at').afirst()
    
    return JsonResponse({
        "total": total_count,
        "has_transactions": has_transactions,
        "latest": latest.to_dict() if latest else None,
        "oldest": oldest.to_dict() if oldest else None,
    })


async def create_transaction(request):
    """Create transaction using async ORM."""
    import json
    data = json.loads(request.body)
    
    # Async create
    tx = await Transaction.objects.acreate(
        user=request.user,
        amount=data["amount"],
        description=data["description"],
        category=data["category"]
    )
    
    return JsonResponse(tx.to_dict(), status=201)


print("=== Django 5.2 Async ORM ===")

print("\nAsync QuerySet Methods:")
print("  aall()    - Async iteration")
print("  aget()    - Single object")
print("  afirst()  - First or None")
print("  acount()  - Count rows")
print("  aexists() - Check existence")
print("  acreate() - Create object")

print("\nUsage Pattern:")
print("  # Build query (sync)")
print("  qs = Model.objects.filter(user=user)")
print("  # Execute query (async)")
print("  results = await qs.aall()")
```
