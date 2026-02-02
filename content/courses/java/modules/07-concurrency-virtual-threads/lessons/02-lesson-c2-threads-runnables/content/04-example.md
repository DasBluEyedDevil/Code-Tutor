---
type: "EXAMPLE"
title: "Basic Thread Operations"
---

Essential thread operations you need to know:

```java
void main() throws InterruptedException {
    // Create a thread with a task
    Thread worker = new Thread(() -> {
        String name = Thread.currentThread().getName();
        IO.println(name + " started");
        
        try {
            // Simulate work
            Thread.sleep(1000);  // Pause for 1 second
        } catch (InterruptedException e) {
            IO.println(name + " was interrupted");
            return;
        }
        
        IO.println(name + " finished");
    });
    
    // Give it a meaningful name (helps debugging)
    worker.setName("DataProcessor");
    
    // Check state before starting
    IO.println("State: " + worker.getState());  // NEW
    
    // Start the thread (don't call run() directly!)
    worker.start();
    IO.println("State: " + worker.getState());  // RUNNABLE
    
    // Main thread continues immediately!
    IO.println("Main thread continues...");
    
    // Wait for worker to complete
    worker.join();  // Blocks until worker finishes
    IO.println("State: " + worker.getState());  // TERMINATED
    
    IO.println("Worker completed, main resumes");
}
```
