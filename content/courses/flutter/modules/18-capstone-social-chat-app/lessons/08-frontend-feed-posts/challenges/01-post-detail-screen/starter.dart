// lib/features/feed/presentation/screens/post_detail_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../../shared/models/post.dart';
import '../../../../shared/models/comment.dart';

class PostDetailScreen extends ConsumerStatefulWidget {
  final String postId;

  const PostDetailScreen({super.key, required this.postId});

  @override
  ConsumerState<PostDetailScreen> createState() => _PostDetailScreenState();
}

class _PostDetailScreenState extends ConsumerState<PostDetailScreen> {
  final _commentController = TextEditingController();
  final _scrollController = ScrollController();

  @override
  void dispose() {
    _commentController.dispose();
    _scrollController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    // TODO: Implement post detail screen
    // 1. Fetch post details by ID
    // 2. Display full post content
    // 3. Show comments list with pagination
    // 4. Add comment input at bottom
    // 5. Implement share functionality
    throw UnimplementedError();
  }

  Future<void> _addComment() async {
    // TODO: Implement adding a comment
    // 1. Validate comment text
    // 2. Optimistically add comment to list
    // 3. Call API to create comment
    // 4. Handle success/failure
    throw UnimplementedError();
  }

  void _sharePost(Post post) {
    // TODO: Implement share functionality
    // 1. Use share_plus package
    // 2. Generate shareable link
    // 3. Show platform share sheet
    throw UnimplementedError();
  }
}

// lib/features/feed/presentation/widgets/comment_item.dart
class CommentItem extends StatelessWidget {
  final Comment comment;
  final VoidCallback onReply;
  final VoidCallback onLike;

  const CommentItem({
    super.key,
    required this.comment,
    required this.onReply,
    required this.onLike,
  });

  @override
  Widget build(BuildContext context) {
    // TODO: Implement comment item widget
    // 1. Show user avatar and name
    // 2. Display comment content
    // 3. Show timestamp and like count
    // 4. Add reply and like buttons
    // 5. Support nested replies
    throw UnimplementedError();
  }
}