// Button Showcase Challenge
// Create an app with different button types

import 'package:flutter/material.dart';

void main() {
  runApp(const ButtonShowcaseApp());
}

class ButtonShowcaseApp extends StatelessWidget {
  const ButtonShowcaseApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Button Types')),
        body: const ButtonShowcase(),
      ),
    );
  }
}

class ButtonShowcase extends StatelessWidget {
  const ButtonShowcase({super.key});

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      padding: const EdgeInsets.all(16),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          // TODO 1: ElevatedButton - Primary action
          ElevatedButton(
            onPressed: () => print('Elevated pressed'),
            child: const Text('Elevated Button'),
          ),
          const SizedBox(height: 16),
          
          // TODO 2: TextButton - Secondary action
          
          // TODO 3: OutlinedButton - Tertiary action
          
          // TODO 4: IconButton - Icon-only
          
          // TODO 5: Disabled button (onPressed: null)
        ],
      ),
    );
  }
}