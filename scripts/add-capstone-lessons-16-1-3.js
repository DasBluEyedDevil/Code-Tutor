/**
 * Script to add Capstone Lessons 16.1-16.3 to the Java course
 * Task Manager Project: Requirements, Architecture, Backend Setup, and REST API
 */

const fs = require('fs');
const path = require('path');

const coursePath = path.join(__dirname, '..', 'content', 'courses', 'java', 'course.json');

// Read the course file
console.log('Reading course.json...');
const course = JSON.parse(fs.readFileSync(coursePath, 'utf8'));

// Find the existing capstone module (module-15) and update it to module-16
const capstoneModuleIndex = course.modules.findIndex(m => m.id === 'module-15');

if (capstoneModuleIndex === -1) {
  console.error('Could not find module-15 (Capstone Project)');
  process.exit(1);
}

console.log('Found capstone module at index:', capstoneModuleIndex);

// Create the new module-16 with restructured lessons
const module16 = {
  "id": "module-16",
  "title": "Capstone Project: Task Manager Application",
  "description": "Build a complete full-stack Task Manager application from scratch. Apply everything you've learned: Spring Boot, JPA, REST APIs, React frontend, authentication, and deployment.",
  "difficulty": "advanced",
  "estimatedHours": 12,
  "lessons": [
    createLesson16_1(),
    createLesson16_2(),
    createLesson16_3()
  ]
};

// Replace module-15 with module-16
course.modules[capstoneModuleIndex] = module16;

// Write the updated course
console.log('Writing updated course.json...');
fs.writeFileSync(coursePath, JSON.stringify(course, null, 2));
console.log('Successfully added lessons 16.1-16.3 to module-16!');

// ========== LESSON 16.1: Project Requirements & Architecture ==========
function createLesson16_1() {
  return {
    "id": "capstone-lesson-1",
    "title": "Lesson 16.1: Project Requirements & Architecture",
    "moduleId": "module-16",
    "order": 1,
    "estimatedMinutes": 45,
    "difficulty": "advanced",
    "contentSections": [
      {
        "type": "THEORY",
        "title": "Introduction to the Task Manager Project",
        "content": "Welcome to the capstone project! Over the next 9 lessons, you will build a complete, production-ready Task Manager application from scratch. This is not a toy project - it is a full-stack application that demonstrates every skill you have learned throughout this course.\n\nThe Task Manager allows users to organize their work through tasks, categories, and priorities. Users can register, log in securely, create and manage tasks, organize them by category, set due dates, and track their progress. The application features a modern React frontend communicating with a Spring Boot REST API, all backed by a PostgreSQL database.\n\nWhy a Task Manager? This project type is perfect for demonstrating your skills because it requires:\n- User authentication and authorization (security)\n- CRUD operations on multiple related entities (database design)\n- Complex business logic (task filtering, sorting, status updates)\n- A responsive user interface (frontend development)\n- RESTful API design (backend architecture)\n- Real-world deployment concerns (Docker, CI/CD)\n\nBy the end of this capstone, you will have a portfolio-worthy project that showcases your ability to build complete applications. Employers specifically look for projects that demonstrate end-to-end thinking - and this project does exactly that.\n\nLet us begin by understanding what we are building and how all the pieces fit together."
      },
      {
        "type": "THEORY",
        "title": "User Stories and Requirements",
        "content": "Every professional project starts with clear requirements. We will define ours through user stories - descriptions of features from the user's perspective.\n\nAuthentication Stories:\n- As a new user, I want to register with my email and password so that I can access the application.\n- As a registered user, I want to log in securely so that I can manage my tasks.\n- As a logged-in user, I want to log out so that my session is terminated securely.\n- As a user, I want my password stored securely (hashed) so that my account is protected.\n\nTask Management Stories:\n- As a user, I want to create tasks with a title, description, due date, and priority so that I can track my work.\n- As a user, I want to view all my tasks in a list so that I can see what needs to be done.\n- As a user, I want to edit my tasks so that I can update information as things change.\n- As a user, I want to delete tasks I no longer need so that my list stays clean.\n- As a user, I want to mark tasks as complete so that I can track my progress.\n- As a user, I want to filter tasks by status (pending, completed) so that I can focus on what matters.\n- As a user, I want to sort tasks by due date or priority so that I can prioritize effectively.\n\nCategory Management Stories:\n- As a user, I want to create categories (Work, Personal, Health, etc.) so that I can organize my tasks.\n- As a user, I want to assign tasks to categories so that related tasks are grouped together.\n- As a user, I want to filter tasks by category so that I can focus on one area at a time.\n\nThese user stories will guide our development. Each feature we build should directly support one or more of these stories. This is how professional teams work - requirements drive development."
      },
      {
        "type": "THEORY",
        "title": "Data Model: User, Task, and Category Entities",
        "content": "Before writing any code, we must design our data model. The data model defines what information we store and how entities relate to each other.\n\nUser Entity:\nThe User represents a registered account in our system. Each user has:\n- id: Unique identifier (auto-generated)\n- email: User's email address (unique, used for login)\n- password: Hashed password (never store plain text!)\n- name: User's display name\n- role: USER or ADMIN (for authorization)\n- createdAt: When the account was created\n- tasks: List of tasks owned by this user\n- categories: List of categories created by this user\n\nTask Entity:\nThe Task represents a single work item. Each task has:\n- id: Unique identifier\n- title: Short description of the task (required)\n- description: Detailed notes about the task (optional)\n- status: PENDING, IN_PROGRESS, or COMPLETED\n- priority: LOW, MEDIUM, HIGH, or URGENT\n- dueDate: When the task should be completed (optional)\n- createdAt: When the task was created\n- updatedAt: When the task was last modified\n- owner: The User who owns this task\n- category: The Category this task belongs to (optional)\n\nCategory Entity:\nThe Category helps organize tasks into groups. Each category has:\n- id: Unique identifier\n- name: Category name (e.g., \"Work\", \"Personal\")\n- description: What this category is for (optional)\n- color: Hex color code for UI display (e.g., \"#3B82F6\")\n- owner: The User who created this category\n- tasks: List of tasks in this category\n\nRelationships:\n- One User has many Tasks (one-to-many)\n- One User has many Categories (one-to-many)\n- One Category has many Tasks (one-to-many)\n- One Task belongs to one User (many-to-one)\n- One Task optionally belongs to one Category (many-to-one)\n\nThis normalized design prevents data duplication and maintains referential integrity. When we delete a User, their Tasks and Categories are also deleted (cascade). When we delete a Category, Tasks in that category have their category set to null (set null)."
      },
      {
        "type": "KEY_POINT",
        "title": "Architecture Overview: React + Spring Boot + PostgreSQL",
        "content": "Our application follows the modern three-tier architecture pattern:\n\nPresentation Tier (Frontend):\n- Technology: React 18+ with TypeScript\n- Responsibility: User interface and user experience\n- Communicates with: Backend via REST API (HTTP/JSON)\n- Features: Component-based UI, state management, form validation, responsive design\n\nApplication Tier (Backend):\n- Technology: Spring Boot 3.2+ with Java 21+\n- Responsibility: Business logic, security, data validation, API endpoints\n- Components:\n  - Controllers: Handle HTTP requests, route to services\n  - Services: Implement business logic, coordinate operations\n  - Repositories: Database access layer (Spring Data JPA)\n  - DTOs: Data transfer objects for API requests/responses\n  - Security: JWT authentication, authorization rules\n\nData Tier (Database):\n- Technology: PostgreSQL 16\n- Responsibility: Persistent data storage, data integrity, queries\n- Managed by: Flyway for schema migrations\n- Accessed by: Spring Data JPA repositories\n\nCommunication Flow:\n1. User interacts with React UI\n2. React sends HTTP request to Spring Boot API\n3. Spring Security validates JWT token\n4. Controller receives request, validates input\n5. Service executes business logic\n6. Repository queries/updates PostgreSQL\n7. Response flows back through layers to React\n8. React updates UI with new data\n\nThis separation of concerns makes our application maintainable, testable, and scalable. Each tier can be developed, tested, and deployed independently."
      },
      {
        "type": "THEORY",
        "title": "Project Structure and Module Organization",
        "content": "A well-organized project structure makes code easier to navigate, maintain, and scale. Here is how we will organize our Task Manager:\n\nBackend Structure (Spring Boot):\ntaskmanager-api/\n  src/main/java/com/taskmanager/\n    TaskManagerApplication.java       # Main entry point\n    config/                            # Configuration classes\n      SecurityConfig.java              # Spring Security setup\n      WebConfig.java                   # CORS, converters\n    controller/                        # REST endpoints\n      AuthController.java              # Login, register, logout\n      TaskController.java              # Task CRUD operations\n      CategoryController.java          # Category CRUD operations\n    service/                           # Business logic\n      AuthService.java                 # Authentication logic\n      TaskService.java                 # Task operations\n      CategoryService.java             # Category operations\n    repository/                        # Database access\n      UserRepository.java\n      TaskRepository.java\n      CategoryRepository.java\n    model/                             # JPA entities\n      User.java\n      Task.java\n      Category.java\n      enums/                           # Enumerations\n        Role.java\n        TaskStatus.java\n        Priority.java\n    dto/                               # Data transfer objects\n      request/                         # Incoming data\n        TaskRequest.java\n        LoginRequest.java\n      response/                        # Outgoing data\n        TaskResponse.java\n        UserResponse.java\n    exception/                         # Custom exceptions\n      ResourceNotFoundException.java\n      UnauthorizedException.java\n    security/                          # JWT and auth\n      JwtTokenProvider.java\n      JwtAuthenticationFilter.java\n  src/main/resources/\n    application.yml                    # Configuration\n    db/migration/                      # Flyway migrations\n      V1__create_users.sql\n      V2__create_categories.sql\n      V3__create_tasks.sql\n\nFrontend Structure (React):\ntaskmanager-ui/\n  src/\n    components/                        # Reusable UI components\n      TaskCard.tsx\n      CategoryBadge.tsx\n      Header.tsx\n    pages/                             # Page components\n      LoginPage.tsx\n      DashboardPage.tsx\n      TasksPage.tsx\n    hooks/                             # Custom React hooks\n      useAuth.ts\n      useTasks.ts\n    services/                          # API communication\n      api.ts\n      authService.ts\n      taskService.ts\n    types/                             # TypeScript interfaces\n      Task.ts\n      User.ts\n    context/                           # React context\n      AuthContext.tsx\n\nThis structure follows industry best practices: separation by feature type, clear naming conventions, and logical grouping of related files."
      },
      {
        "type": "WARNING",
        "title": "Common Architecture Mistakes to Avoid",
        "content": "As you build this project, watch out for these common pitfalls:\n\nMistake 1: Exposing Entities Directly in API\nProblem: Returning JPA entities from controllers exposes internal details (like password hashes) and creates tight coupling.\nSolution: Always use DTOs for API requests and responses.\n\nMistake 2: Business Logic in Controllers\nProblem: Putting validation and business rules in controllers makes them hard to test and reuse.\nSolution: Controllers should only handle HTTP concerns. Move logic to services.\n\nMistake 3: N+1 Query Problem\nProblem: Loading relationships lazily in a loop causes hundreds of database queries.\nSolution: Use JOIN FETCH in JPQL queries or @EntityGraph for eager loading when needed.\n\nMistake 4: Storing Plain Text Passwords\nProblem: If database is breached, all passwords are exposed.\nSolution: Always hash passwords with BCrypt before storing.\n\nMistake 5: No Input Validation\nProblem: Malicious or malformed data can corrupt your database or crash your application.\nSolution: Use Bean Validation (@NotBlank, @Email, @Size) on all request DTOs.\n\nMistake 6: Hardcoding Configuration\nProblem: Database URLs, secrets in code makes deployment difficult and insecure.\nSolution: Use application.yml with environment variable substitution.\n\nMistake 7: No Error Handling\nProblem: Unhandled exceptions expose stack traces to users (security risk) and provide poor UX.\nSolution: Use @ControllerAdvice for global exception handling with proper error responses.\n\nWe will address all of these concerns as we build the application. Keep this list handy as a checklist."
      }
    ],
    "challenges": [
      {
        "type": "MULTIPLE_CHOICE",
        "id": "capstone-16-1-architecture",
        "title": "Understanding the Three-Tier Architecture",
        "description": "Test your understanding of how the application tiers communicate.",
        "question": "In our Task Manager architecture, which component is responsible for converting a TaskRequest DTO into a Task entity and saving it to the database?",
        "options": [
          {
            "id": "a",
            "text": "TaskController - it receives the request and should handle everything",
            "isCorrect": false
          },
          {
            "id": "b",
            "text": "TaskService - it implements business logic and coordinates with the repository",
            "isCorrect": true
          },
          {
            "id": "c",
            "text": "TaskRepository - it handles all database operations",
            "isCorrect": false
          },
          {
            "id": "d",
            "text": "React frontend - it prepares data before sending to the API",
            "isCorrect": false
          }
        ],
        "difficulty": "intermediate"
      },
      {
        "type": "MULTIPLE_CHOICE",
        "id": "capstone-16-1-relationships",
        "title": "Entity Relationships",
        "description": "Understand how entities relate to each other in our data model.",
        "question": "What happens to a user's tasks when we delete that user from the database, based on our data model design?",
        "options": [
          {
            "id": "a",
            "text": "The tasks remain in the database with a null owner",
            "isCorrect": false
          },
          {
            "id": "b",
            "text": "The deletion fails because tasks still reference the user",
            "isCorrect": false
          },
          {
            "id": "c",
            "text": "The tasks are also deleted (cascade delete)",
            "isCorrect": true
          },
          {
            "id": "d",
            "text": "The tasks are automatically assigned to an admin user",
            "isCorrect": false
          }
        ],
        "difficulty": "intermediate"
      }
    ]
  };
}

// ========== LESSON 16.2: Backend Setup & Data Layer ==========
function createLesson16_2() {
  return {
    "id": "capstone-lesson-2",
    "title": "Lesson 16.2: Backend Setup & Data Layer",
    "moduleId": "module-16",
    "order": 2,
    "estimatedMinutes": 60,
    "difficulty": "advanced",
    "contentSections": [
      {
        "type": "THEORY",
        "title": "Creating the Spring Boot Project",
        "content": "Let us set up our backend project using Spring Initializr. We will configure all the dependencies we need for a production-ready application.\n\nUsing Spring Initializr (start.spring.io):\n\nProject Configuration:\n- Project: Maven\n- Language: Java\n- Spring Boot: 3.2.x (or latest stable)\n- Group: com.taskmanager\n- Artifact: taskmanager-api\n- Packaging: Jar\n- Java: 21\n\nDependencies to Add:\n1. Spring Web - REST API endpoints, embedded Tomcat\n2. Spring Data JPA - Database access, repository pattern\n3. Spring Security - Authentication, authorization\n4. PostgreSQL Driver - Database connectivity\n5. Validation - Input validation (@NotBlank, @Email)\n6. Flyway Migration - Database schema versioning\n7. Spring Boot DevTools - Hot reload during development\n8. Lombok - Reduce boilerplate (optional but recommended)\n\nAfter generating the project, your pom.xml will include all these dependencies. The project structure follows Maven conventions with src/main/java for code and src/main/resources for configuration.\n\nImportant: Before running the application, we need to configure the database connection in application.yml. Without this, Spring Boot cannot start because Spring Data JPA requires a datasource."
      },
      {
        "type": "THEORY",
        "title": "Application Configuration",
        "content": "Create your application.yml file in src/main/resources. This YAML configuration is more readable than properties files and supports hierarchical structure.\n\n```yaml\nspring:\n  application:\n    name: taskmanager-api\n  \n  datasource:\n    url: jdbc:postgresql://localhost:5432/taskmanager\n    username: ${DB_USERNAME:taskuser}\n    password: ${DB_PASSWORD:localdev123}\n    driver-class-name: org.postgresql.Driver\n  \n  jpa:\n    hibernate:\n      ddl-auto: validate  # Use Flyway for migrations\n    show-sql: true\n    properties:\n      hibernate:\n        format_sql: true\n        dialect: org.hibernate.dialect.PostgreSQLDialect\n  \n  flyway:\n    enabled: true\n    locations: classpath:db/migration\n    baseline-on-migrate: true\n\nserver:\n  port: 8080\n\nlogging:\n  level:\n    com.taskmanager: DEBUG\n    org.springframework.security: DEBUG\n```\n\nKey Configuration Explained:\n\n- datasource.url: JDBC connection string. Uses environment variables with defaults for local development.\n- jpa.hibernate.ddl-auto: Set to 'validate' because Flyway manages schema changes. Never use 'create' or 'update' in production!\n- flyway.enabled: Enables automatic migration on startup. Flyway runs SQL scripts in order.\n- Environment Variables: ${DB_PASSWORD:localdev123} means \"use DB_PASSWORD if set, otherwise use localdev123\". This allows different values per environment."
      },
      {
        "type": "THEORY",
        "title": "JPA Entity: User",
        "content": "Now let us create our first entity. The User entity represents registered accounts and is the foundation for authentication and task ownership.\n\n```java\npackage com.taskmanager.model;\n\nimport jakarta.persistence.*;\nimport java.time.LocalDateTime;\nimport java.util.ArrayList;\nimport java.util.List;\n\n@Entity\n@Table(name = \"users\")\npublic class User {\n    \n    @Id\n    @GeneratedValue(strategy = GenerationType.IDENTITY)\n    private Long id;\n    \n    @Column(unique = true, nullable = false, length = 255)\n    private String email;\n    \n    @Column(nullable = false)\n    private String password;\n    \n    @Column(length = 100)\n    private String name;\n    \n    @Enumerated(EnumType.STRING)\n    @Column(nullable = false)\n    private Role role = Role.USER;\n    \n    @Column(name = \"created_at\", updatable = false)\n    private LocalDateTime createdAt;\n    \n    @OneToMany(mappedBy = \"owner\", cascade = CascadeType.ALL, orphanRemoval = true)\n    private List<Task> tasks = new ArrayList<>();\n    \n    @OneToMany(mappedBy = \"owner\", cascade = CascadeType.ALL, orphanRemoval = true)\n    private List<Category> categories = new ArrayList<>();\n    \n    // Default constructor required by JPA\n    public User() {}\n    \n    public User(String email, String password, String name) {\n        this.email = email;\n        this.password = password;\n        this.name = name;\n        this.role = Role.USER;\n    }\n    \n    @PrePersist\n    protected void onCreate() {\n        this.createdAt = LocalDateTime.now();\n    }\n    \n    // Getters and setters\n    public Long getId() { return id; }\n    public void setId(Long id) { this.id = id; }\n    \n    public String getEmail() { return email; }\n    public void setEmail(String email) { this.email = email; }\n    \n    public String getPassword() { return password; }\n    public void setPassword(String password) { this.password = password; }\n    \n    public String getName() { return name; }\n    public void setName(String name) { this.name = name; }\n    \n    public Role getRole() { return role; }\n    public void setRole(Role role) { this.role = role; }\n    \n    public LocalDateTime getCreatedAt() { return createdAt; }\n    \n    public List<Task> getTasks() { return tasks; }\n    public void setTasks(List<Task> tasks) { this.tasks = tasks; }\n    \n    public List<Category> getCategories() { return categories; }\n    public void setCategories(List<Category> categories) { this.categories = categories; }\n}\n```\n\nAnnotation Deep Dive:\n- @Entity: Marks this class as a JPA entity (maps to database table)\n- @Table(name = \"users\"): Explicit table name (avoids reserved word issues)\n- @Id + @GeneratedValue: Auto-generated primary key using database sequence\n- @Column: Defines column constraints (unique, nullable, length)\n- @Enumerated(EnumType.STRING): Stores enum as string \"USER\" not ordinal 0\n- @OneToMany: Defines the \"one\" side of one-to-many relationship\n- cascade = CascadeType.ALL: All operations cascade to children\n- orphanRemoval = true: Delete children when removed from collection\n- @PrePersist: Lifecycle hook that runs before INSERT"
      },
      {
        "type": "THEORY",
        "title": "JPA Entity: Category",
        "content": "The Category entity allows users to organize their tasks into logical groups.\n\n```java\npackage com.taskmanager.model;\n\nimport jakarta.persistence.*;\nimport java.time.LocalDateTime;\nimport java.util.ArrayList;\nimport java.util.List;\n\n@Entity\n@Table(name = \"categories\", \n       uniqueConstraints = @UniqueConstraint(columnNames = {\"name\", \"owner_id\"}))\npublic class Category {\n    \n    @Id\n    @GeneratedValue(strategy = GenerationType.IDENTITY)\n    private Long id;\n    \n    @Column(nullable = false, length = 50)\n    private String name;\n    \n    @Column(length = 255)\n    private String description;\n    \n    @Column(length = 7)\n    private String color = \"#6B7280\"; // Default gray\n    \n    @ManyToOne(fetch = FetchType.LAZY)\n    @JoinColumn(name = \"owner_id\", nullable = false)\n    private User owner;\n    \n    @OneToMany(mappedBy = \"category\")\n    private List<Task> tasks = new ArrayList<>();\n    \n    @Column(name = \"created_at\", updatable = false)\n    private LocalDateTime createdAt;\n    \n    public Category() {}\n    \n    public Category(String name, String description, String color, User owner) {\n        this.name = name;\n        this.description = description;\n        this.color = color;\n        this.owner = owner;\n    }\n    \n    @PrePersist\n    protected void onCreate() {\n        this.createdAt = LocalDateTime.now();\n    }\n    \n    // Getters and setters\n    public Long getId() { return id; }\n    public void setId(Long id) { this.id = id; }\n    \n    public String getName() { return name; }\n    public void setName(String name) { this.name = name; }\n    \n    public String getDescription() { return description; }\n    public void setDescription(String description) { this.description = description; }\n    \n    public String getColor() { return color; }\n    public void setColor(String color) { this.color = color; }\n    \n    public User getOwner() { return owner; }\n    public void setOwner(User owner) { this.owner = owner; }\n    \n    public List<Task> getTasks() { return tasks; }\n    public void setTasks(List<Task> tasks) { this.tasks = tasks; }\n    \n    public LocalDateTime getCreatedAt() { return createdAt; }\n}\n```\n\nKey Design Decisions:\n- Composite unique constraint: Each user can only have one category with a given name\n- FetchType.LAZY: Do not load owner automatically (prevents N+1 queries)\n- No cascade on tasks: Deleting category does not delete tasks, just sets category to null\n- Color field: Hex color for UI display, makes the app more visually appealing"
      },
      {
        "type": "THEORY",
        "title": "JPA Entity: Task",
        "content": "The Task entity is the core of our application. It contains all the information about a work item.\n\n```java\npackage com.taskmanager.model;\n\nimport com.taskmanager.model.enums.Priority;\nimport com.taskmanager.model.enums.TaskStatus;\nimport jakarta.persistence.*;\nimport java.time.LocalDate;\nimport java.time.LocalDateTime;\n\n@Entity\n@Table(name = \"tasks\")\npublic class Task {\n    \n    @Id\n    @GeneratedValue(strategy = GenerationType.IDENTITY)\n    private Long id;\n    \n    @Column(nullable = false, length = 255)\n    private String title;\n    \n    @Column(columnDefinition = \"TEXT\")\n    private String description;\n    \n    @Enumerated(EnumType.STRING)\n    @Column(nullable = false)\n    private TaskStatus status = TaskStatus.PENDING;\n    \n    @Enumerated(EnumType.STRING)\n    @Column(nullable = false)\n    private Priority priority = Priority.MEDIUM;\n    \n    @Column(name = \"due_date\")\n    private LocalDate dueDate;\n    \n    @ManyToOne(fetch = FetchType.LAZY)\n    @JoinColumn(name = \"owner_id\", nullable = false)\n    private User owner;\n    \n    @ManyToOne(fetch = FetchType.LAZY)\n    @JoinColumn(name = \"category_id\")\n    private Category category;\n    \n    @Column(name = \"created_at\", updatable = false)\n    private LocalDateTime createdAt;\n    \n    @Column(name = \"updated_at\")\n    private LocalDateTime updatedAt;\n    \n    public Task() {}\n    \n    public Task(String title, String description, User owner) {\n        this.title = title;\n        this.description = description;\n        this.owner = owner;\n        this.status = TaskStatus.PENDING;\n        this.priority = Priority.MEDIUM;\n    }\n    \n    @PrePersist\n    protected void onCreate() {\n        this.createdAt = LocalDateTime.now();\n        this.updatedAt = LocalDateTime.now();\n    }\n    \n    @PreUpdate\n    protected void onUpdate() {\n        this.updatedAt = LocalDateTime.now();\n    }\n    \n    // Getters and setters\n    public Long getId() { return id; }\n    public void setId(Long id) { this.id = id; }\n    \n    public String getTitle() { return title; }\n    public void setTitle(String title) { this.title = title; }\n    \n    public String getDescription() { return description; }\n    public void setDescription(String description) { this.description = description; }\n    \n    public TaskStatus getStatus() { return status; }\n    public void setStatus(TaskStatus status) { this.status = status; }\n    \n    public Priority getPriority() { return priority; }\n    public void setPriority(Priority priority) { this.priority = priority; }\n    \n    public LocalDate getDueDate() { return dueDate; }\n    public void setDueDate(LocalDate dueDate) { this.dueDate = dueDate; }\n    \n    public User getOwner() { return owner; }\n    public void setOwner(User owner) { this.owner = owner; }\n    \n    public Category getCategory() { return category; }\n    public void setCategory(Category category) { this.category = category; }\n    \n    public LocalDateTime getCreatedAt() { return createdAt; }\n    public LocalDateTime getUpdatedAt() { return updatedAt; }\n}\n```\n\nThe Task entity demonstrates:\n- Multiple enums for type-safe status and priority\n- Optional relationship to Category (nullable)\n- Both createdAt and updatedAt timestamps\n- LocalDate for dueDate (date only, no time)\n- TEXT column type for potentially long descriptions"
      },
      {
        "type": "THEORY",
        "title": "Enum Types for Type Safety",
        "content": "We use enums to ensure only valid values are stored in the database. Create these in the model/enums package.\n\n```java\n// com/taskmanager/model/enums/Role.java\npackage com.taskmanager.model.enums;\n\npublic enum Role {\n    USER,\n    ADMIN\n}\n\n// com/taskmanager/model/enums/TaskStatus.java\npackage com.taskmanager.model.enums;\n\npublic enum TaskStatus {\n    PENDING,\n    IN_PROGRESS,\n    COMPLETED,\n    CANCELLED\n}\n\n// com/taskmanager/model/enums/Priority.java\npackage com.taskmanager.model.enums;\n\npublic enum Priority {\n    LOW,\n    MEDIUM,\n    HIGH,\n    URGENT\n}\n```\n\nWhy Enums Instead of Strings?\n\n1. Type Safety: The compiler prevents invalid values. You cannot set status to \"DONE\" - it must be a TaskStatus value.\n\n2. Refactoring: If you rename PENDING to TODO, your IDE updates all usages. With strings, you would miss some.\n\n3. Documentation: Enums are self-documenting. Anyone reading the code knows exactly what values are valid.\n\n4. No Magic Strings: Strings like \"pending\" are error-prone (typos, case sensitivity). Enums eliminate this.\n\n5. Database Storage: With @Enumerated(EnumType.STRING), the database stores \"PENDING\" not 0. This is readable and safer if you reorder enums."
      },
      {
        "type": "THEORY",
        "title": "Flyway Migration Scripts",
        "content": "Flyway manages database schema changes through versioned SQL scripts. Create these in src/main/resources/db/migration:\n\n```sql\n-- V1__create_users.sql\nCREATE TABLE users (\n    id BIGSERIAL PRIMARY KEY,\n    email VARCHAR(255) NOT NULL UNIQUE,\n    password VARCHAR(255) NOT NULL,\n    name VARCHAR(100),\n    role VARCHAR(20) NOT NULL DEFAULT 'USER',\n    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP\n);\n\nCREATE INDEX idx_users_email ON users(email);\n\n-- V2__create_categories.sql\nCREATE TABLE categories (\n    id BIGSERIAL PRIMARY KEY,\n    name VARCHAR(50) NOT NULL,\n    description VARCHAR(255),\n    color VARCHAR(7) DEFAULT '#6B7280',\n    owner_id BIGINT NOT NULL REFERENCES users(id) ON DELETE CASCADE,\n    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,\n    UNIQUE(name, owner_id)\n);\n\nCREATE INDEX idx_categories_owner ON categories(owner_id);\n\n-- V3__create_tasks.sql\nCREATE TABLE tasks (\n    id BIGSERIAL PRIMARY KEY,\n    title VARCHAR(255) NOT NULL,\n    description TEXT,\n    status VARCHAR(20) NOT NULL DEFAULT 'PENDING',\n    priority VARCHAR(20) NOT NULL DEFAULT 'MEDIUM',\n    due_date DATE,\n    owner_id BIGINT NOT NULL REFERENCES users(id) ON DELETE CASCADE,\n    category_id BIGINT REFERENCES categories(id) ON DELETE SET NULL,\n    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,\n    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP\n);\n\nCREATE INDEX idx_tasks_owner ON tasks(owner_id);\nCREATE INDEX idx_tasks_status ON tasks(status);\nCREATE INDEX idx_tasks_due_date ON tasks(due_date);\nCREATE INDEX idx_tasks_category ON tasks(category_id);\n```\n\nFlyway Naming Convention:\n- V1__description.sql - Version number + double underscore + description\n- Scripts run in version order (V1, V2, V3...)\n- Never modify a migration that has been applied!\n- Add new migrations for schema changes\n\nKey SQL Features:\n- BIGSERIAL: Auto-incrementing 64-bit integer (PostgreSQL)\n- REFERENCES: Foreign key constraint\n- ON DELETE CASCADE: Delete children when parent is deleted\n- ON DELETE SET NULL: Set to null when referenced row is deleted\n- Indexes: Improve query performance on frequently filtered columns"
      },
      {
        "type": "THEORY",
        "title": "Repository Interfaces",
        "content": "Spring Data JPA generates repository implementations automatically. We only need to define interfaces.\n\n```java\n// com/taskmanager/repository/UserRepository.java\npackage com.taskmanager.repository;\n\nimport com.taskmanager.model.User;\nimport org.springframework.data.jpa.repository.JpaRepository;\nimport org.springframework.stereotype.Repository;\nimport java.util.Optional;\n\n@Repository\npublic interface UserRepository extends JpaRepository<User, Long> {\n    Optional<User> findByEmail(String email);\n    boolean existsByEmail(String email);\n}\n\n// com/taskmanager/repository/CategoryRepository.java\npackage com.taskmanager.repository;\n\nimport com.taskmanager.model.Category;\nimport org.springframework.data.jpa.repository.JpaRepository;\nimport org.springframework.stereotype.Repository;\nimport java.util.List;\nimport java.util.Optional;\n\n@Repository\npublic interface CategoryRepository extends JpaRepository<Category, Long> {\n    List<Category> findByOwnerId(Long ownerId);\n    Optional<Category> findByIdAndOwnerId(Long id, Long ownerId);\n    boolean existsByNameAndOwnerId(String name, Long ownerId);\n}\n\n// com/taskmanager/repository/TaskRepository.java\npackage com.taskmanager.repository;\n\nimport com.taskmanager.model.Task;\nimport com.taskmanager.model.enums.TaskStatus;\nimport org.springframework.data.domain.Page;\nimport org.springframework.data.domain.Pageable;\nimport org.springframework.data.jpa.repository.JpaRepository;\nimport org.springframework.data.jpa.repository.Query;\nimport org.springframework.data.repository.query.Param;\nimport org.springframework.stereotype.Repository;\nimport java.time.LocalDate;\nimport java.util.List;\nimport java.util.Optional;\n\n@Repository\npublic interface TaskRepository extends JpaRepository<Task, Long> {\n    \n    Page<Task> findByOwnerId(Long ownerId, Pageable pageable);\n    \n    Optional<Task> findByIdAndOwnerId(Long id, Long ownerId);\n    \n    List<Task> findByOwnerIdAndStatus(Long ownerId, TaskStatus status);\n    \n    List<Task> findByOwnerIdAndCategoryId(Long ownerId, Long categoryId);\n    \n    @Query(\"SELECT t FROM Task t WHERE t.owner.id = :ownerId \" +\n           \"AND t.dueDate <= :date AND t.status != 'COMPLETED'\")\n    List<Task> findOverdueTasks(@Param(\"ownerId\") Long ownerId, \n                                 @Param(\"date\") LocalDate date);\n    \n    @Query(\"SELECT COUNT(t) FROM Task t WHERE t.owner.id = :ownerId \" +\n           \"AND t.status = :status\")\n    long countByOwnerIdAndStatus(@Param(\"ownerId\") Long ownerId, \n                                  @Param(\"status\") TaskStatus status);\n}\n```\n\nSpring Data JPA Magic:\n- findByEmail: Generates SELECT * FROM users WHERE email = ?\n- existsByEmail: Generates SELECT COUNT(*) > 0 FROM users WHERE email = ?\n- findByOwnerId with Pageable: Adds LIMIT, OFFSET, ORDER BY automatically\n- @Query: For complex queries that cannot be derived from method names\n- @Param: Named parameters in JPQL queries\n\nReturn Types:\n- Optional<T>: For single results that might not exist\n- List<T>: For multiple results (empty list if none)\n- Page<T>: For paginated results with total count"
      },
      {
        "type": "KEY_POINT",
        "title": "Docker Compose for Local Development",
        "content": "Instead of installing PostgreSQL locally, use Docker Compose for consistent development environments.\n\n```yaml\n# docker-compose.yml (in project root)\nversion: '3.8'\n\nservices:\n  postgres:\n    image: postgres:16-alpine\n    container_name: taskmanager-db\n    environment:\n      POSTGRES_DB: taskmanager\n      POSTGRES_USER: taskuser\n      POSTGRES_PASSWORD: localdev123\n    ports:\n      - \"5432:5432\"\n    volumes:\n      - postgres_data:/var/lib/postgresql/data\n    healthcheck:\n      test: [\"CMD-SHELL\", \"pg_isready -U taskuser -d taskmanager\"]\n      interval: 5s\n      timeout: 5s\n      retries: 5\n\nvolumes:\n  postgres_data:\n```\n\nUsage Commands:\n- docker compose up -d: Start database in background\n- docker compose logs -f postgres: View database logs\n- docker compose down: Stop database (data preserved)\n- docker compose down -v: Stop and delete all data (fresh start)\n\nBenefits:\n- One command starts everything needed\n- Same environment for all team members\n- Easy to reset: down -v, then up -d\n- Matches production environment\n- No \"works on my machine\" problems"
      }
    ],
    "challenges": [
      {
        "type": "MULTIPLE_CHOICE",
        "id": "capstone-16-2-cascade",
        "title": "Understanding Cascade Types",
        "description": "Test your understanding of JPA cascade behavior.",
        "question": "In the User entity, we have @OneToMany(cascade = CascadeType.ALL, orphanRemoval = true) for tasks. What happens when we call userRepository.delete(user)?",
        "options": [
          {
            "id": "a",
            "text": "Only the user is deleted, tasks remain with null owner",
            "isCorrect": false
          },
          {
            "id": "b",
            "text": "The deletion fails because tasks still reference the user",
            "isCorrect": false
          },
          {
            "id": "c",
            "text": "The user and all their tasks are deleted automatically",
            "isCorrect": true
          },
          {
            "id": "d",
            "text": "Tasks are moved to a default system user",
            "isCorrect": false
          }
        ],
        "difficulty": "intermediate"
      },
      {
        "type": "MULTIPLE_CHOICE",
        "id": "capstone-16-2-flyway",
        "title": "Flyway Migration Best Practices",
        "description": "Understand proper schema migration workflow.",
        "question": "You need to add a 'completed_at' timestamp column to the tasks table. The V3__create_tasks.sql migration has already been applied. What should you do?",
        "options": [
          {
            "id": "a",
            "text": "Edit V3__create_tasks.sql to add the column",
            "isCorrect": false
          },
          {
            "id": "b",
            "text": "Create V4__add_completed_at_to_tasks.sql with ALTER TABLE",
            "isCorrect": true
          },
          {
            "id": "c",
            "text": "Delete the flyway_schema_history table and re-run all migrations",
            "isCorrect": false
          },
          {
            "id": "d",
            "text": "Add the column directly to the entity and let Hibernate update the schema",
            "isCorrect": false
          }
        ],
        "difficulty": "intermediate"
      },
      {
        "type": "FREE_CODING",
        "id": "capstone-16-2-repository-method",
        "title": "Write a Custom Repository Method",
        "description": "Add a method to TaskRepository that finds all high-priority tasks for a user that are not yet completed.",
        "instructions": "Write a repository method signature in TaskRepository that finds all tasks where:\n1. The owner matches the given ownerId\n2. Priority is HIGH or URGENT\n3. Status is not COMPLETED\n\nUse Spring Data JPA query derivation (method naming convention).",
        "starterCode": "// Add this method to TaskRepository interface\n// Find all incomplete high priority tasks for a user\n\n// Your method signature here:\n",
        "solution": "// Method using Spring Data JPA query derivation:\nList<Task> findByOwnerIdAndPriorityInAndStatusNot(\n    Long ownerId, \n    List<Priority> priorities, \n    TaskStatus status\n);\n\n// Called like:\n// taskRepository.findByOwnerIdAndPriorityInAndStatusNot(\n//     userId, \n//     List.of(Priority.HIGH, Priority.URGENT), \n//     TaskStatus.COMPLETED\n// );\n\n// Alternative using @Query:\n@Query(\"SELECT t FROM Task t WHERE t.owner.id = :ownerId \" +\n       \"AND t.priority IN ('HIGH', 'URGENT') \" +\n       \"AND t.status != 'COMPLETED'\")\nList<Task> findHighPriorityIncompleteTasks(@Param(\"ownerId\") Long ownerId);",
        "language": "java",
        "testCases": [],
        "hints": [
          {
            "level": 1,
            "text": "Spring Data JPA can derive queries from method names. findByOwnerIdAndPriorityAndStatus would match exact values."
          },
          {
            "level": 2,
            "text": "Use 'In' suffix for matching multiple values, and 'Not' suffix for negation."
          }
        ],
        "difficulty": "intermediate"
      }
    ]
  };
}

// ========== LESSON 16.3: REST API Development ==========
function createLesson16_3() {
  return {
    "id": "capstone-lesson-3",
    "title": "Lesson 16.3: REST API Development",
    "moduleId": "module-16",
    "order": 3,
    "estimatedMinutes": 60,
    "difficulty": "advanced",
    "contentSections": [
      {
        "type": "THEORY",
        "title": "REST API Design Principles",
        "content": "A well-designed REST API follows consistent conventions that make it intuitive for frontend developers and other API consumers. Let us establish our design principles.\n\nURL Structure:\nResources are nouns (not verbs), plural form:\n- /api/tasks (not /api/getTask or /api/task)\n- /api/categories (not /api/category)\n- /api/users (not /api/user)\n\nHTTP Methods define actions:\n- GET /api/tasks - List all tasks (for current user)\n- GET /api/tasks/123 - Get specific task\n- POST /api/tasks - Create new task\n- PUT /api/tasks/123 - Update entire task\n- PATCH /api/tasks/123 - Partial update\n- DELETE /api/tasks/123 - Delete task\n\nHTTP Status Codes communicate results:\n- 200 OK: Successful GET, PUT, PATCH\n- 201 Created: Successful POST (include Location header)\n- 204 No Content: Successful DELETE\n- 400 Bad Request: Invalid input data\n- 401 Unauthorized: Not authenticated\n- 403 Forbidden: Authenticated but not authorized\n- 404 Not Found: Resource does not exist\n- 409 Conflict: Duplicate resource (e.g., email exists)\n- 500 Internal Server Error: Server-side failure\n\nRequest/Response Format:\n- Always JSON (Content-Type: application/json)\n- Use camelCase for field names (not snake_case)\n- Include metadata for paginated responses\n- Never expose internal entity IDs in URLs for security-sensitive operations"
      },
      {
        "type": "THEORY",
        "title": "Data Transfer Objects (DTOs)",
        "content": "DTOs separate our API contract from our internal entities. This provides several benefits: security (we control what is exposed), flexibility (API can differ from database), and stability (internal changes do not break API).\n\n```java\n// com/taskmanager/dto/request/TaskRequest.java\npackage com.taskmanager.dto.request;\n\nimport com.taskmanager.model.enums.Priority;\nimport com.taskmanager.model.enums.TaskStatus;\nimport jakarta.validation.constraints.NotBlank;\nimport jakarta.validation.constraints.Size;\nimport java.time.LocalDate;\n\npublic class TaskRequest {\n    \n    @NotBlank(message = \"Title is required\")\n    @Size(max = 255, message = \"Title must be less than 255 characters\")\n    private String title;\n    \n    @Size(max = 5000, message = \"Description must be less than 5000 characters\")\n    private String description;\n    \n    private TaskStatus status;\n    \n    private Priority priority;\n    \n    private LocalDate dueDate;\n    \n    private Long categoryId;\n    \n    // Getters and setters\n    public String getTitle() { return title; }\n    public void setTitle(String title) { this.title = title; }\n    \n    public String getDescription() { return description; }\n    public void setDescription(String description) { this.description = description; }\n    \n    public TaskStatus getStatus() { return status; }\n    public void setStatus(TaskStatus status) { this.status = status; }\n    \n    public Priority getPriority() { return priority; }\n    public void setPriority(Priority priority) { this.priority = priority; }\n    \n    public LocalDate getDueDate() { return dueDate; }\n    public void setDueDate(LocalDate dueDate) { this.dueDate = dueDate; }\n    \n    public Long getCategoryId() { return categoryId; }\n    public void setCategoryId(Long categoryId) { this.categoryId = categoryId; }\n}\n```\n\nValidation annotations ensure data integrity before it reaches the service layer. @NotBlank checks for null and empty strings, @Size limits string length."
      },
      {
        "type": "THEORY",
        "title": "Response DTOs",
        "content": "Response DTOs define exactly what data we return to clients. They never include sensitive information like passwords.\n\n```java\n// com/taskmanager/dto/response/TaskResponse.java\npackage com.taskmanager.dto.response;\n\nimport com.taskmanager.model.Task;\nimport com.taskmanager.model.enums.Priority;\nimport com.taskmanager.model.enums.TaskStatus;\nimport java.time.LocalDate;\nimport java.time.LocalDateTime;\n\npublic class TaskResponse {\n    \n    private Long id;\n    private String title;\n    private String description;\n    private TaskStatus status;\n    private Priority priority;\n    private LocalDate dueDate;\n    private CategoryResponse category;\n    private LocalDateTime createdAt;\n    private LocalDateTime updatedAt;\n    \n    // Factory method to create from entity\n    public static TaskResponse fromEntity(Task task) {\n        TaskResponse response = new TaskResponse();\n        response.setId(task.getId());\n        response.setTitle(task.getTitle());\n        response.setDescription(task.getDescription());\n        response.setStatus(task.getStatus());\n        response.setPriority(task.getPriority());\n        response.setDueDate(task.getDueDate());\n        response.setCreatedAt(task.getCreatedAt());\n        response.setUpdatedAt(task.getUpdatedAt());\n        \n        if (task.getCategory() != null) {\n            response.setCategory(CategoryResponse.fromEntity(task.getCategory()));\n        }\n        \n        return response;\n    }\n    \n    // Getters and setters\n    public Long getId() { return id; }\n    public void setId(Long id) { this.id = id; }\n    \n    public String getTitle() { return title; }\n    public void setTitle(String title) { this.title = title; }\n    \n    public String getDescription() { return description; }\n    public void setDescription(String description) { this.description = description; }\n    \n    public TaskStatus getStatus() { return status; }\n    public void setStatus(TaskStatus status) { this.status = status; }\n    \n    public Priority getPriority() { return priority; }\n    public void setPriority(Priority priority) { this.priority = priority; }\n    \n    public LocalDate getDueDate() { return dueDate; }\n    public void setDueDate(LocalDate dueDate) { this.dueDate = dueDate; }\n    \n    public CategoryResponse getCategory() { return category; }\n    public void setCategory(CategoryResponse category) { this.category = category; }\n    \n    public LocalDateTime getCreatedAt() { return createdAt; }\n    public void setCreatedAt(LocalDateTime createdAt) { this.createdAt = createdAt; }\n    \n    public LocalDateTime getUpdatedAt() { return updatedAt; }\n    public void setUpdatedAt(LocalDateTime updatedAt) { this.updatedAt = updatedAt; }\n}\n\n// com/taskmanager/dto/response/CategoryResponse.java\npackage com.taskmanager.dto.response;\n\nimport com.taskmanager.model.Category;\n\npublic class CategoryResponse {\n    \n    private Long id;\n    private String name;\n    private String description;\n    private String color;\n    \n    public static CategoryResponse fromEntity(Category category) {\n        CategoryResponse response = new CategoryResponse();\n        response.setId(category.getId());\n        response.setName(category.getName());\n        response.setDescription(category.getDescription());\n        response.setColor(category.getColor());\n        return response;\n    }\n    \n    // Getters and setters\n    public Long getId() { return id; }\n    public void setId(Long id) { this.id = id; }\n    \n    public String getName() { return name; }\n    public void setName(String name) { this.name = name; }\n    \n    public String getDescription() { return description; }\n    public void setDescription(String description) { this.description = description; }\n    \n    public String getColor() { return color; }\n    public void setColor(String color) { this.color = color; }\n}\n```\n\nNotice the fromEntity factory method. This pattern keeps entity-to-DTO conversion logic in one place, making it easy to maintain and test."
      },
      {
        "type": "THEORY",
        "title": "Service Layer Pattern",
        "content": "The service layer contains business logic and coordinates between controllers and repositories. Controllers should be thin - they handle HTTP concerns only.\n\n```java\n// com/taskmanager/service/TaskService.java\npackage com.taskmanager.service;\n\nimport com.taskmanager.dto.request.TaskRequest;\nimport com.taskmanager.dto.response.TaskResponse;\nimport com.taskmanager.exception.ResourceNotFoundException;\nimport com.taskmanager.exception.UnauthorizedException;\nimport com.taskmanager.model.Category;\nimport com.taskmanager.model.Task;\nimport com.taskmanager.model.User;\nimport com.taskmanager.model.enums.Priority;\nimport com.taskmanager.model.enums.TaskStatus;\nimport com.taskmanager.repository.CategoryRepository;\nimport com.taskmanager.repository.TaskRepository;\nimport org.springframework.data.domain.Page;\nimport org.springframework.data.domain.Pageable;\nimport org.springframework.stereotype.Service;\nimport org.springframework.transaction.annotation.Transactional;\n\n@Service\n@Transactional\npublic class TaskService {\n    \n    private final TaskRepository taskRepository;\n    private final CategoryRepository categoryRepository;\n    \n    public TaskService(TaskRepository taskRepository, \n                       CategoryRepository categoryRepository) {\n        this.taskRepository = taskRepository;\n        this.categoryRepository = categoryRepository;\n    }\n    \n    @Transactional(readOnly = true)\n    public Page<TaskResponse> getTasksForUser(User user, Pageable pageable) {\n        return taskRepository.findByOwnerId(user.getId(), pageable)\n                .map(TaskResponse::fromEntity);\n    }\n    \n    @Transactional(readOnly = true)\n    public TaskResponse getTask(Long taskId, User user) {\n        Task task = findTaskAndVerifyOwnership(taskId, user);\n        return TaskResponse.fromEntity(task);\n    }\n    \n    public TaskResponse createTask(TaskRequest request, User user) {\n        Task task = new Task();\n        task.setTitle(request.getTitle());\n        task.setDescription(request.getDescription());\n        task.setStatus(request.getStatus() != null ? request.getStatus() : TaskStatus.PENDING);\n        task.setPriority(request.getPriority() != null ? request.getPriority() : Priority.MEDIUM);\n        task.setDueDate(request.getDueDate());\n        task.setOwner(user);\n        \n        if (request.getCategoryId() != null) {\n            Category category = categoryRepository\n                .findByIdAndOwnerId(request.getCategoryId(), user.getId())\n                .orElseThrow(() -> new ResourceNotFoundException(\n                    \"Category not found with id: \" + request.getCategoryId()));\n            task.setCategory(category);\n        }\n        \n        Task saved = taskRepository.save(task);\n        return TaskResponse.fromEntity(saved);\n    }\n    \n    public TaskResponse updateTask(Long taskId, TaskRequest request, User user) {\n        Task task = findTaskAndVerifyOwnership(taskId, user);\n        \n        task.setTitle(request.getTitle());\n        task.setDescription(request.getDescription());\n        if (request.getStatus() != null) {\n            task.setStatus(request.getStatus());\n        }\n        if (request.getPriority() != null) {\n            task.setPriority(request.getPriority());\n        }\n        task.setDueDate(request.getDueDate());\n        \n        if (request.getCategoryId() != null) {\n            Category category = categoryRepository\n                .findByIdAndOwnerId(request.getCategoryId(), user.getId())\n                .orElseThrow(() -> new ResourceNotFoundException(\n                    \"Category not found with id: \" + request.getCategoryId()));\n            task.setCategory(category);\n        } else {\n            task.setCategory(null);\n        }\n        \n        Task saved = taskRepository.save(task);\n        return TaskResponse.fromEntity(saved);\n    }\n    \n    public void deleteTask(Long taskId, User user) {\n        Task task = findTaskAndVerifyOwnership(taskId, user);\n        taskRepository.delete(task);\n    }\n    \n    private Task findTaskAndVerifyOwnership(Long taskId, User user) {\n        return taskRepository.findByIdAndOwnerId(taskId, user.getId())\n                .orElseThrow(() -> new ResourceNotFoundException(\n                    \"Task not found with id: \" + taskId));\n    }\n}\n```\n\nKey Service Layer Principles:\n- @Service marks this as a Spring-managed service bean\n- @Transactional ensures database operations are atomic\n- @Transactional(readOnly = true) optimizes read-only operations\n- Constructor injection for dependencies (not @Autowired on fields)\n- Always verify ownership before modifying resources\n- Convert entities to DTOs before returning"
      },
      {
        "type": "THEORY",
        "title": "TaskController: Full CRUD Implementation",
        "content": "The controller handles HTTP requests, delegates to services, and returns proper responses.\n\n```java\n// com/taskmanager/controller/TaskController.java\npackage com.taskmanager.controller;\n\nimport com.taskmanager.dto.request.TaskRequest;\nimport com.taskmanager.dto.response.TaskResponse;\nimport com.taskmanager.model.User;\nimport com.taskmanager.service.TaskService;\nimport jakarta.validation.Valid;\nimport org.springframework.data.domain.Page;\nimport org.springframework.data.domain.Pageable;\nimport org.springframework.data.domain.Sort;\nimport org.springframework.data.web.PageableDefault;\nimport org.springframework.http.HttpStatus;\nimport org.springframework.http.ResponseEntity;\nimport org.springframework.security.core.annotation.AuthenticationPrincipal;\nimport org.springframework.web.bind.annotation.*;\n\nimport java.net.URI;\n\n@RestController\n@RequestMapping(\"/api/tasks\")\npublic class TaskController {\n    \n    private final TaskService taskService;\n    \n    public TaskController(TaskService taskService) {\n        this.taskService = taskService;\n    }\n    \n    @GetMapping\n    public ResponseEntity<Page<TaskResponse>> getAllTasks(\n            @AuthenticationPrincipal User user,\n            @PageableDefault(size = 20, sort = \"createdAt\", \n                           direction = Sort.Direction.DESC) Pageable pageable) {\n        Page<TaskResponse> tasks = taskService.getTasksForUser(user, pageable);\n        return ResponseEntity.ok(tasks);\n    }\n    \n    @GetMapping(\"/{id}\")\n    public ResponseEntity<TaskResponse> getTask(\n            @PathVariable Long id,\n            @AuthenticationPrincipal User user) {\n        TaskResponse task = taskService.getTask(id, user);\n        return ResponseEntity.ok(task);\n    }\n    \n    @PostMapping\n    public ResponseEntity<TaskResponse> createTask(\n            @Valid @RequestBody TaskRequest request,\n            @AuthenticationPrincipal User user) {\n        TaskResponse created = taskService.createTask(request, user);\n        URI location = URI.create(\"/api/tasks/\" + created.getId());\n        return ResponseEntity.created(location).body(created);\n    }\n    \n    @PutMapping(\"/{id}\")\n    public ResponseEntity<TaskResponse> updateTask(\n            @PathVariable Long id,\n            @Valid @RequestBody TaskRequest request,\n            @AuthenticationPrincipal User user) {\n        TaskResponse updated = taskService.updateTask(id, request, user);\n        return ResponseEntity.ok(updated);\n    }\n    \n    @DeleteMapping(\"/{id}\")\n    public ResponseEntity<Void> deleteTask(\n            @PathVariable Long id,\n            @AuthenticationPrincipal User user) {\n        taskService.deleteTask(id, user);\n        return ResponseEntity.noContent().build();\n    }\n}\n```\n\nController Annotations Explained:\n- @RestController: Combines @Controller and @ResponseBody (returns JSON)\n- @RequestMapping(\"/api/tasks\"): Base URL for all endpoints\n- @GetMapping, @PostMapping, etc.: Map HTTP methods to handlers\n- @PathVariable: Extract values from URL path\n- @RequestBody: Deserialize JSON body to object\n- @Valid: Trigger validation annotations on the request object\n- @AuthenticationPrincipal: Inject the currently authenticated user\n- @PageableDefault: Set default pagination (size, sort field, direction)"
      },
      {
        "type": "THEORY",
        "title": "Pagination with Page and Pageable",
        "content": "Pagination is essential for APIs that return potentially large collections. Spring Data provides excellent pagination support out of the box.\n\nClient Request:\nGET /api/tasks?page=0&size=10&sort=dueDate,asc\n\nParsed by Spring into Pageable:\n- page: 0 (first page, zero-indexed)\n- size: 10 (items per page)\n- sort: dueDate ascending\n\nRepository method returns Page<Task>:\n```java\nPage<Task> findByOwnerId(Long ownerId, Pageable pageable);\n```\n\nPage object contains:\n- content: List of items for this page\n- totalElements: Total count across all pages\n- totalPages: Calculated from totalElements and size\n- number: Current page number\n- size: Requested page size\n- numberOfElements: Actual items on this page\n\nJSON Response Structure:\n```json\n{\n  \"content\": [\n    { \"id\": 1, \"title\": \"Task 1\", ... },\n    { \"id\": 2, \"title\": \"Task 2\", ... }\n  ],\n  \"pageable\": {\n    \"pageNumber\": 0,\n    \"pageSize\": 10,\n    \"sort\": { \"sorted\": true, \"direction\": \"ASC\" }\n  },\n  \"totalElements\": 47,\n  \"totalPages\": 5,\n  \"last\": false,\n  \"first\": true,\n  \"empty\": false\n}\n```\n\nFrontend can use this metadata to render pagination controls (\"Page 1 of 5\", Previous/Next buttons)."
      },
      {
        "type": "THEORY",
        "title": "Global Exception Handling",
        "content": "Proper error handling improves user experience and prevents exposing internal details.\n\n```java\n// com/taskmanager/exception/ResourceNotFoundException.java\npackage com.taskmanager.exception;\n\npublic class ResourceNotFoundException extends RuntimeException {\n    public ResourceNotFoundException(String message) {\n        super(message);\n    }\n}\n\n// com/taskmanager/exception/GlobalExceptionHandler.java\npackage com.taskmanager.exception;\n\nimport org.springframework.http.HttpStatus;\nimport org.springframework.http.ResponseEntity;\nimport org.springframework.validation.FieldError;\nimport org.springframework.web.bind.MethodArgumentNotValidException;\nimport org.springframework.web.bind.annotation.ExceptionHandler;\nimport org.springframework.web.bind.annotation.RestControllerAdvice;\n\nimport java.time.LocalDateTime;\nimport java.util.HashMap;\nimport java.util.Map;\n\n@RestControllerAdvice\npublic class GlobalExceptionHandler {\n    \n    @ExceptionHandler(ResourceNotFoundException.class)\n    public ResponseEntity<ErrorResponse> handleResourceNotFound(\n            ResourceNotFoundException ex) {\n        ErrorResponse error = new ErrorResponse(\n            HttpStatus.NOT_FOUND.value(),\n            ex.getMessage(),\n            LocalDateTime.now()\n        );\n        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(error);\n    }\n    \n    @ExceptionHandler(MethodArgumentNotValidException.class)\n    public ResponseEntity<Map<String, Object>> handleValidationErrors(\n            MethodArgumentNotValidException ex) {\n        Map<String, String> fieldErrors = new HashMap<>();\n        for (FieldError error : ex.getBindingResult().getFieldErrors()) {\n            fieldErrors.put(error.getField(), error.getDefaultMessage());\n        }\n        \n        Map<String, Object> response = new HashMap<>();\n        response.put(\"status\", HttpStatus.BAD_REQUEST.value());\n        response.put(\"message\", \"Validation failed\");\n        response.put(\"errors\", fieldErrors);\n        response.put(\"timestamp\", LocalDateTime.now());\n        \n        return ResponseEntity.badRequest().body(response);\n    }\n    \n    @ExceptionHandler(Exception.class)\n    public ResponseEntity<ErrorResponse> handleGenericException(Exception ex) {\n        // Log the full exception for debugging\n        // logger.error(\"Unexpected error\", ex);\n        \n        ErrorResponse error = new ErrorResponse(\n            HttpStatus.INTERNAL_SERVER_ERROR.value(),\n            \"An unexpected error occurred\",  // Never expose details\n            LocalDateTime.now()\n        );\n        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(error);\n    }\n}\n\n// com/taskmanager/exception/ErrorResponse.java\npackage com.taskmanager.exception;\n\nimport java.time.LocalDateTime;\n\npublic class ErrorResponse {\n    private int status;\n    private String message;\n    private LocalDateTime timestamp;\n    \n    public ErrorResponse(int status, String message, LocalDateTime timestamp) {\n        this.status = status;\n        this.message = message;\n        this.timestamp = timestamp;\n    }\n    \n    // Getters\n    public int getStatus() { return status; }\n    public String getMessage() { return message; }\n    public LocalDateTime getTimestamp() { return timestamp; }\n}\n```\n\n@RestControllerAdvice catches exceptions from all controllers and converts them to proper HTTP responses. This centralizes error handling and ensures consistent error format across the API."
      },
      {
        "type": "KEY_POINT",
        "title": "Testing the API with curl or Postman",
        "content": "Before building the frontend, test your API thoroughly using curl or Postman.\n\nCreate a Task:\n```bash\ncurl -X POST http://localhost:8080/api/tasks \\\n  -H \"Content-Type: application/json\" \\\n  -H \"Authorization: Bearer <your-jwt-token>\" \\\n  -d '{\n    \"title\": \"Learn Spring Boot\",\n    \"description\": \"Complete the capstone project\",\n    \"priority\": \"HIGH\",\n    \"dueDate\": \"2024-12-31\"\n  }'\n```\n\nExpected Response (201 Created):\n```json\n{\n  \"id\": 1,\n  \"title\": \"Learn Spring Boot\",\n  \"description\": \"Complete the capstone project\",\n  \"status\": \"PENDING\",\n  \"priority\": \"HIGH\",\n  \"dueDate\": \"2024-12-31\",\n  \"category\": null,\n  \"createdAt\": \"2024-01-15T10:30:00\",\n  \"updatedAt\": \"2024-01-15T10:30:00\"\n}\n```\n\nGet All Tasks (Paginated):\n```bash\ncurl http://localhost:8080/api/tasks?page=0&size=5 \\\n  -H \"Authorization: Bearer <your-jwt-token>\"\n```\n\nUpdate a Task:\n```bash\ncurl -X PUT http://localhost:8080/api/tasks/1 \\\n  -H \"Content-Type: application/json\" \\\n  -H \"Authorization: Bearer <your-jwt-token>\" \\\n  -d '{\"title\": \"Updated Title\", \"status\": \"COMPLETED\"}'\n```\n\nDelete a Task:\n```bash\ncurl -X DELETE http://localhost:8080/api/tasks/1 \\\n  -H \"Authorization: Bearer <your-jwt-token>\"\n```\n\nExpected Response: 204 No Content (empty body)"
      },
      {
        "type": "WARNING",
        "title": "Common REST API Mistakes",
        "content": "Avoid these common mistakes when building REST APIs:\n\nMistake 1: Verbs in URLs\nWrong: POST /api/createTask\nRight: POST /api/tasks\n\nMistake 2: Inconsistent naming\nWrong: /api/tasks but /api/getCategories\nRight: /api/tasks and /api/categories\n\nMistake 3: Wrong HTTP methods\nWrong: GET /api/tasks/delete/1\nRight: DELETE /api/tasks/1\n\nMistake 4: Not using proper status codes\nWrong: Return 200 OK with {\"error\": \"Not found\"}\nRight: Return 404 Not Found with error body\n\nMistake 5: Exposing entity IDs for all operations\nWrong: GET /api/users/1/password\nRight: Do not expose password endpoint at all\n\nMistake 6: No pagination for list endpoints\nWrong: GET /api/tasks returns all 10,000 tasks\nRight: GET /api/tasks?page=0&size=20 with limits\n\nMistake 7: Returning entities instead of DTOs\nWrong: Return User entity (includes password hash)\nRight: Return UserResponse DTO (no password)\n\nMistake 8: No input validation\nWrong: Accept any input, let database fail\nRight: Validate with @Valid and return 400 for bad input"
      }
    ],
    "challenges": [
      {
        "type": "MULTIPLE_CHOICE",
        "id": "capstone-16-3-http-status",
        "title": "HTTP Status Codes",
        "description": "Choose the correct status code for each scenario.",
        "question": "A user tries to update a task that belongs to another user. What HTTP status code should the API return?",
        "options": [
          {
            "id": "a",
            "text": "400 Bad Request - the request data was invalid",
            "isCorrect": false
          },
          {
            "id": "b",
            "text": "401 Unauthorized - the user is not logged in",
            "isCorrect": false
          },
          {
            "id": "c",
            "text": "403 Forbidden - the user is logged in but not allowed to access this resource",
            "isCorrect": false
          },
          {
            "id": "d",
            "text": "404 Not Found - the task was not found (for this user)",
            "isCorrect": true
          }
        ],
        "difficulty": "intermediate"
      },
      {
        "type": "MULTIPLE_CHOICE",
        "id": "capstone-16-3-pagination",
        "title": "Understanding Pagination",
        "description": "Test your understanding of Spring Data pagination.",
        "question": "A client requests GET /api/tasks?page=2&size=10. The database has 47 tasks total. What will the response's 'content' array contain?",
        "options": [
          {
            "id": "a",
            "text": "Tasks 1-10 (first page, ignoring page parameter)",
            "isCorrect": false
          },
          {
            "id": "b",
            "text": "Tasks 11-20 (page 2 means second page)",
            "isCorrect": false
          },
          {
            "id": "c",
            "text": "Tasks 21-30 (page is zero-indexed, so page=2 is the third page)",
            "isCorrect": true
          },
          {
            "id": "d",
            "text": "Tasks 21-47 (all remaining tasks from page 2 onwards)",
            "isCorrect": false
          }
        ],
        "difficulty": "intermediate"
      },
      {
        "type": "FREE_CODING",
        "id": "capstone-16-3-category-controller",
        "title": "Implement CategoryController",
        "description": "Create a CategoryController with CRUD endpoints following the patterns from TaskController.",
        "instructions": "Implement the CategoryController class with the following endpoints:\n1. GET /api/categories - List all categories for current user\n2. POST /api/categories - Create new category\n3. PUT /api/categories/{id} - Update category\n4. DELETE /api/categories/{id} - Delete category\n\nFollow the same patterns as TaskController: use DTOs, inject the authenticated user, return proper status codes.",
        "starterCode": "// Implement CategoryController\n// Follow the patterns from TaskController\n\n@RestController\n@RequestMapping(\"/api/categories\")\npublic class CategoryController {\n    \n    // TODO: Add constructor and dependencies\n    \n    // TODO: Implement CRUD endpoints\n    \n}",
        "solution": "@RestController\n@RequestMapping(\"/api/categories\")\npublic class CategoryController {\n    \n    private final CategoryService categoryService;\n    \n    public CategoryController(CategoryService categoryService) {\n        this.categoryService = categoryService;\n    }\n    \n    @GetMapping\n    public ResponseEntity<List<CategoryResponse>> getAllCategories(\n            @AuthenticationPrincipal User user) {\n        List<CategoryResponse> categories = categoryService.getCategoriesForUser(user);\n        return ResponseEntity.ok(categories);\n    }\n    \n    @GetMapping(\"/{id}\")\n    public ResponseEntity<CategoryResponse> getCategory(\n            @PathVariable Long id,\n            @AuthenticationPrincipal User user) {\n        CategoryResponse category = categoryService.getCategory(id, user);\n        return ResponseEntity.ok(category);\n    }\n    \n    @PostMapping\n    public ResponseEntity<CategoryResponse> createCategory(\n            @Valid @RequestBody CategoryRequest request,\n            @AuthenticationPrincipal User user) {\n        CategoryResponse created = categoryService.createCategory(request, user);\n        URI location = URI.create(\"/api/categories/\" + created.getId());\n        return ResponseEntity.created(location).body(created);\n    }\n    \n    @PutMapping(\"/{id}\")\n    public ResponseEntity<CategoryResponse> updateCategory(\n            @PathVariable Long id,\n            @Valid @RequestBody CategoryRequest request,\n            @AuthenticationPrincipal User user) {\n        CategoryResponse updated = categoryService.updateCategory(id, request, user);\n        return ResponseEntity.ok(updated);\n    }\n    \n    @DeleteMapping(\"/{id}\")\n    public ResponseEntity<Void> deleteCategory(\n            @PathVariable Long id,\n            @AuthenticationPrincipal User user) {\n        categoryService.deleteCategory(id, user);\n        return ResponseEntity.noContent().build();\n    }\n}",
        "language": "java",
        "testCases": [],
        "hints": [
          {
            "level": 1,
            "text": "Follow the exact pattern from TaskController - constructor injection, @AuthenticationPrincipal, ResponseEntity return types."
          },
          {
            "level": 2,
            "text": "Categories do not need pagination since users typically have fewer categories. Return List<CategoryResponse> instead of Page."
          }
        ],
        "difficulty": "intermediate"
      }
    ]
  };
}
