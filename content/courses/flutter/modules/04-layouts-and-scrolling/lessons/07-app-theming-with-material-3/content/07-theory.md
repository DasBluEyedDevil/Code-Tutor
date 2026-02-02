---
type: "THEORY"
title: "Section 5: Component-Specific Theming"
---


### Customizing Button Themes


### Customizing AppBar Theme


### Customizing Input Decoration Theme


### Customizing Card Theme




```dart
ThemeData(
  colorScheme: ColorScheme.fromSeed(seedColor: Colors.purple),

  cardTheme: CardTheme(
    elevation: 4,
    shape: RoundedRectangleBorder(
      borderRadius: BorderRadius.circular(16),
    ),
    margin: const EdgeInsets.all(8),
  ),
)
```
