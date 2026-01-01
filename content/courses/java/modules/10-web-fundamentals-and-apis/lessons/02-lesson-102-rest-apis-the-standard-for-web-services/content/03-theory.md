---
type: "THEORY"
title: "JSON - JavaScript Object Notation"
---

JSON is the standard data format for REST APIs:

SIMPLE OBJECT:
{
    "id": 1,
    "name": "Alice",
    "age": 20,
    "active": true
}

ARRAY:
["apple", "banana", "cherry"]

NESTED OBJECTS:
{
    "user": {
        "id": 1,
        "name": "Alice"
    },
    "orders": [
        { "id": 101, "total": 50.00 },
        { "id": 102, "total": 75.50 }
    ]
}

DATA TYPES:
- String: "hello" (double quotes only)
- Number: 42, 3.14
- Boolean: true, false
- Null: null
- Object: { }
- Array: [ ]