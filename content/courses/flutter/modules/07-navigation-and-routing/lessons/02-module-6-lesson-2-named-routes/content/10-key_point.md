---
type: KEY_POINT
---

- Define all routes in `MaterialApp.routes` as a `Map<String, WidgetBuilder>` for centralized navigation management
- Navigate with `Navigator.pushNamed(context, '/details')` instead of constructing routes inline everywhere
- Pass arguments via `Navigator.pushNamed(context, '/details', arguments: item)` and retrieve with `ModalRoute.of(context)!.settings.arguments`
- `onGenerateRoute` handles dynamic routes and type-safe argument parsing when the simple routes map is not flexible enough
- Named routes improve maintainability in large apps but GoRouter (next lesson) is the modern recommended approach
