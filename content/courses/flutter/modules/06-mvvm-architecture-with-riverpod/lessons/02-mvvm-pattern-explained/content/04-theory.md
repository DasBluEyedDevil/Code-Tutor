---
type: "THEORY"
title: "Why Not Just StatefulWidget?"
---

You might wonder: "Why not just use StatefulWidget with setState?" Let us compare both approaches.

### StatefulWidget Approach

With StatefulWidget, you put everything in one place:

```dart
// STATEFULWIDGET APPROACH - Everything mixed together
class CounterPage extends StatefulWidget {
  @override
  State<CounterPage> createState() => _CounterPageState();
}

class _CounterPageState extends State<CounterPage> {
  int count = 0;  // State in the widget

  void increment() {
    // Logic in the widget
    if (count < 100) {
      setState(() => count++);
    }
  }

  void decrement() {
    // More logic in the widget
    if (count > 0) {
      setState(() => count--);
    }
  }

  @override
  Widget build(BuildContext context) {
    // UI in the same class as logic
    return Column(
      children: [
        Text('$count'),
        Row(
          children: [
            ElevatedButton(onPressed: decrement, child: Text('-')),
            ElevatedButton(onPressed: increment, child: Text('+')),
          ],
        ),
      ],
    );
  }
}

// PROBLEMS WITH THIS APPROACH:
//
// 1. TESTING IS HARD
//    How do you test increment() without rendering the widget?
//    You cannot. You must use widget tests for everything.
//
// 2. NO CODE REUSE
//    Need the same counter logic on another screen?
//    You must copy-paste everything.
//
// 3. STATE IS LOST
//    Navigate away and back? Count resets to 0.
//    StatefulWidget state is tied to the widget tree.
//
// 4. NO SEPARATION
//    Logic and UI are mixed. Changes to one risk breaking the other.
```
