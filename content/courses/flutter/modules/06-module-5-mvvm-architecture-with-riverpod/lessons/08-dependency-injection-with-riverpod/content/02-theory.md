---
type: "THEORY"
title: "Riverpod IS Dependency Injection"
---

Here is the great news: **if you are using Riverpod, you are already doing dependency injection!** You do not need a separate DI framework like GetIt, Injectable, or Dagger.

Riverpod's provider system IS a dependency injection container. Let us see how.

### Providers as a DI Container

In traditional DI frameworks, you register dependencies in a container:
```dart
// Other frameworks
container.register<UserRepository>(() => ApiUserRepository());
container.register<UserViewModel>(() => UserViewModel(container.get<UserRepository>()));
```

With Riverpod, providers ARE the container:
```dart
// Riverpod
final userRepositoryProvider = Provider((ref) => ApiUserRepository());

@riverpod
class UserViewModel extends _$UserViewModel {
  @override
  User build() {
    final repo = ref.watch(userRepositoryProvider);  // DI!
    return repo.fetchUser();
  }
}
```

### ref.watch and ref.read ARE Dependency Injection

When you call `ref.watch(someProvider)` or `ref.read(someProvider)`, you are:
1. Asking Riverpod's container for a dependency
2. Getting the instance Riverpod manages
3. Establishing a dependency relationship

This is exactly what DI frameworks do, but with a more elegant, declarative API.

### Benefits of Riverpod as DI

1. **No setup required**: No need to configure a separate container
2. **Type-safe**: Providers are strongly typed
3. **Lazy initialization**: Dependencies created only when needed
4. **Automatic disposal**: Dependencies cleaned up when no longer used
5. **Override for testing**: Built-in support for swapping implementations
6. **Reactive**: Dependencies can rebuild when their dependencies change

```dart
// =====================================================
// RIVERPOD AS DEPENDENCY INJECTION
// =====================================================

import 'package:riverpod_annotation/riverpod_annotation.dart';

part 'providers.g.dart';

// =====================================================
// STEP 1: Define your dependencies as providers
// =====================================================

// Low-level dependency: API client
@riverpod
ApiClient apiClient(Ref ref) {
  return ApiClient(baseUrl: 'https://api.example.com');
}

// Mid-level dependency: Repository (depends on ApiClient)
@riverpod
UserRepository userRepository(Ref ref) {
  // DEPENDENCY INJECTION: Get ApiClient from Riverpod
  final client = ref.watch(apiClientProvider);
  return UserRepository(client);
}

// High-level: ViewModel (depends on Repository)
@riverpod
class UserViewModel extends _$UserViewModel {
  @override
  Future<User> build() async {
    // DEPENDENCY INJECTION: Get Repository from Riverpod
    final repo = ref.watch(userRepositoryProvider);
    return await repo.getCurrentUser();
  }

  Future<void> updateProfile(String name) async {
    final repo = ref.read(userRepositoryProvider);
    state = const AsyncLoading();
    state = await AsyncValue.guard(() => repo.updateUser(name));
  }
}

// =====================================================
// THE DEPENDENCY CHAIN
// =====================================================
//
// UserViewModel
//      |
//      | ref.watch(userRepositoryProvider)
//      v
// UserRepository
//      |
//      | ref.watch(apiClientProvider)
//      v
//   ApiClient
//
// Each level gets its dependencies injected by Riverpod!

// =====================================================
// USAGE IN WIDGET
// =====================================================

class ProfileScreen extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // You don't create dependencies - Riverpod handles it!
    final userAsync = ref.watch(userViewModelProvider);

    return userAsync.when(
      loading: () => CircularProgressIndicator(),
      error: (e, _) => Text('Error: $e'),
      data: (user) => Column(
        children: [
          Text('Hello, ${user.name}'),
          ElevatedButton(
            onPressed: () {
              ref.read(userViewModelProvider.notifier).updateProfile('New Name');
            },
            child: Text('Update Profile'),
          ),
        ],
      ),
    );
  }
}

// Notice: The widget has NO idea how UserViewModel gets its dependencies.
// It just asks Riverpod for the ViewModel, and everything is wired up.
```
