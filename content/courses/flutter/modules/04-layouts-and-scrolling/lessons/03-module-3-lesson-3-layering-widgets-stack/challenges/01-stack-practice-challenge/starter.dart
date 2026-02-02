// Profile Header Challenge
// Use Stack to layer widgets on top of each other

import 'package:flutter/material.dart';

void main() {
  runApp(const ProfileHeaderApp());
}

class ProfileHeaderApp extends StatelessWidget {
  const ProfileHeaderApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        body: Column(
          children: const [
            ProfileHeader(),
            Expanded(
              child: Center(child: Text('Profile Content')),
            ),
          ],
        ),
      ),
    );
  }
}

class ProfileHeader extends StatelessWidget {
  const ProfileHeader({super.key});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 250,
      // TODO: Use Stack to layer widgets
      child: Stack(
        children: [
          // TODO 1: Background image using Positioned.fill
          // Image.network('https://picsum.photos/800/400', fit: BoxFit.cover)
          
          // TODO 2: Profile photo (CircleAvatar) positioned in center
          
          // TODO 3: Name text positioned at bottom
        ],
      ),
    );
  }
}