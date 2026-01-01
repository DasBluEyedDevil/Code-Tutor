---
type: "THEORY"
title: "Types of Thread Pools"
---

Executors factory provides several pool types:

FIXED THREAD POOL:
Executors.newFixedThreadPool(int n)
- Exactly n threads, no more, no less
- Tasks queue when all threads busy
- Best for: known workload, predictable resources

CACHED THREAD POOL:
Executors.newCachedThreadPool()
- Creates threads as needed
- Reuses idle threads
- Removes threads idle > 60 seconds
- Best for: many short-lived tasks
- WARNING: Can create unbounded threads under load!

SINGLE THREAD EXECUTOR:
Executors.newSingleThreadExecutor()
- Exactly 1 thread
- Tasks execute sequentially
- Best for: ordered task execution, avoiding concurrency

SCHEDULED THREAD POOL:
Executors.newScheduledThreadPool(int n)
- Run tasks after delay or periodically
- Best for: timers, periodic jobs

2025 BEST PRACTICE:
For most web apps, use fixed pool sized to available cores:
Executors.newFixedThreadPool(Runtime.getRuntime().availableProcessors())