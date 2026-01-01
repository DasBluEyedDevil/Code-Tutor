---
type: "THEORY"
title: "Testing Async Code"
---


Backend code is heavily asynchronous - database calls, HTTP requests, file operations all use `Future` or `Stream`. Testing async code requires special attention.

**Basic Async Test:**

```dart
test('fetches data asynchronously', () async {
  // Mark test function as async
  final result = await fetchData();
  expect(result, isNotNull);
});
```

**Testing Futures:**

```dart
test('completes with expected value', () async {
  final future = computeValue();
  
  // Wait for completion and check result
  expect(await future, equals(42));
  
  // Or use completion matcher
  expect(future, completion(equals(42)));
});

test('throws expected exception', () async {
  final future = fetchInvalidData();
  
  // Use throwsA for async exceptions
  expect(future, throwsA(isA<NotFoundException>()));
});
```

**Testing Streams:**

```dart
test('emits expected values', () async {
  final stream = countTo(3);
  
  // Check all emitted values
  expect(stream, emitsInOrder([1, 2, 3]));
});

test('stream emits error', () {
  final stream = failingStream();
  
  expect(stream, emitsError(isA<Exception>()));
});

test('stream completes', () {
  final stream = finiteStream();
  
  expect(stream, emitsDone);
});
```

**Timeout for Slow Operations:**

```dart
test('completes within timeout', () async {
  final result = await slowOperation();
  expect(result, isNotNull);
}, timeout: Timeout(Duration(seconds: 10)));
```

