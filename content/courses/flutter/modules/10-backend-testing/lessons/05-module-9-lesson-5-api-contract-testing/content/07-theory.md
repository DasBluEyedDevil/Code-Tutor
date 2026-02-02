---
type: "THEORY"
title: "Breaking Change Detection"
---


**Breaking changes** are modifications to an API that cause existing clients to fail. Detecting these before deployment is critical.

**Types of Breaking Changes:**

1. **Removal Changes**: Removing an endpoint or field
2. **Type Changes**: Changing field type (string to int)
3. **Semantic Changes**: Changing the meaning of a field
4. **Structural Changes**: Renaming or moving fields

**Non-Breaking Changes (Safe):**

- Adding new optional fields
- Adding new endpoints
- Adding new optional query parameters
- Relaxing validation (making required fields optional)

