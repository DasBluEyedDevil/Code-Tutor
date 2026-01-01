---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Not Handling Missing JSON File**
```python
# WRONG - Crashes if file doesn't exist
def load_data():
    return json.loads(DATA_FILE.read_text())  # FileNotFoundError!

# CORRECT - Check if file exists first
def load_data():
    if DATA_FILE.exists():
        return json.loads(DATA_FILE.read_text())
    return []
```

**2. ID Conflicts After Deletion**
```python
# WRONG - Using list length as ID causes duplicates after delete
new_entry = {"id": len(transactions) + 1, ...}  # IDs repeat!

# CORRECT - Find max ID from existing entries
def next_id(transactions):
    if not transactions:
        return 1
    return max(t["id"] for t in transactions) + 1
```

**3. Forgetting to Save After Modifications**
```python
# WRONG - Changes lost when program exits
@app.command()
def delete(transaction_id: int):
    transactions = load_data()
    transactions.pop(idx)
    # Forgot save_data(transactions)!

# CORRECT - Always save after modifying
    save_data(transactions)  # Persist changes
```

**4. Not Inheriting from str in Enum**
```python
# WRONG - Typer can't serialize this properly
class Category(Enum):
    food = "food"

# CORRECT - Inherit from str for CLI compatibility
class Category(str, Enum):
    food = "food"
```

**5. Hardcoding File Paths**
```python
# WRONG - Doesn't work across users/systems
DATA_FILE = "/Users/alice/.finance.json"

# CORRECT - Use Path.home() for user directory
from pathlib import Path
DATA_FILE = Path.home() / ".finance.json"
```