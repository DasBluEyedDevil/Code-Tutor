# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Advanced Kotlin
- **Lesson:** Lesson 4.13: Part 4 Capstone - Task Scheduler with Coroutines (ID: 4.13)
- **Difficulty:** intermediate
- **Estimated Time:** 30 minutes

## Current Lesson Content

{
    "id":  "4.13",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 4-5 hours\n**Difficulty**: Advanced\n**Prerequisites**: All Part 4 lessons\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Project Overview",
                                "content":  "\nCongratulations on completing all the lessons in Part 4! You\u0027ve learned the most advanced features of Kotlin:\n\n- ✅ Generics and type parameters\n- ✅ Coroutines fundamentals\n- ✅ Advanced coroutines (Flows, Channels, StateFlow)\n- ✅ Delegation and lazy initialization\n- ✅ Annotations and reflection\n- ✅ DSLs and type-safe builders\n\nNow it\u0027s time to put it all together in a **comprehensive capstone project**: a **Task Scheduler with Coroutines**.\n\nThis project will challenge you to apply all advanced concepts in a real-world scenario where you build a sophisticated task scheduling system with async execution, monitoring, and configuration.\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "The Project: TaskFlow",
                                "content":  "\n**TaskFlow** is a complete task scheduling and execution system that allows:\n- Generic task definitions with type-safe results\n- Coroutine-based async execution\n- Task dependencies and workflows\n- Progress monitoring with StateFlow\n- Custom property delegates for task configuration\n- Reflection-based task discovery and execution\n- DSL for task and workflow configuration\n- Scheduled and recurring tasks\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Requirements",
                                "content":  "\n### 1. Generic Task System\n\n**Generic Task Interface**:\n- Type parameter for result type\n- Async execution with suspend functions\n- Task metadata (name, priority, retries)\n- Result handling (Success, Failure, Cancelled)\n\n**Task Types**:\n- `SimpleTask\u003cT\u003e` - single operation\n- `WorkflowTask\u003cT\u003e` - composite of multiple tasks\n- `ScheduledTask\u003cT\u003e` - runs at specific times\n- `RecurringTask\u003cT\u003e` - runs periodically\n\n### 2. Coroutine-Based Execution\n\n**Task Executor**:\n- Concurrent task execution\n- Dispatcher management\n- Cancellation support\n- Retry logic with exponential backoff\n- Timeout handling\n\n**Progress Monitoring**:\n- StateFlow for task status\n- SharedFlow for events\n- Real-time progress updates\n\n### 3. Custom Delegates\n\n**Task Properties**:\n- Lazy resource initialization\n- Observable task state\n- Validated configuration\n- Cached results\n\n### 4. Reflection-Based Discovery\n\n**Task Registry**:\n- Discover tasks annotated with `@Task`\n- Auto-register tasks\n- Inspect task metadata\n- Dynamic task instantiation\n\n### 5. Configuration DSL\n\n**Type-Safe Builder**:\n- Task definition DSL\n- Workflow composition\n- Scheduler configuration\n- Execution policies\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Phase 1: Core Task System (60 minutes)",
                                "content":  "\nLet\u0027s start by building the core task system with generics.\n\n### Task Result Types\n\n\n### Task Metadata\n\n\n### Base Task Interface\n\n\n### Simple Task Implementation\n\n\nWait, let me fix this implementation:\n\n\n---\n\n",
                                "code":  "abstract class SimpleTask\u003cT\u003e(override val metadata: TaskMetadata) : Task\u003cT\u003e {\n    private val _status = MutableStateFlow(TaskStatus.PENDING)\n    override val status: StateFlow\u003cTaskStatus\u003e = _status\n\n    private var job: Job? = null\n\n    protected abstract suspend fun run(): T\n\n    override suspend fun execute(): TaskResult\u003cT\u003e {\n        _status.value = TaskStatus.RUNNING\n\n        return try {\n            val result = if (metadata.timeout \u003e 0) {\n                withTimeout(metadata.timeout) { run() }\n            } else {\n                run()\n            }\n\n            _status.value = TaskStatus.COMPLETED\n            TaskResult.Success(result)\n        } catch (e: CancellationException) {\n            _status.value = TaskStatus.CANCELLED\n            TaskResult.Cancelled\n        } catch (e: Exception) {\n            _status.value = TaskStatus.FAILED\n            TaskResult.Failure(e)\n        }\n    }\n\n    override fun cancel() {\n        job?.cancel()\n        _status.value = TaskStatus.CANCELLED\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Phase 2: Task Executor with Coroutines (60 minutes)",
                                "content":  "\n### Task Executor\n\n\n---\n\n",
                                "code":  "import kotlinx.coroutines.*\nimport kotlinx.coroutines.flow.*\n\nclass TaskExecutor(\n    private val dispatcher: CoroutineDispatcher = Dispatchers.Default,\n    private val maxConcurrentTasks: Int = 4\n) {\n    private val scope = CoroutineScope(dispatcher + SupervisorJob())\n    private val _events = MutableSharedFlow\u003cTaskEvent\u003e()\n    val events: SharedFlow\u003cTaskEvent\u003e = _events\n\n    private val activeTasks = MutableStateFlow(0)\n\n    suspend fun \u003cT\u003e execute(task: Task\u003cT\u003e): TaskResult\u003cT\u003e {\n        return withContext(dispatcher) {\n            // Wait if max concurrent tasks reached\n            while (activeTasks.value \u003e= maxConcurrentTasks) {\n                delay(100)\n            }\n\n            activeTasks.value++\n            _events.emit(TaskEvent.Started(task.metadata.name))\n\n            try {\n                val result = executeWithRetry(task)\n\n                when (result) {\n                    is TaskResult.Success -\u003e _events.emit(TaskEvent.Completed(task.metadata.name))\n                    is TaskResult.Failure -\u003e _events.emit(TaskEvent.Failed(task.metadata.name, result.error))\n                    is TaskResult.Cancelled -\u003e _events.emit(TaskEvent.Cancelled(task.metadata.name))\n                }\n\n                result\n            } finally {\n                activeTasks.value--\n            }\n        }\n    }\n\n    private suspend fun \u003cT\u003e executeWithRetry(task: Task\u003cT\u003e): TaskResult\u003cT\u003e {\n        var lastError: Throwable? = null\n        var attempt = 0\n        val maxAttempts = task.metadata.retries + 1\n\n        while (attempt \u003c maxAttempts) {\n            val result = task.execute()\n\n            when (result) {\n                is TaskResult.Success -\u003e return result\n                is TaskResult.Cancelled -\u003e return result\n                is TaskResult.Failure -\u003e {\n                    lastError = result.error\n                    attempt++\n\n                    if (attempt \u003c maxAttempts) {\n                        val delayMs = (100 * (1 shl attempt)).toLong()\n                        _events.emit(TaskEvent.Retrying(task.metadata.name, attempt, delayMs))\n                        delay(delayMs)\n                    }\n                }\n            }\n        }\n\n        return TaskResult.Failure(lastError ?: Exception(\"Unknown error\"))\n    }\n\n    fun shutdown() {\n        scope.cancel()\n    }\n}\n\nsealed class TaskEvent {\n    data class Started(val taskName: String) : TaskEvent()\n    data class Completed(val taskName: String) : TaskEvent()\n    data class Failed(val taskName: String, val error: Throwable) : TaskEvent()\n    data class Cancelled(val taskName: String) : TaskEvent()\n    data class Retrying(val taskName: String, val attempt: Int, val delayMs: Long) : TaskEvent()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Phase 3: Delegation Patterns (45 minutes)",
                                "content":  "\n### Lazy Task Resource\n\n\n### Observable Task State\n\n\n### Validated Configuration\n\n\n---\n\n",
                                "code":  "class ValidatedProperty\u003cT\u003e(\n    private var value: T,\n    private val validator: (T) -\u003e Boolean,\n    private val errorMessage: (T) -\u003e String\n) {\n    operator fun getValue(thisRef: Any?, property: KProperty\u003c*\u003e): T {\n        return value\n    }\n\n    operator fun setValue(thisRef: Any?, property: KProperty\u003c*\u003e, newValue: T) {\n        if (!validator(newValue)) {\n            throw IllegalArgumentException(errorMessage(newValue))\n        }\n        value = newValue\n    }\n}\n\nfun \u003cT\u003e validated(\n    initialValue: T,\n    validator: (T) -\u003e Boolean,\n    errorMessage: (T) -\u003e String = { \"Invalid value: $it\" }\n) = ValidatedProperty(initialValue, validator, errorMessage)",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Phase 4: Annotations and Reflection (45 minutes)",
                                "content":  "\n### Task Annotations\n\n\n### Task Registry with Reflection\n\n\n---\n\n",
                                "code":  "import kotlin.reflect.KClass\nimport kotlin.reflect.full.*\n\nobject TaskRegistry {\n    private val tasks = mutableMapOf\u003cString, KClass\u003cout Task\u003c*\u003e\u003e\u003e()\n\n    fun register(taskClass: KClass\u003cout Task\u003c*\u003e\u003e) {\n        val annotation = taskClass.annotations.filterIsInstance\u003cRegisteredTask\u003e().firstOrNull()\n            ?: throw IllegalArgumentException(\"Task must be annotated with @RegisteredTask\")\n\n        tasks[annotation.name] = taskClass\n    }\n\n    fun \u003cT\u003e create(name: String): Task\u003cT\u003e? {\n        val taskClass = tasks[name] ?: return null\n\n        // Find primary constructor\n        val constructor = taskClass.constructors.firstOrNull() ?: return null\n\n        // Create metadata from annotation\n        val annotation = taskClass.annotations.filterIsInstance\u003cRegisteredTask\u003e().first()\n        val metadata = TaskMetadata(\n            name = annotation.name,\n            priority = annotation.priority,\n            retries = annotation.retries\n        )\n\n        // Call constructor with metadata\n        val instance = if (constructor.parameters.isEmpty()) {\n            constructor.call()\n        } else {\n            constructor.call(metadata)\n        }\n\n        @Suppress(\"UNCHECKED_CAST\")\n        return instance as? Task\u003cT\u003e\n    }\n\n    fun listTasks(): List\u003cString\u003e = tasks.keys.toList()\n\n    fun getTaskInfo(name: String): TaskMetadata? {\n        val taskClass = tasks[name] ?: return null\n        val annotation = taskClass.annotations.filterIsInstance\u003cRegisteredTask\u003e().first()\n\n        return TaskMetadata(\n            name = annotation.name,\n            priority = annotation.priority,\n            retries = annotation.retries\n        )\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Phase 5: DSL Configuration (60 minutes)",
                                "content":  "\n### Task DSL\n\n\n### Workflow DSL\n\n\n---\n\n",
                                "code":  "@TaskFlowDsl\nclass WorkflowBuilder\u003cT\u003e {\n    var name: String = \"\"\n    var description: String = \"\"\n\n    private val tasks = mutableListOf\u003cTask\u003c*\u003e\u003e()\n    private var finalTask: (suspend (List\u003cAny?\u003e) -\u003e T)? = null\n\n    fun \u003cR\u003e task(name: String, action: suspend () -\u003e R) {\n        val task = task\u003cR\u003e {\n            this.name = name\n            action(action)\n        }\n        tasks.add(task)\n    }\n\n    fun finalize(action: suspend (List\u003cAny?\u003e) -\u003e T) {\n        finalTask = action\n    }\n\n    fun build(): WorkflowTask\u003cT\u003e {\n        val metadata = TaskMetadata(name, description)\n        return WorkflowTask(metadata, tasks, finalTask!!)\n    }\n}\n\nclass WorkflowTask\u003cT\u003e(\n    override val metadata: TaskMetadata,\n    private val tasks: List\u003cTask\u003c*\u003e\u003e,\n    private val finalizer: suspend (List\u003cAny?\u003e) -\u003e T\n) : Task\u003cT\u003e {\n    private val _status = MutableStateFlow(TaskStatus.PENDING)\n    override val status: StateFlow\u003cTaskStatus\u003e = _status\n\n    override suspend fun execute(): TaskResult\u003cT\u003e {\n        _status.value = TaskStatus.RUNNING\n\n        return try {\n            val results = tasks.map { task -\u003e\n                when (val result = task.execute()) {\n                    is TaskResult.Success -\u003e result.value\n                    is TaskResult.Failure -\u003e throw result.error\n                    is TaskResult.Cancelled -\u003e throw CancellationException(\"Subtask cancelled\")\n                }\n            }\n\n            val finalResult = finalizer(results)\n            _status.value = TaskStatus.COMPLETED\n            TaskResult.Success(finalResult)\n        } catch (e: CancellationException) {\n            _status.value = TaskStatus.CANCELLED\n            TaskResult.Cancelled\n        } catch (e: Exception) {\n            _status.value = TaskStatus.FAILED\n            TaskResult.Failure(e)\n        }\n    }\n\n    override fun cancel() {\n        tasks.forEach { it.cancel() }\n        _status.value = TaskStatus.CANCELLED\n    }\n}\n\nfun \u003cT\u003e workflow(block: WorkflowBuilder\u003cT\u003e.() -\u003e Unit): WorkflowTask\u003cT\u003e {\n    return WorkflowBuilder\u003cT\u003e().apply(block).build()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Complete Solution: TaskFlow System",
                                "content":  "\nHere\u0027s the complete integrated solution:\n\n\n---\n\n",
                                "code":  "import kotlinx.coroutines.*\nimport kotlinx.coroutines.flow.*\nimport kotlin.reflect.KClass\nimport kotlin.reflect.full.*\n\n// ========== Core Types ==========\n\nsealed class TaskResult\u003cout T\u003e {\n    data class Success\u003cT\u003e(val value: T) : TaskResult\u003cT\u003e()\n    data class Failure(val error: Throwable) : TaskResult\u003cNothing\u003e()\n    object Cancelled : TaskResult\u003cNothing\u003e()\n\n    fun \u003cR\u003e map(transform: (T) -\u003e R): TaskResult\u003cR\u003e = when (this) {\n        is Success -\u003e Success(transform(value))\n        is Failure -\u003e this\n        is Cancelled -\u003e this\n    }\n\n    fun getOrNull(): T? = (this as? Success)?.value\n}\n\ndata class TaskMetadata(\n    val name: String,\n    val description: String = \"\",\n    val priority: TaskPriority = TaskPriority.NORMAL,\n    val retries: Int = 0,\n    val timeout: Long = 0\n)\n\nenum class TaskPriority { LOW, NORMAL, HIGH, CRITICAL }\nenum class TaskStatus { PENDING, RUNNING, COMPLETED, FAILED, CANCELLED }\n\n// ========== Task Interface ==========\n\ninterface Task\u003cT\u003e {\n    val metadata: TaskMetadata\n    val status: StateFlow\u003cTaskStatus\u003e\n    suspend fun execute(): TaskResult\u003cT\u003e\n    fun cancel()\n}\n\n// ========== Example Tasks ==========\n\n@RegisteredTask(name = \"DataFetch\", priority = TaskPriority.HIGH, retries = 3)\nclass DataFetchTask(override val metadata: TaskMetadata) : SimpleTask\u003cString\u003e(metadata) {\n    override suspend fun run(): String {\n        delay(1000)\n        return \"Fetched data at ${System.currentTimeMillis()}\"\n    }\n}\n\n@RegisteredTask(name = \"DataProcess\", priority = TaskPriority.NORMAL, retries = 2)\nclass DataProcessTask(override val metadata: TaskMetadata) : SimpleTask\u003cString\u003e(metadata) {\n    override suspend fun run(): String {\n        delay(500)\n        return \"Processed data\"\n    }\n}\n\n// ========== Main Demo ==========\n\nfun main() = runBlocking {\n    println(\"=== TaskFlow Demo ===\\n\")\n\n    // 1. Simple Task with DSL\n    println(\"1. Creating task with DSL:\")\n    val simpleTask = task\u003cString\u003e {\n        name = \"GreetingTask\"\n        description = \"Generates a greeting\"\n        timeout = 5000\n\n        action {\n            delay(500)\n            \"Hello from TaskFlow!\"\n        }\n    }\n\n    val result1 = simpleTask.execute()\n    println(\"Result: ${result1.getOrNull()}\\n\")\n\n    // 2. Workflow Task\n    println(\"2. Creating workflow:\")\n    val workflowTask = workflow\u003cString\u003e {\n        name = \"DataPipeline\"\n        description = \"Fetch and process data\"\n\n        task(\"fetch\") {\n            delay(1000)\n            \"Raw Data\"\n        }\n\n        task(\"transform\") {\n            delay(500)\n            \"Transformed\"\n        }\n\n        finalize { results -\u003e\n            \"Pipeline completed: $results\"\n        }\n    }\n\n    val result2 = workflowTask.execute()\n    println(\"Workflow result: ${result2.getOrNull()}\\n\")\n\n    // 3. Task Executor with monitoring\n    println(\"3. Task Executor with monitoring:\")\n    val executor = TaskExecutor(maxConcurrentTasks = 2)\n\n    launch {\n        executor.events.collect { event -\u003e\n            when (event) {\n                is TaskEvent.Started -\u003e println(\"  ▶ Started: ${event.taskName}\")\n                is TaskEvent.Completed -\u003e println(\"  ✅ Completed: ${event.taskName}\")\n                is TaskEvent.Failed -\u003e println(\"  ❌ Failed: ${event.taskName}\")\n                is TaskEvent.Retrying -\u003e println(\"  🔄 Retrying: ${event.taskName} (attempt ${event.attempt})\")\n                is TaskEvent.Cancelled -\u003e println(\"  ⛔ Cancelled: ${event.taskName}\")\n            }\n        }\n    }\n\n    val tasks = (1..5).map { i -\u003e\n        task\u003cInt\u003e {\n            name = \"Task-$i\"\n            retries = 2\n            action {\n                delay((500..1500).random().toLong())\n                if (i == 3) throw Exception(\"Simulated failure\")\n                i * 10\n            }\n        }\n    }\n\n    val results = tasks.map { async { executor.execute(it) } }.awaitAll()\n\n    println(\"\\nResults:\")\n    results.forEach { result -\u003e\n        println(\"  ${result.getOrNull() ?: \"Failed\"}\")\n    }\n\n    // 4. Task Registry with Reflection\n    println(\"\\n4. Task Registry:\")\n    TaskRegistry.register(DataFetchTask::class)\n    TaskRegistry.register(DataProcessTask::class)\n\n    println(\"Registered tasks: ${TaskRegistry.listTasks()}\")\n\n    val fetchTask = TaskRegistry.create\u003cString\u003e(\"DataFetch\")\n    if (fetchTask != null) {\n        val result = executor.execute(fetchTask)\n        println(\"Registry task result: ${result.getOrNull()}\")\n    }\n\n    delay(1000)\n    executor.shutdown()\n\n    println(\"\\n=== Demo Complete ===\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Extension Challenges",
                                "content":  "\nReady for more? Try these advanced challenges:\n\n### Challenge 1: Dependency Management\n\nAdd task dependencies so tasks only run after their dependencies complete:\n\n\n### Challenge 2: Task Scheduler\n\nImplement scheduled and recurring tasks:\n\n\n### Challenge 3: Persistence\n\nSave and restore task state:\n\n\n### Challenge 4: Priority Queue\n\nImplement priority-based task execution:\n\n\n### Challenge 5: Error Recovery\n\nAdd sophisticated error recovery strategies:\n\n\n---\n\n",
                                "code":  "sealed class RecoveryStrategy {\n    object Retry : RecoveryStrategy()\n    data class Fallback(val alternativeTask: Task\u003c*\u003e) : RecoveryStrategy()\n    data class Circuit(val threshold: Int, val resetTime: Duration) : RecoveryStrategy()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Testing Your Implementation",
                                "content":  "\n\n---\n\n",
                                "code":  "import kotlinx.coroutines.test.*\nimport kotlin.test.*\n\nclass TaskFlowTests {\n    @Test\n    fun testSimpleTaskSuccess() = runTest {\n        val task = task\u003cInt\u003e {\n            name = \"Test\"\n            action { 42 }\n        }\n\n        val result = task.execute()\n        assertTrue(result is TaskResult.Success)\n        assertEquals(42, result.getOrNull())\n    }\n\n    @Test\n    fun testTaskRetry() = runTest {\n        var attempts = 0\n        val task = task\u003cInt\u003e {\n            name = \"RetryTest\"\n            retries = 2\n            action {\n                attempts++\n                if (attempts \u003c 3) throw Exception(\"Fail\")\n                42\n            }\n        }\n\n        val executor = TaskExecutor()\n        val result = executor.execute(task)\n\n        assertEquals(3, attempts)\n        assertTrue(result is TaskResult.Success)\n    }\n\n    @Test\n    fun testWorkflow() = runTest {\n        val workflow = workflow\u003cInt\u003e {\n            name = \"TestWorkflow\"\n\n            task(\"step1\") { 10 }\n            task(\"step2\") { 20 }\n\n            finalize { results -\u003e\n                (results[0] as Int) + (results[1] as Int)\n            }\n        }\n\n        val result = workflow.execute()\n        assertEquals(30, result.getOrNull())\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What You\u0027ve Built",
                                "content":  "\nCongratulations! You\u0027ve built a production-quality task scheduling system that demonstrates:\n\n✅ **Generics** - Type-safe task system with generic results\n✅ **Coroutines** - Async task execution with proper concurrency\n✅ **Flows** - Real-time status monitoring and events\n✅ **Delegation** - Lazy resources, observable state, validated config\n✅ **Reflection** - Dynamic task discovery and registration\n✅ **DSLs** - Beautiful, type-safe configuration API\n✅ **Error Handling** - Retry logic, timeouts, cancellation\n✅ **Structured Concurrency** - Proper lifecycle management\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Summary",
                                "content":  "\nYou\u0027ve completed Part 4: Advanced Kotlin Features! Here\u0027s everything you learned:\n\n### Lesson 4.1: Generics\n- Generic classes and functions\n- Type constraints and variance\n- Reified type parameters\n\n### Lesson 4.2: Coroutines Fundamentals\n- Suspend functions\n- launch, async, runBlocking\n- Scopes and contexts\n\n### Lesson 4.3: Advanced Coroutines\n- Structured concurrency\n- Flows and Channels\n- StateFlow and SharedFlow\n\n### Lesson 4.4: Delegation\n- Class delegation\n- Property delegation\n- Lazy initialization\n\n### Lesson 4.5: Annotations and Reflection\n- Custom annotations\n- Runtime reflection\n- Metadata inspection\n\n### Lesson 4.6: DSLs\n- Lambda with receiver\n- Type-safe builders\n- @DslMarker\n\n### Lesson 4.7: Capstone Project\n- Real-world integration\n- Production patterns\n- Advanced architectures\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Next Steps",
                                "content":  "\nYou\u0027re now ready for **Part 5: Backend Development with Ktor**! You\u0027ll learn to:\n- Build RESTful APIs\n- Handle HTTP requests and responses\n- Implement authentication and authorization\n- Work with databases\n- Deploy production applications\n\nKeep this capstone project as a reference—many patterns you built here apply to backend development!\n\n---\n\n**Final Challenge**: Extend TaskFlow with a web dashboard using Ktor. Create REST endpoints to submit tasks, monitor progress, view history, and manage the scheduler. Combine everything you\u0027ve learned in Parts 1-5!\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "intermediate",
    "title":  "Lesson 4.13: Part 4 Capstone - Task Scheduler with Coroutines",
    "estimatedMinutes":  30
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
- Search for "kotlin Lesson 4.13: Part 4 Capstone - Task Scheduler with Coroutines 2024 2025" to find latest practices
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
  "lessonId": "4.13",
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

