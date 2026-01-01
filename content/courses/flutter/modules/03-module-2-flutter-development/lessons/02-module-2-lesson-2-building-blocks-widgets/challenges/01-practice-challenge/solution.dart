// Solution: Nested Widgets Demo
// This demonstrates widget nesting with 5+ different widgets

import 'package:flutter/material.dart';

void main() {
  runApp(const NestedWidgetsApp());
}

class NestedWidgetsApp extends StatelessWidget {
  const NestedWidgetsApp({super.key});

  @override
  Widget build(BuildContext context) {
    // Widget Tree Structure:
    // MaterialApp (Widget 1)
    //   -> Scaffold (Widget 2)
    //        -> Center (Widget 3)
    //             -> Card (Widget 4)
    //                  -> Padding (Widget 5)
    //                       -> Column (Widget 6)
    //                            -> Icon, Text, SizedBox (Widgets 7-9)
    
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(
          title: const Text('Nested Widgets'),
        ),
        body: Center(
          child: Card(
            elevation: 4,
            child: Padding(
              padding: const EdgeInsets.all(24),
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: const [
                  Icon(
                    Icons.flutter_dash,
                    size: 64,
                    color: Colors.blue,
                  ),
                  SizedBox(height: 16),
                  Text(
                    'Hello Flutter!',
                    style: TextStyle(
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  SizedBox(height: 8),
                  Text(
                    'Widgets nest inside each other',
                    style: TextStyle(color: Colors.grey),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

// Key concepts:
// - Widgets nest inside each other like Russian dolls
// - Each widget has a specific purpose (layout, styling, content)
// - The 'child' property connects widgets together