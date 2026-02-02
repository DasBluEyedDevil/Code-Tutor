---
type: "EXAMPLE"
title: "Section 8: Best Practices"
---


### 1. Use One Seed Color for Consistency

**Good:**

### 2. Extract Theme to Separate File


### 3. Always Use Theme Colors, Never Hardcode

**Bad:**

**Good:**

### 4. Use Material 3 Color Roles

Material 3 provides semantic color roles:
- `primary` - Main brand actions
- `secondary` - Less prominent actions
- `tertiary` - Complementary accents
- `error` - Errors and warnings
- `surface` - Card and sheet backgrounds
- `onPrimary`, `onSecondary`, etc. - Text on those colors

Use these instead of arbitrary colors!

### 5. Test Both Light and Dark Themes

Always test your app in both themes:



```dart
// In main.dart, temporarily force dark mode for testing
themeMode: ThemeMode.dark,  // Change to test
```
