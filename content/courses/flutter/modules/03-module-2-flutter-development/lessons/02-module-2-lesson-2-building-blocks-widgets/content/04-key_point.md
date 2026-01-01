---
type: "KEY_POINT"
title: "Why 'const' Constructors Make Your App FASTER"
---


You'll see `const` everywhere in Flutter code. Here's WHY it matters for performance:

### The Problem: Flutter Rebuilds Widgets

When state changes, Flutter rebuilds your widget tree. Every widget gets recreated:

```
📱 User taps button
    ↓
🔄 setState() called
    ↓
🏗️ build() runs again
    ↓
🧱 Every widget is recreated
    ↓
💡 Flutter compares old vs new
    ↓
🖼️ Only changed parts actually repaint
```

**Without const**, Flutter creates new widget objects EVERY rebuild:

```dart
// ❌ Creates NEW Text object every rebuild
Text('Hello')  // Object A (rebuild 1)
Text('Hello')  // Object B (rebuild 2) - DIFFERENT object!
```

**With const**, Flutter REUSES the same object:

```dart
// ✅ Same object is reused across rebuilds
const Text('Hello')  // Object A (rebuild 1)
const Text('Hello')  // Object A (rebuild 2) - SAME object!
```

### Visual: Widget Tree Rebuilds

```
🔴 = Rebuilds every time (expensive)
🟢 = Reused (free!)

Without const:        With const:
                      
   App 🔴                App 🔴
    │                     │
 Scaffold 🔴          Scaffold 🔴
    │                     │
  Column 🔴            Column 🔴
  ┌─┴─┐               ┌─┴─┐
Text🔴 Text🔴      Text🟢 Text🟢

Result: 6 objects    Result: 4 objects
        recreated           (2 reused!)
```

### When Can You Use const?

**✅ CAN use const:**
- Widget with all constant values
- No variables or dynamic data
- Lists where all items are const

```dart
// ✅ All values are known at compile time
const Text('Hello')
const Icon(Icons.star)
const SizedBox(height: 16)
const EdgeInsets.all(8)
const [1, 2, 3]  // const list
```

**❌ CANNOT use const:**
- Widget uses variables
- Values computed at runtime
- Dynamic data

```dart
// ❌ Uses variable - can't be const
Text(userName)  // userName changes

// ❌ Calculated at runtime
Text('Count: $count')  // count changes

// ❌ Uses method call
Text(DateTime.now().toString())  // changes every call
```

### Real Example: const Optimization

```dart
class MyHomePage extends StatefulWidget {
  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  int counter = 0;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        const Text('Welcome!'),       // 🟢 REUSED - never changes
        const Icon(Icons.star),       // 🟢 REUSED - never changes
        const SizedBox(height: 16),   // 🟢 REUSED - never changes
        Text('Count: $counter'),      // 🔴 REBUILDS - uses variable
        ElevatedButton(
          onPressed: () => setState(() => counter++),
          child: const Text('Add'),   // 🟢 REUSED - text is constant
        ),
      ],
    );
  }
}
```

### Rule of Thumb

1. **Add `const` wherever possible** - VS Code will hint when you can
2. **Extract constant widgets** into `const` constructor classes
3. **Use `const` constructors** in your custom widgets

```dart
// Custom widget with const constructor
class WelcomeCard extends StatelessWidget {
  const WelcomeCard({super.key});  // 👈 const constructor!

  @override
  Widget build(BuildContext context) {
    return const Card(
      child: Text('Welcome!'),
    );
  }
}

// Now you can use it as const:
const WelcomeCard()  // 🟢 Reused across rebuilds!
```

**Performance Impact**: In complex apps with frequent rebuilds, const widgets can reduce frame drops and improve smoothness significantly!

---

