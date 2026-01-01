class TypingIndicator extends StatefulWidget {
  final List<TypingUser> typingUsers;
  
  const TypingIndicator({required this.typingUsers, super.key});
  
  @override
  State<TypingIndicator> createState() => _TypingIndicatorState();
}

class _TypingIndicatorState extends State<TypingIndicator>
    with TickerProviderStateMixin {
  // TODO: Add AnimationControllers for dot animations
  
  @override
  void initState() {
    super.initState();
    // TODO: Initialize animations
  }
  
  @override
  Widget build(BuildContext context) {
    if (widget.typingUsers.isEmpty) {
      return const SizedBox.shrink();
    }
    
    // TODO: Build the typing indicator UI
    // - Show avatar(s) of typing user(s)
    // - Show animated dots
    // - Show 'is typing...' or 'are typing...' text
    throw UnimplementedError();
  }
  
  Widget _buildAnimatedDots() {
    // TODO: Create three dots with staggered bounce animations
    throw UnimplementedError();
  }
  
  String _buildTypingText() {
    // TODO: Return appropriate text based on number of typing users
    // 1 user: 'Alice is typing...'
    // 2 users: 'Alice and Bob are typing...'
    // 3+ users: 'Alice and 2 others are typing...'
    throw UnimplementedError();
  }
  
  @override
  void dispose() {
    // TODO: Dispose animation controllers
    super.dispose();
  }
}

class TypingUser {
  final String id;
  final String name;
  final String? avatarUrl;
  
  TypingUser({required this.id, required this.name, this.avatarUrl});
}