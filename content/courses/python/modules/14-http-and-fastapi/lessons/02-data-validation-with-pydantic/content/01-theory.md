---
type: "THEORY"
title: "Why Data Validation Matters"
---

**The Problem: Garbage In, Garbage Out**

APIs receive data from users, and users make mistakes:
- Wrong data types (string instead of number)
- Missing required fields
- Invalid formats (bad email, wrong date)
- Malicious input (SQL injection, XSS)

**Pydantic solves this:**
- Automatic type validation
- Clear error messages
- Data conversion (str to int)
- Default values
- Complex nested structures

**Pydantic v2 (2023+):**
- 5-50x faster than v1
- Better error messages
- Stricter validation
- New syntax features