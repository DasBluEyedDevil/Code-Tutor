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
        System.out.println(name + " started");
        
        try {
            // Simulate work
            Thread.sleep(1000);  // Pause for 1 second
        } catch (InterruptedException e) {
            System.out.println(name + " was interrupted");
            return;
        }
        
        System.out.println(name + " finished");
    });
    
    // Give it a meaningful name (helps debugging)
    worker.setName("DataProcessor");
    
    // Check state before starting
    System.out.println("State: " + worker.getState());  // NEW
    
    // Start the thread (don't call run() directly!)
    worker.start();
    System.out.println("State: " + worker.getState());  // RUNNABLE
    
    // Main thread continues immediately!
    System.out.println("Main thread continues...");
    
    // Wait for worker to complete
    worker.join();  // Blocks until worker finishes
    System.out.println("State: " + worker.getState());  // TERMINATED
    
    System.out.println("Worker completed, main resumes");
}
```
