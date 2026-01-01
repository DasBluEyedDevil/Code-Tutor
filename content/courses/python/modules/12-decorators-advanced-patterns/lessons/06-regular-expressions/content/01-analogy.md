---
type: "ANALOGY"
title: "The Concept: Pattern Matching"
---

**Regular Expressions (regex) = Advanced find/replace**

**Think of a search filter:**

❌ **Simple string matching:**
```python
if '@' in email and '.' in email:
    # Too simple!
    pass
```

✅ **Regex pattern:**
```python
import re
if re.match(r'^[\w.-]+@[\w.-]+\.\w+$', email):
    # Precise pattern!
    pass
```

**What regex can do:**

1. **Validate** ✓
   - Email addresses
   - Phone numbers
   - Passwords
   - URLs

2. **Extract** 🔍
   - Dates from text
   - Phone numbers from documents
   - URLs from HTML

3. **Replace** 🔄
   - Format phone numbers
   - Remove unwanted characters
   - Transform text patterns

4. **Split** ✂️
   - Complex delimiters
   - Multiple separators
   - Conditional splitting

**Common patterns:**
- `.` - Any character (except newline)
- `\d` - Digit (0-9)
- `\w` - Word character (a-z, A-Z, 0-9, _)
- `\s` - Whitespace (space, tab, newline)
- `*` - 0 or more
- `+` - 1 or more
- `?` - 0 or 1 (optional)
- `[]` - Character class
- `()` - Group
- `^` - Start of string
- `$` - End of string