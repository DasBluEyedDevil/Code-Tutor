// Draggable Grid Challenge
// Create tiles that can be dragged and dropped

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
          // TODO: Wrap with DragTarget and Draggable
          // Draggable<int> for dragging
          // DragTarget<int> for dropping
          return Container(
            decoration: BoxDecoration(
              color: Colors.primaries[tile % Colors.primaries.length],
              borderRadius: BorderRadius.circular(12),
            ),
            child: Center(
              child: Text(
                '$tile',
                style: const TextStyle(color: Colors.white, fontSize: 24),
              ),
            ),
          );
        },
      ),
    );
  }
}