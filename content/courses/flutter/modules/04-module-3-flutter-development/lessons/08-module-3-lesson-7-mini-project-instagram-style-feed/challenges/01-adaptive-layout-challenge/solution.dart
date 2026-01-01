// Solution: Adaptive Grid Columns
// Changes column count based on screen width breakpoints

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

  // Determine column count based on screen width
  int _getColumnCount(double width) {
    if (width < 400) return 2;       // Mobile portrait
    if (width < 600) return 3;       // Mobile landscape / small tablet
    if (width < 900) return 4;       // Tablet
    return 5;                         // Desktop
  }

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        final width = constraints.maxWidth;
        final columns = _getColumnCount(width);
        
        return Column(
          children: [
            // Info bar showing current layout
            Container(
              width: double.infinity,
              padding: const EdgeInsets.all(12),
              color: Colors.blue.shade50,
              child: Text(
                'Width: ${width.toStringAsFixed(0)}px | Columns: $columns',
                style: const TextStyle(fontWeight: FontWeight.bold),
                textAlign: TextAlign.center,
              ),
            ),
            
            // Adaptive grid
            Expanded(
              child: GridView.builder(
                padding: const EdgeInsets.all(8),
                gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                  crossAxisCount: columns,
                  crossAxisSpacing: 8,
                  mainAxisSpacing: 8,
                  childAspectRatio: 1,
                ),
                itemCount: 20,
                itemBuilder: (context, index) {
                  return Card(
                    color: Colors.primaries[index % Colors.primaries.length].shade100,
                    child: Center(
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Icon(
                            Icons.grid_view,
                            size: 32,
                            color: Colors.primaries[index % Colors.primaries.length],
                          ),
                          const SizedBox(height: 8),
                          Text(
                            'Item ${index + 1}',
                            style: const TextStyle(fontWeight: FontWeight.w500),
                          ),
                        ],
                      ),
                    ),
                  );
                },
              ),
            ),
          ],
        );
      },
    );
  }
}

// Key concepts:
// - LayoutBuilder provides parent constraints
// - Breakpoints define column count for different widths
// - SliverGridDelegateWithFixedCrossAxisCount for fixed columns
// - Resize window to see columns change dynamically