---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Pydantic validates at runtime** - Type hints actually enforced
- **Auto-coercion** - "123" becomes 123.0 for float fields
- **v2 is much faster** - Rust core, 5-50x faster than v1
- **Use Field()** - For constraints like gt, min_length, pattern
- **@field_validator + @classmethod** - Custom validation (v2 syntax)
- **@computed_field** - Derived properties included in serialization
- **model_dump() / model_dump_json()** - Serialization methods
- **ConfigDict(frozen=True)** - Immutable models
- **Great for APIs** - Request/response validation, JSON handling