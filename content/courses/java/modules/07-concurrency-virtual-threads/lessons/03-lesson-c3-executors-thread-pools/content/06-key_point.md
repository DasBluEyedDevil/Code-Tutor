---
type: "KEY_POINT"
title: "Proper Shutdown Pattern"
---

The standard shutdown pattern ensures clean termination:

void shutdownGracefully(ExecutorService executor) {
    executor.shutdown();  // Stop accepting new tasks
    
    try {
        // Wait for existing tasks to finish
        if (!executor.awaitTermination(60, TimeUnit.SECONDS)) {
            // Tasks didn't finish in time
            executor.shutdownNow();  // Cancel running tasks
            
            // Wait a bit more
            if (!executor.awaitTermination(60, TimeUnit.SECONDS)) {
                System.err.println("Executor did not terminate");
            }
        }
    } catch (InterruptedException e) {
        // Current thread was interrupted
        executor.shutdownNow();
        Thread.currentThread().interrupt();
    }
}

JAVA 19+ ALTERNATIVE:
ExecutorService implements AutoCloseable:

try (var executor = Executors.newFixedThreadPool(4)) {
    executor.submit(task);
}  // Automatically calls shutdown() and awaits termination