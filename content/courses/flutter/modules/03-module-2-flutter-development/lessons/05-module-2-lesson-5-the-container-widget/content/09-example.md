---
type: "EXAMPLE"
title: "Complete Card Example"
---


Let's combine everything to create a nice card:




```dart
Container(
  margin: EdgeInsets.all(20),
  padding: EdgeInsets.all(20),
  decoration: BoxDecoration(
    color: Colors.white,
    borderRadius: BorderRadius.circular(15),
    boxShadow: [
      BoxShadow(
        color: Colors.grey.withOpacity(0.5),
        blurRadius: 10,
        offset: Offset(0, 3),
      ),
    ],
  ),
  child: Column(
    mainAxisSize: MainAxisSize.min,
    children: [
      Text(
        'Card Title',
        style: TextStyle(
          fontSize: 20,
          fontWeight: FontWeight.bold,
        ),
      ),
      SizedBox(height: 10),
      Text(
        'This is a nice card with shadow and rounded corners!',
        textAlign: TextAlign.center,
      ),
    ],
  ),
)
```
