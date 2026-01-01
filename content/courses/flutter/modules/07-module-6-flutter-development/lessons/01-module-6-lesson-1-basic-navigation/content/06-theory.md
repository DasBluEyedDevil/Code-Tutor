---
type: "THEORY"
title: "Receiving Data Back from Screen"
---


Use `await` with `Navigator.push`:


**Pattern**: `Navigator.pop(context, dataToReturn)`



```dart
// Screen 1: Get result from Screen 2
class HomeScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Home')),
      body: Center(
        child: ElevatedButton(
          onPressed: () async {
            // Wait for result
            final result = await Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => SelectColorScreen()),
            );

            if (result != null) {
              ScaffoldMessenger.of(context).showSnackBar(
                SnackBar(content: Text('Selected: $result')),
              );
            }
          },
          child: Text('Select Color'),
        ),
      ),
    );
  }
}

// Screen 2: Return result
class SelectColorScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Select Color')),
      body: Column(
        children: [
          ListTile(
            leading: CircleAvatar(backgroundColor: Colors.red),
            title: Text('Red'),
            onTap: () {
              Navigator.pop(context, 'Red');  // Return data!
            },
          ),
          ListTile(
            leading: CircleAvatar(backgroundColor: Colors.blue),
            title: Text('Blue'),
            onTap: () {
              Navigator.pop(context, 'Blue');
            },
          ),
          ListTile(
            leading: CircleAvatar(backgroundColor: Colors.green),
            title: Text('Green'),
            onTap: () {
              Navigator.pop(context, 'Green');
            },
          ),
        ],
      ),
    );
  }
}
```
