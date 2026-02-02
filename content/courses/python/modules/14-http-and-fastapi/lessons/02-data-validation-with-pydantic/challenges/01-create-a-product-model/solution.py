from pydantic import BaseModel, Field, ValidationError
from enum import Enum
from typing import Optional

class Category(Enum):
    ELECTRONICS = "electronics"
    CLOTHING = "clothing"
    FOOD = "food"

class Product(BaseModel):
    name: str = Field(min_length=1, max_length=100)
    price: float = Field(gt=0)
    category: Category
    description: Optional[str] = Field(default=None, max_length=500)

# Test valid product
product = Product(
    name="Laptop",
    price=999.99,
    category=Category.ELECTRONICS,
    description="Powerful laptop"
)
print(f"Valid: {product.model_dump()}")

# Test invalid product
try:
    bad = Product(name="", price=-10, category="invalid")
except ValidationError as e:
    print(f"Validation errors: {len(e.errors())} issues found")