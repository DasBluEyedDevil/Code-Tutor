// Solution: Dark Theme Bottom Navigation with Animations
// Custom styled BottomNavigationBar with animated transitions

import 'package:flutter/material.dart';

void main() {
  runApp(const DarkNavApp());
}

class DarkNavApp extends StatelessWidget {
  const DarkNavApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      theme: ThemeData.dark().copyWith(
        scaffoldBackgroundColor: const Color(0xFF1A1A2E),
        colorScheme: const ColorScheme.dark(
          primary: Color(0xFF00D9FF),
          secondary: Color(0xFFE94560),
          surface: Color(0xFF16213E),
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

  final List<Widget> _screens = const [
    ScreenPlaceholder(title: 'Home', icon: Icons.home),
    ScreenPlaceholder(title: 'Search', icon: Icons.search),
    ScreenPlaceholder(title: 'Favorites', icon: Icons.favorite),
    ScreenPlaceholder(title: 'Profile', icon: Icons.person),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: AnimatedSwitcher(
        duration: const Duration(milliseconds: 300),
        transitionBuilder: (child, animation) {
          return FadeTransition(
            opacity: animation,
            child: SlideTransition(
              position: Tween<Offset>(
                begin: const Offset(0.0, 0.1),
                end: Offset.zero,
              ).animate(animation),
              child: child,
            ),
          );
        },
        child: KeyedSubtree(
          key: ValueKey(_currentIndex),
          child: _screens[_currentIndex],
        ),
      ),
      bottomNavigationBar: Container(
        decoration: BoxDecoration(
          color: const Color(0xFF16213E),
          boxShadow: [
            BoxShadow(
              color: const Color(0xFF00D9FF).withOpacity(0.2),
              blurRadius: 20,
              offset: const Offset(0, -5),
            ),
          ],
        ),
        child: BottomNavigationBar(
          currentIndex: _currentIndex,
          onTap: (index) => setState(() => _currentIndex = index),
          type: BottomNavigationBarType.fixed,
          backgroundColor: Colors.transparent,
          elevation: 0,
          selectedItemColor: const Color(0xFF00D9FF),
          unselectedItemColor: Colors.grey,
          selectedLabelStyle: const TextStyle(fontWeight: FontWeight.bold),
          items: [
            _buildNavItem(Icons.home, 'Home', 0),
            _buildNavItem(Icons.search, 'Search', 1),
            _buildNavItem(Icons.favorite, 'Favorites', 2),
            _buildNavItem(Icons.person, 'Profile', 3),
          ],
        ),
      ),
    );
  }

  BottomNavigationBarItem _buildNavItem(IconData icon, String label, int index) {
    final isSelected = _currentIndex == index;
    return BottomNavigationBarItem(
      icon: AnimatedContainer(
        duration: const Duration(milliseconds: 200),
        padding: EdgeInsets.all(isSelected ? 8 : 4),
        decoration: BoxDecoration(
          color: isSelected ? const Color(0xFF00D9FF).withOpacity(0.2) : Colors.transparent,
          borderRadius: BorderRadius.circular(12),
        ),
        child: Icon(icon),
      ),
      label: label,
    );
  }
}

class ScreenPlaceholder extends StatelessWidget {
  final String title;
  final IconData icon;

  const ScreenPlaceholder({super.key, required this.title, required this.icon});

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(icon, size: 64, color: const Color(0xFF00D9FF)),
          const SizedBox(height: 16),
          Text(
            title,
            style: const TextStyle(fontSize: 24, color: Colors.white),
          ),
        ],
      ),
    );
  }
}

// Key concepts:
// - ThemeData.dark(): Dark theme base
// - Custom ColorScheme for brand colors
// - AnimatedSwitcher: Smooth page transitions
// - AnimatedContainer: Animated icon backgrounds
// - BoxShadow with color: Glowing effect
// - type: BottomNavigationBarType.fixed for equal spacing