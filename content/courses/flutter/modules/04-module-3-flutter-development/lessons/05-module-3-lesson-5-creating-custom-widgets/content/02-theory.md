---
type: "THEORY"
title: "The Structure of a Custom Widget"
---

A custom `StatelessWidget` always follows this pattern:

1.  **Class Declaration**: Extends `StatelessWidget`.
2.  **Fields**: Marked as `final` because widgets are immutable (they don't change after being built).
3.  **Constructor**: Initializes the fields and uses `super.key` for Flutter's internal tracking.
4.  **Build Method**: Returns the widget tree for this component.

```dart
class MyHeader extends StatelessWidget {
  // 1. Final fields
  final String title;
  final IconData icon;

  // 2. Constructor with super.key
  const MyHeader({
    required this.title,
    required this.icon,
    super.key,
  });

  // 3. The build method
  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        Icon(icon),
        Text(title),
      ],
    );
  }
}
```