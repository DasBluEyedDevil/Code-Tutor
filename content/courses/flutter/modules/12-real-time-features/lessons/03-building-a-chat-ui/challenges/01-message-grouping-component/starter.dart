enum GroupPosition { first, middle, last, only }

class MessageGrouper {
  // TODO: Implement method to determine message group positions
  static List<GroupedMessage> groupMessages(List<ChatMessage> messages) {
    throw UnimplementedError();
  }
  
  static GroupPosition getPosition(
    ChatMessage current,
    ChatMessage? previous,
    ChatMessage? next,
  ) {
    // TODO: Determine if this message is first, middle, last, or only
    // in its group based on sender ID
    throw UnimplementedError();
  }
}

class GroupedMessage {
  final ChatMessage message;
  final GroupPosition position;
  final bool showAvatar;
  final bool showName;
  final bool showTimestamp;
  
  GroupedMessage({
    required this.message,
    required this.position,
    required this.showAvatar,
    required this.showName,
    required this.showTimestamp,
  });
}

class GroupedMessageBubble extends StatelessWidget {
  final GroupedMessage groupedMessage;
  
  const GroupedMessageBubble({required this.groupedMessage, super.key});
  
  @override
  Widget build(BuildContext context) {
    // TODO: Implement bubble with position-based styling
    throw UnimplementedError();
  }
  
  BorderRadius _getBorderRadius(bool isMe, GroupPosition position) {
    // TODO: Return appropriate border radius based on position
    // First: rounded top, squared bottom on sender's side
    // Middle: squared on sender's side
    // Last: squared top, rounded bottom on sender's side
    // Only: fully rounded
    throw UnimplementedError();
  }
}