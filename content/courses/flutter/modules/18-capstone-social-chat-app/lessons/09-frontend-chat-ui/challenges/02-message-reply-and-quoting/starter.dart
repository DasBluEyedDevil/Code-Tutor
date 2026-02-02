// lib/features/chat/presentation/widgets/swipeable_message.dart
import 'package:flutter/material.dart';
import '../../domain/message.dart';

class SwipeableMessage extends StatefulWidget {
  final Widget child;
  final Message message;
  final Function(Message) onReply;

  const SwipeableMessage({
    super.key,
    required this.child,
    required this.message,
    required this.onReply,
  });

  @override
  State<SwipeableMessage> createState() => _SwipeableMessageState();
}

class _SwipeableMessageState extends State<SwipeableMessage>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  double _dragExtent = 0;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 200),
    );
  }

  @override
  Widget build(BuildContext context) {
    // TODO: Implement swipe-to-reply
    // 1. GestureDetector with horizontal drag
    // 2. Reveal reply icon as user swipes
    // 3. Trigger reply at threshold
    // 4. Animate back to original position
    // 5. Haptic feedback at threshold
    throw UnimplementedError();
  }
}

// lib/features/chat/presentation/widgets/reply_preview_bar.dart
class ReplyPreviewBar extends StatelessWidget {
  final Message replyingTo;
  final VoidCallback onCancel;

  const ReplyPreviewBar({
    super.key,
    required this.replyingTo,
    required this.onCancel,
  });

  @override
  Widget build(BuildContext context) {
    // TODO: Implement reply preview bar
    // 1. Show quoted message content preview
    // 2. Display sender name
    // 3. Cancel button to dismiss
    // 4. Visual connection to input field
    throw UnimplementedError();
  }
}

// lib/features/chat/presentation/widgets/quoted_message.dart
class QuotedMessage extends StatelessWidget {
  final Message? quotedMessage;
  final VoidCallback? onTap;
  final bool isFromMe;

  const QuotedMessage({
    super.key,
    required this.quotedMessage,
    this.onTap,
    this.isFromMe = false,
  });

  @override
  Widget build(BuildContext context) {
    // TODO: Implement quoted message display
    // 1. Compact preview of original message
    // 2. Colored left border indicating sender
    // 3. Tap to scroll to original
    // 4. Handle deleted message case
    throw UnimplementedError();
  }
}