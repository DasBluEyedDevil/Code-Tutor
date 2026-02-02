---
type: "WARNING"
title: "Common ListView Mistakes"
---


**Horizontal ListView Needs a Height!**

A horizontal ListView inside a Column/Scaffold will crash with 'unbounded height':

❌ This crashes:
```dart
Column(
  children: [
    ListView(scrollDirection: Axis.horizontal, ...)  // Error!
  ],
)
```

✅ Fix - Give it a height:
```dart
Column(
  children: [
    SizedBox(
      height: 100,  // Now it knows how tall to be
      child: ListView(scrollDirection: Axis.horizontal, ...),
    ),
  ],
)
```

**Forgetting itemCount in ListView.builder**

Without `itemCount`, ListView.builder creates items forever (infinite loop):
```dart
// ❌ Missing itemCount - infinite items!
ListView.builder(
  itemBuilder: (ctx, index) => Text('Item $index'),
)

// ✅ Always specify itemCount
ListView.builder(
  itemCount: myList.length,
  itemBuilder: (ctx, index) => Text(myList[index]),
)
```

**Not Returning a Widget**

itemBuilder MUST return a widget:
```dart
// ❌ Forgot to return
itemBuilder: (ctx, index) {
  Text('Item $index');  // No return!
}

// ✅ Return the widget
itemBuilder: (ctx, index) {
  return Text('Item $index');
}
```

