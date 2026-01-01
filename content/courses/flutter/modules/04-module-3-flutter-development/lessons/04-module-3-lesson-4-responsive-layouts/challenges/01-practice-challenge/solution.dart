// Solution: Responsive Grid with GridView.extent
// Automatically adjusts columns based on screen width

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
    // Get screen width using MediaQuery
    final screenWidth = MediaQuery.sizeOf(context).width;
    
    return Column(
      children: [
        // Display screen info
        Container(
          padding: const EdgeInsets.all(16),
          color: Colors.blue.shade50,
          width: double.infinity,
          child: Text(
            'Screen width: ${screenWidth.toStringAsFixed(0)}px',
            style: const TextStyle(fontWeight: FontWeight.bold),
          ),
        ),
        
        // GridView.extent: Auto-adjusts columns based on item size
        Expanded(
          child: GridView.extent(
            maxCrossAxisExtent: 150, // Max width of each item
            padding: const EdgeInsets.all(8),
            crossAxisSpacing: 8,
            mainAxisSpacing: 8,
            children: List.generate(12, (index) {
              return Card(
                color: Colors.primaries[index % Colors.primaries.length],
                child: Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Icon(
                        Icons.widgets,
                        color: Colors.white,
                        size: 32,
                      ),
                      const SizedBox(height: 8),
                      Text(
                        'Item ${index + 1}',
                        style: const TextStyle(
                          color: Colors.white,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ],
                  ),
                ),
              );
            }),
          ),
        ),
      ],
    );
  }
}

// Alternative: Using MediaQuery for manual control
class MediaQueryGrid extends StatelessWidget {
  const MediaQueryGrid({super.key});

  @override
  Widget build(BuildContext context) {
    final width = MediaQuery.sizeOf(context).width;
    // Calculate columns based on screen width
    final columns = width < 400 ? 2 : (width < 800 ? 3 : 4);
    
    return GridView.builder(
      gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: columns,
        crossAxisSpacing: 8,
        mainAxisSpacing: 8,
      ),
      itemCount: 12,
      itemBuilder: (context, index) => Card(
        child: Center(child: Text('Item ${index + 1}')),
      ),
    );
  }
}

// Key concepts:
// - GridView.extent: Auto columns based on maxCrossAxisExtent
// - MediaQuery: Manual control over layout based on screen size
// - Responsive design: Adapts to any screen width