---
type: "EXAMPLE"
title: "Inline Flag Modifiers"
---

ES2025 adds the ability to enable or disable flags within a pattern:

```javascript
// Enable case-insensitive for part of the pattern
const re1 = /hello(?i:world)/;
re1.test('helloWorld');  // true - 'world' is case-insensitive
re1.test('Helloworld');  // false - 'hello' is still case-sensitive

// Disable a flag for part of the pattern
const re2 = /(?-i:strict)(?i:flexible)/i;  // Global 'i' flag
re2.test('STRICTflexible');  // false - 'strict' must match exactly
re2.test('strictFLEXIBLE');  // true

// Multiple flags at once
const re3 = /(?ims:pattern)/;  // Enable i, m, s for this group

// Real-world: Case-insensitive keywords, case-sensitive values
const configLine = /(?i:setting)=(.+)/;
configLine.exec('SETTING=MyValue');  // ['SETTING=MyValue', 'MyValue']
```
