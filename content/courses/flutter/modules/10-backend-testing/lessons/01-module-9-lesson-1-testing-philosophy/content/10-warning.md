---
type: "WARNING"
title: "Common Testing Mistakes"
---


**1. Testing Implementation Instead of Behavior**

```dart
// BAD: Tests internal structure
test('uses HashMap internally', () {
  final cache = Cache();
  expect(cache._storage, isA<HashMap>()); // Testing private implementation!
});

// GOOD: Tests observable behavior
test('returns cached value', () {
  final cache = Cache();
  cache.set('key', 'value');
  expect(cache.get('key'), 'value');
});
```

**2. Ignoring Edge Cases**

```dart
// BAD: Only tests happy path
test('divides numbers', () {
  expect(divide(10, 2), 5);
});

// GOOD: Tests edge cases too
test('throws on division by zero', () {
  expect(() => divide(10, 0), throwsA(isA<DivisionByZeroError>()));
});
```

**3. Tests That Always Pass**

```dart
// BAD: Test does not actually verify anything useful
test('user service exists', () {
  final service = UserService();
  expect(service, isNotNull); // This always passes!
});
```

**4. Flaky Tests (Non-Deterministic)**

```dart
// BAD: Depends on timing
test('completes within timeout', () async {
  await Future.delayed(Duration(milliseconds: 100));
  // May pass or fail depending on system load
});
```

**5. Over-Mocking**

```dart
// BAD: Mocking everything means you are not testing real behavior
test('registers user', () {
  final mockDb = MockDatabase();
  final mockEmail = MockEmailService();
  final mockLogger = MockLogger();
  final mockValidator = MockValidator();
  // At this point, what are you actually testing?
});
```

