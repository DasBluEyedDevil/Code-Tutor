---
type: "THEORY"
title: "The Problem Structured Concurrency Solves"
---

Imagine you're loading a user profile screen:

```kotlin
// Unstructured approach (BAD)
fun loadProfileScreen() {
    GlobalScope.launch {
        val user = fetchUser()      // Takes 2 seconds
        updateUI(user)
    }
    GlobalScope.launch {
        val posts = fetchPosts()    // Takes 3 seconds
        updateUI(posts)
    }
}
```

**Problems:**
1. What if the user navigates away after 1 second?
2. The coroutines keep running (memory leak)
3. They might update a UI that no longer exists (crash)
4. If one fails, how do you cancel the others?
5. Who is responsible for these coroutines?

**Structured concurrency** solves this by enforcing:
- Parent coroutines own child coroutines
- When parent is cancelled, all children are cancelled
- Parent waits for all children before completing
- Exceptions propagate properly through the hierarchy