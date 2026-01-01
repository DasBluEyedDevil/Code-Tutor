---
type: "THEORY"
title: "Introduction: Why Proper Serialization Matters"
---

When your Flutter app communicates with APIs, data travels as JSON (JavaScript Object Notation) - a text format that looks like this: `{"name": "John", "age": 30}`. But Dart is a strongly-typed language. You cannot just use raw JSON data directly in your app without converting it to Dart objects first.

**What is Serialization?**

Serialization is the process of converting data between formats:
- **Deserialization (fromJson)**: Converting JSON text into Dart objects
- **Serialization (toJson)**: Converting Dart objects back into JSON text

**Why This Matters for Your App**

Imagine you fetch user data from an API:

```json
{"id": 1, "name": "Alice", "email": "alice@example.com", "created_at": "2024-01-15T10:30:00Z"}
```

Without proper serialization, you would access this data like:

```dart
// BAD: No type safety, easy to make typos
final name = json['name'] as String;  // What if 'name' is missing?
final age = json['age'] as int;       // What if it's actually a String?
final email = json['emial'];          // Typo! No compile-time error
```

With proper serialization, you get:

```dart
// GOOD: Type-safe, autocomplete, compile-time checks
final user = User.fromJson(json);
print(user.name);   // Autocomplete works
print(user.email);  // Typo would cause compile error
```

**Benefits of Proper JSON Serialization**

1. **Type Safety**: The compiler catches errors before runtime
2. **IDE Support**: Autocomplete shows available fields
3. **Maintainability**: Changing field names updates everywhere
4. **Null Safety**: Handle missing or nullable fields properly
5. **Documentation**: Model classes document your API structure

**What You Will Learn**

In this lesson, you will master three approaches to JSON serialization, from manual to fully automated:

1. **Manual serialization** using dart:convert (understanding the basics)
2. **json_serializable** package (code generation for type safety)
3. **freezed** package (immutable models with extra features)

By the end, you will know which approach to use for different scenarios and how to handle complex real-world API responses.

