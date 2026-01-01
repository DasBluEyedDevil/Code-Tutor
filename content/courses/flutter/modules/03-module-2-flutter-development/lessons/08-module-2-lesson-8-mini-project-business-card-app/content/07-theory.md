---
type: "THEORY"
title: "Step 6: Create Contact Info Cards"
---


Let's create a reusable widget for contact info:




```dart
// Add this widget outside BusinessCardScreen class
class ContactCard extends StatelessWidget {
  final IconData icon;
  final String text;

  ContactCard({required this.icon, required this.text});

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 10, horizontal: 25),
      padding: EdgeInsets.all(10),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(5),
      ),
      child: Row(
        children: [
          Icon(icon, color: Colors.teal),
          SizedBox(width: 10),
          Text(
            text,
            style: TextStyle(
              color: Colors.teal[900],
              fontSize: 16,
            ),
          ),
        ],
      ),
    );
  }
}
```
