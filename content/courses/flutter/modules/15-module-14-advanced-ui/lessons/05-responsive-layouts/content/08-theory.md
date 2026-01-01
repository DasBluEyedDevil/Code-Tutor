---
type: "THEORY"
title: "Adaptive Grids - GridView and Slivers"
---


**Grids are essential for responsive layouts.** Different screen sizes call for different column counts.

**GridView Options:**

| Constructor | Use Case |
|-------------|----------|
| `GridView.count` | Fixed column count |
| `GridView.extent` | Max item width, auto columns |
| `GridView.builder` | Lazy loading with delegate |
| `GridView.custom` | Full control with SliverGridDelegate |

**SliverGridDelegates:**

| Delegate | Description |
|----------|-------------|
| `SliverGridDelegateWithFixedCrossAxisCount` | Fixed number of columns |
| `SliverGridDelegateWithMaxCrossAxisExtent` | Max item width, auto columns |

**Auto-calculating Columns:**

```dart
// Fixed columns based on breakpoint
int getColumnCount(double width) {
  if (width < 600) return 2;
  if (width < 900) return 3;
  return 4;
}

// Or use maxCrossAxisExtent for automatic calculation
GridView.builder(
  gridDelegate: const SliverGridDelegateWithMaxCrossAxisExtent(
    maxCrossAxisExtent: 200, // Each item max 200px wide
    mainAxisSpacing: 16,
    crossAxisSpacing: 16,
  ),
  itemBuilder: (context, index) => YourWidget(),
)
```

