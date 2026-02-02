---
type: "EXAMPLE"
title: "Section 7: Complete Theming Example"
---


### Comprehensive App Theme


### Using the Theme




```dart
// lib/main.dart
import 'package:flutter/material.dart';
import 'theme/app_theme.dart';

void main() => runApp(const MyApp());

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Themed App',
      theme: AppTheme.lightTheme,
      darkTheme: AppTheme.darkTheme,
      themeMode: ThemeMode.system,
      home: const HomeScreen(),
    );
  }
}

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Material 3 Theme'),
      ),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: [
          // Headline
          Text(
            'Welcome!',
            style: Theme.of(context).textTheme.headlineLarge,
          ),
          const SizedBox(height: 16),

          // Body text
          Text(
            'This app uses a custom Material 3 theme with consistent colors, typography, and component styling.',
            style: Theme.of(context).textTheme.bodyLarge,
          ),
          const SizedBox(height: 24),

          // Buttons
          ElevatedButton(
            onPressed: () {},
            child: const Text('Elevated Button'),
          ),
          const SizedBox(height: 16),

          FilledButton(
            onPressed: () {},
            child: const Text('Filled Button'),
          ),
          const SizedBox(height: 16),

          OutlinedButton(
            onPressed: () {},
            child: const Text('Outlined Button'),
          ),
          const SizedBox(height: 24),

          // Card
          Card(
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    'Themed Card',
                    style: Theme.of(context).textTheme.titleLarge,
                  ),
                  const SizedBox(height: 8),
                  Text(
                    'This card automatically uses the theme\'s card styling.',
                    style: Theme.of(context).textTheme.bodyMedium,
                  ),
                ],
              ),
            ),
          ),
          const SizedBox(height: 24),

          // Input field
          const TextField(
            decoration: InputDecoration(
              labelText: 'Username',
              hintText: 'Enter your username',
              prefixIcon: Icon(Icons.person),
            ),
          ),
        ],
      ),
    );
  }
}
```
