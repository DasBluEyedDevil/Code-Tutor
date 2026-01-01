---
type: "THEORY"
title: "Generated Provider Types"
---

The Riverpod generator is smart about what type of provider to create based on how you write your code. Here are the different patterns:

### Pattern 1: Function with @riverpod -> Provider

Annotating a simple function creates a basic Provider (read-only, computed value):

```dart
@riverpod
String greeting(Ref ref) {
  return 'Hello, World!';
}
// Generates: greetingProvider (Provider<String>)
```

### Pattern 2: Class with @riverpod -> NotifierProvider

Annotating a class creates a NotifierProvider (read-write state with methods):

```dart
@riverpod
class Counter extends _$Counter {
  @override
  int build() => 0;

  void increment() => state++;
}
// Generates: counterProvider (NotifierProvider<Counter, int>)
```

### Pattern 3: Async Function -> FutureProvider

Annotating an async function creates a FutureProvider:

```dart
@riverpod
Future<User> currentUser(Ref ref) async {
  final response = await http.get(Uri.parse('api/user'));
  return User.fromJson(jsonDecode(response.body));
}
// Generates: currentUserProvider (FutureProvider<User>)
```

### Pattern 4: Async Class -> AsyncNotifierProvider

Annotating a class with an async build method creates AsyncNotifierProvider:

```dart
@riverpod
class UserProfile extends _$UserProfile {
  @override
  Future<User> build() async {
    return await fetchUser();
  }

  Future<void> updateName(String name) async {
    state = const AsyncLoading();
    state = await AsyncValue.guard(() => api.updateUser(name));
  }
}
// Generates: userProfileProvider (AsyncNotifierProvider<UserProfile, User>)
```

### Pattern 5: keepAlive for Persistent State

By default, generated providers are auto-disposed when no longer used. Use `@Riverpod(keepAlive: true)` to keep state alive:

```dart
@Riverpod(keepAlive: true)  // Note: Capital R, parentheses
class AuthState extends _$AuthState {
  @override
  User? build() => null;

  void login(User user) => state = user;
  void logout() => state = null;
}
// Generates: authStateProvider that NEVER disposes
// Perfect for auth state that should persist for app lifetime
```

### Pattern 6: Family Providers (Parameters)

Add parameters to your function or build method to create family providers:

```dart
@riverpod
Future<Product> product(Ref ref, String productId) async {
  return await api.getProduct(productId);
}
// Generates: productProvider (can call productProvider('123'))

// Usage:
final product = ref.watch(productProvider('123'));
```

```dart
// COMPLETE EXAMPLE: All generated provider types in one file

import 'package:riverpod_annotation/riverpod_annotation.dart';

part 'providers.g.dart';

// =====================================================
// 1. SIMPLE PROVIDER (read-only value)
// =====================================================
@riverpod
String appName(Ref ref) {
  return 'My Awesome App';
}
// -> appNameProvider (Provider<String>)

// =====================================================
// 2. COMPUTED PROVIDER (depends on other providers)
// =====================================================
@riverpod
String welcomeMessage(Ref ref) {
  final name = ref.watch(appNameProvider);
  return 'Welcome to $name!';
}
// -> welcomeMessageProvider (Provider<String>)

// =====================================================
// 3. NOTIFIER (stateful with methods)
// =====================================================
@riverpod
class Counter extends _$Counter {
  @override
  int build() => 0;

  void increment() => state++;
  void decrement() => state--;
}
// -> counterProvider (NotifierProvider<Counter, int>)

// =====================================================
// 4. FUTURE PROVIDER (async data fetch)
// =====================================================
@riverpod
Future<List<String>> fetchTodos(Ref ref) async {
  await Future.delayed(Duration(seconds: 1));
  return ['Learn Riverpod', 'Build an app', 'Deploy to store'];
}
// -> fetchTodosProvider (FutureProvider<List<String>>)

// =====================================================
// 5. ASYNC NOTIFIER (async + methods)
// =====================================================
@riverpod
class TodoList extends _$TodoList {
  @override
  Future<List<String>> build() async {
    return await _fetchTodos();
  }

  Future<List<String>> _fetchTodos() async {
    await Future.delayed(Duration(seconds: 1));
    return ['Task 1', 'Task 2', 'Task 3'];
  }

  Future<void> addTodo(String todo) async {
    state = const AsyncLoading();
    final currentTodos = await future;  // Get current data
    state = AsyncData([...currentTodos, todo]);
  }

  Future<void> refresh() async {
    state = const AsyncLoading();
    state = await AsyncValue.guard(_fetchTodos);
  }
}
// -> todoListProvider (AsyncNotifierProvider<TodoList, List<String>>)

// =====================================================
// 6. KEEP ALIVE (never disposed)
// =====================================================
@Riverpod(keepAlive: true)
class AppSettings extends _$AppSettings {
  @override
  Map<String, dynamic> build() => {'theme': 'light', 'language': 'en'};

  void updateTheme(String theme) {
    state = {...state, 'theme': theme};
  }
}
// -> appSettingsProvider (NotifierProvider that persists)

// =====================================================
// 7. FAMILY PROVIDER (with parameters)
// =====================================================
@riverpod
Future<String> userById(Ref ref, int userId) async {
  await Future.delayed(Duration(milliseconds: 500));
  return 'User #$userId';
}
// -> userByIdProvider (call with userByIdProvider(123))

// =====================================================
// 8. FAMILY NOTIFIER (class with parameters)
// =====================================================
@riverpod
class ProductDetail extends _$ProductDetail {
  @override
  Future<Map<String, dynamic>> build(String productId) async {
    // productId is available as a parameter!
    return await fetchProduct(productId);
  }

  Future<Map<String, dynamic>> fetchProduct(String id) async {
    await Future.delayed(Duration(seconds: 1));
    return {'id': id, 'name': 'Product $id', 'price': 29.99};
  }

  Future<void> refresh() async {
    // Access the productId passed to build
    state = await AsyncValue.guard(() => fetchProduct(arg));
  }
}
// -> productDetailProvider('ABC123') to get product ABC123
```
