# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Professional Development & Deployment
- **Lesson:** Lesson 7.4: Security Best Practices (ID: 7.4)
- **Difficulty:** advanced
- **Estimated Time:** 90 minutes

## Current Lesson Content

{
    "id":  "7.4",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 90 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "\nSecurity isn\u0027t optional - it\u0027s your responsibility as a developer.\n\nA single security vulnerability can:\n- Expose millions of user credentials\n- Cost companies millions in damages\n- Destroy user trust forever\n- End careers\n\nIn this lesson, you\u0027ll master security best practices for Kotlin applications:\n- ✅ Secure coding principles\n- ✅ Input validation and sanitization\n- ✅ Encryption and hashing\n- ✅ API security (OAuth 2.0, JWT)\n- ✅ Android security (KeyStore, ProGuard/R8)\n- ✅ OWASP Top 10 vulnerabilities\n\nBy the end, you\u0027ll build applications that protect user data and withstand attacks.\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "The Cost of Insecurity",
                                "content":  "\n### Real-World Breaches\n\n**Equifax (2017)**:\n- Vulnerability: Unpatched Apache Struts\n- Impact: 147 million records exposed\n- Cost: $1.4 billion in damages\n- Cause: Security neglect\n\n**Facebook (2019)**:\n- Vulnerability: Passwords stored in plaintext\n- Impact: 600 million passwords exposed\n- Cause: Not hashing passwords\n\n**Uber (2016)**:\n- Vulnerability: AWS keys in GitHub repository\n- Impact: 57 million users compromised\n- Cost: $148 million fine\n- Cause: Hardcoded secrets\n\n**The Pattern**: These weren\u0027t sophisticated attacks. They were basic security mistakes that could have been prevented.\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Secure Coding Principles",
                                "content":  "\n### Principle 1: Defense in Depth\n\nNever rely on a single security measure.\n\n❌ **Bad** (Single layer):\n\n✅ **Good** (Multiple layers):\n\n### Principle 2: Least Privilege\n\nGrant minimum permissions necessary.\n\n❌ **Bad** (Admin for everyone):\n\n✅ **Good** (Minimal permissions):\n\n### Principle 3: Fail Securely\n\nWhen errors occur, fail in a secure state.\n\n❌ **Bad** (Fails open):\n\n✅ **Good** (Fails closed):\n\n---\n\n",
                                "code":  "fun checkAccess(userId: String, resourceId: String): Boolean {\n    return try {\n        val user = userService.getUser(userId) ?: return false\n        val resource = resourceService.getResource(resourceId) ?: return false\n        user.hasAccessTo(resource)\n    } catch (e: Exception) {\n        logger.error(\"Access check failed\", e)\n        // ✅ Error = deny access\n        false\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Input Validation",
                                "content":  "\n### Never Trust User Input\n\n**Golden Rule**: All input is malicious until proven otherwise.\n\n### SQL Injection Prevention\n\n❌ **DANGER** (SQL Injection vulnerable):\n\n✅ **Safe** (Parameterized queries):\n\n### XSS Prevention\n\n❌ **Bad** (XSS vulnerable):\n\n✅ **Good** (Sanitized):\n\n### Email Validation\n\n❌ **Bad** (Weak validation):\n\n✅ **Good** (Robust validation):\n\n### Path Traversal Prevention\n\n❌ **DANGER** (Path traversal):\n\n✅ **Safe** (Validated path):\n\n---\n\n",
                                "code":  "fun getFile(filename: String): File? {\n    // Validate filename\n    if (filename.contains(\"..\") || filename.contains(\"/\")) {\n        logger.warn(\"Path traversal attempt: $filename\")\n        return null\n    }\n\n    val file = File(\"/uploads\", filename).canonicalFile\n    val uploadDir = File(\"/uploads\").canonicalFile\n\n    // Ensure file is within upload directory\n    if (!file.path.startsWith(uploadDir.path)) {\n        logger.warn(\"Path traversal detected: $filename\")\n        return null\n    }\n\n    return file\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Password Security",
                                "content":  "\n### Hashing with bcrypt\n\n❌ **NEVER** (Plaintext):\n\n❌ **BAD** (Simple hash):\n\n✅ **GOOD** (bcrypt):\n\n### Password Strength Requirements\n\n\n---\n\n",
                                "code":  "object PasswordValidator {\n    data class ValidationResult(\n        val isValid: Boolean,\n        val errors: List\u003cString\u003e\n    )\n\n    fun validate(password: String): ValidationResult {\n        val errors = mutableListOf\u003cString\u003e()\n\n        if (password.length \u003c 8) {\n            errors.add(\"Password must be at least 8 characters\")\n        }\n\n        if (!password.any { it.isUpperCase() }) {\n            errors.add(\"Password must contain an uppercase letter\")\n        }\n\n        if (!password.any { it.isLowerCase() }) {\n            errors.add(\"Password must contain a lowercase letter\")\n        }\n\n        if (!password.any { it.isDigit() }) {\n            errors.add(\"Password must contain a number\")\n        }\n\n        if (!password.any { \"!@#$%^\u0026*()_+-=[]{}|;:,.\u003c\u003e?\".contains(it) }) {\n            errors.add(\"Password must contain a special character\")\n        }\n\n        // Check against common passwords\n        if (isCommonPassword(password)) {\n            errors.add(\"Password is too common\")\n        }\n\n        return ValidationResult(errors.isEmpty(), errors)\n    }\n\n    private fun isCommonPassword(password: String): Boolean {\n        val common = setOf(\n            \"password\", \"12345678\", \"qwerty\", \"abc123\",\n            \"password123\", \"admin\", \"letmein\"\n        )\n        return password.lowercase() in common\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "JWT Security",
                                "content":  "\n### Secure JWT Implementation\n\n❌ **Bad** (Insecure):\n\n✅ **Good** (Secure):\n\n### Refresh Tokens\n\n\n---\n\n",
                                "code":  "@Entity\ndata class RefreshToken(\n    @PrimaryKey val id: String = UUID.randomUUID().toString(),\n    val userId: String,\n    val token: String,\n    val expiresAt: Long,\n    val createdAt: Long = System.currentTimeMillis()\n)\n\nobject TokenService {\n    private const val REFRESH_TOKEN_EXPIRATION = 7 * 24 * 3600000L // 7 days\n\n    fun generateTokenPair(user: User): TokenPair {\n        val accessToken = JwtConfig.generateToken(user)\n\n        val refreshToken = RefreshToken(\n            userId = user.id,\n            token = generateSecureRandomToken(),\n            expiresAt = System.currentTimeMillis() + REFRESH_TOKEN_EXPIRATION\n        )\n\n        refreshTokenRepository.save(refreshToken)\n\n        return TokenPair(accessToken, refreshToken.token)\n    }\n\n    suspend fun refreshAccessToken(refreshToken: String): String? {\n        val token = refreshTokenRepository.findByToken(refreshToken) ?: return null\n\n        if (token.expiresAt \u003c System.currentTimeMillis()) {\n            refreshTokenRepository.delete(token.id)\n            return null\n        }\n\n        val user = userRepository.findById(token.userId) ?: return null\n\n        return JwtConfig.generateToken(user)\n    }\n\n    private fun generateSecureRandomToken(): String {\n        val bytes = ByteArray(32)\n        SecureRandom().nextBytes(bytes)\n        return bytes.joinToString(\"\") { \"%02x\".format(it) }\n    }\n}\n\ndata class TokenPair(\n    val accessToken: String,\n    val refreshToken: String\n)",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Android Security",
                                "content":  "\n### KeyStore for Secrets\n\n❌ **Bad** (Hardcoded secrets):\n\n✅ **Good** (KeyStore):\n\n### ProGuard/R8 Configuration\n\n\n**proguard-rules.pro**:\n\n### Certificate Pinning\n\n\n---\n\n",
                                "code":  "// build.gradle.kts\ndependencies {\n    implementation(\"com.squareup.okhttp3:okhttp:4.12.0\")\n}\n\n// Certificate pinning\nval certificatePinner = CertificatePinner.Builder()\n    .add(\n        \"api.example.com\",\n        \"sha256/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=\"\n    )\n    .build()\n\nval client = OkHttpClient.Builder()\n    .certificatePinner(certificatePinner)\n    .build()\n\n// Get SHA256 hash:\n// openssl s_client -connect api.example.com:443 | openssl x509 -pubkey -noout | openssl pkey -pubin -outform der | openssl dgst -sha256 -binary | openssl enc -base64",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "OWASP Top 10",
                                "content":  "\n### 1. Broken Access Control\n\n❌ **Bad**:\n\n✅ **Good**:\n\n### 2. Cryptographic Failures\n\n✅ **Use HTTPS everywhere**:\n\n### 3. Injection\n\n✅ **Always use parameterized queries** (shown earlier)\n\n### 4. Insecure Design\n\n✅ **Security by design**:\n\n### 5. Security Misconfiguration\n\n✅ **Secure defaults**:\n\n---\n\n",
                                "code":  "// application.conf\nktor {\n    deployment {\n        port = 8080\n        watch = []  # Disable auto-reload in production\n    }\n    application {\n        modules = [ com.example.ApplicationKt.module ]\n    }\n}\n\nsecurity {\n    ssl {\n        enabled = true\n        keyStore = ${?SSL_KEY_STORE}\n        keyStorePassword = ${?SSL_KEY_STORE_PASSWORD}\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 1: Secure User Registration",
                                "content":  "\nBuild a secure user registration system.\n\n### Requirements\n\n1. **Password Requirements**:\n   - Minimum 12 characters\n   - Uppercase, lowercase, number, special char\n   - Not in common password list\n\n2. **Email Validation**:\n   - Valid format\n   - Domain verification (MX record check)\n   - Unique in database\n\n3. **Security Features**:\n   - Hash passwords with bcrypt (cost 12)\n   - Email verification required\n   - Rate limiting (5 attempts per hour per IP)\n   - CAPTCHA on repeated failures\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 1",
                                "content":  "\n\n---\n\n",
                                "code":  "// Password validator\nobject PasswordValidator {\n    private val commonPasswords = setOf(\n        \"password123\", \"qwerty123\", \"admin123\",\n        // ... load from file\n    )\n\n    fun validate(password: String): ValidationResult {\n        val errors = mutableListOf\u003cString\u003e()\n\n        if (password.length \u003c 12) {\n            errors.add(\"Password must be at least 12 characters\")\n        }\n\n        if (!password.any { it.isUpperCase() }) {\n            errors.add(\"Must contain uppercase letter\")\n        }\n\n        if (!password.any { it.isLowerCase() }) {\n            errors.add(\"Must contain lowercase letter\")\n        }\n\n        if (!password.any { it.isDigit() }) {\n            errors.add(\"Must contain number\")\n        }\n\n        if (!password.any { \"!@#$%^\u0026*()\".contains(it) }) {\n            errors.add(\"Must contain special character\")\n        }\n\n        if (password.lowercase() in commonPasswords) {\n            errors.add(\"Password is too common\")\n        }\n\n        return ValidationResult(errors.isEmpty(), errors)\n    }\n}\n\n// Email validator with DNS check\nobject EmailValidator {\n    fun validate(email: String): ValidationResult {\n        val errors = mutableListOf\u003cString\u003e()\n\n        if (!basicValidation(email)) {\n            errors.add(\"Invalid email format\")\n            return ValidationResult(false, errors)\n        }\n\n        val domain = email.substringAfter(\"@\")\n        if (!hasMXRecord(domain)) {\n            errors.add(\"Email domain does not exist\")\n        }\n\n        return ValidationResult(errors.isEmpty(), errors)\n    }\n\n    private fun basicValidation(email: String): Boolean {\n        val pattern = Regex(\"^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+\\\\.[A-Za-z]{2,}$\")\n        return email.matches(pattern)\n    }\n\n    private fun hasMXRecord(domain: String): Boolean {\n        return try {\n            val attributes = InitialDirContext().getAttributes(\n                \"dns:/$domain\",\n                arrayOf(\"MX\")\n            )\n            attributes.get(\"MX\") != null\n        } catch (e: Exception) {\n            false\n        }\n    }\n}\n\n// Rate limiter\nclass RateLimiter(private val maxAttempts: Int, private val windowMs: Long) {\n    // Note: ConcurrentHashMap is correct here for thread-safe concurrent access\n    // mutableMapOf() is NOT thread-safe and would cause race conditions\n    private val attempts = ConcurrentHashMap\u003cString, MutableList\u003cLong\u003e\u003e()\n\n    fun isAllowed(key: String): Boolean {\n        val now = System.currentTimeMillis()\n        val userAttempts = attempts.getOrPut(key) { mutableListOf() }\n\n        // Remove old attempts\n        userAttempts.removeIf { it \u003c now - windowMs }\n\n        if (userAttempts.size \u003e= maxAttempts) {\n            return false\n        }\n\n        userAttempts.add(now)\n        return true\n    }\n}\n\n// Registration service\nclass RegistrationService(\n    private val userRepository: UserRepository,\n    private val emailService: EmailService,\n    private val rateLimiter: RateLimiter\n) {\n    suspend fun register(\n        email: String,\n        password: String,\n        ipAddress: String\n    ): Result\u003cUser\u003e {\n        // Rate limiting\n        if (!rateLimiter.isAllowed(ipAddress)) {\n            return Result.failure(RateLimitException(\"Too many registration attempts\"))\n        }\n\n        // Validate email\n        val emailValidation = EmailValidator.validate(email)\n        if (!emailValidation.isValid) {\n            return Result.failure(ValidationException(emailValidation.errors))\n        }\n\n        // Check uniqueness\n        if (userRepository.existsByEmail(email)) {\n            return Result.failure(ValidationException(\"Email already registered\"))\n        }\n\n        // Validate password\n        val passwordValidation = PasswordValidator.validate(password)\n        if (!passwordValidation.isValid) {\n            return Result.failure(ValidationException(passwordValidation.errors))\n        }\n\n        // Hash password\n        val passwordHash = BCrypt.hashpw(password, BCrypt.gensalt(12))\n\n        // Create user (unverified)\n        val user = User(\n            id = UUID.randomUUID().toString(),\n            email = email,\n            passwordHash = passwordHash,\n            emailVerified = false,\n            createdAt = System.currentTimeMillis()\n        )\n\n        userRepository.save(user)\n\n        // Send verification email\n        val verificationToken = generateVerificationToken(user.id)\n        emailService.sendVerificationEmail(email, verificationToken)\n\n        return Result.success(user)\n    }\n\n    private fun generateVerificationToken(userId: String): String {\n        val token = UUID.randomUUID().toString()\n        // Save token with expiration (24 hours)\n        return token\n    }\n}\n\ndata class ValidationResult(val isValid: Boolean, val errors: List\u003cString\u003e)",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 2: Implement API Rate Limiting",
                                "content":  "\nCreate a rate limiting middleware for Ktor.\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 2",
                                "content":  "\n\n---\n\n",
                                "code":  "class RateLimitPlugin(private val config: Configuration) {\n    class Configuration {\n        var maxRequests: Int = 100\n        var windowMs: Long = 60000 // 1 minute\n        var keyExtractor: (ApplicationCall) -\u003e String = { call -\u003e\n            call.request.origin.remoteHost\n        }\n    }\n\n    companion object Feature : ApplicationPlugin\u003cApplication, Configuration, RateLimitPlugin\u003e {\n        override val key = AttributeKey\u003cRateLimitPlugin\u003e(\"RateLimit\")\n\n        // ConcurrentHashMap is correct for thread-safe server-side state\n        private val rateLimitData = ConcurrentHashMap\u003cString, RateLimitInfo\u003e()\n\n        override fun install(\n            pipeline: Application,\n            configure: Configuration.() -\u003e Unit\n        ): RateLimitPlugin {\n            val config = Configuration().apply(configure)\n            val plugin = RateLimitPlugin(config)\n\n            pipeline.intercept(ApplicationCallPipeline.Plugins) {\n                val key = config.keyExtractor(call)\n                val now = System.currentTimeMillis()\n\n                val info = rateLimitData.getOrPut(key) {\n                    RateLimitInfo(mutableListOf(), now)\n                }\n\n                synchronized(info) {\n                    // Clean old requests\n                    info.requests.removeIf { it \u003c now - config.windowMs }\n\n                    if (info.requests.size \u003e= config.maxRequests) {\n                        call.response.headers.append(\"X-RateLimit-Limit\", config.maxRequests.toString())\n                        call.response.headers.append(\"X-RateLimit-Remaining\", \"0\")\n                        call.response.headers.append(\"Retry-After\", \"60\")\n\n                        call.respond(HttpStatusCode.TooManyRequests, mapOf(\n                            \"error\" to \"Rate limit exceeded\",\n                            \"limit\" to config.maxRequests,\n                            \"window\" to \"${config.windowMs / 1000}s\"\n                        ))\n                        finish()\n                        return@intercept\n                    }\n\n                    info.requests.add(now)\n\n                    call.response.headers.append(\"X-RateLimit-Limit\", config.maxRequests.toString())\n                    call.response.headers.append(\n                        \"X-RateLimit-Remaining\",\n                        (config.maxRequests - info.requests.size).toString()\n                    )\n                }\n            }\n\n            return plugin\n        }\n    }\n\n    private data class RateLimitInfo(\n        val requests: MutableList\u003cLong\u003e,\n        val windowStart: Long\n    )\n}\n\n// Usage\nfun Application.module() {\n    install(RateLimitPlugin) {\n        maxRequests = 100\n        windowMs = 60000 // 1 minute\n\n        keyExtractor = { call -\u003e\n            // Use authenticated user ID if available, else IP\n            call.principal\u003cUserPrincipal\u003e()?.id\n                ?: call.request.origin.remoteHost\n        }\n    }\n\n    routing {\n        get(\"/api/data\") {\n            call.respond(\"Hello!\")\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 3: Secure File Upload",
                                "content":  "\nCreate a secure file upload endpoint.\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 3",
                                "content":  "\n\n---\n\n",
                                "code":  "class FileUploadService(\n    private val uploadDir: File,\n    private val maxFileSize: Long = 10 * 1024 * 1024, // 10 MB\n    private val allowedExtensions: Set\u003cString\u003e = setOf(\"jpg\", \"png\", \"pdf\")\n) {\n    init {\n        if (!uploadDir.exists()) {\n            uploadDir.mkdirs()\n        }\n    }\n\n    suspend fun upload(\n        file: MultiPartData,\n        userId: String\n    ): Result\u003cUploadedFile\u003e {\n        var uploadedFile: UploadedFile? = null\n        var tempFile: File? = null\n\n        try {\n            file.forEachPart { part -\u003e\n                when (part) {\n                    is PartData.FileItem -\u003e {\n                        val fileName = part.originalFileName ?: return@forEachPart\n\n                        // Validate filename\n                        if (!isValidFilename(fileName)) {\n                            return Result.failure(ValidationException(\"Invalid filename\"))\n                        }\n\n                        // Validate extension\n                        val extension = fileName.substringAfterLast(\".\", \"\")\n                        if (extension.lowercase() !in allowedExtensions) {\n                            return Result.failure(\n                                ValidationException(\"File type not allowed. Allowed: $allowedExtensions\")\n                            )\n                        }\n\n                        // Generate safe filename\n                        val safeFilename = \"${UUID.randomUUID()}.${extension.lowercase()}\"\n                        tempFile = File(uploadDir, safeFilename)\n\n                        var size = 0L\n                        tempFile!!.outputStream().use { output -\u003e\n                            part.streamProvider().use { input -\u003e\n                                val buffer = ByteArray(8192)\n                                var bytesRead: Int\n\n                                while (input.read(buffer).also { bytesRead = it } != -1) {\n                                    size += bytesRead\n\n                                    if (size \u003e maxFileSize) {\n                                        return Result.failure(\n                                            ValidationException(\"File too large. Max: ${maxFileSize / 1024 / 1024}MB\")\n                                        )\n                                    }\n\n                                    output.write(buffer, 0, bytesRead)\n                                }\n                            }\n                        }\n\n                        // Validate file type (magic numbers)\n                        if (!isValidFileType(tempFile!!, extension)) {\n                            tempFile!!.delete()\n                            return Result.failure(ValidationException(\"File content doesn\u0027t match extension\"))\n                        }\n\n                        // Scan for malware (integrate with antivirus)\n                        if (containsMalware(tempFile!!)) {\n                            tempFile!!.delete()\n                            return Result.failure(SecurityException(\"Malware detected\"))\n                        }\n\n                        uploadedFile = UploadedFile(\n                            id = UUID.randomUUID().toString(),\n                            originalFilename = fileName,\n                            storedFilename = safeFilename,\n                            extension = extension,\n                            size = size,\n                            uploadedBy = userId,\n                            uploadedAt = System.currentTimeMillis()\n                        )\n                    }\n                    else -\u003e {}\n                }\n                part.dispose()\n            }\n\n            return uploadedFile?.let { Result.success(it) }\n                ?: Result.failure(Exception(\"No file uploaded\"))\n\n        } catch (e: Exception) {\n            tempFile?.delete()\n            return Result.failure(e)\n        }\n    }\n\n    private fun isValidFilename(filename: String): Boolean {\n        // No path traversal\n        if (filename.contains(\"..\") || filename.contains(\"/\") || filename.contains(\"\\\\\")) {\n            return false\n        }\n\n        // No special characters\n        if (!filename.matches(Regex(\"^[a-zA-Z0-9._-]+$\"))) {\n            return false\n        }\n\n        return true\n    }\n\n    private fun isValidFileType(file: File, expectedExtension: String): Boolean {\n        val bytes = file.inputStream().use { it.readNBytes(12) }\n\n        return when (expectedExtension.lowercase()) {\n            \"jpg\", \"jpeg\" -\u003e bytes.take(3).toByteArray().contentEquals(\n                byteArrayOf(0xFF.toByte(), 0xD8.toByte(), 0xFF.toByte())\n            )\n            \"png\" -\u003e bytes.take(8).toByteArray().contentEquals(\n                byteArrayOf(0x89.toByte(), 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A)\n            )\n            \"pdf\" -\u003e bytes.take(4).toByteArray().contentEquals(\n                byteArrayOf(0x25, 0x50, 0x44, 0x46) // %PDF\n            )\n            else -\u003e false\n        }\n    }\n\n    private fun containsMalware(file: File): Boolean {\n        // Integrate with ClamAV or similar\n        // For now, return false\n        return false\n    }\n}\n\ndata class UploadedFile(\n    val id: String,\n    val originalFilename: String,\n    val storedFilename: String,\n    val extension: String,\n    val size: Long,\n    val uploadedBy: String,\n    val uploadedAt: Long\n)\n\n// Ktor route\nfun Route.fileUpload(fileUploadService: FileUploadService) {\n    post(\"/upload\") {\n        val principal = call.principal\u003cUserPrincipal\u003e()\n            ?: return@post call.respond(HttpStatusCode.Unauthorized)\n\n        val multipart = call.receiveMultipart()\n\n        val result = fileUploadService.upload(multipart, principal.id)\n\n        result.fold(\n            onSuccess = { uploadedFile -\u003e\n                call.respond(HttpStatusCode.Created, uploadedFile)\n            },\n            onFailure = { error -\u003e\n                when (error) {\n                    is ValidationException -\u003e call.respond(\n                        HttpStatusCode.BadRequest,\n                        mapOf(\"error\" to error.message)\n                    )\n                    is SecurityException -\u003e call.respond(\n                        HttpStatusCode.Forbidden,\n                        mapOf(\"error\" to error.message)\n                    )\n                    else -\u003e call.respond(\n                        HttpStatusCode.InternalServerError,\n                        mapOf(\"error\" to \"Upload failed\")\n                    )\n                }\n            }\n        )\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why This Matters",
                                "content":  "\n### The Stakes\n\n**Data Breaches Cost**:\n- Average cost: $4.45 million\n- Customer churn: 60% after breach\n- Legal penalties: GDPR fines up to 4% of revenue\n\n**Career Impact**:\n- Security-aware developers earn 25% more\n- Companies require security knowledge\n- One breach can end a career\n\n**User Trust**:\n- 87% won\u0027t use an app after a breach\n- Trust takes years to build, seconds to destroy\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Checkpoint Quiz",
                                "content":  "\n### Question 1\nWhy should you NEVER store passwords in plaintext?\n\nA) It takes up too much space\nB) If database is compromised, all passwords are exposed\nC) It\u0027s slower than hashing\nD) It\u0027s not compatible with databases\n\n### Question 2\nWhat is the N+1 query problem related to in security?\n\nA) It\u0027s a type of SQL injection\nB) It creates performance issues that can be exploited for DoS\nC) It allows unauthorized access\nD) It\u0027s not a security issue\n\n### Question 3\nWhat\u0027s the purpose of certificate pinning?\n\nA) Faster HTTPS connections\nB) Prevents man-in-the-middle attacks\nC) Reduces app size\nD) Improves SEO\n\n### Question 4\nWhat should you do when security validation fails?\n\nA) Grant access anyway\nB) Fail securely (deny access)\nC) Log the user out\nD) Restart the app\n\n### Question 5\nWhy use bcrypt instead of SHA-256 for passwords?\n\nA) bcrypt is faster\nB) bcrypt includes salt and is designed to be slow\nC) SHA-256 is deprecated\nD) bcrypt produces smaller hashes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Quiz Answers",
                                "content":  "\n**Question 1: B) If database is compromised, all passwords are exposed**\n\nStoring plaintext passwords = catastrophic breach:\n- Attackers get all passwords\n- Users reuse passwords across sites\n- One breach = compromise everywhere\n\nAlways hash passwords with bcrypt!\n\n---\n\n**Question 2: B) Creates performance issues that can be exploited for DoS**\n\nN+1 queries = performance vulnerability:\n- Attacker requests large dataset\n- Triggers thousands of queries\n- Server becomes unresponsive (DoS)\n\nSolution: Use JOINs and optimize queries\n\n---\n\n**Question 3: B) Prevents man-in-the-middle attacks**\n\nCertificate pinning ensures:\n- App only trusts specific certificates\n- Can\u0027t be fooled by fake certificates\n- Prevents attackers intercepting traffic\n\n---\n\n**Question 4: B) Fail securely (deny access)**\n\nWhen in doubt, deny:\n- Error in authentication? Deny\n- Exception in authorization? Deny\n- Can\u0027t verify request? Deny\n\nNever fail open!\n\n---\n\n**Question 5: B) bcrypt includes salt and is designed to be slow**\n\nbcrypt advantages:\n- Automatically salts (unique hash per password)\n- Configurable cost (slower = harder to crack)\n- Designed for passwords (SHA-256 is not)\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What You\u0027ve Learned",
                                "content":  "\n✅ Why security is critical (real breach examples)\n✅ Secure coding principles (defense in depth, least privilege, fail securely)\n✅ Input validation and sanitization (SQL injection, XSS, path traversal)\n✅ Password security (bcrypt hashing, strength validation)\n✅ JWT security (proper signing, expiration, refresh tokens)\n✅ Android security (KeyStore, ProGuard, certificate pinning)\n✅ OWASP Top 10 vulnerabilities and how to prevent them\n✅ Practical security implementations (registration, rate limiting, file upload)\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Next Steps",
                                "content":  "\nIn **Lesson 7.5: CI/CD and DevOps**, you\u0027ll learn:\n- Continuous Integration with GitHub Actions\n- Automated testing in CI/CD pipelines\n- Build automation with Gradle\n- Code quality tools (ktlint, detekt)\n- Docker for backend applications\n- Publishing Android apps to Play Store\n\nSecure code is worthless if you can\u0027t deploy it reliably!\n\n---\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "advanced",
    "title":  "Lesson 7.4: Security Best Practices",
    "estimatedMinutes":  90
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
- Search for "kotlin Lesson 7.4: Security Best Practices 2024 2025" to find latest practices
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
  "lessonId": "7.4",
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

