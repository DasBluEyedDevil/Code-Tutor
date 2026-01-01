---
type: "THEORY"
title: "Section 4: Customizing TextTheme"
---


### Understanding TextTheme Styles

TextTheme provides predefined styles for different text purposes:

| Style | Purpose | Example |
|-------|---------|---------|
| `displayLarge` | Very large headlines | "Welcome" on splash screen |
| `headlineLarge` | Section headers | "Settings" title |
| `titleLarge` | Card titles | "New Message" dialog title |
| `bodyLarge` | Main content | Article text |
| `bodyMedium` | Default body text | Paragraph text |
| `labelLarge` | Button text | "SUBMIT" button |

### Custom TextTheme Example


### Using Custom Fonts





```dart
ThemeData(
  colorScheme: ColorScheme.fromSeed(seedColor: Colors.purple),

  textTheme: const TextTheme(
    headlineLarge: TextStyle(
      fontFamily: 'Poppins',
      fontSize: 32,
      fontWeight: FontWeight.bold,
    ),
    bodyLarge: TextStyle(
      fontFamily: 'Poppins',
      fontSize: 16,
    ),
  ),
)
```
