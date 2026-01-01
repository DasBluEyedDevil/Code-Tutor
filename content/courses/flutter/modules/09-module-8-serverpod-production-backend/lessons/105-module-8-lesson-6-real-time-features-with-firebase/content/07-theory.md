---
type: "THEORY"
title: "Part 3: Typing Indicator"
---


Show when someone is typing!

### Typing Service


### Add to Chat Screen




```dart
// In ChatScreen, add typing indicator
Widget _buildTypingIndicator() {
  return StreamBuilder<bool>(
    stream: _typingService.getTypingStatus(
      chatRoomId: _chatRoomId,
      otherUserId: widget.otherUserId,
    ),
    builder: (context, snapshot) {
      if (snapshot.data == true) {
        return Padding(
          padding: const EdgeInsets.all(8.0),
          child: Row(
            children: [
              const SizedBox(width: 16),
              ...List.generate(
                3,
                (index) => Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 2),
                  child: _AnimatedDot(delay: index * 200),
                ),
              ),
              const SizedBox(width: 8),
              Text(
                '${widget.otherUserName} is typing...',
                style: TextStyle(
                  color: Colors.grey.shade600,
                  fontStyle: FontStyle.italic,
                ),
              ),
            ],
          ),
        );
      }
      return const SizedBox.shrink();
    },
  );
}

// Animated dot widget
class _AnimatedDot extends StatefulWidget {
  final int delay;

  const _AnimatedDot({required this.delay});

  @override
  State<_AnimatedDot> createState() => _AnimatedDotState();
}

class _AnimatedDotState extends State<_AnimatedDot>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 600),
    )..repeat();

    Future.delayed(Duration(milliseconds: widget.delay), () {
      if (mounted) _controller.forward();
    });
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return FadeTransition(
      opacity: _controller,
      child: Container(
        width: 8,
        height: 8,
        decoration: BoxDecoration(
          color: Colors.grey.shade400,
          shape: BoxShape.circle,
        ),
      ),
    );
  }
}

// Update TextField to track typing
TextField(
  controller: _messageController,
  onChanged: (text) {
    _typingService.setTyping(
      chatRoomId: _chatRoomId,
      userId: _authService.currentUser!.uid,
      isTyping: text.isNotEmpty,
    );
  },
  // ... rest of TextField
)
```
