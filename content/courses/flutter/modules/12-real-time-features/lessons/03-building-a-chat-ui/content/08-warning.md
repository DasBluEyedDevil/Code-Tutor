---
type: WARNING
---

**StreamSubscription leaks cause memory leaks and duplicate messages.** Every `stream.listen()` call creates a `StreamSubscription` that continues receiving data even after the widget is removed from the tree. If you navigate away and back, a new subscription is created while the old one keeps running.

```dart
// WRONG - subscription never cancelled
@override
void initState() {
  super.initState();
  messageStream.listen((msg) => setState(() => messages.add(msg)));
}

// RIGHT - cancel in dispose
late StreamSubscription _subscription;

@override
void initState() {
  super.initState();
  _subscription = messageStream.listen((msg) => setState(() => messages.add(msg)));
}

@override
void dispose() {
  _subscription.cancel();
  super.dispose();
}
```

For every `listen()` call, store the returned `StreamSubscription` and cancel it in `dispose()`. When using Riverpod, prefer `ref.watch()` with a `StreamProvider` -- it handles subscription lifecycle automatically.
