---
type: "THEORY"
title: "Assertions and Matchers"
---


**Assertions** are the heart of tests. They verify that your code produces the expected results. Dart's test package provides the `expect()` function and a rich set of **matchers**.

**Basic Syntax:**

```dart
expect(actual, matcher);
```

**Equality Matchers:**

```dart
expect(value, equals(5));           // Exact equality
expect(value, isNot(equals(5)));    // Not equal
expect(value, same(otherObject));   // Same instance (identity)
```

**Type Matchers:**

```dart
expect(value, isA<String>());       // Type check
expect(value, isNull);              // Is null
expect(value, isNotNull);           // Is not null
expect(value, isTrue);              // Boolean true
expect(value, isFalse);             // Boolean false
```

**Numeric Matchers:**

```dart
expect(value, greaterThan(5));      // > 5
expect(value, lessThan(10));        // < 10
expect(value, greaterThanOrEqualTo(5));
expect(value, lessThanOrEqualTo(10));
expect(value, inInclusiveRange(5, 10)); // 5 <= value <= 10
expect(value, closeTo(3.14, 0.01)); // For floating point
```

**String Matchers:**

```dart
expect(str, contains('hello'));     // Contains substring
expect(str, startsWith('Hello'));   // Starts with
expect(str, endsWith('world'));     // Ends with
expect(str, matches(RegExp(r'^\d+')));  // Regex match
expect(str, isEmpty);               // Empty string
expect(str, isNotEmpty);            // Non-empty string
```

**Collection Matchers:**

```dart
expect(list, hasLength(3));         // List length
expect(list, contains(5));          // Contains element
expect(list, containsAll([1, 2]));  // Contains all elements
expect(list, isEmpty);              // Empty collection
expect(list, isNotEmpty);           // Non-empty collection
expect(list, orderedEquals([1, 2, 3])); // Exact order match
expect(list, unorderedEquals([3, 1, 2])); // Any order match
```

