---
type: "THEORY"
title: "Complete Syntax Reference"
---

**Object Destructuring Syntax:**

```javascript
// Basic extraction
let { prop1, prop2 } = object;

// With rename
let { prop: newName } = object;

// With default
let { prop = defaultValue } = object;

// Rename AND default
let { prop: newName = defaultValue } = object;

// Nested extraction
let { outer: { inner } } = object;

// Rest pattern
let { extracted, ...rest } = object;
```

**Array Destructuring Syntax:**

```javascript
// Basic extraction
let [first, second, third] = array;

// Skip elements
let [first, , third] = array;

// With default
let [first = 'default'] = array;

// Rest pattern
let [first, ...rest] = array;

// Swap values
[a, b] = [b, a];
```

**Spread Operator Syntax:**

```javascript
// Copy object
let copy = { ...original };

// Merge objects (later wins)
let merged = { ...obj1, ...obj2 };

// Add/override properties
let updated = { ...obj, newProp: value };

// Copy array
let arrCopy = [...original];

// Combine arrays
let combined = [...arr1, ...arr2];

// Spread in function call
func(...argsArray);
```

**Key Differences:**

| Feature | Destructuring | Spread |
|---------|---------------|--------|
| Purpose | Extract values | Expand values |
| Position | Left side of = | Right side of = |
| Objects | `let { a } = obj` | `{ ...obj }` |
| Arrays | `let [a] = arr` | `[...arr]` |
| Result | Individual variables | New object/array |

**When to use which:**

- **Destructuring**: When you need to pull out specific values from a structure
- **Spread**: When you need to combine, copy, or expand values
- **Rest (...)**: When you need to collect remaining values after destructuring