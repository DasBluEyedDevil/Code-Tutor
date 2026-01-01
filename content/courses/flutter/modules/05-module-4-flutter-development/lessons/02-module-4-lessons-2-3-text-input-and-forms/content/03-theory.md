---
type: "THEORY"
title: "Reading Input with TextEditingController"
---

To read what the user typed, use a `TextEditingController`.

```dart
class MyFormState extends State<MyForm> {
  // 1. Create the controller
  final _controller = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        TextField(controller: _controller), // 2. Attach it
        ElevatedButton(
          onPressed: () {
            // 3. Read the value
            print('Input: ${_controller.text}');
          },
          child: Text('Submit'),
        ),
      ],
    );
  }

  @override
  void dispose() {
    // 4. ALWAYS dispose controllers to save memory!
    _controller.dispose();
    super.dispose();
  }
}
```