---
type: "EXAMPLE"
title: "LayoutBuilder for Responsive Components"
---


Build components that adapt to their container:



```dart
import 'package:flutter/material.dart';

// Responsive card that changes layout based on available width
class ResponsiveCard extends StatelessWidget {
  final String title;
  final String description;
  final IconData icon;
  
  const ResponsiveCard({
    super.key,
    required this.title,
    required this.description,
    required this.icon,
  });

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        // Decide layout based on available width
        final isWide = constraints.maxWidth > 400;
        
        if (isWide) {
          // Horizontal layout for wide containers
          return Card(
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Row(
                children: [
                  Icon(icon, size: 48),
                  const SizedBox(width: 16),
                  Expanded(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(title, style: Theme.of(context).textTheme.titleLarge),
                        Text(description),
                      ],
                    ),
                  ),
                ],
              ),
            ),
          );
        } else {
          // Vertical layout for narrow containers
          return Card(
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                children: [
                  Icon(icon, size: 48),
                  const SizedBox(height: 8),
                  Text(title, style: Theme.of(context).textTheme.titleLarge),
                  Text(description, textAlign: TextAlign.center),
                ],
              ),
            ),
          );
        }
      },
    );
  }
}

// Adaptive grid that calculates columns from available width
class AdaptiveGrid extends StatelessWidget {
  final List<Widget> children;
  final double minItemWidth;
  final double spacing;
  
  const AdaptiveGrid({
    super.key,
    required this.children,
    this.minItemWidth = 200,
    this.spacing = 16,
  });

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        // Calculate how many columns fit
        final width = constraints.maxWidth;
        final columns = (width / minItemWidth).floor().clamp(1, 6);
        
        return GridView.count(
          crossAxisCount: columns,
          mainAxisSpacing: spacing,
          crossAxisSpacing: spacing,
          padding: EdgeInsets.all(spacing),
          children: children,
        );
      },
    );
  }
}

// Usage example
class LayoutBuilderDemo extends StatelessWidget {
  const LayoutBuilderDemo({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Row(
        children: [
          // Sidebar - 300px
          Container(
            width: 300,
            color: Colors.grey[200],
            child: const ResponsiveCard(
              title: 'Sidebar Card',
              description: 'Will use vertical layout',
              icon: Icons.menu,
            ),
          ),
          // Main content - remaining space
          Expanded(
            child: AdaptiveGrid(
              minItemWidth: 250,
              children: List.generate(
                12,
                (i) => Card(
                  child: Center(child: Text('Item $i')),
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
```
