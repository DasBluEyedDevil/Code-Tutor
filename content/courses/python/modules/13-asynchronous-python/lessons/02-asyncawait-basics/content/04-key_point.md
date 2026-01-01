---
type: "KEY_POINT"
title: "The Golden Rules"
---

**Cannot call async function directly:**
```python
async def get_data():
    return "data"

# WRONG - creates coroutine object, doesn't run
result = get_data()  # <coroutine object>

# RIGHT - use await (inside async function)
result = await get_data()

# RIGHT - use asyncio.run() (from regular code)
result = asyncio.run(get_data())
```

**await only works inside async def:**
```python
# WRONG - SyntaxError
def regular_function():
    result = await get_data()  # Error!

# RIGHT
async def async_function():
    result = await get_data()  # Works!
```

**The entry point:**
Use `asyncio.run(main())` to start your async code from regular Python.