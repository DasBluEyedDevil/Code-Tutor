---
type: "KEY_POINT"
title: "Dependency Injection Best Practices"
---

Follow these best practices to get the most out of dependency injection with Riverpod:

### Best Practice 1: Create Providers for All External Dependencies

Anything that interacts with the outside world should be a provider:
- HTTP clients
- Database connections
- Firebase instances
- SharedPreferences
- Local storage
- Bluetooth/sensors
- Analytics services

```dart
@Riverpod(keepAlive: true)
SharedPreferences sharedPreferences(Ref ref) {
  throw UnimplementedError('Must be overridden in main()');
}

// In main.dart:
void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  final prefs = await SharedPreferences.getInstance();

  runApp(
    ProviderScope(
      overrides: [
        sharedPreferencesProvider.overrideWithValue(prefs),
      ],
      child: MyApp(),
    ),
  );
}
```

### Best Practice 2: ViewModels Get Dependencies via ref

ViewModels should NEVER create their own dependencies. They should always get them from Riverpod:

```dart
// GOOD:
@riverpod
class UserViewModel extends _$UserViewModel {
  @override
  Future<User> build() async {
    final repo = ref.watch(userRepositoryProvider);  // Get from Riverpod
    return repo.getCurrentUser();
  }
}

// BAD:
@riverpod
class UserViewModel extends _$UserViewModel {
  final repo = UserRepository();  // DON'T create directly!
}
```

### Best Practice 3: Use Abstract Classes for Repositories

Define repository interfaces as abstract classes. This makes mocking easy:

```dart
// Define interface
abstract class UserRepository {
  Future<User> getUser(String id);
  Future<void> updateUser(User user);
}

// Real implementation
class ApiUserRepository implements UserRepository { ... }

// Mock for testing
class MockUserRepository implements UserRepository { ... }
```

### Best Practice 4: keepAlive for Singletons

For services that should exist for the entire app lifetime, use keepAlive:

```dart
@Riverpod(keepAlive: true)
AnalyticsService analytics(Ref ref) => AnalyticsService();

@Riverpod(keepAlive: true)
AuthService authService(Ref ref) => AuthService();
```

### Best Practice 5: Layer Your Dependencies

Organize providers in clear layers:
1. **Core layer**: HTTP clients, storage, platform services
2. **Data layer**: Repositories that use core layer
3. **Domain layer**: ViewModels that use data layer
4. **Presentation layer**: Widgets that use domain layer

Each layer should only depend on the layer below it.

### Best Practice 6: Test at the Right Level

- **Unit tests**: Override at repository level, test ViewModel logic
- **Integration tests**: Override at API client level, test full feature flow
- **Widget tests**: Override to control what data the widget receives