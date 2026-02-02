---
type: "KEY_POINT"
title: "Lesson Summary and Best Practices"
---

You have mastered JSON serialization in Dart! Here is a summary of what you learned and when to use each approach:

**Three Approaches to JSON Serialization**

1. **Manual (dart:convert)**
   - Use for: Learning, small projects, scripts
   - Pros: No dependencies, full control
   - Cons: Repetitive, error-prone, no IDE support

2. **json_serializable**
   - Use for: Medium projects, when you need JSON but not immutability
   - Pros: Generated code, type-safe, @JsonKey customization
   - Cons: Requires build_runner, mutable by default

3. **freezed + json_serializable**
   - Use for: Production apps, state management, complex domains
   - Pros: Immutable, copyWith, equality, union types, pattern matching
   - Cons: More generated code, learning curve

**Recommended Approach**

For most Flutter projects, use **freezed** for all your models. The benefits outweigh the slightly longer setup:
- Immutability prevents bugs
- copyWith makes state updates clean
- Union types are perfect for API states
- Generated equality simplifies testing

**Essential Commands**

```bash
# Generate code once
flutter pub run build_runner build --delete-conflicting-outputs

# Watch for changes and regenerate
flutter pub run build_runner watch --delete-conflicting-outputs

# Clean generated files
flutter pub run build_runner clean
```

**Common Patterns Checklist**

- Use `@JsonKey(name: 'api_name')` when API uses different naming
- Use `@Default(value)` for optional fields with defaults
- Use `fieldRename: FieldRename.snake` for automatic snake_case conversion
- Use `explicitToJson: true` when you have nested objects
- Use `includeIfNull: false` to omit null values in requests
- Use union types for loading/success/error states
- Always test serialization roundtrips

**File Organization**

```
lib/
  models/
    user.dart          # User model
    user.freezed.dart  # Generated (gitignore optional)
    user.g.dart        # Generated (gitignore optional)
    address.dart
    api_response.dart
  services/
    api_client.dart    # Uses the models
```

**Next Steps**

You now have the skills to handle any JSON API response. In the next lessons, you will apply these serialization techniques to build complete data layers with repositories, caching, and error handling.

