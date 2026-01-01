---
type: "THEORY"
title: "Why Coroutines?"
---

Modern apps need to handle multiple tasks simultaneously:
- Downloading data from the internet
- Reading/writing to databases
- Processing user input
- Updating the UI

**The Problem with Traditional Approaches:**

**Threads** are expensive:
- Each thread uses ~1MB of memory
- Creating thousands of threads crashes your app
- Switching between threads is CPU-intensive

**Callbacks** create "callback hell":
```kotlin
// Callback hell - nested callbacks are hard to read
fetchUser(userId) { user ->
    fetchPosts(user.id) { posts ->
        fetchComments(posts[0].id) { comments ->
            // 3 levels deep and growing...
            updateUI(user, posts, comments)
        }
    }
}
```

**Coroutines** solve both problems:
- Lightweight: You can create 100,000+ coroutines on a single device
- Sequential-looking code that runs asynchronously
- Built into Kotlin - not a library bolted on
- Native cancellation support
- Structured concurrency prevents leaks