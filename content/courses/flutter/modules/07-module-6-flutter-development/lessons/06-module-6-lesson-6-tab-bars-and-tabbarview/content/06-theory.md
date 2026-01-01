---
type: "THEORY"
title: "Scrollable Tabs (Many Categories)"
---


When you have many tabs:


**Use scrollable when:**
- More than 4-5 tabs
- Tab labels are long
- Screen size varies (responsive design)



```dart
TabBar(
  isScrollable: true,  // Tabs can scroll horizontally
  tabs: [
    Tab(text: 'All'),
    Tab(text: 'Technology'),
    Tab(text: 'Sports'),
    Tab(text: 'Entertainment'),
    Tab(text: 'Politics'),
    Tab(text: 'Science'),
    Tab(text: 'Health'),
    Tab(text: 'Business'),
    Tab(text: 'Travel'),
  ],
)
```
