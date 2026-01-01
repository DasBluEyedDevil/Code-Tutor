# Kotlin Multiplatform Course Expansion - Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Transform the existing Kotlin Multiplatform course from "strong language fundamentals" into a complete "newbie to production-ready KMP developer" path by adding missing modules for coroutines, persistence, architecture, testing, deployment, and capstone projects.

**Architecture:** The course will be expanded from 10 modules to 17 modules, inserting new content strategically between existing modules. New modules cover: Coroutines & Flows (after OOP), Persistence with SQLDelight (after Backend), Architecture & DI with Koin (after Mobile), Testing & Quality (new), Deployment & Distribution (expanded), and a comprehensive Capstone Project. Each lesson will be complete, thorough, with no placeholders or stubs.

**Tech Stack:** Kotlin 2.3.0, Compose Multiplatform 1.8.0+, Ktor Client 3.3.x, SQLDelight 2.x, Koin 4.0, kotlinx.coroutines, kotlinx.serialization, GitHub Actions, Fastlane

---

## Executive Summary

### Current State Analysis

The existing Kotlin course has 10 modules:
1. Module 01: The Absolute Basics
2. Module 02: Controlling the Flow
3. Module 03: Object-Oriented Programming
4. Module 04: Advanced Kotlin
5. Module 05: Backend Development with Ktor
6. Module 06: Mobile Development with Compose Multiplatform
7. Module 07: Professional Development & Deployment
8. Module 08: Gradle Mastery for Kotlin Developers
9. Module 09: Functional Kotlin with Arrow
10. Module 10: The K2 Era - Modern Kotlin Tooling

### Critical Gaps Identified

| Gap | Priority | New Module Required |
|-----|----------|---------------------|
| Coroutines & structured concurrency | Critical | Module 04A: Coroutines & Flows |
| Expect/actual declarations | High | Enhance existing + new lessons |
| Persistence (SQLDelight) | Critical | Module 06A: Persistence with SQLDelight |
| App Architecture (MVVM/MVI) | Critical | Module 06B: KMP Architecture Patterns |
| Dependency Injection (Koin) | High | Module 06C: Dependency Injection with Koin |
| Testing (unit, UI, platform) | Critical | Module 07A: Testing KMP Applications |
| CI/CD and Deployment | Critical | Enhance Module 07 + new lessons |
| End-to-end capstone | Critical | Module 11: Capstone Project |

### Proposed Module Structure (Post-Expansion)

```
Module 01: The Absolute Basics (existing)
Module 02: Controlling the Flow (existing)
Module 03: Object-Oriented Programming (existing)
Module 04: Advanced Kotlin (existing - enhanced)
>>> NEW: Module 04A: Coroutines & Flows <<<
Module 05: Backend Development with Ktor (existing - enhanced)
Module 06: Mobile Development with Compose Multiplatform (existing - enhanced)
>>> NEW: Module 06A: Persistence with SQLDelight <<<
>>> NEW: Module 06B: KMP Architecture Patterns <<<
>>> NEW: Module 06C: Dependency Injection with Koin <<<
>>> NEW: Module 07A: Testing KMP Applications <<<
Module 07: Professional Development & Deployment (existing - significantly enhanced)
Module 08: Gradle Mastery for Kotlin Developers (existing)
Module 09: Functional Kotlin with Arrow (existing)
Module 10: The K2 Era - Modern Kotlin Tooling (existing)
>>> NEW: Module 11: Capstone - Cross-Platform Notes App <<<
```

---

## Technology Version Reference (2025 Current)

Based on web research conducted December 2025:

| Technology | Version | Status | Source |
|------------|---------|--------|--------|
| Kotlin | 2.3.0 | Stable | [Kotlin 2.3.0 Release](https://blog.jetbrains.com/kotlin/2025/12/kotlin-2-3-0-released/) |
| Compose Multiplatform | 1.8.0+ | iOS Stable, Web Beta | [Compose MP 1.8.0](https://blog.jetbrains.com/kotlin/2025/05/compose-multiplatform-1-8-0-released-compose-multiplatform-for-ios-is-stable-and-production-ready/) |
| Ktor Client | 3.3.3 | Stable | [Ktor Documentation](https://ktor.io/docs/client-engines.html) |
| SQLDelight | 2.0.x | Stable | [SQLDelight GitHub](https://github.com/cashapp/sqldelight) |
| Koin | 4.0 | Stable | [Koin GitHub](https://github.com/InsertKoinIO/koin) |
| Android Studio | Narwhal 2025.1.1+ | Stable | [Developer Android](https://developer.android.com/studio) |
| Swift Export | Kotlin 2.2.20+ | Experimental | [KotlinConf 2025](https://blog.jetbrains.com/kotlin/2025/05/kotlinconf-2025-language-features-ai-powered-development-and-kotlin-multiplatform/) |

---

## Task 1: Update Tooling Version References

**Files:**
- Modify: `content/courses/kotlin/course.json` (Module 01, Lesson 1.1)

**Step 1: Read the current setup instructions**

Read the existing content in Module 01, Lesson 1.1 that mentions "Android Studio Ladybug (2024.2)".

**Step 2: Update version references to be evergreen**

Replace hard-coded version numbers with guidance on finding current versions:

```json
{
  "type": "THEORY",
  "title": "Setting Up Your Development Environment",
  "content": "### The Multiplatform Setup\n\nIn 2025, learning Kotlin means learning **Kotlin Multiplatform (KMP)** from day one. You'll write code once and run it on:\n- Android phones and tablets\n- iPhones and iPads\n- Desktop (Windows, macOS, Linux)\n- Web browsers\n\n### Required Tools\n\n**1. Android Studio (Current Stable Version)**\n- Download from [developer.android.com/studio](https://developer.android.com/studio)\n- As of late 2025, this is Android Studio Narwhal (2025.1.1) or newer\n- **Important**: Always use the latest stable version, not canary/beta builds\n- Includes Kotlin plugin and Android SDK\n\n**2. Xcode (macOS only, for iOS development)**\n- Download from Mac App Store\n- Required for iOS simulator and building iOS apps\n- Windows/Linux users: Use Android-only mode initially, or use a macOS cloud service\n\n**3. Kotlin Multiplatform Plugin**\n- In Android Studio: Settings → Plugins → Search \"Kotlin Multiplatform\"\n- The plugin is available for all platforms (macOS, Windows, Linux) as of 2025\n- Install and restart\n\n### Checking Your Kotlin Version\n\nAfter installation, verify your Kotlin version:\n1. Open Android Studio\n2. Go to: Settings → Languages & Frameworks → Kotlin\n3. Ensure you have Kotlin 2.1.0 or newer (required for Compose Multiplatform 1.8+)\n\n### A Note on Evolving Tools\n\nKMP is a rapidly evolving ecosystem. The exact wizard names and UI may change between Android Studio versions. The core concepts remain the same:\n1. Create a new KMP project using the wizard\n2. Configure targets (Android, iOS, Desktop, Web)\n3. Your code lives in `commonMain` for shared logic\n\nIf the wizard looks different from screenshots, check the [official Kotlin Multiplatform documentation](https://kotlinlang.org/docs/multiplatform-get-started.html) for current setup instructions."
}
```

**Step 3: Commit**

```bash
git add content/courses/kotlin/course.json
git commit -m "docs(kotlin): update tooling versions to be evergreen

🤖 Generated with [Claude Code](https://claude.com/claude-code)

Co-Authored-By: Claude Opus 4.5 <noreply@anthropic.com>"
```

---

## Task 2: Create Module 04A - Coroutines & Flows

**Files:**
- Create: New module section in `content/courses/kotlin/course.json`

This is a CRITICAL gap. The current course has NO dedicated coroutines content.

### Lesson 04A.1: Introduction to Coroutines

**Step 1: Create the lesson structure**

```json
{
  "id": "4a.1",
  "title": "Lesson 4A.1: Introduction to Coroutines",
  "moduleId": "module-04a",
  "order": 1,
  "estimatedMinutes": 60,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "THEORY",
      "title": "Introduction",
      "content": "**Estimated Time**: 60 minutes\n\nThis lesson introduces Kotlin coroutines - the foundation for asynchronous programming in Kotlin Multiplatform."
    },
    {
      "type": "THEORY",
      "title": "Why Coroutines?",
      "content": "Modern apps need to handle multiple tasks simultaneously:\n- Downloading data from the internet\n- Reading/writing to databases\n- Processing user input\n- Updating the UI\n\n**The Problem with Traditional Approaches:**\n\n**Threads** are expensive:\n- Each thread uses ~1MB of memory\n- Creating thousands of threads crashes your app\n- Switching between threads is CPU-intensive\n\n**Callbacks** create \"callback hell\":\n```kotlin\n// Callback hell - nested callbacks are hard to read\nfetchUser(userId) { user ->\n    fetchPosts(user.id) { posts ->\n        fetchComments(posts[0].id) { comments ->\n            // 3 levels deep and growing...\n            updateUI(user, posts, comments)\n        }\n    }\n}\n```\n\n**Coroutines** solve both problems:\n- Lightweight: You can create 100,000+ coroutines\n- Sequential-looking code that runs asynchronously\n- Built into Kotlin - not a library bolted on"
    },
    {
      "type": "ANALOGY",
      "title": "The Restaurant Analogy",
      "content": "Think of a restaurant:\n\n**Threads = Waiters**\n- A waiter (thread) takes your order, goes to the kitchen, waits for food, brings it back\n- If you only have 4 waiters and 100 customers, 96 customers wait\n- Hiring more waiters is expensive\n\n**Coroutines = Smart Waiters**\n- A waiter takes your order, tells the kitchen, then immediately serves another table\n- When your food is ready, any available waiter delivers it\n- 4 waiters can efficiently serve 100 customers\n\nCoroutines don't block while waiting - they suspend and let other work happen."
    },
    {
      "type": "EXAMPLE",
      "title": "Your First Coroutine",
      "content": "Let's write your first coroutine:",
      "code": "import kotlinx.coroutines.*\n\nfun main() = runBlocking {\n    println(\"Starting on ${Thread.currentThread().name}\")\n    \n    launch {\n        delay(1000L) // Non-blocking delay for 1 second\n        println(\"World! on ${Thread.currentThread().name}\")\n    }\n    \n    println(\"Hello,\")\n}\n\n// Output:\n// Starting on main\n// Hello,\n// World! on main",
      "language": "kotlin"
    },
    {
      "type": "THEORY",
      "title": "Key Concepts Explained",
      "content": "### runBlocking\n```kotlin\nfun main() = runBlocking {\n    // Coroutine code here\n}\n```\n- Creates a coroutine that **blocks** the current thread until all child coroutines complete\n- Used as a bridge between regular blocking code and coroutines\n- **Never use in production code** - only for `main()` or tests\n\n### launch\n```kotlin\nlaunch {\n    // This runs in a new coroutine\n}\n```\n- Starts a new coroutine **concurrently** with the rest of the code\n- Returns a `Job` that can be used to cancel the coroutine\n- \"Fire and forget\" - doesn't return a result\n\n### delay\n```kotlin\ndelay(1000L) // Suspends for 1 second\n```\n- **Suspends** the coroutine (pauses without blocking the thread)\n- Unlike `Thread.sleep()`, other coroutines can run during this time\n- Only works inside a coroutine or suspend function"
    },
    {
      "type": "THEORY",
      "title": "suspend Functions",
      "content": "The `suspend` keyword marks functions that can pause and resume:\n\n```kotlin\nsuspend fun fetchUserFromNetwork(userId: String): User {\n    delay(2000) // Simulate network delay\n    return User(userId, \"John Doe\")\n}\n\nsuspend fun saveUserToDatabase(user: User) {\n    delay(500) // Simulate database write\n    println(\"User ${user.name} saved\")\n}\n\nfun main() = runBlocking {\n    val user = fetchUserFromNetwork(\"123\") // Suspends here\n    saveUserToDatabase(user) // Then suspends here\n    println(\"Done!\")\n}\n```\n\n**Key Rules:**\n1. `suspend` functions can only be called from coroutines or other `suspend` functions\n2. They look like regular sequential code but execute asynchronously\n3. The compiler transforms them into state machines (you don't need to understand this yet)"
    },
    {
      "type": "WARNING",
      "title": "Common Mistakes",
      "content": "### Mistake 1: Using Thread.sleep() in coroutines\n```kotlin\n// ❌ WRONG - blocks the thread\nlaunch {\n    Thread.sleep(1000)\n    println(\"Done\")\n}\n\n// ✅ RIGHT - suspends the coroutine\nlaunch {\n    delay(1000)\n    println(\"Done\")\n}\n```\n\n### Mistake 2: Forgetting runBlocking in main()\n```kotlin\n// ❌ WRONG - launch needs a CoroutineScope\nfun main() {\n    launch { // Compiler error!\n        delay(1000)\n    }\n}\n\n// ✅ RIGHT - use runBlocking\nfun main() = runBlocking {\n    launch {\n        delay(1000)\n    }\n}\n```\n\n### Mistake 3: Calling suspend functions from regular functions\n```kotlin\n// ❌ WRONG - can't call suspend from regular function\nfun loadData() {\n    val user = fetchUserFromNetwork(\"123\") // Compiler error!\n}\n\n// ✅ RIGHT - make it suspend or use a coroutine\nsuspend fun loadData() {\n    val user = fetchUserFromNetwork(\"123\")\n}\n```"
    },
    {
      "type": "THEORY",
      "title": "Exercise: Sequential vs Concurrent",
      "content": "**Goal**: Understand the difference between sequential and concurrent execution.\n\n**Part 1**: Run these functions sequentially and measure time:\n```kotlin\nsuspend fun fetchUser(): String {\n    delay(1000)\n    return \"User\"\n}\n\nsuspend fun fetchPosts(): String {\n    delay(1000)\n    return \"Posts\"\n}\n\nfun main() = runBlocking {\n    val start = System.currentTimeMillis()\n    \n    val user = fetchUser()    // 1 second\n    val posts = fetchPosts()  // 1 second\n    \n    val time = System.currentTimeMillis() - start\n    println(\"$user and $posts in ${time}ms\")\n    // Output: User and Posts in 2000ms (approximately)\n}\n```\n\n**Part 2**: Now run them concurrently:\n```kotlin\nfun main() = runBlocking {\n    val start = System.currentTimeMillis()\n    \n    val userDeferred = async { fetchUser() }\n    val postsDeferred = async { fetchPosts() }\n    \n    val user = userDeferred.await()\n    val posts = postsDeferred.await()\n    \n    val time = System.currentTimeMillis() - start\n    println(\"$user and $posts in ${time}ms\")\n    // Output: User and Posts in 1000ms (approximately)\n}\n```\n\n**Key Insight**: `async` starts coroutines concurrently, `await()` waits for results."
    }
  ],
  "challenges": [
    {
      "id": "challenge-4a-1-1",
      "title": "Parallel Data Fetcher",
      "description": "Create a function that fetches data from three 'API endpoints' (simulated with delay) in parallel and returns the combined result.",
      "difficulty": "intermediate",
      "starterCode": "import kotlinx.coroutines.*\n\nsuspend fun fetchUserProfile(): String {\n    delay(1000)\n    return \"Profile Data\"\n}\n\nsuspend fun fetchUserSettings(): String {\n    delay(800)\n    return \"Settings Data\"\n}\n\nsuspend fun fetchUserNotifications(): String {\n    delay(600)\n    return \"Notifications\"\n}\n\n// TODO: Implement this function to fetch all three in parallel\n// It should complete in ~1000ms, not ~2400ms\nsuspend fun fetchAllUserData(): Triple<String, String, String> {\n    // Your code here\n}\n\nfun main() = runBlocking {\n    val start = System.currentTimeMillis()\n    val result = fetchAllUserData()\n    val time = System.currentTimeMillis() - start\n    println(\"Fetched: $result\")\n    println(\"Time: ${time}ms\")\n}",
      "solution": "import kotlinx.coroutines.*\n\nsuspend fun fetchUserProfile(): String {\n    delay(1000)\n    return \"Profile Data\"\n}\n\nsuspend fun fetchUserSettings(): String {\n    delay(800)\n    return \"Settings Data\"\n}\n\nsuspend fun fetchUserNotifications(): String {\n    delay(600)\n    return \"Notifications\"\n}\n\nsuspend fun fetchAllUserData(): Triple<String, String, String> = coroutineScope {\n    val profileDeferred = async { fetchUserProfile() }\n    val settingsDeferred = async { fetchUserSettings() }\n    val notificationsDeferred = async { fetchUserNotifications() }\n    \n    Triple(\n        profileDeferred.await(),\n        settingsDeferred.await(),\n        notificationsDeferred.await()\n    )\n}\n\nfun main() = runBlocking {\n    val start = System.currentTimeMillis()\n    val result = fetchAllUserData()\n    val time = System.currentTimeMillis() - start\n    println(\"Fetched: $result\")\n    println(\"Time: ${time}ms\") // Should be ~1000ms\n}",
      "hints": [
        "Use async { } to start each fetch concurrently",
        "Use coroutineScope { } to create a scope for your async calls",
        "Call .await() on each Deferred to get the results"
      ],
      "testCases": [
        {
          "description": "Should complete in approximately 1000ms (not 2400ms)",
          "expectedBehavior": "Time should be around 1000-1100ms since all three run in parallel"
        }
      ]
    }
  ]
}
```

**Step 2: Verify the JSON is valid**

Run: `node -e "JSON.parse(require('fs').readFileSync('content/courses/kotlin/course.json'))"`
Expected: No output (valid JSON)

**Step 3: Commit**

```bash
git add content/courses/kotlin/course.json
git commit -m "feat(kotlin): add Lesson 4A.1 - Introduction to Coroutines

🤖 Generated with [Claude Code](https://claude.com/claude-code)

Co-Authored-By: Claude Opus 4.5 <noreply@anthropic.com>"
```

---

### Lesson 04A.2: CoroutineScope and Structured Concurrency

**Step 1: Create the lesson**

```json
{
  "id": "4a.2",
  "title": "Lesson 4A.2: CoroutineScope and Structured Concurrency",
  "moduleId": "module-04a",
  "order": 2,
  "estimatedMinutes": 55,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "THEORY",
      "title": "Introduction",
      "content": "**Estimated Time**: 55 minutes\n\nThis lesson covers CoroutineScope, structured concurrency, and how Kotlin prevents resource leaks in asynchronous code."
    },
    {
      "type": "THEORY",
      "title": "The Problem Structured Concurrency Solves",
      "content": "Imagine you're loading a user profile screen:\n\n```kotlin\n// Unstructured approach (BAD)\nfun loadProfileScreen() {\n    GlobalScope.launch {\n        val user = fetchUser()      // Takes 2 seconds\n        updateUI(user)\n    }\n    GlobalScope.launch {\n        val posts = fetchPosts()    // Takes 3 seconds\n        updateUI(posts)\n    }\n}\n```\n\n**Problems:**\n1. What if the user navigates away after 1 second?\n2. The coroutines keep running (memory leak)\n3. They might update a UI that no longer exists (crash)\n4. If one fails, how do you cancel the others?\n\n**Structured concurrency** solves this by enforcing:\n- Parent coroutines own child coroutines\n- When parent is cancelled, all children are cancelled\n- Parent waits for all children before completing\n- Exceptions propagate properly"
    },
    {
      "type": "THEORY",
      "title": "Understanding CoroutineScope",
      "content": "Every coroutine runs in a **scope**. The scope defines:\n- **Lifetime**: When the scope is cancelled, all its coroutines are cancelled\n- **Context**: Default dispatcher, exception handler, etc.\n\n```kotlin\n// Creating your own scope\nval myScope = CoroutineScope(Dispatchers.Default)\n\nmyScope.launch {\n    // This coroutine is tied to myScope's lifetime\n}\n\n// When you're done with the scope:\nmyScope.cancel() // Cancels all coroutines in this scope\n```\n\n### Built-in Scopes in KMP\n\n| Scope | Platform | Lifecycle |\n|-------|----------|----------|\n| `viewModelScope` | Android | ViewModel destroyed |\n| `lifecycleScope` | Android | Activity/Fragment destroyed |\n| `rememberCoroutineScope()` | Compose | Composable leaves composition |\n| `MainScope()` | All | Manual cancellation |\n| `GlobalScope` | All | App process lifetime (avoid!) |"
    },
    {
      "type": "WARNING",
      "title": "Never Use GlobalScope",
      "content": "```kotlin\n// ❌ NEVER do this\nGlobalScope.launch {\n    // This coroutine lives forever (until app is killed)\n    // Memory leaks, lifecycle issues, hard to test\n}\n```\n\n**Why GlobalScope is bad:**\n1. No automatic cancellation\n2. Hard to test (coroutines outlive tests)\n3. Can't track running work\n4. Violates structured concurrency principles\n\n**Always use a proper scope:**\n```kotlin\n// ✅ In a ViewModel\nclass MyViewModel : ViewModel() {\n    fun loadData() {\n        viewModelScope.launch {\n            // Automatically cancelled when ViewModel is cleared\n        }\n    }\n}\n\n// ✅ In a Composable\n@Composable\nfun MyScreen() {\n    val scope = rememberCoroutineScope()\n    Button(onClick = {\n        scope.launch {\n            // Cancelled when composable leaves\n        }\n    }) {\n        Text(\"Load\")\n    }\n}\n```"
    },
    {
      "type": "THEORY",
      "title": "Parent-Child Relationships",
      "content": "```kotlin\nfun main() = runBlocking {\n    println(\"Parent starting\")\n    \n    launch {  // Child 1\n        delay(1000)\n        println(\"Child 1 done\")\n    }\n    \n    launch {  // Child 2\n        delay(2000)\n        println(\"Child 2 done\")\n    }\n    \n    println(\"Parent waiting for children...\")\n    // runBlocking automatically waits for children\n}\n\n// Output:\n// Parent starting\n// Parent waiting for children...\n// Child 1 done\n// Child 2 done\n```\n\n### Cancellation Propagates Down\n\n```kotlin\nfun main() = runBlocking {\n    val parentJob = launch {\n        launch { // Child 1\n            repeat(1000) { i ->\n                println(\"Child 1: $i\")\n                delay(500)\n            }\n        }\n        \n        launch { // Child 2\n            repeat(1000) { i ->\n                println(\"Child 2: $i\")\n                delay(500)\n            }\n        }\n    }\n    \n    delay(1100)  // Let them run briefly\n    parentJob.cancel()  // Cancel parent → cancels both children\n    println(\"Parent cancelled\")\n}\n```"
    },
    {
      "type": "THEORY",
      "title": "coroutineScope vs supervisorScope",
      "content": "**coroutineScope** - One child fails, all siblings are cancelled:\n```kotlin\nsuspend fun loadAllData() = coroutineScope {\n    val user = async { fetchUser() }        // If this fails...\n    val posts = async { fetchPosts() }       // ...this is cancelled\n    val settings = async { fetchSettings() } // ...and this too\n    \n    Triple(user.await(), posts.await(), settings.await())\n}\n```\n\n**supervisorScope** - Children are independent:\n```kotlin\nsuspend fun loadAllDataIndependently() = supervisorScope {\n    val user = async { fetchUser() }        // If this fails...\n    val posts = async { fetchPosts() }       // ...this keeps running\n    val settings = async { fetchSettings() } // ...and this too\n    \n    // Handle failures individually\n    val userData = runCatching { user.await() }.getOrNull()\n    val postsData = runCatching { posts.await() }.getOrNull()\n    val settingsData = runCatching { settings.await() }.getOrNull()\n    \n    Triple(userData, postsData, settingsData)\n}\n```\n\n**When to use which:**\n- `coroutineScope`: When all operations must succeed (transactional)\n- `supervisorScope`: When operations are independent (dashboard loading)"
    },
    {
      "type": "EXAMPLE",
      "title": "Complete Example: Loading a Profile Screen",
      "content": "Here's a real-world pattern for loading a screen with multiple data sources:",
      "code": "import kotlinx.coroutines.*\n\ndata class User(val id: String, val name: String)\ndata class Post(val id: String, val title: String)\ndata class ProfileScreenState(\n    val user: User? = null,\n    val posts: List<Post> = emptyList(),\n    val isLoading: Boolean = true,\n    val error: String? = null\n)\n\nclass ProfileViewModel {\n    private val scope = CoroutineScope(Dispatchers.Default + SupervisorJob())\n    \n    var state = ProfileScreenState()\n        private set\n    \n    fun loadProfile(userId: String) {\n        scope.launch {\n            state = state.copy(isLoading = true, error = null)\n            \n            try {\n                // Load user and posts in parallel\n                coroutineScope {\n                    val userDeferred = async { fetchUser(userId) }\n                    val postsDeferred = async { fetchPosts(userId) }\n                    \n                    state = state.copy(\n                        user = userDeferred.await(),\n                        posts = postsDeferred.await(),\n                        isLoading = false\n                    )\n                }\n            } catch (e: Exception) {\n                state = state.copy(\n                    isLoading = false,\n                    error = e.message\n                )\n            }\n        }\n    }\n    \n    fun onCleared() {\n        scope.cancel() // Clean up when ViewModel is destroyed\n    }\n    \n    private suspend fun fetchUser(id: String): User {\n        delay(1000) // Simulate network\n        return User(id, \"John Doe\")\n    }\n    \n    private suspend fun fetchPosts(userId: String): List<Post> {\n        delay(800)\n        return listOf(Post(\"1\", \"First Post\"), Post(\"2\", \"Second Post\"))\n    }\n}\n\nfun main() = runBlocking {\n    val viewModel = ProfileViewModel()\n    viewModel.loadProfile(\"user123\")\n    \n    delay(1500) // Wait for loading\n    println(\"State: ${viewModel.state}\")\n    \n    viewModel.onCleared()\n}",
      "language": "kotlin"
    }
  ],
  "challenges": []
}
```

---

### Lesson 04A.3: Dispatchers and Context

**Step 1: Create the lesson**

Full lesson content covering:
- What dispatchers are
- Dispatchers.Default, Dispatchers.IO, Dispatchers.Main
- Platform-specific dispatchers in KMP
- withContext for switching dispatchers
- CoroutineContext composition

### Lesson 04A.4: Exception Handling in Coroutines

Full lesson content covering:
- try-catch in coroutines
- CoroutineExceptionHandler
- Exception propagation rules
- supervisorScope for independent failures
- runCatching for safe execution

### Lesson 04A.5: Kotlin Flow - Reactive Streams

Full lesson content covering:
- What is Flow and why use it
- Creating flows with flow { }
- Collecting flows
- Flow operators (map, filter, transform)
- Terminal operators (collect, first, toList)
- Cold vs Hot flows

### Lesson 04A.6: StateFlow and SharedFlow

Full lesson content covering:
- StateFlow for UI state
- SharedFlow for events
- MutableStateFlow usage
- Collecting flows in Compose
- KMP-specific considerations

### Lesson 04A.7: Flows Across Platforms

Full lesson content covering:
- Using flows in shared KMP code
- Collecting flows on Android with lifecycleScope
- Collecting flows on iOS with SKIE
- CommonFlow patterns
- Testing flows

---

## Task 3: Create Module 06A - Persistence with SQLDelight

**Files:**
- Create: New module section in `content/courses/kotlin/course.json`

### Module Structure

```
Module 06A: Persistence with SQLDelight
├── Lesson 06A.1: Introduction to SQLDelight
├── Lesson 06A.2: Setting Up SQLDelight in KMP
├── Lesson 06A.3: Writing SQL Queries
├── Lesson 06A.4: Migrations and Schema Changes
├── Lesson 06A.5: Flows and Reactive Queries
├── Lesson 06A.6: Platform-Specific Drivers
└── Lesson 06A.7: Secure Storage Patterns
```

### Lesson 06A.1: Introduction to SQLDelight

Key content:
- What SQLDelight is and why it's preferred for KMP
- Comparison with Room (Android-only) and Core Data (iOS-only)
- Type-safe SQL with compile-time verification
- Current version: SQLDelight 2.x (as of 2025)

### Lesson 06A.2: Setting Up SQLDelight in KMP

Key content:
- Gradle plugin setup (`app.cash.sqldelight` version 2.0.x)
- Platform-specific driver dependencies:
  - `android-driver` for Android
  - `native-driver` for iOS
  - `sqlite-driver` for desktop
- Database configuration in build.gradle.kts
- Generating code from SQL files

### Lesson 06A.3: Writing SQL Queries

Complete content:
```kotlin
// In commonMain/sqldelight/com/example/app/data/Notes.sq

CREATE TABLE Note (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    content TEXT NOT NULL,
    created_at INTEGER NOT NULL,
    updated_at INTEGER NOT NULL
);

getAllNotes:
SELECT * FROM Note ORDER BY updated_at DESC;

getNoteById:
SELECT * FROM Note WHERE id = ?;

insertNote:
INSERT INTO Note(title, content, created_at, updated_at)
VALUES (?, ?, ?, ?);

updateNote:
UPDATE Note
SET title = ?, content = ?, updated_at = ?
WHERE id = ?;

deleteNote:
DELETE FROM Note WHERE id = ?;

searchNotes:
SELECT * FROM Note
WHERE title LIKE '%' || ? || '%'
   OR content LIKE '%' || ? || '%'
ORDER BY updated_at DESC;
```

### Lesson 06A.4: Migrations and Schema Changes

Key content:
- Migration files (1.sqm, 2.sqm, etc.)
- Version tracking
- Safe migration patterns
- Testing migrations

### Lesson 06A.5: Flows and Reactive Queries

Key content:
- `asFlow()` extension for reactive updates
- Combining with StateFlow
- Automatic UI updates when data changes

```kotlin
// In your repository
class NoteRepository(private val database: AppDatabase) {
    fun observeAllNotes(): Flow<List<Note>> {
        return database.notesQueries
            .getAllNotes()
            .asFlow()
            .mapToList(Dispatchers.Default)
    }
}
```

---

## Task 4: Create Module 06B - KMP Architecture Patterns

**Files:**
- Create: New module section in `content/courses/kotlin/course.json`

### Module Structure

```
Module 06B: KMP Architecture Patterns
├── Lesson 06B.1: Why Architecture Matters
├── Lesson 06B.2: Clean Architecture for KMP
├── Lesson 06B.3: MVVM Pattern Implementation
├── Lesson 06B.4: MVI Pattern Implementation
├── Lesson 06B.5: Shared ViewModels in KMP
├── Lesson 06B.6: Navigation Patterns
└── Lesson 06B.7: Architecture in Practice
```

### Key Content for Lesson 06B.3: MVVM Pattern Implementation

```kotlin
// Shared ViewModel in commonMain
class NotesViewModel(
    private val noteRepository: NoteRepository,
    private val dispatcher: CoroutineDispatcher = Dispatchers.Default
) {
    private val viewModelScope = CoroutineScope(dispatcher + SupervisorJob())

    private val _uiState = MutableStateFlow(NotesUiState())
    val uiState: StateFlow<NotesUiState> = _uiState.asStateFlow()

    init {
        loadNotes()
    }

    private fun loadNotes() {
        viewModelScope.launch {
            noteRepository.observeAllNotes().collect { notes ->
                _uiState.update { it.copy(notes = notes, isLoading = false) }
            }
        }
    }

    fun addNote(title: String, content: String) {
        viewModelScope.launch {
            _uiState.update { it.copy(isLoading = true) }
            noteRepository.addNote(title, content)
            // Flow will automatically update the notes list
        }
    }

    fun deleteNote(noteId: Long) {
        viewModelScope.launch {
            noteRepository.deleteNote(noteId)
        }
    }

    fun onCleared() {
        viewModelScope.cancel()
    }
}

data class NotesUiState(
    val notes: List<Note> = emptyList(),
    val isLoading: Boolean = true,
    val error: String? = null
)
```

---

## Task 5: Create Module 06C - Dependency Injection with Koin

**Files:**
- Create: New module section in `content/courses/kotlin/course.json`

### Module Structure

```
Module 06C: Dependency Injection with Koin
├── Lesson 06C.1: Why Dependency Injection?
├── Lesson 06C.2: Koin Fundamentals
├── Lesson 06C.3: Koin in KMP Projects
├── Lesson 06C.4: Platform-Specific Dependencies
├── Lesson 06C.5: Koin Annotations (Modern Approach)
├── Lesson 06C.6: Testing with Koin
└── Lesson 06C.7: Advanced Patterns
```

### Key Content for Lesson 06C.3: Koin in KMP Projects

```kotlin
// commonMain/src/di/CommonModule.kt
val commonModule = module {
    // Repositories
    single<NoteRepository> { NoteRepositoryImpl(get()) }

    // Use cases
    factory { GetAllNotesUseCase(get()) }
    factory { AddNoteUseCase(get()) }
    factory { DeleteNoteUseCase(get()) }

    // ViewModels
    viewModel { NotesViewModel(get()) }
}

// androidMain/src/di/AndroidModule.kt
val androidModule = module {
    // Android-specific: database driver
    single {
        AndroidSqliteDriver(
            schema = AppDatabase.Schema,
            context = get(),
            name = "notes.db"
        )
    }
    single { AppDatabase(get()) }
}

// iosMain/src/di/IosModule.kt
val iosModule = module {
    single {
        NativeSqliteDriver(
            schema = AppDatabase.Schema,
            name = "notes.db"
        )
    }
    single { AppDatabase(get()) }
}

// Shared initialization
fun initKoin(platformModule: Module) {
    startKoin {
        modules(commonModule, platformModule)
    }
}
```

---

## Task 6: Create Module 07A - Testing KMP Applications

**Files:**
- Create: New module section in `content/courses/kotlin/course.json`

This is a CRITICAL gap - the current course has no testing content.

### Module Structure

```
Module 07A: Testing KMP Applications
├── Lesson 07A.1: Testing Philosophy in KMP
├── Lesson 07A.2: Unit Testing Shared Code
├── Lesson 07A.3: Testing Coroutines and Flows
├── Lesson 07A.4: Mocking in KMP (Limitations and Workarounds)
├── Lesson 07A.5: UI Testing with Compose Multiplatform
├── Lesson 07A.6: Integration Testing
└── Lesson 07A.7: CI/CD for KMP Testing
```

### Key Content for Lesson 07A.2: Unit Testing Shared Code

Based on [KMPShip Testing Guide 2025](https://www.kmpship.app/blog/kotlin-multiplatform-testing-guide-2025):

```kotlin
// commonTest/kotlin/data/NoteRepositoryTest.kt
class NoteRepositoryTest {
    private lateinit var repository: NoteRepository
    private lateinit var database: AppDatabase

    @BeforeTest
    fun setup() {
        // Use in-memory database for testing
        val driver = JdbcSqliteDriver(JdbcSqliteDriver.IN_MEMORY)
        AppDatabase.Schema.create(driver)
        database = AppDatabase(driver)
        repository = NoteRepositoryImpl(database)
    }

    @AfterTest
    fun teardown() {
        database.close()
    }

    @Test
    fun `addNote should insert note into database`() = runTest {
        // Given
        val title = "Test Note"
        val content = "Test Content"

        // When
        repository.addNote(title, content)

        // Then
        val notes = repository.getAllNotes().first()
        assertEquals(1, notes.size)
        assertEquals(title, notes[0].title)
        assertEquals(content, notes[0].content)
    }

    @Test
    fun `deleteNote should remove note from database`() = runTest {
        // Given
        repository.addNote("Note to delete", "Content")
        val notes = repository.getAllNotes().first()
        val noteId = notes[0].id

        // When
        repository.deleteNote(noteId)

        // Then
        val remainingNotes = repository.getAllNotes().first()
        assertTrue(remainingNotes.isEmpty())
    }
}
```

### Key Content for Lesson 07A.3: Testing Coroutines and Flows

```kotlin
class NotesViewModelTest {
    @Test
    fun `uiState should emit notes when repository has data`() = runTest {
        // Given
        val fakeRepository = FakeNoteRepository()
        fakeRepository.addTestNote("Title", "Content")

        val viewModel = NotesViewModel(
            noteRepository = fakeRepository,
            dispatcher = StandardTestDispatcher(testScheduler)
        )

        // When
        advanceUntilIdle() // Process all coroutines

        // Then
        val state = viewModel.uiState.value
        assertFalse(state.isLoading)
        assertEquals(1, state.notes.size)
    }

    @Test
    fun `addNote should update uiState with new note`() = runTest {
        val fakeRepository = FakeNoteRepository()
        val viewModel = NotesViewModel(
            noteRepository = fakeRepository,
            dispatcher = StandardTestDispatcher(testScheduler)
        )

        advanceUntilIdle()

        // When
        viewModel.addNote("New Title", "New Content")
        advanceUntilIdle()

        // Then
        assertEquals(1, viewModel.uiState.value.notes.size)
    }
}
```

---

## Task 7: Enhance Module 07 - Deployment and Distribution

**Files:**
- Modify: `content/courses/kotlin/course.json` (Module 07)

### New Lessons to Add

Based on [Marco Gomiero's CI/CD series](https://www.marcogomiero.com/posts/2024/kmp-app-ci/) and [KMPShip CI/CD Guide](https://www.kmpship.app/blog/ci-cd-kotlin-multiplatform-2025):

```
Module 07: Professional Development & Deployment (Enhanced)
├── Existing lessons...
├── NEW: Lesson 07.x: Android App Signing
├── NEW: Lesson 07.x: iOS Provisioning Profiles and Certificates
├── NEW: Lesson 07.x: GitHub Actions for KMP
├── NEW: Lesson 07.x: Deploying to Google Play Store
├── NEW: Lesson 07.x: Deploying to App Store via TestFlight
└── NEW: Lesson 07.x: Fastlane Automation
```

### Key Content: GitHub Actions Workflow

```yaml
# .github/workflows/build-and-test.yml
name: Build and Test KMP

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build-android:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Set up JDK 17
        uses: actions/setup-java@v4
        with:
          java-version: '17'
          distribution: 'temurin'

      - name: Setup Gradle
        uses: gradle/gradle-build-action@v2

      - name: Run Tests (commonTest on JVM)
        run: ./gradlew :shared:jvmTest

      - name: Build Android Debug
        run: ./gradlew :androidApp:assembleDebug

      - name: Upload APK
        uses: actions/upload-artifact@v4
        with:
          name: android-debug-apk
          path: androidApp/build/outputs/apk/debug/*.apk

  build-ios:
    runs-on: macos-14
    steps:
      - uses: actions/checkout@v4

      - name: Set up JDK 17
        uses: actions/setup-java@v4
        with:
          java-version: '17'
          distribution: 'temurin'

      - name: Run iOS Tests
        run: ./gradlew :shared:iosSimulatorArm64Test

      - name: Build iOS Framework
        run: ./gradlew :shared:linkDebugFrameworkIosSimulatorArm64
```

### Key Content: Play Store Deployment

```yaml
# .github/workflows/deploy-android.yml
name: Deploy to Play Store

on:
  push:
    tags:
      - 'v*-android'

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Set up JDK 17
        uses: actions/setup-java@v4
        with:
          java-version: '17'
          distribution: 'temurin'

      - name: Decode Keystore
        env:
          KEYSTORE_BASE64: ${{ secrets.KEYSTORE_BASE64 }}
        run: echo "$KEYSTORE_BASE64" | base64 -d > keystore.jks

      - name: Build Release Bundle
        env:
          KEYSTORE_PASSWORD: ${{ secrets.KEYSTORE_PASSWORD }}
          KEY_ALIAS: ${{ secrets.KEY_ALIAS }}
          KEY_PASSWORD: ${{ secrets.KEY_PASSWORD }}
        run: ./gradlew :androidApp:bundleRelease

      - name: Upload to Play Store
        uses: r0adkll/upload-google-play@v1
        with:
          serviceAccountJsonPlainText: ${{ secrets.PLAY_STORE_CONFIG_JSON }}
          packageName: com.example.app
          releaseFiles: androidApp/build/outputs/bundle/release/*.aab
          track: internal
```

---

## Task 8: Create Module 11 - Capstone Project

**Files:**
- Create: New module section in `content/courses/kotlin/course.json`

### Module Structure

```
Module 11: Capstone - Cross-Platform Notes App
├── Lesson 11.1: Project Overview and Requirements
├── Lesson 11.2: Setting Up the KMP Project
├── Lesson 11.3: Database Layer with SQLDelight
├── Lesson 11.4: Repository and Use Cases
├── Lesson 11.5: Shared ViewModel with Flows
├── Lesson 11.6: Android UI with Compose
├── Lesson 11.7: iOS UI with Compose Multiplatform
├── Lesson 11.8: Adding Search and Filtering
├── Lesson 11.9: Offline-First with Sync
├── Lesson 11.10: Testing the Full Stack
├── Lesson 11.11: CI/CD Pipeline Setup
└── Lesson 11.12: Publishing to Stores
```

### Lesson 11.1: Project Overview and Requirements

```json
{
  "id": "11.1",
  "title": "Lesson 11.1: Project Overview and Requirements",
  "moduleId": "module-11",
  "order": 1,
  "estimatedMinutes": 30,
  "difficulty": "advanced",
  "contentSections": [
    {
      "type": "THEORY",
      "title": "Introduction",
      "content": "**Estimated Time**: 30 minutes\n\nWelcome to the Capstone Project! Over the next 12 lessons, you'll build a complete, production-ready Notes app that runs on Android and iOS from a single codebase."
    },
    {
      "type": "THEORY",
      "title": "What You'll Build",
      "content": "## NoteSync - A Cross-Platform Notes App\n\n### Features:\n- Create, edit, and delete notes with title and content\n- Search notes by title or content\n- Organize notes with tags/categories\n- Offline-first: works without internet\n- Sync across devices (simulated with local backend)\n- Dark/Light theme support\n- Native performance on both platforms\n\n### Technical Stack:\n\n| Layer | Technology |\n|-------|------------|\n| UI | Compose Multiplatform |\n| State Management | StateFlow + MVVM |\n| Networking | Ktor Client |\n| Database | SQLDelight |\n| DI | Koin |\n| Testing | kotlin.test + Turbine |\n| CI/CD | GitHub Actions + Fastlane |\n\n### Code Sharing:\n- **95%** shared business logic (data, domain, viewmodels)\n- **90%** shared UI (Compose Multiplatform)\n- **5%** platform-specific (drivers, system APIs)"
    },
    {
      "type": "THEORY",
      "title": "Project Architecture",
      "content": "We'll use Clean Architecture with three layers:\n\n```\n┌─────────────────────────────────────────────┐\n│           UI Layer (Compose)                │\n│    - Screens, Components, Navigation       │\n└─────────────────┬───────────────────────────┘\n                  │\n┌─────────────────▼───────────────────────────┐\n│         Domain Layer (Pure Kotlin)          │\n│    - Use Cases, Entities, Repository Interfaces │\n└─────────────────┬───────────────────────────┘\n                  │\n┌─────────────────▼───────────────────────────┐\n│           Data Layer                        │\n│    - Repository Impl, SQLDelight, Ktor      │\n└─────────────────────────────────────────────┘\n```\n\n### Project Structure:\n```\nshared/\n├── src/\n│   ├── commonMain/\n│   │   ├── kotlin/\n│   │   │   ├── data/\n│   │   │   │   ├── local/          # SQLDelight\n│   │   │   │   ├── remote/         # Ktor Client\n│   │   │   │   └── repository/     # Repository Impl\n│   │   │   ├── domain/\n│   │   │   │   ├── model/          # Domain Entities\n│   │   │   │   ├── repository/     # Repository Interfaces\n│   │   │   │   └── usecase/        # Use Cases\n│   │   │   ├── presentation/\n│   │   │   │   └── viewmodel/      # ViewModels\n│   │   │   └── di/                 # Koin Modules\n│   │   └── sqldelight/             # SQL Queries\n│   ├── androidMain/                # Android-specific\n│   └── iosMain/                    # iOS-specific\ncomposeApp/\n├── src/\n│   └── commonMain/\n│       └── kotlin/\n│           └── ui/                 # Compose Screens\n```"
    },
    {
      "type": "THEORY",
      "title": "Prerequisites Checklist",
      "content": "Before starting this capstone, ensure you've completed:\n\n✅ Module 01-03: Kotlin basics, control flow, OOP\n✅ Module 04: Advanced Kotlin (generics, extensions)\n✅ Module 04A: Coroutines & Flows\n✅ Module 05: Ktor basics (we'll use client features)\n✅ Module 06: Compose Multiplatform\n✅ Module 06A: SQLDelight\n✅ Module 06B: Architecture Patterns\n✅ Module 06C: Koin DI\n✅ Module 07A: Testing\n\nIf you've skipped any modules, go back and complete them first. This capstone integrates everything you've learned."
    }
  ],
  "challenges": []
}
```

---

## Task 9: Add Expect/Actual Content

**Files:**
- Modify: `content/courses/kotlin/course.json` (add to Module 04 or create new lesson)

### New Lesson: Expect/Actual Declarations

```kotlin
// commonMain - Declare the expectation
expect class PlatformContext

expect fun getPlatformName(): String

expect fun createUUID(): String

// androidMain - Fulfill for Android
actual typealias PlatformContext = Context

actual fun getPlatformName(): String = "Android ${Build.VERSION.RELEASE}"

actual fun createUUID(): String = UUID.randomUUID().toString()

// iosMain - Fulfill for iOS
actual class PlatformContext

actual fun getPlatformName(): String = UIDevice.currentDevice.systemName + " " + UIDevice.currentDevice.systemVersion

actual fun createUUID(): String = NSUUID().UUIDString
```

---

## Task 10: Version Update Lesson

**Files:**
- Modify: Add version guidance throughout

### Content to Add in Module Setup Sections

```markdown
## Staying Current with KMP Versions

The Kotlin Multiplatform ecosystem evolves rapidly. Here's how to stay current:

### Version Compatibility Matrix (Late 2025)

| Component | Minimum | Recommended | Notes |
|-----------|---------|-------------|-------|
| Kotlin | 2.1.0 | 2.3.0 | Required for Compose MP 1.8+ |
| Compose MP | 1.8.0 | Latest 1.8.x | iOS stable since May 2025 |
| Ktor | 3.0.0 | 3.3.x | Major version for KMP |
| SQLDelight | 2.0.0 | 2.0.x | Breaking changes from 1.x |
| Koin | 4.0.0 | 4.0.x | Full KMP support |

### Checking for Updates

1. **Kotlin**: https://kotlinlang.org/docs/releases.html
2. **Compose Multiplatform**: https://github.com/JetBrains/compose-multiplatform/releases
3. **Ktor**: https://ktor.io/docs/releases.html
4. **SQLDelight**: https://github.com/cashapp/sqldelight/releases

### Updating Your Project

In `gradle/libs.versions.toml`:
```toml
[versions]
kotlin = "2.3.0"
compose-multiplatform = "1.8.0"
ktor = "3.3.3"
sqldelight = "2.0.2"
koin = "4.0.0"
```
```

---

## Implementation Schedule

### Phase 1: Critical Gaps (Weeks 1-3)
- Task 1: Update tooling version references
- Task 2: Create Module 04A (Coroutines) - 7 lessons
- Task 3: Create Module 06A (SQLDelight) - 7 lessons

### Phase 2: Architecture & DI (Weeks 4-5)
- Task 4: Create Module 06B (Architecture) - 7 lessons
- Task 5: Create Module 06C (Koin DI) - 7 lessons

### Phase 3: Testing & Deployment (Weeks 6-7)
- Task 6: Create Module 07A (Testing) - 7 lessons
- Task 7: Enhance Module 07 (Deployment) - 6 new lessons

### Phase 4: Capstone & Polish (Weeks 8-10)
- Task 8: Create Module 11 (Capstone) - 12 lessons
- Task 9: Add Expect/Actual content
- Task 10: Version update documentation

---

## Verification Checklist

Before marking any task complete:

- [ ] All code examples compile and run
- [ ] No placeholder comments like `// TODO` or `// Add code here`
- [ ] All exercises have complete solutions
- [ ] Version numbers match current 2025 releases
- [ ] JSON is valid (run through JSON parser)
- [ ] Content flows logically from previous lessons
- [ ] No references to deprecated APIs

---

## Sources

Research conducted December 31, 2025:

- [Kotlin 2.3.0 Released](https://blog.jetbrains.com/kotlin/2025/12/kotlin-2-3-0-released/)
- [Compose Multiplatform 1.8.0](https://blog.jetbrains.com/kotlin/2025/05/compose-multiplatform-1-8-0-released-compose-multiplatform-for-ios-is-stable-and-production-ready/)
- [KotlinConf 2025 Updates](https://blog.jetbrains.com/kotlin/2025/05/kotlinconf-2025-language-features-ai-powered-development-and-kotlin-multiplatform/)
- [Ktor Client Documentation](https://ktor.io/docs/client-engines.html)
- [SQLDelight Multiplatform Tutorial](https://kotlinlang.org/docs/multiplatform/multiplatform-ktor-sqldelight.html)
- [Koin for KMP](https://insert-koin.io/docs/reference/koin-mp/kmp/)
- [KMP Testing Guide 2025](https://www.kmpship.app/blog/kotlin-multiplatform-testing-guide-2025)
- [CI/CD for KMP 2025](https://www.kmpship.app/blog/ci-cd-kotlin-multiplatform-2025)
- [Marco Gomiero's KMP CI Series](https://www.marcogomiero.com/posts/2024/kmp-app-ci/)
- [Kotlin Coroutines Best Practices](https://proandroiddev.com/inside-kotlin-coroutines-state-machines-continuations-and-structured-concurrency-b8d3d4e48e62)
- [Koin 4.0 Announcement](https://dev.to/arsenikavalchuk/modern-dependency-injection-with-koin-the-smart-di-choice-for-2025-550i)
