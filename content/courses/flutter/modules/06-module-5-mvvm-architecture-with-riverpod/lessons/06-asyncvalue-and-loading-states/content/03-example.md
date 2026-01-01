---
type: "EXAMPLE"
title: "FutureProvider for Simple Cases"
---

**FutureProvider** is the simplest way to fetch async data. Use it when you just need to fetch data once and display it. The provider automatically manages the loading and error states for you.

```dart
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:http/http.dart' as http;

// User Model
class User {
  final String id;
  final String name;
  final String email;

  User({required this.id, required this.name, required this.email});

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      id: json['id'].toString(),
      name: json['name'],
      email: json['email'],
    );
  }
}

// FutureProvider - fetches user data
// Riverpod handles loading/error states automatically!
final userProvider = FutureProvider<User>((ref) async {
  // Simulate network delay for demo
  await Future.delayed(const Duration(seconds: 2));
  
  // In real app: fetch from API
  final response = await http.get(
    Uri.parse('https://jsonplaceholder.typicode.com/users/1'),
  );
  
  if (response.statusCode == 200) {
    return User.fromJson(jsonDecode(response.body));
  } else {
    throw Exception('Failed to load user');
  }
});

// Widget using the provider
class UserScreen extends ConsumerWidget {
  const UserScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Watch returns AsyncValue<User>
    final userAsync = ref.watch(userProvider);

    return Scaffold(
      appBar: AppBar(title: const Text('User Profile')),
      body: Center(
        // The magic: when() handles all three states!
        child: userAsync.when(
          // LOADING STATE: Show spinner
          loading: () => const Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              CircularProgressIndicator(),
              SizedBox(height: 16),
              Text('Loading user...'),
            ],
          ),
          
          // ERROR STATE: Show error message
          error: (error, stackTrace) => Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              const Icon(Icons.error, color: Colors.red, size: 48),
              const SizedBox(height: 16),
              Text('Error: $error'),
              const SizedBox(height: 16),
              ElevatedButton(
                // Refresh: invalidate the provider to retry
                onPressed: () => ref.invalidate(userProvider),
                child: const Text('Retry'),
              ),
            ],
          ),
          
          // DATA STATE: Show the user
          data: (user) => Card(
            margin: const EdgeInsets.all(16),
            child: Padding(
              padding: const EdgeInsets.all(24),
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  const CircleAvatar(
                    radius: 40,
                    child: Icon(Icons.person, size: 40),
                  ),
                  const SizedBox(height: 16),
                  Text(
                    user.name,
                    style: const TextStyle(
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  const SizedBox(height: 8),
                  Text(
                    user.email,
                    style: TextStyle(color: Colors.grey[600]),
                  ),
                  const SizedBox(height: 16),
                  ElevatedButton.icon(
                    onPressed: () => ref.invalidate(userProvider),
                    icon: const Icon(Icons.refresh),
                    label: const Text('Refresh'),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

// Main app
void main() {
  runApp(
    const ProviderScope(
      child: MaterialApp(home: UserScreen()),
    ),
  );
}
```
