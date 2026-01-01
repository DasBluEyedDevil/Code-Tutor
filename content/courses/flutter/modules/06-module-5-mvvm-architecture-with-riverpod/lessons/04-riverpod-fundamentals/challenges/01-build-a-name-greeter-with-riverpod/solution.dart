import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

// STEP 1: Create a StateProvider for the name
// StateProvider is perfect for simple values like strings
final nameProvider = StateProvider<String>((ref) => '');

void main() {
  runApp(
    // STEP 2: Wrap the app in ProviderScope
    // This is REQUIRED for Riverpod to work
    const ProviderScope(
      child: MyApp(),
    ),
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

// STEP 3: Use ConsumerWidget to access providers
class GreeterScreen extends ConsumerWidget {
  const GreeterScreen({super.key});

  @override
  // STEP 4: Add WidgetRef ref parameter to build method
  Widget build(BuildContext context, WidgetRef ref) {
    // STEP 5: Watch the provider to get the current name
    // Using watch() means this widget rebuilds when name changes
    final name = ref.watch(nameProvider);
    
    // STEP 6: Create greeting based on whether name is empty
    final greeting = name.isEmpty 
        ? 'Hello, stranger!' 
        : 'Hello, $name!';

    return Scaffold(
      appBar: AppBar(
        title: const Text('Riverpod Greeter'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(24.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            // Display the greeting - updates automatically!
            Text(
              greeting,
              style: const TextStyle(
                fontSize: 32,
                fontWeight: FontWeight.bold,
              ),
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: 40),
            // STEP 7: TextField that updates the provider
            TextField(
              decoration: const InputDecoration(
                labelText: 'Enter your name',
                border: OutlineInputBorder(),
                hintText: 'Type your name here...',
              ),
              // Use ref.read() in callbacks, NOT ref.watch()
              // ref.read() gets the notifier so we can update state
              onChanged: (value) {
                ref.read(nameProvider.notifier).state = value;
              },
            ),
            const SizedBox(height: 20),
            // Bonus: Clear button to reset the name
            TextButton(
              onPressed: () {
                ref.read(nameProvider.notifier).state = '';
              },
              child: const Text('Clear'),
            ),
          ],
        ),
      ),
    );
  }
}

// KEY CONCEPTS DEMONSTRATED:
//
// 1. StateProvider - holds a simple String value
//
// 2. ProviderScope - wraps the app to enable Riverpod
//
// 3. ConsumerWidget - allows access to providers via ref
//
// 4. ref.watch() - used in build() to read AND subscribe to changes
//    The widget automatically rebuilds when the value changes
//
// 5. ref.read() - used in callbacks (onChanged, onPressed) to update
//    Does not subscribe to changes, just gets the value/notifier once
//
// 6. .notifier.state - how to update a StateProvider's value
//    ref.read(nameProvider.notifier).state = newValue;