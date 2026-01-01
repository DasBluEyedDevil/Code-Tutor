from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from typing import List

app = FastAPI(title="My First API")

# In-memory storage
items = {}

class Item(BaseModel):
    name: str
    price: float

# TODO: Add these endpoints:
# GET / - return {"status": "ok"}
# GET /items - return list of all items
# GET /items/{item_id} - return single item or 404
# POST /items - create new item, return it with id

print("FastAPI app created!")
print("Run with: uvicorn main:app --reload")
print("Docs at: http://localhost:8000/docs")