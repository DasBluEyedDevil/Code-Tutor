// Solution: Reusable CommentWidget in ListView
// Creates a reusable comment widget and displays 5 comments

import 'package:flutter/material.dart';

void main() {
  runApp(const CommentsApp());
}

class CommentsApp extends StatelessWidget {
  const CommentsApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Comments')),
        body: const CommentsList(),
      ),
    );
  }
}

// Reusable CommentWidget
class CommentWidget extends StatelessWidget {
  final String authorName;
  final String authorAvatar;
  final String commentText;
  final String timestamp;
  final int likes;

  const CommentWidget({
    super.key,
    required this.authorName,
    required this.authorAvatar,
    required this.commentText,
    required this.timestamp,
    this.likes = 0,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // Author avatar
          CircleAvatar(
            radius: 20,
            backgroundImage: NetworkImage(authorAvatar),
          ),
          const SizedBox(width: 12),
          
          // Comment content
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // Author name and timestamp
                Row(
                  children: [
                    Text(
                      authorName,
                      style: const TextStyle(fontWeight: FontWeight.bold),
                    ),
                    const SizedBox(width: 8),
                    Text(
                      timestamp,
                      style: TextStyle(color: Colors.grey.shade600, fontSize: 12),
                    ),
                  ],
                ),
                const SizedBox(height: 4),
                
                // Comment text
                Text(commentText),
                const SizedBox(height: 8),
                
                // Like button
                Row(
                  children: [
                    Icon(Icons.thumb_up_outlined, size: 16, color: Colors.grey),
                    const SizedBox(width: 4),
                    Text('$likes', style: TextStyle(color: Colors.grey)),
                    const SizedBox(width: 16),
                    Text('Reply', style: TextStyle(color: Colors.blue)),
                  ],
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

// Display 5 comments using ListView
class CommentsList extends StatelessWidget {
  const CommentsList({super.key});

  static const List<Map<String, dynamic>> comments = [
    {'name': 'Alice', 'text': 'Great article! Very helpful.', 'time': '2h ago', 'likes': 12},
    {'name': 'Bob', 'text': 'I learned a lot from this. Thanks!', 'time': '3h ago', 'likes': 8},
    {'name': 'Carol', 'text': 'Can you explain the Stack widget more?', 'time': '5h ago', 'likes': 3},
    {'name': 'David', 'text': 'Flutter is amazing!', 'time': '1d ago', 'likes': 25},
    {'name': 'Eve', 'text': 'Following for more content like this.', 'time': '2d ago', 'likes': 5},
  ];

  @override
  Widget build(BuildContext context) {
    return ListView.separated(
      itemCount: comments.length,
      separatorBuilder: (_, __) => const Divider(),
      itemBuilder: (context, index) {
        final comment = comments[index];
        return CommentWidget(
          authorName: comment['name'],
          authorAvatar: 'https://picsum.photos/100?${index + 1}',
          commentText: comment['text'],
          timestamp: comment['time'],
          likes: comment['likes'],
        );
      },
    );
  }
}

// Key concepts:
// - Reusable widget with constructor parameters
// - ListView.separated for dividers between items
// - Flexible layout with Row and Expanded