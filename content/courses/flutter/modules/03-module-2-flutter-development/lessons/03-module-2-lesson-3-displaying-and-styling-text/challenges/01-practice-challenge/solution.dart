// Solution: Text Styling Profile Card
// Demonstrates various TextStyle properties

import 'package:flutter/material.dart';

void main() {
  runApp(const ProfileApp());
}

class ProfileApp extends StatelessWidget {
  const ProfileApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('My Profile')),
        body: Center(
          child: Padding(
            padding: const EdgeInsets.all(24),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                // 1. Name - Large, bold text (Color 1: Deep Purple)
                const Text(
                  'Jane Developer',
                  style: TextStyle(
                    fontSize: 32,
                    fontWeight: FontWeight.bold,
                    color: Colors.deepPurple,
                  ),
                ),
                const SizedBox(height: 16),
                
                // 2. Age - Medium, colored text (Color 2: Teal)
                const Text(
                  'Age: 28',
                  style: TextStyle(
                    fontSize: 20,
                    color: Colors.teal,
                  ),
                ),
                const SizedBox(height: 24),
                
                // 3. Quote - Italic text (Color 3: Grey)
                const Text(
                  '"Code is poetry written for machines"',
                  style: TextStyle(
                    fontSize: 18,
                    fontStyle: FontStyle.italic,
                    color: Colors.grey,
                  ),
                  textAlign: TextAlign.center,
                ),
                const SizedBox(height: 24),
                
                // 4. Fun fact - Underlined text (Color 4: Orange)
                const Text(
                  'Fun fact: I love building Flutter apps!',
                  style: TextStyle(
                    fontSize: 16,
                    color: Colors.orange,
                    decoration: TextDecoration.underline,
                    decorationColor: Colors.orange,
                  ),
                ),
                const SizedBox(height: 16),
                
                // Bonus: Combining multiple styles
                const Text(
                  'Flutter Developer',
                  style: TextStyle(
                    fontSize: 14,
                    color: Colors.blue,
                    fontWeight: FontWeight.w500,
                    letterSpacing: 2,
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}

// TextStyle properties used:
// - fontSize: Size of text
// - fontWeight: Bold/light/normal
// - fontStyle: Italic/normal
// - color: Text color
// - decoration: Underline/strikethrough
// - letterSpacing: Space between letters