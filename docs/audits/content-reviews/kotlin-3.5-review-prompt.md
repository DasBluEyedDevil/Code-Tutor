# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Object-Oriented Programming
- **Lesson:** Lesson 2.5: Data Classes and Sealed Classes (ID: 3.5)
- **Difficulty:** beginner
- **Estimated Time:** 65 minutes

## Current Lesson Content

{
    "id":  "3.5",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 65 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Topic Introduction",
                                "content":  "\nKotlin provides special class types that solve common programming patterns elegantly. You\u0027ve learned about regular classes, abstract classes, and interfaces. Now let\u0027s explore two powerful Kotlin features:\n\n**Data Classes**: Classes designed to hold data with automatic implementations of `equals()`, `hashCode()`, `toString()`, and `copy()`.\n\n**Sealed Classes**: Classes with a restricted hierarchy where all subclasses are known at compile-time, perfect for representing state or result types.\n\nThese features make Kotlin code more concise, safer, and more expressive than traditional OOP languages.\n\n---\n\n"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept",
                                "content":  "\n### Why Special Class Types?\n\n**Problem with Regular Classes**:\n\n\n**Solution with Data Classes**:\n\n\n---\n\n",
                                "code":  "data class User(val name: String, val age: Int)\n\nval user1 = User(\"Alice\", 25)\nval user2 = User(\"Alice\", 25)\n\nprintln(user1 == user2)  // true (compares data!)\nprintln(user1)           // User(name=Alice, age=25) (readable!)",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Data Classes",
                                "content":  "\n### Creating Data Classes\n\nUse the `data` keyword before `class`:\n\n\n**What Kotlin generates automatically**:\n1. **`equals()`** - Compares data, not references\n2. **`hashCode()`** - Consistent with `equals()`\n3. **`toString()`** - Readable string representation\n4. **`copy()`** - Creates copies with modified properties\n5. **`componentN()`** - Destructuring declarations\n\n### Requirements for Data Classes\n\n1. Primary constructor must have at least one parameter\n2. All primary constructor parameters must be `val` or `var`\n3. Cannot be `abstract`, `open`, `sealed`, or `inner`\n4. May extend other classes or implement interfaces\n\n### Auto-Generated Functions\n\n**1. `toString()`** - Readable representation\n\n\n**2. `equals()` and `hashCode()`** - Structural equality\n\n\n**3. `copy()`** - Create modified copies\n\n\n**Why `copy()` matters**:\n- Immutability: Don\u0027t modify original, create new versions\n- Thread safety: Immutable data is inherently thread-safe\n- Functional programming: Transform data without side effects\n\n---\n\n",
                                "code":  "data class User(val name: String, val age: Int, val email: String)\n\nval user = User(\"Alice\", 25, \"alice@example.com\")\n\n// Create a copy with modified age\nval olderUser = user.copy(age = 26)\n\nprintln(user)       // User(name=Alice, age=25, email=alice@example.com)\nprintln(olderUser)  // User(name=Alice, age=26, email=alice@example.com)\n\n// Copy with multiple changes\nval differentUser = user.copy(name = \"Bob\", age = 30)\nprintln(differentUser)  // User(name=Bob, age=30, email=alice@example.com)",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Destructuring Declarations",
                                "content":  "\nData classes support **destructuring** - extracting multiple values at once:\n\n\n**How it works**: Kotlin generates `component1()`, `component2()`, etc. functions:\n\n\n**Partial Destructuring**:\n\n\n**Destructuring in Loops**:\n\n\n---\n\n",
                                "code":  "data class Person(val name: String, val age: Int)\n\nval people = listOf(\n    Person(\"Alice\", 25),\n    Person(\"Bob\", 30),\n    Person(\"Carol\", 22)\n)\n\nfor ((name, age) in people) {\n    println(\"$name is $age years old\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Real-World Data Class Examples",
                                "content":  "\n### Example 1: API Response\n\n\n### Example 2: Coordinates and Geometry\n\n\n---\n\n",
                                "code":  "data class Point(val x: Double, val y: Double) {\n    fun distanceTo(other: Point): Double {\n        val dx = x - other.x\n        val dy = y - other.y\n        return kotlin.math.sqrt(dx * dx + dy * dy)\n    }\n}\n\ndata class Rectangle(val topLeft: Point, val bottomRight: Point) {\n    val width: Double\n        get() = bottomRight.x - topLeft.x\n\n    val height: Double\n        get() = bottomRight.y - topLeft.y\n\n    val area: Double\n        get() = width * height\n}\n\nfun main() {\n    val p1 = Point(0.0, 0.0)\n    val p2 = Point(3.0, 4.0)\n\n    println(\"Distance: ${p1.distanceTo(p2)}\")  // 5.0\n\n    val rect = Rectangle(Point(0.0, 10.0), Point(5.0, 0.0))\n    println(\"Area: ${rect.area}\")  // 50.0\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Sealed Classes",
                                "content":  "\n**Sealed classes** represent restricted class hierarchies where all subclasses are known at compile-time.\n\n### Why Sealed Classes?\n\n**Problem**: Modeling states or results with regular classes\n\n\n**Solution**: Sealed classes\n\n\n### Defining Sealed Classes\n\n\n**Key Points**:\n- Subclasses must be defined in the same file (or as nested classes)\n- Cannot be instantiated directly\n- Perfect for `when` expressions (exhaustive checking)\n\n---\n\n",
                                "code":  "sealed class NetworkResult {\n    data class Success(val data: String) : NetworkResult()\n    data class Error(val code: Int, val message: String) : NetworkResult()\n    object Loading : NetworkResult()\n    object Idle : NetworkResult()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Sealed Classes for State Management",
                                "content":  "\n\n---\n\n",
                                "code":  "sealed class UiState {\n    object Loading : UiState()\n    data class Success(val items: List\u003cString\u003e) : UiState()\n    data class Error(val message: String) : UiState()\n    object Empty : UiState()\n}\n\nclass ViewModel {\n    private var state: UiState = UiState.Loading\n\n    fun loadData() {\n        state = UiState.Loading\n        displayState()\n\n        // Simulate loading\n        Thread.sleep(1000)\n\n        val items = listOf(\"Item 1\", \"Item 2\", \"Item 3\")\n        state = if (items.isNotEmpty()) {\n            UiState.Success(items)\n        } else {\n            UiState.Empty\n        }\n        displayState()\n    }\n\n    fun displayState() {\n        when (state) {\n            is UiState.Loading -\u003e println(\"⏳ Loading...\")\n            is UiState.Success -\u003e {\n                val items = (state as UiState.Success).items\n                println(\"✅ Loaded ${items.size} items: $items\")\n            }\n            is UiState.Error -\u003e {\n                val message = (state as UiState.Error).message\n                println(\"❌ Error: $message\")\n            }\n            UiState.Empty -\u003e println(\"📭 No items found\")\n        }\n    }\n}\n\nfun main() {\n    val viewModel = ViewModel()\n    viewModel.loadData()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Enum Classes",
                                "content":  "\n**Enum classes** define a fixed set of constants.\n\n\n**Enum vs Sealed Class**:\n\n| Feature | Enum | Sealed Class |\n|---------|------|--------------|\n| Fixed set of instances | ✅ Yes (all at compile-time) | ✅ Yes (types known at compile-time) |\n| Can have different data | ❌ No (same structure) | ✅ Yes (different properties) |\n| Can inherit | ❌ No | ✅ Yes |\n| When to use | Finite set of constants | Type hierarchies with different data |\n\n---\n\n",
                                "code":  "enum class Direction {\n    NORTH, SOUTH, EAST, WEST\n}\n\nenum class Priority(val level: Int) {\n    LOW(1),\n    MEDIUM(2),\n    HIGH(3),\n    CRITICAL(4);\n\n    fun isUrgent() = level \u003e= 3\n}\n\nfun main() {\n    val direction = Direction.NORTH\n    println(direction)  // NORTH\n\n    val priority = Priority.HIGH\n    println(\"Level: ${priority.level}\")  // Level: 3\n    println(\"Urgent: ${priority.isUrgent()}\")  // Urgent: true\n\n    // Iterate over all values\n    Priority.values().forEach { p -\u003e\n        println(\"${p.name}: Level ${p.level}\")\n    }\n\n    // String to enum\n    val p = Priority.valueOf(\"MEDIUM\")\n    println(p.level)  // 2\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Value Classes (Inline Classes)",
                                "content":  "\n**Value classes** provide type safety without runtime overhead.\n\n\n**Benefits**:\n- Type safety: Can\u0027t accidentally pass wrong type\n- Zero runtime overhead: Unwrapped at runtime\n- Validation in init block\n\n---\n\n",
                                "code":  "@JvmInline\nvalue class UserId(val value: Int)\n\n@JvmInline\nvalue class Email(val value: String) {\n    init {\n        require(value.contains(\"@\")) { \"Invalid email\" }\n    }\n}\n\nfun sendEmail(email: Email) {\n    println(\"Sending email to ${email.value}\")\n}\n\nfun main() {\n    val userId = UserId(123)\n    val email = Email(\"alice@example.com\")\n\n    // sendEmail(UserId(456))  // ❌ Type mismatch!\n    sendEmail(email)  // ✅ Correct type\n\n    // At runtime, email is just a String (no wrapper object)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Value Classes for Domain Modeling",
                                "content":  "\n### Type-Safe IDs Prevent Mixing Entity IDs\n\nOne of the most powerful uses of value classes is creating type-safe IDs:\n\n\n### Validated Primitives\n\nValue classes can validate data at construction time:\n\n\n**Key Benefits**:\n- **Zero runtime overhead**: Compiled to primitives\n- **Type safety**: Compiler prevents mixing different ID types\n- **Validation**: Business rules enforced at creation\n- **Self-documenting**: Code expresses intent clearly\n\n---\n\n",
                                "code":  "// Type-safe IDs prevent mixing different entity IDs\n@JvmInline\nvalue class UserId(val value: Long)\n\n@JvmInline\nvalue class OrderId(val value: Long)\n\n@JvmInline\nvalue class ProductId(val value: Long)\n\n// Compiler prevents mistakes!\nfun getOrder(orderId: OrderId): Order { /* ... */ }\n\nval userId = UserId(123)\n// getOrder(userId)  // Compile error: Type mismatch!\n//                   // Required: OrderId, Found: UserId\n\nval orderId = OrderId(456)\ngetOrder(orderId)  // Works correctly\n\n// Validated primitives with business rules\n@JvmInline\nvalue class Email(val address: String) {\n    init {\n        require(address.contains(\"@\") \u0026\u0026 address.contains(\".\")) {\n            \"Invalid email format: $address\"\n        }\n    }\n}\n\n@JvmInline\nvalue class PositiveInt(val value: Int) {\n    init {\n        require(value \u003e 0) { \"Value must be positive, got: $value\" }\n    }\n}\n\n@JvmInline\nvalue class Percentage(val value: Double) {\n    init {\n        require(value in 0.0..100.0) { \"Percentage must be 0-100, got: $value\" }\n    }\n}\n\n// Usage - validation happens at construction\nval email = Email(\"user@example.com\")  // OK\n// val badEmail = Email(\"invalid\")     // Throws IllegalArgumentException\n\nval discount = Percentage(15.0)  // OK\n// val badDiscount = Percentage(150.0)  // Throws IllegalArgumentException",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise: Create Value Classes for a Banking Domain",
                                "content":  "\n**Goal**: Create type-safe value classes for a banking application.\n\n**Requirements**:\n1. Create `AccountNumber` value class (8-digit string, must start with digit)\n2. Create `Money` value class (Double, must be non-negative)\n3. Create `TransactionId` value class (UUID string format)\n4. Write a `transfer` function that uses these types\n\n**Starter Code**:\n\n\n---\n\n",
                                "code":  "@JvmInline\nvalue class AccountNumber(val value: String) {\n    init {\n        require(value.length == 8 \u0026\u0026 value[0].isDigit()) {\n            \"Account number must be 8 digits starting with a digit\"\n        }\n    }\n}\n\n@JvmInline\nvalue class Money(val amount: Double) {\n    init {\n        require(amount \u003e= 0) { \"Money cannot be negative: $amount\" }\n    }\n}\n\n@JvmInline\nvalue class TransactionId(val value: String) {\n    init {\n        require(value.matches(Regex(\"[a-f0-9-]{36}\"))) {\n            \"Invalid UUID format: $value\"\n        }\n    }\n}\n\nfun transfer(\n    from: AccountNumber,\n    to: AccountNumber,\n    amount: Money,\n    transactionId: TransactionId\n): Boolean {\n    println(\"Transferring \\${amount.amount} from ${from.value} to ${to.value}\")\n    println(\"Transaction ID: ${transactionId.value}\")\n    return true\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 1: Product Catalog System",
                                "content":  "\n**Goal**: Create a product catalog using data classes.\n\n**Requirements**:\n1. Data class `Product` with: `id`, `name`, `price`, `category`, `inStock`\n2. Data class `Order` with: `orderId`, `products: List\u003cProduct\u003e`, `total`\n3. Function to calculate total from products\n4. Function to create a modified order with discount\n5. Test with sample products and orders\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution: Product Catalog",
                                "content":  "\n\n---\n\n",
                                "code":  "data class Product(\n    val id: Int,\n    val name: String,\n    val price: Double,\n    val category: String,\n    val inStock: Boolean = true\n)\n\ndata class Order(\n    val orderId: String,\n    val products: List\u003cProduct\u003e,\n    val discount: Double = 0.0\n) {\n    val subtotal: Double\n        get() = products.sumOf { it.price }\n\n    val total: Double\n        get() = subtotal - discount\n\n    fun applyDiscount(discountAmount: Double): Order {\n        return copy(discount = discountAmount)\n    }\n\n    fun displayOrder() {\n        println(\"\\n=== Order $orderId ===\")\n        products.forEach { product -\u003e\n            println(\"${product.name} - ${product.price}\")\n        }\n        println(\"---\")\n        println(\"Subtotal: $subtotal\")\n        if (discount \u003e 0) {\n            println(\"Discount: -$discount\")\n        }\n        println(\"Total: $total\")\n        println(\"===================\\n\")\n    }\n}\n\nfun main() {\n    val products = listOf(\n        Product(1, \"Laptop\", 999.99, \"Electronics\"),\n        Product(2, \"Mouse\", 29.99, \"Electronics\"),\n        Product(3, \"Keyboard\", 79.99, \"Electronics\"),\n        Product(4, \"Monitor\", 299.99, \"Electronics\"),\n        Product(5, \"Desk Lamp\", 39.99, \"Furniture\", inStock = false)\n    )\n\n    // Filter in-stock products\n    val availableProducts = products.filter { it.inStock }\n\n    // Create order\n    val order = Order(\n        orderId = \"ORD-2025-001\",\n        products = listOf(\n            products[0],  // Laptop\n            products[1],  // Mouse\n            products[2]   // Keyboard\n        )\n    )\n\n    order.displayOrder()\n\n    // Apply discount\n    val discountedOrder = order.applyDiscount(50.0)\n    discountedOrder.displayOrder()\n\n    // Destructuring\n    val (orderId, items, discount) = discountedOrder\n    println(\"Order ID: $orderId\")\n    println(\"Number of items: ${items.size}\")\n    println(\"Discount: $discount\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 2: API Result with Sealed Classes",
                                "content":  "\n**Goal**: Model API responses using sealed classes.\n\n**Requirements**:\n1. Sealed class `ApiResult\u003cT\u003e` with subclasses: `Success`, `Error`, `Loading`\n2. Function `fetchData()` that returns different results\n3. Function `handleResult()` that processes each case\n4. Test with different scenarios\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution: API Result",
                                "content":  "\n\n---\n\n",
                                "code":  "sealed class ApiResult\u003cout T\u003e {\n    data class Success\u003cT\u003e(val data: T) : ApiResult\u003cT\u003e()\n    data class Error(val code: Int, val message: String) : ApiResult\u003cNothing\u003e()\n    object Loading : ApiResult\u003cNothing\u003e()\n}\n\ndata class User(val id: Int, val name: String, val email: String)\n\nfun fetchUser(userId: Int): ApiResult\u003cUser\u003e {\n    return when {\n        userId \u003c= 0 -\u003e ApiResult.Error(400, \"Invalid user ID\")\n        userId == 999 -\u003e ApiResult.Loading\n        else -\u003e ApiResult.Success(User(userId, \"User $userId\", \"user$userId@example.com\"))\n    }\n}\n\nfun \u003cT\u003e handleResult(result: ApiResult\u003cT\u003e, onSuccess: (T) -\u003e Unit) {\n    when (result) {\n        is ApiResult.Success -\u003e {\n            println(\"✅ Success!\")\n            onSuccess(result.data)\n        }\n        is ApiResult.Error -\u003e {\n            println(\"❌ Error ${result.code}: ${result.message}\")\n        }\n        ApiResult.Loading -\u003e {\n            println(\"⏳ Loading...\")\n        }\n    }\n}\n\nfun main() {\n    println(\"=== Fetch User 1 ===\")\n    val result1 = fetchUser(1)\n    handleResult(result1) { user -\u003e\n        println(\"User: ${user.name} (${user.email})\")\n    }\n\n    println(\"\\n=== Fetch Invalid User ===\")\n    val result2 = fetchUser(-1)\n    handleResult(result2) { user -\u003e\n        println(\"User: ${user.name}\")\n    }\n\n    println(\"\\n=== Fetch Loading State ===\")\n    val result3 = fetchUser(999)\n    handleResult(result3) { user -\u003e\n        println(\"User: ${user.name}\")\n    }\n\n    // Using when expression directly\n    println(\"\\n=== Direct when expression ===\")\n    val message = when (val result = fetchUser(5)) {\n        is ApiResult.Success -\u003e \"Loaded: ${result.data.name}\"\n        is ApiResult.Error -\u003e \"Failed: ${result.message}\"\n        ApiResult.Loading -\u003e \"Please wait...\"\n    }\n    println(message)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 3: Task Management with Sealed Classes",
                                "content":  "\n**Goal**: Build a task management system using sealed classes for task states.\n\n**Requirements**:\n1. Sealed class `TaskState` with: `Todo`, `InProgress`, `Completed`, `Cancelled`\n2. Data class `Task` with: `id`, `title`, `description`, `state`\n3. Functions to transition between states\n4. Track state change history\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution: Task Management",
                                "content":  "\n\n---\n\n",
                                "code":  "sealed class TaskState {\n    object Todo : TaskState() {\n        override fun toString() = \"TODO\"\n    }\n\n    data class InProgress(val assignee: String, val startedAt: Long = System.currentTimeMillis()) : TaskState() {\n        override fun toString() = \"IN_PROGRESS (Assignee: $assignee)\"\n    }\n\n    data class Completed(val completedBy: String, val completedAt: Long = System.currentTimeMillis()) : TaskState() {\n        override fun toString() = \"COMPLETED (By: $completedBy)\"\n    }\n\n    data class Cancelled(val reason: String) : TaskState() {\n        override fun toString() = \"CANCELLED (Reason: $reason)\"\n    }\n}\n\ndata class Task(\n    val id: Int,\n    val title: String,\n    val description: String,\n    val state: TaskState = TaskState.Todo,\n    val history: List\u003cTaskState\u003e = listOf(TaskState.Todo)\n) {\n    fun startWork(assignee: String): Task {\n        require(state is TaskState.Todo) { \"Task must be in TODO state to start\" }\n        val newState = TaskState.InProgress(assignee)\n        return copy(state = newState, history = history + newState)\n    }\n\n    fun complete(completedBy: String): Task {\n        require(state is TaskState.InProgress) { \"Task must be in progress to complete\" }\n        val newState = TaskState.Completed(completedBy)\n        return copy(state = newState, history = history + newState)\n    }\n\n    fun cancel(reason: String): Task {\n        require(state !is TaskState.Completed) { \"Cannot cancel completed task\" }\n        val newState = TaskState.Cancelled(reason)\n        return copy(state = newState, history = history + newState)\n    }\n\n    fun displayTask() {\n        println(\"\\n=== Task #$id ===\")\n        println(\"Title: $title\")\n        println(\"Description: $description\")\n        println(\"Current State: $state\")\n        println(\"\\nState History:\")\n        history.forEachIndexed { index, state -\u003e\n            println(\"  ${index + 1}. $state\")\n        }\n        println(\"================\\n\")\n    }\n\n    fun getStatusEmoji(): String = when (state) {\n        is TaskState.Todo -\u003e \"📝\"\n        is TaskState.InProgress -\u003e \"🔄\"\n        is TaskState.Completed -\u003e \"✅\"\n        is TaskState.Cancelled -\u003e \"❌\"\n    }\n}\n\nclass TaskManager {\n    private val tasks = mutableMapOf\u003cInt, Task\u003e()\n    private var nextId = 1\n\n    fun createTask(title: String, description: String): Task {\n        val task = Task(nextId++, title, description)\n        tasks[task.id] = task\n        println(\"Created task: ${task.getStatusEmoji()} ${task.title}\")\n        return task\n    }\n\n    fun updateTask(task: Task) {\n        tasks[task.id] = task\n        println(\"Updated task: ${task.getStatusEmoji()} ${task.title} -\u003e ${task.state}\")\n    }\n\n    fun listTasks() {\n        println(\"\\n=== All Tasks ===\")\n        tasks.values.forEach { task -\u003e\n            println(\"${task.getStatusEmoji()} #${task.id}: ${task.title} [${task.state}]\")\n        }\n        println(\"=================\\n\")\n    }\n}\n\nfun main() {\n    val manager = TaskManager()\n\n    // Create tasks\n    var task1 = manager.createTask(\"Implement login\", \"Add JWT authentication\")\n    var task2 = manager.createTask(\"Fix bug #123\", \"Null pointer exception in profile\")\n    var task3 = manager.createTask(\"Write tests\", \"Unit tests for payment module\")\n\n    manager.listTasks()\n\n    // Start working on tasks\n    task1 = task1.startWork(\"Alice\")\n    manager.updateTask(task1)\n\n    task2 = task2.startWork(\"Bob\")\n    manager.updateTask(task2)\n\n    manager.listTasks()\n\n    // Complete a task\n    task1 = task1.complete(\"Alice\")\n    manager.updateTask(task1)\n\n    // Cancel a task\n    task3 = task3.cancel(\"Requirements changed\")\n    manager.updateTask(task3)\n\n    manager.listTasks()\n\n    // Display full history\n    task1.displayTask()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Checkpoint Quiz",
                                "content":  "\n### Question 1\nWhat does the `data` keyword do?\n\nA) Makes the class immutable\nB) Automatically generates `equals()`, `hashCode()`, `toString()`, and `copy()`\nC) Makes the class faster\nD) Allows inheritance\n\n### Question 2\nWhat is destructuring in data classes?\n\nA) Deleting the class\nB) Extracting multiple properties into separate variables at once\nC) Breaking inheritance\nD) Splitting the class into multiple files\n\n### Question 3\nWhat is the main advantage of sealed classes?\n\nA) They\u0027re faster\nB) They provide exhaustive `when` expression checking\nC) They use less memory\nD) They can have multiple constructors\n\n### Question 4\nWhen should you use a data class?\n\nA) When you need inheritance\nB) When you primarily need to hold data\nC) When you need abstract methods\nD) When you need multiple constructors\n\n### Question 5\nWhat\u0027s the difference between enum and sealed classes?\n\nA) Enums are faster\nB) Sealed classes can have subclasses with different properties; enums cannot\nC) Enums can inherit; sealed classes cannot\nD) There is no difference\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Quiz Answers",
                                "content":  "\n**Question 1: B) Automatically generates `equals()`, `hashCode()`, `toString()`, and `copy()`**\n\nData classes save you from writing boilerplate code.\n\n\n---\n\n**Question 2: B) Extracting multiple properties into separate variables at once**\n\nDestructuring uses the `componentN()` functions generated by data classes.\n\n\n---\n\n**Question 3: B) They provide exhaustive `when` expression checking**\n\nThe compiler ensures you handle all subclasses of a sealed class.\n\n\n---\n\n**Question 4: B) When you primarily need to hold data**\n\nData classes are perfect for DTOs, API models, configuration, etc.\n\n\n---\n\n**Question 5: B) Sealed classes can have subclasses with different properties; enums cannot**\n\nEnums are for fixed constants with the same structure. Sealed classes are for type hierarchies with varying data.\n\n\n---\n\n",
                                "code":  "// Enum: All instances have same structure\nenum class Color(val hex: String) {\n    RED(\"#FF0000\"),\n    GREEN(\"#00FF00\")\n}\n\n// Sealed: Subclasses have different properties\nsealed class Result {\n    data class Success(val data: String) : Result()\n    data class Error(val code: Int, val message: String) : Result()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What You\u0027ve Learned",
                                "content":  "\n✅ Data classes and their auto-generated functions\n✅ The `copy()` function for immutable updates\n✅ Destructuring declarations\n✅ Sealed classes for restricted hierarchies\n✅ Enum classes for fixed constants\n✅ Value classes for type-safe primitives\n✅ When to use each special class type\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Next Steps",
                                "content":  "\nIn **Lesson 2.6: Object Declarations and Companion Objects**, you\u0027ll learn:\n- Object expressions for anonymous objects\n- Object declarations for singletons\n- Companion objects for static-like members\n- Factory methods and constants\n- When to use objects vs classes\n\nYou\u0027re almost done with Part 2!\n\n---\n\n**Congratulations on completing Lesson 2.5!** 🎉\n\nData classes and sealed classes are Kotlin superpowers that make your code more concise and safer!\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "beginner",
    "title":  "Lesson 2.5: Data Classes and Sealed Classes",
    "estimatedMinutes":  65
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
- Search for "kotlin Lesson 2.5: Data Classes and Sealed Classes 2024 2025" to find latest practices
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
  "lessonId": "3.5",
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

