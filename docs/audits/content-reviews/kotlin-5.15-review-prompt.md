# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Backend Development with Ktor
- **Lesson:** Lesson 5.15: Part 5 Capstone Project - Task Management API (ID: 5.15)
- **Difficulty:** intermediate
- **Estimated Time:** 30 minutes

## Current Lesson Content

{
    "id":  "5.15",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 4-6 hours\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Project Overview",
                                "content":  "\nCongratulations on completing Part 5! You\u0027ve learned backend development with Ktor, from HTTP fundamentals to authentication, testing, and clean architecture.\n\nNow it\u0027s time to put everything together in a **complete, production-ready Task Management API**.\n\nThis capstone project will challenge you to integrate all the skills you\u0027ve learned:\n- ✅ HTTP REST API design\n- ✅ Database operations with Exposed\n- ✅ Clean architecture (repositories, services, routes)\n- ✅ Request validation and error handling\n- ✅ JWT authentication and authorization\n- ✅ Role-based access control\n- ✅ Dependency injection with Koin\n- ✅ Comprehensive testing\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "The Project: TaskMaster API",
                                "content":  "\n**TaskMaster** is a collaborative task management system where users can:\n- Create and manage personal tasks\n- Share tasks with team members\n- Assign tasks and track progress\n- Filter and search tasks\n- Receive notifications (bonus)\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Requirements",
                                "content":  "\n### 1. User Management\n\n**Models**:\n\n**Endpoints**:\n- `POST /api/auth/register` - Register new user\n- `POST /api/auth/login` - Login and receive JWT token\n- `GET /api/auth/me` - Get current user (protected)\n- `PUT /api/users/me` - Update profile (protected)\n- `DELETE /api/users/me` - Delete account (protected)\n\n**Requirements**:\n- Email validation\n- Password strength requirements (min 8 chars, uppercase, lowercase, number, special char)\n- Username uniqueness\n- JWT tokens with 1-hour expiration\n- bcrypt password hashing\n\n---\n\n### 2. Task Management\n\n**Models**:\n\n**Endpoints**:\n- `POST /api/tasks` - Create task (protected)\n- `GET /api/tasks` - Get all user\u0027s tasks with filters (protected)\n- `GET /api/tasks/:id` - Get task by ID (protected)\n- `PUT /api/tasks/:id` - Update task (owner or assignee)\n- `DELETE /api/tasks/:id` - Delete task (owner only)\n- `POST /api/tasks/:id/assign` - Assign task to user (owner only)\n- `PATCH /api/tasks/:id/status` - Update task status (owner or assignee)\n\n**Query Parameters for GET /api/tasks**:\n- `status` - Filter by status (TODO, IN_PROGRESS, DONE)\n- `priority` - Filter by priority (LOW, MEDIUM, HIGH)\n- `assignedToMe` - Show only tasks assigned to current user\n- `search` - Search in title and description\n- `sortBy` - Sort by (dueDate, priority, createdAt)\n- `order` - Order (asc, desc)\n\n**Authorization Rules**:\n- Users can only see tasks they own or are assigned to\n- Users can only create tasks\n- Owners can update, delete, and assign tasks\n- Assignees can update task status only\n- Admins can see and modify all tasks\n\n---\n\n### 3. Comments (Optional Enhancement)\n\n**Models**:\n\n**Endpoints**:\n- `POST /api/tasks/:id/comments` - Add comment (protected)\n- `GET /api/tasks/:id/comments` - Get task comments (protected)\n- `DELETE /api/comments/:id` - Delete comment (author or admin)\n\n---\n\n### 4. Error Handling\n\nAll errors must return consistent JSON format:\n\n\n**HTTP Status Codes**:\n- 200 OK - Success\n- 201 Created - Resource created\n- 400 Bad Request - Validation error\n- 401 Unauthorized - Not authenticated\n- 403 Forbidden - Not authorized\n- 404 Not Found - Resource doesn\u0027t exist\n- 409 Conflict - Duplicate resource\n- 500 Internal Server Error - Unexpected error\n\n---\n\n### 5. Validation\n\n**Task Validation**:\n- Title: required, 1-200 characters\n- Description: optional, max 1000 characters\n- Status: must be TODO, IN_PROGRESS, or DONE\n- Priority: must be LOW, MEDIUM, or HIGH\n- DueDate: optional, must be valid ISO 8601, can\u0027t be in the past\n- AssignedToId: optional, must be existing user\n\n**User Validation**:\n- Email: valid email format, unique\n- Username: 3-20 chars, alphanumeric + underscore, unique\n- Password: min 8 chars, uppercase, lowercase, number, special char\n- FullName: 2-100 characters\n\n---\n\n### 6. Testing Requirements\n\n**Unit Tests** (minimum 70% coverage):\n- UserService tests with mock repository\n- AuthService tests for login/register\n- TaskService tests for CRUD and authorization\n- Validator tests for all validation rules\n\n**Integration Tests**:\n- Auth endpoints (register, login)\n- Task CRUD endpoints\n- Authorization tests (owner, assignee, non-member)\n- Query parameter filtering\n- Error cases (validation, not found, forbidden)\n\n---\n\n### 7. Architecture Requirements\n\n**Clean Architecture**:\n\n**Dependency Injection**:\n- Use Koin for all dependency management\n- Separate modules for repositories, services, database\n- Easy to swap implementations for testing\n\n---\n\n",
                                "code":  "┌─────────────────────────────────────┐\n│  Routes (HTTP Layer)                │\n│  - Parse requests                   │\n│  - Call services                    │\n│  - Return responses                 │\n└─────────────────────────────────────┘\n              ↓\n┌─────────────────────────────────────┐\n│  Services (Business Logic)          │\n│  - Validation                       │\n│  - Authorization                    │\n│  - Orchestration                    │\n└─────────────────────────────────────┘\n              ↓\n┌─────────────────────────────────────┐\n│  Repositories (Data Access)         │\n│  - Database queries                 │\n│  - Data mapping                     │\n└─────────────────────────────────────┘\n              ↓\n┌─────────────────────────────────────┐\n│  Database (Exposed + H2)            │\n└─────────────────────────────────────┘",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Step-by-Step Implementation Guide",
                                "content":  "\n### Phase 1: Project Setup (30 minutes)\n\n1. **Create New Project**:\n   ```bash\n   mkdir taskmaster-api\n   cd taskmaster-api\n   gradle init --type kotlin-application\n   ```\n\n2. **Add Dependencies** in `build.gradle.kts`:\n   ```kotlin\n   dependencies {\n       implementation(\"io.ktor:ktor-server-core-jvm:3.0.2\")\n       implementation(\"io.ktor:ktor-server-cio-jvm:3.0.2\")\n       implementation(\"io.ktor:ktor-server-content-negotiation-jvm:3.0.2\")\n       implementation(\"io.ktor:ktor-serialization-kotlinx-json-jvm:3.0.2\")\n       implementation(\"io.ktor:ktor-server-auth-jvm:3.0.2\")\n       implementation(\"io.ktor:ktor-server-auth-jwt-jvm:3.0.2\")\n       implementation(\"org.jetbrains.exposed:exposed-core:0.50.0\")\n       implementation(\"org.jetbrains.exposed:exposed-jdbc:0.50.0\")\n       implementation(\"com.h2database:h2:2.2.224\")\n       implementation(\"com.zaxxer:HikariCP:5.1.0\")\n       implementation(\"de.nycode:bcrypt:2.3.0\")\n       implementation(\"com.auth0:java-jwt:4.5.0\")\n       implementation(\"io.insert-koin:koin-ktor:4.0.3\")\n       implementation(\"io.insert-koin:koin-logger-slf4j:4.0.3\")\n       implementation(\"ch.qos.logback:logback-classic:1.4.14\")\n\n       testImplementation(\"io.ktor:ktor-server-test-host:3.0.2\")\n       testImplementation(\"org.jetbrains.kotlin:kotlin-test-junit5:2.0.0\")\n       testImplementation(\"org.junit.jupiter:junit-jupiter-api:5.10.2\")\n       testRuntimeOnly(\"org.junit.jupiter:junit-jupiter-engine:5.10.2\")\n       testImplementation(\"io.insert-koin:koin-test:4.0.3\")\n       testImplementation(\"io.insert-koin:koin-test-junit5:4.0.3\")\n   }\n   ```\n\n3. **Create Package Structure**:\n   ```\n   src/main/kotlin/com/taskmaster/\n   ├── Application.kt\n   ├── models/\n   │   ├── User.kt\n   │   ├── Task.kt\n   │   └── Responses.kt\n   ├── repositories/\n   │   ├── UserRepository.kt\n   │   └── TaskRepository.kt\n   ├── services/\n   │   ├── UserService.kt\n   │   ├── AuthService.kt\n   │   └── TaskService.kt\n   ├── routes/\n   │   ├── AuthRoutes.kt\n   │   └── TaskRoutes.kt\n   ├── validation/\n   │   ├── Validator.kt\n   │   ├── UserValidator.kt\n   │   └── TaskValidator.kt\n   ├── security/\n   │   ├── JwtConfig.kt\n   │   └── PasswordHasher.kt\n   ├── exceptions/\n   │   └── ApiExceptions.kt\n   ├── plugins/\n   │   ├── Authentication.kt\n   │   └── ErrorHandling.kt\n   ├── database/\n   │   └── DatabaseFactory.kt\n   └── di/\n       ├── RepositoryModule.kt\n       ├── ServiceModule.kt\n       └── AppModule.kt\n   ```\n\n---\n\n### Phase 2: Core Models \u0026 Database (45 minutes)\n\n1. **Define Models** (`models/User.kt`, `models/Task.kt`):\n   - User model with all fields\n   - Task model with status and priority enums\n   - Request/Response DTOs\n\n2. **Create Database Tables** (`database/DatabaseFactory.kt`):\n   - Users table with unique constraints\n   - Tasks table with foreign keys\n   - Initialize H2 database\n\n3. **Implement Repositories**:\n   - UserRepository interface and implementation\n   - TaskRepository interface and implementation\n   - Include query methods (filters, search)\n\n---\n\n### Phase 3: Validation \u0026 Error Handling (30 minutes)\n\n1. **Create Validators**:\n   - Base Validator class with common methods\n   - UserValidator for registration\n   - TaskValidator for task creation/updates\n\n2. **Define Exceptions**:\n   - ValidationException\n   - NotFoundException\n   - ConflictException\n   - ForbiddenException\n   - UnauthorizedException\n\n3. **Configure Error Handling**:\n   - StatusPages plugin configuration\n   - Consistent error response format\n\n---\n\n### Phase 4: Authentication System (60 minutes)\n\n1. **Implement Password Hashing**:\n   - PasswordHasher utility with bcrypt\n   - Password strength validation\n\n2. **Configure JWT**:\n   - JwtConfig with token generation\n   - Include user ID, email, role in claims\n\n3. **Build Auth Services**:\n   - UserService with registration\n   - AuthService with login\n\n4. **Create Auth Routes**:\n   - POST /api/auth/register\n   - POST /api/auth/login\n   - GET /api/auth/me\n\n5. **Configure Authentication Plugin**:\n   - JWT validation\n   - UserPrincipal extraction\n\n---\n\n### Phase 5: Task Management (90 minutes)\n\n1. **Implement TaskService**:\n   - Create task (with validation)\n   - Update task (with ownership check)\n   - Delete task (owner only)\n   - Get tasks with filters\n   - Assign task to user\n   - Update task status\n\n2. **Create Task Routes**:\n   - All CRUD endpoints\n   - Query parameter handling\n   - Authorization checks\n\n3. **Implement Authorization Logic**:\n   - canViewTask(task, user)\n   - canModifyTask(task, user)\n   - canDeleteTask(task, user)\n\n---\n\n### Phase 6: Dependency Injection (30 minutes)\n\n1. **Define Koin Modules**:\n   - RepositoryModule\n   - ServiceModule\n   - DatabaseModule\n\n2. **Configure Koin**:\n   - Install Koin plugin\n   - Load modules\n\n3. **Inject Dependencies**:\n   - Update routes to inject services\n   - Remove manual wiring\n\n---\n\n### Phase 7: Testing (90 minutes)\n\n1. **Unit Tests**:\n   - UserService tests (5+ tests)\n   - AuthService tests (5+ tests)\n   - TaskService tests (10+ tests covering authorization)\n   - Validator tests\n\n2. **Integration Tests**:\n   - Auth endpoint tests\n   - Task CRUD tests\n   - Authorization tests\n   - Query filter tests\n\n3. **Run Coverage**:\n   - Configure JaCoCo\n   - Aim for 70%+ coverage\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Evaluation Criteria",
                                "content":  "\n### Core Requirements (80 points)\n\n- ✅ All endpoints implemented and working (20 points)\n- ✅ Authentication with JWT (15 points)\n- ✅ Authorization (owner/assignee/admin) (15 points)\n- ✅ Validation with clear error messages (10 points)\n- ✅ Clean architecture (repositories, services, routes) (10 points)\n- ✅ Dependency injection with Koin (10 points)\n\n### Testing (15 points)\n\n- ✅ Unit tests with 70%+ coverage (10 points)\n- ✅ Integration tests for main flows (5 points)\n\n### Code Quality (5 points)\n\n- ✅ Consistent code style\n- ✅ Clear naming conventions\n- ✅ No code duplication\n- ✅ Proper error handling\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Bonus Challenges (+20 points)",
                                "content":  "\n### Challenge 1: Task Comments (+5 points)\nImplement the comment system:\n- Add comments to tasks\n- Get task comments\n- Delete comments (author or admin only)\n\n### Challenge 2: Pagination (+5 points)\nAdd pagination to GET /api/tasks:\n- `page` query parameter (default: 1)\n- `pageSize` query parameter (default: 20)\n- Return metadata: totalPages, totalItems, currentPage\n\n### Challenge 3: Task Tags (+5 points)\nAdd tagging system:\n- Tasks can have multiple tags\n- Filter tasks by tags\n- Create/delete tags\n\n### Challenge 4: Task Analytics (+5 points)\nAdd analytics endpoints:\n- GET /api/analytics/summary - Task counts by status\n- GET /api/analytics/user/:id - User\u0027s task statistics\n- GET /api/analytics/overdue - Overdue tasks report\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Example Solution Structure",
                                "content":  "\n\n---\n\n",
                                "code":  "// models/Task.kt\n@Serializable\ndata class Task(\n    val id: Int,\n    val title: String,\n    val description: String?,\n    val status: TaskStatus,\n    val priority: TaskPriority,\n    val dueDate: String?,\n    val ownerId: Int,\n    val assignedToId: Int?,\n    val createdAt: String,\n    val updatedAt: String\n)\n\n@Serializable\nenum class TaskStatus {\n    TODO, IN_PROGRESS, DONE\n}\n\n@Serializable\nenum class TaskPriority {\n    LOW, MEDIUM, HIGH\n}\n\n@Serializable\ndata class CreateTaskRequest(\n    val title: String,\n    val description: String? = null,\n    val status: String = \"TODO\",\n    val priority: String = \"MEDIUM\",\n    val dueDate: String? = null,\n    val assignedToId: Int? = null\n)\n\n@Serializable\ndata class UpdateTaskRequest(\n    val title: String,\n    val description: String?,\n    val status: String,\n    val priority: String,\n    val dueDate: String?\n)\n\n@Serializable\ndata class UpdateTaskStatusRequest(\n    val status: String\n)\n\n@Serializable\ndata class AssignTaskRequest(\n    val assignedToId: Int\n)\n\n// repositories/TaskRepository.kt\ninterface TaskRepository {\n    fun insert(task: Task): Int\n    fun update(id: Int, task: Task): Boolean\n    fun delete(id: Int): Boolean\n    fun getById(id: Int): Task?\n    fun getAllForUser(userId: Int): List\u003cTask\u003e\n    fun getAssignedToUser(userId: Int): List\u003cTask\u003e\n    fun search(userId: Int, filters: TaskFilters): List\u003cTask\u003e\n}\n\ndata class TaskFilters(\n    val status: TaskStatus? = null,\n    val priority: TaskPriority? = null,\n    val assignedToMe: Boolean = false,\n    val search: String? = null,\n    val sortBy: String = \"createdAt\",\n    val order: String = \"desc\"\n)\n\n// services/TaskService.kt\nclass TaskService(\n    private val taskRepository: TaskRepository,\n    private val userRepository: UserRepository\n) {\n    fun createTask(request: CreateTaskRequest, principal: UserPrincipal): Result\u003cTask\u003e\n    fun updateTask(id: Int, request: UpdateTaskRequest, principal: UserPrincipal): Result\u003cTask\u003e\n    fun deleteTask(id: Int, principal: UserPrincipal): Result\u003cUnit\u003e\n    fun getTaskById(id: Int, principal: UserPrincipal): Result\u003cTask\u003e\n    fun getUserTasks(principal: UserPrincipal, filters: TaskFilters): Result\u003cList\u003cTask\u003e\u003e\n    fun assignTask(id: Int, request: AssignTaskRequest, principal: UserPrincipal): Result\u003cTask\u003e\n    fun updateTaskStatus(id: Int, request: UpdateTaskStatusRequest, principal: UserPrincipal): Result\u003cTask\u003e\n\n    private fun canViewTask(task: Task, principal: UserPrincipal): Boolean {\n        return task.ownerId == principal.userId ||\n               task.assignedToId == principal.userId ||\n               principal.role == \"ADMIN\"\n    }\n\n    private fun canModifyTask(task: Task, principal: UserPrincipal): Boolean {\n        return task.ownerId == principal.userId || principal.role == \"ADMIN\"\n    }\n\n    private fun canUpdateStatus(task: Task, principal: UserPrincipal): Boolean {\n        return task.ownerId == principal.userId ||\n               task.assignedToId == principal.userId ||\n               principal.role == \"ADMIN\"\n    }\n}\n\n// routes/TaskRoutes.kt\nfun Route.taskRoutes() {\n    val taskService by inject\u003cTaskService\u003e()\n\n    authenticate(\"jwt-auth\") {\n        route(\"/api/tasks\") {\n            post {\n                val principal = call.principal\u003cUserPrincipal\u003e()!!\n                val request = call.receive\u003cCreateTaskRequest\u003e()\n\n                taskService.createTask(request, principal)\n                    .onSuccess { task -\u003e\n                        call.respond(\n                            HttpStatusCode.Created,\n                            ApiResponse(data = task, message = \"Task created\")\n                        )\n                    }\n                    .onFailure { error -\u003e throw error }\n            }\n\n            get {\n                val principal = call.principal\u003cUserPrincipal\u003e()!!\n                val filters = TaskFilters(\n                    status = call.request.queryParameters[\"status\"]?.let { TaskStatus.valueOf(it) },\n                    priority = call.request.queryParameters[\"priority\"]?.let { TaskPriority.valueOf(it) },\n                    assignedToMe = call.request.queryParameters[\"assignedToMe\"]?.toBoolean() ?: false,\n                    search = call.request.queryParameters[\"search\"],\n                    sortBy = call.request.queryParameters[\"sortBy\"] ?: \"createdAt\",\n                    order = call.request.queryParameters[\"order\"] ?: \"desc\"\n                )\n\n                taskService.getUserTasks(principal, filters)\n                    .onSuccess { tasks -\u003e\n                        call.respond(ApiResponse(data = tasks))\n                    }\n                    .onFailure { error -\u003e throw error }\n            }\n\n            // ... other routes\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Testing Your Solution",
                                "content":  "\n### Manual Testing with cURL\n\n\n### Automated Tests\n\nRun all tests:\n\nCheck coverage:\n\n---\n\n",
                                "code":  "./gradlew test jacocoTestReport\nopen build/reports/jacoco/test/html/index.html",
                                "language":  "bash"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Submission Checklist",
                                "content":  "\nBefore submitting, ensure you have:\n\n- [ ] All core endpoints implemented\n- [ ] JWT authentication working\n- [ ] Authorization rules enforced\n- [ ] Validation with clear error messages\n- [ ] Clean architecture (repositories, services, routes)\n- [ ] Dependency injection with Koin\n- [ ] Unit tests with 70%+ coverage\n- [ ] Integration tests for main flows\n- [ ] README.md with setup instructions\n- [ ] No hardcoded secrets (use environment variables)\n- [ ] Code follows Kotlin conventions\n- [ ] Git repository with meaningful commits\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Tips for Success",
                                "content":  "\n### Time Management\n- **Don\u0027t skip Phase 1**: Proper setup saves time later\n- **Test as you go**: Don\u0027t wait until the end to test\n- **Use previous lessons**: Copy patterns from earlier exercises\n- **Focus on core features first**: Get basics working before bonuses\n\n### Common Pitfalls\n- ❌ Forgetting to hash passwords\n- ❌ Not validating token expiration\n- ❌ Missing authorization checks\n- ❌ Inconsistent error responses\n- ❌ Not testing edge cases\n\n### Debugging Tips\n- Use `println()` for quick debugging\n- Check logs for SQL queries\n- Test endpoints with Postman or cURL\n- Verify JWT tokens at jwt.io\n- Run tests frequently\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What Success Looks Like",
                                "content":  "\nBy completing this capstone, you will have:\n\n✅ **Built a production-ready REST API** from scratch\n✅ **Implemented authentication and authorization** with JWT\n✅ **Designed clean, maintainable architecture** following best practices\n✅ **Written comprehensive tests** for confidence in your code\n✅ **Integrated multiple technologies** (Ktor, Exposed, Koin, JWT, bcrypt)\n✅ **Demonstrated professional development skills** that employers value\n\nThis project is portfolio-worthy. Host it on GitHub, deploy it to a cloud platform, and showcase it to potential employers!\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Next Steps After Part 5",
                                "content":  "\nCongratulations on completing Part 5! Here\u0027s what comes next:\n\n**Part 6: Android Development**\n- Jetpack Compose fundamentals\n- MVVM architecture\n- Retrofit for API consumption\n- Connecting your TaskMaster API to an Android app\n\n**Part 7: Advanced Topics**\n- Coroutines and async programming\n- Kotlin Multiplatform\n- Performance optimization\n- Production deployment\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Final Words",
                                "content":  "\nYou\u0027ve come a long way! From understanding HTTP basics to building a complete backend API with authentication, authorization, testing, and clean architecture.\n\nThe skills you\u0027ve learned in Part 5 are in high demand:\n- Backend development with Ktor\n- REST API design\n- Authentication with JWT\n- Clean architecture patterns\n- Testing strategies\n- Dependency injection\n\nTake your time with this capstone. It\u0027s challenging, but every challenge you overcome makes you a better developer.\n\n**You\u0027ve got this!** 🚀\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Resources",
                                "content":  "\n### Documentation\n- [Ktor Official Docs](https://ktor.io/docs/)\n- [Exposed Documentation](https://github.com/JetBrains/Exposed/wiki)\n- [Koin Documentation](https://insert-koin.io/docs/)\n- [JWT.io](https://jwt.io/)\n\n### Tools\n- [Postman](https://www.postman.com/) - API testing\n- [IntelliJ IDEA](https://www.jetbrains.com/idea/) - Kotlin IDE\n- [H2 Console](http://localhost:8080/h2-console) - Database browser\n\n### Community\n- [Kotlin Slack](https://surveys.jetbrains.com/s3/kotlin-slack-sign-up)\n- [r/Kotlin](https://www.reddit.com/r/Kotlin/)\n- [Stack Overflow](https://stackoverflow.com/questions/tagged/kotlin)\n\n---\n\n**Good luck with your capstone project!** 🎯\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "intermediate",
    "title":  "Lesson 5.15: Part 5 Capstone Project - Task Management API",
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
- Search for "kotlin Lesson 5.15: Part 5 Capstone Project - Task Management API 2024 2025" to find latest practices
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
  "lessonId": "5.15",
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

