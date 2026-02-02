// Responsive Grid Challenge
// Create a grid that adapts to screen size

import 'package:flutter/material.dart';

void main() {
  runApp(const ResponsiveGridApp());
}

class ResponsiveGridApp extends StatelessWidget {
  const ResponsiveGridApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Responsive Grid')),
        body: const ResponsiveGrid(),
      ),
    );
  }
}

class ResponsiveGrid extends StatelessWidget {
  const ResponsiveGrid({super.key});

  @override
  Widget build(BuildContext context) {
    // TODO: Get screen width using MediaQuery.sizeOf(context).width
    
    // TODO: Use GridView.extent with maxCrossAxisExtent
    // This auto-adjusts columns based on item size!
    return GridView.extent(
      maxCrossAxisExtent: 150, // Max width per item
      padding: const EdgeInsets.all(8),
      crossAxisSpacing: 8,
      mainAxisSpacing: 8,
      children: List.generate(12, (index) {
        // TODO: Create Card widgets for each item
        return Card(
          child: Center(child: Text('Item ${index + 1}')),
        );
      }),
    );
  }
}