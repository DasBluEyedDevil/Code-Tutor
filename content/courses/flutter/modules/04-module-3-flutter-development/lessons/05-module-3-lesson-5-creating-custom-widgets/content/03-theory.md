---
type: "THEORY"
title: "Passing Callbacks"
---


To make your custom widgets interactive, pass callback functions as parameters. Use `VoidCallback` for functions with no arguments, or `Function(Type)` for functions that take parameters.

This lets parent widgets handle the logic while child widgets handle the UI:



```dart
class CustomButton extends StatelessWidget {
  final String label;
  final VoidCallback onPressed;  // Function parameter!
  
  CustomButton({required this.label, required this.onPressed});
  
  @override
  Widget build(BuildContext context) {
    return ElevatedButton(
      onPressed: onPressed,
      child: Text(label),
    );
  }
}

// Usage:
CustomButton(
  label: 'Click Me',
  onPressed: () {
    print('Button clicked!');
  },
)
```
