# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Advanced Kotlin
- **Lesson:** Lesson 4.9: Advanced Coroutines (ID: 4.9)
- **Difficulty:** intermediate
- **Estimated Time:** 75 minutes

## Current Lesson Content

{
    "id":  "4.9",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 75 minutes\n**Difficulty**: Advanced\n**Prerequisites**: Lesson 4.2 (Coroutines Fundamentals)\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Topic Introduction",
                                "content":  "\nNow that you understand coroutine basics, it\u0027s time to explore the advanced features that make coroutines truly powerful. These features enable you to build reactive systems, handle streams of data, communicate between coroutines, and gracefully handle errors in concurrent code.\n\nIn this lesson, you\u0027ll learn:\n- Structured concurrency patterns\n- Exception handling in coroutines\n- Flows for reactive streams\n- Channels for coroutine communication\n- StateFlow and SharedFlow for state management\n- `withContext` for context switching\n- Advanced dispatchers and supervisors\n\nBy the end, you\u0027ll build production-ready concurrent applications!\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Structured Concurrency",
                                "content":  "\nStructured concurrency ensures coroutines have a clear lifecycle and don\u0027t leak.\n\n### The Principle\n\nCoroutines should:\n1. Have a clear parent-child relationship\n2. Be automatically cancelled when parent is cancelled\n3. Complete or fail together as a unit\n\n\n### `coroutineScope` - Structured Concurrency Builder\n\n`coroutineScope` creates a scope that completes only when all children complete:\n\n\nIf any child fails, all siblings are cancelled:\n\n\n### `supervisorScope` - Independent Children\n\n`supervisorScope` allows children to fail independently:\n\n\n---\n\n",
                                "code":  "suspend fun fetchWithSupervision() = supervisorScope {\n    launch {\n        delay(500)\n        println(\"Task 1 completed\")\n    }\n\n    launch {\n        delay(300)\n        throw RuntimeException(\"Task 2 failed!\")\n    }\n\n    launch {\n        delay(700)\n        println(\"Task 3 completed\")  // Still executes\n    }\n}\n\nfun main() = runBlocking {\n    try {\n        fetchWithSupervision()\n        delay(1000)\n    } catch (e: Exception) {\n        println(\"Caught: ${e.message}\")\n    }\n}\n// Output:\n// Task 1 completed\n// Task 3 completed",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exception Handling in Coroutines",
                                "content":  "\nException handling in coroutines has special rules.\n\n### Try-Catch in Coroutines\n\n\n### Try-Catch Outside Launch (Doesn\u0027t Work!)\n\n\n### Exception Handling with Async\n\n\n### CoroutineExceptionHandler\n\nGlobal exception handler for coroutines:\n\n\n### SupervisorJob for Independent Failures\n\n\n---\n\n",
                                "code":  "fun main() = runBlocking {\n    val supervisor = SupervisorJob()\n    val scope = CoroutineScope(Dispatchers.Default + supervisor)\n\n    val job1 = scope.launch {\n        delay(500)\n        println(\"Job 1 completed\")\n    }\n\n    val job2 = scope.launch {\n        delay(300)\n        throw RuntimeException(\"Job 2 failed!\")\n    }\n\n    val job3 = scope.launch {\n        delay(700)\n        println(\"Job 3 completed\")\n    }\n\n    joinAll(job1, job2, job3)\n    supervisor.cancel()\n}\n// Output:\n// Job 1 completed\n// Job 3 completed",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Flows - Reactive Streams",
                                "content":  "\nFlows represent asynchronous streams of values.\n\n### Basic Flow\n\n\n### Flow Builders\n\n\n### Flow Operators\n\n\n### Flow Context\n\nFlows preserve the context of the collector:\n\n\n### `flowOn` - Change Flow Context\n\n\n### Buffer and Conflate\n\n\n### Combining Flows\n\n\n### Flow Completion\n\n\n---\n\n",
                                "code":  "fun main() = runBlocking {\n    (1..3).asFlow()\n        .onEach { println(\"Emitting $it\") }\n        .onCompletion { println(\"Flow completed\") }\n        .collect { println(\"Collected $it\") }\n\n    // With exception handling\n    flow {\n        emit(1)\n        throw RuntimeException(\"Error!\")\n    }\n        .onCompletion { cause -\u003e\n            if (cause != null) {\n                println(\"Completed with error: ${cause.message}\")\n            }\n        }\n        .catch { println(\"Caught: ${it.message}\") }\n        .collect()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Channels - Communication Between Coroutines",
                                "content":  "\nChannels are hot streams for sending data between coroutines.\n\n### Basic Channel\n\n\n### Producer-Consumer Pattern\n\n\n### Channel Buffering\n\n\n### Fan-out and Fan-in\n\n\n---\n\n",
                                "code":  "// Fan-out - multiple consumers\nfun CoroutineScope.produceNumbers() = produce\u003cInt\u003e {\n    var x = 1\n    while (true) {\n        send(x++)\n        delay(100)\n    }\n}\n\nfun CoroutineScope.consumeNumbers(id: Int, channel: ReceiveChannel\u003cInt\u003e) = launch {\n    for (msg in channel) {\n        println(\"Consumer $id received $msg\")\n    }\n}\n\nfun main() = runBlocking {\n    val producer = produceNumbers()\n\n    repeat(3) {\n        consumeNumbers(it + 1, producer)\n    }\n\n    delay(1000)\n    producer.cancel()\n}\n\n// Fan-in - multiple producers\nsuspend fun sendString(channel: SendChannel\u003cString\u003e, s: String, time: Long) {\n    while (true) {\n        delay(time)\n        channel.send(s)\n    }\n}\n\nfun main2() = runBlocking {\n    val channel = Channel\u003cString\u003e()\n\n    launch { sendString(channel, \"foo\", 200) }\n    launch { sendString(channel, \"bar\", 500) }\n\n    repeat(10) {\n        println(channel.receive())\n    }\n\n    coroutineContext.cancelChildren()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "StateFlow and SharedFlow - Modern State Management",
                                "content":  "\nStateFlow and SharedFlow are the modern, recommended way to handle observable state and events in Kotlin. They are hot flows that actively maintain values or broadcast events, making them ideal for UI state management in Android and other reactive applications.\n\n### Why StateFlow and SharedFlow?\n\n**Modern Alternative to LiveData**:\n- Kotlin-first, not Android-specific\n- Works with coroutines natively\n- Better nullability handling\n- No lifecycle awareness required (more flexible)\n\n### StateFlow - Observable State Container\n\nStateFlow always holds a current value and emits it to new collectors immediately.\n\n**Key Characteristics**:\n- Always has a value (never null unless explicitly typed as nullable)\n- Conflates duplicate consecutive values (won\u0027t emit same value twice)\n- Hot flow - active regardless of collectors\n- Thread-safe value updates\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "StateFlow for UI State",
                                "content":  "StateFlow maintains a current value that\u0027s immediately available to new collectors. Use MutableStateFlow privately and expose read-only StateFlow publicly. The update() function provides atomic state modifications, and copy() enables immutable state updates.",
                                "code":  "import kotlinx.coroutines.*\nimport kotlinx.coroutines.flow.*\n\n// UI State pattern - commonly used in Android MVVM\ndata class UiState(\n    val isLoading: Boolean = false,\n    val data: List\u003cString\u003e = emptyList(),\n    val error: String? = null\n)\n\nclass ViewModel {\n    // Private mutable state\n    private val _uiState = MutableStateFlow(UiState())\n    \n    // Public read-only state\n    val uiState: StateFlow\u003cUiState\u003e = _uiState.asStateFlow()\n    \n    suspend fun loadData() {\n        // Show loading\n        _uiState.value = _uiState.value.copy(isLoading = true)\n        \n        try {\n            delay(1000) // Simulate network call\n            val data = listOf(\"Item 1\", \"Item 2\", \"Item 3\")\n            \n            // Update with data\n            _uiState.value = _uiState.value.copy(\n                isLoading = false,\n                data = data,\n                error = null\n            )\n        } catch (e: Exception) {\n            _uiState.value = _uiState.value.copy(\n                isLoading = false,\n                error = e.message\n            )\n        }\n    }\n    \n    // Alternative: use update for atomic updates\n    fun addItem(item: String) {\n        _uiState.update { currentState -\u003e\n            currentState.copy(data = currentState.data + item)\n        }\n    }\n}\n\nfun main() = runBlocking {\n    val viewModel = ViewModel()\n    \n    // Collector (simulating UI)\n    val job = launch {\n        viewModel.uiState.collect { state -\u003e\n            when {\n                state.isLoading -\u003e println(\"Loading...\")\n                state.error != null -\u003e println(\"Error: ${state.error}\")\n                else -\u003e println(\"Data: ${state.data}\")\n            }\n        }\n    }\n    \n    delay(100)\n    viewModel.loadData()\n    delay(100)\n    viewModel.addItem(\"New Item\")\n    delay(100)\n    \n    job.cancel()\n}\n// Output:\n// Data: []\n// Loading...\n// Data: [Item 1, Item 2, Item 3]\n// Data: [Item 1, Item 2, Item 3, New Item]",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "SharedFlow - Event Broadcasting",
                                "content":  "\nSharedFlow is designed for events that should be delivered to all collectors. Unlike StateFlow, it doesn\u0027t hold a current value by default.\n\n### When to Use SharedFlow\n\n**One-time events**:\n- Navigation events\n- Snackbar/Toast messages\n- Error notifications\n- Analytics events\n\n### SharedFlow Configuration\n\n```kotlin\nMutableSharedFlow\u003cT\u003e(\n    replay: Int = 0,           // How many past values new collectors get\n    extraBufferCapacity: Int = 0,  // Additional buffer capacity\n    onBufferOverflow: BufferOverflow = SUSPEND  // What to do when buffer full\n)\n```\n\n### BufferOverflow Options\n\n- `SUSPEND` - Suspend emitter until buffer has space\n- `DROP_OLDEST` - Drop oldest value when buffer full\n- `DROP_LATEST` - Drop newest value when buffer full\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "SharedFlow for Events",
                                "content":  "SharedFlow broadcasts one-time events to all collectors without replay by default. Use sealed classes to define event types, and emit() to send events. Events like navigation, snackbars, and loading indicators are perfect use cases.",
                                "code":  "import kotlinx.coroutines.*\nimport kotlinx.coroutines.flow.*\n\n// Event types for one-time UI events\nsealed class UiEvent {\n    data class ShowSnackbar(val message: String) : UiEvent()\n    data class Navigate(val route: String) : UiEvent()\n    data object ShowLoading : UiEvent()\n    data object HideLoading : UiEvent()\n}\n\nclass EventViewModel {\n    // SharedFlow for events - no replay, events are one-time\n    private val _events = MutableSharedFlow\u003cUiEvent\u003e()\n    val events: SharedFlow\u003cUiEvent\u003e = _events.asSharedFlow()\n    \n    suspend fun login(username: String, password: String) {\n        _events.emit(UiEvent.ShowLoading)\n        \n        try {\n            delay(1000) // Simulate network call\n            \n            if (username == \"admin\" \u0026\u0026 password == \"123\") {\n                _events.emit(UiEvent.HideLoading)\n                _events.emit(UiEvent.Navigate(\"/dashboard\"))\n            } else {\n                _events.emit(UiEvent.HideLoading)\n                _events.emit(UiEvent.ShowSnackbar(\"Invalid credentials\"))\n            }\n        } catch (e: Exception) {\n            _events.emit(UiEvent.HideLoading)\n            _events.emit(UiEvent.ShowSnackbar(\"Network error: ${e.message}\"))\n        }\n    }\n}\n\nfun main() = runBlocking {\n    val viewModel = EventViewModel()\n    \n    // Event collector (simulating UI event handler)\n    val job = launch {\n        viewModel.events.collect { event -\u003e\n            when (event) {\n                is UiEvent.ShowSnackbar -\u003e println(\"SNACKBAR: ${event.message}\")\n                is UiEvent.Navigate -\u003e println(\"NAVIGATE TO: ${event.route}\")\n                UiEvent.ShowLoading -\u003e println(\"SHOW LOADING SPINNER\")\n                UiEvent.HideLoading -\u003e println(\"HIDE LOADING SPINNER\")\n            }\n        }\n    }\n    \n    delay(100)\n    println(\"--- Attempting login ---\")\n    viewModel.login(\"admin\", \"123\")\n    \n    delay(1500)\n    println(\"--- Failed login attempt ---\")\n    viewModel.login(\"user\", \"wrong\")\n    \n    delay(1500)\n    job.cancel()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "StateFlow vs SharedFlow vs LiveData",
                                "content":  "\n### Comparison Table\n\n| Feature | StateFlow | SharedFlow | LiveData |\n|---------|-----------|------------|----------|\n| Initial value | Required | Not required | Not required |\n| Null values | Explicit | Explicit | Implicit |\n| Lifecycle-aware | No | No | Yes |\n| Conflation | Always | Configurable | Always |\n| Replay | Always 1 | Configurable | Always 1 |\n| Platform | Multiplatform | Multiplatform | Android only |\n\n### When to Use What\n\n**StateFlow**:\n- UI state that should always have a value\n- Observable properties\n- When you need `.value` access\n- Counter, form state, loading state\n\n**SharedFlow**:\n- One-time events\n- Messages to multiple subscribers\n- Events that shouldn\u0027t be replayed\n- Navigation, snackbars, analytics\n\n**LiveData** (Android legacy):\n- When lifecycle awareness is critical\n- Simple Android-only projects\n- Existing codebases using it\n\n### Modern Recommendation\n\nFor new Android projects, prefer **StateFlow for state** and **SharedFlow for events**. They integrate better with Kotlin coroutines and work across platforms.\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "SharedFlow with Replay",
                                "content":  "The replay parameter determines how many past values new collectors receive. With replay=2, late collectors get the last 2 emitted values immediately. This is useful for caching recent events or ensuring collectors don\u0027t miss important updates.",
                                "code":  "import kotlinx.coroutines.*\nimport kotlinx.coroutines.flow.*\n\nfun main() = runBlocking {\n    // SharedFlow with replay = 2 keeps last 2 values\n    val sharedFlow = MutableSharedFlow\u003cInt\u003e(replay = 2)\n    \n    // Emit values before any collectors\n    sharedFlow.emit(1)\n    sharedFlow.emit(2)\n    sharedFlow.emit(3)\n    \n    println(\"=== Collector 1 joins (gets replay) ===\")\n    val job1 = launch {\n        sharedFlow.collect { println(\"Collector 1: $it\") }\n    }\n    \n    delay(100)\n    \n    println(\"\\n=== Emitting new value ===\")\n    sharedFlow.emit(4)\n    \n    delay(100)\n    \n    println(\"\\n=== Collector 2 joins (gets replay) ===\")\n    val job2 = launch {\n        sharedFlow.collect { println(\"Collector 2: $it\") }\n    }\n    \n    delay(100)\n    \n    println(\"\\n=== Emitting another value ===\")\n    sharedFlow.emit(5)\n    \n    delay(100)\n    job1.cancel()\n    job2.cancel()\n}\n// Output:\n// === Collector 1 joins (gets replay) ===\n// Collector 1: 2\n// Collector 1: 3\n//\n// === Emitting new value ===\n// Collector 1: 4\n//\n// === Collector 2 joins (gets replay) ===\n// Collector 2: 3\n// Collector 2: 4\n//\n// === Emitting another value ===\n// Collector 1: 5\n// Collector 2: 5",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Advanced Context Switching",
                                "content":  "\n### `withContext` - Temporary Context Switch\n\n\n### Context Elements\n\n\n---\n\n",
                                "code":  "fun main() = runBlocking {\n    val context = CoroutineName(\"MyCoroutine\") + Dispatchers.Default\n\n    launch(context) {\n        println(\"Running in: ${coroutineContext[CoroutineName]?.name}\")\n        println(\"On thread: ${Thread.currentThread().name}\")\n    }\n\n    delay(100)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercises",
                                "content":  "\n### Exercise 1: Temperature Monitor with Flow (Medium)\n\nCreate a temperature monitoring system using Flow.\n\n**Requirements**:\n- Generate random temperatures every second\n- Filter temperatures above 30°C\n- Calculate running average\n- Emit alerts for high temperatures\n\n**Solution**:\n\n\n### Exercise 2: Download Manager with Channels (Hard)\n\nBuild a concurrent download manager using channels.\n\n**Requirements**:\n- Multiple download workers\n- Task queue with channel\n- Progress reporting\n- Completion notification\n\n**Solution**:\n\n\n### Exercise 3: Real-Time Chat with StateFlow (Hard)\n\nCreate a simple chat system with StateFlow for state management.\n\n**Requirements**:\n- User state (online/offline)\n- Message history\n- Real-time updates\n- Multiple observers\n\n**Solution**:\n\n\n---\n\n",
                                "code":  "import kotlinx.coroutines.*\nimport kotlinx.coroutines.flow.*\n\ndata class Message(val user: String, val text: String, val timestamp: Long)\ndata class ChatState(\n    val users: Set\u003cString\u003e,\n    val messages: List\u003cMessage\u003e\n)\n\nclass ChatRoom {\n    private val _state = MutableStateFlow(ChatState(emptySet(), emptyList()))\n    val state: StateFlow\u003cChatState\u003e = _state\n\n    fun userJoin(username: String) {\n        _state.value = _state.value.copy(\n            users = _state.value.users + username,\n            messages = _state.value.messages + Message(\n                \"System\",\n                \"$username joined\",\n                System.currentTimeMillis()\n            )\n        )\n    }\n\n    fun userLeave(username: String) {\n        _state.value = _state.value.copy(\n            users = _state.value.users - username,\n            messages = _state.value.messages + Message(\n                \"System\",\n                \"$username left\",\n                System.currentTimeMillis()\n            )\n        )\n    }\n\n    fun sendMessage(username: String, text: String) {\n        _state.value = _state.value.copy(\n            messages = _state.value.messages + Message(\n                username,\n                text,\n                System.currentTimeMillis()\n            )\n        )\n    }\n}\n\nfun main() = runBlocking {\n    val chatRoom = ChatRoom()\n\n    // Observer 1\n    launch {\n        chatRoom.state\n            .map { it.users.size }\n            .distinctUntilChanged()\n            .collect { count -\u003e\n                println(\"👥 Users online: $count\")\n            }\n    }\n\n    // Observer 2\n    launch {\n        chatRoom.state\n            .map { it.messages.lastOrNull() }\n            .filterNotNull()\n            .collect { msg -\u003e\n                println(\"💬 [${msg.user}]: ${msg.text}\")\n            }\n    }\n\n    delay(100)\n\n    chatRoom.userJoin(\"Alice\")\n    delay(100)\n    chatRoom.userJoin(\"Bob\")\n    delay(100)\n    chatRoom.sendMessage(\"Alice\", \"Hello, Bob!\")\n    delay(100)\n    chatRoom.sendMessage(\"Bob\", \"Hi, Alice!\")\n    delay(100)\n    chatRoom.userLeave(\"Alice\")\n\n    delay(500)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Checkpoint Quiz",
                                "content":  "\n### Question 1: Structured Concurrency\n\nWhat happens in `coroutineScope` if one child fails?\n\n**A)** Only that child is cancelled\n**B)** All children are cancelled and exception is propagated\n**C)** The exception is ignored\n**D)** Other children continue running\n\n**Answer**: **B** - In `coroutineScope`, if one child fails, all siblings are cancelled and the exception is propagated to the parent.\n\n---\n\n### Question 2: Flow vs Channel\n\nWhat\u0027s the main difference between Flow and Channel?\n\n**A)** Flow is hot, Channel is cold\n**B)** Flow is cold (lazy), Channel is hot (active)\n**C)** They are the same\n**D)** Channel can\u0027t be cancelled\n\n**Answer**: **B** - Flow is cold (starts on collection), while Channel is hot (actively sends/receives regardless of consumers).\n\n---\n\n### Question 3: StateFlow\n\nWhat makes StateFlow special?\n\n**A)** It\u0027s the fastest flow type\n**B)** It always has a current value and conflates duplicates\n**C)** It can only emit once\n**D)** It doesn\u0027t support multiple collectors\n\n**Answer**: **B** - StateFlow always has a current value (accessible via `.value`) and automatically conflates duplicate consecutive values.\n\n---\n\n### Question 4: Exception Handling\n\nWhy doesn\u0027t this catch the exception?\n\n\n**A)** launch is not a suspend function\n**B)** launch is fire-and-forget, exception happens async\n**C)** Exception handling doesn\u0027t work in coroutines\n**D)** Missing await()\n\n**Answer**: **B** - `launch` returns immediately (fire-and-forget), so the exception happens asynchronously after the try-catch block.\n\n---\n\n### Question 5: flowOn\n\nWhat does `flowOn` do?\n\n**A)** Changes the dispatcher for downstream operators\n**B)** Changes the dispatcher for upstream operators\n**C)** Stops the flow\n**D)** Buffers the flow\n\n**Answer**: **B** - `flowOn` changes the dispatcher for upstream operators (everything before it in the chain).\n\n---\n\n",
                                "code":  "try {\n    launch {\n        throw Exception(\"Error\")\n    }\n} catch (e: Exception) {\n    println(\"Caught\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Summary",
                                "content":  "\nCongratulations! You\u0027ve mastered advanced coroutines. Here\u0027s what you learned:\n\n✅ **Structured Concurrency** - `coroutineScope` and `supervisorScope`\n✅ **Exception Handling** - Try-catch patterns and exception handlers\n✅ **Flows** - Reactive streams with operators and transformations\n✅ **Channels** - Communication between coroutines\n✅ **StateFlow/SharedFlow** - State management and event broadcasting\n✅ **Context Switching** - `withContext` for dispatcher changes\n\n### Key Takeaways\n\n1. **Use `coroutineScope`** for related tasks that should fail together\n2. **Use `supervisorScope`** for independent tasks\n3. **Flows are cold** (start on collection), **Channels are hot**\n4. **StateFlow** for state, **SharedFlow** for events\n5. **Exception handling** in `launch` requires `CoroutineExceptionHandler`\n6. **`flowOn`** changes dispatcher for upstream operators\n\n### Next Steps\n\nIn the next lesson, we\u0027ll explore **Delegation and Lazy Initialization** - powerful patterns for delegating behavior and optimizing resource usage!\n\n---\n\n**Practice Challenge**: Build a stock price monitoring system that fetches prices from multiple sources using Flows, combines them, and alerts when prices cross thresholds using StateFlow.\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "intermediate",
    "title":  "Lesson 4.9: Advanced Coroutines",
    "estimatedMinutes":  75
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current kotlin documentation
- Search the web for the latest kotlin version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "kotlin Lesson 4.9: Advanced Coroutines 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "4.9",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

