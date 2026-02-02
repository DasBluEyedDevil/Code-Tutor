from pydantic import BaseModel, Field
from enum import Enum
from typing import Optional

class Category(Enum):
    ELECTRONICS = "electronics"
    CLOTHING = "clothing"
    FOOD = "food"

# TODO: Create Product model
# - name: str, 1-100 characters
# - price: float, must be > 0
# - category: Category enum
# - description: Optional[str], max 500 chars

# Test your model
try:
    product = Product(
        name="Laptop",
        price=999.99,
        category=Category.ELECTRONICS
    )
    print(f"Valid product: {product.name}")
except Exception as e:
    print(f"Error: {e}")