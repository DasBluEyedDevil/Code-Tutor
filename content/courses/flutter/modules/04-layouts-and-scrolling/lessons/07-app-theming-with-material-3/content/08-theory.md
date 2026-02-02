---
type: "THEORY"
title: "Section 6: Accessing Theme Data"
---


### Using Theme.of(context)


### Common Theme Properties




```dart
// Colors
Theme.of(context).colorScheme.primary
Theme.of(context).colorScheme.secondary
Theme.of(context).colorScheme.surface
Theme.of(context).colorScheme.error
Theme.of(context).colorScheme.onPrimary  // Text color on primary background

// Text styles
Theme.of(context).textTheme.headlineLarge
Theme.of(context).textTheme.bodyLarge
Theme.of(context).textTheme.labelLarge

// Check if dark mode
Theme.of(context).brightness == Brightness.dark
```
