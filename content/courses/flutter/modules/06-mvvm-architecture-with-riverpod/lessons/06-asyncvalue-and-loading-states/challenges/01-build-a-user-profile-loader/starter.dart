import 'dart:math';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

void main() {
  runApp(const ProviderScope(child: MyApp()));
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'User Profile',
      theme: ThemeData(primarySwatch: Colors.blue),
      home: const UserProfileScreen(),
    );
  }
}

// TODO 1: Create User model
// - id (String)
// - name (String)
// - email (String)
// - phone (String)
class User {
  // Your code here
}

// TODO 2: Create FutureProvider
// - Simulate 2 second API delay
// - 20% chance of throwing error
// - Return mock User on success
// final userProvider = FutureProvider<User>((ref) async {
//   Your code here
// });

// TODO 3: Build UserProfileScreen
class UserProfileScreen extends ConsumerWidget {
  const UserProfileScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // TODO: Watch userProvider
    // TODO: Use .when() to handle loading, error, and data states
    
    return Scaffold(
      appBar: AppBar(
        title: const Text('User Profile'),
        // TODO: Add refresh button using ref.invalidate()
      ),
      body: const Center(
        child: Text('Implement loading states here'),
      ),
    );
  }
}

// TODO 4: Create UserCard widget to display user data nicely
// - CircleAvatar with initials
// - Name in large bold text
// - Email with icon
// - Phone with icon