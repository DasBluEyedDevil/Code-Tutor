---
type: "THEORY"
title: "coroutineScope vs supervisorScope"
---

**coroutineScope** - One child fails, all siblings are cancelled:
```kotlin
suspend fun loadAllData() = coroutineScope {
    val user = async { fetchUser() }        // If this fails...
    val posts = async { fetchPosts() }       // ...this is cancelled
    val settings = async { fetchSettings() } // ...and this too
    
    Triple(user.await(), posts.await(), settings.await())
}
```

**supervisorScope** - Children are independent:
```kotlin
suspend fun loadAllDataIndependently() = supervisorScope {
    val user = async { fetchUser() }        // If this fails...
    val posts = async { fetchPosts() }       // ...this keeps running
    val settings = async { fetchSettings() } // ...and this too
    
    // Handle failures individually
    val userData = runCatching { user.await() }.getOrNull()
    val postsData = runCatching { posts.await() }.getOrNull()
    val settingsData = runCatching { settings.await() }.getOrNull()
    
    Triple(userData, postsData, settingsData)
}
```

**When to use which:**
- `coroutineScope`: When all operations must succeed together (transactional)
- `supervisorScope`: When operations are independent (dashboard with multiple cards)

### Visual Comparison
```
coroutineScope:              supervisorScope:
    Parent                       Parent
   /  |  \                      /  |  \
Child1 Child2 Child3       Child1 Child2 Child3
   ↓                            ↓
Child1 fails                 Child1 fails
   ↓                            ↓
All cancelled!               Others continue!
```