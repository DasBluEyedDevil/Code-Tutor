---
type: "THEORY"
title: "How the Event Loop Works"
---

**The Event Loop: The Conductor of Async Code**

Think of the event loop as a conductor in an orchestra:
- It coordinates all the async tasks
- Decides which task runs next
- Handles switching between paused tasks

**How it works:**
1. You submit coroutines to the event loop
2. Event loop runs them until they hit `await`
3. When one pauses, it runs another
4. When awaited operation completes, it resumes that coroutine

**Key functions:**
- `asyncio.run(coro)` - Create loop, run coroutine, close loop
- `asyncio.gather(*coros)` - Run multiple coroutines concurrently
- `asyncio.create_task(coro)` - Schedule coroutine to run soon

**Important:** There's only ONE event loop per thread. All async operations share it.