// Refactor this code to use dot shorthands
// where applicable!

import 'package:flutter/material.dart';

class ProfileCard extends StatelessWidget {
  const ProfileCard({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      alignment: Alignment.center,
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            'Jane Developer',
            textAlign: TextAlign.left,
            style: TextStyle(
              fontWeight: FontWeight.bold,
              fontStyle: FontStyle.italic,
            ),
          ),
          Image.asset(
            'profile.png',
            fit: BoxFit.cover,
          ),
          Container(
            color: Colors.amber, // Don't change this!
            alignment: Alignment.centerLeft,
            child: Text('Flutter Expert'),
          ),
        ],
      ),
    );
  }
}