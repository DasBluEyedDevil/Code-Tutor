---
type: "THEORY"
title: "Why FastAPI?"
---

**FastAPI: The Modern Python Web Framework**

FastAPI has rapidly become the go-to framework for building APIs in Python. Here's why:

**1. Modern and Fast**
- Built on Python 3.6+ type hints
- One of the fastest Python frameworks (on par with NodeJS and Go)
- Async support out of the box

**2. Automatic Documentation**
- OpenAPI (Swagger) docs generated automatically at `/docs`
- ReDoc alternative at `/redoc`
- No manual documentation maintenance

**3. Type-Based Validation**
```python
def get_item(item_id: int):  # Validates item_id is an integer
    ...
```
- Type hints = automatic request validation
- Pydantic models for complex data
- Clear error messages

**4. Developer Experience**
- Excellent IDE support (autocomplete, type checking)
- Intuitive API design
- Comprehensive error messages

**5. Industry Adoption**
- 78k+ GitHub stars (surpassed Flask)
- Used by Microsoft, Netflix, Uber
- Active community and ecosystem

**When to Use FastAPI:**
- New API projects
- Microservices
- Data-heavy applications
- When you want automatic docs
- When performance matters