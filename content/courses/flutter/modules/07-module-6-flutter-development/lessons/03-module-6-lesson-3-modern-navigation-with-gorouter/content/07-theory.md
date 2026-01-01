---
type: "THEORY"
title: "go() vs push()"
---


GoRouter provides two navigation methods:

### context.go() - Replaces Current Route

**Use for**: Main navigation where you want to replace the current screen

### context.push() - Adds to Stack

**Use for**: Modal-style navigation where you want back button to work

**Best Practice**: Prefer `go()` for most cases, use `push()` for modals/overlays.



```dart
context.push('/details');
// Stack: [Home, Details]

context.push('/settings');
// Stack: [Home, Details, Settings]  (Details is KEPT)
```
