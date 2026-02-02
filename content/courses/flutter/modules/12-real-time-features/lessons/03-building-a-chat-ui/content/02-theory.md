---
type: "THEORY"
title: "Message List"
---

The message list is the heart of your chat UI. We use ListView.builder with reverse scrolling so new messages appear at the bottom while older messages load as users scroll up.

**Reverse Scrolling Pattern:**

By setting `reverse: true` on ListView, the list starts at the bottom. This means:
- Index 0 is the most recent message (at bottom)
- New messages are inserted at index 0
- Scrolling up loads older messages
- No need to calculate scroll position for new messages

**Performance Optimization:**

- Use `itemExtent` or `prototypeItem` if messages are similar height
- Implement pagination to load messages in chunks
- Use `findChildIndexCallback` for efficient reordering
- Consider `CustomScrollView` with `SliverList` for complex layouts



```dart
class ChatScreen extends StatefulWidget {
  final String conversationId;
  
  const ChatScreen({required this.conversationId, super.key});
  
  @override
  State<ChatScreen> createState() => _ChatScreenState();
}

class _ChatScreenState extends State<ChatScreen> {
  final ScrollController _scrollController = ScrollController();
  final List<ChatMessage> _messages = [];
  bool _isLoadingMore = false;
  bool _hasMoreMessages = true;
  
  @override
  void initState() {
    super.initState();
    _loadInitialMessages();
    _scrollController.addListener(_onScroll);
  }
  
  void _onScroll() {
    // Load more when near the top (which is the end in reverse mode)
    if (_scrollController.position.pixels >=
        _scrollController.position.maxScrollExtent - 200) {
      _loadMoreMessages();
    }
  }
  
  Future<void> _loadInitialMessages() async {
    final messages = await _chatService.getMessages(
      widget.conversationId,
      limit: 50,
    );
    setState(() {
      _messages.addAll(messages);
    });
  }
  
  Future<void> _loadMoreMessages() async {
    if (_isLoadingMore || !_hasMoreMessages) return;
    
    setState(() => _isLoadingMore = true);
    
    final oldestMessage = _messages.last;
    final moreMessages = await _chatService.getMessages(
      widget.conversationId,
      before: oldestMessage.timestamp,
      limit: 50,
    );
    
    setState(() {
      _isLoadingMore = false;
      if (moreMessages.isEmpty) {
        _hasMoreMessages = false;
      } else {
        _messages.addAll(moreMessages);
      }
    });
  }
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: _buildAppBar(),
      body: Column(
        children: [
          Expanded(
            child: ListView.builder(
              controller: _scrollController,
              reverse: true, // Start from bottom
              padding: const EdgeInsets.symmetric(
                horizontal: 16,
                vertical: 8,
              ),
              itemCount: _messages.length + (_isLoadingMore ? 1 : 0),
              itemBuilder: (context, index) {
                if (index == _messages.length) {
                  return const Center(
                    child: Padding(
                      padding: EdgeInsets.all(16),
                      child: CircularProgressIndicator(),
                    ),
                  );
                }
                
                final message = _messages[index];
                final previousMessage = index < _messages.length - 1
                    ? _messages[index + 1]
                    : null;
                
                return MessageBubble(
                  message: message,
                  showAvatar: _shouldShowAvatar(message, previousMessage),
                  showTimestamp: _shouldShowTimestamp(message, previousMessage),
                );
              },
            ),
          ),
          ChatInputField(
            onSendMessage: _handleSendMessage,
          ),
        ],
      ),
    );
  }
  
  bool _shouldShowAvatar(
    ChatMessage current,
    ChatMessage? previous,
  ) {
    if (previous == null) return true;
    return current.senderId != previous.senderId;
  }
  
  bool _shouldShowTimestamp(
    ChatMessage current,
    ChatMessage? previous,
  ) {
    if (previous == null) return true;
    final diff = previous.timestamp.difference(current.timestamp);
    return diff.inMinutes > 5;
  }
  
  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }
}
```
