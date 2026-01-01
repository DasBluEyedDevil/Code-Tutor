---
type: "THEORY"
title: "Pydantic v2 - Data Validation Made Easy"
---

**What is Pydantic?**

Pydantic is the most popular data validation library in Python, used by FastAPI, LangChain, and thousands of production applications.

**Why use Pydantic?**
- **Type Safety**: Define your data structure once, get validation everywhere
- **Clear Error Messages**: Know exactly what went wrong and where
- **IDE Support**: Autocomplete and type checking work perfectly
- **JSON Serialization**: Easy conversion to/from JSON, dicts, and more
- **Settings Management**: Load config from environment variables

**Pydantic v2 Revolution (2023):**

Pydantic v2 was completely rewritten in Rust, making it:
- **5-50x faster** than v1
- **More memory efficient**
- **Stricter validation** by default

**Key v2 Syntax Changes:**

| v1 (Old) | v2 (New) |
|----------|----------|
| `@validator` | `@field_validator` |
| `@root_validator` | `@model_validator` |
| `.dict()` | `.model_dump()` |
| `.parse_obj()` | `.model_validate()` |
| `Config` class | `model_config` dict |
| `schema()` | `model_json_schema()` |

**When to Use Pydantic:**
- API request/response validation
- Configuration management
- Data parsing and cleaning
- Any structured data handling