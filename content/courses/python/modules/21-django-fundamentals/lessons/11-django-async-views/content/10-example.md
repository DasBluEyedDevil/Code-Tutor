---
type: "EXAMPLE"
title: "Finance Tracker: Async Dashboard"
---

**Real-World Example: Async Dashboard for Finance Tracker**

This example shows how to build an async dashboard that fetches multiple data sources concurrently.

**The Scenario:**
A finance dashboard needs to display:
- User's recent transactions
- Spending by category
- Account balance
- External exchange rates (API call)

**Sync Approach:** ~400ms (100ms each, sequential)
**Async Approach:** ~100ms (all concurrent)

**Architecture:**
1. Async view coordinates data fetching
2. Each data source has its own async helper
3. asyncio.gather() runs all concurrently
4. Results combined into single response

```python
import asyncio
from django.http import JsonResponse
from django.contrib.auth.decorators import login_required
from asgiref.sync import sync_to_async
from django.db.models import Sum
from decimal import Decimal


# Async helper functions
async def get_recent_transactions(user, limit=10):
    """Fetch recent transactions asynchronously."""
    from .models import Transaction
    
    transactions = []
    async for tx in Transaction.objects.filter(
        user=user
    ).order_by('-created_at')[:limit]:
        transactions.append(tx.to_dict())
    
    return transactions


async def get_spending_by_category(user):
    """Get spending breakdown by category."""
    from .models import Transaction
    
    # Complex aggregation - wrap in sync_to_async
    @sync_to_async
    def aggregate_spending():
        return list(
            Transaction.objects.filter(
                user=user,
                amount__lt=0  # Expenses are negative
            ).values('category').annotate(
                total=Sum('amount')
            ).order_by('total')[:5]
        )
    
    return await aggregate_spending()


async def get_account_balance(user):
    """Calculate current account balance."""
    from .models import Transaction
    
    @sync_to_async
    def calculate_balance():
        result = Transaction.objects.filter(
            user=user
        ).aggregate(balance=Sum('amount'))
        return result['balance'] or Decimal('0')
    
    return await calculate_balance()


async def get_exchange_rates():
    """Fetch exchange rates from external API."""
    # In production: use aiohttp or httpx
    # Simulating async API call
    await asyncio.sleep(0.05)
    return {
        "USD": 1.0,
        "EUR": 0.85,
        "GBP": 0.73,
    }


@login_required
async def async_dashboard(request):
    """
    Async dashboard that fetches all data concurrently.
    
    Performance comparison:
    - Sync: ~400ms (100ms * 4 operations)
    - Async: ~100ms (all run concurrently)
    """
    user = request.user
    
    # Fetch all data concurrently
    transactions, categories, balance, rates = await asyncio.gather(
        get_recent_transactions(user),
        get_spending_by_category(user),
        get_account_balance(user),
        get_exchange_rates(),
    )
    
    return JsonResponse({
        "transactions": transactions,
        "spending_by_category": categories,
        "balance": str(balance),
        "exchange_rates": rates,
    })


# URL configuration (unchanged from sync views)
# urls.py
from django.urls import path
from . import views

urlpatterns = [
    path('dashboard/', views.async_dashboard, name='dashboard'),
]


print("=== Finance Tracker Async Dashboard ===")

print("\nData Sources (concurrent):")
print("  1. Recent transactions (async ORM)")
print("  2. Spending by category (sync_to_async)")
print("  3. Account balance (sync_to_async)")
print("  4. Exchange rates (external API)")

print("\nPerformance:")
print("  Sync:  ~400ms (sequential)")
print("  Async: ~100ms (concurrent)")

print("\nPattern:")
print("  results = await asyncio.gather(")
print("      fetch_data_1(),")
print("      fetch_data_2(),")
print("      fetch_data_3(),")
print("  )")
```
