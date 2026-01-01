# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Backend Development with Ktor
- **Lesson:** Lesson 5.14: Testing Your API (ID: 5.14)
- **Difficulty:** intermediate
- **Estimated Time:** 70 minutes

## Current Lesson Content

{
    "id":  "5.14",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 70 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Topic Introduction",
                                "content":  "\nYou\u0027ve built a complete backend API with authentication, validation, and clean architecture. But how do you know it works correctly? How do you ensure new features don\u0027t break existing functionality?\n\nThe answer: **automated testing**.\n\nIn this lesson, you\u0027ll learn how to write comprehensive tests for your Ktor API, from unit tests for individual services to integration tests for full HTTP endpoints. You\u0027ll use Ktor\u0027s testing utilities and Koin\u0027s test features to build a robust test suite.\n\n---\n\n"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept",
                                "content":  "\n### The Safety Net Analogy\n\nThink of tests like a safety net for trapeze artists:\n\n**Without Tests (No Safety Net)**:\n- Every code change is scary\n- Fear of breaking things prevents improvements\n- Bugs discovered by users (embarrassing!)\n- Hours spent manually testing after each change\n- 😰 High stress, low confidence\n\n**With Tests (Safety Net)**:\n- Confident refactoring\n- Catch bugs before deployment\n- Automated validation (run tests in seconds)\n- Documentation (tests show how code should work)\n- ✅ Low stress, high confidence!\n\nTests are your safety net—they catch you when you fall.\n\n### The Testing Pyramid\n\n\n**Test Distribution**:\n- **70%** Unit Tests: Fast, isolated, test individual functions\n- **20%** Integration Tests: Test components working together\n- **10%** End-to-End Tests: Test entire system from UI to database\n\nWe\u0027ll focus on unit and integration tests for backend APIs.\n\n### Types of Tests for APIs\n\n| Test Type | What It Tests | Example |\n|-----------|---------------|---------|\n| **Unit** | Single function/class in isolation | UserService.createUser() with mock repository |\n| **Integration** | Multiple components together | POST /api/users endpoint with real database |\n| **Contract** | API matches specification | Response has required fields |\n| **Performance** | Speed and scalability | API handles 1000 req/sec |\n\n---\n\n",
                                "code":  "          /\\\n         /  \\        E2E Tests (Few)\n        /____\\       - Full system, slow, brittle\n       /      \\\n      /        \\     Integration Tests (Some)\n     /__________\\    - Multiple components, medium speed\n    /            \\\n   /              \\  Unit Tests (Many)\n  /________________\\ - Single component, fast, reliable",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Setting Up Testing",
                                "content":  "\n### Step 1: Add Test Dependencies\n\nUpdate your `build.gradle.kts`:\n\n\n---\n\n",
                                "code":  "dependencies {\n    // Production dependencies\n    implementation(\"io.ktor:ktor-server-core-jvm:3.0.2\")\n    implementation(\"io.ktor:ktor-server-cio-jvm:3.0.2\")\n    implementation(\"io.ktor:ktor-server-content-negotiation-jvm:3.0.2\")\n    implementation(\"io.ktor:ktor-serialization-kotlinx-json-jvm:3.0.2\")\n    implementation(\"io.ktor:ktor-server-auth-jvm:3.0.2\")\n    implementation(\"io.ktor:ktor-server-auth-jwt-jvm:3.0.2\")\n    implementation(\"org.jetbrains.exposed:exposed-core:0.50.0\")\n    implementation(\"org.jetbrains.exposed:exposed-jdbc:0.50.0\")\n    implementation(\"com.h2database:h2:2.2.224\")\n    implementation(\"com.zaxxer:HikariCP:5.1.0\")\n    implementation(\"de.nycode:bcrypt:2.3.0\")\n    implementation(\"com.auth0:java-jwt:4.5.0\")\n    implementation(\"io.insert-koin:koin-ktor:4.0.3\")\n    implementation(\"io.insert-koin:koin-logger-slf4j:4.0.3\")\n\n    // Test dependencies\n    testImplementation(\"io.ktor:ktor-server-test-host:3.0.2\")\n    testImplementation(\"org.jetbrains.kotlin:kotlin-test-junit5:2.0.0\")\n    testImplementation(\"org.junit.jupiter:junit-jupiter-api:5.10.2\")\n    testRuntimeOnly(\"org.junit.jupiter:junit-jupiter-engine:5.10.2\")\n    testImplementation(\"io.insert-koin:koin-test:4.0.3\")\n    testImplementation(\"io.insert-koin:koin-test-junit5:4.0.3\")\n}\n\ntasks.withType\u003cTest\u003e {\n    useJUnitPlatform()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Unit Testing Services",
                                "content":  "\n### Example: Testing UserService\n\n\n### Running Unit Tests\n\n\nOutput:\n\n---\n\n",
                                "code":  "UserServiceTest \u003e should create user successfully PASSED\nUserServiceTest \u003e should fail when email already exists PASSED\nUserServiceTest \u003e should retrieve user by ID PASSED\nUserServiceTest \u003e should return not found for non-existent user PASSED\nUserServiceTest \u003e should update user profile PASSED\n\nBUILD SUCCESSFUL in 2s\n5 tests completed, 5 passed",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Integration Testing Endpoints",
                                "content":  "\n### Example: Testing Auth Endpoints\n\n\n---\n\n",
                                "code":  "// src/test/kotlin/com/example/routes/AuthRoutesTest.kt\npackage com.example.routes\n\nimport com.example.database.DatabaseFactory\nimport com.example.di.appModules\nimport com.example.models.ApiResponse\nimport com.example.models.LoginRequest\nimport com.example.models.LoginResponse\nimport com.example.models.RegisterRequest\nimport com.example.module\nimport io.ktor.client.call.*\nimport io.ktor.client.plugins.contentnegotiation.*\nimport io.ktor.client.request.*\nimport io.ktor.http.*\nimport io.ktor.serialization.kotlinx.json.*\nimport io.ktor.server.testing.*\nimport kotlinx.serialization.json.Json\nimport org.junit.jupiter.api.AfterAll\nimport org.junit.jupiter.api.BeforeAll\nimport org.junit.jupiter.api.Test\nimport org.koin.core.context.stopKoin\nimport kotlin.test.assertEquals\nimport kotlin.test.assertNotNull\nimport kotlin.test.assertTrue\n\nclass AuthRoutesTest {\n\n    companion object {\n        @BeforeAll\n        @JvmStatic\n        fun setup() {\n            // Initialize test database\n            DatabaseFactory.init()\n        }\n\n        @AfterAll\n        @JvmStatic\n        fun teardown() {\n            stopKoin()\n        }\n    }\n\n    @Test\n    fun `test user registration`() = testApplication {\n        application {\n            module()  // Load your application module\n        }\n\n        // Create HTTP client with JSON support\n        val client = createClient {\n            install(ContentNegotiation) {\n                json(Json {\n                    ignoreUnknownKeys = true\n                })\n            }\n        }\n\n        // Send registration request\n        val response = client.post(\"/api/auth/register\") {\n            contentType(ContentType.Application.Json)\n            setBody(\n                RegisterRequest(\n                    email = \"test@example.com\",\n                    password = \"SecurePass123!\",\n                    fullName = \"Test User\"\n                )\n            )\n        }\n\n        // Assert response\n        assertEquals(HttpStatusCode.Created, response.status)\n\n        val apiResponse = response.body\u003cApiResponse\u003cRegisterResponse\u003e\u003e()\n        assertTrue(apiResponse.success)\n        assertNotNull(apiResponse.data)\n        assertEquals(\"test@example.com\", apiResponse.data?.user?.email)\n        assertEquals(\"Test User\", apiResponse.data?.user?.fullName)\n    }\n\n    @Test\n    fun `test user registration with weak password`() = testApplication {\n        application {\n            module()\n        }\n\n        val client = createClient {\n            install(ContentNegotiation) {\n                json(Json {\n                    ignoreUnknownKeys = true\n                })\n            }\n        }\n\n        val response = client.post(\"/api/auth/register\") {\n            contentType(ContentType.Application.Json)\n            setBody(\n                RegisterRequest(\n                    email = \"test2@example.com\",\n                    password = \"weak\",  // Weak password\n                    fullName = \"Test User 2\"\n                )\n            )\n        }\n\n        // Assert validation error\n        assertEquals(HttpStatusCode.BadRequest, response.status)\n\n        val apiResponse = response.body\u003cErrorResponse\u003e()\n        assertEquals(false, apiResponse.success)\n        assertNotNull(apiResponse.errors)\n        assertTrue(apiResponse.errors!!.containsKey(\"password\"))\n    }\n\n    @Test\n    fun `test user login`() = testApplication {\n        application {\n            module()\n        }\n\n        val client = createClient {\n            install(ContentNegotiation) {\n                json(Json {\n                    ignoreUnknownKeys = true\n                })\n            }\n        }\n\n        // First, register a user\n        client.post(\"/api/auth/register\") {\n            contentType(ContentType.Application.Json)\n            setBody(\n                RegisterRequest(\n                    email = \"login@example.com\",\n                    password = \"SecurePass123!\",\n                    fullName = \"Login User\"\n                )\n            )\n        }\n\n        // Now, login with credentials\n        val loginResponse = client.post(\"/api/auth/login\") {\n            contentType(ContentType.Application.Json)\n            setBody(\n                LoginRequest(\n                    email = \"login@example.com\",\n                    password = \"SecurePass123!\"\n                )\n            )\n        }\n\n        // Assert successful login\n        assertEquals(HttpStatusCode.OK, loginResponse.status)\n\n        val apiResponse = loginResponse.body\u003cApiResponse\u003cLoginResponse\u003e\u003e()\n        assertTrue(apiResponse.success)\n        assertNotNull(apiResponse.data)\n        assertNotNull(apiResponse.data?.token)\n        assertEquals(\"login@example.com\", apiResponse.data?.user?.email)\n    }\n\n    @Test\n    fun `test login with wrong password`() = testApplication {\n        application {\n            module()\n        }\n\n        val client = createClient {\n            install(ContentNegotiation) {\n                json(Json {\n                    ignoreUnknownKeys = true\n                })\n            }\n        }\n\n        // Register user\n        client.post(\"/api/auth/register\") {\n            contentType(ContentType.Application.Json)\n            setBody(\n                RegisterRequest(\n                    email = \"wrong@example.com\",\n                    password = \"SecurePass123!\",\n                    fullName = \"Wrong User\"\n                )\n            )\n        }\n\n        // Try to login with wrong password\n        val loginResponse = client.post(\"/api/auth/login\") {\n            contentType(ContentType.Application.Json)\n            setBody(\n                LoginRequest(\n                    email = \"wrong@example.com\",\n                    password = \"WrongPassword!\"\n                )\n            )\n        }\n\n        // Assert unauthorized\n        assertEquals(HttpStatusCode.Unauthorized, loginResponse.status)\n\n        val apiResponse = loginResponse.body\u003cErrorResponse\u003e()\n        assertEquals(false, apiResponse.success)\n        assertEquals(\"Invalid email or password\", apiResponse.message)\n    }\n\n    @Test\n    fun `test duplicate email registration`() = testApplication {\n        application {\n            module()\n        }\n\n        val client = createClient {\n            install(ContentNegotiation) {\n                json(Json {\n                    ignoreUnknownKeys = true\n                })\n            }\n        }\n\n        // Register first user\n        client.post(\"/api/auth/register\") {\n            contentType(ContentType.Application.Json)\n            setBody(\n                RegisterRequest(\n                    email = \"duplicate@example.com\",\n                    password = \"SecurePass123!\",\n                    fullName = \"First User\"\n                )\n            )\n        }\n\n        // Try to register second user with same email\n        val response = client.post(\"/api/auth/register\") {\n            contentType(ContentType.Application.Json)\n            setBody(\n                RegisterRequest(\n                    email = \"duplicate@example.com\",\n                    password = \"DifferentPass456!\",\n                    fullName = \"Second User\"\n                )\n            )\n        }\n\n        // Assert conflict error\n        assertEquals(HttpStatusCode.Conflict, response.status)\n\n        val apiResponse = response.body\u003cErrorResponse\u003e()\n        assertEquals(false, apiResponse.success)\n        assertTrue(apiResponse.message.contains(\"already exists\"))\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Testing Protected Endpoints",
                                "content":  "\n\n---\n\n",
                                "code":  "// src/test/kotlin/com/example/routes/UserRoutesTest.kt\npackage com.example.routes\n\nimport com.example.database.DatabaseFactory\nimport com.example.models.*\nimport com.example.module\nimport io.ktor.client.call.*\nimport io.ktor.client.plugins.contentnegotiation.*\nimport io.ktor.client.request.*\nimport io.ktor.http.*\nimport io.ktor.serialization.kotlinx.json.*\nimport io.ktor.server.testing.*\nimport kotlinx.serialization.json.Json\nimport org.junit.jupiter.api.AfterAll\nimport org.junit.jupiter.api.BeforeAll\nimport org.junit.jupiter.api.Test\nimport org.koin.core.context.stopKoin\nimport kotlin.test.assertEquals\nimport kotlin.test.assertNotNull\n\nclass UserRoutesTest {\n\n    companion object {\n        @BeforeAll\n        @JvmStatic\n        fun setup() {\n            DatabaseFactory.init()\n        }\n\n        @AfterAll\n        @JvmStatic\n        fun teardown() {\n            stopKoin()\n        }\n    }\n\n    /**\n     * Helper function to register and login, returning the JWT token\n     */\n    private suspend fun ApplicationTestBuilder.registerAndLogin(\n        client: io.ktor.client.HttpClient,\n        email: String = \"test@example.com\",\n        password: String = \"SecurePass123!\",\n        fullName: String = \"Test User\"\n    ): String {\n        // Register\n        client.post(\"/api/auth/register\") {\n            contentType(ContentType.Application.Json)\n            setBody(RegisterRequest(email, password, fullName))\n        }\n\n        // Login\n        val loginResponse = client.post(\"/api/auth/login\") {\n            contentType(ContentType.Application.Json)\n            setBody(LoginRequest(email, password))\n        }\n\n        val apiResponse = loginResponse.body\u003cApiResponse\u003cLoginResponse\u003e\u003e()\n        return apiResponse.data?.token ?: throw Exception(\"No token received\")\n    }\n\n    @Test\n    fun `test get current user profile`() = testApplication {\n        application {\n            module()\n        }\n\n        val client = createClient {\n            install(ContentNegotiation) {\n                json(Json {\n                    ignoreUnknownKeys = true\n                })\n            }\n        }\n\n        // Get token\n        val token = registerAndLogin(client, email = \"profile@example.com\")\n\n        // Get profile\n        val response = client.get(\"/api/users/me\") {\n            header(HttpHeaders.Authorization, \"Bearer $token\")\n        }\n\n        // Assert\n        assertEquals(HttpStatusCode.OK, response.status)\n\n        val apiResponse = response.body\u003cApiResponse\u003cUser\u003e\u003e()\n        assertNotNull(apiResponse.data)\n        assertEquals(\"profile@example.com\", apiResponse.data?.email)\n    }\n\n    @Test\n    fun `test access protected route without token`() = testApplication {\n        application {\n            module()\n        }\n\n        val client = createClient {\n            install(ContentNegotiation) {\n                json(Json {\n                    ignoreUnknownKeys = true\n                })\n            }\n        }\n\n        // Try to access without token\n        val response = client.get(\"/api/users/me\")\n\n        // Assert unauthorized\n        assertEquals(HttpStatusCode.Unauthorized, response.status)\n    }\n\n    @Test\n    fun `test access protected route with invalid token`() = testApplication {\n        application {\n            module()\n        }\n\n        val client = createClient {\n            install(ContentNegotiation) {\n                json(Json {\n                    ignoreUnknownKeys = true\n                })\n            }\n        }\n\n        // Try with invalid token\n        val response = client.get(\"/api/users/me\") {\n            header(HttpHeaders.Authorization, \"Bearer invalid-token\")\n        }\n\n        // Assert unauthorized\n        assertEquals(HttpStatusCode.Unauthorized, response.status)\n    }\n\n    @Test\n    fun `test update user profile`() = testApplication {\n        application {\n            module()\n        }\n\n        val client = createClient {\n            install(ContentNegotiation) {\n                json(Json {\n                    ignoreUnknownKeys = true\n                })\n            }\n        }\n\n        val token = registerAndLogin(\n            client,\n            email = \"update@example.com\",\n            fullName = \"Original Name\"\n        )\n\n        // Update profile\n        val response = client.put(\"/api/users/me\") {\n            header(HttpHeaders.Authorization, \"Bearer $token\")\n            contentType(ContentType.Application.Json)\n            setBody(UpdateProfileRequest(fullName = \"Updated Name\"))\n        }\n\n        // Assert\n        assertEquals(HttpStatusCode.OK, response.status)\n\n        val apiResponse = response.body\u003cApiResponse\u003cUser\u003e\u003e()\n        assertEquals(\"Updated Name\", apiResponse.data?.fullName)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Testing with Koin",
                                "content":  "\n### Test Module Setup\n\n\n---\n\n",
                                "code":  "// src/test/kotlin/com/example/TestModule.kt\npackage com.example\n\nimport com.example.repositories.UserRepository\nimport com.example.services.AuthService\nimport com.example.services.UserService\nimport org.koin.dsl.module\n\nclass MockUserRepository : UserRepository {\n    // ... implementation\n}\n\nval testModule = module {\n    single\u003cUserRepository\u003e { MockUserRepository() }\n    single { UserService(get()) }\n    single { AuthService(get()) }\n}\n\n// Usage in tests\n@ExtendWith(KoinExtension::class)\n@KoinTest\nclass MyServiceTest {\n\n    @BeforeEach\n    fun setup() {\n        startKoin {\n            modules(testModule)\n        }\n    }\n\n    @AfterEach\n    fun teardown() {\n        stopKoin()\n    }\n\n    @Test\n    fun `test with Koin`() {\n        val userService by inject\u003cUserService\u003e()\n        // Test using injected service\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Test Coverage",
                                "content":  "\n### Generate Coverage Report\n\nAdd JaCoCo plugin to `build.gradle.kts`:\n\n\nRun tests with coverage:\n\nView report at: `build/reports/jacoco/test/html/index.html`\n\n---\n\n",
                                "code":  "./gradlew test jacocoTestReport",
                                "language":  "bash"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Best Practices",
                                "content":  "\n### 1. Test Naming Convention\n\n\nUse backticks for descriptive test names that read like sentences.\n\n### 2. AAA Pattern\n\n\n### 3. Test Isolation\n\n\nEach test should be independent and not affect others.\n\n### 4. Test Data Builders\n\n\n---\n\n",
                                "code":  "object TestDataBuilder {\n    fun createUser(\n        id: Int = 1,\n        email: String = \"test@example.com\",\n        fullName: String = \"Test User\",\n        role: String = \"USER\"\n    ) = User(\n        id = id,\n        email = email,\n        fullName = fullName,\n        role = role,\n        createdAt = \"2025-01-01T00:00:00\"\n    )\n\n    fun createRegisterRequest(\n        email: String = \"test@example.com\",\n        password: String = \"SecurePass123!\",\n        fullName: String = \"Test User\"\n    ) = RegisterRequest(email, password, fullName)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise: Complete Test Suite",
                                "content":  "\nWrite a complete test suite for the Post API.\n\n### Requirements\n\n1. **Unit Tests for PostService**:\n   - Test create post\n   - Test update post with ownership check\n   - Test delete post with ownership check\n   - Test get posts by user\n\n2. **Integration Tests for Post Routes**:\n   - Test POST /api/posts (create post)\n   - Test GET /api/posts (get all posts)\n   - Test PUT /api/posts/:id (update post - owner only)\n   - Test DELETE /api/posts/:id (delete post - owner only)\n   - Test authorization (user can\u0027t modify others\u0027 posts)\n   - Test admin can modify any post\n\n3. **Test Coverage**:\n   - Aim for 80%+ coverage on services\n   - Test all error paths (validation, not found, forbidden)\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution",
                                "content":  "\n\n---\n\n",
                                "code":  "// src/test/kotlin/com/example/services/PostServiceTest.kt\npackage com.example.services\n\nimport com.example.models.Post\nimport com.example.models.CreatePostRequest\nimport com.example.plugins.UserPrincipal\nimport com.example.repositories.PostRepository\nimport org.junit.jupiter.api.BeforeEach\nimport org.junit.jupiter.api.Test\nimport kotlin.test.*\n\nclass MockPostRepository : PostRepository {\n    private val posts = mutableMapOf\u003cInt, Post\u003e()\n    private var nextId = 1\n\n    override fun insert(title: String, content: String, authorId: Int): Int {\n        val id = nextId++\n        posts[id] = Post(\n            id = id,\n            title = title,\n            content = content,\n            authorId = authorId,\n            authorName = \"Test User\",\n            createdAt = \"2025-01-01T00:00:00\"\n        )\n        return id\n    }\n\n    override fun update(id: Int, title: String, content: String): Boolean {\n        val post = posts[id] ?: return false\n        posts[id] = post.copy(title = title, content = content)\n        return true\n    }\n\n    override fun delete(id: Int): Boolean {\n        return posts.remove(id) != null\n    }\n\n    override fun getById(id: Int): Post? = posts[id]\n\n    override fun getAll(): List\u003cPost\u003e = posts.values.toList()\n\n    fun reset() {\n        posts.clear()\n        nextId = 1\n    }\n}\n\nclass PostServiceTest {\n\n    private lateinit var mockPostRepository: MockPostRepository\n    private lateinit var postService: PostService\n\n    @BeforeEach\n    fun setup() {\n        mockPostRepository = MockPostRepository()\n        postService = PostService(mockPostRepository)\n    }\n\n    @Test\n    fun `should create post successfully`() {\n        // Arrange\n        val request = CreatePostRequest(\n            title = \"Test Post\",\n            content = \"Test content\"\n        )\n        val principal = UserPrincipal(1, \"test@example.com\", \"USER\")\n\n        // Act\n        val result = postService.createPost(request, principal)\n\n        // Assert\n        assertTrue(result.isSuccess)\n        val post = result.getOrNull()\n        assertNotNull(post)\n        assertEquals(\"Test Post\", post?.title)\n        assertEquals(1, post?.authorId)\n    }\n\n    @Test\n    fun `should allow owner to update post`() {\n        // Arrange\n        val principal = UserPrincipal(1, \"test@example.com\", \"USER\")\n        val createRequest = CreatePostRequest(\"Original\", \"Content\")\n        val postId = postService.createPost(createRequest, principal).getOrNull()?.id!!\n\n        // Act\n        val updateRequest = UpdatePostRequest(\"Updated\", \"New content\")\n        val result = postService.updatePost(postId, updateRequest, principal)\n\n        // Assert\n        assertTrue(result.isSuccess)\n        assertEquals(\"Updated\", result.getOrNull()?.title)\n    }\n\n    @Test\n    fun `should deny non-owner from updating post`() {\n        // Arrange\n        val owner = UserPrincipal(1, \"owner@example.com\", \"USER\")\n        val attacker = UserPrincipal(2, \"attacker@example.com\", \"USER\")\n\n        val createRequest = CreatePostRequest(\"Owner\u0027s Post\", \"Content\")\n        val postId = postService.createPost(createRequest, owner).getOrNull()?.id!!\n\n        // Act\n        val updateRequest = UpdatePostRequest(\"Hacked\", \"Bad content\")\n        val result = postService.updatePost(postId, updateRequest, attacker)\n\n        // Assert\n        assertTrue(result.isFailure)\n        val exception = result.exceptionOrNull()\n        assertTrue(exception is ForbiddenException)\n    }\n\n    @Test\n    fun `should allow admin to update any post`() {\n        // Arrange\n        val user = UserPrincipal(1, \"user@example.com\", \"USER\")\n        val admin = UserPrincipal(2, \"admin@example.com\", \"ADMIN\")\n\n        val createRequest = CreatePostRequest(\"User\u0027s Post\", \"Content\")\n        val postId = postService.createPost(createRequest, user).getOrNull()?.id!!\n\n        // Act\n        val updateRequest = UpdatePostRequest(\"Admin Edit\", \"Updated by admin\")\n        val result = postService.updatePost(postId, updateRequest, admin)\n\n        // Assert\n        assertTrue(result.isSuccess)\n        assertEquals(\"Admin Edit\", result.getOrNull()?.title)\n    }\n\n    @Test\n    fun `should delete post when owner requests`() {\n        // Arrange\n        val principal = UserPrincipal(1, \"test@example.com\", \"USER\")\n        val createRequest = CreatePostRequest(\"Delete Me\", \"Content\")\n        val postId = postService.createPost(createRequest, principal).getOrNull()?.id!!\n\n        // Act\n        val result = postService.deletePost(postId, principal)\n\n        // Assert\n        assertTrue(result.isSuccess)\n\n        // Verify post is gone\n        val getResult = postService.getPostById(postId)\n        assertTrue(getResult.isFailure)\n    }\n}\n\n// src/test/kotlin/com/example/routes/PostRoutesTest.kt\npackage com.example.routes\n\nimport com.example.database.DatabaseFactory\nimport com.example.models.*\nimport com.example.module\nimport io.ktor.client.call.*\nimport io.ktor.client.plugins.contentnegotiation.*\nimport io.ktor.client.request.*\nimport io.ktor.http.*\nimport io.ktor.serialization.kotlinx.json.*\nimport io.ktor.server.testing.*\nimport kotlinx.serialization.json.Json\nimport org.junit.jupiter.api.*\nimport org.koin.core.context.stopKoin\nimport kotlin.test.assertEquals\nimport kotlin.test.assertNotNull\nimport kotlin.test.assertTrue\n\n@TestInstance(TestInstance.Lifecycle.PER_CLASS)\nclass PostRoutesTest {\n\n    @BeforeAll\n    fun setup() {\n        DatabaseFactory.init()\n    }\n\n    @AfterAll\n    fun teardown() {\n        stopKoin()\n    }\n\n    private suspend fun ApplicationTestBuilder.getToken(\n        client: io.ktor.client.HttpClient,\n        email: String,\n        password: String = \"SecurePass123!\"\n    ): String {\n        // Register\n        client.post(\"/api/auth/register\") {\n            contentType(ContentType.Application.Json)\n            setBody(RegisterRequest(email, password, email.substringBefore(\"@\")))\n        }\n\n        // Login\n        val loginResponse = client.post(\"/api/auth/login\") {\n            contentType(ContentType.Application.Json)\n            setBody(LoginRequest(email, password))\n        }\n\n        return loginResponse.body\u003cApiResponse\u003cLoginResponse\u003e\u003e().data?.token!!\n    }\n\n    @Test\n    fun `test create post`() = testApplication {\n        application { module() }\n\n        val client = createClient {\n            install(ContentNegotiation) {\n                json(Json { ignoreUnknownKeys = true })\n            }\n        }\n\n        val token = getToken(client, \"post-creator@example.com\")\n\n        // Create post\n        val response = client.post(\"/api/posts\") {\n            header(HttpHeaders.Authorization, \"Bearer $token\")\n            contentType(ContentType.Application.Json)\n            setBody(CreatePostRequest(\"My Post\", \"Post content\"))\n        }\n\n        assertEquals(HttpStatusCode.Created, response.status)\n\n        val apiResponse = response.body\u003cApiResponse\u003cPost\u003e\u003e()\n        assertTrue(apiResponse.success)\n        assertEquals(\"My Post\", apiResponse.data?.title)\n    }\n\n    @Test\n    fun `test user cannot update others post`() = testApplication {\n        application { module() }\n\n        val client = createClient {\n            install(ContentNegotiation) {\n                json(Json { ignoreUnknownKeys = true })\n            }\n        }\n\n        // User A creates post\n        val tokenA = getToken(client, \"usera@example.com\")\n        val createResponse = client.post(\"/api/posts\") {\n            header(HttpHeaders.Authorization, \"Bearer $tokenA\")\n            contentType(ContentType.Application.Json)\n            setBody(CreatePostRequest(\"User A Post\", \"Content\"))\n        }\n        val postId = createResponse.body\u003cApiResponse\u003cPost\u003e\u003e().data?.id!!\n\n        // User B tries to update\n        val tokenB = getToken(client, \"userb@example.com\")\n        val updateResponse = client.put(\"/api/posts/$postId\") {\n            header(HttpHeaders.Authorization, \"Bearer $tokenB\")\n            contentType(ContentType.Application.Json)\n            setBody(UpdatePostRequest(\"Hacked\", \"Bad content\"))\n        }\n\n        assertEquals(HttpStatusCode.Forbidden, updateResponse.status)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why This Matters",
                                "content":  "\n### Real-World Impact\n\n**Companies With Good Tests**:\n- Deploy multiple times per day with confidence\n- Catch bugs before users do\n- Refactor fearlessly\n- Onboard new developers faster (tests are documentation)\n- Lower maintenance costs\n\n**Companies Without Tests**:\n- Manual testing takes hours\n- Fear of changing code\n- Bugs discovered in production\n- Slow feature development\n- High stress, long hours\n\n**Statistics**:\n- Bugs caught in testing cost 10x less than bugs in production\n- Well-tested code has 40-80% fewer production bugs\n- Test suites pay for themselves within 6 months\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Checkpoint Quiz",
                                "content":  "\n### Question 1\nWhat\u0027s the testing pyramid ratio for a backend API?\n\nA) 10% unit, 20% integration, 70% E2E\nB) 70% unit, 20% integration, 10% E2E\nC) Equal distribution (33% each)\nD) 100% integration tests only\n\n### Question 2\nWhat does the AAA pattern stand for in testing?\n\nA) Assert, Act, Arrange\nB) Arrange, Act, Assert\nC) Always Automate Assertions\nD) API, Authentication, Authorization\n\n### Question 3\nWhy use mock repositories in unit tests?\n\nA) They\u0027re faster than real databases\nB) They provide test isolation and don\u0027t require database setup\nC) They\u0027re required by JUnit\nD) They generate better test reports\n\n### Question 4\nWhat HTTP status code should a test expect when accessing a protected route without a token?\n\nA) 200 OK\nB) 400 Bad Request\nC) 401 Unauthorized\nD) 404 Not Found\n\n### Question 5\nWhat\u0027s the main benefit of high test coverage?\n\nA) Makes code run faster\nB) Reduces file size\nC) Increases confidence that code works correctly\nD) Automatically fixes bugs\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Quiz Answers",
                                "content":  "\n**Question 1: B) 70% unit, 20% integration, 10% E2E**\n\nThe testing pyramid recommends:\n- **Most**: Unit tests (fast, cheap, isolated)\n- **Some**: Integration tests (medium speed, test combinations)\n- **Few**: E2E tests (slow, expensive, brittle)\n\n---\n\n**Question 2: B) Arrange, Act, Assert**\n\n\n---\n\n**Question 3: B) They provide test isolation and don\u0027t require database setup**\n\nMock repositories:\n- No database needed (tests run in memory)\n- Fast execution (no I/O overhead)\n- Complete control (easily simulate edge cases)\n- Isolated (one test doesn\u0027t affect another)\n\n---\n\n**Question 4: C) 401 Unauthorized**\n\nHTTP status codes in authentication:\n- **401 Unauthorized**: Missing or invalid credentials/token\n- **403 Forbidden**: Authenticated but not authorized (valid token, insufficient permissions)\n\n---\n\n**Question 5: C) Increases confidence that code works correctly**\n\nTest coverage shows which code paths are tested:\n- 80%+ coverage = most code is verified\n- Low coverage = many code paths untested (likely bugs)\n- Confidence to refactor and add features\n\nNote: 100% coverage doesn\u0027t guarantee bug-free code, but it helps!\n\n---\n\n",
                                "code":  "@Test\nfun `example test`() {\n    // Arrange - Set up test data and dependencies\n    val user = createTestUser()\n\n    // Act - Perform the action being tested\n    val result = userService.deleteUser(user.id)\n\n    // Assert - Verify the outcome\n    assertTrue(result.isSuccess)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What You\u0027ve Learned",
                                "content":  "\n✅ Why automated testing is critical for maintainable codebases\n✅ The testing pyramid and when to use each test type\n✅ How to write unit tests for services with mock repositories\n✅ How to write integration tests for HTTP endpoints with testApplication\n✅ How to test protected routes requiring JWT authentication\n✅ How to test authorization (ownership and role-based access)\n✅ Best practices: AAA pattern, test isolation, descriptive names\n✅ How to measure test coverage with JaCoCo\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Next Steps",
                                "content":  "\nIn **Lesson 5.15: Part 5 Capstone Project**, you\u0027ll build a complete production-ready API from scratch using everything you\u0027ve learned:\n- Full authentication system (registration, login, JWT)\n- Role-based access control\n- Clean architecture (repositories, services, routes)\n- Dependency injection with Koin\n- Comprehensive test suite\n- Validation and error handling\n\nTime to put all your knowledge together into a real-world application!\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "intermediate",
    "title":  "Lesson 5.14: Testing Your API",
    "estimatedMinutes":  70
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
- Search for "kotlin Lesson 5.14: Testing Your API 2024 2025" to find latest practices
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
  "lessonId": "5.14",
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

