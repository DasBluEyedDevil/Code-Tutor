---
type: "THEORY"
title: "What are Hooks?"
---

**Flutter Hooks** bring a React-inspired pattern to Flutter that lets you create reusable stateful logic without needing StatefulWidget.

### The Problem Hooks Solve

With StatefulWidget, you often write repetitive boilerplate:

```dart
class MyForm extends StatefulWidget {
  @override
  State<MyForm> createState() => _MyFormState();
}

class _MyFormState extends State<MyForm> {
  late TextEditingController _nameController;
  late TextEditingController _emailController;
  late FocusNode _nameFocus;
  late FocusNode _emailFocus;

  @override
  void initState() {
    super.initState();
    _nameController = TextEditingController();
    _emailController = TextEditingController();
    _nameFocus = FocusNode();
    _emailFocus = FocusNode();
  }

  @override
  void dispose() {
    _nameController.dispose();
    _emailController.dispose();
    _nameFocus.dispose();
    _emailFocus.dispose();
    super.dispose();
  }

  // ... build method
}
```

That is a lot of code just to manage two text fields! And if you forget to dispose something, you get memory leaks.

### How Hooks Help

With hooks, the same code becomes:

```dart
class MyForm extends HookWidget {
  @override
  Widget build(BuildContext context) {
    final nameController = useTextEditingController();
    final emailController = useTextEditingController();
    final nameFocus = useFocusNode();
    final emailFocus = useFocusNode();

    // ... rest of build method
  }
}
```

No initState, no dispose, no State class. The hooks automatically handle creation and cleanup.

### Installation

Add to pubspec.yaml:

```yaml
dependencies:
  flutter_hooks: ^0.20.5
  hooks_riverpod: ^2.5.1  # If using with Riverpod
```

### Is This Required?

No! Hooks are optional. You can build excellent Flutter apps without them. Use hooks when:
- You want less boilerplate
- You reuse stateful logic across widgets
- Your team is comfortable with the pattern

Skip hooks if:
- Your team prefers explicit StatefulWidget
- You want to minimize external dependencies
- You find hooks confusing