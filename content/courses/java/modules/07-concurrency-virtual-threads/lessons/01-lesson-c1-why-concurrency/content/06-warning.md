---
type: "WARNING"
title: "Concurrency is Hard (But Learnable)"
---

Concurrency bugs are notoriously difficult:

RACE CONDITIONS:
Thread 1: read balance (100)
Thread 2: read balance (100)
Thread 1: write balance (100 + 50 = 150)
Thread 2: write balance (100 - 30 = 70)  // WRONG! Should be 120

DEADLOCKS:
Thread 1: holds Lock A, waiting for Lock B
Thread 2: holds Lock B, waiting for Lock A
Both wait forever!

MEMORY VISIBILITY:
Thread 1: sets flag = true
Thread 2: reads flag... still sees false (cached!)

These bugs are:
- Intermittent (work 99% of the time, fail in production)
- Hard to reproduce (timing-dependent)
- Hard to debug (stepping through changes timing)

BUT: Java provides tools to prevent them. This course teaches you the safe patterns.