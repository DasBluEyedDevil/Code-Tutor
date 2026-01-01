---
type: "THEORY"
title: "The Problem with Future"
---

Basic Future has limitations:

Future<String> future = executor.submit(() -> fetchData());

// Problem 1: Blocking get()
String result = future.get();  // BLOCKS the thread!

// Problem 2: No callbacks
// Can't say: 'when done, do this'
// Must poll: while (!future.isDone()) { }

// Problem 3: Can't chain operations
// Want: fetch data -> parse -> transform -> save
// Future gives no way to compose these

// Problem 4: No exception handling in chain
// Must try/catch around every get()

CompletableFuture solves all of these:
- Non-blocking composition
- Callbacks on completion
- Exception handling in the chain
- Combine multiple futures