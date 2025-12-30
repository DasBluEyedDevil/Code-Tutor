# Python Course Content Quality Review Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Systematically review all 108 lessons in the Python course for accuracy, completeness, freshness, and pedagogical gaps.

**Architecture:** Process each lesson through an AI agent with web search capability. The agent verifies content against Python 3.12/3.13, modern tooling (uv, ruff, pytest), and current best practices.

**Tech Stack:** PowerShell scripts, AI agents with web search, JSON course files, Markdown review reports

---

## Course Overview

- **Course ID:** python
- **Total Modules:** 18
- **Total Lessons:** 108
- **Topics Covered:** Python Basics → Variables → Logic → Loops → Collections → Functions → OOP → Modules → File I/O → HTTP/APIs → CLI Tools → Async → Testing

## Review Criteria

For each lesson, the AI reviewer must:

1. **Accuracy** - Verify code works with Python 3.12/3.13
2. **Completeness** - Ensure all concepts needed for understanding are present
3. **Freshness** - Check against latest Python features and modern tooling
4. **Pedagogical Gaps** - Identify missing explanations or prerequisite knowledge

---

## Module 1: Getting Started (4 lessons)

### Tasks 1.1-1.4: Review module-01-lesson-01 through module-01-lesson-04

**Topics:** Installing Python, First Program, REPL, IDE Setup

**AI Review Focus:**
- Python 3.12/3.13 installation instructions
- uv as modern package manager option
- VS Code Python extension setup
- Type hints from the start?

---

## Module 2: Variables & Data Types (6 lessons)

### Tasks 2.1-2.6: Review module-02-lesson-01 through module-02-lesson-06

**Topics:** Variables, Numbers, Strings, Booleans, Type Conversion

**AI Review Focus:**
- Type hints with Python 3.12 syntax
- f-strings as standard
- `match` statement awareness
- Walrus operator (`:=`)

---

## Module 3: Logic & Conditions (6 lessons)

### Tasks 3.1-3.6: Review module-03-lesson-01 through module-03-lesson-06

**Topics:** If/Elif/Else, Comparison, Boolean Logic, Match/Case

**AI Review Focus:**
- Match statement (Python 3.10+)
- Pattern matching features
- Guard clauses
- Truthiness edge cases

---

## Module 4: Loops & Iteration (5 lessons)

### Tasks 4.1-4.5: Review module-04-lesson-01 through module-04-lesson-05

**Topics:** For Loops, While Loops, Range, Enumerate, Zip

**AI Review Focus:**
- `enumerate()` as preferred pattern
- `zip()` strict parameter
- Generator expressions
- Loop else clause

---

## Module 5: Collections (6 lessons)

### Tasks 5.1-5.6: Review module-05-lesson-01 through module-05-lesson-06

**Topics:** Lists, Tuples, Sets, Dictionaries, Comprehensions

**AI Review Focus:**
- Dictionary merge operators (`|`, `|=`)
- Set operations
- `dict.get()` vs `[]` access
- Comprehension readability limits

---

## Module 6: Functions (6 lessons)

### Tasks 6.1-6.6: Review module-06-lesson-01 through module-06-lesson-06

**Topics:** Defining Functions, Parameters, Return Values, Scope, Decorators

**AI Review Focus:**
- Type hints for parameters/returns
- `*args` and `**kwargs`
- Positional-only parameters (`/`)
- Keyword-only parameters (`*`)

---

## Module 7: Object-Oriented Programming (8 lessons)

### Tasks 7.1-7.8: Review module-07-lesson-01 through module-07-lesson-08

**Topics:** Classes, Objects, Methods, Inheritance, Encapsulation, Properties

**AI Review Focus:**
- Dataclasses as default recommendation
- `@property` decorator
- `__slots__` for memory
- Protocols vs ABCs

---

## Module 8: Modules & Packages (5 lessons)

### Tasks 8.1-8.5: Review module-08-lesson-01 through module-08-lesson-05

**Topics:** Imports, Creating Modules, Packages, Virtual Environments

**AI Review Focus:**
- uv for virtual environments
- pyproject.toml as standard
- `__init__.py` requirements
- Relative imports

---

## Module 9: File Handling (5 lessons)

### Tasks 9.1-9.5: Review module-09-lesson-01 through module-09-lesson-05

**Topics:** Reading Files, Writing Files, Context Managers, CSV, JSON

**AI Review Focus:**
- `pathlib` as preferred
- Context managers (`with`)
- `json` module patterns
- `tomllib` for TOML (Python 3.11+)

---

## Module 10: Error Handling (5 lessons)

### Tasks 10.1-10.5: Review module-10-lesson-01 through module-10-lesson-05

**Topics:** Try/Except, Exception Types, Custom Exceptions, Logging

**AI Review Focus:**
- Exception chaining (`from`)
- Exception groups (Python 3.11+)
- `logging` best practices
- Structured logging

---

## Module 11: HTTP & APIs (6 lessons)

### Tasks 11.1-11.6: Review module-11-lesson-01 through module-11-lesson-06

**Topics:** Requests Library, REST APIs, Authentication, Error Handling

**AI Review Focus:**
- `httpx` as async-capable alternative
- `requests` vs `urllib3`
- API key security
- Rate limiting patterns

---

## Module 12: Web Frameworks (6 lessons)

### Tasks 12.1-12.6: Review module-12-lesson-01 through module-12-lesson-06

**Topics:** Flask/FastAPI Basics, Routes, Templates, Forms

**AI Review Focus:**
- FastAPI as modern choice
- Pydantic v2 syntax
- Type-based validation
- OpenAPI generation

---

## Module 13: Database Access (5 lessons)

### Tasks 13.1-13.5: Review module-13-lesson-01 through module-13-lesson-05

**Topics:** SQLite, SQLAlchemy, Queries, Migrations

**AI Review Focus:**
- SQLAlchemy 2.0 syntax
- async SQLAlchemy
- Alembic migrations
- Type-safe queries

---

## Module 14: CLI Tools (4 lessons)

### Tasks 14.1-14.4: Review module-14-lesson-01 through module-14-lesson-04

**Topics:** argparse, Click, Rich, Building CLIs

**AI Review Focus:**
- Typer as modern alternative
- Rich for output
- Click vs argparse vs Typer
- Entry points in pyproject.toml

---

## Module 15: Async Python (6 lessons)

### Tasks 15.1-15.6: Review module-15-lesson-01 through module-15-lesson-06

**Topics:** asyncio, async/await, Concurrent Tasks, aiohttp

**AI Review Focus:**
- `asyncio.TaskGroup` (Python 3.11+)
- `asyncio.to_thread()`
- Exception groups with tasks
- Structured concurrency

---

## Module 16: Data Processing (6 lessons)

### Tasks 16.1-16.6: Review module-16-lesson-01 through module-16-lesson-06

**Topics:** CSV, JSON, Pandas, Data Transformation

**AI Review Focus:**
- Polars as Pandas alternative
- PyArrow integration
- Memory-efficient processing
- Type hints with Pandas

---

## Module 17: Testing (6 lessons)

### Tasks 17.1-17.6: Review module-17-lesson-01 through module-17-lesson-06

**Topics:** pytest, Fixtures, Mocking, Coverage

**AI Review Focus:**
- pytest as standard
- pytest-asyncio for async
- Hypothesis for property testing
- Coverage configuration

---

## Module 18: Advanced Testing (6 lessons)

### Tasks 18.1-18.6: Review module-18-lesson-01 through module-18-lesson-06

**Topics:** Integration Testing, Fixtures, Mocking, pytest-asyncio

**AI Review Focus:**
- pytest latest features
- Testcontainers for integration
- Async test patterns
- conftest.py organization

---

## Execution Steps

### Step 1: Generate all review prompts

```powershell
powershell -File scripts/batch-review-lessons.ps1 -Course python
```

### Step 2: Process each prompt with AI agent

For each `python-lesson-*-review-prompt.md`:
1. Load prompt
2. AI performs web searches for Python 3.12/3.13 documentation
3. Save result as `python-lesson-*-review-result.json`

### Step 3: Aggregate results

```powershell
powershell -File scripts/aggregate-reviews.ps1
```

### Step 4: Fix high-priority issues

Review `docs/audits/content-review-summary.md` and apply fixes.

---

## Success Criteria

- [ ] All 108 lessons reviewed
- [ ] All code examples verified against Python 3.12/3.13
- [ ] Modern tooling (uv, ruff, pytest) mentioned where appropriate
- [ ] All content sections > 50 characters
- [ ] All deprecated patterns flagged and updated
- [ ] All pedagogical gaps documented
