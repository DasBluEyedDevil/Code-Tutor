---
type: "KEY_POINT"
title: "Exception Handling"
---

CompletableFuture provides several exception handling methods:

exceptionally(Function<Throwable, T>): Recover from exceptions

CompletableFuture.supplyAsync(() -> riskyOperation())
    .exceptionally(ex -> "Default value");  // Return fallback

handle(BiFunction<T, Throwable, R>): Handle both success and failure

.handle((result, ex) -> {
    if (ex != null) {
        return "Error: " + ex.getMessage();
    }
    return "Success: " + result;
});

whenComplete(BiConsumer<T, Throwable>): Side effects, doesn't transform

.whenComplete((result, ex) -> {
    if (ex != null) {
        logger.error("Failed", ex);
    } else {
        logger.info("Success: {}", result);
    }
});

IMPORTANT: Exceptions propagate through the chain. If you don't handle them, join()/get() will throw.