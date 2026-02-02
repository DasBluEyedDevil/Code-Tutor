// Solution: Nested TabBars for Settings
// Main tabs with sub-tabs inside each section

import 'package:flutter/material.dart';

void main() {
  runApp(const NestedTabsApp());
}

class NestedTabsApp extends StatelessWidget {
  const NestedTabsApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      theme: ThemeData(useMaterial3: true),
      home: const SettingsScreen(),
    );
  }
}

class SettingsScreen extends StatelessWidget {
  const SettingsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 3,
      child: Scaffold(
        appBar: AppBar(
          title: const Text('Settings'),
          bottom: const TabBar(
            tabs: [
              Tab(text: 'General', icon: Icon(Icons.settings)),
              Tab(text: 'Display', icon: Icon(Icons.palette)),
              Tab(text: 'Notifications', icon: Icon(Icons.notifications)),
            ],
          ),
        ),
        body: const TabBarView(
          children: [
            GeneralTab(),
            DisplayTab(),
            NotificationsTab(),
          ],
        ),
      ),
    );
  }
}

// General Tab with nested Account/Privacy tabs
class GeneralTab extends StatelessWidget {
  const GeneralTab({super.key});

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 2,
      child: Column(
        children: [
          const TabBar(
            labelColor: Colors.blue,
            tabs: [
              Tab(text: 'Account'),
              Tab(text: 'Privacy'),
            ],
          ),
          Expanded(
            child: TabBarView(
              children: [
                _buildSettingsList(['Username', 'Email', 'Password', 'Phone']),
                _buildSettingsList(['Profile Visibility', 'Online Status', 'Read Receipts']),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

// Display Tab with nested Theme/Font tabs
class DisplayTab extends StatelessWidget {
  const DisplayTab({super.key});

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 2,
      child: Column(
        children: [
          const TabBar(
            labelColor: Colors.blue,
            tabs: [
              Tab(text: 'Theme'),
              Tab(text: 'Font'),
            ],
          ),
          Expanded(
            child: TabBarView(
              children: [
                _buildSettingsList(['Light Mode', 'Dark Mode', 'Auto']),
                _buildSettingsList(['Font Size', 'Font Family', 'Bold Text']),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

// Notifications Tab with nested Email/Push/SMS tabs
class NotificationsTab extends StatelessWidget {
  const NotificationsTab({super.key});

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 3,
      child: Column(
        children: [
          const TabBar(
            labelColor: Colors.blue,
            tabs: [
              Tab(text: 'Email'),
              Tab(text: 'Push'),
              Tab(text: 'SMS'),
            ],
          ),
          Expanded(
            child: TabBarView(
              children: [
                _buildSettingsList(['Newsletters', 'Promotions', 'Updates']),
                _buildSettingsList(['Messages', 'Likes', 'Comments', 'Follows']),
                _buildSettingsList(['Security Alerts', 'Login Codes']),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

Widget _buildSettingsList(List<String> items) {
  return ListView.builder(
    itemCount: items.length,
    itemBuilder: (context, index) {
      return SwitchListTile(
        title: Text(items[index]),
        value: index % 2 == 0,
        onChanged: (value) {},
      );
    },
  );
}

// Key concepts:
// - DefaultTabController: Manages tab state
// - Nested TabControllers: Each section has its own
// - TabBar + TabBarView: Tab header and content
// - Column with TabBar + Expanded TabBarView: Nested layout
// - Each tab level independent of others