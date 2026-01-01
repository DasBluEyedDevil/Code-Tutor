---
type: "EXAMPLE"
title: "Using RegExp.escape()"
---

The new standard method escapes all regex special characters:

```javascript
// Basic usage
const escaped = RegExp.escape('Hello. How are you?');
console.log(escaped);  // 'Hello\\. How are you\\?'

// Building a safe dynamic regex
const searchTerm = 'file.txt';
const pattern = new RegExp(RegExp.escape(searchTerm));
pattern.test('file.txt');    // true
pattern.test('fileXtxt');    // false (. is literal now)

// With flags
const caseInsensitive = new RegExp(RegExp.escape('Price: $50'), 'i');
caseInsensitive.test('PRICE: $50');  // true
caseInsensitive.test('price: $50');  // true

// Searching for literal regex patterns
const codePattern = 'function* generator()';
const regex = new RegExp(RegExp.escape(codePattern));
regex.test('function* generator()');  // true - * is escaped
```
