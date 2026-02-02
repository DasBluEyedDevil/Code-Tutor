---
type: "THEORY"
title: "onUnknownRoute (404 Handler)"
---


Handle invalid routes gracefully:




```dart
MaterialApp(
  routes: {
    '/': (context) => HomeScreen(),
    '/about': (context) => AboutScreen(),
  },
  onUnknownRoute: (settings) {
    return MaterialPageRoute(
      builder: (context) => NotFoundScreen(routeName: settings.name),
    );
  },
);

class NotFoundScreen extends StatelessWidget {
  final String? routeName;

  NotFoundScreen({this.routeName});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('404')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(Icons.error_outline, size: 100, color: Colors.red),
            SizedBox(height: 16),
            Text('Page Not Found', style: TextStyle(fontSize: 24)),
            if (routeName != null)
              Text('Route: $routeName', style: TextStyle(color: Colors.grey)),
            SizedBox(height: 24),
            ElevatedButton(
              onPressed: () => Navigator.pushNamedAndRemoveUntil(context, '/', (route) => false),
              child: Text('Go Home'),
            ),
          ],
        ),
      ),
    );
  }
}
```
