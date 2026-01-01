---
type: "ANALOGY"
title: "The Restaurant Analogy"
---

Think of a restaurant:

**Threads = Waiters**
- A waiter (thread) takes your order, goes to the kitchen, waits for food, brings it back
- If you only have 4 waiters and 100 customers, 96 customers wait
- Hiring more waiters is expensive

**Coroutines = Smart Waiters**
- A waiter takes your order, tells the kitchen, then immediately serves another table
- When your food is ready, any available waiter delivers it
- 4 waiters can efficiently serve 100 customers

Coroutines don't block while waiting - they **suspend** and let other work happen. This is the key insight: suspending is not the same as blocking.