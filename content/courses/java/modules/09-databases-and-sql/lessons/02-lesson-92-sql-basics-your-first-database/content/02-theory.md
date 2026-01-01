---
type: "THEORY"
title: "Common Data Types in SQL"
---

TEXT TYPES:
- CHAR(n): Fixed-length text (CHAR(5) for "HELLO")
- VARCHAR(n): Variable-length text (VARCHAR(255) for emails)
- TEXT: Very long text (articles, comments)

NUMBER TYPES:
- INT: Whole numbers (-2147483648 to 2147483647)
- BIGINT: Bigger whole numbers
- DECIMAL(p, s): Exact decimals (DECIMAL(10, 2) for money: 12345678.90)
- FLOAT/DOUBLE: Approximate decimals (scientific calculations)

DATE/TIME TYPES:
- DATE: Just date (2025-01-15)
- TIME: Just time (14:30:00)
- DATETIME: Both (2025-01-15 14:30:00)
- TIMESTAMP: Date + time with timezone

BOOLEAN:
- BOOLEAN: true/false (stored as 0 or 1)