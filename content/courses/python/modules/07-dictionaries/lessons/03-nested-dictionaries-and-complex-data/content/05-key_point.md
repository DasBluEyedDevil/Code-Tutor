---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Nested dictionaries** store dictionaries inside dictionaries
- **Access nested values** by chaining keys: `data["outer"]["inner"]["deep"]`
- **Safe nested access**: `data.get("key", {}).get("nested", default)`
- **Modify nested values**: `data["outer"]["inner"] = new_value`
- **Loop through nested data** with nested for loops
- **Lists in dictionaries** are common: `data["items"]` can be a list
- **API responses** are typically deeply nested JSON structures
- **Pattern**: Check if keys exist before accessing deeply nested data
- **Keep it manageable** - If nesting gets too deep, consider restructuring