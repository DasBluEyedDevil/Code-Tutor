from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from typing import List, Dict

app = FastAPI(title="My First API")

# In-memory storage
items: Dict[int, dict] = {}
next_id = 1

class Item(BaseModel):
    name: str
    price: float

class ItemResponse(BaseModel):
    id: int
    name: str
    price: float

@app.get("/")
def root():
    return {"status": "ok"}

@app.get("/items", response_model=List[ItemResponse])
def list_items():
    return [ItemResponse(id=id, **item) for id, item in items.items()]

@app.get("/items/{item_id}", response_model=ItemResponse)
def get_item(item_id: int):
    if item_id not in items:
        raise HTTPException(status_code=404, detail="Item not found")
    return ItemResponse(id=item_id, **items[item_id])

@app.post("/items", response_model=ItemResponse, status_code=201)
def create_item(item: Item):
    global next_id
    items[next_id] = item.model_dump()
    response = ItemResponse(id=next_id, **items[next_id])
    next_id += 1
    return response

print("FastAPI app created!")
print("Endpoints: GET /, GET /items, GET /items/{id}, POST /items")