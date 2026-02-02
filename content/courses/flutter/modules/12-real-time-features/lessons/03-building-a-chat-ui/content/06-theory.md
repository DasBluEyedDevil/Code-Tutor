---
type: "THEORY"
title: "Scroll Behavior"
---

Intelligent scroll behavior is crucial for a good chat experience. The app should auto-scroll for new messages when viewing the latest, but preserve position when reading history.

**Scroll-to-Bottom Button:**

When users scroll up to read history, show a floating button to jump back to the latest messages. This button should also show a badge with the count of new unread messages.

**Auto-Scroll Logic:**

- Auto-scroll when user is at or near the bottom
- Don't auto-scroll when user is reading history
- Animate scrolling for smooth UX
- Update unread count when scrolled away



```dart
class _ChatScreenState extends State<ChatScreen> {
  final ScrollController _scrollController = ScrollController();
  bool _showScrollToBottom = false;
  int _unreadCount = 0;
  
  // Threshold for "near bottom" detection (in pixels)
  static const double _scrollThreshold = 150;
  
  @override
  void initState() {
    super.initState();
    _scrollController.addListener(_onScrollChanged);
  }
  
  void _onScrollChanged() {
    final isNearBottom = _scrollController.position.pixels < _scrollThreshold;
    
    if (isNearBottom != !_showScrollToBottom) {
      setState(() {
        _showScrollToBottom = !isNearBottom;
        if (isNearBottom) {
          _unreadCount = 0; // Clear unread when at bottom
        }
      });
    }
  }
  
  bool get _isNearBottom {
    if (!_scrollController.hasClients) return true;
    return _scrollController.position.pixels < _scrollThreshold;
  }
  
  void _scrollToBottom({bool animated = true}) {
    if (!_scrollController.hasClients) return;
    
    if (animated) {
      _scrollController.animateTo(
        0, // In reverse mode, 0 is the bottom
        duration: const Duration(milliseconds: 300),
        curve: Curves.easeOut,
      );
    } else {
      _scrollController.jumpTo(0);
    }
  }
  
  void _handleNewMessage(ChatMessage message) {
    setState(() {
      _messages.insert(0, message);
      
      // Increment unread if not at bottom and message is from others
      if (_showScrollToBottom && message.senderId != currentUserId) {
        _unreadCount++;
      }
    });
    
    // Auto-scroll if near bottom
    if (_isNearBottom) {
      // Use addPostFrameCallback to scroll after the new item is rendered
      WidgetsBinding.instance.addPostFrameCallback((_) {
        _scrollToBottom();
      });
    }
  }
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: _buildAppBar(),
      body: Stack(
        children: [
          Column(
            children: [
              Expanded(
                child: ListView.builder(
                  controller: _scrollController,
                  reverse: true,
                  padding: const EdgeInsets.symmetric(
                    horizontal: 16,
                    vertical: 8,
                  ),
                  itemCount: _messages.length,
                  itemBuilder: (context, index) {
                    return MessageBubble(message: _messages[index]);
                  },
                ),
              ),
              ChatInputField(onSendMessage: _handleSendMessage),
            ],
          ),
          
          // Scroll to bottom FAB
          if (_showScrollToBottom)
            Positioned(
              right: 16,
              bottom: 80, // Above the input field
              child: _ScrollToBottomButton(
                unreadCount: _unreadCount,
                onPressed: () {
                  _scrollToBottom();
                  setState(() {
                    _unreadCount = 0;
                  });
                },
              ),
            ),
        ],
      ),
    );
  }
}

class _ScrollToBottomButton extends StatelessWidget {
  final int unreadCount;
  final VoidCallback onPressed;
  
  const _ScrollToBottomButton({
    required this.unreadCount,
    required this.onPressed,
  });
  
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    
    return Stack(
      clipBehavior: Clip.none,
      children: [
        FloatingActionButton.small(
          onPressed: onPressed,
          backgroundColor: theme.colorScheme.surfaceVariant,
          foregroundColor: theme.colorScheme.onSurfaceVariant,
          elevation: 2,
          child: const Icon(Icons.keyboard_arrow_down),
        ),
        if (unreadCount > 0)
          Positioned(
            right: -4,
            top: -4,
            child: Container(
              padding: const EdgeInsets.symmetric(
                horizontal: 6,
                vertical: 2,
              ),
              decoration: BoxDecoration(
                color: theme.colorScheme.primary,
                borderRadius: BorderRadius.circular(10),
              ),
              child: Text(
                unreadCount > 99 ? '99+' : unreadCount.toString(),
                style: TextStyle(
                  color: theme.colorScheme.onPrimary,
                  fontSize: 11,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ),
      ],
    );
  }
}
```
