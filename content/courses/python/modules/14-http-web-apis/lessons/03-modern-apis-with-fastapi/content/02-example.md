---
type: "EXAMPLE"
title: "FastAPI Hello World"
---

FastAPI uses decorators like @app.get() and @app.post() to define routes. Type hints in parameters enable automatic validation, and Pydantic models handle request body parsing. Auto-generated docs are available at /docs.

```python
from fastapi import FastAPI
from pydantic import BaseModel

app = FastAPI()

class Item(BaseModel):
    name: str
    price: float

@app.get("/")
def read_root():
    return {"message": "Hello World"}

@app.get("/items/{item_id}")
def read_item(item_id: int, q: str = None):
    return {"item_id": item_id, "query": q}

@app.post("/items/")
def create_item(item: Item):
    return {"name": item.name, "price": item.price}

# Run with: uvicorn main:app --reload
# Docs at: http://localhost:8000/docs
```
