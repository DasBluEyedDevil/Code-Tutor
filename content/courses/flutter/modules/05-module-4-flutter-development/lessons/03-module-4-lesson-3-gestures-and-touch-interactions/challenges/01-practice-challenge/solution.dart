// Solution: Draggable 3x3 Grid
// Tiles can be dragged and dropped to reorder

import 'package:flutter/material.dart';

void main() {
  runApp(const DraggableGridApp());
}

class DraggableGridApp extends StatelessWidget {
  const DraggableGridApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Drag to Reorder')),
        body: const DraggableGrid(),
      ),
    );
  }
}

class DraggableGrid extends StatefulWidget {
  const DraggableGrid({super.key});

  @override
  State<DraggableGrid> createState() => _DraggableGridState();
}

class _DraggableGridState extends State<DraggableGrid> {
  // 9 tiles for 3x3 grid
  List<int> tiles = List.generate(9, (i) => i + 1);

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(16),
      child: GridView.builder(
        gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
          crossAxisCount: 3,
          crossAxisSpacing: 8,
          mainAxisSpacing: 8,
        ),
        itemCount: tiles.length,
        itemBuilder: (context, index) {
          final tile = tiles[index];
          return DragTarget<int>(
            onAcceptWithDetails: (details) {
              setState(() {
                final draggedIndex = tiles.indexOf(details.data);
                tiles.removeAt(draggedIndex);
                tiles.insert(index, details.data);
              });
            },
            builder: (context, candidateData, rejectedData) {
              final isHighlighted = candidateData.isNotEmpty;
              return Draggable<int>(
                data: tile,
                feedback: Material(
                  elevation: 8,
                  borderRadius: BorderRadius.circular(12),
                  child: _buildTile(tile, isDragging: true),
                ),
                childWhenDragging: Container(
                  decoration: BoxDecoration(
                    color: Colors.grey.shade200,
                    borderRadius: BorderRadius.circular(12),
                    border: Border.all(color: Colors.grey, style: BorderStyle.solid),
                  ),
                ),
                child: AnimatedContainer(
                  duration: const Duration(milliseconds: 200),
                  decoration: BoxDecoration(
                    color: isHighlighted ? Colors.blue.shade100 : null,
                    borderRadius: BorderRadius.circular(12),
                  ),
                  child: _buildTile(tile),
                ),
              );
            },
          );
        },
      ),
    );
  }

  Widget _buildTile(int number, {bool isDragging = false}) {
    return Container(
      width: 100,
      height: 100,
      decoration: BoxDecoration(
        color: Colors.primaries[number % Colors.primaries.length],
        borderRadius: BorderRadius.circular(12),
        boxShadow: isDragging
            ? [BoxShadow(color: Colors.black26, blurRadius: 10)]
            : null,
      ),
      child: Center(
        child: Text(
          '$number',
          style: const TextStyle(
            color: Colors.white,
            fontSize: 32,
            fontWeight: FontWeight.bold,
          ),
        ),
      ),
    );
  }
}

// Key concepts:
// - Draggable: Makes widget draggable
// - DragTarget: Accepts dropped items
// - feedback: Widget shown while dragging
// - childWhenDragging: Placeholder at original position
// - onAcceptWithDetails: Handle drop and reorder list