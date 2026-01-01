---
type: "THEORY"
title: "🔍 Handling JSON Errors"
---


### Catching Deserialization Errors


### Common Errors and Solutions

**1. Missing required field:**
**Solution**: Either make field nullable or provide default value

**2. Wrong type:**
**Solution**: Use correct JSON types or create custom serializer

**3. Unknown fields:**
**Solution**: Set `ignoreUnknownKeys = true` in JSON config

---



```json
// Extra field "publisher"
{"id": 1, "title": "1984", "publisher": "Penguin"}
```
