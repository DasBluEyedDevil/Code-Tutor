---
type: "THEORY"
title: "What is Architecture?"
---

**Architecture** is how you organize your code into separate layers, where each layer has ONE clear responsibility. This principle is called **Separation of Concerns**.

Let us take that messy example and see how architecture transforms it:

### Layer 1: Model (Data)
Defines WHAT data looks like. Nothing else.

### Layer 2: Repository (Data Access)
Knows HOW to get and save data. Handles API calls, database operations, caching.

### Layer 3: ViewModel (Business Logic)
Decides WHAT to do with data. Handles loading states, validation, transformations.

### Layer 4: View (UI)
Shows data to users. Handles ONLY visual presentation.

Here is the same user profile, properly architected:

```dart
// LAYER 1: MODEL - Just data, nothing else
class User {
  final String id;
  final String name;
  final String email;

  User({required this.id, required this.name, required this.email});

  // Factory to create from JSON
  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      id: json['id'],
      name: json['name'],
      email: json['email'],
    );
  }

  // Convert back to JSON
  Map<String, dynamic> toJson() {
    return {'id': id, 'name': name, 'email': email};
  }
}

// LAYER 2: REPOSITORY - Handles data fetching
class UserRepository {
  final String baseUrl = 'https://api.example.com';

  Future<User> getUser(String id) async {
    final response = await http.get(Uri.parse('$baseUrl/users/$id'));
    if (response.statusCode == 200) {
      return User.fromJson(jsonDecode(response.body));
    }
    throw Exception('Failed to load user');
  }

  Future<void> updateUser(User user) async {
    await http.put(
      Uri.parse('$baseUrl/users/${user.id}'),
      body: jsonEncode(user.toJson()),
    );
  }
}

// LAYER 3: VIEWMODEL - Manages state and logic
// (Using Riverpod - we will learn this soon!)
@riverpod
class UserViewModel extends _$UserViewModel {
  @override
  Future<User> build(String userId) async {
    final repository = ref.read(userRepositoryProvider);
    return repository.getUser(userId);
  }

  Future<void> updateName(String newName) async {
    final user = await future;
    final updatedUser = User(
      id: user.id,
      name: newName,
      email: user.email,
    );
    final repository = ref.read(userRepositoryProvider);
    await repository.updateUser(updatedUser);
    ref.invalidateSelf();  // Refresh data
  }
}

// LAYER 4: VIEW - Only UI, no logic
class UserProfileScreen extends ConsumerWidget {
  final String userId;
  const UserProfileScreen({required this.userId});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final userAsync = ref.watch(userViewModelProvider(userId));

    return userAsync.when(
      loading: () => CircularProgressIndicator(),
      error: (err, _) => Text('Error: $err'),
      data: (user) => Column(
        children: [
          Text(user.name),
          Text(user.email),
          ElevatedButton(
            onPressed: () => ref
                .read(userViewModelProvider(userId).notifier)
                .updateName('New Name'),
            child: Text('Update'),
          ),
        ],
      ),
    );
  }
}
```
