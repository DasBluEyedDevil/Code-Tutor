---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Getting Started:**
- Install: `uv add "fastapi[standard]"` or `pip install "fastapi[standard]"`
- Run: `uvicorn main:app --reload`
- Auto-docs at `/docs` (Swagger) and `/redoc`

**Core Concepts:**
- `@app.get()`, `@app.post()`, `@app.put()`, `@app.delete()` - HTTP method decorators
- **Path parameters:** `/items/{item_id}` - extracted from URL
- **Query parameters:** Function params not in path become query params
- **Request bodies:** Use Pydantic models for automatic validation

**Type Hints = Validation:**
```python
def endpoint(item_id: int):  # Validates as integer
    ...
```

**Pydantic for Complex Data:**
```python
class Item(BaseModel):
    name: str
    price: float = Field(..., gt=0)
```

**Key Advantages over Flask:**
- Automatic request validation
- Automatic API documentation
- Async by default
- Better IDE support
- Type-safe development