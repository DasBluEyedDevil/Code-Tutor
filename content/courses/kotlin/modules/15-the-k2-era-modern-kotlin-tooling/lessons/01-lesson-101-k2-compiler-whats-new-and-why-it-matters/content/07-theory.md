---
type: "THEORY"
title: "Better Diagnostics"
---


### Clearer Error Messages

K2 provides more helpful error messages with actionable suggestions:

**Before (K1)**
```
Type mismatch: inferred type is Int but String was expected
```

**After (K2)**
```
Type mismatch: expected String, found Int.
Did you mean to call .toString()?
```

**More Examples of K2 Diagnostics**

```
// Unused variable warning with suggestion
Warning: Variable 'result' is never used.
Consider using '_' if the variable is intentionally unused.

// Deprecation with migration path
Warning: 'oldFunction()' is deprecated.
Use 'newFunction()' instead. Quick-fix available.

// Null safety with context
Error: Only safe (?.) or non-null asserted (!!.) calls are allowed
on a nullable receiver of type String?.
The value could be null because 'getUserInput()' returns String?.
```

---

