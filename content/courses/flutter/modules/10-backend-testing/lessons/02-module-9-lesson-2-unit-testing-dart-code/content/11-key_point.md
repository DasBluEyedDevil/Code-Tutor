---
type: "KEY_POINT"
title: "Lesson Summary"
---


You now have the skills to write comprehensive unit tests for Dart backend code:

**dart test Package:**
- Install as dev dependency
- Test files end with `_test.dart`
- Run with `dart test` command

**Writing Tests:**
- Use `test()` function with descriptive names
- Use `expect()` with matchers for assertions
- Group related tests with `group()`

**Matchers:**
- Equality: `equals()`, `isNot()`, `same()`
- Types: `isA<T>()`, `isNull`, `isNotNull`
- Numbers: `greaterThan()`, `lessThan()`, `closeTo()`
- Collections: `contains()`, `hasLength()`, `isEmpty`

**Mocking with Mocktail:**
- Create mock classes extending `Mock`
- Use `when()` to define mock behavior
- Use `verify()` to check mock was called correctly

**Async Testing:**
- Mark tests as `async`
- Use `await` for Future operations
- Use `emitsInOrder()` for Stream testing

**Setup and Teardown:**
- `setUp()` runs before each test
- `tearDown()` runs after each test
- `setUpAll()` and `tearDownAll()` for one-time setup

In the next lesson, we will apply these skills to testing Dart Frog routes specifically.

