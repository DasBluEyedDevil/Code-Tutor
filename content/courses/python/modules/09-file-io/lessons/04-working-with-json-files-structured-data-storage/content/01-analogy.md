---
type: "ANALOGY"
title: "The Concept: Speaking the Internet's Language"
---

**JSON = JavaScript Object Notation** - The universal language for exchanging data between programs, websites, and APIs.

**Real-world analogy: International Shipping Labels**

Imagine you need to ship a package internationally. You can't write the address in just English or just Chinese - you need a universal format that every postal service understands.

**JSON is that universal format for data.**

Every programming language speaks JSON:
- Python ↔ JSON ↔ JavaScript
- Java ↔ JSON ↔ Ruby
- C++ ↔ JSON ↔ Go

**Why JSON is everywhere:**
1. **APIs** - 99% of web APIs send/receive JSON
2. **Configuration files** - package.json, settings.json, config.json
3. **Data exchange** - Save Python data, load in JavaScript
4. **Databases** - MongoDB, Postgres use JSON
5. **Human-readable** - You can read and edit it

**JSON looks like Python dictionaries:**

```python
# Python dictionary
person = {
    "name": "Alice",
    "age": 25,
    "hobbies": ["reading", "coding"],
    "active": True
}

# JSON (almost identical!)
{
    "name": "Alice",
    "age": 25,
    "hobbies": ["reading", "coding"],
    "active": true
}
```

**Key differences:**
- JSON uses `true`/`false`/`null` (lowercase)
- Python uses `True`/`False`/`None`
- JSON requires double quotes " (not single ')

**Two main operations:**

1. **Serialization (Python → JSON):**
   - Convert Python object to JSON string
   - `json.dumps()` - dump to string
   - `json.dump()` - dump to file

2. **Deserialization (JSON → Python):**
   - Convert JSON string to Python object  
   - `json.loads()` - load from string
   - `json.load()` - load from file

**Common use cases:**
- Save app settings to config.json
- Store user data between sessions
- Send data to/from web APIs
- Exchange data between programs
- Create data files for testing