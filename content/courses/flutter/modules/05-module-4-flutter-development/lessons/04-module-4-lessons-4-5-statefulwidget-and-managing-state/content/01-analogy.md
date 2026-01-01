---
type: "ANALOGY"
title: "The Update Problem"
---


Right now, your apps are **static**. When you click a button, nothing changes on screen!

Try this - it WON'T work:


**Problem**: The screen doesn't know to rebuild!

**Solution**: **StatefulWidget** and **setState()**!



```dart
class CounterBroken extends StatelessWidget {
  int counter = 0;  // This won't update the UI!
  
  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Text('Count: $counter'),
        ElevatedButton(
          onPressed: () {
            counter++;  // Changes variable but UI doesn't rebuild!
            print(counter);  // Console shows it changes
          },
          child: Text('Increment'),
        ),
      ],
    );
  }
}
```
