---
type: "EXAMPLE"
title: "Basic Async View Syntax"
---

**Writing Your First Async View**

Async views in Django look just like regular views, but use `async def` and can `await` async operations.

**Key Points:**

1. **Use `async def`** - The view function must be async
2. **Use `await`** - For any async operations inside
3. **Return normal responses** - JsonResponse, HttpResponse work the same
4. **URL routing unchanged** - No special URL configuration needed

**Running Async Django:**

You need an ASGI server instead of WSGI:
```bash
# Development
uvicorn myproject.asgi:application --reload

# Production
uvicorn myproject.asgi:application --workers 4
```

**ASGI vs WSGI:**
- WSGI (gunicorn): Sync only, one request per worker
- ASGI (uvicorn): Async support, many requests per worker

**Mixed Sync/Async is Fine:**
Django handles sync views under ASGI automatically - you don't need to convert everything at once.

```python
# views.py
import asyncio
from django.http import JsonResponse

# Basic async view
async def health_check(request):
    """Simple async endpoint - no await needed."""
    return JsonResponse({
        "status": "healthy",
        "async": True
    })


# Async view with await
async def fetch_data(request):
    """Async view that awaits an operation."""
    # Simulate async I/O (API call, file read, etc.)
    await asyncio.sleep(0.1)
    
    return JsonResponse({
        "message": "Data fetched asynchronously"
    })


# Concurrent operations
async def dashboard_data(request):
    """Fetch multiple data sources concurrently."""
    
    async def get_user_stats():
        await asyncio.sleep(0.05)  # Simulated API call
        return {"users": 1000, "active": 250}
    
    async def get_transaction_stats():
        await asyncio.sleep(0.05)  # Simulated API call
        return {"total": 5000, "today": 42}
    
    # Run both concurrently
    user_stats, tx_stats = await asyncio.gather(
        get_user_stats(),
        get_transaction_stats()
    )
    
    return JsonResponse({
        "users": user_stats,
        "transactions": tx_stats
    })


print("=== Django Async Views ===")

print("\nBasic Async View:")
print("  async def my_view(request):")
print("      return JsonResponse({...})")

print("\nWith Await:")
print("  async def my_view(request):")
print("      data = await async_operation()")
print("      return JsonResponse(data)")

print("\nConcurrent Calls:")
print("  results = await asyncio.gather(")
print("      api_call_1(),")
print("      api_call_2()")
print("  )")

print("\nRunning ASGI:")
print("  uvicorn myproject.asgi:application --reload")
```
