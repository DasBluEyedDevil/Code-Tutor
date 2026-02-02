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
      theme: ThemeData(
        primarySwatch: Colors.blue,
        useMaterial3: true,
      ),
      home: const UserProfileScreen(),
    );
  }
}

// STEP 1: User Model
class User {
  final String id;
  final String name;
  final String email;
  final String phone;
  final DateTime fetchedAt;  // Bonus: track when data was fetched

  const User({
    required this.id,
    required this.name,
    required this.email,
    required this.phone,
    required this.fetchedAt,
  });

  // Get initials for avatar
  String get initials {
    final parts = name.split(' ');
    if (parts.length >= 2) {
      return '${parts[0][0]}${parts[1][0]}'.toUpperCase();
    }
    return name.substring(0, 2).toUpperCase();
  }
}

// STEP 2: FutureProvider with simulated API call
final userProvider = FutureProvider<User>((ref) async {
  // Simulate network delay (2 seconds)
  await Future.delayed(const Duration(seconds: 2));
  
  // 20% chance of error to test error handling
  if (Random().nextDouble() < 0.2) {
    throw Exception('Network error: Failed to fetch user profile. Please try again.');
  }
  
  // Return mock user data
  return User(
    id: '12345',
    name: 'John Doe',
    email: 'john.doe@example.com',
    phone: '+1 (555) 123-4567',
    fetchedAt: DateTime.now(),
  );
});

// STEP 3: UserProfileScreen with proper state handling
class UserProfileScreen extends ConsumerWidget {
  const UserProfileScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Watch the provider - returns AsyncValue<User>
    final userAsync = ref.watch(userProvider);

    return Scaffold(
      appBar: AppBar(
        title: const Text('User Profile'),
        centerTitle: true,
        actions: [
          // Refresh button
          IconButton(
            icon: const Icon(Icons.refresh),
            tooltip: 'Refresh',
            onPressed: () {
              // invalidate() disposes the provider and triggers a new fetch
              ref.invalidate(userProvider);
            },
          ),
        ],
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Center(
          // Use when() to handle all three states
          child: userAsync.when(
            // LOADING STATE
            loading: () => const Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                CircularProgressIndicator(),
                SizedBox(height: 24),
                Text(
                  'Loading profile...',
                  style: TextStyle(fontSize: 16, color: Colors.grey),
                ),
              ],
            ),
            
            // ERROR STATE
            error: (error, stackTrace) => Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Icon(
                  Icons.error_outline,
                  size: 64,
                  color: Colors.red[300],
                ),
                const SizedBox(height: 16),
                Text(
                  'Something went wrong',
                  style: TextStyle(
                    fontSize: 20,
                    fontWeight: FontWeight.bold,
                    color: Colors.red[700],
                  ),
                ),
                const SizedBox(height: 8),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 32),
                  child: Text(
                    error.toString(),
                    textAlign: TextAlign.center,
                    style: TextStyle(color: Colors.grey[600]),
                  ),
                ),
                const SizedBox(height: 24),
                ElevatedButton.icon(
                  onPressed: () => ref.invalidate(userProvider),
                  icon: const Icon(Icons.refresh),
                  label: const Text('Retry'),
                  style: ElevatedButton.styleFrom(
                    padding: const EdgeInsets.symmetric(
                      horizontal: 32,
                      vertical: 12,
                    ),
                  ),
                ),
              ],
            ),
            
            // DATA STATE - Show user card
            data: (user) => UserCard(user: user),
          ),
        ),
      ),
    );
  }
}

// STEP 4: UserCard widget
class UserCard extends StatelessWidget {
  final User user;

  const UserCard({super.key, required this.user});

  @override
  Widget build(BuildContext context) {
    return Card(
      elevation: 4,
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(16),
      ),
      child: Padding(
        padding: const EdgeInsets.all(24),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            // Avatar with initials
            CircleAvatar(
              radius: 50,
              backgroundColor: Colors.blue,
              child: Text(
                user.initials,
                style: const TextStyle(
                  fontSize: 32,
                  fontWeight: FontWeight.bold,
                  color: Colors.white,
                ),
              ),
            ),
            const SizedBox(height: 20),
            
            // Name
            Text(
              user.name,
              style: const TextStyle(
                fontSize: 28,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 8),
            
            // User ID badge
            Container(
              padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 4),
              decoration: BoxDecoration(
                color: Colors.grey[200],
                borderRadius: BorderRadius.circular(12),
              ),
              child: Text(
                'ID: ${user.id}',
                style: TextStyle(color: Colors.grey[600], fontSize: 12),
              ),
            ),
            const SizedBox(height: 24),
            
            // Email row
            _InfoRow(
              icon: Icons.email_outlined,
              label: 'Email',
              value: user.email,
            ),
            const SizedBox(height: 12),
            
            // Phone row
            _InfoRow(
              icon: Icons.phone_outlined,
              label: 'Phone',
              value: user.phone,
            ),
            
            const SizedBox(height: 24),
            const Divider(),
            const SizedBox(height: 12),
            
            // Last refreshed timestamp (Bonus)
            Text(
              'Last updated: ${_formatTime(user.fetchedAt)}',
              style: TextStyle(
                fontSize: 12,
                color: Colors.grey[500],
              ),
            ),
          ],
        ),
      ),
    );
  }

  String _formatTime(DateTime time) {
    return '${time.hour.toString().padLeft(2, '0')}:${time.minute.toString().padLeft(2, '0')}:${time.second.toString().padLeft(2, '0')}';
  }
}

// Helper widget for info rows
class _InfoRow extends StatelessWidget {
  final IconData icon;
  final String label;
  final String value;

  const _InfoRow({
    required this.icon,
    required this.label,
    required this.value,
  });

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        Icon(icon, color: Colors.blue, size: 24),
        const SizedBox(width: 12),
        Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              label,
              style: TextStyle(
                fontSize: 12,
                color: Colors.grey[600],
              ),
            ),
            Text(
              value,
              style: const TextStyle(fontSize: 16),
            ),
          ],
        ),
      ],
    );
  }
}

// KEY CONCEPTS DEMONSTRATED:
//
// 1. FutureProvider: Automatically wraps async data in AsyncValue
//
// 2. AsyncValue.when(): Handle all three states (loading, error, data)
//    - loading: Show spinner and message
//    - error: Show error with retry button
//    - data: Show the actual content
//
// 3. ref.invalidate(): Dispose and refetch the provider
//    - Used for refresh/retry functionality
//
// 4. Simulated errors: 20% chance to test error UI
//
// 5. Clean separation: UserCard widget only knows about User data,
//    not about loading states