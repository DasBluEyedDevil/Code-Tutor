# Add Missing Challenges Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Add coding challenges to the 29 lessons currently without any interactive exercises.

**Architecture:** Each lesson needs 1-2 challenges (FREE_CODING or MULTIPLE_CHOICE) that test the concepts taught. Challenges follow the established JSON structure with id, title, description, starterCode, solution, and testCases.

**Tech Stack:** JSON course file, JavaScript for batch updates

---

## Challenge Structure Reference

```json
{
  "type": "FREE_CODING",
  "id": "unique-challenge-id",
  "title": "Challenge Title",
  "description": "What the student needs to do",
  "instructions": "Same as description",
  "starterCode": "// Starter code with void main() {...}",
  "solution": "// Complete working solution",
  "language": "java",
  "testCases": [
    {
      "id": "test-id",
      "description": "What this test checks",
      "expectedOutput": "Expected console output",
      "isVisible": true
    }
  ]
}
```

---

## Lessons Requiring Challenges

| Module | Lessons | Count |
|--------|---------|-------|
| module-01 (Fundamentals) | 1.1 | 1 |
| module-02 (Data Types) | 2.6 | 1 |
| module-08 (Testing) | 8.1, 8.3, 8.4, 8.6 | 4 |
| module-06 (Databases) | 9.1-9.7 | 7 |
| module-07 (Web) | 10.1-10.3 | 3 |
| module-11 (Spring Boot) | 11.1-11.7 | 7 |
| module-full-stack | 15.1-15.6 | 6 |
| **Total** | | **29** |

---

## Task 1: Add Challenges to Module 1 & 2 (2 lessons)

**Files:**
- Modify: `content/courses/java/course.json`

**Lessons:**
1. **Lesson 1.1: What is a Computer Program?** (epoch-0-lesson-1)
2. **Lesson 2.6: What Do 'public', 'static', and 'void' Mean?** (epoch-1-lesson-6)

**Challenges to Add:**

### Lesson 1.1 Challenge: Recipe Understanding
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-1-1-recipe",
  "title": "Computer Instructions Quiz",
  "description": "What happens if you tell a computer to 'spread peanut butter on bread' without specifying which side?",
  "options": [
    "The computer figures out you meant the inside",
    "The computer asks for clarification",
    "The computer does exactly what you said, even if wrong",
    "The computer refuses to execute"
  ],
  "correctAnswer": 2,
  "explanation": "Computers are literal - they do exactly what you tell them, nothing more. They cannot infer intent or use common sense."
}
```

### Lesson 2.6 Challenge: Access Modifiers
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-2-6-access",
  "title": "Understanding public static void",
  "description": "In 'public static void main(String[] args)', what does 'static' mean?",
  "options": [
    "The method cannot be changed",
    "The method belongs to the class, not an object",
    "The method runs automatically",
    "The method is required"
  ],
  "correctAnswer": 1,
  "explanation": "Static means the method belongs to the class itself, so it can be called without creating an object first. That's why main() can run before any objects exist."
}
```

**Step 1:** Read course.json and locate lesson epoch-0-lesson-1

**Step 2:** Add the MULTIPLE_CHOICE challenge to the challenges array

**Step 3:** Locate lesson epoch-1-lesson-6 and add its challenge

**Step 4:** Commit
```bash
git add content/courses/java/course.json
git commit -m "feat(java): add challenges to lessons 1.1 and 2.6"
```

---

## Task 2: Add Challenges to Module 8 - Testing (4 lessons)

**Files:**
- Modify: `content/courses/java/course.json`

**Lessons:**
1. **8.1: Why Test Your Code?** (epoch-4-lesson-1)
2. **8.3: Test-Driven Development** (epoch-4-lesson-3)
3. **8.4: Maven - Managing Projects** (epoch-4-lesson-4)
4. **8.6: Debugging, Logging, Professional Habits** (epoch-4-lesson-6)

**Challenges to Add:**

### Lesson 8.1: Testing Benefits Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-8-1-testing-benefits",
  "title": "Why Write Tests?",
  "description": "What is the PRIMARY benefit of automated tests over manual testing?",
  "options": [
    "Tests are faster to write",
    "Tests can run automatically and repeatedly without human effort",
    "Tests are always 100% accurate",
    "Tests eliminate the need for debugging"
  ],
  "correctAnswer": 1,
  "explanation": "Automated tests run instantly and consistently every time. Manual testing is slow, error-prone, and doesn't scale."
}
```

### Lesson 8.3: TDD Order Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-8-3-tdd-order",
  "title": "TDD Cycle",
  "description": "What is the correct order of the TDD cycle?",
  "options": [
    "Write code, write test, refactor",
    "Write test, write code, refactor",
    "Refactor, write test, write code",
    "Write code, refactor, write test"
  ],
  "correctAnswer": 1,
  "explanation": "TDD follows Red-Green-Refactor: Write a failing test (Red), write minimal code to pass (Green), then clean up (Refactor)."
}
```

### Lesson 8.4: Maven Commands Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-8-4-maven-commands",
  "title": "Maven Commands",
  "description": "Which Maven command compiles code, runs tests, and creates a JAR file?",
  "options": [
    "mvn compile",
    "mvn test",
    "mvn package",
    "mvn install"
  ],
  "correctAnswer": 2,
  "explanation": "mvn package runs the full lifecycle: compile -> test -> package (create JAR). mvn install goes further by copying to local repository."
}
```

### Lesson 8.6: Logging Levels Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-8-6-logging",
  "title": "Logging Best Practices",
  "description": "Which logging level should you use for 'User logged in successfully'?",
  "options": [
    "DEBUG - it's implementation detail",
    "INFO - it's a normal business event",
    "WARN - something might be wrong",
    "ERROR - something failed"
  ],
  "correctAnswer": 1,
  "explanation": "INFO is for normal, significant business events. DEBUG is for development details. WARN/ERROR are for problems."
}
```

**Step 1:** Locate lessons epoch-4-lesson-1, epoch-4-lesson-3, epoch-4-lesson-4, epoch-4-lesson-6

**Step 2:** Add challenges to each lesson's challenges array

**Step 3:** Commit
```bash
git add content/courses/java/course.json
git commit -m "feat(java): add challenges to testing module (8.1, 8.3, 8.4, 8.6)"
```

---

## Task 3: Add Challenges to Module 6 - Databases (7 lessons)

**Files:**
- Modify: `content/courses/java/course.json`

**Lessons:**
1. **9.1: Why Do We Need Databases?** (epoch-5-lesson-1)
2. **9.2: SQL Basics** (epoch-5-lesson-2)
3. **9.3: SQL Queries** (epoch-5-lesson-3)
4. **9.4: JOINs** (epoch-5-lesson-4)
5. **9.5: JDBC** (epoch-5-lesson-5)
6. **9.6: JPA and Hibernate** (epoch-5-lesson-6)
7. **9.7: Flyway Migrations** (epoch-5-lesson-7)

**Challenges to Add:**

### Lesson 9.1: Database Persistence Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-9-1-persistence",
  "title": "Why Databases?",
  "description": "Why can't we just use variables to store user data permanently?",
  "options": [
    "Variables are too slow",
    "Variables only exist while the program runs",
    "Variables can only store numbers",
    "Variables are not secure"
  ],
  "correctAnswer": 1,
  "explanation": "Variables exist in RAM (memory) and disappear when the program ends. Databases store data on disk permanently."
}
```

### Lesson 9.2: SQL CREATE TABLE
```json
{
  "type": "FREE_CODING",
  "id": "lesson-9-2-create-table",
  "title": "Create a Students Table",
  "description": "Write SQL to create a 'students' table with: id (integer, primary key), name (varchar 100), email (varchar 255, unique), gpa (decimal).",
  "instructions": "Write the CREATE TABLE statement. Use standard SQL syntax.",
  "starterCode": "-- Create the students table\n",
  "solution": "-- Create the students table\nCREATE TABLE students (\n    id INT PRIMARY KEY AUTO_INCREMENT,\n    name VARCHAR(100) NOT NULL,\n    email VARCHAR(255) UNIQUE,\n    gpa DECIMAL(3,2)\n);",
  "language": "sql",
  "testCases": [
    {
      "id": "test-create-table",
      "description": "Should create table with correct columns",
      "expectedOutput": "CREATE TABLE students",
      "isVisible": true
    }
  ]
}
```

### Lesson 9.3: SQL SELECT with WHERE
```json
{
  "type": "FREE_CODING",
  "id": "lesson-9-3-select-where",
  "title": "Query High-GPA Students",
  "description": "Write a SQL query to find all students with GPA above 3.5, sorted by GPA descending.",
  "instructions": "Use SELECT, WHERE, and ORDER BY clauses.",
  "starterCode": "-- Find high-GPA students\n",
  "solution": "-- Find high-GPA students\nSELECT * FROM students\nWHERE gpa > 3.5\nORDER BY gpa DESC;",
  "language": "sql",
  "testCases": [
    {
      "id": "test-select-where",
      "description": "Should filter and sort correctly",
      "expectedOutput": "SELECT * FROM students WHERE gpa > 3.5 ORDER BY gpa DESC",
      "isVisible": true
    }
  ]
}
```

### Lesson 9.4: SQL JOIN Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-9-4-join-types",
  "title": "JOIN Types",
  "description": "You want ALL students, even those not enrolled in any course. Which JOIN should you use?",
  "options": [
    "INNER JOIN",
    "LEFT JOIN from students",
    "RIGHT JOIN from courses",
    "CROSS JOIN"
  ],
  "correctAnswer": 1,
  "explanation": "LEFT JOIN keeps all rows from the left table (students) even if there's no match in the right table (enrollments)."
}
```

### Lesson 9.5: JDBC PreparedStatement
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-9-5-prepared-stmt",
  "title": "Why PreparedStatement?",
  "description": "Why should you use PreparedStatement instead of concatenating SQL strings?",
  "options": [
    "PreparedStatement is faster",
    "PreparedStatement prevents SQL injection attacks",
    "PreparedStatement uses less memory",
    "PreparedStatement is easier to write"
  ],
  "correctAnswer": 1,
  "explanation": "PreparedStatement parameterizes queries, preventing attackers from injecting malicious SQL. String concatenation is a major security vulnerability."
}
```

### Lesson 9.6: JPA Entity Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-9-6-jpa-entity",
  "title": "JPA Annotations",
  "description": "Which annotation marks a class as a database entity in JPA?",
  "options": [
    "@Table",
    "@Entity",
    "@Repository",
    "@Model"
  ],
  "correctAnswer": 1,
  "explanation": "@Entity marks a class as a JPA entity (maps to a database table). @Table customizes the table name. @Repository is for Spring Data."
}
```

### Lesson 9.7: Flyway Migration Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-9-7-flyway",
  "title": "Flyway Naming",
  "description": "What is the correct Flyway migration file naming convention?",
  "options": [
    "migration_1.sql",
    "V1__create_users.sql",
    "1_create_users.sql",
    "create_users_v1.sql"
  ],
  "correctAnswer": 1,
  "explanation": "Flyway uses V{version}__{description}.sql format. The V prefix and double underscore are required. Version determines execution order."
}
```

**Step 1:** Locate all 7 database lessons (epoch-5-lesson-1 through epoch-5-lesson-7)

**Step 2:** Add appropriate challenges to each lesson

**Step 3:** Commit
```bash
git add content/courses/java/course.json
git commit -m "feat(java): add challenges to database module (9.1-9.7)"
```

---

## Task 4: Add Challenges to Module 7 - Web Fundamentals (3 lessons)

**Files:**
- Modify: `content/courses/java/course.json`

**Lessons:**
1. **10.1: How Does the Web Work?** (epoch-6-lesson-1)
2. **10.2: REST APIs** (epoch-6-lesson-2)
3. **10.3: HttpClient** (epoch-6-lesson-3)

**Challenges to Add:**

### Lesson 10.1: HTTP Methods Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-10-1-http-methods",
  "title": "HTTP Request Methods",
  "description": "Which HTTP method should you use to UPDATE an existing resource?",
  "options": [
    "GET",
    "POST",
    "PUT",
    "DELETE"
  ],
  "correctAnswer": 2,
  "explanation": "PUT updates an existing resource. POST creates new resources. GET reads data. DELETE removes resources."
}
```

### Lesson 10.2: REST Status Codes Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-10-2-status-codes",
  "title": "HTTP Status Codes",
  "description": "What does HTTP status code 404 mean?",
  "options": [
    "Request succeeded",
    "Server error",
    "Resource not found",
    "Unauthorized"
  ],
  "correctAnswer": 2,
  "explanation": "404 = Not Found. 200 = OK. 500 = Server Error. 401 = Unauthorized. 403 = Forbidden."
}
```

### Lesson 10.3: HttpClient Usage
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-10-3-httpclient",
  "title": "Java HttpClient",
  "description": "Which Java HttpClient method sends the request and waits for the response?",
  "options": [
    "client.execute(request)",
    "client.send(request, handler)",
    "client.call(request)",
    "client.fetch(request)"
  ],
  "correctAnswer": 1,
  "explanation": "HttpClient.send(request, bodyHandler) sends the request synchronously. sendAsync() is for async requests."
}
```

**Step 1:** Locate lessons epoch-6-lesson-1, epoch-6-lesson-2, epoch-6-lesson-3

**Step 2:** Add challenges to each

**Step 3:** Commit
```bash
git add content/courses/java/course.json
git commit -m "feat(java): add challenges to web fundamentals module (10.1-10.3)"
```

---

## Task 5: Add Challenges to Module 11 - Spring Boot (7 lessons)

**Files:**
- Modify: `content/courses/java/course.json`

**Lessons:**
1. **11.1: Why Spring Boot?** (epoch-7-lesson-1)
2. **11.2: REST Controllers** (epoch-7-lesson-2)
3. **11.3: Spring Data JPA** (epoch-7-lesson-3)
4. **11.4: Dependency Injection** (epoch-7-lesson-4)
5. **11.5: Configuration** (epoch-7-lesson-5)
6. **11.6: Exception Handling** (epoch-7-lesson-6)
7. **11.7: Spring Security** (epoch-7-lesson-7)

**Challenges to Add:**

### Lesson 11.1: Spring Boot Benefits Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-11-1-spring-benefits",
  "title": "Why Spring Boot?",
  "description": "What is the main advantage of Spring Boot over plain Spring Framework?",
  "options": [
    "Spring Boot is faster",
    "Spring Boot auto-configures common patterns",
    "Spring Boot uses less memory",
    "Spring Boot is more secure"
  ],
  "correctAnswer": 1,
  "explanation": "Spring Boot's auto-configuration eliminates boilerplate. It detects dependencies and configures them automatically."
}
```

### Lesson 11.2: REST Controller Annotations
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-11-2-rest-annotations",
  "title": "Controller Annotations",
  "description": "Which annotation combination creates a REST API controller?",
  "options": [
    "@Controller only",
    "@RestController (or @Controller + @ResponseBody)",
    "@Service + @RequestMapping",
    "@Component + @GetMapping"
  ],
  "correctAnswer": 1,
  "explanation": "@RestController = @Controller + @ResponseBody. It returns data directly (not views) as JSON/XML."
}
```

### Lesson 11.3: Spring Data JPA Methods
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-11-3-jpa-methods",
  "title": "Repository Query Methods",
  "description": "What method does Spring Data JPA generate for 'findByEmailAndActiveTrue'?",
  "options": [
    "SELECT * FROM users",
    "SELECT * FROM users WHERE email = ? AND active = true",
    "SELECT email FROM users WHERE active = 1",
    "SELECT * FROM users WHERE email LIKE ?"
  ],
  "correctAnswer": 1,
  "explanation": "Spring Data parses method names into queries. 'findByEmailAndActiveTrue' becomes WHERE email = ? AND active = true."
}
```

### Lesson 11.4: Dependency Injection Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-11-4-di",
  "title": "Dependency Injection",
  "description": "What is the recommended way to inject dependencies in Spring?",
  "options": [
    "@Autowired on fields",
    "Constructor injection",
    "Setter injection",
    "Static factory methods"
  ],
  "correctAnswer": 1,
  "explanation": "Constructor injection is recommended because: dependencies are explicit, objects are immutable, and testing is easier."
}
```

### Lesson 11.5: Configuration Properties
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-11-5-config",
  "title": "External Configuration",
  "description": "Where does Spring Boot look for application.yml by default?",
  "options": [
    "src/main/java/",
    "src/main/resources/",
    "project root folder",
    "~/.spring/"
  ],
  "correctAnswer": 1,
  "explanation": "Spring Boot looks in src/main/resources/ for application.yml or application.properties."
}
```

### Lesson 11.6: Exception Handler
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-11-6-exception",
  "title": "Global Exception Handling",
  "description": "Which annotation creates a global exception handler in Spring?",
  "options": [
    "@ExceptionHandler",
    "@ControllerAdvice",
    "@ErrorHandler",
    "@GlobalHandler"
  ],
  "correctAnswer": 1,
  "explanation": "@ControllerAdvice creates a global handler. @ExceptionHandler inside it handles specific exceptions."
}
```

### Lesson 11.7: Spring Security Authentication
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-11-7-security",
  "title": "Spring Security",
  "description": "In Spring Security 6, how do you configure authorization rules?",
  "options": [
    "Extend WebSecurityConfigurerAdapter",
    "Create a SecurityFilterChain bean with lambda DSL",
    "Use @Secured annotations only",
    "Configure in application.yml"
  ],
  "correctAnswer": 1,
  "explanation": "Spring Security 6 uses SecurityFilterChain beans with lambda DSL. WebSecurityConfigurerAdapter is deprecated."
}
```

**Step 1:** Locate all 7 Spring Boot lessons (epoch-7-lesson-1 through epoch-7-lesson-7)

**Step 2:** Add appropriate challenges

**Step 3:** Commit
```bash
git add content/courses/java/course.json
git commit -m "feat(java): add challenges to Spring Boot module (11.1-11.7)"
```

---

## Task 6: Add Challenges to Full-Stack Module (6 lessons)

**Files:**
- Modify: `content/courses/java/course.json`

**Lessons:**
1. **15.1: Connecting Frontend to API** (epoch-8-lesson-1)
2. **15.2: Full-Stack Feature End to End** (epoch-8-lesson-2)
3. **15.3: REST API Design Standards** (epoch-8-lesson-3)
4. **15.4: Error Handling from DB to UI** (epoch-8-lesson-4)
5. **15.5: Deployment** (epoch-8-lesson-5)
6. **15.6: Complete Feature** (epoch-8-lesson-6)

**Challenges to Add:**

### Lesson 15.1: CORS Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-15-1-cors",
  "title": "CORS Configuration",
  "description": "Why do you need CORS configuration when React runs on localhost:5173 and Spring on localhost:8080?",
  "options": [
    "The ports are different",
    "Browsers block cross-origin requests by default",
    "React and Spring use different protocols",
    "Spring requires explicit frontend registration"
  ],
  "correctAnswer": 1,
  "explanation": "Browsers enforce Same-Origin Policy. Different ports = different origins. CORS headers explicitly allow cross-origin requests."
}
```

### Lesson 15.2: Full-Stack Flow Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-15-2-flow",
  "title": "Request Flow",
  "description": "In a full-stack app, what is the correct order when a user submits a form?",
  "options": [
    "Database -> Backend -> Frontend",
    "Frontend -> Database -> Backend",
    "Frontend -> Backend -> Database -> Backend -> Frontend",
    "Backend -> Frontend -> Database"
  ],
  "correctAnswer": 2,
  "explanation": "User action -> Frontend (React) -> API call -> Backend (Spring) -> Database -> Response -> Backend -> Frontend -> UI update."
}
```

### Lesson 15.3: API Design Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-15-3-api-design",
  "title": "RESTful URL Design",
  "description": "Which URL follows REST best practices for getting user 123's orders?",
  "options": [
    "GET /getUserOrders?id=123",
    "GET /users/123/orders",
    "POST /orders/getByUser/123",
    "GET /api/fetch-orders-for-user-123"
  ],
  "correctAnswer": 1,
  "explanation": "REST uses nouns (resources) and nesting. /users/123/orders is resource-oriented. Verbs belong in HTTP methods, not URLs."
}
```

### Lesson 15.4: Error Propagation Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-15-4-errors",
  "title": "Error Handling Flow",
  "description": "When a database constraint fails, how should the error reach the user?",
  "options": [
    "Show the SQL error message directly",
    "Return 500 Internal Server Error with no details",
    "Catch exception, return structured error response, display user-friendly message",
    "Let the frontend handle the raw exception"
  ],
  "correctAnswer": 2,
  "explanation": "Backend catches DB exceptions, maps to Problem Details (RFC 7807), returns appropriate status. Frontend shows user-friendly message."
}
```

### Lesson 15.5: Deployment Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-15-5-deployment",
  "title": "Production Deployment",
  "description": "Why use environment variables for database credentials instead of hardcoding?",
  "options": [
    "Environment variables are faster",
    "Hardcoded values don't work in production",
    "Secrets shouldn't be in source code (security + flexibility)",
    "Environment variables use less memory"
  ],
  "correctAnswer": 2,
  "explanation": "Source code is often public (GitHub). Environment variables keep secrets separate and allow different values per environment."
}
```

### Lesson 15.6: Integration Testing Quiz
```json
{
  "type": "MULTIPLE_CHOICE",
  "id": "lesson-15-6-integration",
  "title": "Full-Stack Testing",
  "description": "What's the difference between unit tests and integration tests?",
  "options": [
    "Unit tests are faster, integration tests are slower",
    "Unit tests test components in isolation, integration tests test components together",
    "Unit tests use mocks, integration tests don't",
    "Integration tests require a browser"
  ],
  "correctAnswer": 1,
  "explanation": "Unit tests isolate single components (mock dependencies). Integration tests verify components work together correctly."
}
```

**Step 1:** Locate all 6 full-stack lessons (epoch-8-lesson-1 through epoch-8-lesson-6)

**Step 2:** Add appropriate challenges

**Step 3:** Commit
```bash
git add content/courses/java/course.json
git commit -m "feat(java): add challenges to full-stack module (15.1-15.6)"
```

---

## Task 7: Final Verification

**Step 1:** Run verification script to count challenges
```bash
node -e "
const fs = require('fs');
const course = JSON.parse(fs.readFileSync('content/courses/java/course.json', 'utf8'));
let totalChallenges = 0;
let lessonsWithChallenges = 0;
let lessonsWithoutChallenges = [];
course.modules.forEach(m => {
  m.lessons.forEach(l => {
    if (l.challenges && l.challenges.length > 0) {
      lessonsWithChallenges++;
      totalChallenges += l.challenges.length;
    } else {
      lessonsWithoutChallenges.push(l.title);
    }
  });
});
console.log('Total lessons:', lessonsWithChallenges + lessonsWithoutChallenges.length);
console.log('Lessons WITH challenges:', lessonsWithChallenges);
console.log('Lessons WITHOUT challenges:', lessonsWithoutChallenges.length);
console.log('Total challenges:', totalChallenges);
if (lessonsWithoutChallenges.length > 0) {
  console.log('Missing:', lessonsWithoutChallenges);
}
"
```

**Expected Output:**
```
Total lessons: 96
Lessons WITH challenges: 96
Lessons WITHOUT challenges: 0
Total challenges: ~160+
```

**Step 2:** Commit final state
```bash
git add -A
git commit -m "feat(java): complete - all 96 lessons now have challenges"
```

---

## Success Criteria

- [ ] All 29 lessons now have at least 1 challenge
- [ ] Challenges match lesson content
- [ ] Challenge formats follow established JSON structure
- [ ] All commits pass (no JSON syntax errors)
- [ ] Verification script shows 0 lessons without challenges
