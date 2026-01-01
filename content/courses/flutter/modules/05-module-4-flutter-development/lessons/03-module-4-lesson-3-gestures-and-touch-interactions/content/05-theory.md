---
type: "THEORY"
title: "Swipe to Dismiss"
---



**Swipe left to delete - like iOS Mail!**



```dart
class SwipeableTodo extends StatelessWidget {
  final List<String> todos = ['Buy milk', 'Walk dog', 'Code Flutter'];

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: todos.length,
      itemBuilder: (context, index) {
        return Dismissible(
          key: Key(todos[index]),
          background: Container(
            color: Colors.red,
            alignment: Alignment.centerRight,
            padding: EdgeInsets.only(right: 20),
            child: Icon(Icons.delete, color: Colors.white),
          ),
          direction: DismissDirection.endToStart,
          onDismissed: (direction) {
            ScaffoldMessenger.of(context).showSnackBar(
              SnackBar(content: Text('${todos[index]} deleted')),
            );
          },
          child: ListTile(
            leading: Icon(Icons.check_box_outline_blank),
            title: Text(todos[index]),
          ),
        );
      },
    );
  }
}
```
