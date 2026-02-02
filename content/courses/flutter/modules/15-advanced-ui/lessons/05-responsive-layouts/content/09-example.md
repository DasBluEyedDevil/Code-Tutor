---
type: "EXAMPLE"
title: "Building Adaptive Grids"
---


Create grids that automatically adjust to screen size:



```dart
import 'package:flutter/material.dart';

class AdaptiveGridDemo extends StatelessWidget {
  const AdaptiveGridDemo({super.key});

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 3,
      child: Scaffold(
        appBar: AppBar(
          title: const Text('Adaptive Grids'),
          bottom: const TabBar(
            tabs: [
              Tab(text: 'Fixed Count'),
              Tab(text: 'Max Extent'),
              Tab(text: 'Calculated'),
            ],
          ),
        ),
        body: TabBarView(
          children: [
            // Approach 1: Fixed count based on breakpoint
            _FixedCountGrid(),
            
            // Approach 2: Max extent (auto-calculates columns)
            _MaxExtentGrid(),
            
            // Approach 3: Calculated from LayoutBuilder
            _CalculatedGrid(),
          ],
        ),
      ),
    );
  }
}

// Approach 1: Fixed column count per breakpoint
class _FixedCountGrid extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        final width = constraints.maxWidth;
        final columns = width < 600 ? 2 : width < 900 ? 3 : 4;
        
        return GridView.count(
          crossAxisCount: columns,
          mainAxisSpacing: 16,
          crossAxisSpacing: 16,
          padding: const EdgeInsets.all(16),
          children: List.generate(
            20,
            (i) => _GridItem(index: i, label: '$columns columns'),
          ),
        );
      },
    );
  }
}

// Approach 2: Max cross-axis extent
class _MaxExtentGrid extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return GridView.builder(
      gridDelegate: const SliverGridDelegateWithMaxCrossAxisExtent(
        maxCrossAxisExtent: 250, // Items won't exceed 250px
        mainAxisSpacing: 16,
        crossAxisSpacing: 16,
        childAspectRatio: 1.0, // Square items
      ),
      padding: const EdgeInsets.all(16),
      itemCount: 20,
      itemBuilder: (context, i) => _GridItem(
        index: i,
        label: 'Max 250px',
      ),
    );
  }
}

// Approach 3: Calculate optimal columns
class _CalculatedGrid extends StatelessWidget {
  static const minItemWidth = 180.0;
  static const maxItemWidth = 280.0;
  static const spacing = 16.0;
  
  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        final availableWidth = constraints.maxWidth - spacing * 2;
        
        // Calculate columns that keep items between min and max width
        int columns = (availableWidth / minItemWidth).floor();
        columns = columns.clamp(1, 6);
        
        // Actual item width
        final itemWidth = (availableWidth - spacing * (columns - 1)) / columns;
        
        return GridView.builder(
          gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
            crossAxisCount: columns,
            mainAxisSpacing: spacing,
            crossAxisSpacing: spacing,
            childAspectRatio: itemWidth / (itemWidth * 0.8), // Slightly taller
          ),
          padding: EdgeInsets.all(spacing),
          itemCount: 20,
          itemBuilder: (context, i) => _GridItem(
            index: i,
            label: '${itemWidth.toInt()}px wide',
          ),
        );
      },
    );
  }
}

class _GridItem extends StatelessWidget {
  final int index;
  final String label;
  
  const _GridItem({required this.index, required this.label});
  
  @override
  Widget build(BuildContext context) {
    return Card(
      color: Colors.primaries[index % Colors.primaries.length][100],
      child: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text('Item $index', style: const TextStyle(fontWeight: FontWeight.bold)),
            Text(label, style: const TextStyle(fontSize: 12)),
          ],
        ),
      ),
    );
  }
}

// SliverGrid for use in CustomScrollView
class SliverGridExample extends StatelessWidget {
  const SliverGridExample({super.key});

  @override
  Widget build(BuildContext context) {
    return CustomScrollView(
      slivers: [
        const SliverAppBar(
          title: Text('Sliver Grid'),
          floating: true,
        ),
        SliverPadding(
          padding: const EdgeInsets.all(16),
          sliver: SliverGrid(
            gridDelegate: const SliverGridDelegateWithMaxCrossAxisExtent(
              maxCrossAxisExtent: 200,
              mainAxisSpacing: 16,
              crossAxisSpacing: 16,
            ),
            delegate: SliverChildBuilderDelegate(
              (context, index) => Card(
                child: Center(child: Text('Item $index')),
              ),
              childCount: 50,
            ),
          ),
        ),
      ],
    );
  }
}
```
