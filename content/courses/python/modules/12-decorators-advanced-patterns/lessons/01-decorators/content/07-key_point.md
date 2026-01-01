---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Decorators wrap functions** to add functionality without modifying them
- **@decorator syntax** is shorthand for `func = decorator(func)`
- **Use @wraps(func)** from functools to preserve function metadata
- **Pattern: def decorator(func): def wrapper(): return wrapper**
- **Decorators with arguments** require an extra level (factory pattern)
- **Stack decorators** by using multiple @ - applied bottom to top
- **Class-based decorators** use __init__ and __call__
- **@cache (3.9+):** unbounded memoization for pure functions
- **@lru_cache(maxsize=N):** bounded cache with LRU eviction
- **Common uses:** logging, timing, validation, caching, authentication