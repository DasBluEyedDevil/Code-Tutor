---
type: "EXAMPLE"
title: "Real-World Use Cases"
---

Common scenarios where RegExp.escape is essential:

```javascript
// 1. Search and highlight functionality
function highlightText(text, searchTerm) {
  const escaped = RegExp.escape(searchTerm);
  const regex = new RegExp(`(${escaped})`, 'gi');
  return text.replace(regex, '<mark>$1</mark>');
}

highlightText(
  'The price is $100 (plus tax).',
  '$100 (plus'
);
// 'The price is <mark>$100 (plus</mark> tax).'

// 2. Validating exact matches
function exactMatch(input, expected) {
  const pattern = new RegExp(`^${RegExp.escape(expected)}$`);
  return pattern.test(input);
}

exactMatch('hello.world', 'hello.world');  // true
exactMatch('helloXworld', 'hello.world');  // false

// 3. Building complex patterns with user input
function createPathMatcher(userPath) {
  // User might enter: './src/**/*.ts'
  const escaped = RegExp.escape(userPath);
  // Now safe to use in a larger pattern
  return new RegExp(`^${escaped}$`);
}

// 4. Search with word boundaries
function searchWord(text, word) {
  const escaped = RegExp.escape(word);
  const pattern = new RegExp(`\\b${escaped}\\b`, 'g');
  return [...text.matchAll(pattern)];
}

searchWord('C++ is not C# or C.', 'C++');
// Finds only 'C++', not 'C#' or 'C.'
```
