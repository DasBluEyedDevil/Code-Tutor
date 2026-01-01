---
type: "THEORY"
title: "AsyncNotifier for Complex Cases"
---

**FutureProvider** is great for simple fetch-and-display scenarios. But what if you need to:
- Refresh the data on demand?
- Modify the data after fetching?
- Have multiple async operations?

This is where **AsyncNotifier** comes in. It combines the power of Notifier (methods for state management) with AsyncValue (loading/error handling).

### AsyncNotifier Structure

AsyncNotifier is like Notifier, but the `build()` method is async and returns the data directly (not wrapped in AsyncValue - Riverpod does that for you):

```dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'dart:convert';
import 'package:http/http.dart' as http;

// User model
class User {
  final String id;
  final String name;
  final String email;

  User({required this.id, required this.name, required this.email});

  factory User.fromJson(Map<String, dynamic> json) => User(
    id: json['id'].toString(),
    name: json['name'],
    email: json['email'],
  );

  User copyWith({String? name, String? email}) => User(
    id: id,
    name: name ?? this.name,
    email: email ?? this.email,
  );
}

// AsyncNotifier for complex async state management
class UserNotifier extends AsyncNotifier<User> {
  @override
  Future<User> build() async {
    // This is called when provider is first accessed
    // Returns the initial data (fetched from API)
    return await _fetchUser();
  }

  // Private method to fetch user
  Future<User> _fetchUser() async {
    final response = await http.get(
      Uri.parse('https://jsonplaceholder.typicode.com/users/1'),
    );
    
    if (response.statusCode == 200) {
      return User.fromJson(jsonDecode(response.body));
    } else {
      throw Exception('Failed to load user');
    }
  }

  // PUBLIC METHOD: Refresh user data
  Future<void> refresh() async {
    // Set state to loading
    state = const AsyncValue.loading();
    
    // Use AsyncValue.guard to handle errors automatically
    state = await AsyncValue.guard(() => _fetchUser());
  }

  // PUBLIC METHOD: Update user name
  Future<void> updateName(String newName) async {
    // Get current user (if we have data)
    final currentUser = state.valueOrNull;
    if (currentUser == null) return;

    // Set loading state while updating
    state = const AsyncValue.loading();
    
    // Simulate API call to update
    state = await AsyncValue.guard(() async {
      // In real app: await api.updateUser(currentUser.id, name: newName);
      await Future.delayed(const Duration(seconds: 1));
      
      // Return updated user
      return currentUser.copyWith(name: newName);
    });
  }

  // PUBLIC METHOD: Update user email  
  Future<void> updateEmail(String newEmail) async {
    final currentUser = state.valueOrNull;
    if (currentUser == null) return;

    state = const AsyncValue.loading();
    
    state = await AsyncValue.guard(() async {
      await Future.delayed(const Duration(seconds: 1));
      return currentUser.copyWith(email: newEmail);
    });
  }
}

// Create the provider
final userNotifierProvider = AsyncNotifierProvider<UserNotifier, User>(() {
  return UserNotifier();
});

// UNDERSTANDING AsyncValue.guard()
//
// AsyncValue.guard() is a helper that:
// 1. Runs your async function
// 2. If successful: returns AsyncValue.data(result)
// 3. If error: returns AsyncValue.error(error, stackTrace)
//
// Without guard, you would write:
//   try {
//     final result = await someAsyncOperation();
//     state = AsyncValue.data(result);
//   } catch (e, stack) {
//     state = AsyncValue.error(e, stack);
//   }
//
// With guard, just:
//   state = await AsyncValue.guard(() => someAsyncOperation());

// Example usage in widget:
// 
// ElevatedButton(
//   onPressed: () {
//     ref.read(userNotifierProvider.notifier).updateName('New Name');
//   },
//   child: Text('Update Name'),
// )
```
