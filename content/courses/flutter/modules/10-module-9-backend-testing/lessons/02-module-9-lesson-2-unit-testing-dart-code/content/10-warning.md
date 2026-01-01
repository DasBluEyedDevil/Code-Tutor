---
type: "WARNING"
title: "Common Async Testing Mistakes"
---


**1. Forgetting to await async operations:**

```dart
// BAD - Test passes immediately, async operation runs after test ends
test('saves user', () {
  repository.save(user); // Missing await!
  // Test ends before save completes
});

// GOOD - Wait for operation to complete
test('saves user', () async {
  await repository.save(user);
  final saved = await repository.findById(user.id);
  expect(saved, isNotNull);
});
```

**2. Not handling async errors properly:**

```dart
// BAD - Error might be swallowed
test('handles error', () async {
  try {
    await failingOperation();
  } catch (e) {
    // Test might pass even if wrong exception
  }
});

// GOOD - Use expect with throwsA
test('handles error', () async {
  expect(
    () => failingOperation(),
    throwsA(isA<SpecificException>()),
  );
});
```

**3. Relying on timing instead of completion:**

```dart
// BAD - Flaky, depends on system speed
test('updates after delay', () async {
  triggerUpdate();
  await Future.delayed(Duration(milliseconds: 100));
  expect(getValue(), 'updated');
});

// GOOD - Wait for the actual completion
test('updates after operation completes', () async {
  await triggerUpdateAndWait();
  expect(getValue(), 'updated');
});
```

