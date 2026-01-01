---
type: "THEORY"
title: "When Each Error Type is Thrown"
---

Understanding when JavaScript throws each error type helps you anticipate and handle errors correctly:

**TypeError** - Thrown when:
- Calling a non-function: `(5)()`
- Accessing properties on null/undefined: `null.x`
- Using wrong type for built-in operations: `'hello' - 5`
- Calling methods that don't exist on a type: `(42).toUpperCase()`

**ReferenceError** - Thrown when:
- Using an undeclared variable: `console.log(xyz)`
- Accessing `let`/`const` before declaration (TDZ)
- Assigning to an undeclared variable in strict mode

**SyntaxError** - Thrown when:
- Invalid syntax in eval() or Function()
- Parsing JSON with JSON.parse() when JSON is malformed
- Note: Static syntax errors prevent code from running at all

**RangeError** - Thrown when:
- Array with invalid length: `new Array(-1)`
- Number methods with out-of-range arguments: `(1).toFixed(200)`
- Stack overflow from too much recursion
- Invalid date: `new Date('invalid').toISOString()`

**URIError** - Thrown when:
- decodeURI() or decodeURIComponent() with malformed sequences
- encodeURI() or encodeURIComponent() with invalid characters

**AggregateError** - Thrown when:
- Promise.any() rejects (all promises rejected)
- Multiple errors need to be grouped together

**Summary Table:**
```
| Error Type      | Common Cause                          |
|-----------------|---------------------------------------|
| TypeError       | Wrong type, null access, bad method  |
| ReferenceError  | Undefined variable                   |
| SyntaxError     | Bad JSON, eval() syntax              |
| RangeError      | Out of bounds, stack overflow        |
| URIError        | Malformed URI                        |
| AggregateError  | Multiple grouped errors              |
```