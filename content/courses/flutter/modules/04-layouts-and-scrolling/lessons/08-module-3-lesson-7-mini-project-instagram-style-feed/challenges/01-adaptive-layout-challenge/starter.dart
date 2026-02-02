// Adaptive Grid Challenge
// Create a grid that changes columns based on screen width

import 'package:flutter/material.dart';

void main() {
  runApp(const AdaptiveGridApp());
}

class AdaptiveGridApp extends StatelessWidget {
  const AdaptiveGridApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Adaptive Grid')),
        body: const AdaptiveGrid(),
      ),
    );
  }
}

class AdaptiveGrid extends StatelessWidget {
  const AdaptiveGrid({super.key});

  // TODO: Create a function to determine column count based on width
  // Hint: if (width < 400) return 2; etc.
  int _getColumnCount(double width) {
    return 2; // TODO: Return different values based on width
  }

  @override
  Widget build(BuildContext context) {
    // TODO: Use LayoutBuilder to get available width
    return LayoutBuilder(
      builder: (context, constraints) {
        final width = constraints.maxWidth;
        final columns = _getColumnCount(width);
        
        return GridView.builder(
          padding: const EdgeInsets.all(8),
          gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
            crossAxisCount: columns,
            crossAxisSpacing: 8,
            mainAxisSpacing: 8,
          ),
          itemCount: 12,
          itemBuilder: (context, index) {
            return Card(
              child: Center(child: Text('Item ${index + 1}')),
            );
          },
        );
      },
    );
  }
}