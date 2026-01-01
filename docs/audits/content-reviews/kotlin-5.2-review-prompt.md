# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Backend Development with Ktor
- **Lesson:** Lesson 5.2: Setting Up Your First Ktor Project (ID: 5.2)
- **Difficulty:** intermediate
- **Estimated Time:** 35 minutes

## Current Lesson Content

{
    "id":  "5.2",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 35 minutes\n**Difficulty**: Beginner\n**Prerequisites**: Lesson 5.1 (HTTP Fundamentals), Kotlin basics\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "📖 Topic Introduction",
                                "content":  "\nIn the previous lesson, you learned the concepts: HTTP methods, status codes, and REST API design. Now it\u0027s time to build your first actual backend server!\n\nIn this lesson, you\u0027ll:\n- Create a Ktor project from scratch\n- Understand the project structure\n- Install essential plugins\n- Run your first server that responds to HTTP requests\n- Test your API with a web browser\n\nBy the end, you\u0027ll have a working server running on your computer that you can visit in your browser!\n\n---\n\n"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "💡 The Concept: What Is Ktor?",
                                "content":  "\n### The Building Blocks Analogy\n\nImagine you\u0027re building a house:\n\n**Traditional frameworks** = Pre-fabricated houses\n- Lots of features you might not need\n- Heavy and opinionated\n- Hard to customize\n\n**Ktor** = A box of high-quality building blocks\n- Start with a minimal foundation\n- Add only what you need (plugins)\n- Lightweight and flexible\n- Perfect for learning because you see every piece\n\n### Why Ktor for Learning?\n\n1. **Kotlin-first**: Written specifically for Kotlin, not a Java framework adapted for Kotlin\n2. **Lightweight**: Minimal boilerplate, clear code\n3. **Plugin-based**: Each feature (routing, JSON, authentication) is a separate plugin you explicitly add\n4. **Async by default**: Uses Kotlin coroutines for efficient handling of many requests\n5. **Modern**: Built with current best practices\n\n### Ktor Architecture\n\n\n---\n\n",
                                "code":  "┌─────────────────────────────────────┐\n│      Your Ktor Application          │\n├─────────────────────────────────────┤\n│  ┌───────────────────────────────┐  │\n│  │  Routing Plugin               │  │  \u003c-- Define endpoints\n│  ├───────────────────────────────┤  │\n│  │  ContentNegotiation Plugin    │  │  \u003c-- JSON support\n│  ├───────────────────────────────┤  │\n│  │  Authentication Plugin        │  │  \u003c-- Login/JWT\n│  ├───────────────────────────────┤  │\n│  │  Your Business Logic          │  │  \u003c-- Your code\n│  └───────────────────────────────┘  │\n├─────────────────────────────────────┤\n│      Ktor Engine (CIO/Netty)        │  \u003c-- Handles HTTP\n└─────────────────────────────────────┘",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "🚀 Setting Up Your Development Environment",
                                "content":  "\n### Prerequisites Check\n\nBefore we start, ensure you have:\n\n1. **JDK 17 or higher** installed\n   ```bash\n   java -version\n   # Should show: java version \"17\" or higher\n   ```\n\n2. **IntelliJ IDEA** (Community Edition is free) or any IDE with Kotlin support\n\n3. **Gradle** (usually bundled with IDE, but verify):\n   ```bash\n   gradle -version\n   # Should show: Gradle 8.0 or higher\n   ```\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "💻 Creating Your First Ktor Project",
                                "content":  "\n### Method 1: Using the Ktor Project Generator (Recommended for Beginners)\n\n1. **Visit the Generator**\n   - Open your browser and go to: https://start.ktor.io/\n\n2. **Configure Your Project**\n   ```\n   Project Name: my-first-api\n   Build System: Gradle Kotlin\n   Website: example.com\n   Artifact: com.example.myfirstapi\n   Ktor Version: 3.2.0 (or latest)\n   Engine: CIO\n   Configuration: Code (not YAML/HOCON for now)\n   ```\n\n3. **Add Plugins**\n   - **Routing**: For defining endpoints (essential!)\n   - **Content Negotiation**: For JSON support (essential!)\n   - **kotlinx.serialization**: For converting objects to/from JSON\n\n4. **Generate and Download**\n   - Click \"Generate Project\"\n   - Download the ZIP file\n   - Extract it to your projects folder\n\n### Method 2: Manual Setup with Gradle (For Understanding)\n\nIf you want to understand every piece, let\u0027s build it manually:\n\n**Step 1: Create a new directory**\n\n**Step 2: Create the Gradle build file**\n\nCreate `build.gradle.kts`:\n\n\n**Step 3: Create Gradle wrapper files**\n\nCreate `gradle.properties`:\n\nCreate `settings.gradle.kts`:\n\n---\n\n",
                                "code":  "rootProject.name = \"my-first-api\"",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "📁 Understanding the Project Structure",
                                "content":  "\nAfter creation, your project should look like this:\n\n\nLet\u0027s understand each piece:\n\n- **build.gradle.kts**: Defines dependencies and build configuration\n- **Application.kt**: The main file that starts your server\n- **plugins/**: Modular plugin configurations\n- **resources/**: Configuration files (logging, etc.)\n\n---\n\n",
                                "code":  "my-first-api/\n├── build.gradle.kts              # Gradle build configuration\n├── settings.gradle.kts           # Project settings\n├── gradle.properties             # Gradle properties\n├── gradlew                       # Gradle wrapper (Unix)\n├── gradlew.bat                   # Gradle wrapper (Windows)\n├── src/\n│   └── main/\n│       ├── kotlin/\n│       │   └── com/example/\n│       │       ├── Application.kt      # Main entry point\n│       │       └── plugins/\n│       │           ├── Routing.kt      # Route definitions\n│       │           └── Serialization.kt # JSON config\n│       └── resources/\n│           └── logback.xml             # Logging configuration\n└── .gitignore",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "🔧 Writing Your First Server Code",
                                "content":  "\n### Step 1: Create the Main Application File\n\nCreate `src/main/kotlin/com/example/Application.kt`:\n\n\n**Let\u0027s break this down:**\n\n- **embeddedServer**: Runs Ktor inside your application (no separate Tomcat/Jetty)\n- **CIO**: Coroutine-based I/O engine (lightweight and perfect for learning)\n- **port = 8080**: Your server will be accessible at `http://localhost:8080`\n- **host = \"0.0.0.0\"**: Accept connections from any network interface\n\n- This is an **extension function** on the `Application` class\n- It\u0027s where you configure all your plugins and routes\n\n### Step 2: Configure JSON Serialization\n\nCreate `src/main/kotlin/com/example/plugins/Serialization.kt`:\n\n\n**What this does:**\n- **ContentNegotiation**: Plugin that handles converting Kotlin objects ↔ JSON\n- **json()**: Configure JSON serialization settings\n- **prettyPrint**: Makes the JSON output readable (with indentation)\n\n### Step 3: Define Your First Routes\n\nCreate `src/main/kotlin/com/example/plugins/Routing.kt`:\n\n\n**Understanding the routing:**\n\n- **routing { }**: Block where you define all routes\n- **get(\"/\")**: Handle GET requests to the root path\n- **call**: Represents the current HTTP request/response\n- **respondText()**: Send plain text response\n\n### Step 4: Add Logging Configuration\n\nCreate `src/main/resources/logback.xml`:\n\n\nThis configures logging so you can see what your server is doing.\n\n---\n\n",
                                "code":  "\u003cconfiguration\u003e\n    \u003cappender name=\"STDOUT\" class=\"ch.qos.logback.core.ConsoleAppender\"\u003e\n        \u003cencoder\u003e\n            \u003cpattern\u003e%d{HH:mm:ss.SSS} [%thread] %-5level %logger{36} - %msg%n\u003c/pattern\u003e\n        \u003c/encoder\u003e\n    \u003c/appender\u003e\n\n    \u003croot level=\"INFO\"\u003e\n        \u003cappender-ref ref=\"STDOUT\"/\u003e\n    \u003c/root\u003e\n\n    \u003clogger name=\"io.ktor\" level=\"DEBUG\"/\u003e\n\u003c/configuration\u003e",
                                "language":  "xml"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "🏃 Running Your Server",
                                "content":  "\n### Using IntelliJ IDEA\n\n1. Open the project in IntelliJ IDEA\n2. Wait for Gradle to sync dependencies (bottom right corner)\n3. Open `Application.kt`\n4. Click the green play button next to `fun main()`\n5. Wait for the server to start (you\u0027ll see logs in the console)\n\n### Using Command Line\n\n\n### Expected Output\n\nYou should see something like:\n\n\n🎉 **Your server is now running!**\n\n---\n\n",
                                "code":  "[main] INFO  Application - Autoreload is disabled because the development mode is off.\n[main] INFO  Application - Responding at http://0.0.0.0:8080\n[DefaultDispatcher-worker-1] INFO  Application - Application started in 0.453 seconds.",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "🧪 Testing Your API",
                                "content":  "\n### Method 1: Web Browser (Simplest)\n\n1. Open your web browser\n2. Visit: `http://localhost:8080/`\n3. You should see: **\"Hello, Ktor! Your server is running! 🚀\"**\n\nTry these URLs:\n- `http://localhost:8080/health` → \"OK\"\n- `http://localhost:8080/api/hello` → \"Hello from the API!\"\n\n### Method 2: curl (Command Line)\n\n\nThe `-i` flag shows headers:\n\n\n### Method 3: Postman (GUI Tool)\n\n1. Download Postman (free): https://www.postman.com/downloads/\n2. Create a new request\n3. Set method to GET\n4. Enter URL: `http://localhost:8080/`\n5. Click \"Send\"\n6. See the response in the bottom panel\n\n---\n\n",
                                "code":  "HTTP/1.1 200 OK\nContent-Length: 42\nContent-Type: text/plain; charset=UTF-8\n\nHello, Ktor! Your server is running! 🚀",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "🔍 Code Breakdown: How It All Works",
                                "content":  "\nLet\u0027s trace what happens when you visit `http://localhost:8080/`:\n\n\n### Understanding the `call` Object\n\n\n**`call` provides access to:**\n- `call.request` - Information about the incoming request\n- `call.response` - The response you\u0027re building\n- `call.respondText()` - Send plain text\n- `call.respond()` - Send any object (will be converted to JSON)\n- `call.parameters` - URL parameters\n- `call.receive\u003cT\u003e()` - Get request body as object\n\n---\n\n",
                                "code":  "get(\"/\") {\n    call.respondText(\"Hello\")\n    // \u0027call\u0027 is of type ApplicationCall\n    // It represents the current request/response\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "🎯 Exercise: Add Your Own Endpoints",
                                "content":  "\nNow it\u0027s your turn! Add these endpoints to your `Routing.kt`:\n\n### Exercise Tasks\n\n1. **Create a `/ping` endpoint** that returns \"pong\"\n\n2. **Create a `/api/time` endpoint** that returns the current server time\n\n3. **Create a `/api/greet/{name}` endpoint** that greets the user by name\n   - Example: `/api/greet/Alice` → \"Hello, Alice!\"\n\n4. **Create a `/api/random` endpoint** that returns a random number between 1 and 100\n\n### Hints\n\n\nTry to complete these on your own before looking at the solution!\n\n---\n\n",
                                "code":  "// Hint for current time\nimport java.time.LocalDateTime\nval now = LocalDateTime.now().toString()\n\n// Hint for path parameter\nget(\"/api/greet/{name}\") {\n    val name = call.parameters[\"name\"]\n    call.respondText(\"Hello, $name!\")\n}\n\n// Hint for random number\nimport kotlin.random.Random\nval number = Random.nextInt(1, 101)",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "✅ Solution \u0026 Explanation",
                                "content":  "\nHere\u0027s the complete `Routing.kt` with all exercises:\n\n\n### Testing Your Solutions\n\n\n### Key Concepts Demonstrated\n\n1. **Path Parameters**: `{name}` in the route becomes accessible via `call.parameters[\"name\"]`\n2. **String Templates**: `\"Hello, $name\"` embeds variables in strings\n3. **Null Safety**: `name.isNullOrBlank()` checks for null or empty values\n4. **Libraries**: Using `LocalDateTime` and `Random` from Kotlin/Java standard library\n\n---\n\n",
                                "code":  "# Test ping\ncurl http://localhost:8080/ping\n# Output: pong\n\n# Test time\ncurl http://localhost:8080/api/time\n# Output: Current server time: 2024-11-13T15:30:45.123\n\n# Test greeting\ncurl http://localhost:8080/api/greet/Alice\n# Output: Hello, Alice! Welcome to our API! 👋\n\n# Test random\ncurl http://localhost:8080/api/random\n# Output: Your random number is: 42\n\n# Test bonus\ncurl http://localhost:8080/api/greet/John/Doe\n# Output: Hello, John Doe! 🎉",
                                "language":  "bash"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "📝 Lesson Checkpoint Quiz",
                                "content":  "\nTest your understanding of Ktor project setup:\n\n### Question 1\nWhat is the purpose of the `embeddedServer` function in Ktor?\n\nA) It connects to an external web server like Apache\nB) It runs the Ktor application as a standalone server inside your program\nC) It embeds HTML files in your application\nD) It compresses the server code to reduce file size\n\n---\n\n### Question 2\nIn the route definition `get(\"/api/users/{id}\")`, what does `{id}` represent?\n\nA) A comment that will be ignored\nB) A literal string that must include the curly braces\nC) A path parameter that captures a dynamic value from the URL\nD) An error in the syntax\n\n---\n\n### Question 3\nWhich Ktor plugin is required to automatically convert Kotlin objects to JSON?\n\nA) Routing\nB) ContentNegotiation with kotlinx.serialization\nC) CIO\nD) Authentication\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "🎯 Why This Matters",
                                "content":  "\nYou just built a **real HTTP server** from scratch! This is the foundation of:\n\n- **Every website backend** (Facebook, Twitter, Reddit)\n- **Every mobile app backend** (Instagram, WhatsApp, TikTok)\n- **Every IoT device** that communicates over the internet\n- **Every microservice** in modern cloud architecture\n\n### What You\u0027ve Actually Accomplished\n\nBefore today, when you visited a website, it felt like magic. Now you understand:\n\n✅ **How servers listen** for requests on ports (`:8080`)\n✅ **How routing works** - matching URLs to code that handles them\n✅ **How responses are built** - your code generates what users see\n✅ **How to test APIs** - using browsers, curl, or Postman\n\n### The Next Steps\n\nRight now, your server only returns simple text. In the next lessons, you\u0027ll learn to:\n\n- Return **JSON data** (structured data, not just text)\n- Accept **data from clients** (POST requests with body)\n- Connect to **databases** (persistent storage)\n- Add **authentication** (login systems)\n- **Validate input** (prevent bad data)\n\nBut you\u0027ve crossed the biggest hurdle: **you have a working server**. Everything else builds on this foundation.\n\n---\n\n"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "📚 Key Takeaways",
                                "content":  "\n✅ **Ktor** is a lightweight Kotlin framework for building servers\n✅ **embeddedServer()** runs your application as a standalone server\n✅ **Plugins** add functionality (routing, JSON, auth) modularly\n✅ **routing { }** is where you define URL endpoints\n✅ **get(\"/path\")** handles HTTP GET requests to that path\n✅ **call.respondText()** sends text responses\n✅ **call.parameters[\"name\"]** accesses URL path parameters\n✅ Test with **browser**, **curl**, or **Postman**\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "🔜 Next Steps",
                                "content":  "\nIn **Lesson 5.3**, you\u0027ll:\n- Build a proper REST API for managing books\n- Return JSON instead of plain text\n- Handle POST requests to create data\n- Organize routes into logical groups\n- Implement all CRUD operations (Create, Read, Update, Delete)\n\n---\n\n"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "✏️ Quiz Answer Key",
                                "content":  "\n**Question 1**: **B) It runs the Ktor application as a standalone server inside your program**\n\nExplanation: `embeddedServer` starts Ktor as an embedded server (no external Tomcat/Jetty needed). Your application *is* the server.\n\n---\n\n**Question 2**: **C) A path parameter that captures a dynamic value from the URL**\n\nExplanation: `{id}` is a path parameter placeholder. When someone visits `/api/users/42`, the value `42` is captured and accessible via `call.parameters[\"id\"]`.\n\n---\n\n**Question 3**: **B) ContentNegotiation with kotlinx.serialization**\n\nExplanation: ContentNegotiation handles content type negotiation (JSON, XML, etc.), and kotlinx.serialization provides the actual JSON conversion. Together, they enable automatic Kotlin object ↔ JSON transformation.\n\n---\n\n**Congratulations!** You\u0027ve set up your first Ktor project and built a working server. You\u0027re now officially a backend developer! 🎉\n\n"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "5.2.1",
                           "title":  "User Model with Validation",
                           "description":  "Create a User data class with name and email. Add a method `isValid()` that checks if email contains \u0027@\u0027.",
                           "instructions":  "Create a User data class with name and email. Add a method `isValid()` that checks if email contains \u0027@\u0027.",
                           "starterCode":  "data class User(val name: String, val email: String) {\n    // Add isValid method\n}\n\nfun main() {\n    val user1 = User(\"Alice\", \"alice@example.com\")\n    val user2 = User(\"Bob\", \"invalid-email\")\n    println(\"User1 valid: ${user1.isValid()}\")\n    println(\"User2 valid: ${user2.isValid()}\")\n}",
                           "solution":  "data class User(val name: String, val email: String) {\n    fun isValid(): Boolean {\n        return email.contains(\"@\") \u0026\u0026 name.isNotBlank()\n    }\n}\n\nfun main() {\n    val user1 = User(\"Alice\", \"alice@example.com\")\n    val user2 = User(\"Bob\", \"invalid-email\")\n    println(\"User1 valid: ${user1.isValid()}\")\n    println(\"User2 valid: ${user2.isValid()}\")\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Valid user should return true",
                                                 "expectedOutput":  "User1 valid: true",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Invalid email should return false",
                                                 "expectedOutput":  "User2 valid: false",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Add method inside data class"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Check if email contains @ using contains()"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "Also check if name is not blank"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting null safety operators",
                                                      "consequence":  "NullPointerException",
                                                      "correction":  "Use ?. for safe calls, ?: for elvis operator"
                                                  },
                                                  {
                                                      "mistake":  "Using var when val would suffice",
                                                      "consequence":  "Unnecessary mutability",
                                                      "correction":  "Prefer val for immutable values"
                                                  },
                                                  {
                                                      "mistake":  "Incorrect string interpolation",
                                                      "consequence":  "Syntax error",
                                                      "correction":  "Use $variable or ${expression}"
                                                  }
                                              ],
                           "difficulty":  "intermediate"
                       },
                       {
                           "type":  "FREE_CODING",
                           "id":  "5.2.2",
                           "title":  "Simple In-Memory Database",
                           "description":  "Create a simple in-memory user database using a mutableListOf. Add methods to add and find users.",
                           "instructions":  "Create a simple in-memory user database using a mutableListOf. Add methods to add and find users.",
                           "starterCode":  "data class User(val id: Int, val name: String, val email: String)\n\nclass UserDatabase {\n    private val users = mutableListOf\u003cUser\u003e()\n    \n    fun addUser(user: User) {\n        // Add user to list\n    }\n    \n    fun findById(id: Int): User? {\n        // Find user by ID\n    }\n}\n\nfun main() {\n    val db = UserDatabase()\n    db.addUser(User(1, \"Alice\", \"alice@example.com\"))\n    db.addUser(User(2, \"Bob\", \"bob@example.com\"))\n    \n    println(db.findById(1))\n    println(db.findById(99))\n}",
                           "solution":  "data class User(val id: Int, val name: String, val email: String)\n\nclass UserDatabase {\n    private val users = mutableListOf\u003cUser\u003e()\n    \n    fun addUser(user: User) {\n        users.add(user)\n    }\n    \n    fun findById(id: Int): User? {\n        return users.find { it.id == id }\n    }\n}\n\nfun main() {\n    val db = UserDatabase()\n    db.addUser(User(1, \"Alice\", \"alice@example.com\"))\n    db.addUser(User(2, \"Bob\", \"bob@example.com\"))\n    \n    println(db.findById(1))\n    println(db.findById(99))\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Should find existing user",
                                                 "expectedOutput":  "User(id=1, name=Alice, email=alice@example.com)",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Should return null for non-existent user",
                                                 "expectedOutput":  "null",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use mutableListOf to store users"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "add() method adds to list"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "find() method searches list with predicate"
                                         },
                                         {
                                             "level":  4,
                                             "text":  "Return null if not found"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting null safety operators",
                                                      "consequence":  "NullPointerException",
                                                      "correction":  "Use ?. for safe calls, ?: for elvis operator"
                                                  },
                                                  {
                                                      "mistake":  "Using var when val would suffice",
                                                      "consequence":  "Unnecessary mutability",
                                                      "correction":  "Prefer val for immutable values"
                                                  },
                                                  {
                                                      "mistake":  "Incorrect string interpolation",
                                                      "consequence":  "Syntax error",
                                                      "correction":  "Use $variable or ${expression}"
                                                  }
                                              ],
                           "difficulty":  "intermediate"
                       }
                   ],
    "difficulty":  "intermediate",
    "title":  "Lesson 5.2: Setting Up Your First Ktor Project",
    "estimatedMinutes":  35
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
- Search for "kotlin Lesson 5.2: Setting Up Your First Ktor Project 2024 2025" to find latest practices
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
  "lessonId": "5.2",
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

