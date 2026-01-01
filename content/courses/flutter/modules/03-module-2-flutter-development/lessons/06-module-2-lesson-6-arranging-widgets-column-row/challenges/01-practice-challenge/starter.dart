// Social Media Post Challenge
// Build a post layout with profile, text, and action buttons

import 'package:flutter/material.dart';

void main() {
  runApp(const SocialMediaApp());
}

class SocialMediaApp extends StatelessWidget {
  const SocialMediaApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Social Feed')),
        body: Card(
          margin: const EdgeInsets.all(16),
          child: Padding(
            padding: const EdgeInsets.all(16),
            // TODO: Use Column to arrange sections vertically
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisSize: MainAxisSize.min,
              children: [
                // TODO 1: Top Row with CircleAvatar and name Text
                
                const SizedBox(height: 16),
                
                // TODO 2: Middle - Post text content
                
                const SizedBox(height: 16),
                
                // TODO 3: Bottom Row with Like, Comment, Share buttons
                // Hint: Use mainAxisAlignment: MainAxisAlignment.spaceAround
              ],
            ),
          ),
        ),
      ),
    );
  }
}