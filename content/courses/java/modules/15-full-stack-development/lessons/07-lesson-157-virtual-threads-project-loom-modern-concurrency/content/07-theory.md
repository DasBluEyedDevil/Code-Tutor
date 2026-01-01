---
type: "THEORY"
title: "Structured Concurrency (Stable in Java 23)"
---

Problem: Running multiple concurrent tasks and handling results/errors:

// Old approach - error prone
ExecutorService executor = Executors.newVirtualThreadPerTaskExecutor();
Future<User> userFuture = executor.submit(() -> fetchUser(userId));
Future<List<Order>> ordersFuture = executor.submit(() -> fetchOrders(userId));

User user = userFuture.get();      // What if this throws?
List<Order> orders = ordersFuture.get();  // This might still be running!

// STRUCTURED CONCURRENCY (Stable in Java 23):
try (var scope = new StructuredTaskScope.ShutdownOnFailure()) {
    
    Subtask<User> userTask = scope.fork(() -> fetchUser(userId));
    Subtask<List<Order>> ordersTask = scope.fork(() -> fetchOrders(userId));
    
    scope.join();           // Wait for all tasks
    scope.throwIfFailed();  // Propagate any exception
    
    // Both succeeded - get results
    User user = userTask.get();
    List<Order> orders = ordersTask.get();
    
    return new UserProfile(user, orders);
}
// If userTask fails, ordersTask is automatically cancelled!
// No orphan threads, no resource leaks

NOTE: Structured Concurrency was a preview feature in Java 21-22
and became stable in Java 23. No --enable-preview flag needed!