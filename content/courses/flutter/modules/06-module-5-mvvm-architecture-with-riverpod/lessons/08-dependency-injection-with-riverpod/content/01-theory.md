---
type: "THEORY"
title: "What is Dependency Injection?"
---

Dependency Injection (DI) is a fundamental software design pattern that makes your code more flexible, testable, and maintainable. Let us understand what it means and why it matters.

### The Problem: Hard-Coded Dependencies

Imagine you are building a UserViewModel that needs to fetch user data from an API. A naive approach creates the dependency directly inside the class:

```dart
class UserViewModel {
  // BAD: Creating dependency inside the class
  final UserRepository _repo = UserRepository();

  Future<User> getUser(String id) {
    return _repo.fetchUser(id);
  }
}
```

This seems simple, but it has serious problems:

**Problem 1: Cannot Test in Isolation**
When you test UserViewModel, you MUST use the real UserRepository. If that repository makes HTTP calls, your unit tests now require a running server. Tests become slow, flaky, and dependent on network connectivity.

**Problem 2: Cannot Swap Implementations**
What if you need different repositories for different environments?
- Development: Use a fake repository with mock data
- Staging: Use a repository pointing to staging servers
- Production: Use the real production repository

With hard-coded dependencies, you cannot swap these without modifying the ViewModel code.

**Problem 3: Hidden Dependencies**
Looking at `new UserViewModel()`, you cannot tell what it depends on. The dependencies are hidden inside. This makes debugging and understanding code flow difficult.

### The Solution: Dependency Injection

**Dependency Injection means passing dependencies from the outside rather than creating them inside.**

```dart
class UserViewModel {
  // GOOD: Dependency is injected (passed in)
  final UserRepository _repo;

  UserViewModel(this._repo);  // Repository comes from outside

  Future<User> getUser(String id) {
    return _repo.fetchUser(id);
  }
}
```

Now you can:
- Pass a real repository in production
- Pass a mock repository in tests
- Pass different repositories for different environments

The ViewModel does not know or care which repository it gets. It just uses whatever is provided.

```dart
// =====================================================
// BAD: Hard-coded dependency (NOT Dependency Injection)
// =====================================================

class UserRepository {
  Future<User> fetchUser(String id) async {
    // Makes real HTTP call
    final response = await http.get(Uri.parse('https://api.example.com/users/$id'));
    return User.fromJson(jsonDecode(response.body));
  }
}

class UserViewModel {
  // Creating the dependency directly - BAD!
  final UserRepository _repo = UserRepository();

  Future<User> loadUser(String id) async {
    return await _repo.fetchUser(id);
  }
}

// TESTING IS IMPOSSIBLE:
void testUserViewModel() {
  final vm = UserViewModel();
  // This will make a REAL HTTP call!
  // Test requires network, running server, etc.
  final user = await vm.loadUser('123');
}

// =====================================================
// GOOD: Dependency Injection
// =====================================================

abstract class UserRepository {
  Future<User> fetchUser(String id);
}

class ApiUserRepository implements UserRepository {
  @override
  Future<User> fetchUser(String id) async {
    final response = await http.get(Uri.parse('https://api.example.com/users/$id'));
    return User.fromJson(jsonDecode(response.body));
  }
}

class MockUserRepository implements UserRepository {
  @override
  Future<User> fetchUser(String id) async {
    // Returns fake data instantly - no network needed!
    return User(id: id, name: 'Test User', email: 'test@example.com');
  }
}

class UserViewModel {
  // Dependency is INJECTED from outside
  final UserRepository _repo;

  UserViewModel(this._repo);  // Receives repository as parameter

  Future<User> loadUser(String id) async {
    return await _repo.fetchUser(id);
  }
}

// PRODUCTION:
final vm = UserViewModel(ApiUserRepository());

// TESTING:
void testUserViewModel() {
  final vm = UserViewModel(MockUserRepository());  // Use mock!
  final user = await vm.loadUser('123');
  // No HTTP call! Fast, reliable test.
  expect(user.name, equals('Test User'));
}
```
