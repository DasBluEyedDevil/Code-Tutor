---
type: "KEY_POINT"
title: "When to Use Modern Switch Features"
---

USE SWITCH EXPRESSIONS WHEN:
- Assigning a value based on multiple conditions
- Mapping one value to another
- Replacing if-else-if chains

USE PATTERN MATCHING instanceof WHEN:
- You need to check type AND use the value
- Processing heterogeneous collections
- Simplifying type-checking code

USE PATTERN MATCHING IN SWITCH WHEN:
- Processing different types differently
- Combining type checks with value conditions
- Working with sealed classes/interfaces

BEST PRACTICES:
- Always handle all cases (be exhaustive)
- Use guards (when) for additional conditions
- Handle null explicitly when needed
- Prefer switch expressions over statements for assignments