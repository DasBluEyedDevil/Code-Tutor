---
type: KEY_POINT
---

- `StatefulWidget` creates a companion `State` object that holds mutable data and persists across rebuilds
- Call `setState(() { ... })` to modify state variables and trigger a UI rebuild -- never modify state without it
- `initState()` runs once when the widget is inserted; `dispose()` runs when removed -- use them for setup and cleanup
- Keep state as close to where it is used as possible; lift state up only when multiple widgets need to share it
- `StatelessWidget` is for UI that never changes after construction; convert to `StatefulWidget` only when you need mutable state
