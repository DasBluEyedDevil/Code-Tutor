---
type: WARNING
---

**JSON type mismatches cause runtime crashes, not compile-time errors.** If the API returns a number where your model expects a String (or vice versa), the app will crash when users encounter that data -- not during development.

```dart
// API returns: {"age": "25"}  (string)
// Model expects: int age
class User {
  final int age;
  User.fromJson(Map<String, dynamic> json)
    : age = json['age']; // Runtime TypeError: String is not int
}
```

Always validate JSON structure before casting. Use `json_serializable` or `freezed` with code generation to produce type-safe `fromJson`/`toJson` methods that handle type coercion. When working with external APIs you do not control, add null checks and default values for every field since the API response shape may change without notice.
