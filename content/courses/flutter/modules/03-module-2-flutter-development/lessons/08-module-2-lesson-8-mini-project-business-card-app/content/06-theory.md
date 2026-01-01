---
type: "THEORY"
title: "Step 3: Add the Profile Photo"
---



**Tip**: Replace `Icon(Icons.person...)` with `backgroundImage: NetworkImage('YOUR_PHOTO_URL')` to use a real photo!



```dart
class BusinessCardScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.teal,
      body: SafeArea(
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              CircleAvatar(
                radius: 50,
                backgroundColor: Colors.white,
                child: Icon(
                  Icons.person,
                  size: 60,
                  color: Colors.teal,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
```
