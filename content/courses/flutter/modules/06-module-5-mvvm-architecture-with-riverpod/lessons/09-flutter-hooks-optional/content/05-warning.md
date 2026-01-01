---
type: "WARNING"
title: "Hooks Rules"
---

Hooks have strict rules. Breaking them causes bugs that are hard to debug.

### Rule 1: Only Call Hooks at the Top of build()

```dart
// CORRECT: Hooks at top level
class MyWidget extends HookWidget {
  @override
  Widget build(BuildContext context) {
    final controller = useTextEditingController();  // OK
    final counter = useState(0);  // OK
    final focus = useFocusNode();  // OK

    return TextField(controller: controller);
  }
}

// WRONG: Hook inside a condition
class BadWidget extends HookWidget {
  final bool showSearch;
  BadWidget({required this.showSearch});

  @override
  Widget build(BuildContext context) {
    // BAD! Hook count changes based on showSearch
    if (showSearch) {
      final controller = useTextEditingController();  // WRONG!
    }

    return Container();
  }
}
```

**Why?** Flutter tracks hooks by their order of calls. If the order changes between builds, hooks get mixed up and your app breaks.

### Rule 2: Do NOT Call Hooks in Loops

```dart
// WRONG: Hook in a loop
class BadWidget extends HookWidget {
  final List<String> items;
  BadWidget({required this.items});

  @override
  Widget build(BuildContext context) {
    // BAD! Number of hooks changes with items.length
    for (final item in items) {
      final controller = useTextEditingController(text: item);  // WRONG!
    }
    return Container();
  }
}

// CORRECT: Use a single hook with a list
class GoodWidget extends HookWidget {
  final List<String> items;
  GoodWidget({required this.items});

  @override
  Widget build(BuildContext context) {
    // Create a list of controllers with useMemoized
    final controllers = useMemoized(
      () => items.map((item) => TextEditingController(text: item)).toList(),
      [items.length],
    );

    // Dispose them manually with useEffect
    useEffect(() {
      return () => controllers.forEach((c) => c.dispose());
    }, [controllers]);

    return Column(
      children: controllers.map((c) => TextField(controller: c)).toList(),
    );
  }
}
```

### Rule 3: Do NOT Call Hooks in Callbacks

```dart
// WRONG: Hook in onPressed
class BadWidget extends HookWidget {
  @override
  Widget build(BuildContext context) {
    return ElevatedButton(
      onPressed: () {
        final counter = useState(0);  // WRONG! Called outside build()
      },
      child: Text('Tap'),
    );
  }
}

// CORRECT: Define hook at top, use in callback
class GoodWidget extends HookWidget {
  @override
  Widget build(BuildContext context) {
    final counter = useState(0);  // OK: at top of build()

    return ElevatedButton(
      onPressed: () {
        counter.value++;  // OK: using the hook's value
      },
      child: Text('Count: ${counter.value}'),
    );
  }
}
```

### Rule 4: Do NOT Call Hooks in Nested Functions

```dart
// WRONG: Hook in helper function
class BadWidget extends HookWidget {
  @override
  Widget build(BuildContext context) {
    Widget buildHeader() {
      final title = useState('Header');  // WRONG!
      return Text(title.value);
    }

    return Column(
      children: [
        buildHeader(),
        Text('Body'),
      ],
    );
  }
}

// CORRECT: Pass values to helper functions
class GoodWidget extends HookWidget {
  @override
  Widget build(BuildContext context) {
    final title = useState('Header');  // OK: at top of build()

    Widget buildHeader(String titleText) {
      return Text(titleText);
    }

    return Column(
      children: [
        buildHeader(title.value),  // Pass value in
        Text('Body'),
      ],
    );
  }
}
```

### Summary of Rules

| Do | Do Not |
|---|---|
| Call hooks at top of build() | Call hooks conditionally |
| Call hooks in same order every build | Call hooks in loops |
| Use hook values in callbacks | Call hooks in callbacks |
| Pass values to helper functions | Call hooks in nested functions |