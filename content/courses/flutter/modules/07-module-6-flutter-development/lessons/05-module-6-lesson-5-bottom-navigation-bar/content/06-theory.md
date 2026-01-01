---
type: "THEORY"
title: "Adding Badges (Notification Counts)"
---



**Conditional badge:**



```dart
NavigationDestination(
  icon: Badge(
    isLabelVisible: notificationCount > 0,
    label: Text('$notificationCount'),
    child: Icon(Icons.notifications_outlined),
  ),
  label: 'Notifications',
),
```
