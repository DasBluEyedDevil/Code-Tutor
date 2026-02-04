---
type: "EXAMPLE"
title: "Your First TabBar"
---



**How it works:**
1. **DefaultTabController**: Manages tab state automatically
2. **TabBar**: Shows the tabs (usually in AppBar bottom)
3. **TabBarView**: Shows content for each tab
4. **Swipe to switch** tabs - built-in gesture support!



```dart
import 'package:flutter/material.dart';

void main() => runApp(MaterialApp(
  theme: ThemeData(),
  home: TabBarExample(),
));

class TabBarExample extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 3,  // Number of tabs
      child: Scaffold(
        appBar: AppBar(
          title: Text('Tabs Demo'),
          bottom: TabBar(
            tabs: [
              Tab(icon: Icon(Icons.home), text: 'Home'),
              Tab(icon: Icon(Icons.star), text: 'Favorites'),
              Tab(icon: Icon(Icons.person), text: 'Profile'),
            ],
          ),
        ),
        body: TabBarView(
          children: [
            Center(child: Text('Home Tab', style: TextStyle(fontSize: 24))),
            Center(child: Text('Favorites Tab', style: TextStyle(fontSize: 24))),
            Center(child: Text('Profile Tab', style: TextStyle(fontSize: 24))),
          ],
        ),
      ),
    );
  }
}
```
