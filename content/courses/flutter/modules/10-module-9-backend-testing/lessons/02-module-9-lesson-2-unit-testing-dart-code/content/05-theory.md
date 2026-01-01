---
type: "THEORY"
title: "Mocking with Mocktail"
---


**Mocktail** is the modern mocking library for Dart. It allows you to create fake implementations of dependencies so you can test your code in isolation.

**Why Mock?**

Consider a `UserService` that depends on an `ApiClient`. When testing `UserService`, you do not want to make real HTTP requests. Mocking the `ApiClient` lets you:

1. Test without network access
2. Control exactly what the API returns
3. Test error handling by making the mock throw exceptions
4. Keep tests fast and deterministic

**Installation:**

```yaml
dev_dependencies:
  mocktail: ^1.0.0
```

**Basic Mocking Pattern:**

```dart
import 'package:mocktail/mocktail.dart';

// 1. Create a mock class
class MockApiClient extends Mock implements ApiClient {}

// 2. Use it in tests
void main() {
  late MockApiClient mockApi;
  late UserService userService;
  
  setUp(() {
    mockApi = MockApiClient();
    userService = UserService(mockApi);
  });
  
  test('fetches user from API', () async {
    // 3. Define what the mock should return
    when(() => mockApi.getUser('123'))
        .thenAnswer((_) async => User(id: '123', name: 'Alice'));
    
    // 4. Call the method under test
    final user = await userService.getUser('123');
    
    // 5. Verify the result
    expect(user.name, 'Alice');
    
    // 6. Verify the mock was called correctly
    verify(() => mockApi.getUser('123')).called(1);
  });
}
```

