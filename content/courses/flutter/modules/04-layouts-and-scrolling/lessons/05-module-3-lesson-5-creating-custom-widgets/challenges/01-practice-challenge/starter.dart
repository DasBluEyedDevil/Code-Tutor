// Custom CommentWidget Challenge
// Create a reusable comment widget

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

// TODO: Create a reusable CommentWidget class
// It should have these properties:
// - authorName (String)
// - commentText (String)
// - timestamp (String)
class CommentWidget extends StatelessWidget {
  final String authorName;
  final String commentText;
  final String timestamp;

  const CommentWidget({
    super.key,
    required this.authorName,
    required this.commentText,
    required this.timestamp,
  });

  @override
  Widget build(BuildContext context) {
    // TODO: Build the comment UI
    // Hint: Use Row with CircleAvatar and Column
    return const Placeholder();
  }
}

class CommentsList extends StatelessWidget {
  const CommentsList({super.key});

  @override
  Widget build(BuildContext context) {
    // TODO: Use ListView.builder to display 5 CommentWidget instances
    return ListView(
      children: const [
        // Add CommentWidget instances here
      ],
    );
  }
}