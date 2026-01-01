// Dark Theme Navigation Challenge
// Build a styled dark theme bottom navigation

import 'package:flutter/material.dart';

void main() {
  runApp(const DarkNavApp());
}

class DarkNavApp extends StatelessWidget {
  const DarkNavApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      // TODO 1: Configure dark theme with custom colors
      theme: ThemeData.dark().copyWith(
        useMaterial3: true,
        colorScheme: ColorScheme.dark(
          primary: Colors.purple,
          secondary: Colors.purpleAccent,
          surface: const Color(0xFF1E1E1E),
        ),
        navigationBarTheme: NavigationBarThemeData(
          // TODO 2: Customize navigation bar colors
          backgroundColor: const Color(0xFF1E1E1E),
          indicatorColor: Colors.purple.withOpacity(0.3),
          labelTextStyle: WidgetStateProperty.all(
            const TextStyle(fontSize: 12, fontWeight: FontWeight.w500),
          ),
        ),
      ),
      home: const MainScreen(),
    );
  }
}

class MainScreen extends StatefulWidget {
  const MainScreen({super.key});

  @override
  State<MainScreen> createState() => _MainScreenState();
}

class _MainScreenState extends State<MainScreen> {
  int _currentIndex = 0;
  int _notificationCount = 3; // Example badge count

  final List<Widget> _pages = const [
    HomeTab(),
    SearchTab(),
    NotificationsTab(),
    ProfileTab(),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // TODO 3: Use IndexedStack to preserve state
      body: IndexedStack(
        index: _currentIndex,
        children: _pages,
      ),
      bottomNavigationBar: NavigationBar(
        selectedIndex: _currentIndex,
        onDestinationSelected: (index) {
          setState(() {
            _currentIndex = index;
            // Clear notification badge when viewing notifications
            if (index == 2) _notificationCount = 0;
          });
        },
        // TODO 4: Add custom animation duration
        animationDuration: const Duration(milliseconds: 400),
        destinations: [
          const NavigationDestination(
            icon: Icon(Icons.home_outlined),
            selectedIcon: Icon(Icons.home),
            label: 'Home',
          ),
          const NavigationDestination(
            icon: Icon(Icons.search),
            label: 'Search',
          ),
          // TODO 5: Add badge for notifications
          NavigationDestination(
            icon: Badge(
              isLabelVisible: _notificationCount > 0,
              label: Text('\$_notificationCount'),
              child: const Icon(Icons.notifications_outlined),
            ),
            selectedIcon: Badge(
              isLabelVisible: _notificationCount > 0,
              label: Text('\$_notificationCount'),
              child: const Icon(Icons.notifications),
            ),
            label: 'Alerts',
          ),
          const NavigationDestination(
            icon: Icon(Icons.person_outline),
            selectedIcon: Icon(Icons.person),
            label: 'Profile',
          ),
        ],
      ),
    );
  }
}

class HomeTab extends StatelessWidget {
  const HomeTab({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Home')),
      body: const Center(
        child: Text('Home Tab', style: TextStyle(fontSize: 24)),
      ),
    );
  }
}

class SearchTab extends StatelessWidget {
  const SearchTab({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Search')),
      body: const Center(
        child: Text('Search Tab', style: TextStyle(fontSize: 24)),
      ),
    );
  }
}

class NotificationsTab extends StatelessWidget {
  const NotificationsTab({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Notifications')),
      body: const Center(
        child: Text('Notifications Tab', style: TextStyle(fontSize: 24)),
      ),
    );
  }
}

class ProfileTab extends StatelessWidget {
  const ProfileTab({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Profile')),
      body: const Center(
        child: Text('Profile Tab', style: TextStyle(fontSize: 24)),
      ),
    );
  }
}