---
type: "ANALOGY"
title: "Routes as Restaurant Menu Items"
---

**Understanding CRUD Routes Through a Restaurant Menu**

Think of your API as a restaurant menu system:

**The Menu (Your API):**

| Route | HTTP Method | Menu Action |
|-------|-------------|-------------|
| `GET /dishes` | GET | View the menu |
| `GET /dishes/42` | GET | See dish details |
| `POST /dishes` | POST | Chef creates new dish |
| `PUT /dishes/42` | PUT | Replace entire recipe |
| `PATCH /dishes/42` | PATCH | Adjust one ingredient |
| `DELETE /dishes/42` | DELETE | Remove from menu |

**In Code:**

```python
# Viewing the menu
@app.get("/dishes")
def list_dishes():
    return menu.get_all()

# Ordering a specific dish
@app.get("/dishes/{dish_id}")
def get_dish(dish_id: int):
    return menu.get(dish_id)

# Chef creates a new dish
@app.post("/dishes")
def create_dish(dish: Dish):
    return menu.add(dish)

# Replace the entire recipe
@app.put("/dishes/{dish_id}")
def replace_dish(dish_id: int, dish: Dish):
    return menu.replace(dish_id, dish)

# Just change the price
@app.patch("/dishes/{dish_id}")
def update_dish(dish_id: int, price: float):
    return menu.update_price(dish_id, price)

# Remove from menu
@app.delete("/dishes/{dish_id}")
def delete_dish(dish_id: int):
    return menu.remove(dish_id)
```

**The Key Insight:**

- **GET** = "Let me see..." (reading)
- **POST** = "Please create..." (new item)
- **PUT** = "Replace this with..." (full update)
- **PATCH** = "Just change this part..." (partial update)
- **DELETE** = "Remove this..." (deletion)

Each HTTP method has a purpose. Use the right tool for the job.
