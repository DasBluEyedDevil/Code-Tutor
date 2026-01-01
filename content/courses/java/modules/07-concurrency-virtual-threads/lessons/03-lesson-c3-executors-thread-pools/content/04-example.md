---
type: "EXAMPLE"
title: "Using ExecutorService"
---

Complete example with proper shutdown and result handling:

```java
import java.util.concurrent.*;
import java.util.*;

void main() throws Exception {
    // Create a thread pool with 4 workers
    ExecutorService executor = Executors.newFixedThreadPool(4);
    
    try {
        // Submit Runnable (no return value)
        executor.submit(() -> {
            System.out.println("Task 1 on " + Thread.currentThread().getName());
        });
        
        // Submit Callable (returns a value)
        Future<Integer> future = executor.submit(() -> {
            Thread.sleep(500);
            return 42;
        });
        
        // Do other work while task runs...
        System.out.println("Doing other work...");
        
        // Get result (blocks until complete)
        Integer result = future.get();  // Waits up to forever
        System.out.println("Result: " + result);
        
        // Get with timeout
        Future<String> future2 = executor.submit(() -> {
            Thread.sleep(2000);
            return "Done!";
        });
        
        try {
            String result2 = future2.get(1, TimeUnit.SECONDS);  // Only wait 1s
        } catch (TimeoutException e) {
            System.out.println("Task took too long!");
            future2.cancel(true);  // Cancel the task
        }
        
        // Submit multiple tasks
        List<Callable<Integer>> tasks = List.of(
            () -> { Thread.sleep(100); return 1; },
            () -> { Thread.sleep(200); return 2; },
            () -> { Thread.sleep(150); return 3; }
        );
        
        // invokeAll waits for ALL to complete
        List<Future<Integer>> futures = executor.invokeAll(tasks);
        for (Future<Integer> f : futures) {
            System.out.println("Got: " + f.get());
        }
        
    } finally {
        // CRITICAL: Always shutdown!
        executor.shutdown();
        
        // Wait for tasks to finish (with timeout)
        if (!executor.awaitTermination(5, TimeUnit.SECONDS)) {
            System.out.println("Forcing shutdown...");
            executor.shutdownNow();  // Force stop
        }
    }
}
```
