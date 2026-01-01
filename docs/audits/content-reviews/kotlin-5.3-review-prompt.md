# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Backend Development with Ktor
- **Lesson:** Lesson 5.3: Routing Fundamentals - Building Your First Endpoints (ID: 5.3)
- **Difficulty:** intermediate
- **Estimated Time:** 40 minutes

## Current Lesson Content

{
    "id":  "5.3",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 40 minutes\n**Difficulty**: Beginner-Intermediate\n**Prerequisites**: Lessons 5.1-5.2 (HTTP fundamentals, Ktor setup)\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "📖 Topic Introduction",
                                "content":  "\nIn the previous lesson, you created a basic server that returns plain text. Now it\u0027s time to build something more realistic: a **complete REST API** for managing a collection of books.\n\nIn this lesson, you\u0027ll:\n- Organize routes into logical groups\n- Return JSON data instead of plain text\n- Implement all CRUD operations (Create, Read, Update, Delete)\n- Use proper HTTP methods and status codes\n- Store data in memory (temporarily, before we learn databases)\n\nBy the end, you\u0027ll have a fully functional Books API that behaves like a real-world backend!\n\n---\n\n"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "💡 The Concept: RESTful Resource Management",
                                "content":  "\n### The Library Catalog Analogy\n\nThink of your API as a library\u0027s card catalog system:\n\n**GET /books** = \"Show me all books in the catalog\"\n- Like looking at the entire catalog drawer\n\n**GET /books/42** = \"Show me the details of book #42\"\n- Like pulling out a specific card\n\n**POST /books** = \"Add a new book to the catalog\"\n- Like creating a new catalog card\n\n**PUT /books/42** = \"Update all information for book #42\"\n- Like replacing an entire catalog card\n\n**DELETE /books/42** = \"Remove book #42 from the catalog\"\n- Like throwing away a catalog card\n\n### What Makes an API \"RESTful\"?\n\n**REST** (Representational State Transfer) is a set of conventions for building APIs:\n\n1. **Resources are nouns**: `/books`, not `/getBooks`\n2. **HTTP methods are verbs**: Use GET/POST/PUT/DELETE, not custom action names\n3. **Stateless**: Each request contains all needed information\n4. **Standard status codes**: 200 for success, 404 for not found, etc.\n5. **JSON for data**: Structured, language-independent format\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "🗂️ Organizing Routes",
                                "content":  "\nAs your API grows, putting all routes in one function becomes messy. Let\u0027s learn to organize them properly.\n\n### Route Organization Patterns\n\n**Pattern 1: Flat (What We Did Before)**\n\n**Pattern 2: Grouped by Resource (Better!)**\n\n**Pattern 3: Separate Files by Resource (Best for Large Projects)**\n\nFor this lesson, we\u0027ll use **Pattern 2** (grouped routes).\n\n---\n\n",
                                "code":  "// BookRoutes.kt\nfun Route.bookRoutes() {\n    route(\"/books\") {\n        get { }\n        post { }\n        // etc.\n    }\n}\n\n// Application.kt\nrouting {\n    bookRoutes()\n    userRoutes()\n    orderRoutes()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "💻 Code: Building a Complete Books API",
                                "content":  "\nLet\u0027s build a complete CRUD API step by step.\n\n### Step 1: Define the Data Model\n\nCreate `src/main/kotlin/com/example/models/Book.kt`:\n\n\n**Understanding the annotations:**\n\n- **@Serializable**: Tells kotlinx.serialization this class can be converted to/from JSON\n- **data class**: Automatically generates `equals()`, `hashCode()`, `toString()`, `copy()`\n- **String?**: The `?` makes `isbn` nullable (optional)\n\n### Step 2: Create an In-Memory Data Store\n\nCreate `src/main/kotlin/com/example/data/BookStorage.kt`:\n\n\n**Key concepts:**\n\n- **object BookStorage**: Singleton pattern (only one instance)\n- **AtomicInteger**: Thread-safe counter for generating IDs\n- **init { }**: Code that runs when the object is first accessed\n- **find { }**: Returns first matching item or `null`\n- **indexOfFirst { }**: Returns index of first match or `-1`\n- **removeIf { }**: Removes all items matching the predicate\n\n### Step 3: Define Request/Response Models\n\nCreate `src/main/kotlin/com/example/models/BookRequest.kt`:\n\n\n**Why separate request models?**\n\n1. **Security**: Clients shouldn\u0027t send IDs when creating (server assigns them)\n2. **Flexibility**: Updates can be partial (only changed fields)\n3. **Clarity**: Clear what data is expected\n\n### Step 4: Build the Routes\n\nNow for the main event! Update `src/main/kotlin/com/example/plugins/Routing.kt`:\n\n\n---\n\n",
                                "code":  "package com.example.plugins\n\nimport com.example.data.BookStorage\nimport com.example.models.*\nimport io.ktor.http.*\nimport io.ktor.server.application.*\nimport io.ktor.server.request.*\nimport io.ktor.server.response.*\nimport io.ktor.server.routing.*\n\nfun Application.configureRouting() {\n    routing {\n        // Root endpoint\n        get(\"/\") {\n            call.respondText(\"Books API is running! Visit /api/books\")\n        }\n\n        // API routes\n        route(\"/api\") {\n            bookRoutes()\n        }\n    }\n}\n\n// Book routes grouped together\nfun Route.bookRoutes() {\n    route(\"/books\") {\n        // GET /api/books - List all books\n        get {\n            val books = BookStorage.getAll()\n            call.respond(\n                HttpStatusCode.OK,\n                ApiResponse(success = true, data = books)\n            )\n        }\n\n        // GET /api/books/{id} - Get specific book\n        get(\"/{id}\") {\n            val id = call.parameters[\"id\"]?.toIntOrNull()\n\n            if (id == null) {\n                call.respond(\n                    HttpStatusCode.BadRequest,\n                    ApiResponse\u003cBook\u003e(\n                        success = false,\n                        message = \"Invalid book ID\"\n                    )\n                )\n                return@get\n            }\n\n            val book = BookStorage.getById(id)\n\n            if (book == null) {\n                call.respond(\n                    HttpStatusCode.NotFound,\n                    ApiResponse\u003cBook\u003e(\n                        success = false,\n                        message = \"Book not found\"\n                    )\n                )\n            } else {\n                call.respond(\n                    HttpStatusCode.OK,\n                    ApiResponse(success = true, data = book)\n                )\n            }\n        }\n\n        // POST /api/books - Create new book\n        post {\n            val request = call.receive\u003cCreateBookRequest\u003e()\n\n            // Simple validation\n            if (request.title.isBlank() || request.author.isBlank()) {\n                call.respond(\n                    HttpStatusCode.BadRequest,\n                    ApiResponse\u003cBook\u003e(\n                        success = false,\n                        message = \"Title and author are required\"\n                    )\n                )\n                return@post\n            }\n\n            val newBook = Book(\n                id = 0, // Will be replaced by storage\n                title = request.title,\n                author = request.author,\n                year = request.year,\n                isbn = request.isbn\n            )\n\n            val created = BookStorage.add(newBook)\n\n            call.respond(\n                HttpStatusCode.Created,\n                ApiResponse(\n                    success = true,\n                    data = created,\n                    message = \"Book created successfully\"\n                )\n            )\n        }\n\n        // PUT /api/books/{id} - Update book\n        put(\"/{id}\") {\n            val id = call.parameters[\"id\"]?.toIntOrNull()\n\n            if (id == null) {\n                call.respond(\n                    HttpStatusCode.BadRequest,\n                    ApiResponse\u003cBook\u003e(\n                        success = false,\n                        message = \"Invalid book ID\"\n                    )\n                )\n                return@put\n            }\n\n            val request = call.receive\u003cCreateBookRequest\u003e()\n\n            val updatedBook = Book(\n                id = id,\n                title = request.title,\n                author = request.author,\n                year = request.year,\n                isbn = request.isbn\n            )\n\n            val success = BookStorage.update(id, updatedBook)\n\n            if (success) {\n                call.respond(\n                    HttpStatusCode.OK,\n                    ApiResponse(\n                        success = true,\n                        data = updatedBook,\n                        message = \"Book updated successfully\"\n                    )\n                )\n            } else {\n                call.respond(\n                    HttpStatusCode.NotFound,\n                    ApiResponse\u003cBook\u003e(\n                        success = false,\n                        message = \"Book not found\"\n                    )\n                )\n            }\n        }\n\n        // DELETE /api/books/{id} - Delete book\n        delete(\"/{id}\") {\n            val id = call.parameters[\"id\"]?.toIntOrNull()\n\n            if (id == null) {\n                call.respond(\n                    HttpStatusCode.BadRequest,\n                    ApiResponse\u003cUnit\u003e(\n                        success = false,\n                        message = \"Invalid book ID\"\n                    )\n                )\n                return@delete\n            }\n\n            val success = BookStorage.delete(id)\n\n            if (success) {\n                call.respond(\n                    HttpStatusCode.OK,\n                    ApiResponse\u003cUnit\u003e(\n                        success = true,\n                        message = \"Book deleted successfully\"\n                    )\n                )\n            } else {\n                call.respond(\n                    HttpStatusCode.NotFound,\n                    ApiResponse\u003cUnit\u003e(\n                        success = false,\n                        message = \"Book not found\"\n                    )\n                )\n            }\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "🔍 Code Breakdown",
                                "content":  "\nLet\u0027s analyze the key patterns:\n\n### 1. Route Organization\n\n\n**Benefits:**\n- Clear hierarchy\n- Easy to add new routes\n- Can move to separate files as project grows\n\n### 2. Receiving Request Bodies\n\n\n- **call.receive\u003cT\u003e()**: Automatically parses JSON to Kotlin object\n- Throws exception if JSON is invalid (we\u0027ll handle this in later lessons)\n\n### 3. Responding with Status Codes\n\n\n**Common patterns:**\n- **200 OK**: Successful GET/PUT\n- **201 Created**: Successful POST (new resource)\n- **204 No Content**: Successful DELETE (no body needed)\n- **400 Bad Request**: Invalid input\n- **404 Not Found**: Resource doesn\u0027t exist\n\n### 4. Parameter Extraction and Validation\n\n\n**Key techniques:**\n- **?.toIntOrNull()**: Safe conversion (null if not a number)\n- **Early return**: If validation fails, respond and exit\n- **@get/@post/@put/@delete**: Label for return statement\n\n### 5. API Response Wrapper\n\n\n**Consistent responses:**\n\n---\n\n",
                                "code":  "{\n  \"success\": true,\n  \"data\": { \"id\": 1, \"title\": \"1984\" },\n  \"message\": \"Book created successfully\"\n}",
                                "language":  "json"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "🧪 Testing Your Complete API",
                                "content":  "\n### Test GET All Books\n\n\n**Expected Response:**\n\n### Test GET Single Book\n\n\n### Test CREATE New Book\n\n\n**Expected Response:**\n\n### Test UPDATE Book\n\n\n### Test DELETE Book\n\n\n### Test Error Cases\n\n\n---\n\n",
                                "code":  "# Invalid ID (not a number)\ncurl http://localhost:8080/api/books/abc\n\n# Non-existent book\ncurl http://localhost:8080/api/books/9999\n\n# Empty title (validation error)\ncurl -X POST http://localhost:8080/api/books \\\n  -H \"Content-Type: application/json\" \\\n  -d \u0027{\"title\": \"\", \"author\": \"Unknown\", \"year\": 2024}\u0027",
                                "language":  "bash"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "🎯 Exercise: Extend the API",
                                "content":  "\nAdd these features to your Books API:\n\n### Exercise 1: Search by Title\n\nAdd a route that searches books by title (case-insensitive).\n\n**Endpoint**: `GET /api/books/search?title=brave`\n\n**Expected**: Return all books whose title contains \"brave\" (case-insensitive)\n\n**Hints:**\n\n### Exercise 2: Filter by Year\n\nAdd a route to get books published in a specific year range.\n\n**Endpoint**: `GET /api/books/filter?minYear=1930\u0026maxYear=1950`\n\n**Expected**: Return books published between 1930 and 1950 (inclusive)\n\n### Exercise 3: Get Books by Author\n\nAdd a route to get all books by a specific author.\n\n**Endpoint**: `GET /api/books/author/{authorName}`\n\n**Expected**: Return all books by that author (case-insensitive match)\n\n### Exercise 4: Count Endpoint\n\nAdd a route that returns the total number of books.\n\n**Endpoint**: `GET /api/books/count`\n\n**Expected Response**:\n\n---\n\n",
                                "code":  "{\n  \"success\": true,\n  \"data\": { \"count\": 5 },\n  \"message\": \"Total books counted\"\n}",
                                "language":  "json"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "✅ Solution \u0026 Explanation",
                                "content":  "\nHere\u0027s the complete solution with all exercises:\n\n\n### Testing the Solutions\n\n\n### Key Techniques Used\n\n1. **Query Parameters**: `call.request.queryParameters[\"key\"]`\n2. **Filtering**: `filter { predicate }` on lists\n3. **Range Check**: `it.year in minYear..maxYear`\n4. **Case-Insensitive Search**: `contains(query, ignoreCase = true)`\n5. **Inline Data Classes**: Define response structure locally\n\n---\n\n",
                                "code":  "# Exercise 1: Search\ncurl \"http://localhost:8080/api/books/search?title=brave\"\n\n# Exercise 2: Filter by year\ncurl \"http://localhost:8080/api/books/filter?minYear=1930\u0026maxYear=1950\"\n\n# Exercise 3: Books by author\ncurl http://localhost:8080/api/books/author/Orwell\n\n# Exercise 4: Count\ncurl http://localhost:8080/api/books/count",
                                "language":  "bash"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "📝 Lesson Checkpoint Quiz",
                                "content":  "\nTest your understanding of Ktor routing:\n\n### Question 1\nIn the route definition `route(\"/books\") { get(\"/{id}\") { } }`, what is the full path that will be matched?\n\nA) `/books`\nB) `/books/{id}`\nC) `/{id}/books`\nD) `/books/id`\n\n---\n\n### Question 2\nWhich HTTP status code should you return when a client tries to create a book with an empty title?\n\nA) 200 OK\nB) 201 Created\nC) 400 Bad Request\nD) 404 Not Found\n\n---\n\n### Question 3\nWhat does `call.receive\u003cCreateBookRequest\u003e()` do?\n\nA) Sends a CreateBookRequest to the client\nB) Converts the JSON request body into a CreateBookRequest object\nC) Creates a new book in the database\nD) Validates that the request is correctly formatted\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "🎯 Why This Matters",
                                "content":  "\nYou just built a **production-ready REST API**! This exact pattern is used by:\n\n- **E-commerce sites** for managing products\n- **Social media** for managing posts and comments\n- **Banking apps** for managing accounts and transactions\n- **Any mobile app** that needs to store data on a server\n\n### What You\u0027ve Mastered\n\n✅ **CRUD Operations**: The foundation of 90% of all APIs\n✅ **RESTful Design**: Industry-standard API architecture\n✅ **JSON Serialization**: Converting Kotlin ↔ JSON automatically\n✅ **Route Organization**: Keeping code clean as it grows\n✅ **Error Handling**: Proper status codes for different scenarios\n✅ **Request/Response Models**: Type-safe API contracts\n\n### The Missing Piece\n\nRight now, your data disappears when the server restarts (it\u0027s only in memory). In the next lessons, you\u0027ll learn:\n\n- **Databases**: Persistent storage that survives restarts\n- **Validation**: More sophisticated input checking\n- **Authentication**: Protecting routes (login required)\n- **Testing**: Ensuring your API works correctly\n\nBut the routing patterns you learned today? **They stay the same**. You\u0027ll just swap the in-memory storage for a database.\n\n---\n\n"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "📚 Key Takeaways",
                                "content":  "\n✅ **route(\"/path\")** groups related endpoints together\n✅ **GET** retrieves data, **POST** creates, **PUT** updates, **DELETE** removes\n✅ **call.receive\u003cT\u003e()** parses JSON request body to Kotlin object\n✅ **call.respond(status, data)** sends JSON response with status code\n✅ **@Serializable** makes Kotlin classes convertible to/from JSON\n✅ **Path parameters** capture dynamic parts of URLs: `/{id}`\n✅ **Query parameters** provide filters: `?title=kotlin\u0026year=2020`\n✅ **Status codes** communicate results: 200, 201, 400, 404\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "🔜 Next Steps",
                                "content":  "\nIn **Lesson 5.4**, you\u0027ll dive deeper into:\n- Path parameters vs. query parameters (when to use each)\n- Accessing request headers\n- Complex query parameters (multiple values, optional params)\n- Request body validation patterns\n- Nested routes and sub-resources\n\n---\n\n"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "✏️ Quiz Answer Key",
                                "content":  "\n**Question 1**: **B) `/books/{id}`**\n\nExplanation: The `route(\"/books\")` sets the base path, and `get(\"/{id}\")` appends to it, resulting in `/books/{id}`.\n\n---\n\n**Question 2**: **C) 400 Bad Request**\n\nExplanation: 400 indicates the client sent invalid data. The request format is correct (it\u0027s JSON), but the content violates business rules (empty title).\n\n---\n\n**Question 3**: **B) Converts the JSON request body into a CreateBookRequest object**\n\nExplanation: `call.receive\u003cT\u003e()` uses kotlinx.serialization to automatically parse the JSON body into the specified Kotlin type. It\u0027s the \"receive\" counterpart to \"respond\".\n\n---\n\n**Congratulations!** You\u0027ve built a complete REST API with full CRUD operations! You now have a real, testable backend that handles JSON data. 🎉\n\n"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "5.3.1",
                           "title":  "Request Validation",
                           "description":  "Create a function that validates a registration request. Check that username is at least 3 characters and email contains \u0027@\u0027.",
                           "instructions":  "Create a function that validates a registration request. Check that username is at least 3 characters and email contains \u0027@\u0027.",
                           "starterCode":  "data class RegisterRequest(val username: String, val email: String, val password: String)\n\nfun validateRegistration(request: RegisterRequest): List\u003cString\u003e {\n    val errors = mutableListOf\u003cString\u003e()\n    // Add validation logic\n    \n    return errors\n}\n\nfun main() {\n    val request1 = RegisterRequest(\"ab\", \"invalidemail\", \"pass\")\n    val errors = validateRegistration(request1)\n    println(\"Errors: $errors\")\n    \n    val request2 = RegisterRequest(\"alice\", \"alice@example.com\", \"password123\")\n    val errors2 = validateRegistration(request2)\n    println(\"Valid: ${errors2.isEmpty()}\")\n}",
                           "solution":  "data class RegisterRequest(val username: String, val email: String, val password: String)\n\nfun validateRegistration(request: RegisterRequest): List\u003cString\u003e {\n    val errors = mutableListOf\u003cString\u003e()\n    \n    if (request.username.length \u003c 3) {\n        errors.add(\"Username must be at least 3 characters\")\n    }\n    \n    if (!request.email.contains(\"@\")) {\n        errors.add(\"Email must contain @\")\n    }\n    \n    if (request.password.length \u003c 8) {\n        errors.add(\"Password must be at least 8 characters\")\n    }\n    \n    return errors\n}\n\nfun main() {\n    val request1 = RegisterRequest(\"ab\", \"invalidemail\", \"pass\")\n    val errors = validateRegistration(request1)\n    println(\"Errors: $errors\")\n    \n    val request2 = RegisterRequest(\"alice\", \"alice@example.com\", \"password123\")\n    val errors2 = validateRegistration(request2)\n    println(\"Valid: ${errors2.isEmpty()}\")\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Should return errors for invalid data",
                                                 "expectedOutput":  "Errors:",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Should return empty list for valid data",
                                                 "expectedOutput":  "Valid: true",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Create a mutableListOf\u003cString\u003e for errors"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Check username.length \u003e= 3"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "Check email.contains(\"@\")"
                                         },
                                         {
                                             "level":  4,
                                             "text":  "Check password.length \u003e= 8"
                                         },
                                         {
                                             "level":  5,
                                             "text":  "Add error message for each failed check"
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
    "title":  "Lesson 5.3: Routing Fundamentals - Building Your First Endpoints",
    "estimatedMinutes":  40
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
- Search for "kotlin Lesson 5.3: Routing Fundamentals - Building Your First Endpoints 2024 2025" to find latest practices
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
  "lessonId": "5.3",
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

