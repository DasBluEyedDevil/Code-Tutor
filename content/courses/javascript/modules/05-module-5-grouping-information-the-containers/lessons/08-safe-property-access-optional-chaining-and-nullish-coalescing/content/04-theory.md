---
type: "THEORY"
title: "Breaking Down the Syntax"
---

**Optional Chaining (?.)**

Three forms of optional chaining:

1. **Property access**: obj?.property
   - Returns undefined if obj is null/undefined
   - Otherwise returns obj.property

2. **Method calls**: obj.method?.(args)
   - Returns undefined if method doesn't exist
   - Otherwise calls the method

3. **Array access**: arr?.[index]
   - Returns undefined if arr is null/undefined
   - Otherwise returns arr[index]

**How it works:**
```
user?.address?.city
```
is equivalent to:
```
user != null ? (user.address != null ? user.address.city : undefined) : undefined
```

**Nullish Coalescing (??)**

Syntax: `leftValue ?? rightValue`

- Returns rightValue only if leftValue is null or undefined
- Otherwise returns leftValue (even if it's 0, '', or false)

**Comparison with ||:**

| Expression | || result | ?? result |
|------------|-----------|----------|
| 0 \|\| 'default' | 'default' | - |
| 0 ?? 'default' | - | 0 |
| '' \|\| 'default' | 'default' | - |
| '' ?? 'default' | - | '' |
| null \|\| 'default' | 'default' | - |
| null ?? 'default' | - | 'default' |
| undefined \|\| 'default' | 'default' | - |
| undefined ?? 'default' | - | 'default' |

**Combining both:**
```javascript
const value = obj?.deeply?.nested?.value ?? 'default';
```
This safely navigates the object and provides a default if the value is null/undefined.