---
type: "THEORY"
title: "JSDoc Type Syntax"
---

**Primitive types:**
- `{string}` - text
- `{number}` - any number
- `{boolean}` - true/false
- `{null}` - null value
- `{undefined}` - undefined value

**Arrays:**
- `{string[]}` - array of strings
- `{Array<number>}` - array of numbers (alternative syntax)

**Objects:**
- `{{ name: string, age: number }}` - inline object type
- `{Object}` - any object (avoid, too vague)

**Optional parameters:**
- `{string} [name]` - optional string
- `{number} [count=0]` - optional with default

**Union types:**
- `{string|number}` - either string or number
- `{string|null}` - string or null