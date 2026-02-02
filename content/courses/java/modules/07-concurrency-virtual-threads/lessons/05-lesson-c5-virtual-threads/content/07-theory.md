---
type: "THEORY"
title: "CompletableFuture WITH Virtual Threads"
---

Virtual threads complement CompletableFuture:

// Use virtual threads as the executor for CompletableFuture
var executor = Executors.newVirtualThreadPerTaskExecutor();

CompletableFuture<String> future = CompletableFuture
    .supplyAsync(() -> {
        // Blocking I/O is now efficient!
        return httpClient.get("https://api.example.com/data");
    }, executor)
    .thenApply(response -> parseJson(response))
    .exceptionally(ex -> handleError(ex));

BEST OF BOTH WORLDS:
- CompletableFuture: Composition, callbacks, combining results
- Virtual threads: Efficient blocking I/O

WHEN TO USE WHICH:

Virtual threads alone:
- Simple request-per-thread model
- Straightforward sequential logic
- Spring/Jakarta EE web apps

CompletableFuture + virtual threads:
- Complex async composition
- Parallel API calls with combination
- Reactive-style pipelines

CompletableFuture alone (platform threads):
- CPU-bound parallel processing
- When you need thread pool control