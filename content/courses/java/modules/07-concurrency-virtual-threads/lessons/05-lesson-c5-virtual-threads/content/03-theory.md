---
type: "THEORY"
title: "Creating Virtual Threads"
---

Three ways to create virtual threads:

1. Thread.startVirtualThread()

Thread vt = Thread.startVirtualThread(() -> {
    IO.println("Running on: " + Thread.currentThread());
});
vt.join();

2. Thread.ofVirtual() builder

Thread vt = Thread.ofVirtual()
    .name("my-virtual-thread")
    .start(() -> doWork());

3. Virtual thread executor (RECOMMENDED)

try (var executor = Executors.newVirtualThreadPerTaskExecutor()) {
    // Each task gets its OWN virtual thread
    for (int i = 0; i < 100_000; i++) {
        executor.submit(() -> {
            Thread.sleep(1000);  // Blocking is fine!
            return processRequest();
        });
    }
}  // Waits for all to complete

100,000 concurrent tasks with simple blocking code!