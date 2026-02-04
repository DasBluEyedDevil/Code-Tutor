---
type: KEY_POINT
---

- Every Flutter app starts with `main()` calling `runApp()`, which inflates your root widget and attaches it to the screen
- `MaterialApp` provides theming, navigation, and Material Design defaults -- always wrap your app in one
- `Scaffold` gives you the standard app layout with `appBar`, `body`, `floatingActionButton`, and `drawer` slots
- Everything in Flutter is a widget: text, buttons, layouts, even the app itself -- widgets compose into a tree
- The widget tree rebuilds efficiently; Flutter compares old and new trees and only repaints what changed
