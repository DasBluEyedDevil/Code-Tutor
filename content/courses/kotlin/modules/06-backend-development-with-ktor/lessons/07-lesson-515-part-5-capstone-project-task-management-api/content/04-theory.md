---
type: "THEORY"
title: "Requirements"
---


### 1. User Management

**Models**:

**Endpoints**:
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and receive JWT token
- `GET /api/auth/me` - Get current user (protected)
- `PUT /api/users/me` - Update profile (protected)
- `DELETE /api/users/me` - Delete account (protected)

**Requirements**:
- Email validation
- Password strength requirements (min 8 chars, uppercase, lowercase, number, special char)
- Username uniqueness
- JWT tokens with 1-hour expiration
- bcrypt password hashing

---

### 2. Task Management

**Models**:

**Endpoints**:
- `POST /api/tasks` - Create task (protected)
- `GET /api/tasks` - Get all user's tasks with filters (protected)
- `GET /api/tasks/:id` - Get task by ID (protected)
- `PUT /api/tasks/:id` - Update task (owner or assignee)
- `DELETE /api/tasks/:id` - Delete task (owner only)
- `POST /api/tasks/:id/assign` - Assign task to user (owner only)
- `PATCH /api/tasks/:id/status` - Update task status (owner or assignee)

**Query Parameters for GET /api/tasks**:
- `status` - Filter by status (TODO, IN_PROGRESS, DONE)
- `priority` - Filter by priority (LOW, MEDIUM, HIGH)
- `assignedToMe` - Show only tasks assigned to current user
- `search` - Search in title and description
- `sortBy` - Sort by (dueDate, priority, createdAt)
- `order` - Order (asc, desc)

**Authorization Rules**:
- Users can only see tasks they own or are assigned to
- Users can only create tasks
- Owners can update, delete, and assign tasks
- Assignees can update task status only
- Admins can see and modify all tasks

---

### 3. Comments (Optional Enhancement)

**Models**:

**Endpoints**:
- `POST /api/tasks/:id/comments` - Add comment (protected)
- `GET /api/tasks/:id/comments` - Get task comments (protected)
- `DELETE /api/comments/:id` - Delete comment (author or admin)

---

### 4. Error Handling

All errors must return consistent JSON format:


**HTTP Status Codes**:
- 200 OK - Success
- 201 Created - Resource created
- 400 Bad Request - Validation error
- 401 Unauthorized - Not authenticated
- 403 Forbidden - Not authorized
- 404 Not Found - Resource doesn't exist
- 409 Conflict - Duplicate resource
- 500 Internal Server Error - Unexpected error

---

### 5. Validation

**Task Validation**:
- Title: required, 1-200 characters
- Description: optional, max 1000 characters
- Status: must be TODO, IN_PROGRESS, or DONE
- Priority: must be LOW, MEDIUM, or HIGH
- DueDate: optional, must be valid ISO 8601, can't be in the past
- AssignedToId: optional, must be existing user

**User Validation**:
- Email: valid email format, unique
- Username: 3-20 chars, alphanumeric + underscore, unique
- Password: min 8 chars, uppercase, lowercase, number, special char
- FullName: 2-100 characters

---

### 6. Testing Requirements

**Unit Tests** (minimum 70% coverage):
- UserService tests with mock repository
- AuthService tests for login/register
- TaskService tests for CRUD and authorization
- Validator tests for all validation rules

**Integration Tests**:
- Auth endpoints (register, login)
- Task CRUD endpoints
- Authorization tests (owner, assignee, non-member)
- Query parameter filtering
- Error cases (validation, not found, forbidden)

---

### 7. Architecture Requirements

**Clean Architecture**:

**Dependency Injection**:
- Use Koin for all dependency management
- Separate modules for repositories, services, database
- Easy to swap implementations for testing

---



```kotlin
┌─────────────────────────────────────┐
│  Routes (HTTP Layer)                │
│  - Parse requests                   │
│  - Call services                    │
│  - Return responses                 │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│  Services (Business Logic)          │
│  - Validation                       │
│  - Authorization                    │
│  - Orchestration                    │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│  Repositories (Data Access)         │
│  - Database queries                 │
│  - Data mapping                     │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│  Database (Exposed + H2)            │
└─────────────────────────────────────┘
```
