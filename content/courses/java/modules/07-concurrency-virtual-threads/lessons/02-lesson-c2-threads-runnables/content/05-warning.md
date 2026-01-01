---
type: "WARNING"
title: "Critical Thread Pitfalls"
---

MISTAKE 1: Calling run() instead of start()

thread.run();   // WRONG! Runs on CURRENT thread
thread.start(); // CORRECT! Runs on NEW thread

run() just calls the method normally. start() creates a new thread.

MISTAKE 2: Starting a thread twice

thread.start();
thread.start();  // IllegalThreadStateException!

Once terminated, a thread cannot restart. Create a new Thread.

MISTAKE 3: Ignoring InterruptedException

try {
    Thread.sleep(1000);
} catch (InterruptedException e) {
    // DON'T just ignore it!
    Thread.currentThread().interrupt();  // Restore flag
    return;  // Or handle appropriately
}

Interruption is a cooperative cancellation mechanism. Don't swallow it.

MISTAKE 4: Not joining threads

If main() ends before worker threads, the JVM might exit. Use join() to wait, or use ExecutorService with proper shutdown.