---
type: "THEORY"
title: "Material 3: Tab Alignment"
---



**TabAlignment options:**
- `TabAlignment.start` - Left-aligned
- `TabAlignment.startOffset` - Left-aligned with 52px offset (default for scrollable)
- `TabAlignment.center` - Centered
- `TabAlignment.fill` - Stretch to fill width




```dart
// Example: Scrollable tabs with custom alignment
TabBar(
  isScrollable: true,
  tabAlignment: TabAlignment.center,
  tabs: [
    Tab(text: 'Technology'),
    Tab(text: 'Sports'),
    Tab(text: 'Entertainment'),
    Tab(text: 'Politics'),
    Tab(text: 'Science'),
    Tab(text: 'Health'),
  ],
)
```
