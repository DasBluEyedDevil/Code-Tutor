---
type: "THEORY"
title: "Understanding Coroutines"
---

**What is a coroutine?**

A coroutine is a special function that can pause and resume its execution. When you write `async def`, you're creating a coroutine function.

**Key concepts:**

1. **`async def`** - Declares a coroutine function
   ```python
   async def my_function():
       return "Hello"
   ```

2. **Coroutine object** - What you get when you CALL an async function
   ```python
   coro = my_function()  # Creates coroutine object
   # Does NOT run the function yet!
   ```

3. **`await`** - Actually runs the coroutine and waits for result
   ```python
   result = await my_function()  # NOW it runs
   ```

**Important:** You can only use `await` inside an `async def` function!

**The mental model:**
- Regular function: Runs to completion, returns value
- Coroutine: Can pause at `await`, let other code run, then resume