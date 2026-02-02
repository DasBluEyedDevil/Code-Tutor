---
type: "THEORY"
title: "End Drawer (Right Side)"
---



**Use endDrawer for:**
- Filters
- Settings panels
- Secondary actions
- Right-to-left language support



```dart
Scaffold(
  appBar: AppBar(
    title: Text('End Drawer'),
    // No hamburger icon on left
  ),
  endDrawer: Drawer(  // Opens from right!
    child: ListView(
      children: [
        DrawerHeader(
          child: Text('Filter Options'),
        ),
        CheckboxListTile(
          title: Text('Option 1'),
          value: true,
          onChanged: (value) {},
        ),
        CheckboxListTile(
          title: Text('Option 2'),
          value: false,
          onChanged: (value) {},
        ),
      ],
    ),
  ),
  body: Center(child: Text('Main Content')),
)
```
