import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

// TODO 1: Create a StateProvider<String> called nameProvider
// Initial value should be an empty string ''


void main() {
  runApp(
    // TODO 2: Wrap MyApp with ProviderScope
    MyApp(),
  );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Riverpod Greeter',
      theme: ThemeData(primarySwatch: Colors.blue),
      home: const GreeterScreen(),
    );
  }
}

// TODO 3: Change this to a ConsumerWidget
class GreeterScreen extends StatelessWidget {
  const GreeterScreen({super.key});

  @override
  // TODO 4: Add WidgetRef ref parameter
  Widget build(BuildContext context) {
    // TODO 5: Watch the nameProvider to get the current name
    final name = '';  // Replace this with ref.watch()
    
    // TODO 6: Create the greeting message
    // If name is empty, show 'Hello, stranger!'
    // Otherwise show 'Hello, {name}!'
    final greeting = 'Hello!';

    return Scaffold(
      appBar: AppBar(
        title: const Text('Riverpod Greeter'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(24.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            // Display the greeting
            Text(
              greeting,
              style: const TextStyle(
                fontSize: 32,
                fontWeight: FontWeight.bold,
              ),
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: 40),
            // TODO 7: Create TextField that updates nameProvider
            TextField(
              decoration: const InputDecoration(
                labelText: 'Enter your name',
                border: OutlineInputBorder(),
              ),
              onChanged: (value) {
                // TODO: Update nameProvider using ref.read()
                // ref.read(nameProvider.notifier).state = value;
              },
            ),
          ],
        ),
      ),
    );
  }
}