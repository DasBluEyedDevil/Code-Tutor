---
type: "THEORY"
title: "The JavaScript Runtime"
---

To understand how JavaScript can do "multiple things at once" with only one brain, we have to look at the **Event Loop**.

### 1. The Call Stack
Think of this as JavaScript's "To-Do List." It can only do the task at the very top of the stack. Synchronous code stays on the stack until it's finished.

### 2. The Web APIs (or Node APIs)
When you run a "slow" command like `setTimeout` or a network request, JavaScript doesn't do the waiting itself. It hands the task over to the **Browser** (or the computer's operating system) and says: "Hey, tell me when 2 seconds have passed."
*   JavaScript then immediately clears its "To-Do List" and moves to the next line of code.

### 3. The Callback Queue
Once the 2 seconds are up, the Browser puts your task (the callback function) into a "Waiting Room" called the **Queue**.

### 4. The Event Loop
This is a tiny piece of machinery that constantly checks two things:
1.  Is the **Call Stack** empty? (Is JavaScript finished with its current tasks?)
2.  Is there anything in the **Waiting Room**?

If the answer to both is "Yes," the Event Loop moves the task from the Waiting Room onto the Stack, and JavaScript finally executes it.

### Why this matters
Because of this system, **Asynchronous tasks can never interrupt synchronous ones.** Even if a timer finishes in 0 seconds, it still has to wait for every other line of synchronous code to finish before the Event Loop lets it run.
