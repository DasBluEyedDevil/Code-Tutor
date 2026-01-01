---
type: "THEORY"
title: "Passing Data to New Screen"
---


Pass data via constructor:




```dart
class DetailScreen extends StatelessWidget {
  final String title;
  final int id;

  DetailScreen({required this.title, required this.id});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text(title)),
      body: Center(
        child: Text('Item ID: $id', style: TextStyle(fontSize: 24)),
      ),
    );
  }
}

// Navigate with data
ElevatedButton(
  onPressed: () {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => DetailScreen(
          title: 'Product Detail',
          id: 42,
        ),
      ),
    );
  },
  child: Text('View Product'),
)
```
