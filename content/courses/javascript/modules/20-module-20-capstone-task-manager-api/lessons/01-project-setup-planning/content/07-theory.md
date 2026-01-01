---
type: "THEORY"
title: "API Endpoint Planning"
---

Before coding, let's plan our API endpoints:

**Authentication (Public)**
```
POST /api/auth/register  - Create new user
POST /api/auth/login     - Get JWT token
GET  /api/auth/me        - Get current user (protected)
```

**Tasks (Protected - Owner Only)**
```
GET    /api/tasks        - List user's tasks (with filters)
POST   /api/tasks        - Create new task
GET    /api/tasks/:id    - Get single task
PUT    /api/tasks/:id    - Update task
DELETE /api/tasks/:id    - Delete task
```

**Categories (Protected - Owner Only)**
```
GET    /api/categories       - List user's categories
POST   /api/categories       - Create category
PUT    /api/categories/:id   - Update category
DELETE /api/categories/:id   - Delete category
```

**Query Parameters for Tasks:**
- `?status=pending|completed` - Filter by status
- `?categoryId=123` - Filter by category
- `?page=1&limit=10` - Pagination
- `?search=keyword` - Search in title/description