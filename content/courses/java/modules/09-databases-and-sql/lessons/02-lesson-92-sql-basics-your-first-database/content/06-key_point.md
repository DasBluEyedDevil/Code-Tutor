---
type: "KEY_POINT"
title: "Primary Keys - The Unique Identifier"
---

Every table should have a PRIMARY KEY:

WHY?
- Uniquely identifies each row
- Prevents duplicate entries
- Allows referencing from other tables

GOOD PRIMARY KEYS:
✓ id INT AUTO_INCREMENT
✓ email (if truly unique)
✓ username (if truly unique)

BAD PRIMARY KEYS:
✗ name (not unique - many "John Smith"s)
✗ age (definitely not unique)

BEST PRACTICE:
Always use an auto-incrementing ID as primary key.
Simple, guaranteed unique, fast.