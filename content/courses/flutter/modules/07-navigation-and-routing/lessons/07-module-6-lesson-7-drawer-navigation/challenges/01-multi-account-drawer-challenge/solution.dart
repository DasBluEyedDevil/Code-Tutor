// Solution: Drawer with Account Switcher
// UserAccountsDrawerHeader with 3 switchable accounts

import 'package:flutter/material.dart';

void main() {
  runApp(const DrawerApp());
}

class Account {
  final String name;
  final String email;
  final String avatarUrl;

  Account({required this.name, required this.email, required this.avatarUrl});
}

class DrawerApp extends StatelessWidget {
  const DrawerApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: const DrawerScreen(),
    );
  }
}

class DrawerScreen extends StatefulWidget {
  const DrawerScreen({super.key});

  @override
  State<DrawerScreen> createState() => _DrawerScreenState();
}

class _DrawerScreenState extends State<DrawerScreen> {
  final List<Account> accounts = [
    Account(name: 'John Doe', email: 'john@example.com', avatarUrl: 'https://picsum.photos/200?1'),
    Account(name: 'Jane Smith', email: 'jane@work.com', avatarUrl: 'https://picsum.photos/200?2'),
    Account(name: 'Dev Account', email: 'dev@company.com', avatarUrl: 'https://picsum.photos/200?3'),
  ];

  int currentAccountIndex = 0;

  Account get currentAccount => accounts[currentAccountIndex];

  void switchAccount(int index) {
    setState(() => currentAccountIndex = index);
    Navigator.pop(context);
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text('Switched to ${accounts[index].name}')),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Welcome, ${currentAccount.name.split(' ').first}')),
      drawer: Drawer(
        child: ListView(
          padding: EdgeInsets.zero,
          children: [
            UserAccountsDrawerHeader(
              accountName: Text(currentAccount.name),
              accountEmail: Text(currentAccount.email),
              currentAccountPicture: CircleAvatar(
                backgroundImage: NetworkImage(currentAccount.avatarUrl),
              ),
              // Other accounts shown in top-right
              otherAccountsPictures: accounts
                  .asMap()
                  .entries
                  .where((e) => e.key != currentAccountIndex)
                  .map((e) => GestureDetector(
                        onTap: () => switchAccount(e.key),
                        child: CircleAvatar(
                          backgroundImage: NetworkImage(e.value.avatarUrl),
                        ),
                      ))
                  .toList(),
              decoration: const BoxDecoration(
                gradient: LinearGradient(
                  colors: [Colors.blue, Colors.purple],
                  begin: Alignment.topLeft,
                  end: Alignment.bottomRight,
                ),
              ),
              onDetailsPressed: () {
                showModalBottomSheet(
                  context: context,
                  builder: (_) => _buildAccountPicker(),
                );
              },
            ),
            ListTile(
              leading: const Icon(Icons.home),
              title: const Text('Home'),
              onTap: () => Navigator.pop(context),
            ),
            ListTile(
              leading: const Icon(Icons.settings),
              title: const Text('Settings'),
              onTap: () => Navigator.pop(context),
            ),
            const Divider(),
            ListTile(
              leading: const Icon(Icons.logout),
              title: const Text('Logout'),
              onTap: () => Navigator.pop(context),
            ),
          ],
        ),
      ),
      body: Center(
        child: Text('Logged in as ${currentAccount.email}'),
      ),
    );
  }

  Widget _buildAccountPicker() {
    return Column(
      mainAxisSize: MainAxisSize.min,
      children: [
        const Padding(
          padding: EdgeInsets.all(16),
          child: Text('Switch Account', style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
        ),
        ...accounts.asMap().entries.map((e) {
          final isSelected = e.key == currentAccountIndex;
          return ListTile(
            leading: CircleAvatar(backgroundImage: NetworkImage(e.value.avatarUrl)),
            title: Text(e.value.name),
            subtitle: Text(e.value.email),
            trailing: isSelected ? const Icon(Icons.check, color: Colors.green) : null,
            onTap: () {
              Navigator.pop(context);
              switchAccount(e.key);
            },
          );
        }),
        const SizedBox(height: 16),
      ],
    );
  }
}

// Key concepts:
// - UserAccountsDrawerHeader: Built-in account header
// - currentAccountPicture: Main avatar
// - otherAccountsPictures: Secondary avatars for switching
// - onDetailsPressed: Tap handler for expand arrow
// - BottomSheet: Full account picker modal