---
type: "KEY_POINT"
title: "MVVM Rules To Remember"
---

Following these rules strictly will keep your architecture clean:

### Rule 1: View Never Calls APIs Directly
The View should NEVER import http, dio, or any networking library. If you need data from an API, the View asks the ViewModel, which asks a Repository.

**Bad:**
```dart
// In a widget
onPressed: () async {
  final response = await http.get(Uri.parse('api/data'));
  setState(() => data = response.body);
}
```

**Good:**
```dart
// In a widget
onPressed: () {
  ref.read(myViewModelProvider.notifier).fetchData();
}
```

### Rule 2: ViewModel Does Not Import Flutter Widgets
The ViewModel should have NO knowledge of Flutter UI. It should not import `package:flutter/material.dart`. It only knows about state and data.

**Bad:**
```dart
class MyViewModel extends Notifier<MyState> {
  void showError() {
    // ViewModel trying to show UI - WRONG!
    ScaffoldMessenger.of(context).showSnackBar(...);
  }
}
```

**Good:**
```dart
class MyViewModel extends Notifier<MyState> {
  void processAction() {
    // ViewModel updates state - View reacts to state
    state = state.copyWith(errorMessage: 'Something went wrong');
  }
}
// View watches errorMessage and shows SnackBar when it changes
```

### Rule 3: Model Is Pure Dart
Model classes should be plain Dart. No Flutter, no Riverpod, no external dependencies. They should be so simple that you could use them in a Dart CLI app.

### Rule 4: State Is Immutable
Never modify state directly. Always create new state objects. This makes tracking changes easy and prevents bugs.

**Bad:**
```dart
state.items.add(newItem);  // Mutating existing state!
```

**Good:**
```dart
state = state.copyWith(items: [...state.items, newItem]);
```