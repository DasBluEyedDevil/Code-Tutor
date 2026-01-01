---
type: "EXAMPLE"
title: "Using sync_to_async"
---

**Bridging Sync and Async Code**

Not all code is async-ready. Use `sync_to_async` to safely call synchronous code from async views.

**When to Use sync_to_async:**

1. **Legacy ORM code** - Existing sync functions
2. **Third-party libraries** - Sync-only packages
3. **Complex queries** - Easier to write sync, wrap for async

**How It Works:**
Wraps the sync function to run in a thread pool, preventing it from blocking the event loop.

**thread_sensitive Parameter:**
- `True` (default): Runs in same thread (safe for Django ORM)
- `False`: Runs in any thread pool thread (faster for non-Django code)

**Performance Note:**
Thread pool overhead exists - don't wrap tiny operations. Batch related sync operations into one wrapped function when possible.

```python
from asgiref.sync import sync_to_async
from django.http import JsonResponse
from django.db.models import Sum, Avg

# Wrap existing sync functions
@sync_to_async
def get_user_summary(user):
    """Complex sync query - easier to write, wrap for async."""
    from .models import Transaction
    
    transactions = Transaction.objects.filter(user=user)
    
    return {
        "count": transactions.count(),
        "total_income": transactions.filter(
            amount__gt=0
        ).aggregate(total=Sum('amount'))['total'] or 0,
        "total_expenses": transactions.filter(
            amount__lt=0
        ).aggregate(total=Sum('amount'))['total'] or 0,
        "average": transactions.aggregate(
            avg=Avg('amount')
        )['avg'] or 0,
    }


async def user_summary_view(request):
    """Async view using sync helper."""
    summary = await get_user_summary(request.user)
    return JsonResponse(summary)


# Inline wrapping for one-off sync calls
async def mixed_operations(request):
    """Mix async ORM with wrapped sync code."""
    from .models import Transaction
    
    # Native async ORM
    latest = await Transaction.objects.filter(
        user=request.user
    ).order_by('-created_at').afirst()
    
    # Wrap sync operation inline
    @sync_to_async
    def complex_calculation():
        # Sync code that's hard to convert
        return calculate_monthly_trends(request.user)
    
    trends = await complex_calculation()
    
    return JsonResponse({
        "latest": latest.to_dict() if latest else None,
        "trends": trends
    })


# Working with sync libraries
@sync_to_async(thread_sensitive=False)
def call_sync_api_client(endpoint):
    """Wrap a sync HTTP client."""
    import requests  # Sync library
    response = requests.get(endpoint)
    return response.json()


async def fetch_external_data(request):
    """Use sync library in async view."""
    import asyncio
    
    # Run multiple sync API calls concurrently
    results = await asyncio.gather(
        call_sync_api_client("https://api.example.com/data1"),
        call_sync_api_client("https://api.example.com/data2"),
    )
    
    return JsonResponse({"data": results})


print("=== sync_to_async ===")

print("\nDecorator Style:")
print("  @sync_to_async")
print("  def sync_function():")
print("      return sync_operation()")

print("\nInline Style:")
print("  wrapped = sync_to_async(sync_func)")
print("  result = await wrapped()")

print("\nthread_sensitive:")
print("  True (default) - Same thread, safe for Django ORM")
print("  False - Any thread, faster for non-Django code")
```
