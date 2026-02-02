---
type: "THEORY"
title: "Grouping and Organizing Tests"
---


Well-organized tests are easier to maintain and debug. Use `group()` to create logical hierarchies.

**Basic Grouping:**

```dart
void main() {
  group('UserService', () {
    group('registration', () {
      test('creates new user', () { /* ... */ });
      test('validates email format', () { /* ... */ });
      test('hashes password', () { /* ... */ });
    });
    
    group('authentication', () {
      test('returns token for valid credentials', () { /* ... */ });
      test('throws for invalid password', () { /* ... */ });
      test('locks account after 5 failed attempts', () { /* ... */ });
    });
  });
}
```

**Output:**
```
UserService
  registration
    creates new user
    validates email format
    hashes password
  authentication
    returns token for valid credentials
    throws for invalid password
    locks account after 5 failed attempts
```

**Naming Conventions:**

```dart
// Pattern: [unit] [condition] [expected result]
test('divide throws ArgumentError when divisor is zero', () { });
test('findUser returns null when user does not exist', () { });
test('hashPassword produces different hash for same password with different salt', () { });
```

