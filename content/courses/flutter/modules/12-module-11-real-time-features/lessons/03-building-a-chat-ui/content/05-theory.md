---
type: "THEORY"
title: "Real-Time Updates"
---

Integrating real-time updates from Serverpod streams makes your chat feel alive. New messages appear instantly, and status updates reflect in real-time.

**Optimistic Updates:**

When a user sends a message, add it to the UI immediately with a 'sending' status. Update the status when the server confirms receipt. This makes the app feel responsive even with network latency.

**Stream Integration:**

- Subscribe to the chat stream when entering the screen
- Handle incoming messages by inserting at the beginning of the list
- Update existing messages when status changes (e.g., read receipts)
- Handle errors gracefully with retry logic



```dart
class _ChatScreenState extends State<ChatScreen> {
  final List<ChatMessage> _messages = [];
  StreamSubscription? _messageSubscription;
  StreamSubscription? _statusSubscription;
  
  @override
  void initState() {
    super.initState();
    _loadInitialMessages();
    _subscribeToUpdates();
  }
  
  void _subscribeToUpdates() {
    // Subscribe to new messages
    _messageSubscription = _chatService
        .messageStream(widget.conversationId)
        .listen(_handleNewMessage);
    
    // Subscribe to status updates (read receipts, etc.)
    _statusSubscription = _chatService
        .statusStream(widget.conversationId)
        .listen(_handleStatusUpdate);
  }
  
  void _handleNewMessage(ChatMessage message) {
    // Check if we already have this message (optimistic update)
    final existingIndex = _messages.indexWhere(
      (m) => m.localId == message.localId || m.id == message.id,
    );
    
    setState(() {
      if (existingIndex >= 0) {
        // Update existing message with server data
        _messages[existingIndex] = message;
      } else {
        // Insert new message at the beginning (most recent)
        _messages.insert(0, message);
      }
    });
    
    // Auto-scroll if we're near the bottom
    if (_isNearBottom) {
      _scrollToBottom();
    }
  }
  
  void _handleStatusUpdate(MessageStatusUpdate update) {
    final index = _messages.indexWhere((m) => m.id == update.messageId);
    if (index >= 0) {
      setState(() {
        _messages[index] = _messages[index].copyWith(
          status: update.newStatus,
        );
      });
    }
  }
  
  Future<void> _handleSendMessage(String text) async {
    // Create message with local ID for optimistic update
    final localId = const Uuid().v4();
    final optimisticMessage = ChatMessage(
      localId: localId,
      text: text,
      senderId: currentUserId,
      senderName: currentUserName,
      timestamp: DateTime.now(),
      status: MessageStatus.sending,
    );
    
    // Add to UI immediately
    setState(() {
      _messages.insert(0, optimisticMessage);
    });
    _scrollToBottom();
    
    try {
      // Send to server
      final serverMessage = await _chatService.sendMessage(
        conversationId: widget.conversationId,
        text: text,
        localId: localId,
      );
      
      // Update with server response
      setState(() {
        final index = _messages.indexWhere((m) => m.localId == localId);
        if (index >= 0) {
          _messages[index] = serverMessage;
        }
      });
    } catch (e) {
      // Mark as failed
      setState(() {
        final index = _messages.indexWhere((m) => m.localId == localId);
        if (index >= 0) {
          _messages[index] = _messages[index].copyWith(
            status: MessageStatus.failed,
          );
        }
      });
      
      _showRetrySnackbar(localId);
    }
  }
  
  void _showRetrySnackbar(String localId) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: const Text('Message failed to send'),
        action: SnackBarAction(
          label: 'Retry',
          onPressed: () => _retryMessage(localId),
        ),
      ),
    );
  }
  
  Future<void> _retryMessage(String localId) async {
    final message = _messages.firstWhere((m) => m.localId == localId);
    setState(() {
      final index = _messages.indexWhere((m) => m.localId == localId);
      _messages[index] = message.copyWith(status: MessageStatus.sending);
    });
    
    try {
      final serverMessage = await _chatService.sendMessage(
        conversationId: widget.conversationId,
        text: message.text,
        localId: localId,
      );
      
      setState(() {
        final index = _messages.indexWhere((m) => m.localId == localId);
        if (index >= 0) {
          _messages[index] = serverMessage;
        }
      });
    } catch (e) {
      setState(() {
        final index = _messages.indexWhere((m) => m.localId == localId);
        _messages[index] = message.copyWith(status: MessageStatus.failed);
      });
    }
  }
  
  @override
  void dispose() {
    _messageSubscription?.cancel();
    _statusSubscription?.cancel();
    super.dispose();
  }
}
```
