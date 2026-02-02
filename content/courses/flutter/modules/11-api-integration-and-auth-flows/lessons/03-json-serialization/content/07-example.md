---
type: "EXAMPLE"
title: "freezed Union Types for State Management"
---

One of freezed's most powerful features is union types (also called sealed classes). They let you define multiple states for a single type, perfect for representing loading states, results, or any situation with distinct variants.

**Basic Union Type Example**

```dart
import 'package:freezed_annotation/freezed_annotation.dart';

part 'auth_state.freezed.dart';

@freezed
class AuthState with _$AuthState {
  // Each factory represents a different state
  const factory AuthState.initial() = AuthInitial;
  const factory AuthState.loading() = AuthLoading;
  const factory AuthState.authenticated(User user) = AuthAuthenticated;
  const factory AuthState.unauthenticated() = AuthUnauthenticated;
  const factory AuthState.error(String message) = AuthError;
}
```

**Using Union Types with Pattern Matching**

```dart
// In your UI code
Widget build(BuildContext context) {
  return authState.when(
    initial: () => const SizedBox.shrink(),
    loading: () => const CircularProgressIndicator(),
    authenticated: (user) => HomeScreen(user: user),
    unauthenticated: () => const LoginScreen(),
    error: (message) => ErrorWidget(message: message),
  );
}

// Or with maybeWhen for partial matching
Widget build(BuildContext context) {
  return authState.maybeWhen(
    loading: () => const CircularProgressIndicator(),
    orElse: () => const SizedBox.shrink(),
  );
}

// Or with map for type-safe access
void handleState(AuthState state) {
  state.map(
    initial: (_) => print('App just started'),
    loading: (_) => print('Loading...'),
    authenticated: (auth) => print('Welcome ${auth.user.name}'),
    unauthenticated: (_) => print('Please log in'),
    error: (err) => print('Error: ${err.message}'),
  );
}
```

**Result Type for API Responses**

Create a reusable Result type for handling success/failure:

```dart
import 'package:freezed_annotation/freezed_annotation.dart';

part 'result.freezed.dart';

@freezed
class Result<T> with _$Result<T> {
  const factory Result.success(T data) = Success<T>;
  const factory Result.failure(String message, [Exception? exception]) = Failure<T>;
  const factory Result.loading() = Loading<T>;
}

// Usage in a repository
class UserRepository {
  final ApiClient _client;
  
  UserRepository(this._client);
  
  Future<Result<User>> getUser(int id) async {
    try {
      final response = await _client.get('/users/$id');
      final user = User.fromJson(response.data);
      return Result.success(user);
    } on DioException catch (e) {
      return Result.failure(
        'Failed to fetch user',
        e,
      );
    } catch (e) {
      return Result.failure('Unexpected error: $e');
    }
  }
}

// Using the result
void loadUser() async {
  final result = await userRepository.getUser(1);
  
  result.when(
    success: (user) {
      print('Loaded user: ${user.name}');
    },
    failure: (message, exception) {
      print('Error: $message');
    },
    loading: () {
      print('Loading...');
    },
  );
}
```

**Network Request State**

```dart
@freezed
class NetworkState<T> with _$NetworkState<T> {
  const factory NetworkState.idle() = NetworkIdle<T>;
  const factory NetworkState.loading() = NetworkLoading<T>;
  const factory NetworkState.data(T data) = NetworkData<T>;
  const factory NetworkState.error(String message, {StackTrace? stackTrace}) = NetworkError<T>;
}

// In a ViewModel or Cubit
class UsersViewModel extends ChangeNotifier {
  NetworkState<List<User>> _state = const NetworkState.idle();
  NetworkState<List<User>> get state => _state;
  
  Future<void> fetchUsers() async {
    _state = const NetworkState.loading();
    notifyListeners();
    
    try {
      final users = await _repository.getUsers();
      _state = NetworkState.data(users);
    } catch (e, stackTrace) {
      _state = NetworkState.error(
        'Failed to load users',
        stackTrace: stackTrace,
      );
    }
    notifyListeners();
  }
}

// In the UI
Widget build(BuildContext context) {
  return viewModel.state.when(
    idle: () => const Text('Tap to load'),
    loading: () => const CircularProgressIndicator(),
    data: (users) => UserListView(users: users),
    error: (message, _) => Text('Error: $message'),
  );
}
```

**Form Validation State**

```dart
@freezed
class ValidationResult with _$ValidationResult {
  const factory ValidationResult.valid() = ValidationValid;
  const factory ValidationResult.invalid(List<String> errors) = ValidationInvalid;
}

@freezed
class FormState with _$FormState {
  const factory FormState({
    @Default('') String email,
    @Default('') String password,
    @Default(ValidationResult.valid()) ValidationResult emailValidation,
    @Default(ValidationResult.valid()) ValidationResult passwordValidation,
    @Default(false) bool isSubmitting,
  }) = _FormState;
}

// Usage
ValidationResult validateEmail(String email) {
  final errors = <String>[];
  
  if (email.isEmpty) {
    errors.add('Email is required');
  } else if (!email.contains('@')) {
    errors.add('Invalid email format');
  }
  
  return errors.isEmpty 
      ? const ValidationResult.valid() 
      : ValidationResult.invalid(errors);
}
```

