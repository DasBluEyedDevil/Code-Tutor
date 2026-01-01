---
type: "THEORY"
title: "The Problem with Raw Threads"
---

Creating threads directly has problems:

// Bad: Create new thread for every task
for (int i = 0; i < 10000; i++) {
    new Thread(() -> handleRequest()).start();
}

PROBLEMS:

1. EXPENSIVE CREATION:
   - Each Thread = ~1MB stack memory
   - OS resources allocated
   - 10,000 threads = 10GB memory!

2. UNCONTROLLED GROWTH:
   - No limit on concurrent threads
   - Server overload under high load
   - Out of memory crashes

3. NO REUSE:
   - Thread dies after task completes
   - Create new one for next task
   - Wasted setup/teardown

4. POOR ERROR HANDLING:
   - Exceptions in threads are silent
   - No way to get results
   - Hard to monitor

SOLUTION: Thread Pools via ExecutorService