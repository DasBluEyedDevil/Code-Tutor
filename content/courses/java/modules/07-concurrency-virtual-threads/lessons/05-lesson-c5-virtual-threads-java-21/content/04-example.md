---
type: "EXAMPLE"
title: "Virtual Threads in Action"
---

Demonstrating virtual threads' power with concurrent I/O:

```java
import java.util.concurrent.*;
import java.time.*;
import java.util.stream.*;

void main() throws Exception {
    int taskCount = 10_000;
    
    // Simulate I/O-bound task
    Runnable ioTask = () -> {
        try {
            Thread.sleep(100);  // Simulate 100ms I/O
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
    };
    
    // Virtual threads: 10,000 concurrent tasks
    Instant start = Instant.now();
    
    try (var executor = Executors.newVirtualThreadPerTaskExecutor()) {
        IntStream.range(0, taskCount)
            .forEach(i -> executor.submit(ioTask));
    }  // Blocks until all complete
    
    Duration virtualTime = Duration.between(start, Instant.now());
    System.out.println("Virtual threads: " + virtualTime.toMillis() + "ms");
    // Output: ~100-200ms (tasks run concurrently)
    
    // Compare with platform thread pool (10 threads)
    start = Instant.now();
    
    try (var executor = Executors.newFixedThreadPool(10)) {
        IntStream.range(0, taskCount)
            .forEach(i -> executor.submit(ioTask));
    }
    
    Duration poolTime = Duration.between(start, Instant.now());
    System.out.println("Platform threads (10): " + poolTime.toMillis() + "ms");
    // Output: ~100,000ms (10 threads, 1000 batches of 100ms)
    
    // Check if current thread is virtual
    Thread.startVirtualThread(() -> {
        System.out.println("Is virtual: " + Thread.currentThread().isVirtual());
    }).join();
}
```
