---
type: "THEORY"
title: "Common Hooks"
---

Flutter Hooks provides many built-in hooks. Here are the ones you will use most often:

### useState<T>() - Local State

The simplest hook. Creates a piece of state that persists across rebuilds:

```dart
class Counter extends HookWidget {
  @override
  Widget build(BuildContext context) {
    // Creates state that persists across rebuilds
    final counter = useState(0);

    return Column(
      children: [
        Text('Count: ${counter.value}'),
        ElevatedButton(
          onPressed: () => counter.value++,  // Triggers rebuild
          child: Text('Increment'),
        ),
      ],
    );
  }
}
```

`useState` returns a `ValueNotifier<T>`. Access the value with `.value`, and setting it triggers a rebuild.

### useEffect() - Side Effects

Runs side effects like initState/dispose, but more flexible:

```dart
class UserProfile extends HookWidget {
  final String userId;
  UserProfile({required this.userId});

  @override
  Widget build(BuildContext context) {
    final userData = useState<User?>(null);

    // Runs when widget mounts (like initState)
    // Runs again if userId changes
    useEffect(() {
      fetchUser(userId).then((user) => userData.value = user);

      // Return a cleanup function (like dispose)
      return () {
        print('Cleaning up for user: $userId');
      };
    }, [userId]);  // Dependencies - re-run if these change

    return userData.value == null
        ? CircularProgressIndicator()
        : Text(userData.value!.name);
  }
}
```

The second argument is the dependency list:
- `[]` - Run once on mount, cleanup on dispose
- `[userId]` - Run on mount and whenever userId changes
- No argument - Run after every build (rarely needed)

### useMemoized() - Cached Computations

Caches expensive computations so they do not re-run on every build:

```dart
class ExpensiveList extends HookWidget {
  final List<Item> items;
  ExpensiveList({required this.items});

  @override
  Widget build(BuildContext context) {
    // Only recomputes when items changes
    final sortedItems = useMemoized(
      () => items.toList()..sort((a, b) => a.name.compareTo(b.name)),
      [items],
    );

    return ListView(
      children: sortedItems.map((i) => ListTile(title: Text(i.name))).toList(),
    );
  }
}
```

### useTextEditingController() - Auto-Disposed Controllers

Creates a TextEditingController that is automatically disposed:

```dart
class SearchBar extends HookWidget {
  @override
  Widget build(BuildContext context) {
    final controller = useTextEditingController();

    return TextField(
      controller: controller,
      onSubmitted: (query) => search(query),
    );
  }
}

// With initial text:
final controller = useTextEditingController(text: 'Initial value');
```

### Other Useful Hooks

```dart
// Focus management
final focusNode = useFocusNode();

// Animation controller (auto-disposed)
final animController = useAnimationController(
  duration: Duration(milliseconds: 300),
);

// Scroll controller
final scrollController = useScrollController();

// Tab controller
final tabController = useTabController(initialLength: 3);

// Page controller
final pageController = usePageController();

// Stream subscription
useStream(myStream);

// Future
final snapshot = useFuture(myFuture);
```

```dart
// Complete example showing common hooks together

import 'package:flutter/material.dart';
import 'package:flutter_hooks/flutter_hooks.dart';

class UserSearchScreen extends HookWidget {
  @override
  Widget build(BuildContext context) {
    // useState: track loading state
    final isLoading = useState(false);
    final users = useState<List<User>>([]);
    final errorMessage = useState<String?>(null);

    // useTextEditingController: auto-disposed text controller
    final searchController = useTextEditingController();

    // useFocusNode: auto-disposed focus node
    final searchFocus = useFocusNode();

    // useEffect: auto-focus on mount
    useEffect(() {
      searchFocus.requestFocus();
      return null;  // No cleanup needed
    }, []);  // Empty deps = run once on mount

    // useMemoized: filter users based on search (cached)
    final filteredUsers = useMemoized(
      () {
        final query = searchController.text.toLowerCase();
        if (query.isEmpty) return users.value;
        return users.value
            .where((u) => u.name.toLowerCase().contains(query))
            .toList();
      },
      [users.value, searchController.text],
    );

    // useEffect: fetch users on mount
    useEffect(() {
      isLoading.value = true;
      fetchUsers().then((result) {
        users.value = result;
        isLoading.value = false;
      }).catchError((e) {
        errorMessage.value = e.toString();
        isLoading.value = false;
      });

      return null;  // No cleanup
    }, []);

    return Scaffold(
      appBar: AppBar(title: Text('User Search')),
      body: Column(
        children: [
          // Search field with auto-disposed controller
          Padding(
            padding: EdgeInsets.all(16),
            child: TextField(
              controller: searchController,
              focusNode: searchFocus,
              decoration: InputDecoration(
                hintText: 'Search users...',
                prefixIcon: Icon(Icons.search),
              ),
              onChanged: (_) {
                // Force rebuild to update filteredUsers
                // In real app, you'd debounce this
              },
            ),
          ),

          // Results
          Expanded(
            child: isLoading.value
                ? Center(child: CircularProgressIndicator())
                : errorMessage.value != null
                    ? Center(child: Text('Error: ${errorMessage.value}'))
                    : ListView.builder(
                        itemCount: filteredUsers.length,
                        itemBuilder: (context, index) {
                          final user = filteredUsers[index];
                          return ListTile(
                            leading: CircleAvatar(child: Text(user.name[0])),
                            title: Text(user.name),
                            subtitle: Text(user.email),
                          );
                        },
                      ),
          ),
        ],
      ),
    );
  }
}

class User {
  final String id;
  final String name;
  final String email;
  User({required this.id, required this.name, required this.email});
}

Future<List<User>> fetchUsers() async {
  await Future.delayed(Duration(seconds: 1));
  return [
    User(id: '1', name: 'Alice Johnson', email: 'alice@example.com'),
    User(id: '2', name: 'Bob Smith', email: 'bob@example.com'),
    User(id: '3', name: 'Carol White', email: 'carol@example.com'),
  ];
}
```
