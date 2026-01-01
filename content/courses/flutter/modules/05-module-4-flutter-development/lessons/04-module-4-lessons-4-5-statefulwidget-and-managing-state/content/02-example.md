---
type: "EXAMPLE"
title: "Your First StatefulWidget"
---



**Now it works!** Click the button and the number updates!



```dart
class Counter extends StatefulWidget {
  @override
  _CounterState createState() => _CounterState();
}

class _CounterState extends State<Counter> {
  int counter = 0;
  
  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Text('Count: $counter', style: TextStyle(fontSize: 48)),
        ElevatedButton(
          onPressed: () {
            setState(() {
              counter++;  // setState tells Flutter to rebuild!
            });
          },
          child: Text('Increment'),
        ),
      ],
    );
  }
}
```
