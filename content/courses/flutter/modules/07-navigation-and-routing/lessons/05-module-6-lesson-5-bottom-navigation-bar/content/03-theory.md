---
type: "THEORY"
title: "Material 3: NavigationBar (Modern Approach)"
---


Flutter's Material 3 has a newer, better widget: **NavigationBar**!


**NavigationBar advantages:**
- Modern Material 3 design
- Better animations
- Supports both outlined and filled icons
- More accessible
- Better color theming



```dart
import 'package:flutter/material.dart';

void main() => runApp(MaterialApp(
  theme: ThemeData(useMaterial3: true),  // Enable Material 3
  home: MyApp(),
));

class MyApp extends StatefulWidget {
  @override
  _MyAppState createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  int _currentIndex = 0;

  final List<Widget> _pages = [
    HomeScreen(),
    SearchScreen(),
    ProfileScreen(),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: _pages[_currentIndex],
      bottomNavigationBar: NavigationBar(
        selectedIndex: _currentIndex,
        onDestinationSelected: (index) {
          setState(() {
            _currentIndex = index;
          });
        },
        destinations: [
          NavigationDestination(
            icon: Icon(Icons.home_outlined),
            selectedIcon: Icon(Icons.home),
            label: 'Home',
          ),
          NavigationDestination(
            icon: Icon(Icons.search),
            label: 'Search',
          ),
          NavigationDestination(
            icon: Icon(Icons.person_outline),
            selectedIcon: Icon(Icons.person),
            label: 'Profile',
          ),
        ],
      ),
    );
  }
}
```
