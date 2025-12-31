const fs = require('fs');
const path = require('path');

const coursePath = path.join(__dirname, '..', 'content', 'courses', 'kotlin', 'course.json');
const content = fs.readFileSync(coursePath, 'utf8');
const data = JSON.parse(content);

// Define warnings for remaining lessons (Modules 5-10)
const warnings = {
  // Module 5 - Backend Development with Ktor
  '5.1': {
    title: 'Common Mistakes in Backend Development',
    content: '### Mistake 1: Ignoring HTTP Status Codes\n\nUse appropriate status codes for different responses:\n\n### Mistake 2: Not Handling Errors\n\nAlways handle exceptions gracefully:\n\n### Mistake 3: Exposing Internal Errors\n\nDo not expose stack traces to clients.',
    code: '// ❌ Wrong - always 200\ncall.respond("Error occurred")\n\n// ✅ Correct - proper status codes\ncall.respond(HttpStatusCode.BadRequest, "Invalid input")\ncall.respond(HttpStatusCode.NotFound, "Resource not found")\ncall.respond(HttpStatusCode.InternalServerError, "Server error")\n\n// Error handling\ninstall(StatusPages) {\n    exception<NotFoundException> { call, cause ->\n        call.respond(HttpStatusCode.NotFound, cause.message ?: "Not found")\n    }\n    exception<Throwable> { call, cause ->\n        // Log internally, return generic message\n        logger.error("Error", cause)\n        call.respond(HttpStatusCode.InternalServerError, "An error occurred")\n    }\n}'
  },
  '5.2': {
    title: 'Common Mistakes Setting Up Ktor',
    content: '### Mistake 1: Missing Dependencies\n\nKtor is modular - add required features:\n\n### Mistake 2: Port Conflicts\n\nCheck if port is available:\n\n### Mistake 3: Wrong Engine Configuration\n\nConfigure the engine properly for your use case.',
    code: '// Required dependencies in build.gradle.kts\nimplementation("io.ktor:ktor-server-core:$ktor_version")\nimplementation("io.ktor:ktor-server-netty:$ktor_version")\nimplementation("io.ktor:ktor-server-content-negotiation:$ktor_version")\nimplementation("io.ktor:ktor-serialization-kotlinx-json:$ktor_version")\n\n// Check port availability\nfun main() {\n    embeddedServer(Netty, port = 8080) {\n        // Your application\n    }.start(wait = true)\n}\n\n// Engine configuration\nembeddedServer(Netty, port = 8080) {\n    connector {\n        port = 8080\n        host = "0.0.0.0"  // Listen on all interfaces\n    }\n}'
  },
  '5.3': {
    title: 'Common Mistakes with Ktor Routing',
    content: '### Mistake 1: Route Order Matters\n\nMore specific routes should come first:\n\n### Mistake 2: Missing Response\n\nAll routes must respond:\n\n### Mistake 3: Duplicate Routes\n\nAvoid defining the same route twice.',
    code: '// ❌ Wrong - /users/{id} matches before /users/me\nrouting {\n    get("/users/{id}") { /* ... */ }\n    get("/users/me") { /* Never reached! */ }\n}\n\n// ✅ Correct - specific first\nrouting {\n    get("/users/me") { /* ... */ }\n    get("/users/{id}") { /* ... */ }\n}\n\n// ❌ Wrong - no response\nget("/data") {\n    val data = fetchData()\n    // Missing call.respond()!\n}\n\n// ✅ Correct\nget("/data") {\n    val data = fetchData()\n    call.respond(data)\n}'
  },
  '5.4': {
    title: 'Common Mistakes with Request Parameters',
    content: '### Mistake 1: Not Validating Parameters\n\nAlways validate and sanitize input:\n\n### Mistake 2: Type Conversion Errors\n\nHandle conversion failures gracefully:\n\n### Mistake 3: Optional vs Required Parameters\n\nBe explicit about parameter requirements.',
    code: '// ❌ Wrong - no validation\nval id = call.parameters["id"]!!.toInt()  // Can crash!\n\n// ✅ Correct - validate\nval id = call.parameters["id"]?.toIntOrNull()\n    ?: return@get call.respond(HttpStatusCode.BadRequest, "Invalid ID")\n\n// Optional parameters\nval page = call.request.queryParameters["page"]?.toIntOrNull() ?: 1\nval limit = call.request.queryParameters["limit"]?.toIntOrNull() ?: 20\n\n// Body validation\nval user = call.receive<CreateUserRequest>()\nif (user.email.isBlank()) {\n    call.respond(HttpStatusCode.BadRequest, "Email required")\n    return@post\n}'
  },
  '5.5': {
    title: 'Common Mistakes with JSON Serialization',
    content: '### Mistake 1: Missing @Serializable\n\nAll serialized classes need the annotation:\n\n### Mistake 2: Null Handling\n\nHandle optional fields properly:\n\n### Mistake 3: Incorrect Content Type\n\nSet and check Content-Type headers.',
    code: '// ❌ Wrong - missing annotation\ndata class User(val name: String)\n\n// ✅ Correct\n@Serializable\ndata class User(val name: String)\n\n// Handle optional fields\n@Serializable\ndata class UpdateUser(\n    val name: String? = null,\n    val email: String? = null\n)\n\n// ContentNegotiation setup\ninstall(ContentNegotiation) {\n    json(Json {\n        prettyPrint = true\n        ignoreUnknownKeys = true\n        isLenient = true\n    })\n}\n\n// Respond with JSON\ncall.respond(HttpStatusCode.OK, UserResponse(user))'
  },
  '5.6': {
    title: 'Common Mistakes with Exposed Database Setup',
    content: '### Mistake 1: Connection Pool Issues\n\nConfigure connection pool properly:\n\n### Mistake 2: Missing Transactions\n\nDatabase operations need transactions:\n\n### Mistake 3: Schema Management\n\nDo not create tables in production automatically.',
    code: '// Configure HikariCP\nval config = HikariConfig().apply {\n    driverClassName = "org.postgresql.Driver"\n    jdbcUrl = "jdbc:postgresql://localhost:5432/mydb"\n    maximumPoolSize = 10\n    isAutoCommit = false\n    transactionIsolation = "TRANSACTION_REPEATABLE_READ"\n}\nval dataSource = HikariDataSource(config)\nDatabase.connect(dataSource)\n\n// ❌ Wrong - no transaction\nUsers.selectAll().toList()  // May not work!\n\n// ✅ Correct\ntransaction {\n    Users.selectAll().toList()\n}\n\n// Schema creation - only in development\nif (isDevelopment) {\n    transaction {\n        SchemaUtils.create(Users, Posts)\n    }\n}'
  },
  '5.7': {
    title: 'Common Mistakes with Database CRUD',
    content: '### Mistake 1: N+1 Query Problem\n\nEager load related data:\n\n### Mistake 2: Not Using Prepared Statements\n\nExposed uses them by default, but watch raw SQL:\n\n### Mistake 3: Ignoring Transaction Scope\n\nEntity access outside transaction fails.',
    code: '// ❌ Wrong - N+1 problem\nval users = transaction { User.all().toList() }\nusers.forEach { user ->\n    transaction { println(user.posts.count()) }  // Query per user!\n}\n\n// ✅ Correct - eager loading\nval users = transaction {\n    User.all().with(User::posts).toList()\n}\n\n// ❌ Wrong - SQL injection risk\nval query = "SELECT * FROM users WHERE name = \'$userInput\'"\n\n// ✅ Correct - parameterized\nUsers.select { Users.name eq userInput }\n\n// Transaction scope\nval user = transaction { User.findById(1) }\n// ❌ user.name  // May fail outside transaction\n\n// ✅ Copy data inside transaction\ndata class UserDTO(val id: Int, val name: String)\nval userDto = transaction {\n    User.findById(1)?.let { UserDTO(it.id.value, it.name) }\n}'
  },
  '5.8': {
    title: 'Common Mistakes with Repository Pattern',
    content: '### Mistake 1: Business Logic in Repository\n\nRepositories should only handle data access:\n\n### Mistake 2: Exposing Database Entities\n\nReturn DTOs, not ORM entities:\n\n### Mistake 3: Missing Interface Abstraction\n\nUse interfaces for testability.',
    code: '// ❌ Wrong - business logic in repository\nclass UserRepository {\n    fun createUser(email: String): User {\n        if (!email.contains("@")) throw Exception()  // Business logic!\n        // ...\n    }\n}\n\n// ✅ Correct - service handles logic\nclass UserService(private val repo: UserRepository) {\n    fun createUser(email: String): User {\n        validateEmail(email)  // Service responsibility\n        return repo.save(User(email))\n    }\n}\n\n// Use interfaces\ninterface UserRepository {\n    fun findById(id: Int): UserDTO?\n    fun save(user: CreateUserDTO): UserDTO\n}\n\nclass UserRepositoryImpl : UserRepository {\n    override fun findById(id: Int) = transaction {\n        User.findById(id)?.toDTO()\n    }\n}'
  },
  '5.9': {
    title: 'Common Mistakes with Validation and Errors',
    content: '### Mistake 1: Validating Too Late\n\nValidate at the boundary:\n\n### Mistake 2: Inconsistent Error Responses\n\nUse a consistent error format:\n\n### Mistake 3: Not Logging Errors\n\nLog errors for debugging.',
    code: '// Validate at entry point\npost("/users") {\n    val request = call.receive<CreateUserRequest>()\n    val errors = validate(request)\n    if (errors.isNotEmpty()) {\n        call.respond(HttpStatusCode.BadRequest, ValidationError(errors))\n        return@post\n    }\n    // Process valid request\n}\n\n// Consistent error format\n@Serializable\ndata class ApiError(\n    val code: String,\n    val message: String,\n    val details: Map<String, String>? = null\n)\n\n// Error logging\ninstall(StatusPages) {\n    exception<Throwable> { call, cause ->\n        logger.error("Unhandled exception", cause)\n        call.respond(\n            HttpStatusCode.InternalServerError,\n            ApiError("INTERNAL_ERROR", "An unexpected error occurred")\n        )\n    }\n}'
  },
  '5.10': {
    title: 'Common Mistakes with User Registration',
    content: '### Mistake 1: Storing Plain Text Passwords\n\nAlways hash passwords:\n\n### Mistake 2: Weak Password Requirements\n\nEnforce password complexity:\n\n### Mistake 3: Duplicate Email Handling\n\nHandle existing emails gracefully.',
    code: '// ❌ Wrong - storing plain password\nUser.new {\n    password = request.password  // Insecure!\n}\n\n// ✅ Correct - hash with BCrypt\nimport org.mindrot.jbcrypt.BCrypt\n\nval hashedPassword = BCrypt.hashpw(request.password, BCrypt.gensalt())\nUser.new {\n    password = hashedPassword\n}\n\n// Password validation\nfun validatePassword(password: String): List<String> {\n    val errors = mutableListOf<String>()\n    if (password.length < 8) errors.add("Minimum 8 characters")\n    if (!password.any { it.isDigit() }) errors.add("Must contain a number")\n    if (!password.any { it.isUpperCase() }) errors.add("Must contain uppercase")\n    return errors\n}\n\n// Handle duplicate email\nif (userRepo.findByEmail(email) != null) {\n    call.respond(HttpStatusCode.Conflict, "Email already registered")\n    return@post\n}'
  },
  '5.11': {
    title: 'Common Mistakes with JWT Authentication',
    content: '### Mistake 1: Storing Secrets in Code\n\nUse environment variables:\n\n### Mistake 2: Not Setting Expiration\n\nTokens should expire:\n\n### Mistake 3: Including Sensitive Data\n\nDo not put secrets in JWT payload.',
    code: '// ❌ Wrong - secret in code\nval secret = "my-super-secret-key"\n\n// ✅ Correct - from environment\nval secret = System.getenv("JWT_SECRET")\n    ?: throw IllegalStateException("JWT_SECRET not set")\n\n// Set expiration\nfun generateToken(userId: Int): String {\n    return JWT.create()\n        .withClaim("userId", userId)\n        .withExpiresAt(Date(System.currentTimeMillis() + 3600000))  // 1 hour\n        .sign(Algorithm.HMAC256(secret))\n}\n\n// ❌ Wrong - sensitive data in token\nJWT.create()\n    .withClaim("password", user.password)  // Never!\n    .withClaim("creditCard", user.cc)  // Never!\n\n// ✅ Correct - minimal claims\nJWT.create()\n    .withClaim("userId", user.id)\n    .withClaim("role", user.role)'
  },
  '5.12': {
    title: 'Common Mistakes Protecting Routes',
    content: '### Mistake 1: Checking Auth in Every Route\n\nUse authentication plugins:\n\n### Mistake 2: Not Refreshing Tokens\n\nImplement token refresh:\n\n### Mistake 3: Missing Role Checks\n\nAuthentication is not authorization.',
    code: '// ❌ Wrong - manual check everywhere\nget("/profile") {\n    val token = call.request.headers["Authorization"]\n    if (token == null) { /* ... */ }\n    // Verify manually...\n}\n\n// ✅ Correct - use authentication plugin\ninstall(Authentication) {\n    jwt("auth-jwt") {\n        verifier(JWT.require(Algorithm.HMAC256(secret)).build())\n        validate { credential ->\n            JWTPrincipal(credential.payload)\n        }\n    }\n}\n\nauthenticate("auth-jwt") {\n    get("/profile") {\n        val principal = call.principal<JWTPrincipal>()!!\n        val userId = principal.payload.getClaim("userId").asInt()\n        // Authenticated!\n    }\n}\n\n// Authorization check\nfun Route.adminOnly(build: Route.() -> Unit) {\n    authenticate("auth-jwt") {\n        intercept(ApplicationCallPipeline.Call) {\n            val role = call.principal<JWTPrincipal>()?.payload?.getClaim("role")?.asString()\n            if (role != "admin") {\n                call.respond(HttpStatusCode.Forbidden)\n                finish()\n            }\n        }\n        build()\n    }\n}'
  },
  '5.13': {
    title: 'Common Mistakes with Dependency Injection',
    content: '### Mistake 1: Creating Dependencies Manually\n\nUse DI container:\n\n### Mistake 2: Circular Dependencies\n\nDesign to avoid cycles:\n\n### Mistake 3: Wrong Scope\n\nChoose singleton vs factory appropriately.',
    code: '// ❌ Wrong - manual creation\nclass UserService {\n    private val repo = UserRepositoryImpl()\n    private val emailService = EmailServiceImpl()\n}\n\n// ✅ Correct - Koin injection\nval appModule = module {\n    single<UserRepository> { UserRepositoryImpl() }\n    single<EmailService> { EmailServiceImpl() }\n    single { UserService(get(), get()) }  // Dependencies injected\n}\n\n// In route\nget("/users") {\n    val userService by inject<UserService>()\n    call.respond(userService.getAllUsers())\n}\n\n// Scope examples\nval appModule = module {\n    single { Database.connect(...) }  // One instance\n    factory { RequestLogger() }  // New instance each time\n    scope<Session> {\n        scoped { SessionData() }  // Per session\n    }\n}'
  },
  '5.14': {
    title: 'Common Mistakes Testing APIs',
    content: '### Mistake 1: Testing Against Production\n\nUse test database:\n\n### Mistake 2: Not Testing Error Cases\n\nTest unhappy paths:\n\n### Mistake 3: Flaky Tests\n\nEnsure tests are deterministic.',
    code: '// Use test configuration\n@BeforeTest\nfun setup() {\n    Database.connect(\n        "jdbc:h2:mem:test;DB_CLOSE_DELAY=-1",\n        driver = "org.h2.Driver"\n    )\n    transaction { SchemaUtils.create(Users) }\n}\n\n// Test with testApplication\n@Test\nfun testGetUser() = testApplication {\n    application { configureRouting() }\n    \n    // Test success case\n    client.get("/users/1").apply {\n        assertEquals(HttpStatusCode.OK, status)\n    }\n    \n    // Test error cases\n    client.get("/users/999").apply {\n        assertEquals(HttpStatusCode.NotFound, status)\n    }\n    \n    client.get("/users/invalid").apply {\n        assertEquals(HttpStatusCode.BadRequest, status)\n    }\n}\n\n// Clean up after tests\n@AfterTest\nfun cleanup() {\n    transaction { Users.deleteAll() }\n}'
  },
  '5.15': {
    title: 'Common Mistakes in API Projects',
    content: '### Mistake 1: Missing Documentation\n\nDocument your API endpoints:\n\n### Mistake 2: Inconsistent Naming\n\nFollow REST conventions:\n\n### Mistake 3: Versioning Issues\n\nPlan for API evolution.',
    code: '// Document with OpenAPI/Swagger\ninstall(OpenAPI) {\n    // Configuration\n}\n\n// REST naming conventions\n// ❌ Wrong\nGET /getUsers\nPOST /createUser\nDELETE /removeUser/1\n\n// ✅ Correct\nGET /users\nPOST /users\nDELETE /users/1\nGET /users/1/posts  // Nested resources\n\n// API versioning\nroute("/api/v1") {\n    route("/users") {\n        get { /* v1 implementation */ }\n    }\n}\n\nroute("/api/v2") {\n    route("/users") {\n        get { /* v2 with breaking changes */ }\n    }\n}'
  },
  // Module 6 - Compose Multiplatform
  '6.1': {
    title: 'Common Mistakes with Compose Multiplatform',
    content: '### Mistake 1: Platform-Specific Code in Common\n\nUse expect/actual pattern:\n\n### Mistake 2: Ignoring Platform Differences\n\nTest on all target platforms:\n\n### Mistake 3: Wrong Module Dependencies\n\nConfigure source sets correctly.',
    code: '// ❌ Wrong - platform code in common\n// In commonMain\nandroid.widget.Toast.makeText(...)  // Error!\n\n// ✅ Correct - expect/actual\n// commonMain\nexpect fun showMessage(message: String)\n\n// androidMain\nactual fun showMessage(message: String) {\n    Toast.makeText(context, message, Toast.LENGTH_SHORT).show()\n}\n\n// iosMain\nactual fun showMessage(message: String) {\n    // iOS implementation\n}\n\n// Source set configuration\nkotlin {\n    sourceSets {\n        commonMain.dependencies {\n            implementation(compose.runtime)\n            implementation(compose.ui)\n        }\n        androidMain.dependencies {\n            implementation(libs.android.specific)\n        }\n    }\n}'
  },
  '6.2': {
    title: 'Common Mistakes with Compose UI',
    content: '### Mistake 1: Not Using Modifiers\n\nModifiers define layout behavior:\n\n### Mistake 2: Wrong Composable Scope\n\nSome functions are only available in certain scopes:\n\n### Mistake 3: Forgetting Keys in Lists\n\nKeys help Compose track items.',
    code: '// ❌ Wrong - no modifier\nText("Hello")\n\n// ✅ Correct - with modifiers\nText(\n    text = "Hello",\n    modifier = Modifier\n        .padding(16.dp)\n        .fillMaxWidth()\n)\n\n// Scope-specific functions\nRow {\n    Text(modifier = Modifier.weight(1f))  // weight() only in Row/Column\n}\n\n// ❌ Wrong - no keys\nLazyColumn {\n    items(users) { user ->\n        UserCard(user)\n    }\n}\n\n// ✅ Correct - with keys\nLazyColumn {\n    items(users, key = { it.id }) { user ->\n        UserCard(user)\n    }\n}'
  },
  '6.3': {
    title: 'Common Mistakes with Layouts',
    content: '### Mistake 1: Nested Scrollable Containers\n\nAvoid nested scrollables without fixed height:\n\n### Mistake 2: Ignoring Constraints\n\nUnderstand how constraints propagate:\n\n### Mistake 3: Inefficient Recomposition\n\nStructure composables to minimize recomposition.',
    code: '// ❌ Wrong - nested scroll\nColumn(Modifier.verticalScroll(rememberScrollState())) {\n    LazyColumn { /* Crash or error! */ }\n}\n\n// ✅ Correct - fixed height or use one scroll\nLazyColumn {\n    item { Header() }\n    items(data) { Item(it) }\n}\n\n// Understand constraints\nBox(Modifier.fillMaxSize()) {\n    // Child gets max constraints from parent\n    Text(Modifier.fillMaxWidth())  // Uses parent width\n}\n\n// Minimize recomposition\n// ❌ Wrong - recomposes entire list\nvar selected by remember { mutableStateOf(-1) }\nColumn {\n    items.forEachIndexed { i, item ->\n        ItemRow(item, i == selected) { selected = i }\n    }\n}\n\n// ✅ Correct - only changed items recompose\nLazyColumn {\n    items(items, key = { it.id }) { item ->\n        ItemRow(item)\n    }\n}'
  },
  '6.4': {
    title: 'Common Mistakes with State Management',
    content: '### Mistake 1: Stateful Composables\n\nHoist state for reusability:\n\n### Mistake 2: Wrong remember Scope\n\nremember survives recomposition, not navigation:\n\n### Mistake 3: Mutable State in Data Classes\n\nUse immutable state with copy().',
    code: '// ❌ Wrong - stateful composable\n@Composable\nfun Counter() {\n    var count by remember { mutableStateOf(0) }  // Hard to test/reuse\n    Text("$count")\n}\n\n// ✅ Correct - state hoisting\n@Composable\nfun Counter(count: Int, onIncrement: () -> Unit) {\n    Button(onClick = onIncrement) {\n        Text("$count")\n    }\n}\n\n// remember vs rememberSaveable\nvar text by remember { mutableStateOf("") }  // Lost on rotation\nvar text by rememberSaveable { mutableStateOf("") }  // Survives rotation\n\n// Immutable state\ndata class UiState(val items: List<Item>, val loading: Boolean)\n\n// ❌ Wrong - mutating\nstate.items.add(newItem)\n\n// ✅ Correct - copy\nstate = state.copy(items = state.items + newItem)'
  },
  '6.5': {
    title: 'Common Mistakes with Navigation',
    content: '### Mistake 1: Hardcoded Routes\n\nUse constants or sealed classes:\n\n### Mistake 2: Complex Argument Passing\n\nKeep navigation arguments simple:\n\n### Mistake 3: Back Stack Issues\n\nUnderstand navigation back stack.',
    code: '// ❌ Wrong - hardcoded strings\nnavController.navigate("user/123")\nnavController.navigate("settings")\n\n// ✅ Correct - type-safe routes\nsealed class Screen(val route: String) {\n    object Home : Screen("home")\n    data class User(val id: Int) : Screen("user/$id")\n    object Settings : Screen("settings")\n}\n\n// Keep arguments simple\n// ❌ Wrong - passing complex objects\nnavController.navigate("user/${Json.encode(user)}")  // Fragile!\n\n// ✅ Correct - pass ID, fetch data\nnavController.navigate("user/${user.id}")\n// In destination, fetch user by ID\n\n// Back stack management\nnavController.navigate("home") {\n    popUpTo("login") { inclusive = true }  // Clear login from stack\n    launchSingleTop = true\n}'
  },
  '6.6': {
    title: 'Common Mistakes with Networking',
    content: '### Mistake 1: Network on Main Thread\n\nAlways use background dispatcher:\n\n### Mistake 2: Not Handling Errors\n\nNetwork calls can fail:\n\n### Mistake 3: Ignoring Cancellation\n\nClean up when leaving screen.',
    code: '// ❌ Wrong - network on main thread\n@Composable\nfun UserList() {\n    val users = api.getUsers()  // Blocks UI!\n}\n\n// ✅ Correct - suspend function in ViewModel\nclass UserViewModel : ViewModel() {\n    val users = MutableStateFlow<List<User>>(emptyList())\n    \n    fun loadUsers() {\n        viewModelScope.launch {\n            try {\n                users.value = api.getUsers()\n            } catch (e: Exception) {\n                // Handle error\n            }\n        }\n    }\n}\n\n// Error handling\nsealed class Result<T> {\n    data class Success<T>(val data: T) : Result<T>()\n    data class Error<T>(val message: String) : Result<T>()\n    class Loading<T> : Result<T>()\n}\n\n// Cancel on leave\nLaunchedEffect(Unit) {\n    // Cancelled when composable leaves\n    loadData()\n}'
  },
  '6.7': {
    title: 'Common Mistakes with Local Storage',
    content: '### Mistake 1: Blocking Main Thread\n\nDatabase operations should be async:\n\n### Mistake 2: Not Observing Changes\n\nUse Flow for reactive updates:\n\n### Mistake 3: Platform Differences\n\nStorage APIs differ per platform.',
    code: '// ❌ Wrong - blocking read\nfun getUser(): User {\n    return database.userDao().getUser()  // Blocks!\n}\n\n// ✅ Correct - suspend function\nsuspend fun getUser(): User {\n    return withContext(Dispatchers.IO) {\n        database.userDao().getUser()\n    }\n}\n\n// Observe with Flow\nfun observeUsers(): Flow<List<User>> =\n    database.userDao().getAllUsers()  // Returns Flow\n\n// In ViewModel\nval users = database.observeUsers()\n    .stateIn(viewModelScope, SharingStarted.Lazily, emptyList())\n\n// Platform-specific storage\n// commonMain\nexpect class Storage {\n    fun getString(key: String): String?\n    fun putString(key: String, value: String)\n}\n\n// androidMain - SharedPreferences\n// iosMain - NSUserDefaults'
  },
  '6.8': {
    title: 'Common Mistakes with MVVM',
    content: '### Mistake 1: Logic in Composables\n\nMove logic to ViewModel:\n\n### Mistake 2: Not Exposing Immutable State\n\nExpose StateFlow, not MutableStateFlow:\n\n### Mistake 3: Heavy ViewModel Init\n\nDefer work until needed.',
    code: '// ❌ Wrong - logic in composable\n@Composable\nfun UserScreen() {\n    var users by remember { mutableStateOf(listOf()) }\n    LaunchedEffect(Unit) {\n        users = api.getUsers().filter { it.active }  // Business logic!\n    }\n}\n\n// ✅ Correct - logic in ViewModel\nclass UserViewModel : ViewModel() {\n    private val _users = MutableStateFlow<List<User>>(emptyList())\n    val users: StateFlow<List<User>> = _users.asStateFlow()  // Immutable\n    \n    fun loadUsers() {\n        viewModelScope.launch {\n            _users.value = repository.getActiveUsers()\n        }\n    }\n}\n\n// In composable\n@Composable\nfun UserScreen(viewModel: UserViewModel) {\n    val users by viewModel.users.collectAsState()\n    LaunchedEffect(Unit) { viewModel.loadUsers() }\n}'
  },
  '6.9': {
    title: 'Common Mistakes with Animations',
    content: '### Mistake 1: Not Using animate*AsState\n\nUse built-in animation functions:\n\n### Mistake 2: Animating Non-Stable Values\n\nAnimate with stable targets:\n\n### Mistake 3: Too Many Animations\n\nKeep animations purposeful.',
    code: '// ❌ Wrong - manual animation\nvar size by remember { mutableStateOf(100.dp) }\nLaunchedEffect(expanded) {\n    // Manual size changes\n}\n\n// ✅ Correct - animate*AsState\nval size by animateDpAsState(\n    targetValue = if (expanded) 200.dp else 100.dp,\n    animationSpec = tween(300)\n)\nBox(Modifier.size(size))\n\n// AnimatedVisibility\nAnimatedVisibility(\n    visible = showContent,\n    enter = fadeIn() + slideInVertically(),\n    exit = fadeOut()\n) {\n    Content()\n}\n\n// AnimatedContent for state changes\nAnimatedContent(\n    targetState = currentScreen,\n    transitionSpec = { slideInHorizontally() with slideOutHorizontally() }\n) { screen ->\n    when (screen) {\n        Screen.Home -> HomeContent()\n        Screen.Settings -> SettingsContent()\n    }\n}'
  },
  '6.10': {
    title: 'Common Mistakes in Compose App Projects',
    content: '### Mistake 1: Tight Coupling\n\nUse dependency injection:\n\n### Mistake 2: God ViewModels\n\nSplit by feature:\n\n### Mistake 3: Not Testing Composables\n\nWrite UI tests.',
    code: '// ❌ Wrong - tightly coupled\nclass TaskScreen {\n    val viewModel = TaskViewModel(TaskRepository(Database()))\n}\n\n// ✅ Correct - dependency injection\n@Composable\nfun TaskScreen(viewModel: TaskViewModel = koinViewModel()) {\n    // ...\n}\n\n// Split ViewModels by feature\nclass TaskListViewModel : ViewModel() { /* List operations */ }\nclass TaskDetailViewModel : ViewModel() { /* Single task */ }\nclass TaskCreateViewModel : ViewModel() { /* Creating tasks */ }\n\n// UI testing\nclass TaskScreenTest {\n    @get:Rule\n    val composeRule = createComposeRule()\n    \n    @Test\n    fun showsTaskList() {\n        composeRule.setContent {\n            TaskScreen(fakeViewModel)\n        }\n        composeRule.onNodeWithText("My Task").assertIsDisplayed()\n    }\n}'
  },
  // Module 7 - Advanced KMP
  '7.1': {
    title: 'Common Mistakes with Advanced KMP',
    content: '### Mistake 1: Putting Too Much in Common\n\nSome things belong in platform code:\n\n### Mistake 2: expect/actual Overuse\n\nNot everything needs abstraction:\n\n### Mistake 3: Build Configuration Issues\n\nConfigure targets correctly.',
    code: '// When to use platform code\n// ✅ Common: Business logic, models, API clients\n// ✅ Platform: UI, platform APIs, hardware access\n\n// ❌ Wrong - abstract everything\nexpect fun add(a: Int, b: Int): Int  // Unnecessary!\n\n// ✅ Correct - only platform-specific\nexpect fun getDeviceId(): String  // Actually differs\n\n// Gradle configuration\nkotlin {\n    androidTarget()\n    iosX64()\n    iosArm64()\n    iosSimulatorArm64()\n    \n    sourceSets {\n        commonMain.dependencies {\n            implementation(libs.kotlinx.coroutines)\n        }\n        iosMain.dependencies {\n            // iOS-specific deps\n        }\n    }\n}'
  },
  '7.2': {
    title: 'Common Mistakes with Testing Strategies',
    content: '### Mistake 1: Not Testing Common Code\n\nCommon code runs on all platforms:\n\n### Mistake 2: Mocking Platform Code\n\nUse fakes for platform dependencies:\n\n### Mistake 3: Ignoring Platform Tests\n\nTest on each platform.',
    code: '// Test common code in commonTest\nclass UserRepositoryTest {\n    @Test\n    fun testGetUsers() {\n        val repo = UserRepository(FakeApi())\n        val users = repo.getUsers()\n        assertEquals(2, users.size)\n    }\n}\n\n// Fake for platform dependency\n// commonMain\ninterface Platform {\n    val name: String\n}\n\n// commonTest\nclass FakePlatform : Platform {\n    override val name = "Test"\n}\n\n// Run tests on each platform\n// Gradle: allTests task runs on all targets\n// Or: iosTest, androidTest, jvmTest'
  },
  '7.3': {
    title: 'Common Mistakes with Performance',
    content: '### Mistake 1: Ignoring Memory in iOS\n\niOS has stricter memory management:\n\n### Mistake 2: Large Inline Classes\n\nInlining large functions hurts performance:\n\n### Mistake 3: Not Profiling\n\nMeasure before optimizing.',
    code: '// iOS memory management\n// Use weak references for callbacks\nclass NetworkManager {\n    private val callbacks = WeakHashMap<Callback, Boolean>()\n}\n\n// Avoid large inline functions\n// ❌ Wrong - large inline\ninline fun processData(data: List<Item>): List<Result> {\n    // 100 lines of code...\n}\n\n// ✅ Correct - inline only small functions\ninline fun <T> measureTime(block: () -> T): T {\n    val start = System.currentTimeMillis()\n    val result = block()\n    println("Took ${System.currentTimeMillis() - start}ms")\n    return result\n}\n\n// Profile first\nfun processItems(items: List<Item>): List<Result> {\n    return measureTime {\n        items.map { transform(it) }  // Measure actual bottleneck\n    }\n}'
  },
  '7.4': {
    title: 'Common Mistakes with Security',
    content: '### Mistake 1: Hardcoded Secrets\n\nNever commit secrets:\n\n### Mistake 2: Insecure Storage\n\nUse platform secure storage:\n\n### Mistake 3: Logging Sensitive Data\n\nDo not log credentials.',
    code: '// ❌ Wrong - secret in code\nconst val API_KEY = "sk-abc123"  // Committed to git!\n\n// ✅ Correct - environment/secure storage\nval apiKey = BuildConfig.API_KEY  // From local.properties\n\n// Secure storage per platform\n// commonMain\nexpect class SecureStorage {\n    fun store(key: String, value: String)\n    fun retrieve(key: String): String?\n}\n\n// androidMain - EncryptedSharedPreferences\n// iosMain - Keychain\n\n// ❌ Wrong - logging credentials\nlogger.debug("Login: user=$email, pass=$password")\n\n// ✅ Correct - no sensitive data\nlogger.debug("Login attempt for user: ${email.hashCode()}")'
  },
  '7.5': {
    title: 'Common Mistakes with CI/CD',
    content: '### Mistake 1: Long Build Times\n\nOptimize builds with caching:\n\n### Mistake 2: Not Testing All Platforms\n\nCI should test each target:\n\n### Mistake 3: Missing Signing Configuration\n\nConfigure signing for release builds.',
    code: '// GitHub Actions example\nname: Build\non: [push, pull_request]\njobs:\n  build:\n    strategy:\n      matrix:\n        os: [ubuntu-latest, macos-latest]\n    runs-on: ${{ matrix.os }}\n    steps:\n      - uses: actions/checkout@v3\n      - uses: actions/setup-java@v3\n      - name: Cache Gradle\n        uses: actions/cache@v3\n        with:\n          path: ~/.gradle/caches\n          key: gradle-${{ hashFiles("**/*.gradle*") }}\n      - run: ./gradlew build\n\n// Signing configuration\nandroid {\n    signingConfigs {\n        release {\n            storeFile = file(System.getenv("KEYSTORE_PATH"))\n            storePassword = System.getenv("KEYSTORE_PASSWORD")\n            keyAlias = System.getenv("KEY_ALIAS")\n            keyPassword = System.getenv("KEY_PASSWORD")\n        }\n    }\n}'
  },
  '7.6': {
    title: 'Common Mistakes with Cloud Deployment',
    content: '### Mistake 1: No Health Checks\n\nImplement health endpoints:\n\n### Mistake 2: Missing Environment Config\n\nConfigure for each environment:\n\n### Mistake 3: No Graceful Shutdown\n\nHandle termination signals.',
    code: '// Health check endpoint\nget("/health") {\n    val dbHealthy = try {\n        transaction { exec("SELECT 1") }\n        true\n    } catch (e: Exception) { false }\n    \n    if (dbHealthy) {\n        call.respond(mapOf("status" to "healthy"))\n    } else {\n        call.respond(HttpStatusCode.ServiceUnavailable, mapOf("status" to "unhealthy"))\n    }\n}\n\n// Environment configuration\nval config = when (System.getenv("ENV")) {\n    "production" -> ProductionConfig\n    "staging" -> StagingConfig\n    else -> DevelopmentConfig\n}\n\n// Graceful shutdown\nfun main() {\n    val server = embeddedServer(Netty, port = 8080) { /* ... */ }\n    Runtime.getRuntime().addShutdownHook(Thread {\n        server.stop(1000, 5000)  // Grace period\n    })\n    server.start(wait = true)\n}'
  },
  '7.7': {
    title: 'Common Mistakes with Monitoring',
    content: '### Mistake 1: No Logging Structure\n\nUse structured logging:\n\n### Mistake 2: Missing Metrics\n\nTrack important metrics:\n\n### Mistake 3: No Alerting\n\nSet up alerts for critical issues.',
    code: '// Structured logging\nlogger.info {\n    mapOf(\n        "event" to "user_login",\n        "userId" to userId,\n        "duration" to duration\n    )\n}\n\n// Metrics with Micrometer\nval requestCounter = Counter.builder("http_requests_total")\n    .tag("path", path)\n    .tag("method", method)\n    .register(registry)\n\nrequestCounter.increment()\n\n// Custom metrics\nval loginTimer = Timer.builder("login_duration")\n    .register(registry)\n\nloginTimer.record {\n    authenticateUser()\n}\n\n// Health check with details\nget("/health/detailed") {\n    call.respond(mapOf(\n        "status" to "healthy",\n        "database" to checkDb(),\n        "cache" to checkCache(),\n        "uptime" to uptime()\n    ))\n}'
  },
  '7.8': {
    title: 'Common Mistakes in Full-Stack Projects',
    content: '### Mistake 1: Tight API Coupling\n\nVersion and document APIs:\n\n### Mistake 2: No Error Boundaries\n\nHandle errors at each layer:\n\n### Mistake 3: Poor Code Organization\n\nMaintain clear module boundaries.',
    code: '// API versioning\nroute("/api/v1") { v1Routes() }\nroute("/api/v2") { v2Routes() }\n\n// Shared DTOs between client and server\n// shared/src/commonMain\n@Serializable\ndata class UserDto(\n    val id: Int,\n    val name: String\n)\n\n// Error handling layers\n// Repository - throws domain exceptions\n// Service - catches and transforms\n// Controller - returns HTTP responses\nclass UserService(private val repo: UserRepository) {\n    fun getUser(id: Int): Result<User> = runCatching {\n        repo.findById(id) ?: throw NotFoundException("User $id not found")\n    }\n}\n\n// Clear module structure\n// :shared - Common code\n// :backend - Ktor server\n// :android - Android app\n// :ios - iOS app\n// :web - Web frontend'
  },
  // Module 9 - Error Handling
  '9.6': {
    title: 'Common Mistakes with Error Handling',
    content: '### Mistake 1: Using Exceptions for Control Flow\n\nExceptions are for exceptional cases:\n\n### Mistake 2: Catching Too Broadly\n\nCatch specific exceptions:\n\n### Mistake 3: Ignoring Errors\n\nAlways handle or propagate errors.',
    code: '// ❌ Wrong - exception for control flow\nfun findUser(id: Int): User {\n    try {\n        return users.first { it.id == id }\n    } catch (e: NoSuchElementException) {\n        return defaultUser\n    }\n}\n\n// ✅ Correct - use nullable\nfun findUser(id: Int): User? =\n    users.firstOrNull { it.id == id }\n\n// ❌ Wrong - too broad\ntry {\n    doSomething()\n} catch (e: Exception) {\n    // Catches EVERYTHING including programming errors\n}\n\n// ✅ Correct - specific\ntry {\n    doSomething()\n} catch (e: IOException) {\n    // Handle IO specifically\n} catch (e: TimeoutException) {\n    // Handle timeout\n}\n\n// Result type for error handling\nfun parseNumber(s: String): Result<Int> =\n    runCatching { s.toInt() }\n\nparseNumber("42").getOrElse { 0 }'
  },
  // Module 10 - Kotlin 2.0
  '10.1': {
    title: 'Common Mistakes with K2 Compiler',
    content: '### Mistake 1: Expecting API Changes\n\nK2 is a compiler change, not language change:\n\n### Mistake 2: Not Benefiting from Improvements\n\nLeverage new smart cast capabilities:\n\n### Mistake 3: Ignoring Warnings\n\nK2 has better error detection.',
    code: '// K2 is a compiler upgrade - syntax unchanged\n// Your existing code works!\nfun greet(name: String) = println("Hello, $name")\n\n// K2 smart casts are smarter\nclass Container(val value: String?)\nfun process(c: Container) {\n    if (c.value != null) {\n        // K2 smart casts properties!\n        println(c.value.length)\n    }\n}\n\n// K2 catches more errors\n// Pay attention to new warnings - they often indicate real bugs\n\n// Enable K2 in gradle.properties\n// kotlin.experimental.tryK2=true'
  },
  '10.2': {
    title: 'Common Mistakes Migrating to K2',
    content: '### Mistake 1: Rushing Migration\n\nTest thoroughly before production:\n\n### Mistake 2: Ignoring Dependencies\n\nCheck library compatibility:\n\n### Mistake 3: Not Reading Release Notes\n\nUnderstand what changed.',
    code: '// Gradual migration approach\n// 1. Enable K2 in development\n// 2. Fix any new warnings\n// 3. Run full test suite\n// 4. Enable in CI\n// 5. Deploy to staging\n// 6. Deploy to production\n\n// Check library compatibility\n// Some annotation processors may need updates\n// kapt -> KSP migration recommended\n\n// gradle.properties\nkotlin.experimental.tryK2=true\nkapt.use.worker.api=true\n\n// Known differences to watch for:\n// - Stricter type inference\n// - Better null analysis\n// - Improved error messages'
  },
  '10.3': {
    title: 'Common Mistakes with KSP',
    content: '### Mistake 1: Still Using kapt\n\nKSP is faster - migrate where possible:\n\n### Mistake 2: Wrong KSP Version\n\nKSP version must match Kotlin version:\n\n### Mistake 3: Missing Processor Dependencies\n\nAdd KSP processor dependencies correctly.',
    code: '// Migrate from kapt to KSP\n// ❌ Old - kapt (slow)\nplugins {\n    kotlin("kapt")\n}\ndependencies {\n    kapt("com.example:processor:1.0")\n}\n\n// ✅ New - KSP (faster)\nplugins {\n    id("com.google.devtools.ksp") version "1.9.0-1.0.13"\n}\ndependencies {\n    ksp("com.example:processor-ksp:1.0")\n}\n\n// Version matching\n// KSP version format: kotlinVersion-kspVersion\n// Kotlin 1.9.0 -> KSP 1.9.0-1.0.x\n// Kotlin 2.0.0 -> KSP 2.0.0-1.0.x\n\n// Common KSP-compatible libraries\n// Room, Moshi, Koin, Arrow, Kotlinx.serialization'
  },
  '10.4': {
    title: 'Common Mistakes Writing KSP Processors',
    content: '### Mistake 1: Processing Order\n\nHandle multiple rounds correctly:\n\n### Mistake 2: Invalid Code Generation\n\nGenerate valid Kotlin code:\n\n### Mistake 3: Performance Issues\n\nCache and optimize processing.',
    code: '// Handle multiple processing rounds\nclass MyProcessor : SymbolProcessor {\n    override fun process(resolver: Resolver): List<KSAnnotated> {\n        val symbols = resolver.getSymbolsWithAnnotation("MyAnnotation")\n        val deferred = mutableListOf<KSAnnotated>()\n        \n        symbols.forEach { symbol ->\n            if (!symbol.validate()) {\n                deferred.add(symbol)  // Process next round\n            } else {\n                generateCode(symbol)\n            }\n        }\n        return deferred\n    }\n}\n\n// Use KotlinPoet for code generation\nval file = FileSpec.builder("com.example", "Generated")\n    .addType(\n        TypeSpec.classBuilder("GeneratedClass")\n            .addFunction(\n                FunSpec.builder("greet")\n                    .addParameter("name", String::class)\n                    .returns(String::class)\n                    .addStatement("return %S + name", "Hello, ")\n                    .build()\n            )\n            .build()\n    )\n    .build()\n\nfile.writeTo(codeGenerator, Dependencies.ALL_FILES)'
  },
  '10.5': {
    title: 'Common Mistakes with Context Receivers',
    content: '### Mistake 1: Overusing Context Receivers\n\nUse sparingly for clear boundaries:\n\n### Mistake 2: Too Many Contexts\n\nLimit context parameters:\n\n### Mistake 3: Confusion with Extension Functions\n\nUnderstand the difference.',
    code: '// Context receivers (experimental feature)\n// Enable in build.gradle.kts\n// kotlinOptions.freeCompilerArgs += "-Xcontext-receivers"\n\n// Good use - clear domain boundary\ncontext(Logger, Transaction)\nfun processOrder(order: Order) {\n    log("Processing order ${order.id}")  // From Logger context\n    save(order)  // From Transaction context\n}\n\n// ❌ Wrong - too many contexts\ncontext(A, B, C, D, E)  // Confusing!\nfun doSomething() { }\n\n// ✅ Correct - limit to 2-3\ncontext(Logger, Transaction)\nfun doSomething() { }\n\n// Context vs Extension\n// Extension: fun String.double() - operates on receiver\n// Context: context(Logger) fun work() - uses context capability\n\n// Calling context functions\nwith(logger) {\n    with(transaction) {\n        processOrder(order)\n    }\n}'
  }
};

// Add warnings to lessons
let addedCount = 0;
data.modules.forEach(module => {
  module.lessons.forEach(lesson => {
    if (warnings[lesson.id] && !lesson.contentSections.some(s => s.type === 'WARNING')) {
      const w = warnings[lesson.id];
      lesson.contentSections.push({
        type: 'WARNING',
        title: w.title,
        content: w.content,
        code: w.code,
        language: 'kotlin'
      });
      console.log('Added WARNING to lesson', lesson.id);
      addedCount++;
    }
  });
});

fs.writeFileSync(coursePath, JSON.stringify(data, null, 2));
console.log(`\nFile updated successfully. Added ${addedCount} WARNING sections.`);
