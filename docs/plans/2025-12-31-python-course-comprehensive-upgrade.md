# Python Backend Development Course - Comprehensive Upgrade Plan

**Date:** 2025-12-31
**Status:** ✅ COMPLETED

## Implementation Summary

| Metric | Value |
|--------|-------|
| Total Modules | 21 |
| Total Lessons | 160+ |
| New Modules Added | 3 (Module 19, 20, 21) |
| Commits | 40+ |
| Through-line Project | Personal Finance Tracker (363 references) |
**Estimated Scope:** 18 modules upgrade + new content additions

---

## Executive Summary

This plan outlines a comprehensive upgrade of the Python Backend Development course to align with the 2025 ecosystem, fill identified gaps, and add capstone projects that guide learners from absolute beginner to job-ready backend developer.

### Design Decisions

| Aspect | Decision |
|--------|----------|
| **Python Version** | 3.13 baseline, 3.14 features highlighted |
| **Primary Framework** | FastAPI (with Django introduction) |
| **Database Path** | SQLite → PostgreSQL progression |
| **Deployment** | Docker + PaaS options (Railway, Render, Fly.io, Vercel) |
| **Capstone Structure** | Incremental project + final capstone |
| **Through-line Project** | Personal Finance Tracker |

---

## Part 1: Ecosystem Updates (2025 State)

### 1.1 Python Versions

| Version | Status | Key Features |
|---------|--------|--------------|
| **Python 3.14** | Current stable (3.14.2, Dec 2025) | Free-threaded (no GIL), t-strings (PEP 750), deferred annotations, multiple interpreters, zstd compression |
| **Python 3.13** | Maintained (3.13.11) | New colorized REPL, experimental free-threaded mode, experimental JIT, dbm.sqlite3 |
| **Python 3.15** | Alpha (Oct 2026 release) | In development |

**Course Action:** Baseline content on 3.13, add "Python 3.14+" callout boxes for:
- Template strings (t-strings) in string formatting lessons
- Free-threaded mode in async/concurrency lessons
- `compression.zstd` in file I/O lessons

### 1.2 Framework Versions

| Framework | Latest Version | Key Updates for Course |
|-----------|----------------|----------------------|
| **FastAPI** | 0.115+ | Python 3.9+ required, `pip install "fastapi[standard]"`, surpassed Flask in popularity (78.9k stars) |
| **Django** | 5.2 LTS (Apr 2025) | Async auth methods (`acreate_user`, `aauthenticate`), composite primary keys, Python 3.10-3.14 support |
| **Pydantic** | v2.11+ | `@field_validator` replaces `@validator`, `@model_validator` replaces `@root_validator`, Rust regex engine |
| **SQLAlchemy** | 2.0+ | Native async support, `create_async_engine`, improved type hints |

### 1.3 Tooling Versions

| Tool | Latest | Course Integration |
|------|--------|-------------------|
| **uv** | Latest 2025 | Replace pip/pipenv/poetry content, 10-100x faster, unified workflow |
| **ruff** | Dec 2025 | Replace flake8/black/isort, 10-100x faster, single tool |
| **pytest** | 8.x | pytest-asyncio 1.3.0 for async testing |
| **httpx** | Latest | Replace requests for async HTTP, HTTP/2 support |
| **Typer** | 0.16+ | CLI tools module (already in course) |
| **Alembic** | 1.17+ | Async migrations with `-t async` template |

### 1.4 Deployment Platforms

| Platform | Best For | Pricing |
|----------|----------|---------|
| **Render** | Early-stage, managed DBs, low ops | Free tier available (spins down on inactivity) |
| **Railway** | Engineering teams, precise pricing | $0.10/GB egress, no free tier |
| **Fly.io** | Global low-latency, WebSockets | Predictable VM pricing starting ~$2/mo |
| **Vercel** | Serverless Python functions | Generous free tier for frontend + functions |

---

## Part 2: Gap Analysis vs Current Course

### 2.1 Already Well-Covered (No Major Changes Needed)

These modules are comprehensive and only need ecosystem version updates:

- Module 01: The Absolute Basics
- Module 02: Variables
- Module 03: Boolean Logic
- Module 04: Loops
- Module 05: Lists & Tuples
- Module 06: Functions (includes type hints)
- Module 07: Dictionaries
- Module 08: Exception Handling
- Module 11: Classes & Objects (OOP)
- Module 12: Decorators (generators, context managers, comprehensions, regex)
- Module 17: Exception Groups & Structured Concurrency

### 2.2 Modules Requiring Significant Updates

| Module | Current State | Required Updates |
|--------|--------------|------------------|
| **Module 09: File I/O** | Good coverage | Add `compression.zstd` (3.14), update pathlib examples |
| **Module 10: Modules & Packages** | Has uv basics | Expand uv workflow, add ruff, update to pyproject.toml standards |
| **Module 13: Asynchronous Python** | httpx covered | Add free-threaded mode notes (3.14), TaskGroup patterns |
| **Module 14: HTTP & Web APIs** | Flask + FastAPI + SQLite | Major restructure: FastAPI primary, add Django intro, PostgreSQL |
| **Module 15: Sharing Your Work** | Git, testing, deployment | Update deployment section for Railway/Render/Fly.io, Docker best practices |
| **Module 16: Professional CLI with Typer** | Good | Update to Typer 0.16+, add rich integration examples |
| **Module 18: Advanced pytest** | Good foundation | Add pytest-asyncio 1.3.0 patterns, update mocking examples |

### 2.3 New Modules Required

| New Module | Topic | Justification |
|------------|-------|---------------|
| **Module 19** | Django Fundamentals | Job market coverage, full-stack web apps |
| **Module 20** | PostgreSQL & Advanced Database Patterns | Production database skills |
| **Module 21** | Authentication & Security | JWT, OAuth2, password hashing, OWASP basics |
| **Module 22** | Capstone: Personal Finance Tracker | Incremental project across course |

---

## Part 3: Module-by-Module Implementation Specifications

### Phase 1: Foundation Updates (Modules 1-8)

#### Module 01: The Absolute Basics
**Changes:**
- [ ] Update Python version references to "3.13+" with 3.14 notes
- [ ] Add version check code example: `import sys; print(sys.version)`
- [ ] Update "Your First Python Playground" to mention uv installation option
- [ ] Estimated effort: 2 hours

#### Module 02-08: Core Fundamentals
**Changes:**
- [ ] Search and replace deprecated syntax patterns
- [ ] Update any `typing.Optional` to `X | None` (3.10+ syntax)
- [ ] Ensure all examples use f-strings consistently
- [ ] Add type hints to function examples where missing
- [ ] Estimated effort: 4 hours total

---

### Phase 2: Intermediate Updates (Modules 9-12)

#### Module 09: File I/O
**Changes:**
- [ ] Add lesson: "Compression with zstd (Python 3.14+)"
  - `import compression.zstd`
  - Compare to gzip/bz2
  - Real-world use case: log compression
- [ ] Update pathlib examples to latest patterns
- [ ] Add aiofiles example for async file I/O
- [ ] Estimated effort: 4 hours

#### Module 10: Modules & Packages
**Major Restructure Required:**

**Current content to update:**
- [ ] Replace pip-focused content with uv-first approach
- [ ] Update `requirements.txt` → `pyproject.toml` as primary
- [ ] Add uv commands: `uv init`, `uv add`, `uv run`, `uv sync`

**New lessons to add:**
- [ ] "Modern Python Tooling with uv" (replace/expand current uv lesson)
  ```python
  # Installation
  # curl -LsSf https://astral.sh/uv/install.sh | sh

  # Create new project
  uv init my-project
  cd my-project

  # Add dependencies
  uv add fastapi pydantic sqlalchemy

  # Run scripts
  uv run python app.py
  ```

- [ ] "Code Quality with Ruff"
  ```toml
  # pyproject.toml
  [tool.ruff]
  line-length = 88
  target-version = "py313"

  [tool.ruff.lint]
  select = ["E", "F", "I", "UP", "B", "SIM"]
  ```

- [ ] Estimated effort: 8 hours

#### Module 11: Classes & Objects
**Changes:**
- [ ] Add dataclasses integration with Pydantic v2
- [ ] Update `@dataclass` examples with `slots=True` for performance
- [ ] Add Protocol/ABC examples for interface-driven design
- [ ] Estimated effort: 3 hours

#### Module 12: Decorators & Advanced Patterns
**Changes:**
- [ ] Update type hints lesson to cover:
  - `list[str]` vs `List[str]` (3.9+ native)
  - `X | None` vs `Optional[X]` (3.10+)
  - `type` statement (3.12+)
  - TypedDict, Protocol patterns
- [ ] Add `@functools.cache` vs `@lru_cache` comparison
- [ ] Estimated effort: 4 hours

---

### Phase 3: Backend Core (Modules 13-15) - MAJOR WORK

#### Module 13: Asynchronous Python
**Updates Required:**

- [ ] Update async lesson intro to mention Python 3.14 free-threaded mode
  ```python
  # Python 3.14+ with free-threaded build
  # No GIL = true parallelism for CPU-bound async tasks
  # Note: Still experimental, use async for I/O-bound tasks
  ```

- [ ] Add httpx async client patterns
  ```python
  import httpx

  async with httpx.AsyncClient() as client:
      responses = await asyncio.gather(
          client.get("https://api.example.com/users"),
          client.get("https://api.example.com/posts"),
      )
  ```

- [ ] Expand TaskGroup examples with error handling
- [ ] Add `asyncio.timeout()` (3.11+) patterns
- [ ] Estimated effort: 6 hours

#### Module 14: HTTP & Web APIs - COMPLETE RESTRUCTURE

**Current state:** Flask primary, FastAPI secondary, SQLite, basic auth

**New structure:**

##### Section 1: HTTP Fundamentals (Keep, Update)
- [ ] Update requests examples to show httpx alternative
- [ ] Add async HTTP client patterns

##### Section 2: FastAPI Deep Dive (Expand Significantly)

**Lesson 14.1: FastAPI Fundamentals**
```python
from fastapi import FastAPI, HTTPException, Depends
from pydantic import BaseModel, Field
import uvicorn

app = FastAPI(
    title="Personal Finance API",
    version="1.0.0",
    description="Track your income and expenses"
)

class Transaction(BaseModel):
    amount: float = Field(..., gt=0, description="Transaction amount")
    category: str = Field(..., min_length=1, max_length=50)
    description: str | None = None

@app.post("/transactions/", response_model=Transaction)
async def create_transaction(transaction: Transaction):
    # Save to database (covered later)
    return transaction

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)
```

**Lesson 14.2: Pydantic v2 Deep Dive**
- [ ] BaseModel patterns
- [ ] Field validators (`@field_validator`)
- [ ] Model validators (`@model_validator`)
- [ ] Settings management with `pydantic-settings`
```python
from pydantic import BaseModel, field_validator
from pydantic_settings import BaseSettings

class Settings(BaseSettings):
    database_url: str
    secret_key: str
    debug: bool = False

    model_config = {"env_file": ".env"}

class TransactionCreate(BaseModel):
    amount: float
    category: str

    @field_validator("amount")
    @classmethod
    def amount_must_be_positive(cls, v: float) -> float:
        if v <= 0:
            raise ValueError("Amount must be positive")
        return round(v, 2)
```

**Lesson 14.3: Dependency Injection in FastAPI**
```python
from fastapi import Depends
from sqlalchemy.ext.asyncio import AsyncSession

async def get_db() -> AsyncGenerator[AsyncSession, None]:
    async with async_session() as session:
        yield session

async def get_current_user(
    token: str = Depends(oauth2_scheme),
    db: AsyncSession = Depends(get_db)
) -> User:
    # Validate token, get user
    ...

@app.get("/me")
async def read_users_me(current_user: User = Depends(get_current_user)):
    return current_user
```

**Lesson 14.4: FastAPI + Async SQLAlchemy 2.0**
```python
from sqlalchemy.ext.asyncio import create_async_engine, AsyncSession
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column

class Base(DeclarativeBase):
    pass

class Transaction(Base):
    __tablename__ = "transactions"

    id: Mapped[int] = mapped_column(primary_key=True)
    amount: Mapped[float]
    category: Mapped[str]
    user_id: Mapped[int] = mapped_column(ForeignKey("users.id"))

# Async engine
engine = create_async_engine(
    "postgresql+asyncpg://user:pass@localhost/finance",
    pool_size=10,
    max_overflow=20
)
```

**Lesson 14.5: Database Migrations with Alembic**
```bash
# Initialize async migrations
uv add alembic asyncpg
alembic init -t async migrations

# Generate migration
alembic revision --autogenerate -m "add transactions table"

# Apply migrations
alembic upgrade head
```

##### Section 3: SQLite → PostgreSQL Progression

**Lesson 14.6: SQLite for Development**
- [ ] Why SQLite for learning (zero setup)
- [ ] Limitations for production
- [ ] aiosqlite for async SQLite

**Lesson 14.7: PostgreSQL for Production**
- [ ] Installation options (local, Docker, Supabase/Neon)
- [ ] Connection with asyncpg
- [ ] Key differences from SQLite
- [ ] Indexes, constraints, transactions

##### Section 4: Authentication & Security (NEW)

**Lesson 14.8: Password Hashing**
```python
from passlib.context import CryptContext

pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

def hash_password(password: str) -> str:
    return pwd_context.hash(password)

def verify_password(plain: str, hashed: str) -> bool:
    return pwd_context.verify(plain, hashed)
```

**Lesson 14.9: JWT Authentication**
```python
import jwt  # PyJWT, NOT python-jose (deprecated)
from datetime import datetime, timedelta

SECRET_KEY = "your-secret-key"  # Use env var in production
ALGORITHM = "HS256"

def create_access_token(data: dict, expires_delta: timedelta = timedelta(hours=1)):
    to_encode = data.copy()
    expire = datetime.utcnow() + expires_delta
    to_encode.update({"exp": expire})
    return jwt.encode(to_encode, SECRET_KEY, algorithm=ALGORITHM)

def decode_token(token: str) -> dict:
    return jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])
```

**Lesson 14.10: OAuth2 with FastAPI**
```python
from fastapi.security import OAuth2PasswordBearer, OAuth2PasswordRequestForm

oauth2_scheme = OAuth2PasswordBearer(tokenUrl="token")

@app.post("/token")
async def login(form_data: OAuth2PasswordRequestForm = Depends()):
    user = await authenticate_user(form_data.username, form_data.password)
    if not user:
        raise HTTPException(status_code=401, detail="Invalid credentials")
    token = create_access_token(data={"sub": user.email})
    return {"access_token": token, "token_type": "bearer"}
```

##### Section 5: Django Introduction (NEW)

**Lesson 14.11: Why Django?**
- When to choose Django vs FastAPI
- Django's batteries-included philosophy
- Admin interface, ORM, templates

**Lesson 14.12: Django Project Setup**
```bash
uv add django
django-admin startproject finance_project
cd finance_project
python manage.py startapp tracker
```

**Lesson 14.13: Django Models & Admin**
```python
# tracker/models.py
from django.db import models
from django.contrib.auth.models import User

class Transaction(models.Model):
    user = models.ForeignKey(User, on_delete=models.CASCADE)
    amount = models.DecimalField(max_digits=10, decimal_places=2)
    category = models.CharField(max_length=50)
    created_at = models.DateTimeField(auto_now_add=True)

# tracker/admin.py
from django.contrib import admin
from .models import Transaction

@admin.register(Transaction)
class TransactionAdmin(admin.ModelAdmin):
    list_display = ["user", "amount", "category", "created_at"]
    list_filter = ["category", "created_at"]
```

**Lesson 14.14: Django REST Framework Basics**
```python
from rest_framework import serializers, viewsets
from .models import Transaction

class TransactionSerializer(serializers.ModelSerializer):
    class Meta:
        model = Transaction
        fields = ["id", "amount", "category", "created_at"]

class TransactionViewSet(viewsets.ModelViewSet):
    queryset = Transaction.objects.all()
    serializer_class = TransactionSerializer
```

**Lesson 14.15: Django Async Views (5.2+)**
```python
from django.http import JsonResponse
from asgiref.sync import sync_to_async

async def async_transactions_view(request):
    # Django 5.2+ async ORM methods
    transactions = await Transaction.objects.filter(
        user=request.user
    ).aall()  # Note the 'a' prefix for async
    return JsonResponse({"transactions": list(transactions)})
```

**Estimated effort for Module 14: 40 hours**

---

#### Module 15: Sharing Your Work - MAJOR UPDATE

##### Section 1: Project Structure (Update)
```
my-project/
├── pyproject.toml          # Project metadata + dependencies
├── uv.lock                  # Lockfile (replaces requirements.txt)
├── .env                     # Environment variables (git-ignored)
├── .env.example             # Template for .env
├── src/
│   └── my_project/
│       ├── __init__.py
│       ├── main.py
│       ├── config.py        # Settings with pydantic-settings
│       ├── models/
│       ├── routes/
│       └── services/
├── tests/
│   ├── conftest.py
│   └── test_*.py
├── migrations/              # Alembic migrations
├── Dockerfile
├── docker-compose.yml
└── .github/
    └── workflows/
        └── ci.yml
```

##### Section 2: Docker Best Practices (NEW/Expand)

**Lesson 15.5: Dockerfile for Python**
```dockerfile
# Multi-stage build for smaller images
FROM python:3.13-slim AS builder

# Install uv
COPY --from=ghcr.io/astral-sh/uv:latest /uv /usr/local/bin/uv

WORKDIR /app
COPY pyproject.toml uv.lock ./

# Install dependencies to virtual environment
RUN uv sync --frozen --no-dev

# Final stage - minimal runtime image
FROM python:3.13-slim

WORKDIR /app

# Copy virtual environment from builder
COPY --from=builder /app/.venv /app/.venv
COPY src/ ./src/

# Set PATH to use virtual environment
ENV PATH="/app/.venv/bin:$PATH"

# Run as non-root user
RUN useradd -m appuser && chown -R appuser:appuser /app
USER appuser

EXPOSE 8000
CMD ["uvicorn", "src.main:app", "--host", "0.0.0.0", "--port", "8000"]
```

**Lesson 15.6: Docker Compose for Development**
```yaml
# docker-compose.yml
services:
  app:
    build: .
    ports:
      - "8000:8000"
    environment:
      - DATABASE_URL=postgresql+asyncpg://user:pass@db:5432/finance
    depends_on:
      db:
        condition: service_healthy
    volumes:
      - ./src:/app/src  # Hot reload in dev

  db:
    image: postgres:16
    environment:
      POSTGRES_USER: user
      POSTGRES_PASSWORD: pass
      POSTGRES_DB: finance
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U user -d finance"]
      interval: 5s
      timeout: 5s
      retries: 5
    volumes:
      - postgres_data:/var/lib/postgresql/data

volumes:
  postgres_data:
```

##### Section 3: Deployment to PaaS Platforms (NEW)

**Lesson 15.7: Deploying to Render**
```yaml
# render.yaml
services:
  - type: web
    name: finance-api
    runtime: docker
    dockerfilePath: ./Dockerfile
    envVars:
      - key: DATABASE_URL
        fromDatabase:
          name: finance-db
          property: connectionString

databases:
  - name: finance-db
    plan: free
```

**Lesson 15.8: Deploying to Railway**
```bash
# Install Railway CLI
npm install -g @railway/cli

# Login and initialize
railway login
railway init

# Deploy
railway up

# Add PostgreSQL
railway add --database postgresql
```

**Lesson 15.9: Deploying to Fly.io**
```bash
# Install flyctl
curl -L https://fly.io/install.sh | sh

# Launch app
fly launch

# Create PostgreSQL
fly postgres create

# Deploy
fly deploy
```

##### Section 4: CI/CD with GitHub Actions (Update)

**Lesson 15.10: Modern CI Pipeline**
```yaml
# .github/workflows/ci.yml
name: CI

on:
  push:
    branches: [main]
  pull_request:
    branches: [main]

jobs:
  test:
    runs-on: ubuntu-latest

    services:
      postgres:
        image: postgres:16
        env:
          POSTGRES_USER: test
          POSTGRES_PASSWORD: test
          POSTGRES_DB: test
        options: >-
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5
        ports:
          - 5432:5432

    steps:
      - uses: actions/checkout@v4

      - name: Install uv
        uses: astral-sh/setup-uv@v4
        with:
          version: "latest"

      - name: Set up Python
        run: uv python install 3.13

      - name: Install dependencies
        run: uv sync --all-extras --dev

      - name: Lint with ruff
        run: uv run ruff check .

      - name: Type check with mypy
        run: uv run mypy src/

      - name: Run tests
        run: uv run pytest --cov=src tests/
        env:
          DATABASE_URL: postgresql+asyncpg://test:test@localhost:5432/test
```

**Estimated effort for Module 15: 20 hours**

---

### Phase 4: Advanced & Testing (Modules 16-18)

#### Module 16: Professional CLI with Typer
**Changes:**
- [ ] Update to Typer 0.16+ patterns
- [ ] Add rich integration examples
- [ ] Update typer-cli references (now integrated into typer)
- [ ] Estimated effort: 3 hours

#### Module 17: Exception Groups & Structured Concurrency
**Changes:**
- [ ] Already Python 3.11+ focused - minor updates only
- [ ] Add more practical TaskGroup examples
- [ ] Estimated effort: 2 hours

#### Module 18: Advanced pytest & Test Architecture
**Changes:**
- [ ] Update to pytest-asyncio 1.3.0
- [ ] Add `@pytest.mark.asyncio` mode configuration
- [ ] Add TestClient/AsyncClient examples for FastAPI
- [ ] Add database fixture patterns with async sessions
```python
# conftest.py
import pytest
from httpx import AsyncClient
from sqlalchemy.ext.asyncio import create_async_engine, AsyncSession

@pytest.fixture
async def async_client(app):
    async with AsyncClient(app=app, base_url="http://test") as client:
        yield client

@pytest.fixture
async def db_session():
    engine = create_async_engine("sqlite+aiosqlite:///:memory:")
    async with engine.begin() as conn:
        await conn.run_sync(Base.metadata.create_all)

    async with AsyncSession(engine) as session:
        yield session
```
- [ ] Estimated effort: 6 hours

---

### Phase 5: New Modules

#### Module 19: Django Fundamentals (NEW)
**Lessons:**
1. Django Philosophy & When to Use It
2. Project Structure & Apps
3. Models & the Django ORM
4. Django Admin Deep Dive
5. Views & URL Routing
6. Templates & Static Files
7. Forms & Validation
8. Django REST Framework Introduction
9. Async Django (5.2+)
10. Mini-Project: Finance Tracker Web Interface

**Estimated effort: 30 hours**

#### Module 20: PostgreSQL & Advanced Database Patterns (NEW)
**Lessons:**
1. PostgreSQL Setup & Basics
2. SQL Review for Python Developers
3. Indexes, Constraints & Performance
4. Transactions & Isolation Levels
5. JSON/JSONB Columns
6. Full-Text Search in PostgreSQL
7. Connection Pooling & Performance
8. Database Design Patterns
9. Backup, Restore & Migrations
10. Cloud Databases: Supabase, Neon, Railway Postgres

**Estimated effort: 25 hours**

#### Module 21: Authentication & Security (NEW)
**Lessons:**
1. Security Fundamentals for Backend Developers
2. Password Hashing Best Practices
3. JWT Deep Dive: Access & Refresh Tokens
4. OAuth2 Flows Explained
5. Session-Based vs Token-Based Auth
6. CORS, CSRF, and XSS Prevention
7. Input Validation & SQL Injection Prevention
8. Rate Limiting & DDoS Protection
9. Secrets Management & Environment Variables
10. Security Audit Checklist

**Estimated effort: 25 hours**

---

## Part 4: Through-Line Project - Personal Finance Tracker

The Personal Finance Tracker project is built incrementally across the course, with features added as concepts are taught.

### Project Phases

| Course Stage | Features Added | Concepts Taught |
|--------------|----------------|-----------------|
| **Module 01-08** | CLI expense tracker, store in variables | Basics, data types, control flow |
| **Module 09** | Save/load from JSON file | File I/O |
| **Module 10** | Organize into modules | Package structure |
| **Module 11** | Transaction, Category, Budget classes | OOP |
| **Module 13** | Async data fetching | Async/await |
| **Module 14** | REST API with FastAPI | Web development |
| **Module 14** | PostgreSQL storage | Database |
| **Module 14** | User authentication | Security |
| **Module 15** | Deploy to Render | Deployment |
| **Module 18** | Full test suite | Testing |
| **Module 19** | Django web interface | Full-stack |

### Final Feature Set

```
Personal Finance Tracker API
├── Authentication
│   ├── Register/Login/Logout
│   ├── JWT tokens
│   └── Password reset
├── Accounts
│   ├── Multiple accounts (checking, savings, credit)
│   └── Account balances
├── Transactions
│   ├── CRUD operations
│   ├── Categories (income, expense, transfer)
│   ├── Recurring transactions
│   └── Attachments (receipts)
├── Budgets
│   ├── Monthly budgets by category
│   ├── Budget alerts
│   └── Spending insights
├── Reports
│   ├── Monthly summary
│   ├── Category breakdown
│   ├── Trends over time
│   └── Export to CSV
└── Settings
    ├── Currency preferences
    ├── Notification settings
    └── Data export/import
```

---

## Part 5: Final Capstone Project

After completing all modules, learners design and build their own project with guidance.

### Capstone Options

Learners choose one of these or propose their own:

1. **Blog/Content API** - Posts, comments, tags, search, markdown rendering
2. **E-commerce API** - Products, cart, orders, mock payments
3. **Task Management API** - Projects, tasks, assignments, due dates, notifications
4. **Recipe/Cookbook API** - Recipes, ingredients, meal planning, nutritional info
5. **Fitness Tracker API** - Workouts, exercises, progress tracking, goals

### Capstone Requirements

All capstones must include:

- [ ] FastAPI or Django backend
- [ ] PostgreSQL database with migrations
- [ ] User authentication (JWT)
- [ ] At least 5 CRUD endpoints
- [ ] Input validation with Pydantic
- [ ] Error handling with meaningful responses
- [ ] Async operations where appropriate
- [ ] Comprehensive tests (>80% coverage)
- [ ] Docker containerization
- [ ] CI/CD pipeline
- [ ] Deployed to a PaaS platform
- [ ] API documentation (OpenAPI/Swagger)
- [ ] README with setup instructions

---

## Part 6: Implementation Timeline

### Priority Order

1. **Critical Path (Do First)**
   - Module 14 restructure (FastAPI primary, add Django)
   - Module 10 uv/ruff updates
   - Module 15 deployment updates

2. **High Priority**
   - Module 21 (Security) - new
   - Module 20 (PostgreSQL) - new
   - Module 13 async updates

3. **Medium Priority**
   - Module 19 (Django) - new
   - Module 18 pytest updates
   - Module 09-12 ecosystem updates

4. **Lower Priority**
   - Module 01-08 minor version updates
   - Module 16-17 minor updates

### Estimated Total Effort

| Category | Hours |
|----------|-------|
| Ecosystem updates (Modules 1-12) | 25 |
| Module 14 restructure | 40 |
| Module 15 updates | 20 |
| Module 16-18 updates | 11 |
| New Module 19 (Django) | 30 |
| New Module 20 (PostgreSQL) | 25 |
| New Module 21 (Security) | 25 |
| Through-line project integration | 20 |
| Final capstone documentation | 10 |
| **Total** | **~206 hours** |

---

## Part 7: Quality Standards

### Content Requirements (Per the Brief)

Every lesson must:
- [ ] Completely convey the topic to absolute understanding
- [ ] Be thorough with no shortcuts, stubs, TODOs, or placeholders
- [ ] Include complete, runnable code examples
- [ ] Explain every concept before using it
- [ ] Build logically from previous lessons
- [ ] Include practical exercises with solutions
- [ ] List common mistakes and how to avoid them

### Code Example Standards

All code must:
- [ ] Be complete and runnable (no `...` or `# TODO`)
- [ ] Include all necessary imports
- [ ] Follow PEP 8 + ruff defaults
- [ ] Include type hints (Python 3.13+ syntax)
- [ ] Have docstrings for public functions/classes
- [ ] Show expected output in comments where applicable

### Review Checklist

Before marking any lesson complete:
- [ ] Code runs without errors
- [ ] All concepts explained before first use
- [ ] No assumed knowledge beyond prior lessons
- [ ] Examples use consistent style
- [ ] Challenges have complete solutions
- [ ] Common mistakes section is populated

---

## Sources

### Python & Core
- [Python 3.14 Release Schedule (PEP 745)](https://peps.python.org/pep-0745/)
- [What's New in Python 3.14](https://docs.python.org/3/whatsnew/3.14.html)
- [Python 3.13 Release Notes](https://docs.python.org/3/whatsnew/3.13.html)
- [Python 3.14 T-Strings (PEP 750)](https://peps.python.org/pep-0750/)
- [Real Python T-Strings Tutorial](https://realpython.com/python-t-strings/)

### Frameworks
- [FastAPI Official Documentation](https://fastapi.tiangolo.com/)
- [FastAPI Best Practices](https://github.com/zhanymkanov/fastapi-best-practices)
- [Django 5.2 Release Notes](https://docs.djangoproject.com/en/5.2/releases/5.2/)
- [Pydantic v2 Migration Guide](https://docs.pydantic.dev/latest/migration/)

### Database
- [SQLAlchemy 2.0 Async Documentation](https://docs.sqlalchemy.org/en/20/orm/extensions/asyncio.html)
- [Alembic Documentation](https://alembic.sqlalchemy.org/)

### Tooling
- [uv Documentation](https://docs.astral.sh/uv/)
- [Ruff Documentation](https://docs.astral.sh/ruff/)
- [pytest-asyncio](https://github.com/pytest-dev/pytest-asyncio)
- [httpx Documentation](https://www.python-httpx.org/)

### Deployment
- [Docker Multi-Stage Builds](https://docs.docker.com/get-started/docker-concepts/building-images/multi-stage-builds/)
- [Railway vs Fly.io Comparison](https://docs.railway.com/maturity/compare-to-fly)
- [Python Hosting Comparison 2025](https://www.nandann.com/blog/python-hosting-options-comparison)

### Security
- [Auth0 JWT in Python](https://auth0.com/blog/how-to-handle-jwt-in-python/)
- [PyJWT GitHub](https://github.com/jpadilla/pyjwt)

---

## Approval

This plan is ready for user approval before implementation begins.

**Next Steps After Approval:**
1. Create git worktree for isolated development
2. Use `superpowers:writing-plans` to create detailed implementation plan per module
3. Begin with Module 14 restructure (critical path)
