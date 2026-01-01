// Solution: Button Showcase App
// Demonstrates 5+ button types with different styles

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

  void _showMessage(BuildContext context, String message) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text(message), duration: const Duration(seconds: 1)),
    );
  }

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      padding: const EdgeInsets.all(16),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          // 1. ElevatedButton - Primary action
          ElevatedButton(
            onPressed: () => _showMessage(context, 'ElevatedButton pressed!'),
            style: ElevatedButton.styleFrom(
              backgroundColor: Colors.blue,
              foregroundColor: Colors.white,
              padding: const EdgeInsets.all(16),
            ),
            child: const Text('Elevated Button'),
          ),
          const SizedBox(height: 16),
          
          // 2. TextButton - Secondary action
          TextButton(
            onPressed: () => _showMessage(context, 'TextButton pressed!'),
            style: TextButton.styleFrom(
              foregroundColor: Colors.purple,
            ),
            child: const Text('Text Button'),
          ),
          const SizedBox(height: 16),
          
          // 3. OutlinedButton - Tertiary action
          OutlinedButton(
            onPressed: () => _showMessage(context, 'OutlinedButton pressed!'),
            style: OutlinedButton.styleFrom(
              side: const BorderSide(color: Colors.green, width: 2),
              foregroundColor: Colors.green,
            ),
            child: const Text('Outlined Button'),
          ),
          const SizedBox(height: 16),
          
          // 4. IconButton - Icon-only action
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              IconButton(
                onPressed: () => _showMessage(context, 'Favorite pressed!'),
                icon: const Icon(Icons.favorite),
                color: Colors.red,
                iconSize: 32,
              ),
              IconButton(
                onPressed: () => _showMessage(context, 'Share pressed!'),
                icon: const Icon(Icons.share),
                color: Colors.blue,
                iconSize: 32,
              ),
            ],
          ),
          const SizedBox(height: 16),
          
          // 5. FloatingActionButton style
          Center(
            child: FloatingActionButton.extended(
              onPressed: () => _showMessage(context, 'FAB pressed!'),
              icon: const Icon(Icons.add),
              label: const Text('Add Item'),
            ),
          ),
          const SizedBox(height: 16),
          
          // 6. Disabled button
          ElevatedButton(
            onPressed: null, // null makes it disabled
            style: ElevatedButton.styleFrom(
              disabledBackgroundColor: Colors.grey.shade300,
            ),
            child: const Text('Disabled Button'),
          ),
          const SizedBox(height: 16),
          
          // 7. Custom styled button
          ElevatedButton(
            onPressed: () => _showMessage(context, 'Custom button pressed!'),
            style: ElevatedButton.styleFrom(
              backgroundColor: Colors.orange,
              foregroundColor: Colors.white,
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(20),
              ),
              elevation: 8,
            ),
            child: const Text('Custom Styled'),
          ),
        ],
      ),
    );
  }
}

// Key concepts:
// - ElevatedButton: Primary actions with elevation
// - TextButton: Low-emphasis actions
// - OutlinedButton: Medium-emphasis with border
// - IconButton: Icon-only actions
// - onPressed: null makes button disabled