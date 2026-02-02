---
type: "THEORY"
title: "Passing Callbacks (Interactivity)"
---

To make your custom widgets interactive, pass callback functions as parameters. Use `VoidCallback` for functions with no arguments, or `ValueChanged<T>` for functions that pass a value.

```dart
class CustomButton extends StatelessWidget {
  final String label;
  final VoidCallback onPressed;
  
  const CustomButton({
    required this.label, 
    required this.onPressed,
    super.key,
  });
  
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