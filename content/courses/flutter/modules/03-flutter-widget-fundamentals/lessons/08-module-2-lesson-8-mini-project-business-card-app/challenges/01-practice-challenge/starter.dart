// Responsive Layout Challenge
// Create a layout that adapts to different screen sizes

import 'package:flutter/material.dart';

void main() {
  runApp(const ResponsiveApp());
}

class ResponsiveApp extends StatelessWidget {
  const ResponsiveApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Responsive Layout')),
        body: const ResponsiveContent(),
      ),
    );
  }
}

class ResponsiveContent extends StatelessWidget {
  const ResponsiveContent({super.key});

  @override
  Widget build(BuildContext context) {
    // TODO: Use MediaQuery.sizeOf(context) to get screen dimensions
    final size = MediaQuery.sizeOf(context);
    final screenWidth = size.width;
    
    // TODO: Create responsive values based on screen size
    // Hint: isSmallScreen = screenWidth < 600
    
    return SingleChildScrollView(
      padding: const EdgeInsets.all(16),
      child: Column(
        children: [
          // TODO: Display screen info
          Text('Screen width: ${screenWidth.toStringAsFixed(0)}px'),
          
          const SizedBox(height: 16),
          
          // TODO: Create cards that adapt to screen size
          // Use Wrap widget for responsive grid behavior
        ],
      ),
    );
  }
}