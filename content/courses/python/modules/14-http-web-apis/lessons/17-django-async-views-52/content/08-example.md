---
type: "EXAMPLE"
title: "Async Middleware"
---

**Writing Async Middleware**

Django supports async middleware that can process requests without blocking.

**Two Types of Middleware:**

1. **Sync middleware** - Traditional, uses `__call__`
2. **Async middleware** - Uses `__acall__` for async processing

**Django Detects Automatically:**
- If your middleware has `__acall__`, it's treated as async
- If only `__call__`, it runs in thread pool for async views

**Best Practice:**
Implement both `__call__` and `__acall__` for compatibility with mixed sync/async views.

```python
import time
import asyncio
from django.http import JsonResponse


class AsyncTimingMiddleware:
    """
    Middleware that times request processing.
    Works with both sync and async views.
    """
    
    def __init__(self, get_response):
        self.get_response = get_response
        # Check if get_response is async
        self.async_mode = asyncio.iscoroutinefunction(get_response)
    
    def __call__(self, request):
        """Sync request handling."""
        start = time.perf_counter()
        response = self.get_response(request)
        duration = time.perf_counter() - start
        response['X-Request-Duration'] = f"{duration:.4f}s"
        return response
    
    async def __acall__(self, request):
        """Async request handling."""
        start = time.perf_counter()
        response = await self.get_response(request)
        duration = time.perf_counter() - start
        response['X-Request-Duration'] = f"{duration:.4f}s"
        return response


class AsyncRateLimitMiddleware:
    """
    Simple async rate limiting middleware.
    Demonstrates async middleware with await.
    """
    
    def __init__(self, get_response):
        self.get_response = get_response
        self.request_counts = {}  # Simple in-memory store
    
    async def __acall__(self, request):
        """Check rate limit asynchronously."""
        # Get client IP
        ip = request.META.get('REMOTE_ADDR')
        
        # Simple rate check (use Redis in production)
        current_count = self.request_counts.get(ip, 0)
        
        if current_count >= 100:  # 100 requests limit
            return JsonResponse(
                {"error": "Rate limit exceeded"},
                status=429
            )
        
        self.request_counts[ip] = current_count + 1
        
        # Continue to view
        response = await self.get_response(request)
        return response
    
    def __call__(self, request):
        """Sync fallback."""
        # For sync views, run without async rate limiting
        return self.get_response(request)


# settings.py
MIDDLEWARE = [
    'django.middleware.security.SecurityMiddleware',
    'myapp.middleware.AsyncTimingMiddleware',  # Add async middleware
    'django.contrib.sessions.middleware.SessionMiddleware',
    # ... other middleware
]


print("=== Async Middleware ===")

print("\nMiddleware Structure:")
print("  class AsyncMiddleware:")
print("      def __init__(self, get_response):")
print("          self.get_response = get_response")
print("")
print("      def __call__(self, request):  # Sync")
print("          return self.get_response(request)")
print("")
print("      async def __acall__(self, request):  # Async")
print("          return await self.get_response(request)")

print("\nDjango Auto-Detection:")
print("  - Has __acall__? -> Async middleware")
print("  - Only __call__? -> Sync (runs in thread pool)")
```
