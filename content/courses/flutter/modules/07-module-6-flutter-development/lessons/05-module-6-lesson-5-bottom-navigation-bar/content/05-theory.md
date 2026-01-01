---
type: "THEORY"
title: "IndexedStack vs Switching Widgets"
---


**Two approaches for showing pages:**

### Approach 1: Direct Switching (Simple)

**Pros:** Simple, uses less memory
**Cons:** Rebuilds page each time, loses scroll position

### Approach 2: IndexedStack (Better)

**Pros:** Preserves state, keeps scroll position, smooth transitions
**Cons:** Uses more memory (all pages stay in memory)

**Best practice:** Use IndexedStack for better UX!



```dart
body: IndexedStack(
  index: _currentIndex,
  children: _pages,
),
```
