---
type: "ANALOGY"
title: "Final Capstone Project"
---

**Capstone = Bring it all together**

**Project: Task Management API with Authentication**

**Features to implement:**

**1. User Management** 👤
- Registration (POST /api/auth/register)
- Login (POST /api/auth/login)
- Logout (POST /api/auth/logout)
- Password hashing with bcrypt
- JWT tokens for authentication

**2. Task CRUD** ✅
- Create task (POST /api/tasks)
- List tasks (GET /api/tasks)
- Get task (GET /api/tasks/{id})
- Update task (PUT /api/tasks/{id})
- Delete task (DELETE /api/tasks/{id})
- Filter by status, priority

**3. Categories** 🏷️
- Create category (POST /api/categories)
- Assign tasks to categories
- List tasks by category

**4. Security** 🔒
- Authentication required for all endpoints
- Users can only access their own tasks
- Input validation
- Rate limiting

**5. Testing** 🧪
- Unit tests for all functions
- Integration tests for API endpoints
- 80%+ code coverage

**6. Documentation** 📚
- README with setup instructions
- API documentation
- Docstrings for all functions
- Environment setup guide

**7. Deployment** 🚀
- Deploy to Railway, Render, or Fly.io
- Production database (PostgreSQL)
- Environment variables configured
- HTTPS enabled

**Tech stack:**
- FastAPI (web framework)
- SQLAlchemy 2.0 + asyncio (database ORM)
- PostgreSQL (database - SQLite for development)
- python-jose (JWT authentication)
- pytest + httpx (testing)
- Railway or Render (deployment)
- uv (package management)

**Evaluation criteria:**
- ✅ All features working
- ✅ Tests passing
- ✅ Code quality (PEP 8, docstrings)
- ✅ Git history (clear commits)
- ✅ Documentation complete
- ✅ Successfully deployed
- ✅ Security best practices
- ✅ Using uv for package management
- ✅ pyproject.toml for project configuration