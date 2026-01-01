---
type: "THEORY"
title: "Working with Nested Data"
---

**Accessing nested values:**

```python
user = {
    "profile": {
        "name": "Alice",
        "address": {"city": "NYC", "zip": "10001"}
    }
}

# Chain keys to dig deeper
city = user["profile"]["address"]["city"]  # "NYC"

# Safe access with get() (avoids KeyError)
city = user.get("profile", {}).get("address", {}).get("city", "Unknown")
```

**Modifying nested values:**

```python
# Update existing nested value
user["profile"]["address"]["city"] = "LA"

# Add new nested key
user["profile"]["age"] = 25

# Add entirely new nested structure
user["settings"] = {"theme": "dark", "notifications": True}
```

**Looping through nested dictionaries:**

```python
students = {
    "alice": {"math": 95, "science": 88},
    "bob": {"math": 78, "science": 85}
}

for student, grades in students.items():
    print(f"{student}:")
    for subject, score in grades.items():
        print(f"  {subject}: {score}")
```

**Lists inside dictionaries:**

```python
recipes = {
    "pancakes": {
        "ingredients": ["flour", "eggs", "milk"],
        "time": 20
    }
}

for item in recipes["pancakes"]["ingredients"]:
    print(item)
```