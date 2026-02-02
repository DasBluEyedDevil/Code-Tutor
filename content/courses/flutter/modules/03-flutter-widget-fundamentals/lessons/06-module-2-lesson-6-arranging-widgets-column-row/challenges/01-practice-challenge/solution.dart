// Solution: Social Media Post Layout
// Uses Column for vertical and Row for horizontal arrangement

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
        body: const SocialMediaPost(),
      ),
    );
  }
}

class SocialMediaPost extends StatelessWidget {
  const SocialMediaPost({super.key});

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: const EdgeInsets.all(16),
      child: Padding(
        padding: const EdgeInsets.all(16),
        // Main Column: arranges sections vertically
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisSize: MainAxisSize.min,
          children: [
            // 1. Top Row: Profile photo + name
            Row(
              children: [
                const CircleAvatar(
                  radius: 24,
                  backgroundImage: NetworkImage(
                    'https://picsum.photos/100/100',
                  ),
                ),
                const SizedBox(width: 12),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: const [
                    Text(
                      'Jane Developer',
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: 16,
                      ),
                    ),
                    Text(
                      '2 hours ago',
                      style: TextStyle(color: Colors.grey, fontSize: 12),
                    ),
                  ],
                ),
              ],
            ),
            const SizedBox(height: 16),
            
            // 2. Middle: Post text
            const Text(
              'Just finished building my first Flutter app! The widget system is amazing - everything just clicks together like LEGO blocks.',
              style: TextStyle(fontSize: 15),
            ),
            const SizedBox(height: 16),
            
            // 3. Bottom Row: Action buttons
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: [
                _buildActionButton(Icons.thumb_up_outlined, 'Like', '42'),
                _buildActionButton(Icons.comment_outlined, 'Comment', '8'),
                _buildActionButton(Icons.share_outlined, 'Share', ''),
              ],
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildActionButton(IconData icon, String label, String count) {
    return TextButton.icon(
      onPressed: () {},
      icon: Icon(icon, size: 20),
      label: Text(count.isNotEmpty ? '$label ($count)' : label),
    );
  }
}

// Layout structure:
// Column (vertical)
//   -> Row (horizontal): Avatar + Name
//   -> Text: Post content
//   -> Row (horizontal): Action buttons