---
type: "THEORY"
title: "Extracting a Widget"
---


**Before**: Messy code with repetition


**After**: Clean custom widget




```dart
// Define once
class CustomCard extends StatelessWidget {
  final String text;
  
  CustomCard({required this.text});
  
  @override
  Widget build(BuildContext context) {
    return Container(
      padding: EdgeInsets.all(16),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(8),
        boxShadow: [BoxShadow(color: Colors.grey, blurRadius: 4)],
      ),
      child: Text(text),
    );
  }
}

// Use many times
CustomCard(text: 'Hello'),
CustomCard(text: 'World'),
```
