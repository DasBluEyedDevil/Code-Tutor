---
type: "THEORY"
title: "Step-by-Step Implementation Guide"
---


### Phase 1: Project Setup (30 minutes)

1. **Create New Project**:
   ```bash
   mkdir taskmaster-api
   cd taskmaster-api
   gradle init --type kotlin-application
   ```

2. **Add Dependencies** in `build.gradle.kts`:
   ```kotlin
   dependencies {
       implementation("io.ktor:ktor-server-core-jvm:3.4.0")
       implementation("io.ktor:ktor-server-cio-jvm:3.4.0")
       implementation("io.ktor:ktor-server-content-negotiation-jvm:3.4.0")
       implementation("io.ktor:ktor-serialization-kotlinx-json-jvm:3.4.0")
       implementation("io.ktor:ktor-server-auth-jvm:3.4.0")
       implementation("io.ktor:ktor-server-auth-jwt-jvm:3.4.0")
       implementation("org.jetbrains.exposed:exposed-core:1.0.0")
       implementation("org.jetbrains.exposed:exposed-jdbc:1.0.0")
       implementation("com.h2database:h2:2.2.224")
       implementation("com.zaxxer:HikariCP:5.1.0")
       implementation("de.nycode:bcrypt:2.3.0")
       implementation("com.auth0:java-jwt:4.5.0")
       implementation("io.insert-koin:koin-ktor:4.1.1")
       implementation("io.insert-koin:koin-logger-slf4j:4.1.1")
       implementation("ch.qos.logback:logback-classic:1.4.14")

       testImplementation("io.ktor:ktor-server-test-host:3.4.0")
       testImplementation("org.jetbrains.kotlin:kotlin-test-junit5:2.3.0")
       testImplementation("org.junit.jupiter:junit-jupiter-api:5.10.2")
       testRuntimeOnly("org.junit.jupiter:junit-jupiter-engine:5.10.2")
       testImplementation("io.insert-koin:koin-test:4.1.1")
       testImplementation("io.insert-koin:koin-test-junit5:4.1.1")
   }
   ```

3. **Create Package Structure**:
   ```
   src/main/kotlin/com/taskmaster/
   ├── Application.kt
   ├── models/
   │   ├── User.kt
   │   ├── Task.kt
   │   └── Responses.kt
   ├── repositories/
   │   ├── UserRepository.kt
   │   └── TaskRepository.kt
   ├── services/
   │   ├── UserService.kt
   │   ├── AuthService.kt
   │   └── TaskService.kt
   ├── routes/
   │   ├── AuthRoutes.kt
   │   └── TaskRoutes.kt
   ├── validation/
   │   ├── Validator.kt
   │   ├── UserValidator.kt
   │   └── TaskValidator.kt
   ├── security/
   │   ├── JwtConfig.kt
   │   └── PasswordHasher.kt
   ├── exceptions/
   │   └── ApiExceptions.kt
   ├── plugins/
   │   ├── Authentication.kt
   │   └── ErrorHandling.kt
   ├── database/
   │   └── DatabaseFactory.kt
   └── di/
       ├── RepositoryModule.kt
       ├── ServiceModule.kt
       └── AppModule.kt
   ```

---

### Phase 2: Core Models & Database (45 minutes)

1. **Define Models** (`models/User.kt`, `models/Task.kt`):
   - User model with all fields
   - Task model with status and priority enums
   - Request/Response DTOs

2. **Create Database Tables** (`database/DatabaseFactory.kt`):
   - Users table with unique constraints
   - Tasks table with foreign keys
   - Initialize H2 database

3. **Implement Repositories**:
   - UserRepository interface and implementation
   - TaskRepository interface and implementation
   - Include query methods (filters, search)

---

### Phase 3: Validation & Error Handling (30 minutes)

1. **Create Validators**:
   - Base Validator class with common methods
   - UserValidator for registration
   - TaskValidator for task creation/updates

2. **Define Exceptions**:
   - ValidationException
   - NotFoundException
   - ConflictException
   - ForbiddenException
   - UnauthorizedException

3. **Configure Error Handling**:
   - StatusPages plugin configuration
   - Consistent error response format

---

### Phase 4: Authentication System (60 minutes)

1. **Implement Password Hashing**:
   - PasswordHasher utility with bcrypt
   - Password strength validation

2. **Configure JWT**:
   - JwtConfig with token generation
   - Include user ID, email, role in claims

3. **Build Auth Services**:
   - UserService with registration
   - AuthService with login

4. **Create Auth Routes**:
   - POST /api/auth/register
   - POST /api/auth/login
   - GET /api/auth/me

5. **Configure Authentication Plugin**:
   - JWT validation
   - UserPrincipal extraction

---

### Phase 5: Task Management (90 minutes)

1. **Implement TaskService**:
   - Create task (with validation)
   - Update task (with ownership check)
   - Delete task (owner only)
   - Get tasks with filters
   - Assign task to user
   - Update task status

2. **Create Task Routes**:
   - All CRUD endpoints
   - Query parameter handling
   - Authorization checks

3. **Implement Authorization Logic**:
   - canViewTask(task, user)
   - canModifyTask(task, user)
   - canDeleteTask(task, user)

---

### Phase 6: Dependency Injection (30 minutes)

1. **Define Koin Modules**:
   - RepositoryModule
   - ServiceModule
   - DatabaseModule

2. **Configure Koin**:
   - Install Koin plugin
   - Load modules

3. **Inject Dependencies**:
   - Update routes to inject services
   - Remove manual wiring

---

### Phase 7: Testing (90 minutes)

1. **Unit Tests**:
   - UserService tests (5+ tests)
   - AuthService tests (5+ tests)
   - TaskService tests (10+ tests covering authorization)
   - Validator tests

2. **Integration Tests**:
   - Auth endpoint tests
   - Task CRUD tests
   - Authorization tests
   - Query filter tests

3. **Run Coverage**:
   - Configure JaCoCo
   - Aim for 70%+ coverage

---

