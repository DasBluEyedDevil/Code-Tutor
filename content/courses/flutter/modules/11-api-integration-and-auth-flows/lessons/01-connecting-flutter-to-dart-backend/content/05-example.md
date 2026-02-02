---
type: "EXAMPLE"
title: "Initializing the Client in Your Flutter App"
---


The Client class is the main entry point for all API calls. Here is how to initialize it:



```dart
// lib/main.dart
import 'package:flutter/material.dart';
import 'package:serverpod_flutter/serverpod_flutter.dart';
import 'package:my_app_client/my_app_client.dart';

// Create a global client instance
// In production, you would use dependency injection (covered later)
late Client client;

void main() async {
  // Ensure Flutter is initialized before creating the client
  WidgetsFlutterBinding.ensureInitialized();
  
  // Initialize the Serverpod client
  client = Client(
    // Development URL - your local Serverpod server
    'http://localhost:8080/',
    
    // Optional: Configure connection settings
    authenticationKeyManager: FlutterAuthenticationKeyManager(),
  )..connectivityMonitor = FlutterConnectivityMonitor();
  
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'My Serverpod App',
      home: const HomeScreen(),
    );
  }
}

// Now you can use the client anywhere in your app:
class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});
  
  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  String message = 'Loading...';
  
  @override
  void initState() {
    super.initState();
    _loadGreeting();
  }
  
  Future<void> _loadGreeting() async {
    try {
      // Call the example endpoint that comes with new Serverpod projects
      final greeting = await client.example.hello('Flutter Developer');
      setState(() {
        message = greeting;
      });
    } catch (e) {
      setState(() {
        message = 'Error: $e';
      });
    }
  }
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Serverpod Demo')),
      body: Center(
        child: Text(message, style: Theme.of(context).textTheme.headlineMedium),
      ),
    );
  }
}
```
