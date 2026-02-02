---
type: "THEORY"
title: "Understanding bcrypt Hash Format"
---

A bcrypt hash looks like this:
```
$2b$12$LQv3c1yqBWVHxkd0LHAkCeOXDfG5v.Bn8N2tNLXq1SfYzXnGzpN2e
```

**Breaking it down:**
```
$2b$     - Algorithm identifier (bcrypt, version 2b)
12$      - Cost factor (2^12 = 4,096 iterations)
LQv3...  - 22 characters of salt (16 bytes, base64)
OXDf...  - 31 characters of hash (24 bytes, base64)
```

**Cost Factor Guidelines:**
| Cost | Iterations | Time (approx) | Use Case |
|------|------------|---------------|----------|
| 10   | 1,024      | ~100ms        | Development |
| 12   | 4,096      | ~300ms        | Standard apps |
| 14   | 16,384     | ~1s           | High security |

**Rule of thumb:** Target 250-500ms on your production hardware. Increase cost as hardware gets faster.