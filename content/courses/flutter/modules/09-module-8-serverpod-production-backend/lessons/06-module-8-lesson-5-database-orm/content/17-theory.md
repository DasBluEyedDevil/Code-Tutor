---
type: "THEORY"
title: "Query Builders and Filtering"
---

Serverpod's query builder uses lambda functions to create type-safe filters. The `where` parameter accepts a function that receives a table reference and returns a filter expression.

**Basic Filter Operators:**

```dart
// Equality
t.field.equals(value)      // field = value
t.field.notEquals(value)   // field != value

// Comparison (numbers and dates)
t.field.greaterThan(value)        // field > value
t.field.greaterOrEqual(value)     // field >= value
t.field.lessThan(value)           // field < value
t.field.lessOrEqual(value)        // field <= value

// String matching
t.field.like('pattern')     // Case-sensitive LIKE
t.field.ilike('pattern')    // Case-insensitive LIKE
// Wildcards: % matches any characters, _ matches single character

// Null checks
t.field.equals(null)        // field IS NULL
t.field.notEquals(null)     // field IS NOT NULL

// Range checks
t.field.inSet({value1, value2, value3})  // field IN (values)
t.field.between(low, high)               // field BETWEEN low AND high
```

**Combining Conditions:**

```dart
// AND - both conditions must be true
t.isActive.equals(true) & t.isVerified.equals(true)

// OR - at least one condition must be true
t.role.equals('admin') | t.role.equals('moderator')

// Complex combinations with parentheses
(t.isActive.equals(true) & t.isVerified.equals(true)) |
(t.role.equals('admin'))
```

