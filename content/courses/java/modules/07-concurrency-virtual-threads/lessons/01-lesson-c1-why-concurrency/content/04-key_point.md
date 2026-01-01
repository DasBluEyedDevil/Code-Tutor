---
type: "KEY_POINT"
title: "The Mental Model: Threads as Workers"
---

Think of threads as workers in a factory:

SINGLE-THREADED (1 worker):
[Worker 1]: Task A -> Task B -> Task C -> Task D
Total time: 4 hours

MULTI-THREADED (4 workers):
[Worker 1]: Task A
[Worker 2]: Task B
[Worker 3]: Task C
[Worker 4]: Task D
Total time: 1 hour

BUT there are complications:

SHARED RESOURCES:
What if Task A and Task B both need the printer?
- They might corrupt each other's output
- Need coordination (synchronization)

DEPENDENCIES:
What if Task C needs Task A's result?
- Worker 3 must WAIT for Worker 1
- Not fully parallelizable

COMMUNICATION OVERHEAD:
Workers need to coordinate:
- 'I'm using the printer now'
- 'Here's my result for you'
- Coordination takes time too

This course teaches you to manage all these challenges.