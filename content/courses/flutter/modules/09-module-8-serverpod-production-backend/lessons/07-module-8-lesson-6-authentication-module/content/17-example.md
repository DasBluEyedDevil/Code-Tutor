---
type: "EXAMPLE"
title: "Setting Up the SessionManager"
---

Here is how to properly initialize and use the SessionManager in your Flutter app.



```dart
// lib/main.dart

import 'package:flutter/material.dart';
import 'package:serverpod_flutter/serverpod_flutter.dart';
import 'package:serverpod_auth_shared_flutter/serverpod_auth_shared_flutter.dart';
import 'package:my_project_client/my_project_client.dart';

// Global client instance
late final Client client;
late final SessionManager sessionManager;

void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  // Create the client
  client = Client(
    'http://localhost:8080/',
    authenticationKeyManager: FlutterAuthenticationKeyManager(),
  );

  // Create and initialize the session manager
  sessionManager = SessionManager(
    caller: client.modules.auth,
  );

  // Initialize - this loads any stored session
  await sessionManager.initialize();

  runApp(const MyApp());
}

class MyApp extends StatefulWidget {
  const MyApp({super.key});

  @override
  State<MyApp> createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  @override
  void initState() {
    super.initState();
    // Listen to auth state changes
    sessionManager.addListener(_onAuthStateChanged);
  }

  @override
  void dispose() {
    sessionManager.removeListener(_onAuthStateChanged);
    super.dispose();
  }

  void _onAuthStateChanged() {
    // Rebuild to show appropriate screen
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'My App',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.blue),
        useMaterial3: true,
      ),
      // Show different screens based on auth state
      home: sessionManager.isSignedIn
          ? HomeScreen(user: sessionManager.signedInUser!)
          : const SignInScreen(),
    );
  }
}
```
