// Nested Tabs Challenge
// Create tabs within tabs for a settings screen

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

class SettingsScreen extends StatefulWidget {
  const SettingsScreen({super.key});

  @override
  State<SettingsScreen> createState() => _SettingsScreenState();
}

class _SettingsScreenState extends State<SettingsScreen>
    with SingleTickerProviderStateMixin {
  // TODO 1: Create TabController for main tabs
  late TabController _tabController;

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 3, vsync: this);
  }

  @override
  void dispose() {
    _tabController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Settings'),
        bottom: TabBar(
          controller: _tabController,
          tabs: const [
            Tab(text: 'General'),
            Tab(text: 'Display'),
            Tab(text: 'Notifications'),
          ],
        ),
      ),
      body: TabBarView(
        controller: _tabController,
        children: const [
          // TODO 2: Create nested tabs for each section
          GeneralSettingsTab(),
          DisplaySettingsTab(),
          NotificationsSettingsTab(),
        ],
      ),
    );
  }
}

// TODO 3: Create GeneralSettingsTab with nested Account/Privacy tabs
class GeneralSettingsTab extends StatefulWidget {
  const GeneralSettingsTab({super.key});

  @override
  State<GeneralSettingsTab> createState() => _GeneralSettingsTabState();
}

class _GeneralSettingsTabState extends State<GeneralSettingsTab>
    with SingleTickerProviderStateMixin {
  late TabController _nestedController;

  @override
  void initState() {
    super.initState();
    _nestedController = TabController(length: 2, vsync: this);
  }

  @override
  void dispose() {
    _nestedController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        TabBar(
          controller: _nestedController,
          labelColor: Theme.of(context).colorScheme.primary,
          tabs: const [
            Tab(text: 'Account'),
            Tab(text: 'Privacy'),
          ],
        ),
        Expanded(
          child: TabBarView(
            controller: _nestedController,
            children: const [
              Center(child: Text('Account Settings')),
              Center(child: Text('Privacy Settings')),
            ],
          ),
        ),
      ],
    );
  }
}

// TODO 4: Create DisplaySettingsTab with Theme/Font tabs
class DisplaySettingsTab extends StatelessWidget {
  const DisplaySettingsTab({super.key});

  @override
  Widget build(BuildContext context) {
    // Implement similar to GeneralSettingsTab
    return const Center(child: Text('Display - Add nested tabs: Theme, Font'));
  }
}

// TODO 5: Create NotificationsSettingsTab with Email/Push/SMS tabs
class NotificationsSettingsTab extends StatelessWidget {
  const NotificationsSettingsTab({super.key});

  @override
  Widget build(BuildContext context) {
    // Implement similar but with 3 tabs
    return const Center(child: Text('Notifications - Add nested tabs: Email, Push, SMS'));
  }
}