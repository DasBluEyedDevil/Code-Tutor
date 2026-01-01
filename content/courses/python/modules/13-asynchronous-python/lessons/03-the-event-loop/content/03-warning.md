---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Creating Event Loop When One Exists**
```python
# WRONG - RuntimeError if loop already running
async def main():
    loop = asyncio.get_running_loop()  # OK
    loop.run_until_complete(other())  # Error!

# CORRECT - Just await inside async context
async def main():
    await other()  # Works!
```

**2. Tasks Not Started Until Awaited**
```python
# WRONG - Tasks don't start
async def main():
    tasks = [my_coro() for i in range(3)]  # Just coroutines!
    # Nothing happens yet

# CORRECT - Use create_task to start immediately
async def main():
    tasks = [asyncio.create_task(my_coro()) for i in range(3)]
    # Tasks start running now!
    await asyncio.gather(*tasks)
```

**3. Not Waiting for All Tasks to Complete**
```python
# WRONG - Tasks may not finish before exit
async def main():
    asyncio.create_task(long_task())  # Starts
    asyncio.create_task(long_task())  # Starts
    # main() exits, tasks cancelled!

# CORRECT - Wait for all tasks
async def main():
    tasks = [
        asyncio.create_task(long_task()),
        asyncio.create_task(long_task())
    ]
    await asyncio.gather(*tasks)  # Wait for all
```

**4. Blocking the Event Loop with Sync Code**
```python
# WRONG - requests.get blocks entire loop
async def fetch():
    return requests.get('https://api.example.com')  # Blocks!

# CORRECT - Use async HTTP library
async def fetch():
    async with httpx.AsyncClient() as client:
        return await client.get('https://api.example.com')
```

**5. Getting Wrong Event Loop Reference**
```python
# WRONG in Python 3.10+ (deprecated)
loop = asyncio.get_running_loop()  # Warning!

# CORRECT - Use get_running_loop inside async
async def main():
    loop = asyncio.get_running_loop()  # Correct!
```