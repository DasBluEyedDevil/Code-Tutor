---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using Sync Code in Async Functions**
```python
# WRONG - time.sleep blocks the event loop
async def fetch_data():
    time.sleep(1)  # Blocks everything!
    return 'data'

# CORRECT - Use async sleep
async def fetch_data():
    await asyncio.sleep(1)  # Non-blocking
    return 'data'
```

**2. Calling Async Function Without await**
```python
# WRONG - Returns coroutine, not result
async def get_user():
    return {'name': 'Alice'}

user = get_user()  # <coroutine object>
print(user['name'])  # TypeError!

# CORRECT - Use await
user = await get_user()  # {'name': 'Alice'}
print(user['name'])  # 'Alice'
```

**3. Mixing Sync and Async Incorrectly**
```python
# WRONG - Can't await in non-async function
def main():
    result = await get_data()  # SyntaxError!

# CORRECT - Use async def or asyncio.run()
async def main():
    result = await get_data()  # Works!

# Or from sync code:
result = asyncio.run(get_data())  # Works!
```

**4. Using Async for CPU-Bound Tasks**
```python
# WRONG - Async doesn't help CPU work
async def compute():
    return sum(range(10**7))  # Still blocks!

# CORRECT - Use multiprocessing for CPU
from concurrent.futures import ProcessPoolExecutor

def compute():
    return sum(range(10**7))

with ProcessPoolExecutor() as pool:
    result = pool.submit(compute)
```

**5. Forgetting to Run the Event Loop**
```python
# WRONG - Coroutine never executed
async def main():
    print('Hello')

main()  # Nothing happens! Just creates coroutine

# CORRECT - Use asyncio.run()
import asyncio
asyncio.run(main())  # Prints 'Hello'
```