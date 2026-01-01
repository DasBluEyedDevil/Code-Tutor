# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Professional Development & Deployment
- **Lesson:** Lesson 7.1: Advanced KMP Patterns (ID: 7.1)
- **Difficulty:** advanced
- **Estimated Time:** 75 minutes

## Current Lesson Content

{
    "id":  "7.1",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 75 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "\nWelcome to Part 7 of the Kotlin Training Course! You\u0027ve been building KMP apps throughout this course, so you already understand the fundamentals: shared code, expect/actual declarations, and the basic project structure.\n\nNow it\u0027s time to go deeper. This lesson covers **advanced KMP patterns** that professional developers use to build production-ready cross-platform applications:\n\n- Swift interoperability for iOS integration\n- CocoaPods integration for iOS dependencies\n- C interop (cinterop) for native libraries\n- Publishing KMP libraries for others to use\n- Advanced expect/actual patterns\n\nThese techniques will help you build professional KMP libraries and integrate seamlessly with existing iOS/native ecosystems.\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Swift Interoperability",
                                "content":  "\n### Calling Kotlin from Swift\n\nWhen you compile KMP for iOS, Kotlin generates an Objective-C framework. Swift can call this code, but there are nuances to understand.\n\n**Kotlin code in shared module:**\n\n```kotlin\n// shared/src/commonMain/kotlin/Calculator.kt\nclass Calculator {\n    fun add(a: Int, b: Int): Int = a + b\n    fun multiply(a: Int, b: Int): Int = a * b\n}\n```\n\n**Swift usage:**\n\n```swift\nimport shared  // Your KMP framework name\n\nlet calc = Calculator()\nlet result = calc.add(a: 5, b: 3)  // Named parameters!\nprint(result)  // 8\n```\n\n### Making Kotlin More Swift-Friendly\n\nUse annotations to improve the Swift API:\n\n```kotlin\n// Better Swift names with @ObjCName\n@ObjCName(\"NetworkClient\")\nclass ApiClient {\n    @ObjCName(\"fetchUser\")\n    fun getUserById(id: String): User? = ...\n}\n\n// Swift sees: NetworkClient().fetchUser(id: \"123\")\n```\n\n### Handling Kotlin Nullability\n\n```kotlin\n// Kotlin\nfun findUser(id: String): User?  // Nullable\n\n// Swift automatically gets optional:\n// func findUser(id: String) -\u003e User?\n\nif let user = api.findUser(id: \"123\") {\n    print(user.name)\n}\n```\n\n### Kotlin Sealed Classes in Swift\n\n```kotlin\nsealed class Result\u003cT\u003e {\n    data class Success\u003cT\u003e(val data: T) : Result\u003cT\u003e()\n    data class Error\u003cT\u003e(val message: String) : Result\u003cT\u003e()\n}\n```\n\n**Swift pattern matching:**\n\n```swift\nswitch result {\ncase let success as ResultSuccess\u003cUser\u003e:\n    print(\"Got user: \\(success.data.name)\")\ncase let error as ResultError\u003cUser\u003e:\n    print(\"Error: \\(error.message)\")\ndefault:\n    break\n}\n```\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "CocoaPods Integration",
                                "content":  "\n### What is CocoaPods?\n\nCocoaPods is the most popular dependency manager for iOS/macOS projects. KMP can integrate with CocoaPods to:\n1. **Consume** iOS libraries in your Kotlin code\n2. **Publish** your KMP library as a CocoaPod\n\n### Setting Up CocoaPods Plugin\n\n```kotlin\n// build.gradle.kts\nplugins {\n    kotlin(\"multiplatform\")\n    kotlin(\"native.cocoapods\")\n}\n\nkotlin {\n    iosX64()\n    iosArm64()\n    iosSimulatorArm64()\n\n    cocoapods {\n        // Required: Library name and version\n        name = \"SharedModule\"\n        version = \"1.0.0\"\n        summary = \"Shared KMP code for iOS\"\n        homepage = \"https://github.com/yourcompany/yourproject\"\n\n        // Framework settings\n        framework {\n            baseName = \"shared\"\n            isStatic = true  // or false for dynamic\n        }\n\n        // iOS deployment target\n        ios.deploymentTarget = \"14.0\"\n\n        // Consume iOS libraries\n        pod(\"AFNetworking\") {\n            version = \"~\u003e 4.0\"\n        }\n        pod(\"Alamofire\") {\n            version = \"5.8.0\"\n        }\n    }\n}\n```\n\n### Using CocoaPods Dependencies in Kotlin\n\n```kotlin\n// After adding Alamofire pod, you can use it:\nimport cocoapods.Alamofire.*\n\nclass IosNetworkClient {\n    fun makeRequest(url: String) {\n        // Use Alamofire APIs directly!\n        AF.request(url).response { response in\n            // Handle response\n        }\n    }\n}\n```\n\n### Generating the Pod\n\nRun Gradle to generate Podspec:\n\n```bash\n./gradlew podspec\n```\n\nThis creates `SharedModule.podspec` that iOS developers can use:\n\n```ruby\n# In iOS project Podfile\npod \u0027SharedModule\u0027, :path =\u003e \u0027../shared\u0027\n```\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "C Interop (cinterop)",
                                "content":  "\n### What is cinterop?\n\nKotlin/Native can call C libraries directly using **cinterop** (C interoperability). This is powerful for:\n- Using system libraries (OpenSSL, SQLite, etc.)\n- Integrating legacy C/C++ code\n- Accessing platform APIs not available in Kotlin\n\n### Setting Up cinterop\n\n**Step 1: Create a .def file**\n\n```\n# src/nativeInterop/cinterop/openssl.def\npackage = openssl\nheaders = openssl/ssl.h openssl/crypto.h\ncompilerOpts = -I/usr/local/include\nlinkerOpts = -L/usr/local/lib -lssl -lcrypto\n```\n\n**Step 2: Configure in build.gradle.kts**\n\n```kotlin\nkotlin {\n    linuxX64 {\n        compilations.getByName(\"main\") {\n            cinterops {\n                val openssl by creating {\n                    defFile(project.file(\"src/nativeInterop/cinterop/openssl.def\"))\n                    packageName(\"openssl\")\n                }\n            }\n        }\n    }\n}\n```\n\n**Step 3: Use C functions in Kotlin**\n\n```kotlin\nimport openssl.*\n\nfun generateRandomBytes(count: Int): ByteArray {\n    val buffer = ByteArray(count)\n    buffer.usePinned { pinned -\u003e\n        RAND_bytes(pinned.addressOf(0).reinterpret(), count)\n    }\n    return buffer\n}\n```\n\n### Memory Management with C\n\n```kotlin\nimport kotlinx.cinterop.*\n\nfun workWithCMemory() {\n    // Allocate C memory\n    memScoped {\n        val buffer = allocArray\u003cByteVar\u003e(1024)\n        \n        // Use the buffer\n        some_c_function(buffer, 1024)\n        \n        // Convert to Kotlin string\n        val result = buffer.toKString()\n        \n        // Memory automatically freed when memScoped block ends\n    }\n}\n```\n\n### Platform-Specific cinterop\n\n```kotlin\n// iOS-specific with CoreFoundation\nkotlin {\n    iosArm64 {\n        compilations.getByName(\"main\") {\n            cinterops {\n                val corefoundation by creating {\n                    defFile(\"src/nativeInterop/cinterop/corefoundation.def\")\n                }\n            }\n        }\n    }\n}\n```\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Publishing KMP Libraries",
                                "content":  "\n### Why Publish KMP Libraries?\n\n- Share code across your organization\u0027s projects\n- Contribute to the open-source KMP ecosystem\n- Create SDKs that work on all platforms\n\n### Setting Up for Publishing\n\n```kotlin\n// build.gradle.kts\nplugins {\n    kotlin(\"multiplatform\")\n    id(\"maven-publish\")\n    id(\"signing\")  // For Maven Central\n}\n\ngroup = \"com.yourcompany\"\nversion = \"1.0.0\"\n\nkotlin {\n    // Define all targets\n    jvm()\n    androidTarget {\n        publishLibraryVariants(\"release\")\n    }\n    iosX64()\n    iosArm64()\n    iosSimulatorArm64()\n    js(IR) { browser(); nodejs() }\n    linuxX64()\n    macosX64()\n    macosArm64()\n\n    sourceSets {\n        val commonMain by getting {\n            dependencies {\n                implementation(\"org.jetbrains.kotlinx:kotlinx-coroutines-core:1.8.1\")\n            }\n        }\n    }\n}\n\npublishing {\n    publications {\n        // KMP plugin auto-generates publications\n    }\n    \n    repositories {\n        maven {\n            name = \"GitHubPackages\"\n            url = uri(\"https://maven.pkg.github.com/yourorg/yourrepo\")\n            credentials {\n                username = System.getenv(\"GITHUB_ACTOR\")\n                password = System.getenv(\"GITHUB_TOKEN\")\n            }\n        }\n    }\n}\n```\n\n### Publishing to Maven Central\n\n```kotlin\nsigning {\n    sign(publishing.publications)\n}\n\npublishing {\n    publications.withType\u003cMavenPublication\u003e {\n        pom {\n            name.set(\"My KMP Library\")\n            description.set(\"A cross-platform library\")\n            url.set(\"https://github.com/yourorg/yourrepo\")\n            \n            licenses {\n                license {\n                    name.set(\"Apache-2.0\")\n                    url.set(\"https://opensource.org/licenses/Apache-2.0\")\n                }\n            }\n            \n            developers {\n                developer {\n                    id.set(\"yourid\")\n                    name.set(\"Your Name\")\n                }\n            }\n            \n            scm {\n                connection.set(\"scm:git:git://github.com/yourorg/yourrepo.git\")\n                url.set(\"https://github.com/yourorg/yourrepo\")\n            }\n        }\n    }\n}\n```\n\n### Publishing Commands\n\n```bash\n# Publish to local Maven (~/.m2)\n./gradlew publishToMavenLocal\n\n# Publish to GitHub Packages\n./gradlew publish\n\n# Publish to Maven Central (with signing)\n./gradlew publishAllPublicationsToSonatypeRepository\n```\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Advanced Expect/Actual Patterns",
                                "content":  "\nYou learned the basics of expect/actual in Lesson 3.4. Now let\u0027s explore advanced patterns.\n\n### Expect Classes with Constructors\n\n```kotlin\n// commonMain\nexpect class SecureStorage(context: PlatformContext) {\n    fun store(key: String, value: String)\n    fun retrieve(key: String): String?\n    fun delete(key: String)\n}\n\n// androidMain\nactual class SecureStorage actual constructor(\n    private val context: PlatformContext\n) {\n    private val prefs = context.getEncryptedSharedPreferences()\n    \n    actual fun store(key: String, value: String) {\n        prefs.edit().putString(key, value).apply()\n    }\n    \n    actual fun retrieve(key: String): String? {\n        return prefs.getString(key, null)\n    }\n    \n    actual fun delete(key: String) {\n        prefs.edit().remove(key).apply()\n    }\n}\n\n// iosMain\nactual class SecureStorage actual constructor(\n    private val context: PlatformContext\n) {\n    actual fun store(key: String, value: String) {\n        // Use iOS Keychain\n        KeychainWrapper.standard.set(value, forKey: key)\n    }\n    \n    actual fun retrieve(key: String): String? {\n        return KeychainWrapper.standard.string(forKey: key)\n    }\n    \n    actual fun delete(key: String) {\n        KeychainWrapper.standard.removeObject(forKey: key)\n    }\n}\n```\n\n### Type Aliases for Platform Types\n\n```kotlin\n// commonMain\nexpect class PlatformContext\n\n// androidMain\nactual typealias PlatformContext = android.content.Context\n\n// iosMain\nactual typealias PlatformContext = platform.UIKit.UIViewController\n```\n\n### Expect Object (Singleton)\n\n```kotlin\n// commonMain\nexpect object DeviceInfo {\n    val osName: String\n    val osVersion: String\n    val deviceModel: String\n}\n\n// androidMain\nactual object DeviceInfo {\n    actual val osName: String = \"Android\"\n    actual val osVersion: String = Build.VERSION.RELEASE\n    actual val deviceModel: String = \"${Build.MANUFACTURER} ${Build.MODEL}\"\n}\n\n// iosMain\nactual object DeviceInfo {\n    actual val osName: String = \"iOS\"\n    actual val osVersion: String = UIDevice.currentDevice.systemVersion\n    actual val deviceModel: String = UIDevice.currentDevice.model\n}\n```\n\n### Expect with Default Implementation\n\n```kotlin\n// commonMain\ninterface Logger {\n    fun log(message: String)\n    fun error(message: String)\n}\n\nexpect fun createLogger(): Logger\n\n// Each platform provides its implementation\n// androidMain: Uses Android Log\n// iosMain: Uses NSLog\n// jvmMain: Uses SLF4J\n```\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Hierarchical Source Sets",
                                "content":  "\n### Advanced Project Structure\n\nKMP supports hierarchical source sets for sharing code between subsets of platforms.\n\n```kotlin\nkotlin {\n    androidTarget()\n    iosX64()\n    iosArm64()\n    iosSimulatorArm64()\n    jvm()\n    \n    sourceSets {\n        // Shared across ALL platforms\n        val commonMain by getting\n        \n        // Shared between Android and JVM only\n        val jvmCommonMain by creating {\n            dependsOn(commonMain)\n        }\n        val androidMain by getting {\n            dependsOn(jvmCommonMain)\n        }\n        val jvmMain by getting {\n            dependsOn(jvmCommonMain)\n        }\n        \n        // Shared across all iOS variants\n        val iosMain by creating {\n            dependsOn(commonMain)\n        }\n        val iosX64Main by getting { dependsOn(iosMain) }\n        val iosArm64Main by getting { dependsOn(iosMain) }\n        val iosSimulatorArm64Main by getting { dependsOn(iosMain) }\n        \n        // Shared across native platforms (iOS + desktop native)\n        val nativeMain by creating {\n            dependsOn(commonMain)\n        }\n    }\n}\n```\n\n### Why Hierarchical Source Sets?\n\n- Share JVM-specific code between Android and server\n- Share native-specific code between iOS and desktop\n- Reduce duplication while maintaining platform flexibility\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Sharing Business Logic",
                                "content":  "\n### Example: Shopping Cart\n\n**Shared Models (commonMain)**:\n\n**Shared Business Logic (commonMain)**:\n\n**Usage in Android**:\n\n**Usage in iOS**:\n\n---\n\n",
                                "code":  "// iOS ViewModel (Swift)\nclass ShoppingViewModel: ObservableObject {\n    private let cart = ShoppingCart() // Same shared code!\n\n    @Published var items: [CartItem] = []\n\n    func addToCart(product: Product) {\n        cart.addItem(product: product, quantity: 1)\n        items = cart.getItems()\n    }\n\n    var total: Double {\n        cart.getTotal()\n    }\n}",
                                "language":  "swift"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Platform-Specific Implementations",
                                "content":  "\n### Example: Storage Layer\n\n**Common Interface (commonMain)**:\n\n**Android Implementation (androidMain)**:\n\n**iOS Implementation (iosMain)**:\n\n**Usage in Shared Code**:\n\n---\n\n",
                                "code":  "class UserPreferences(private val storage: KeyValueStorage) {\n    fun saveUsername(username: String) {\n        storage.putString(\"username\", username)\n    }\n\n    fun getUsername(): String? {\n        return storage.getString(\"username\")\n    }\n\n    fun logout() {\n        storage.clear()\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Shared API Client",
                                "content":  "\n### Common Network Layer\n\n**API Client (commonMain)**:\n\n**Models (commonMain)**:\n\nThis API client works on **all platforms** without modification!\n\n---\n\n",
                                "code":  "package com.example.shared.api\n\nimport kotlinx.serialization.Serializable\n\n@Serializable\ndata class CreateOrderRequest(\n    val items: List\u003cOrderItem\u003e,\n    val totalAmount: Double\n)\n\n@Serializable\ndata class OrderItem(\n    val productId: String,\n    val quantity: Int,\n    val price: Double\n)\n\n@Serializable\ndata class Order(\n    val id: String,\n    val items: List\u003cOrderItem\u003e,\n    val totalAmount: Double,\n    val status: String,\n    val createdAt: String\n)",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 1: Build a Shared Authentication Module",
                                "content":  "\nCreate a KMP module that handles user authentication across platforms.\n\n### Requirements\n\n1. **Shared Models** (commonMain):\n   - `User` (id, email, name, token)\n   - `LoginRequest` (email, password)\n   - `RegisterRequest` (email, password, name)\n   - `AuthResponse` (success, user, token, message)\n\n2. **Shared AuthService** (commonMain):\n   - `login(email, password): Result\u003cUser\u003e`\n   - `register(request): Result\u003cUser\u003e`\n   - `logout()`\n   - `isLoggedIn(): Boolean`\n   - `getCurrentUser(): User?`\n\n3. **Platform-Specific Storage**:\n   - Android: Use SharedPreferences\n   - iOS: Use UserDefaults\n   - Store and retrieve auth token\n\n4. **Validation**:\n   - Email validation\n   - Password strength check (min 8 chars, 1 uppercase, 1 number)\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 1",
                                "content":  "\n**Common Models (commonMain)**:\n\n**Validation Utilities (commonMain)**:\n\n**Storage Interface (commonMain)**:\n\n**Android Storage (androidMain)**:\n\n**iOS Storage (iosMain)**:\n\n**Auth Service (commonMain)**:\n\n---\n\n",
                                "code":  "// shared/src/commonMain/kotlin/com/example/shared/service/AuthService.kt\npackage com.example.shared.service\n\nimport com.example.shared.models.*\nimport com.example.shared.storage.TokenStorage\nimport com.example.shared.utils.Validation\nimport io.ktor.client.*\nimport io.ktor.client.call.*\nimport io.ktor.client.request.*\nimport io.ktor.client.plugins.contentnegotiation.*\nimport io.ktor.serialization.kotlinx.json.*\nimport kotlinx.serialization.json.Json\n\nclass AuthService(\n    private val baseUrl: String,\n    private val tokenStorage: TokenStorage\n) {\n    private var currentUser: User? = null\n\n    private val client = HttpClient {\n        install(ContentNegotiation) {\n            json(Json { ignoreUnknownKeys = true })\n        }\n    }\n\n    suspend fun login(email: String, password: String): Result\u003cUser\u003e {\n        val request = LoginRequest(email, password)\n\n        // Validate\n        val validationError = Validation.validateLoginRequest(request)\n        if (validationError != null) {\n            return Result.failure(IllegalArgumentException(validationError))\n        }\n\n        return try {\n            val response: AuthResponse = client.post(\"$baseUrl/auth/login\") {\n                setBody(request)\n            }.body()\n\n            if (response.success \u0026\u0026 response.user != null \u0026\u0026 response.token != null) {\n                currentUser = response.user\n                tokenStorage.saveToken(response.token)\n                Result.success(response.user)\n            } else {\n                Result.failure(Exception(response.message ?: \"Login failed\"))\n            }\n        } catch (e: Exception) {\n            Result.failure(e)\n        }\n    }\n\n    suspend fun register(request: RegisterRequest): Result\u003cUser\u003e {\n        // Validate\n        val validationError = Validation.validateRegisterRequest(request)\n        if (validationError != null) {\n            return Result.failure(IllegalArgumentException(validationError))\n        }\n\n        return try {\n            val response: AuthResponse = client.post(\"$baseUrl/auth/register\") {\n                setBody(request)\n            }.body()\n\n            if (response.success \u0026\u0026 response.user != null \u0026\u0026 response.token != null) {\n                currentUser = response.user\n                tokenStorage.saveToken(response.token)\n                Result.success(response.user)\n            } else {\n                Result.failure(Exception(response.message ?: \"Registration failed\"))\n            }\n        } catch (e: Exception) {\n            Result.failure(e)\n        }\n    }\n\n    fun logout() {\n        currentUser = null\n        tokenStorage.clearToken()\n    }\n\n    fun isLoggedIn(): Boolean {\n        return tokenStorage.getToken() != null\n    }\n\n    fun getCurrentUser(): User? {\n        return currentUser\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 2: Create a Platform Logger",
                                "content":  "\nBuild a logging utility that uses platform-specific logging mechanisms.\n\n### Requirements\n\n1. Create `Logger` expect class with methods:\n   - `debug(tag: String, message: String)`\n   - `info(tag: String, message: String)`\n   - `warning(tag: String, message: String)`\n   - `error(tag: String, message: String, throwable: Throwable?)`\n\n2. Android: Use `android.util.Log`\n3. iOS: Use `NSLog`\n4. JVM: Use `println` with timestamps\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 2",
                                "content":  "\n**Common Declaration (commonMain)**:\n\n**Android Implementation (androidMain)**:\n\n**iOS Implementation (iosMain)**:\n\n**JVM Implementation (jvmMain)**:\n\n**Usage (commonMain)**:\n\n---\n\n",
                                "code":  "class UserRepository {\n    suspend fun getUser(id: String): User? {\n        Logger.debug(\"UserRepository\", \"Fetching user: $id\")\n\n        return try {\n            val user = api.getUser(id)\n            Logger.info(\"UserRepository\", \"User fetched successfully\")\n            user\n        } catch (e: Exception) {\n            Logger.error(\"UserRepository\", \"Failed to fetch user\", e)\n            null\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 3: Build a Shared Repository Pattern",
                                "content":  "\nCreate a repository that manages data fetching and caching across platforms.\n\n### Requirements\n\n1. **ProductRepository** (commonMain):\n   - Fetch products from API\n   - Cache in memory\n   - Return cached data if available and fresh (\u003c 5 minutes)\n   - Refresh from API if cache expired\n\n2. Use Ktor for networking\n3. Handle errors gracefully\n4. Log cache hits/misses\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 3",
                                "content":  "\n\n**Platform Implementations**:\n\n\n\n\n---\n\n",
                                "code":  "// shared/src/jvmMain/kotlin/com/example/shared/repository/Time.kt\npackage com.example.shared.repository\n\nactual fun getCurrentTimeMillis(): Long = System.currentTimeMillis()",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Compose Multiplatform - Share UI Across Platforms",
                                "content":  "\n**Compose Multiplatform** takes code sharing to the next level - you can share not just business logic, but also **UI code** across Android, iOS, Desktop, and Web!\n\n### What is Compose Multiplatform?\n\nDeveloped by JetBrains, Compose Multiplatform brings Jetpack Compose\u0027s declarative UI paradigm to all platforms:\n\n- **Android**: Uses Jetpack Compose directly\n- **iOS**: Compiles Compose to native iOS views\n- **Desktop**: Native Windows, macOS, Linux apps\n- **Web**: Kotlin/JS with Compose HTML\n\n### Code Sharing Comparison\n\n| What You Share | KMP Only | KMP + Compose Multiplatform |\n|----------------|----------|-----------------------------|\n| Business Logic | ✅ Yes | ✅ Yes |\n| Data Models | ✅ Yes | ✅ Yes |\n| API Clients | ✅ Yes | ✅ Yes |\n| **UI Components** | ❌ No | ✅ Yes! |\n| **Screens** | ❌ No | ✅ Yes! |\n\n### Compose Multiplatform Example\n\n```kotlin\n// shared/src/commonMain/kotlin/ui/ProductCard.kt\n// This UI code works on Android, iOS, Desktop, and Web!\n\n@Composable\nfun ProductCard(product: Product, onAddToCart: () -\u003e Unit) {\n    Card(\n        modifier = Modifier\n            .fillMaxWidth()\n            .padding(8.dp)\n    ) {\n        Column(modifier = Modifier.padding(16.dp)) {\n            Text(\n                text = product.name,\n                style = MaterialTheme.typography.headlineSmall\n            )\n            \n            Text(\n                text = \"${product.price}\",\n                style = MaterialTheme.typography.bodyLarge,\n                color = MaterialTheme.colorScheme.primary\n            )\n            \n            Spacer(modifier = Modifier.height(8.dp))\n            \n            Button(onClick = onAddToCart) {\n                Text(\"Add to Cart\")\n            }\n        }\n    }\n}\n\n// This composable runs on:\n// - Android phone\n// - iPhone/iPad\n// - Windows/macOS/Linux desktop\n// - Web browser\n```\n\n### Project Structure with Compose Multiplatform\n\n```\nproject/\n├── shared/\n│   └── src/\n│       ├── commonMain/\n│       │   └── kotlin/\n│       │       ├── ui/           # Shared UI components!\n│       │       │   ├── screens/\n│       │       │   ├── components/\n│       │       │   └── theme/\n│       │       ├── viewmodel/    # Shared ViewModels\n│       │       └── data/         # Shared data layer\n│       └── iosMain/\n│           └── kotlin/           # iOS-specific if needed\n├── androidApp/                    # Android entry point\n├── iosApp/                        # iOS entry point (Swift/SwiftUI)\n├── desktopApp/                    # Desktop entry point\n└── webApp/                        # Web entry point\n```\n\n### Platform-Specific UI When Needed\n\n```kotlin\n// expect/actual for platform-specific UI behavior\nexpect fun getPlatformName(): String\n\n@Composable\nfun WelcomeScreen() {\n    Text(\"Running on: ${getPlatformName()}\")\n}\n\n// androidMain\nactual fun getPlatformName() = \"Android\"\n\n// iosMain  \nactual fun getPlatformName() = \"iOS\"\n\n// desktopMain\nactual fun getPlatformName() = \"Desktop\"\n```\n\n### Real-World Usage\n\n**Companies using Compose Multiplatform:**\n- **JetBrains** - Toolbox App, Space\n- **McDonald\u0027s** - Mobile apps\n- **Philips** - Medical device UIs\n\n**When to Use Compose Multiplatform:**\n✅ New projects targeting multiple platforms\n✅ Internal/enterprise apps where consistent UX matters\n✅ Rapid prototyping across platforms\n✅ Teams with strong Kotlin expertise\n\n**When to Use Platform-Native UI:**\n✅ Apps requiring deep platform integration\n✅ Games with custom rendering\n✅ Apps where \"native feel\" is critical\n\n### Getting Started\n\n```kotlin\n// build.gradle.kts\nplugins {\n    kotlin(\"multiplatform\")\n    id(\"org.jetbrains.compose\") version \"1.6.0\"\n}\n\nkotlin {\n    androidTarget()\n    iosX64()\n    iosArm64()\n    jvm(\"desktop\")\n    \n    sourceSets {\n        val commonMain by getting {\n            dependencies {\n                implementation(compose.runtime)\n                implementation(compose.foundation)\n                implementation(compose.material3)\n                implementation(compose.ui)\n            }\n        }\n    }\n}\n```\n\n**Learn More**: https://www.jetbrains.com/lp/compose-multiplatform/\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why This Matters",
                                "content":  "\n### Real-World Impact\n\n**Companies Using KMP**:\n- **Netflix**: Shares business logic between Android and iOS apps\n- **VMware**: Uses KMP for cross-platform SDK\n- **Philips**: Medical devices with shared Bluetooth logic\n- **Cash App**: Payment processing logic shared across platforms\n- **Autodesk**: CAD software with shared rendering engine\n\n**Business Benefits**:\n- **40-70% code reuse** in production apps\n- **Faster time to market**: Build features once\n- **Fewer bugs**: Single source of truth\n- **Consistent UX**: Same logic = same behavior\n- **Easier maintenance**: Fix once, deploy everywhere\n\n**Developer Benefits**:\n- Write Kotlin (not Swift/Java/JavaScript)\n- Share tests across platforms\n- Type-safe, null-safe code everywhere\n- Use Kotlin coroutines on all platforms\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Checkpoint Quiz",
                                "content":  "\n### Question 1\nWhen using CocoaPods with KMP, what command generates the Podspec file?\n\nA) ./gradlew build\nB) ./gradlew podspec\nC) ./gradlew cocoapods\nD) pod install\n\n### Question 2\nWhat annotation makes Kotlin code more Swift-friendly by providing a custom name?\n\nA) @SwiftName\nB) @ObjCName\nC) @Export\nD) @JsName\n\n### Question 3\nWhat is cinterop used for in KMP?\n\nA) Calling Swift code from Kotlin\nB) Calling C libraries from Kotlin/Native\nC) Converting Kotlin to JavaScript\nD) Testing multiplatform code\n\n### Question 4\nHow do you create a platform-specific type alias in KMP?\n\nA) expect typealias Context = Any\nB) actual typealias PlatformContext = android.content.Context\nC) alias Context = android.content.Context\nD) typedef Context = android.content.Context\n\n### Question 5\nWhat is the main advantage of hierarchical source sets?\n\nA) Faster compilation\nB) Smaller binary size\nC) Share code between subsets of platforms (e.g., Android + JVM)\nD) Better IDE support\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Quiz Answers",
                                "content":  "\n**Question 1: B) ./gradlew podspec**\n\nThe CocoaPods plugin generates a Podspec file that iOS developers can use to integrate your KMP library.\n\n---\n\n**Question 2: B) @ObjCName**\n\nThe @ObjCName annotation lets you specify how Kotlin declarations appear to Swift/Objective-C consumers, improving API ergonomics.\n\n---\n\n**Question 3: B) Calling C libraries from Kotlin/Native**\n\nCinterop (C interoperability) generates Kotlin bindings for C headers, enabling you to use native C libraries like OpenSSL, SQLite, etc.\n\n---\n\n**Question 4: B) actual typealias PlatformContext = android.content.Context**\n\nType aliases let you map expect declarations to existing platform types without wrapping them.\n\n---\n\n**Question 5: C) Share code between subsets of platforms**\n\nHierarchical source sets allow sharing code between specific platform groups (e.g., jvmCommonMain for Android + server, nativeMain for iOS + desktop native).\n\n---\n\n",
                                "code":  "// Hierarchical source set example\nval jvmCommonMain by creating {\n    dependsOn(commonMain)\n}\nval androidMain by getting {\n    dependsOn(jvmCommonMain)  // Share JVM code with Android\n}\nval jvmMain by getting {\n    dependsOn(jvmCommonMain)  // Share JVM code with server\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What You\u0027ve Learned",
                                "content":  "\n✅ Swift interoperability: Calling Kotlin from Swift, nullability, sealed classes\n✅ CocoaPods integration: Consuming iOS libraries and publishing as pods\n✅ C interop (cinterop): Calling native C libraries from Kotlin/Native\n✅ Publishing KMP libraries: Maven, GitHub Packages, Maven Central\n✅ Advanced expect/actual patterns: Classes with constructors, type aliases, singletons\n✅ Hierarchical source sets: Sharing code between platform subsets\n✅ Compose Multiplatform: Sharing UI code across platforms\n✅ Real-world production patterns and best practices\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Next Steps",
                                "content":  "\nIn **Lesson 7.2: Testing Strategies**, you\u0027ll learn:\n- Unit testing with JUnit 5 and Kotest\n- Mocking with MockK\n- Testing coroutines and flows\n- Testing Jetpack Compose UI\n- Test-driven development (TDD)\n- Measuring and improving code coverage\n\nTesting shared KMP code ensures it works correctly on all platforms!\n\n---\n\n"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "7.1.1",
                           "title":  "API Request/Response Models",
                           "description":  "Create data classes for a task management API: Task, CreateTaskRequest, and UpdateTaskRequest.",
                           "instructions":  "Create data classes for a task management API: Task, CreateTaskRequest, and UpdateTaskRequest.",
                           "starterCode":  "// Create Task data class with: id, title, description, completed, priority, userId\n\n// Create CreateTaskRequest with: title, description, priority\n\n// Create UpdateTaskRequest with nullable fields: title?, description?, completed?\n\nfun main() {\n    val task = Task(1, \"Learn Kotlin\", \"Complete course\", false, \"high\", 1)\n    val createRequest = CreateTaskRequest(\"New Task\", \"Description\", \"medium\")\n    val updateRequest = UpdateTaskRequest(null, null, true)\n    \n    println(task)\n    println(createRequest)\n    println(updateRequest)\n}",
                           "solution":  "data class Task(\n    val id: Int,\n    val title: String,\n    val description: String,\n    val completed: Boolean,\n    val priority: String,\n    val userId: Int\n)\n\ndata class CreateTaskRequest(\n    val title: String,\n    val description: String,\n    val priority: String\n)\n\ndata class UpdateTaskRequest(\n    val title: String?,\n    val description: String?,\n    val completed: Boolean?\n)\n\nfun main() {\n    val task = Task(1, \"Learn Kotlin\", \"Complete course\", false, \"high\", 1)\n    val createRequest = CreateTaskRequest(\"New Task\", \"Description\", \"medium\")\n    val updateRequest = UpdateTaskRequest(null, null, true)\n    \n    println(task)\n    println(createRequest)\n    println(updateRequest)\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Should create task model",
                                                 "expectedOutput":  "Task(id=1, title=Learn Kotlin",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Should create request models",
                                                 "expectedOutput":  "CreateTaskRequest(title=New Task",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-3",
                                                 "description":  "Should handle nullable updates",
                                                 "expectedOutput":  "UpdateTaskRequest(title=null, description=null, completed=true)",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use data class for all models"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Task has all required fields"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "CreateTaskRequest doesn\u0027t include id (server generates it)"
                                         },
                                         {
                                             "level":  4,
                                             "text":  "UpdateTaskRequest has nullable fields (only update what\u0027s provided)"
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
                           "id":  "7.1.2",
                           "title":  "Task Repository Pattern",
                           "description":  "Create a TaskRepository class with in-memory storage for CRUD operations on tasks.",
                           "instructions":  "Create a TaskRepository class with in-memory storage for CRUD operations on tasks.",
                           "starterCode":  "data class Task(\n    val id: Int,\n    val title: String,\n    val description: String,\n    var completed: Boolean,\n    val userId: Int\n)\n\nclass TaskRepository {\n    private val tasks = mutableListOf\u003cTask\u003e()\n    private var nextId = 1\n    \n    fun create(title: String, description: String, userId: Int): Task {\n        // Create and add task\n    }\n    \n    fun findAll(userId: Int): List\u003cTask\u003e {\n        // Find all tasks for user\n    }\n    \n    fun findById(id: Int): Task? {\n        // Find task by ID\n    }\n    \n    fun update(id: Int, title: String?, description: String?, completed: Boolean?): Boolean {\n        // Update task fields if not null\n    }\n    \n    fun delete(id: Int): Boolean {\n        // Delete task\n    }\n}\n\nfun main() {\n    val repo = TaskRepository()\n    val task1 = repo.create(\"Task 1\", \"Description 1\", 1)\n    val task2 = repo.create(\"Task 2\", \"Description 2\", 1)\n    \n    println(\"All tasks: ${repo.findAll(1)}\")\n    repo.update(1, null, null, true)\n    println(\"After update: ${repo.findById(1)}\")\n    repo.delete(2)\n    println(\"After delete: ${repo.findAll(1).size}\")\n}",
                           "solution":  "data class Task(\n    val id: Int,\n    val title: String,\n    val description: String,\n    var completed: Boolean,\n    val userId: Int\n)\n\nclass TaskRepository {\n    private val tasks = mutableListOf\u003cTask\u003e()\n    private var nextId = 1\n    \n    fun create(title: String, description: String, userId: Int): Task {\n        val task = Task(nextId++, title, description, false, userId)\n        tasks.add(task)\n        return task\n    }\n    \n    fun findAll(userId: Int): List\u003cTask\u003e {\n        return tasks.filter { it.userId == userId }\n    }\n    \n    fun findById(id: Int): Task? {\n        return tasks.find { it.id == id }\n    }\n    \n    fun update(id: Int, title: String?, description: String?, completed: Boolean?): Boolean {\n        val task = findById(id) ?: return false\n        val index = tasks.indexOf(task)\n        tasks[index] = task.copy(\n            title = title ?: task.title,\n            description = description ?: task.description,\n            completed = completed ?: task.completed\n        )\n        return true\n    }\n    \n    fun delete(id: Int): Boolean {\n        return tasks.removeIf { it.id == id }\n    }\n}\n\nfun main() {\n    val repo = TaskRepository()\n    val task1 = repo.create(\"Task 1\", \"Description 1\", 1)\n    val task2 = repo.create(\"Task 2\", \"Description 2\", 1)\n    \n    println(\"All tasks: ${repo.findAll(1)}\")\n    repo.update(1, null, null, true)\n    println(\"After update: ${repo.findById(1)}\")\n    repo.delete(2)\n    println(\"After delete: ${repo.findAll(1).size}\")\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Should create and find all tasks",
                                                 "expectedOutput":  "All tasks: [Task(id=1",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Should update task",
                                                 "expectedOutput":  "completed=true",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-3",
                                                 "description":  "Should delete task",
                                                 "expectedOutput":  "After delete: 1",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use mutableListOf for storage"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Auto-increment nextId for new tasks"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "filter() returns tasks matching userId"
                                         },
                                         {
                                             "level":  4,
                                             "text":  "Use Elvis operator ?: to keep existing values on update"
                                         },
                                         {
                                             "level":  5,
                                             "text":  "removeIf() returns true if element was removed"
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
                           "difficulty":  "advanced"
                       },
                       {
                           "type":  "FREE_CODING",
                           "id":  "7.1.3",
                           "title":  "Authentication Token Management",
                           "description":  "Create a simple session manager that generates tokens and validates them.",
                           "instructions":  "Create a simple session manager that generates tokens and validates them.",
                           "starterCode":  "import java.util.UUID\n\ndata class Session(val userId: Int, val token: String)\n\nclass SessionManager {\n    private val sessions = mutableMapOf\u003cString, Session\u003e()\n    \n    fun createSession(userId: Int): String {\n        // Generate token and store session\n    }\n    \n    fun validateToken(token: String): Int? {\n        // Return userId if valid, null otherwise\n    }\n    \n    fun removeSession(token: String) {\n        // Remove session\n    }\n}\n\nfun main() {\n    val manager = SessionManager()\n    val token = manager.createSession(1)\n    println(\"Token created: $token\")\n    println(\"User ID: ${manager.validateToken(token)}\")\n    println(\"Invalid token: ${manager.validateToken(\"invalid\")}\")\n    manager.removeSession(token)\n    println(\"After logout: ${manager.validateToken(token)}\")\n}",
                           "solution":  "import java.util.UUID\n\ndata class Session(val userId: Int, val token: String)\n\nclass SessionManager {\n    private val sessions = mutableMapOf\u003cString, Session\u003e()\n    \n    fun createSession(userId: Int): String {\n        val token = UUID.randomUUID().toString()\n        sessions[token] = Session(userId, token)\n        return token\n    }\n    \n    fun validateToken(token: String): Int? {\n        return sessions[token]?.userId\n    }\n    \n    fun removeSession(token: String) {\n        sessions.remove(token)\n    }\n}\n\nfun main() {\n    val manager = SessionManager()\n    val token = manager.createSession(1)\n    println(\"Token created: $token\")\n    println(\"User ID: ${manager.validateToken(token)}\")\n    println(\"Invalid token: ${manager.validateToken(\"invalid\")}\")\n    manager.removeSession(token)\n    println(\"After logout: ${manager.validateToken(token)}\")\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Should create token",
                                                 "expectedOutput":  "Token created:",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Should validate token and return user ID",
                                                 "expectedOutput":  "User ID: 1",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-3",
                                                 "description":  "Should return null for invalid token",
                                                 "expectedOutput":  "Invalid token: null",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-4",
                                                 "description":  "Should invalidate after logout",
                                                 "expectedOutput":  "After logout: null",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use UUID.randomUUID().toString() to generate unique token"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Store sessions in Map with token as key"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "Use safe call ?. to handle missing sessions"
                                         },
                                         {
                                             "level":  4,
                                             "text":  "remove() deletes from map"
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
                           "id":  "7.1.4",
                           "title":  "Full-Stack Integration Simulation",
                           "description":  "Simulate a complete flow: create user, login, create task, fetch tasks. This ties together authentication and task management.",
                           "instructions":  "Simulate a complete flow: create user, login, create task, fetch tasks. This ties together authentication and task management.",
                           "starterCode":  "data class User(val id: Int, val username: String, val email: String)\ndata class Task(val id: Int, val title: String, val completed: Boolean, val userId: Int)\n\nclass Application {\n    private val users = mutableMapOf\u003cInt, User\u003e()\n    private val tasks = mutableListOf\u003cTask\u003e()\n    private val sessions = mutableMapOf\u003cString, Int\u003e() // token to userId\n    private var nextUserId = 1\n    private var nextTaskId = 1\n    \n    fun register(username: String, email: String): User {\n        // Create and store user\n    }\n    \n    fun login(userId: Int): String {\n        // Create session and return token\n    }\n    \n    fun createTask(token: String, title: String): Task? {\n        // Validate token, create task\n    }\n    \n    fun getTasks(token: String): List\u003cTask\u003e? {\n        // Validate token, return user\u0027s tasks\n    }\n}\n\nfun main() {\n    val app = Application()\n    val user = app.register(\"alice\", \"alice@example.com\")\n    println(\"User registered: $user\")\n    \n    val token = app.login(user.id)\n    println(\"Logged in with token\")\n    \n    app.createTask(token, \"Learn Kotlin\")\n    app.createTask(token, \"Build App\")\n    \n    val tasks = app.getTasks(token)\n    println(\"Tasks: $tasks\")\n}",
                           "solution":  "import java.util.UUID\n\ndata class User(val id: Int, val username: String, val email: String)\ndata class Task(val id: Int, val title: String, val completed: Boolean, val userId: Int)\n\nclass Application {\n    private val users = mutableMapOf\u003cInt, User\u003e()\n    private val tasks = mutableListOf\u003cTask\u003e()\n    private val sessions = mutableMapOf\u003cString, Int\u003e() // token to userId\n    private var nextUserId = 1\n    private var nextTaskId = 1\n    \n    fun register(username: String, email: String): User {\n        val user = User(nextUserId++, username, email)\n        users[user.id] = user\n        return user\n    }\n    \n    fun login(userId: Int): String {\n        val token = UUID.randomUUID().toString()\n        sessions[token] = userId\n        return token\n    }\n    \n    fun createTask(token: String, title: String): Task? {\n        val userId = sessions[token] ?: return null\n        val task = Task(nextTaskId++, title, false, userId)\n        tasks.add(task)\n        return task\n    }\n    \n    fun getTasks(token: String): List\u003cTask\u003e? {\n        val userId = sessions[token] ?: return null\n        return tasks.filter { it.userId == userId }\n    }\n}\n\nfun main() {\n    val app = Application()\n    val user = app.register(\"alice\", \"alice@example.com\")\n    println(\"User registered: $user\")\n    \n    val token = app.login(user.id)\n    println(\"Logged in with token\")\n    \n    app.createTask(token, \"Learn Kotlin\")\n    app.createTask(token, \"Build App\")\n    \n    val tasks = app.getTasks(token)\n    println(\"Tasks: $tasks\")\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Should register user",
                                                 "expectedOutput":  "User registered: User(id=1, username=alice",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Should login",
                                                 "expectedOutput":  "Logged in with token",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-3",
                                                 "description":  "Should fetch tasks",
                                                 "expectedOutput":  "Tasks: [Task(id=1, title=Learn Kotlin",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Store users in Map by ID"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Store tasks in List"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "Sessions map token to userId"
                                         },
                                         {
                                             "level":  4,
                                             "text":  "Validate token by checking if it exists in sessions"
                                         },
                                         {
                                             "level":  5,
                                             "text":  "Filter tasks by userId"
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
                           "difficulty":  "advanced"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Lesson 7.1: Advanced KMP Patterns",
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
- Search for "kotlin Lesson 7.1: Advanced KMP Patterns 2024 2025" to find latest practices
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
  "lessonId": "7.1",
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

