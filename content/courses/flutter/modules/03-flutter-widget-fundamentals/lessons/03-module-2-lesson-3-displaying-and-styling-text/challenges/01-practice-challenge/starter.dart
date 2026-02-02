// Text Styling Profile Challenge
// TODO: Style each text differently

import 'package:flutter/material.dart';

void main() {
  runApp(MaterialApp(
    home: Scaffold(
      appBar: AppBar(title: Text('My Profile')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            // TODO: Add your name (large, bold)
            Text('Your Name', style: TextStyle(fontSize: 32)),
            
            // TODO: Add your age (medium, colored)
            Text('Age: XX'),
            
            // TODO: Add a quote (italic)
            Text('Your favorite quote'),
            
            // TODO: Add a fun fact (underlined)
            Text('A fun fact about you'),
          ],
        ),
      ),
    ),
  ));
}