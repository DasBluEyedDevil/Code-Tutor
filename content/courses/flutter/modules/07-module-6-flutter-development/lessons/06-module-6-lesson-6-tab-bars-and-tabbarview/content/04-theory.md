---
type: "THEORY"
title: "Manual TabController (Advanced Control)"
---


For more control (animations, programmatic switching):


**When to use TabController:**
- Need to listen to tab changes
- Want to programmatically switch tabs
- Need custom animations
- Multiple TabBars synchronized



```dart
import 'package:flutter/material.dart';

class ManualTabController extends StatefulWidget {
  @override
  _ManualTabControllerState createState() => _ManualTabControllerState();
}

class _ManualTabControllerState extends State<ManualTabController>
    with SingleTickerProviderStateMixin {
  late TabController _tabController;

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 3, vsync: this);

    // Listen to tab changes
    _tabController.addListener(() {
      if (!_tabController.indexIsChanging) {
        print('Current tab: ${_tabController.index}');
      }
    });
  }

  @override
  void dispose() {
    _tabController.dispose();  // Important: Clean up!
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Manual Controller'),
        bottom: TabBar(
          controller: _tabController,
          tabs: [
            Tab(text: 'Home'),
            Tab(text: 'Search'),
            Tab(text: 'Profile'),
          ],
        ),
      ),
      body: TabBarView(
        controller: _tabController,
        children: [
          Center(child: Text('Home')),
          Center(child: Text('Search')),
          Center(child: Text('Profile')),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          // Programmatically switch to next tab
          int nextIndex = (_tabController.index + 1) % 3;
          _tabController.animateTo(nextIndex);
        },
        child: Icon(Icons.arrow_forward),
      ),
    );
  }
}
```
