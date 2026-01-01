// Business Card Widget
// TODO: Create a styled business card

import 'package:flutter/material.dart';

void main() {
  runApp(MaterialApp(home: Scaffold(body: Center(child: BusinessCard()))));
}

class BusinessCard extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Container(
      // TODO: Add width and padding
      // TODO: Add BoxDecoration with:
      //   - color or gradient
      //   - borderRadius
      //   - boxShadow
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          // TODO: Add name, title, email Text widgets
        ],
      ),
    );
  }
}