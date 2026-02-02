---
type: "KEY_POINT"
title: "CompletableFuture Basics"
---

CompletableFuture represents a future result that can be completed:

// Run async task (uses ForkJoinPool by default)
CompletableFuture<String> cf = CompletableFuture.supplyAsync(() -> {
    return fetchData();  // Runs in background
});

// Non-blocking: attach callback
cf.thenAccept(result -> IO.println("Got: " + result));

// Main thread continues immediately!
IO.println("Not blocked!");

KEY METHODS:

supplyAsync(Supplier<T>): Run async, return result
runAsync(Runnable): Run async, no result

thenApply(Function): Transform result
thenAccept(Consumer): Consume result
thenRun(Runnable): Run action when complete

join(): Get result (like get() but unchecked exception)
get(): Get result (checked exception)

The 'Async' suffix methods run callbacks on thread pool instead of completing thread.