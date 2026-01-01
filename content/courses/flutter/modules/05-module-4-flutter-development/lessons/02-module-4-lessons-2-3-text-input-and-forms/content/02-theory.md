---
type: "THEORY"
title: "Reading TextField Value"
---


To read what the user typed, use a `TextEditingController`. It stores the current text and can also be used to programmatically set or clear the text.

**Important:** Always call `dispose()` on controllers when done to free memory. This is a common source of memory leaks if forgotten:



```dart
class TextFieldDemo extends StatefulWidget {
  @override
  _TextFieldDemoState createState() => _TextFieldDemoState();
}

class _TextFieldDemoState extends State<TextFieldDemo> {
  TextEditingController nameController = TextEditingController();
  
  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        TextField(
          controller: nameController,
          decoration: InputDecoration(labelText: 'Name'),
        ),
        ElevatedButton(
          onPressed: () {
            String name = nameController.text;
            print('Name: $name');
          },
          child: Text('Submit'),
        ),
      ],
    );
  }
  
  @override
  void dispose() {
    nameController.dispose();  // Clean up!
    super.dispose();
  }
}
```
