---
type: "THEORY"
title: "Thread Lifecycle"
---

Threads go through several states:

NEW:
- Thread created but not started
- Thread t = new Thread(task);  // NEW state

RUNNABLE:
- t.start() called, ready to run
- May be running or waiting for CPU

BLOCKED:
- Waiting to acquire a lock
- synchronized block occupied by another thread

WAITING:
- Waiting indefinitely for another thread
- t.join(), Object.wait() without timeout

TIMED_WAITING:
- Waiting with timeout
- Thread.sleep(1000), t.join(1000)

TERMINATED:
- run() method completed (normally or with exception)
- Thread cannot be restarted!

Visualize:
NEW --> RUNNABLE <--> BLOCKED/WAITING --> TERMINATED
           ^                 |
           |_________________|
                (resumed)