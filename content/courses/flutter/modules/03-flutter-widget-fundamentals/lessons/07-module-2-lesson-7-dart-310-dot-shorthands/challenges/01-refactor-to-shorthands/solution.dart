// Refactored to use dot shorthands
// Note: Colors.amber stays verbose (not an enum)

import 'package:flutter/material.dart';

class ProfileCard extends StatelessWidget {
  const ProfileCard({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      alignment: .center,
      child: Column(
        mainAxisAlignment: .center,
        crossAxisAlignment: .start,
        children: [
          Text(
            'Jane Developer',
            textAlign: .left,
            style: TextStyle(
              fontWeight: .bold,
              fontStyle: .italic,
            ),
          ),
          Image.asset(
            'profile.png',
            fit: .cover,
          ),
          Container(
            color: Colors.amber, // Stays verbose - not an enum!
            alignment: .centerLeft,
            child: Text('Flutter Expert'),
          ),
        ],
      ),
    );
  }
}