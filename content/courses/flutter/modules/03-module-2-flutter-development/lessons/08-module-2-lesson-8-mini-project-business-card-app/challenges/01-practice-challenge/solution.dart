// Solution: Responsive Spacing Demo
// Adjusts padding and spacing based on screen size

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
    // Get screen dimensions
    final size = MediaQuery.sizeOf(context);
    final screenWidth = size.width;
    final screenHeight = size.height;
    
    // Calculate responsive values
    final isSmallScreen = screenWidth < 600;
    final horizontalPadding = screenWidth * 0.05; // 5% of screen width
    final verticalSpacing = isSmallScreen ? 12.0 : 24.0;
    final fontSize = isSmallScreen ? 16.0 : 20.0;
    
    return SingleChildScrollView(
      padding: EdgeInsets.symmetric(
        horizontal: horizontalPadding,
        vertical: 16,
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // Display screen info
          Card(
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    'Screen Info',
                    style: TextStyle(
                      fontSize: fontSize,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  SizedBox(height: verticalSpacing),
                  Text('Width: ${screenWidth.toStringAsFixed(0)}px'),
                  Text('Height: ${screenHeight.toStringAsFixed(0)}px'),
                  Text('Device: ${isSmallScreen ? "Mobile" : "Tablet/Desktop"}'),
                ],
              ),
            ),
          ),
          SizedBox(height: verticalSpacing),
          
          // Responsive grid
          Text(
            'Responsive Cards',
            style: TextStyle(fontSize: fontSize, fontWeight: FontWeight.bold),
          ),
          SizedBox(height: verticalSpacing),
          
          // Use Wrap for responsive grid behavior
          Wrap(
            spacing: verticalSpacing,
            runSpacing: verticalSpacing,
            children: List.generate(6, (index) {
              return SizedBox(
                width: isSmallScreen
                    ? (screenWidth - horizontalPadding * 2 - verticalSpacing) / 2
                    : (screenWidth - horizontalPadding * 2 - verticalSpacing * 2) / 3,
                child: Card(
                  child: Padding(
                    padding: EdgeInsets.all(isSmallScreen ? 12 : 20),
                    child: Column(
                      children: [
                        Icon(Icons.widgets, size: isSmallScreen ? 32 : 48),
                        SizedBox(height: verticalSpacing / 2),
                        Text('Card ${index + 1}'),
                      ],
                    ),
                  ),
                ),
              );
            }),
          ),
        ],
      ),
    );
  }
}

// Responsive techniques:
// - MediaQuery for screen dimensions
// - Conditional values based on screen size
// - Percentage-based padding
// - Wrap widget for flexible grid