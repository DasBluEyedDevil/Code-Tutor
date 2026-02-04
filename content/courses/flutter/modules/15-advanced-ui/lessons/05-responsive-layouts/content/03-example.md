---
type: "EXAMPLE"
title: "Using MediaQuery for Responsive Design"
---


Adapt layouts based on screen characteristics:



```dart
import 'package:flutter/material.dart';

class ResponsiveHomePage extends StatelessWidget {
  const ResponsiveHomePage({super.key});

  @override
  Widget build(BuildContext context) {
    // Get screen dimensions
    final size = MediaQuery.sizeOf(context);
    final width = size.width;
    final height = size.height;
    
    // Get orientation
    final orientation = MediaQuery.orientationOf(context);
    final isLandscape = orientation == Orientation.landscape;
    
    // Get safe area padding (notch, status bar, etc.)
    final padding = MediaQuery.paddingOf(context);
    
    // Get text scaler for accessibility
    final textScaler = MediaQuery.textScalerOf(context);
    
    return Scaffold(
      appBar: AppBar(
        title: Text('Screen: ${width.toInt()} x ${height.toInt()}'),
      ),
      body: SafeArea(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text('Orientation: ${isLandscape ? "Landscape" : "Portrait"}'),
                  Text('Safe area top: ${padding.top}'),
                  Text('Text scaler: $textScaler'),
                ],
              ),
            ),
            
            Expanded(
              child: _buildResponsiveContent(width),
            ),
          ],
        ),
      ),
    );
  }
  
  Widget _buildResponsiveContent(double width) {
    // Simple breakpoint-based layout
    if (width < 600) {
      // Phone: Single column
      return ListView(
        children: List.generate(10, (i) => _buildCard('Item $i')),
      );
    } else if (width < 900) {
      // Tablet: Two columns
      return GridView.count(
        crossAxisCount: 2,
        children: List.generate(10, (i) => _buildCard('Item $i')),
      );
    } else {
      // Desktop: Three columns
      return GridView.count(
        crossAxisCount: 3,
        children: List.generate(10, (i) => _buildCard('Item $i')),
      );
    }
  }
  
  Widget _buildCard(String title) {
    return Card(
      margin: const EdgeInsets.all(8),
      child: Center(child: Text(title)),
    );
  }
}

// Handling keyboard appearance
class KeyboardAwareWidget extends StatelessWidget {
  const KeyboardAwareWidget({super.key});

  @override
  Widget build(BuildContext context) {
    // viewInsets.bottom > 0 when keyboard is visible
    final keyboardHeight = MediaQuery.viewInsetsOf(context).bottom;
    final isKeyboardVisible = keyboardHeight > 0;
    
    return Column(
      children: [
        Expanded(
          child: ListView(
            children: [
              // Content
            ],
          ),
        ),
        // Hide bottom bar when keyboard is visible
        if (!isKeyboardVisible)
          Container(
            height: 60,
            color: Colors.blue,
            child: const Center(child: Text('Bottom Bar')),
          ),
      ],
    );
  }
}
```
