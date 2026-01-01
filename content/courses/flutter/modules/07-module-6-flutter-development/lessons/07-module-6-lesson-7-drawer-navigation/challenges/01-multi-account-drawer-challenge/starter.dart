// Multi-Account Drawer Challenge
// Create an account-switching drawer

import 'package:flutter/material.dart';

void main() {
  runApp(const AccountSwitcherApp());
}

class AccountSwitcherApp extends StatelessWidget {
  const AccountSwitcherApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      theme: ThemeData(useMaterial3: true),
      home: const HomeScreen(),
    );
  }
}

// Account model
class Account {
  final String name;
  final String email;
  final Color avatarColor;

  const Account({
    required this.name,
    required this.email,
    required this.avatarColor,
  });
}

class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  // TODO 1: Define list of accounts
  final List<Account> accounts = const [
    Account(name: 'John Doe', email: 'john@example.com', avatarColor: Colors.blue),
    Account(name: 'Jane Smith', email: 'jane@example.com', avatarColor: Colors.green),
    Account(name: 'Bob Wilson', email: 'bob@example.com', avatarColor: Colors.orange),
  ];

  int _currentAccountIndex = 0;

  Account get currentAccount => accounts[_currentAccountIndex];

  void _switchAccount(int index) {
    setState(() {
      _currentAccountIndex = index;
    });
    Navigator.pop(context); // Close drawer
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Welcome, ${currentAccount.name.split(' ').first}'),
      ),
      drawer: Drawer(
        child: ListView(
          padding: EdgeInsets.zero,
          children: [
            // TODO 2: Create UserAccountsDrawerHeader
            UserAccountsDrawerHeader(
              accountName: Text(currentAccount.name),
              accountEmail: Text(currentAccount.email),
              currentAccountPicture: CircleAvatar(
                backgroundColor: currentAccount.avatarColor,
                child: Text(
                  currentAccount.name[0],
                  style: const TextStyle(fontSize: 24, color: Colors.white),
                ),
              ),
              // TODO 3: Add otherAccountsPictures for switching
              otherAccountsPictures: [
                for (int i = 0; i < accounts.length; i++)
                  if (i != _currentAccountIndex)
                    GestureDetector(
                      onTap: () => _switchAccount(i),
                      child: CircleAvatar(
                        backgroundColor: accounts[i].avatarColor,
                        child: Text(
                          accounts[i].name[0],
                          style: const TextStyle(color: Colors.white),
                        ),
                      ),
                    ),
              ],
              decoration: BoxDecoration(
                color: currentAccount.avatarColor,
              ),
            ),
            // TODO 4: Add navigation items
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
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            CircleAvatar(
              radius: 50,
              backgroundColor: currentAccount.avatarColor,
              child: Text(
                currentAccount.name[0],
                style: const TextStyle(fontSize: 40, color: Colors.white),
              ),
            ),
            const SizedBox(height: 16),
            Text(
              currentAccount.name,
              style: const TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
            ),
            Text(
              currentAccount.email,
              style: TextStyle(color: Colors.grey.shade600),
            ),
          ],
        ),
      ),
    );
  }
}