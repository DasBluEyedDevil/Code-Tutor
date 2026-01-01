---
type: "THEORY"
title: "Adaptive Widgets Pattern - One API, Platform-Specific Rendering"
---


**The Adaptive Pattern:**

Create wrapper widgets that internally choose Material or Cupertino based on platform:

```dart
class AdaptiveButton extends StatelessWidget {
  final VoidCallback onPressed;
  final Widget child;
  
  @override
  Widget build(BuildContext context) {
    if (Theme.of(context).platform == TargetPlatform.iOS) {
      return CupertinoButton.filled(
        onPressed: onPressed,
        child: child,
      );
    }
    return ElevatedButton(
      onPressed: onPressed,
      child: child,
    );
  }
}
```

**Benefits:**

- **Single codebase** - Write once, looks native everywhere
- **Consistent API** - Same props for all platforms
- **Easy testing** - Can override platform in tests
- **Gradual adoption** - Replace widgets one at a time

**What to Make Adaptive:**

| Component | Why Adapt |
|-----------|----------|
| Buttons | Different styles and behaviors |
| Dialogs | Alert vs Action Sheet patterns |
| Navigation | AppBar vs NavigationBar |
| Switches | Toggle appearance differs |
| Date/Time Pickers | Completely different UX |
| Scrolling | iOS has bounce, Android has glow |

