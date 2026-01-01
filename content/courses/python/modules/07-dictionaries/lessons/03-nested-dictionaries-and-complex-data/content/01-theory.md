---
type: "THEORY"
title: "Understanding the Concept"
---

Real-world data is rarely flat. Think about a school's data:

- Each **student** has a name, age, and grades
- Each student has **multiple grades** (math, science, english)
- The school has **many students**

**Nested dictionaries** let you model this hierarchical structure:

```python
school = {
    "alice": {
        "age": 16,
        "grades": {"math": 95, "science": 88, "english": 92}
    },
    "bob": {
        "age": 17,
        "grades": {"math": 78, "science": 85, "english": 80}
    }
}

# Access nested data by chaining keys
print(school["alice"]["grades"]["math"])  # 95
```

**Common patterns for nested data:**

- **User profiles** - User -> personal info, preferences, history
- **Product catalogs** - Category -> product -> details, variants
- **API responses** - Usually deeply nested JSON data
- **Configuration files** - Sections -> settings -> values

Nested structures are powerful but require careful handling!