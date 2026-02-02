---
type: "THEORY"
title: "Thread Communication: join() and interrupt()"
---

JOINING THREADS:
Wait for another thread to complete.

Thread worker = new Thread(task);
worker.start();

// Wait at most 5 seconds
worker.join(5000);  // Returns after 5s OR when worker finishes

if (worker.isAlive()) {
    IO.println("Worker still running after 5s");
}

INTERRUPTING THREADS:
Request a thread to stop (cooperative, not forced).

Thread worker = new Thread(() -> {
    while (!Thread.currentThread().isInterrupted()) {
        // Do work
    }
    IO.println("Received interrupt, stopping");
});

worker.start();
Thread.sleep(1000);
worker.interrupt();  // Set interrupt flag

IMPORTANT: interrupt() doesn't STOP the thread. It:
1. Sets a flag (isInterrupted())
2. Throws InterruptedException if sleeping/waiting

The thread must CHECK the flag and stop voluntarily.