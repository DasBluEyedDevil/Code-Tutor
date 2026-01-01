---
type: "EXAMPLE"
title: "Code Example: JSON Operations"
---

Key functions:

**Writing (Serialization):**
- `json.dumps(obj)` - Convert Python object to JSON **string**
- `json.dump(obj, file)` - Write Python object to JSON **file**
- `indent=2` - Make JSON human-readable with indentation

**Reading (Deserialization):**
- `json.loads(string)` - Convert JSON **string** to Python object
- `json.load(file)` - Read JSON **file** to Python object

**Remember:** 
- dumps/loads = string operations (s for string)
- dump/load = file operations
- Always use 'with' statement for files
- JSON keys must be strings!

```python
import json

# Example 1: Python → JSON (Serialization)
print("=== Python to JSON (Serialization) ===")

# Python data
student = {
    "name": "Alice Johnson",
    "age": 20,
    "grades": [95, 87, 92, 88],
    "enrolled": True,
    "graduation_year": None
}

print("Python dictionary:")
print(student)
print(f"Type: {type(student)}\n")

# Convert to JSON string
json_string = json.dumps(student)
print("JSON string:")
print(json_string)
print(f"Type: {type(json_string)}\n")

# Pretty-printed JSON (readable)
json_pretty = json.dumps(student, indent=2)
print("Pretty JSON:")
print(json_pretty)
print("")

# Example 2: JSON → Python (Deserialization)
print("=== JSON to Python (Deserialization) ===")

json_data = '{"product": "Laptop", "price": 999.99, "in_stock": true}'
print("JSON string:")
print(json_data)
print(f"Type: {type(json_data)}\n")

# Convert to Python
product = json.loads(json_data)
print("Python dictionary:")
print(product)
print(f"Type: {type(product)}")
print(f"Accessing: product['price'] = ${product['price']}\n")

# Example 3: Writing JSON to file
print("=== Writing JSON to File ===")

config = {
    "app_name": "MyApp",
    "version": "1.0.0",
    "debug_mode": True,
    "database": {
        "host": "localhost",
        "port": 5432,
        "name": "mydb"
    },
    "features": ["auth", "payments", "analytics"]
}

with open("config.json", "w") as file:
    json.dump(config, file, indent=2)

print("✓ Wrote config.json\n")

# Example 4: Reading JSON from file
print("=== Reading JSON from File ===")

with open("config.json", "r") as file:
    loaded_config = json.load(file)

print("Loaded configuration:")
print(f"  App: {loaded_config['app_name']}")
print(f"  Version: {loaded_config['version']}")
print(f"  Database: {loaded_config['database']['host']}:{loaded_config['database']['port']}")
print(f"  Features: {', '.join(loaded_config['features'])}\n")

# Example 5: Type conversions
print("=== JSON ↔ Python Type Mapping ===")

data = {
    "string": "hello",
    "number_int": 42,
    "number_float": 3.14,
    "boolean": True,
    "null_value": None,
    "array": [1, 2, 3],
    "object": {"key": "value"}
}

json_str = json.dumps(data, indent=2)
print("Python → JSON:")
print(json_str)
print("")

back_to_python = json.loads(json_str)
print("JSON → Python:")
for key, value in back_to_python.items():
    print(f"  {key}: {value} (type: {type(value).__name__})")

print("")

# Example 6: Working with lists of objects
print("=== List of Objects ===")

users = [
    {"id": 1, "name": "Alice", "role": "admin"},
    {"id": 2, "name": "Bob", "role": "user"},
    {"id": 3, "name": "Carol", "role": "moderator"}
]

# Save to file
with open("users.json", "w") as file:
    json.dump(users, file, indent=2)

print("✓ Saved users.json")

# Load and process
with open("users.json", "r") as file:
    loaded_users = json.load(file)

print("\nUsers from file:")
for user in loaded_users:
    print(f"  - {user['name']} (ID: {user['id']}, Role: {user['role']})")

print("")

# Example 7: Error handling
print("=== Error Handling ===")

# Invalid JSON
invalid_json = '{"name": "Alice", age: 25}'  # Missing quotes on 'age'

try:
    json.loads(invalid_json)
except json.JSONDecodeError as e:
    print(f"❌ JSON Error: {e}")
    print("   (Keys must be in double quotes)\n")

# File not found
try:
    with open("missing.json", "r") as file:
        json.load(file)
except FileNotFoundError:
    print("❌ File not found: missing.json\n")

print("✓ All JSON examples completed!")
```
