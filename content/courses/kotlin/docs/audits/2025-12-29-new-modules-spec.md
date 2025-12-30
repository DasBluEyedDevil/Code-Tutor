# New Modules Specification
## Kotlin Course Enhancement Proposal

---

## Module 08: Gradle Mastery for Kotlin Developers

### Overview

| Property | Value |
|----------|-------|
| **ID** | `module-08` |
| **Title** | Gradle Mastery for Kotlin Developers |
| **Description** | Master Gradle build system with Kotlin DSL. Learn dependency management, multiplatform configuration, custom tasks, and convention plugins. |
| **Difficulty** | intermediate |
| **Estimated Hours** | 8 |
| **Prerequisites** | Modules 01-04 (Kotlin fundamentals) |

### Lesson Structure

#### Lesson 8.1: Understanding Gradle Basics with Kotlin DSL
**Estimated Time**: 60 minutes

**Learning Objectives**:
- Understand Gradle's task-based build model
- Read and write `build.gradle.kts` files
- Understand project vs. build scripts
- Configure basic project metadata

**Key Content**:
```kotlin
// settings.gradle.kts
rootProject.name = "my-kotlin-project"

include(":app")
include(":shared")

// build.gradle.kts
plugins {
    kotlin("jvm") version "2.0.21"
    application
}

group = "com.example"
version = "1.0.0"

repositories {
    mavenCentral()
}

dependencies {
    implementation(kotlin("stdlib"))
    testImplementation(kotlin("test"))
}

application {
    mainClass.set("com.example.MainKt")
}
```

**Exercises**:
1. Create a new Kotlin project from scratch with Gradle
2. Configure project metadata and run configurations
3. Add and configure the `application` plugin

---

#### Lesson 8.2: Dependency Management & Version Catalogs
**Estimated Time**: 75 minutes

**Learning Objectives**:
- Declare and manage dependencies
- Use version catalogs (`libs.versions.toml`)
- Understand dependency configurations (implementation, api, testImplementation)
- Resolve dependency conflicts

**Key Content**:
```toml
# gradle/libs.versions.toml
[versions]
kotlin = "2.0.21"
kotlinx-coroutines = "1.9.0"
kotlinx-serialization = "1.7.3"
ktor = "3.0.2"
koin = "4.0.0"
compose-multiplatform = "1.7.1"

[libraries]
kotlinx-coroutines-core = { module = "org.jetbrains.kotlinx:kotlinx-coroutines-core", version.ref = "kotlinx-coroutines" }
kotlinx-serialization-json = { module = "org.jetbrains.kotlinx:kotlinx-serialization-json", version.ref = "kotlinx-serialization" }
ktor-server-core = { module = "io.ktor:ktor-server-core", version.ref = "ktor" }
ktor-server-netty = { module = "io.ktor:ktor-server-netty", version.ref = "ktor" }
koin-core = { module = "io.insert-koin:koin-core", version.ref = "koin" }

[bundles]
ktor-server = ["ktor-server-core", "ktor-server-netty"]

[plugins]
kotlin-jvm = { id = "org.jetbrains.kotlin.jvm", version.ref = "kotlin" }
kotlin-multiplatform = { id = "org.jetbrains.kotlin.multiplatform", version.ref = "kotlin" }
kotlinx-serialization = { id = "org.jetbrains.kotlin.plugin.serialization", version.ref = "kotlin" }
```

```kotlin
// build.gradle.kts using version catalog
plugins {
    alias(libs.plugins.kotlin.jvm)
    alias(libs.plugins.kotlinx.serialization)
}

dependencies {
    implementation(libs.kotlinx.coroutines.core)
    implementation(libs.kotlinx.serialization.json)
    implementation(libs.bundles.ktor.server)
}
```

---

#### Lesson 8.3: Multiplatform Build Configuration
**Estimated Time**: 90 minutes

**Learning Objectives**:
- Configure Kotlin Multiplatform targets
- Set up shared source sets
- Handle platform-specific dependencies
- Configure iOS framework distribution

**Key Content**:
```kotlin
// build.gradle.kts
plugins {
    alias(libs.plugins.kotlin.multiplatform)
    alias(libs.plugins.compose.multiplatform)
}

kotlin {
    // Android target
    androidTarget {
        compilations.all {
            kotlinOptions {
                jvmTarget = "17"
            }
        }
    }

    // iOS targets
    listOf(
        iosX64(),
        iosArm64(),
        iosSimulatorArm64()
    ).forEach { iosTarget ->
        iosTarget.binaries.framework {
            baseName = "Shared"
            isStatic = true
        }
    }

    // JVM target (for desktop/server)
    jvm("desktop")

    // Source sets
    sourceSets {
        val commonMain by getting {
            dependencies {
                implementation(libs.kotlinx.coroutines.core)
                implementation(libs.ktor.client.core)
            }
        }

        val androidMain by getting {
            dependencies {
                implementation(libs.ktor.client.okhttp)
            }
        }

        val iosMain by creating {
            dependsOn(commonMain)
            dependencies {
                implementation(libs.ktor.client.darwin)
            }
        }

        val iosX64Main by getting { dependsOn(iosMain) }
        val iosArm64Main by getting { dependsOn(iosMain) }
        val iosSimulatorArm64Main by getting { dependsOn(iosMain) }
    }
}
```

---

#### Lesson 8.4: Custom Tasks and Plugins
**Estimated Time**: 75 minutes

**Learning Objectives**:
- Create custom Gradle tasks
- Use task dependencies and ordering
- Create simple script plugins
- Understand task inputs/outputs for caching

**Key Content**:
```kotlin
// Custom task in build.gradle.kts
tasks.register("generateBuildInfo") {
    group = "build"
    description = "Generates build information file"

    val outputFile = layout.buildDirectory.file("generated/BuildInfo.kt")
    outputs.file(outputFile)

    doLast {
        outputFile.get().asFile.apply {
            parentFile.mkdirs()
            writeText("""
                package com.example

                object BuildInfo {
                    const val VERSION = "${project.version}"
                    const val BUILD_TIME = "${java.time.Instant.now()}"
                    const val GIT_HASH = "${getGitHash()}"
                }
            """.trimIndent())
        }
    }
}

fun getGitHash(): String =
    providers.exec {
        commandLine("git", "rev-parse", "--short", "HEAD")
    }.standardOutput.asText.get().trim()

// Task dependency
tasks.named("compileKotlin") {
    dependsOn("generateBuildInfo")
}
```

---

#### Lesson 8.5: Convention Plugins for Team Standards
**Estimated Time**: 90 minutes

**Learning Objectives**:
- Create buildSrc convention plugins
- Share build logic across modules
- Implement team coding standards in build
- Use composite builds

**Key Content**:
```kotlin
// buildSrc/build.gradle.kts
plugins {
    `kotlin-dsl`
}

repositories {
    mavenCentral()
    gradlePluginPortal()
}

dependencies {
    implementation("org.jetbrains.kotlin:kotlin-gradle-plugin:2.0.21")
    implementation("io.gitlab.arturbosch.detekt:detekt-gradle-plugin:1.23.7")
}
```

```kotlin
// buildSrc/src/main/kotlin/kotlin-library-conventions.gradle.kts
plugins {
    kotlin("jvm")
    id("io.gitlab.arturbosch.detekt")
}

kotlin {
    jvmToolchain(17)

    compilerOptions {
        allWarningsAsErrors.set(true)
        freeCompilerArgs.addAll(
            "-Xjdk-release=17",
            "-opt-in=kotlin.RequiresOptIn"
        )
    }
}

detekt {
    buildUponDefaultConfig = true
    config.setFrom(rootProject.files("config/detekt.yml"))
}

tasks.withType<Test> {
    useJUnitPlatform()
}
```

```kotlin
// app/build.gradle.kts (uses convention)
plugins {
    id("kotlin-library-conventions")
    application
}

// All standard config inherited from convention plugin!
```

---

#### Lesson 8.6: Build Optimization & Caching
**Estimated Time**: 60 minutes

**Learning Objectives**:
- Enable and configure Gradle build cache
- Use configuration cache
- Optimize build times
- Profile and debug slow builds

**Key Content**:
```properties
# gradle.properties
org.gradle.caching=true
org.gradle.configuration-cache=true
org.gradle.parallel=true
org.gradle.daemon=true
org.gradle.jvmargs=-Xmx4g -XX:+UseParallelGC

kotlin.incremental=true
kotlin.caching.enabled=true
```

```kotlin
// settings.gradle.kts - Remote build cache
buildCache {
    local {
        isEnabled = true
        directory = File(rootDir, "build-cache")
    }

    remote<HttpBuildCache> {
        url = uri("https://cache.example.com/")
        isPush = System.getenv("CI") != null
        credentials {
            username = System.getenv("CACHE_USER")
            password = System.getenv("CACHE_PASSWORD")
        }
    }
}
```

---

## Module 09: Functional Kotlin with Arrow

### Overview

| Property | Value |
|----------|-------|
| **ID** | `module-09` |
| **Title** | Functional Kotlin with Arrow |
| **Description** | Master functional programming in Kotlin. Learn error handling without exceptions, railway-oriented programming, and Arrow's powerful effect system. |
| **Difficulty** | intermediate |
| **Estimated Hours** | 6 |
| **Prerequisites** | Modules 01-04, familiarity with coroutines |

### Lesson Structure

#### Lesson 9.1: Functional Programming Principles
**Estimated Time**: 50 minutes

**Learning Objectives**:
- Understand pure functions and side effects
- Apply immutability principles
- Use function composition
- Understand referential transparency

**Key Content**:
```kotlin
// Pure functions - same input always gives same output
fun add(a: Int, b: Int): Int = a + b  // Pure
fun now(): Long = System.currentTimeMillis()  // Impure (side effect: reads system clock)

// Immutability with data classes
data class User(val id: Long, val name: String, val email: String)

fun updateEmail(user: User, newEmail: String): User =
    user.copy(email = newEmail)  // Returns new instance, original unchanged

// Function composition
val sanitize: (String) -> String = { it.trim().lowercase() }
val validate: (String) -> Boolean = { it.isNotEmpty() && it.contains("@") }

val sanitizeAndValidate: (String) -> Boolean = { validate(sanitize(it)) }

// Higher-order functions
fun <T, R> List<T>.mapIndexedNotNull(transform: (Int, T) -> R?): List<R> =
    mapIndexedNotNull(transform)
```

---

#### Lesson 9.2: Kotlin's Built-in Result Type
**Estimated Time**: 60 minutes

**Learning Objectives**:
- Use `Result<T>` for error handling
- Chain operations with `map`, `mapCatching`, `fold`
- Convert between Result and exceptions
- Apply to real-world scenarios

**Key Content**:
```kotlin
// Creating Results
fun parseNumber(s: String): Result<Int> = runCatching { s.toInt() }

fun divide(a: Int, b: Int): Result<Double> = runCatching {
    require(b != 0) { "Cannot divide by zero" }
    a.toDouble() / b
}

// Chaining operations
fun calculate(input: String): Result<String> =
    parseNumber(input)
        .mapCatching { it * 2 }
        .mapCatching { divide(it, 3).getOrThrow() }
        .map { "Result: $it" }

// Handling results
fun processResult(result: Result<String>) {
    result.fold(
        onSuccess = { println("Success: $it") },
        onFailure = { println("Error: ${it.message}") }
    )

    // Or use when
    result
        .onSuccess { println("Got: $it") }
        .onFailure { println("Failed: ${it.message}") }

    // Get with default
    val value = result.getOrDefault("fallback")
    val nullable = result.getOrNull()
    val recovered = result.getOrElse { "recovered from ${it.message}" }
}
```

---

#### Lesson 9.3: Arrow Core - Either, Option, Validated
**Estimated Time**: 75 minutes

**Learning Objectives**:
- Use `Either<L, R>` for typed errors
- Apply `Option<A>` for explicit nullability
- Accumulate errors with `Validated`
- Understand when to use each type

**Key Content**:
```kotlin
import arrow.core.*

// Either - typed error handling
sealed interface UserError {
    data class NotFound(val id: Long) : UserError
    data class InvalidEmail(val email: String) : UserError
    data object Unauthorized : UserError
}

fun getUser(id: Long): Either<UserError, User> =
    if (id <= 0) UserError.NotFound(id).left()
    else User(id, "John", "john@example.com").right()

fun validateEmail(email: String): Either<UserError, String> =
    if (email.contains("@")) email.right()
    else UserError.InvalidEmail(email).left()

// Chaining with flatMap
fun updateUserEmail(userId: Long, newEmail: String): Either<UserError, User> =
    either {
        val user = getUser(userId).bind()
        val validEmail = validateEmail(newEmail).bind()
        user.copy(email = validEmail)
    }

// Validated - accumulate all errors
data class FormErrors(val errors: List<String>)

fun validateUsername(name: String): ValidatedNel<String, String> =
    if (name.length >= 3) name.validNel()
    else "Username must be at least 3 characters".invalidNel()

fun validatePassword(pass: String): ValidatedNel<String, String> =
    if (pass.length >= 8) pass.validNel()
    else "Password must be at least 8 characters".invalidNel()

fun validateAge(age: Int): ValidatedNel<String, Int> =
    if (age >= 18) age.validNel()
    else "Must be 18 or older".invalidNel()

fun validateRegistration(
    username: String,
    password: String,
    age: Int
): ValidatedNel<String, Registration> =
    validateUsername(username)
        .zip(validatePassword(password), validateAge(age)) { u, p, a ->
            Registration(u, p, a)
        }

// Usage - all errors collected
val result = validateRegistration("ab", "123", 16)
// Invalid(NonEmptyList(
//   "Username must be at least 3 characters",
//   "Password must be at least 8 characters",
//   "Must be 18 or older"
// ))
```

---

#### Lesson 9.4: Railway-Oriented Programming
**Estimated Time**: 60 minutes

**Learning Objectives**:
- Understand the railway metaphor
- Chain operations that can fail
- Handle errors at the end of the pipeline
- Apply to real business workflows

**Key Content**:
```kotlin
// Railway-Oriented Programming
// Success track: ──────────────────────────────>
// Failure track:                      ──────────>

sealed interface OrderError {
    data class ValidationFailed(val reason: String) : OrderError
    data class PaymentFailed(val reason: String) : OrderError
    data class InventoryError(val productId: Long) : OrderError
    data class ShippingError(val reason: String) : OrderError
}

data class Order(
    val id: Long,
    val items: List<OrderItem>,
    val customerId: Long,
    val status: OrderStatus
)

// Each step returns Either - can switch to failure track
fun validateOrder(order: Order): Either<OrderError, Order> = either {
    ensure(order.items.isNotEmpty()) {
        OrderError.ValidationFailed("Order must have items")
    }
    ensure(order.items.all { it.quantity > 0 }) {
        OrderError.ValidationFailed("Invalid quantities")
    }
    order
}

fun checkInventory(order: Order): Either<OrderError, Order> = either {
    order.items.forEach { item ->
        val available = inventoryService.check(item.productId)
        ensure(available >= item.quantity) {
            OrderError.InventoryError(item.productId)
        }
    }
    order
}

fun processPayment(order: Order): Either<OrderError, Order> = either {
    val result = paymentService.charge(order.customerId, order.total)
    ensure(result.success) { OrderError.PaymentFailed(result.message) }
    order.copy(status = OrderStatus.PAID)
}

fun arrangeShipping(order: Order): Either<OrderError, Order> = either {
    val tracking = shippingService.createShipment(order)
    ensure(tracking != null) { OrderError.ShippingError("Shipment creation failed") }
    order.copy(status = OrderStatus.SHIPPED)
}

// The complete railway
fun processOrder(order: Order): Either<OrderError, Order> = either {
    val validated = validateOrder(order).bind()
    val inventoryChecked = checkInventory(validated).bind()
    val paid = processPayment(inventoryChecked).bind()
    val shipped = arrangeShipping(paid).bind()
    shipped
}

// Or using flatMap chain (equivalent)
fun processOrderChain(order: Order): Either<OrderError, Order> =
    validateOrder(order)
        .flatMap { checkInventory(it) }
        .flatMap { processPayment(it) }
        .flatMap { arrangeShipping(it) }

// Handle at the end
fun handleOrder(order: Order) {
    processOrder(order).fold(
        ifLeft = { error ->
            when (error) {
                is OrderError.ValidationFailed -> showValidationError(error.reason)
                is OrderError.PaymentFailed -> retryPayment(order)
                is OrderError.InventoryError -> suggestAlternatives(error.productId)
                is OrderError.ShippingError -> queueForManualReview(order)
            }
        },
        ifRight = { completedOrder ->
            sendConfirmationEmail(completedOrder)
            updateDashboard(completedOrder)
        }
    )
}
```

---

#### Lesson 9.5: Effect System with Arrow
**Estimated Time**: 60 minutes

**Learning Objectives**:
- Use `Raise<E>` for effect-based error handling
- Apply `ensure` and `ensureNotNull`
- Compose effects with `either { }` builder
- Integrate with coroutines

**Key Content**:
```kotlin
import arrow.core.raise.*

context(Raise<UserError>)
suspend fun getUser(id: Long): User {
    ensure(id > 0) { UserError.InvalidId(id) }
    return userRepository.findById(id)
        ?: raise(UserError.NotFound(id))
}

context(Raise<UserError>)
suspend fun validatePermissions(user: User, action: Action) {
    ensure(user.hasPermission(action)) { UserError.Unauthorized }
}

context(Raise<UserError>)
suspend fun performAction(userId: Long, action: Action): ActionResult {
    val user = getUser(userId)
    validatePermissions(user, action)
    return actionService.execute(user, action)
}

// Execute and handle
suspend fun main() {
    val result: Either<UserError, ActionResult> = either {
        performAction(123, Action.DELETE_POST)
    }

    // Or recover inline
    val recovered = recover({
        performAction(123, Action.DELETE_POST)
    }) { error ->
        when (error) {
            is UserError.NotFound -> ActionResult.Skipped
            is UserError.Unauthorized -> ActionResult.Denied
            else -> raise(error)  // Re-raise other errors
        }
    }
}
```

---

#### Lesson 9.6: Practical Patterns - Error Handling Without Exceptions
**Estimated Time**: 45 minutes

**Learning Objectives**:
- Design error hierarchies for domains
- Convert between exceptions and typed errors
- Apply patterns to Ktor and Android
- Test functional error handling

**Key Content**:
```kotlin
// Complete practical example: API Layer

// Domain errors
sealed interface ApiError {
    data class NetworkError(val cause: Throwable) : ApiError
    data class ServerError(val code: Int, val message: String) : ApiError
    data class DeserializationError(val body: String) : ApiError
    data object Timeout : ApiError
}

// Convert Ktor exceptions to typed errors
suspend fun <T> safeApiCall(
    block: suspend () -> HttpResponse
): Either<ApiError, T> = either {
    val response = catch({ block() }) { e ->
        when (e) {
            is SocketTimeoutException -> raise(ApiError.Timeout)
            is IOException -> raise(ApiError.NetworkError(e))
            else -> raise(ApiError.NetworkError(e))
        }
    }

    ensure(response.status.isSuccess()) {
        ApiError.ServerError(response.status.value, response.bodyAsText())
    }

    catch({ response.body<T>() }) { e ->
        raise(ApiError.DeserializationError(response.bodyAsText()))
    }
}

// Usage in repository
class UserRepository(private val client: HttpClient) {
    suspend fun getUser(id: Long): Either<ApiError, User> =
        safeApiCall { client.get("users/$id") }

    suspend fun updateUser(user: User): Either<ApiError, User> =
        safeApiCall { client.put("users/${user.id}") { setBody(user) } }
}

// Usage in ViewModel (Android/Compose)
class UserViewModel(private val repository: UserRepository) : ViewModel() {
    private val _state = MutableStateFlow<UserState>(UserState.Loading)
    val state: StateFlow<UserState> = _state.asStateFlow()

    fun loadUser(id: Long) {
        viewModelScope.launch {
            repository.getUser(id).fold(
                ifLeft = { error ->
                    _state.value = when (error) {
                        is ApiError.NetworkError -> UserState.Offline
                        is ApiError.Timeout -> UserState.Error("Request timed out")
                        is ApiError.ServerError -> UserState.Error(error.message)
                        is ApiError.DeserializationError -> UserState.Error("Invalid response")
                    }
                },
                ifRight = { user ->
                    _state.value = UserState.Success(user)
                }
            )
        }
    }
}
```

---

## Module 10: The K2 Era - Modern Kotlin Tooling

### Overview

| Property | Value |
|----------|-------|
| **ID** | `module-10` |
| **Title** | The K2 Era - Modern Kotlin Tooling |
| **Description** | Master the K2 compiler, migrate from kapt to KSP, and explore Kotlin's cutting-edge features including context receivers. |
| **Difficulty** | advanced |
| **Estimated Hours** | 5 |
| **Prerequisites** | Modules 01-07, practical Kotlin experience |

### Lesson Structure

#### Lesson 10.1: K2 Compiler - What's New and Why It Matters
**Estimated Time**: 45 minutes

**Learning Objectives**:
- Understand K2 compiler architecture
- Identify performance improvements
- Recognize new diagnostics and errors
- Plan migration strategy

**Key Content**:
```kotlin
// K2 Compiler Benefits

// 1. 2x Faster Compilation
// - New frontend architecture
// - Better incremental compilation
// - Parallel analysis

// 2. Improved Type Inference
// Before K2 (fails):
// val result = buildList {
//     add("string")
//     add(if (condition) 1 else "fallback")  // Type error
// }

// With K2 (works):
val result = buildList {
    add("string")
    if (condition) add(1) else add("fallback")  // OK, infers List<Any>
}

// 3. Smarter Smart Casts
fun process(value: Any) {
    if (value is String && value.length > 5) {
        // K2: value is smart-cast to String in BOTH conditions
        println(value.uppercase())
    }

    // K2 handles complex conditions better
    when {
        value is List<*> && value.isNotEmpty() -> {
            // value is List<*> here
            println(value.first())
        }
    }
}

// 4. Better Diagnostics
// K2 provides clearer error messages with suggestions
// "Type mismatch: expected String, found Int.
//  Did you mean to call .toString()?"

// Enable K2 in build.gradle.kts
kotlin {
    compilerOptions {
        languageVersion.set(KotlinVersion.KOTLIN_2_0)
        apiVersion.set(KotlinVersion.KOTLIN_2_0)
    }
}
```

---

#### Lesson 10.2: Migrating Projects to K2
**Estimated Time**: 50 minutes

**Learning Objectives**:
- Assess project readiness for K2
- Handle breaking changes
- Update deprecated patterns
- Verify library compatibility

**Key Content**:
```kotlin
// Migration Checklist

// 1. Update Kotlin version
// gradle/libs.versions.toml
// [versions]
// kotlin = "2.0.21"

// 2. Enable K2 progressively
kotlin {
    compilerOptions {
        // Start with language version only
        languageVersion.set(KotlinVersion.KOTLIN_2_0)

        // Later, also update API version
        // apiVersion.set(KotlinVersion.KOTLIN_2_0)
    }
}

// 3. Common Migration Issues

// Issue: Stricter null checking
// Before (K1 allowed):
val map: Map<String, String> = mapOf()
val value: String = map["key"]!!  // ❌ K2 warns: guaranteed null

// After:
val value: String = map["key"] ?: error("Key not found")

// Issue: Property inference changes
// Before (K1):
val items = listOf(1, 2, 3)
val first = items[0]  // Inferred as Int

// K2 may infer differently in complex expressions
val first: Int = items[0]  // Explicit when needed

// Issue: Lambda return type inference
fun process(block: () -> Any): Any = block()

// Before (K1 - worked):
val result = process {
    if (condition) "string"
    else 42
}

// K2: More precise, may need explicit type
val result: Any = process {
    if (condition) "string" else 42
}

// 4. Library Compatibility Check
// Run: ./gradlew dependencies --configuration compileClasspath
// Verify all libraries support Kotlin 2.0
```

---

#### Lesson 10.3: KSP - Replacing kapt with Speed
**Estimated Time**: 60 minutes

**Learning Objectives**:
- Understand KSP vs kapt differences
- Migrate from kapt to KSP
- Configure KSP for common libraries
- Optimize KSP performance

**Key Content**:
```kotlin
// Why KSP over kapt?
// - 2x faster (no stub generation)
// - Kotlin-native (understands Kotlin types)
// - Better incremental processing
// - No Java stubs = less memory

// Migration: Room (Android)
// Before (kapt):
plugins {
    id("org.jetbrains.kotlin.kapt")
}
dependencies {
    kapt("androidx.room:room-compiler:2.6.1")
}

// After (KSP):
plugins {
    id("com.google.devtools.ksp") version "2.0.21-1.0.28"
}
dependencies {
    ksp("androidx.room:room-compiler:2.6.1")
}

// Migration: Moshi
// Before:
kapt("com.squareup.moshi:moshi-kotlin-codegen:1.15.1")

// After:
ksp("com.squareup.moshi:moshi-kotlin-codegen:1.15.1")

// Migration: Koin Annotations
// Before:
kapt("io.insert-koin:koin-ksp-compiler:1.4.0")

// After:
ksp("io.insert-koin:koin-ksp-compiler:1.4.0")

// KSP Configuration
ksp {
    // Pass arguments to processors
    arg("room.schemaLocation", "$projectDir/schemas")
    arg("room.incremental", "true")
    arg("room.generateKotlin", "true")
}

// Performance optimization
ksp {
    // Enable KSP2 for even faster processing
    useKsp2.set(true)

    // Allow parallel processing
    allWarningsAsErrors.set(false)
}
```

---

#### Lesson 10.4: Writing Your First KSP Processor
**Estimated Time**: 75 minutes

**Learning Objectives**:
- Create a KSP processor project
- Process annotations and generate code
- Handle incremental processing
- Test KSP processors

**Key Content**:
```kotlin
// Example: Auto-generate Builder pattern

// 1. Define the annotation (separate module: annotations)
@Target(AnnotationTarget.CLASS)
@Retention(AnnotationRetention.SOURCE)
annotation class AutoBuilder

// 2. Create the processor (separate module: processor)
class AutoBuilderProcessor(
    private val codeGenerator: CodeGenerator,
    private val logger: KSPLogger
) : SymbolProcessor {

    override fun process(resolver: Resolver): List<KSAnnotated> {
        val symbols = resolver.getSymbolsWithAnnotation(
            AutoBuilder::class.qualifiedName!!
        )

        symbols
            .filterIsInstance<KSClassDeclaration>()
            .filter { it.validate() }
            .forEach { generateBuilder(it) }

        return symbols.filterNot { it.validate() }.toList()
    }

    private fun generateBuilder(classDecl: KSClassDeclaration) {
        val className = classDecl.simpleName.asString()
        val packageName = classDecl.packageName.asString()
        val properties = classDecl.getAllProperties().toList()

        val fileSpec = FileSpec.builder(packageName, "${className}Builder")
            .addType(
                TypeSpec.classBuilder("${className}Builder")
                    .apply {
                        properties.forEach { prop ->
                            addProperty(
                                PropertySpec.builder(
                                    prop.simpleName.asString(),
                                    prop.type.resolve().toTypeName().copy(nullable = true)
                                )
                                    .mutable()
                                    .initializer("null")
                                    .build()
                            )

                            addFunction(
                                FunSpec.builder(prop.simpleName.asString())
                                    .addParameter("value", prop.type.resolve().toTypeName())
                                    .returns(ClassName(packageName, "${className}Builder"))
                                    .addStatement("this.%L = value", prop.simpleName.asString())
                                    .addStatement("return this")
                                    .build()
                            )
                        }

                        addFunction(
                            FunSpec.builder("build")
                                .returns(ClassName(packageName, className))
                                .addStatement(
                                    "return %L(%L)",
                                    className,
                                    properties.joinToString {
                                        "${it.simpleName.asString()} = ${it.simpleName.asString()}!!"
                                    }
                                )
                                .build()
                        )
                    }
                    .build()
            )
            .build()

        codeGenerator.createNewFile(
            Dependencies(true, classDecl.containingFile!!),
            packageName,
            "${className}Builder"
        ).bufferedWriter().use { fileSpec.writeTo(it) }
    }
}

class AutoBuilderProcessorProvider : SymbolProcessorProvider {
    override fun create(environment: SymbolProcessorEnvironment) =
        AutoBuilderProcessor(environment.codeGenerator, environment.logger)
}

// 3. Usage
@AutoBuilder
data class User(
    val id: Long,
    val name: String,
    val email: String
)

// Generated code allows:
val user = UserBuilder()
    .id(1)
    .name("John")
    .email("john@example.com")
    .build()
```

---

#### Lesson 10.5: Context Receivers and Future Features
**Estimated Time**: 50 minutes

**Learning Objectives**:
- Understand context receivers concept
- Apply context receivers for cleaner APIs
- Explore upcoming Kotlin features
- Prepare for future language evolution

**Key Content**:
```kotlin
// Context Receivers (Experimental in Kotlin 2.0)
// Enable with: -Xcontext-receivers

// Problem: Dependency threading
class UserService(private val logger: Logger) {
    fun createUser(name: String): User {
        logger.info("Creating user: $name")
        // ...
    }
}

// With context receivers - cleaner, more composable
context(Logger)
fun createUser(name: String): User {
    info("Creating user: $name")  // Logger is in context
    return User(generateId(), name)
}

context(Logger, Database)
suspend fun registerUser(name: String, email: String): User {
    info("Registering user: $name")
    val user = createUser(name)  // Logger context available
    execute("INSERT INTO users VALUES (?, ?, ?)", user.id, name, email)
    return user
}

// Usage
fun main() {
    val logger = ConsoleLogger()
    val database = PostgresDatabase()

    with(logger) {
        with(database) {
            runBlocking {
                val user = registerUser("John", "john@example.com")
            }
        }
    }
}

// Real-world example: Transaction context
context(TransactionContext)
suspend fun transferMoney(from: Account, to: Account, amount: Money) {
    ensure(from.balance >= amount) { InsufficientFunds() }

    from.withdraw(amount)
    to.deposit(amount)

    // Transaction committed automatically when context exits
}

// DSL example
context(HtmlContext)
fun div(content: context(HtmlContext) () -> Unit) {
    append("<div>")
    content()
    append("</div>")
}

context(HtmlContext)
fun span(text: String) {
    append("<span>$text</span>")
}

// Future Features to Watch
// 1. Name-based destructuring (KT-19627)
// 2. Multiple receivers (beyond context)
// 3. Union types (under discussion)
// 4. Static extensions (KEEP-348)
// 5. Package-level context receivers

// Preparing for the future
// - Write pure functions where possible
// - Prefer composition over inheritance
// - Use sealed types for exhaustive when
// - Embrace immutability
// - Follow Kotlin idioms, not Java patterns
```

---

## Implementation Priority

### Phase 1: Quick Wins (1-2 weeks)
1. Fix `runBlocking` warnings in Module 04
2. Replace Java collection types globally
3. Add expanded value class examples

### Phase 2: New Modules (4-6 weeks)
1. **Module 08: Gradle Mastery** - Most requested by students
2. **Module 09: Functional Kotlin** - Differentiator for advanced learners
3. **Module 10: K2 Era** - Future-proofs the curriculum

### Phase 3: Integration (2 weeks)
1. Update existing modules to reference new content
2. Add cross-module exercises
3. Create learning paths (Backend Dev, Mobile Dev, Full Stack)

---

## Appendix: Library Versions for New Modules

```toml
# gradle/libs.versions.toml additions
[versions]
arrow = "1.2.4"
ksp = "2.0.21-1.0.28"
detekt = "1.23.7"
kotlinpoet = "1.18.1"

[libraries]
arrow-core = { module = "io.arrow-kt:arrow-core", version.ref = "arrow" }
arrow-fx-coroutines = { module = "io.arrow-kt:arrow-fx-coroutines", version.ref = "arrow" }
ksp-api = { module = "com.google.devtools.ksp:symbol-processing-api", version.ref = "ksp" }
kotlinpoet = { module = "com.squareup:kotlinpoet", version.ref = "kotlinpoet" }
kotlinpoet-ksp = { module = "com.squareup:kotlinpoet-ksp", version.ref = "kotlinpoet" }

[plugins]
ksp = { id = "com.google.devtools.ksp", version.ref = "ksp" }
detekt = { id = "io.gitlab.arturbosch.detekt", version.ref = "detekt" }
```
