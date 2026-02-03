---
type: "WARNING"
title: "ExecutorService Pitfalls"
---

MISTAKE 1: Forgetting to shutdown

ExecutorService executor = Executors.newFixedThreadPool(4);
executor.submit(task);
// Program never exits! Non-daemon threads keep JVM alive

ALWAYS call shutdown() in a finally block or use try-with-resources (ExecutorService implements AutoCloseable since Java 19).

MISTAKE 2: Using newCachedThreadPool for I/O tasks

// Under load, can create thousands of threads!
ExecutorService bad = Executors.newCachedThreadPool();
for (int i = 0; i < 100000; i++) {
    bad.submit(() -> httpClient.get(url));  // Each waits on I/O
}
// OutOfMemoryError: unable to create new native thread

Use fixed pool or virtual threads for I/O-bound work.

MISTAKE 3: Ignoring Future exceptions

Future<?> f = executor.submit(() -> {
    throw new RuntimeException("Oops!");
});
// Exception is SILENT until you call get()
f.get();  // Now ExecutionException is thrown

Always call get() or handle exceptions in the task itself.