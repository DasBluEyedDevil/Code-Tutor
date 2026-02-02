---
type: "EXAMPLE"
title: "CompletableFuture in Practice"
---

Real-world async operations with proper error handling:

```java
import java.util.concurrent.*;

void main() {
    // Simulate async operations
    CompletableFuture<String> fetchUser = CompletableFuture.supplyAsync(() -> {
        sleep(100);  // Simulate network call
        return "user-123";
    });
    
    // Chain transformations
    CompletableFuture<String> result = fetchUser
        .thenApply(userId -> {
            IO.println("Got user: " + userId);
            return userId.toUpperCase();
        })
        .thenCompose(userId -> {
            // This returns another CompletableFuture
            return CompletableFuture.supplyAsync(() -> {
                sleep(100);
                return "Profile for " + userId;
            });
        })
        .thenApply(profile -> profile + " [processed]");
    
    // Handle both success and failure
    result
        .thenAccept(r -> IO.println("Success: " + r))
        .exceptionally(ex -> {
            IO.println("Error: " + ex.getMessage());
            return null;
        });
    
    // Combining multiple futures
    CompletableFuture<String> future1 = CompletableFuture.supplyAsync(() -> {
        sleep(200);
        return "Result 1";
    });
    CompletableFuture<String> future2 = CompletableFuture.supplyAsync(() -> {
        sleep(100);
        return "Result 2";
    });
    
    // Wait for both, combine results
    CompletableFuture<String> combined = future1.thenCombine(future2,
        (r1, r2) -> r1 + " + " + r2);
    
    IO.println(combined.join());  // "Result 1 + Result 2"
    
    // Wait for either (first to complete)
    CompletableFuture<String> fastest = future1.applyToEither(future2,
        r -> "Fastest: " + r);
    IO.println(fastest.join());  // Probably "Fastest: Result 2"
    
    // Wait for all
    CompletableFuture<Void> all = CompletableFuture.allOf(future1, future2);
    all.join();  // Both complete
    
    // Wait for first (any)
    CompletableFuture<Object> any = CompletableFuture.anyOf(future1, future2);
    IO.println("First: " + any.join());
}

void sleep(long ms) {
    try { Thread.sleep(ms); } catch (InterruptedException e) { }
}
```
