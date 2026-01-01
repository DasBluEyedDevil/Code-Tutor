---
type: "THEORY"
title: "Chaining Operations"
---

CompletableFuture enables fluent async pipelines:

CompletableFuture.supplyAsync(() -> fetchUserId())
    .thenApply(id -> fetchUserDetails(id))     // Transform
    .thenApply(user -> user.getEmail())        // Transform again
    .thenAccept(email -> sendEmail(email))     // Consume
    .exceptionally(ex -> {                     // Handle errors
        log.error("Failed", ex);
        return null;
    });

DIFFERENCE: thenApply vs thenCompose

thenApply: When transformation returns a VALUE
  .thenApply(id -> "User: " + id)  // String -> String

thenCompose: When transformation returns a FUTURE (flatMap)
  .thenCompose(id -> fetchUserAsync(id))  // String -> CF<User>

Using thenApply with a method returning CompletableFuture gives you CompletableFuture<CompletableFuture<T>> - probably not what you want!