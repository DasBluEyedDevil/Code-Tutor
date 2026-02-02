---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Not Wrapping Individual Operations**
```python
# WRONG - One failure cancels all tasks
async with asyncio.TaskGroup() as tg:
    for email in emails:
        tg.create_task(send_email(email))  # One failure stops everything!

# CORRECT - Wrap each operation to catch failures
async def safe_send(email):
    try:
        return ("success", await send_email(email))
    except EmailError as e:
        return ("failure", e)

async with asyncio.TaskGroup() as tg:
    tasks = [tg.create_task(safe_send(email)) for email in emails]
```

**2. Forgetting to Distinguish Success from Failure in Results**
```python
# WRONG - Can't tell successes from failures
async def safe_send(email):
    try:
        return await send_email(email)
    except EmailError as e:
        return e  # Both return same type of result!

# CORRECT - Use tuples or dataclasses for clarity
async def safe_send(email):
    try:
        return ("success", await send_email(email))
    except EmailError as e:
        return ("failure", e)
```

**3. Catching Too Broad an Exception in Wrapper**
```python
# WRONG - Catches everything including bugs
async def safe_send(email):
    try:
        return ("success", await send_email(email))
    except Exception:  # Hides programming errors!
        return ("failure", "unknown error")

# CORRECT - Catch only expected exceptions
async def safe_send(email):
    try:
        return ("success", await send_email(email))
    except (EmailError, TimeoutError) as e:  # Specific exceptions
        return ("failure", e)
```

**4. Losing Error Context in Failure Results**
```python
# WRONG - Error details lost
except EmailError:
    return ("failure", email)  # Which error? Why did it fail?

# CORRECT - Preserve error information
except EmailError as e:
    return ("failure", {"email": email, "error": str(e), "type": type(e).__name__})
```

**5. Not Returning Consistent Types**
```python
# WRONG - Success returns string, failure returns exception
async def safe_send(email):
    try:
        result = await send_email(email)
        return result  # str
    except EmailError as e:
        return e  # Exception - different type!

# CORRECT - Consistent return structure
async def safe_send(email):
    try:
        return ("success", await send_email(email))
    except EmailError as e:
        return ("failure", e)  # Both are tuples
```