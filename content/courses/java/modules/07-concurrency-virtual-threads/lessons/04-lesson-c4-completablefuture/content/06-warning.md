---
type: "WARNING"
title: "CompletableFuture Pitfalls"
---

MISTAKE 1: Blocking in async callbacks

cf.thenApply(data -> {
    return database.query(data);  // BLOCKS the pool thread!
});

Use thenApplyAsync for blocking operations or virtual threads.

MISTAKE 2: Using common pool for blocking I/O

// Default pool has limited threads (CPU cores)
CompletableFuture.supplyAsync(() -> {
    return httpClient.get(url);  // Blocks a pool thread
});

// Better: Use custom executor
ExecutorService ioPool = Executors.newFixedThreadPool(100);
CompletableFuture.supplyAsync(() -> httpClient.get(url), ioPool);

MISTAKE 3: Ignoring the returned future

cf.thenApply(x -> transform(x));  // Returns NEW future!
// vs
CompletableFuture<R> newCf = cf.thenApply(x -> transform(x));

MISTAKE 4: Not handling exceptions

cf.thenApply(x -> riskyOperation(x));
cf.join();  // Throws if any step failed!

Always add exceptionally() or handle() in the chain.