---
type: "THEORY"
title: "Step 6: Create Contact Info Cards"
---

Instead of repeating code for every contact item, let's create a reusable widget called `ContactCard`. This widget uses a `Card` (which is a pre-styled `Container`) with a `ListTile`.

```dart
// Add this widget outside the BusinessCardScreen class
class ContactCard extends StatelessWidget {
  final IconData icon;
  final String text;

  const ContactCard({required this.icon, required this.text, super.key});

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: const EdgeInsets.symmetric(vertical: 10, horizontal: 25),
      child: ListTile(
        leading: Icon(
          icon,
          color: Colors.teal,
        ),
        title: Text(
          text,
          style: TextStyle(
            color: Colors.teal.shade900,
            fontSize: 20,
          ),
        ),
      ),
    );
  }
}
```