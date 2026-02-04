---
type: "THEORY"
title: "Material 3: NavigationDrawer (Modern Approach)"
---



**NavigationDrawer advantages:**
- Material 3 design
- Built-in selection state
- Better animations
- Supports badges
- More accessible



```dart
import 'package:flutter/material.dart';

void main() => runApp(MaterialApp(
  theme: ThemeData(),
  home: ModernDrawerExample(),
));

class ModernDrawerExample extends StatefulWidget {
  @override
  _ModernDrawerExampleState createState() => _ModernDrawerExampleState();
}

class _ModernDrawerExampleState extends State<ModernDrawerExample> {
  int _selectedIndex = 0;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Modern Drawer')),
      drawer: NavigationDrawer(
        selectedIndex: _selectedIndex,
        onDestinationSelected: (index) {
          setState(() {
            _selectedIndex = index;
          });
          Navigator.pop(context);  // Close drawer
        },
        children: [
          Padding(
            padding: EdgeInsets.all(16),
            child: Text('Menu', style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold)),
          ),
          NavigationDrawerDestination(
            icon: Icon(Icons.home_outlined),
            selectedIcon: Icon(Icons.home),
            label: Text('Home'),
          ),
          NavigationDrawerDestination(
            icon: Icon(Icons.favorite_outline),
            selectedIcon: Icon(Icons.favorite),
            label: Text('Favorites'),
          ),
          NavigationDrawerDestination(
            icon: Icon(Icons.settings_outlined),
            selectedIcon: Icon(Icons.settings),
            label: Text('Settings'),
          ),
          Divider(),
          NavigationDrawerDestination(
            icon: Icon(Icons.logout),
            label: Text('Logout'),
          ),
        ],
      ),
      body: Center(
        child: Text('Selected: ${_getPageName(_selectedIndex)}',
          style: TextStyle(fontSize: 24)),
      ),
    );
  }

  String _getPageName(int index) {
    switch (index) {
      case 0: return 'Home';
      case 1: return 'Favorites';
      case 2: return 'Settings';
      case 3: return 'Logout';
      default: return 'Unknown';
    }
  }
}
```
