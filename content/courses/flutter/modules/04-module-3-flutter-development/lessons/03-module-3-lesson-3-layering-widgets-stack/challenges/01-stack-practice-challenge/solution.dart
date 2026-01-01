// Solution: Profile Header with Stack
// Uses Stack to overlay profile photo and name on background

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
              child: Center(
                child: Text('Profile Content'),
              ),
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
      child: Stack(
        clipBehavior: Clip.none,
        children: [
          // 1. Background image (fills entire Stack)
          Positioned.fill(
            child: Image.network(
              'https://picsum.photos/800/400',
              fit: BoxFit.cover,
            ),
          ),
          
          // Gradient overlay for better text visibility
          Positioned.fill(
            child: Container(
              decoration: BoxDecoration(
                gradient: LinearGradient(
                  begin: Alignment.topCenter,
                  end: Alignment.bottomCenter,
                  colors: [
                    Colors.transparent,
                    Colors.black.withOpacity(0.7),
                  ],
                ),
              ),
            ),
          ),
          
          // 2. Profile photo (centered, overlapping bottom)
          Positioned(
            bottom: -50, // Extends below the header
            left: 0,
            right: 0,
            child: Center(
              child: Container(
                decoration: BoxDecoration(
                  shape: BoxShape.circle,
                  border: Border.all(color: Colors.white, width: 4),
                ),
                child: const CircleAvatar(
                  radius: 50,
                  backgroundImage: NetworkImage(
                    'https://picsum.photos/200/200',
                  ),
                ),
              ),
            ),
          ),
          
          // 3. Name overlay at bottom
          Positioned(
            bottom: 60,
            left: 0,
            right: 0,
            child: Column(
              children: const [
                Text(
                  'Jane Developer',
                  style: TextStyle(
                    color: Colors.white,
                    fontSize: 24,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                SizedBox(height: 4),
                Text(
                  'Flutter Enthusiast',
                  style: TextStyle(
                    color: Colors.white70,
                    fontSize: 14,
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

// Key concepts:
// - Stack: Overlays widgets on top of each other
// - Positioned: Places widgets at specific locations
// - Positioned.fill: Fills entire Stack
// - clipBehavior: Allows content to overflow