---
type: "EXAMPLE"
title: "Mocking Dependencies with Mocktail"
---




```dart
// Mocking an API client
import 'package:mocktail/mocktail.dart';

class MockApiClient extends Mock implements ApiClient {}

void main() {
  late MockApiClient mockApi;
  late ProviderContainer container;

  setUp(() {
    mockApi = MockApiClient();
    container = ProviderContainer(
      overrides: [
        apiClientProvider.overrideWithValue(mockApi),
      ],
    );
  });

  test('fetches users successfully', () async {
    // Arrange
    when(() => mockApi.getUsers()).thenAnswer(
      (_) async => [User(id: '1', name: 'John')],
    );

    // Act
    final users = await container.read(usersProvider.future);

    // Assert
    expect(users, hasLength(1));
    expect(users.first.name, 'John');
    verify(() => mockApi.getUsers()).called(1);
  });

  test('handles API error', () async {
    when(() => mockApi.getUsers()).thenThrow(Exception('Network error'));

    expect(
      () => container.read(usersProvider.future),
      throwsException,
    );
  });
}
```
