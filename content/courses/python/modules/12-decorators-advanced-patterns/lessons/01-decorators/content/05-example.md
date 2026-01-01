---
type: "EXAMPLE"
title: "Code Example: Modern Caching with @cache and @lru_cache"
---

**Python's Built-in Caching Decorators:**

Python provides two powerful caching decorators in functools:

**1. @cache (Python 3.9+):**
- Simple, unbounded cache
- Caches ALL results forever
- Perfect when memory isn't a concern
- Simpler than @lru_cache

**2. @lru_cache:**
- Least Recently Used cache
- Can limit cache size
- Evicts oldest entries when full
- Use when memory is constrained

**Personal Finance Tracker Example:**
We'll use caching to optimize expensive calculations like computing compound interest or fetching exchange rates.

```python
from functools import cache, lru_cache
import time

# ============================================
# Personal Finance Tracker - Caching Example
# ============================================

print("=== @cache vs @lru_cache ===")
print("\n--- Fibonacci with @cache (unbounded) ---")

@cache  # Python 3.9+ - simple, unbounded cache
def fibonacci(n: int) -> int:
    """Calculate nth Fibonacci number.
    
    @cache stores ALL results forever - perfect for 
    pure functions with limited input range.
    """
    if n < 2:
        return n
    return fibonacci(n - 1) + fibonacci(n - 2)

# Without cache: O(2^n) - exponentially slow
# With cache: O(n) - each value computed once!
start = time.time()
result = fibonacci(35)
print(f"fibonacci(35) = {result:,}")
print(f"Time: {time.time() - start:.4f}s")
print(f"Cache info: {fibonacci.cache_info()}")

# Clear cache if needed
fibonacci.cache_clear()
print("Cache cleared!")

print("\n--- Exchange Rate Lookup with @lru_cache ---")

@lru_cache(maxsize=128)  # Keep only last 128 lookups
def get_exchange_rate(from_currency: str, to_currency: str) -> float:
    """Simulated exchange rate lookup.
    
    @lru_cache with maxsize limits memory usage.
    Old entries are evicted when cache is full.
    """
    # Simulate expensive API call
    time.sleep(0.01)
    rates = {
        ('USD', 'EUR'): 0.92,
        ('USD', 'GBP'): 0.79,
        ('EUR', 'USD'): 1.09,
        ('GBP', 'USD'): 1.27,
    }
    return rates.get((from_currency, to_currency), 1.0)

# First calls - cache miss (slow)
print("First lookups (cache miss):")
start = time.time()
for _ in range(5):
    rate = get_exchange_rate('USD', 'EUR')
print(f"5 lookups: {time.time() - start:.4f}s")

# Subsequent calls - cache hit (fast)
print("\nCached lookups (cache hit):")
start = time.time()
for _ in range(100):
    rate = get_exchange_rate('USD', 'EUR')
print(f"100 lookups: {time.time() - start:.4f}s")
print(f"Cache info: {get_exchange_rate.cache_info()}")

print("\n--- Compound Interest Calculator ---")

@cache
def calculate_compound_interest(
    principal: float,
    rate: float,
    years: int,
    compounds_per_year: int = 12
) -> float:
    """Calculate compound interest.
    
    Formula: A = P(1 + r/n)^(nt)
    
    Cached because same inputs always give same result.
    """
    n = compounds_per_year
    amount = principal * (1 + rate / n) ** (n * years)
    return round(amount, 2)

# Finance tracker calculations
initial_investment = 10000.0
annual_rate = 0.07  # 7%

print("Investment Growth Projection:")
for years in [1, 5, 10, 20, 30]:
    future_value = calculate_compound_interest(
        initial_investment, annual_rate, years
    )
    print(f"  Year {years:2d}: ${future_value:,.2f}")

print(f"\nCache info: {calculate_compound_interest.cache_info()}")

print("\n--- When to Use Each ---")
print("""
@cache (Python 3.9+):
  - Unbounded cache (stores everything)
  - Use for: pure functions, limited input range
  - Example: fibonacci, factorial, math calculations
  
@lru_cache(maxsize=N):
  - Bounded cache (evicts old entries)
  - Use for: API calls, database queries, file reads
  - Set maxsize=None for unbounded (like @cache)
  
@lru_cache(maxsize=128)  # Default, good balance
@lru_cache(maxsize=None) # Unbounded, same as @cache
@lru_cache(maxsize=0)    # Disabled, no caching
""")
```
