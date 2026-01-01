---
type: "THEORY"
title: "Core Concepts"
---

Before writing code, let us understand the four core building blocks of Riverpod:

### 1. Provider
A **provider** is a container for a piece of state. Think of it as a box that holds a value and lets others read that value.

```dart
// This provider holds a String value
final greetingProvider = Provider<String>((ref) {
  return 'Hello, World!';
});
```

Providers are:
- **Global constants** - defined at the top level of your files
- **Lazy** - they do not create their value until someone reads them
- **Cached** - once created, the value is reused

### 2. ref
The **ref** object is how you interact with providers. It is your access pass to read, watch, or listen to any provider.

```dart
// Inside another provider
final combinedProvider = Provider<String>((ref) {
  final greeting = ref.watch(greetingProvider);  // ref lets you access other providers
  return '$greeting from Riverpod';
});
```

### 3. ProviderScope
**ProviderScope** is the container that holds all your providers. It must wrap your entire app. Think of it as the root of a tree where all providers live.

```dart
void main() {
  runApp(
    ProviderScope(  // This enables Riverpod for your entire app
      child: MyApp(),
    ),
  );
}
```

### 4. ConsumerWidget / Consumer
To read providers in your widgets, you use **ConsumerWidget** (instead of StatelessWidget) or the **Consumer** builder.

```dart
// ConsumerWidget gives you access to ref in the build method
class MyScreen extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final value = ref.watch(myProvider);  // Now you can access providers!
    return Text(value);
  }
}
```