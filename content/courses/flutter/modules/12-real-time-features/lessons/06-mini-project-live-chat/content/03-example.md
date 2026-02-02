---
type: "EXAMPLE"
title: "Frontend Implementation"
---

The Flutter chat screen integrates all the UI components we've built throughout this module: message list, input field, typing indicators, and real-time updates.

**Key Features:**

- Reverse scrolling list with pagination
- Optimistic message sending
- Real-time updates via stream
- Typing indicator integration
- Connection status display



```dart
// lib/screens/chat/chat_screen.dart
import 'dart:async';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ChatScreen extends ConsumerStatefulWidget {
  final int conversationId;
  final String recipientName;
  
  const ChatScreen({
    required this.conversationId,
    required this.recipientName,
    super.key,
  });
  
  @override
  ConsumerState<ChatScreen> createState() => _ChatScreenState();
}

class _ChatScreenState extends ConsumerState<ChatScreen> {
  final ScrollController _scrollController = ScrollController();
  final TextEditingController _textController = TextEditingController();
  final List<ChatMessage> _messages = [];
  
  StreamSubscription? _messageSubscription;
  StreamSubscription? _typingSubscription;
  StreamSubscription? _connectionSubscription;
  
  late TypingIndicatorService _typingService;
  bool _isLoadingMore = false;
  bool _hasMoreMessages = true;
  bool _showScrollToBottom = false;
  int _unreadCount = 0;
  List<TypingUser> _typingUsers = [];
  ConnectionState _connectionState = ConnectionState.connecting;
  
  @override
  void initState() {
    super.initState();
    _typingService = TypingIndicatorService(
      ref.read(clientProvider),
      widget.conversationId.toString(),
    );
    _initialize();
  }
  
  Future<void> _initialize() async {
    await _loadInitialMessages();
    await _connectToStream();
    _scrollController.addListener(_onScroll);
    _textController.addListener(_onTextChanged);
  }
  
  Future<void> _loadInitialMessages() async {
    try {
      final messages = await ref.read(clientProvider).chat.getMessages(
        conversationId: widget.conversationId,
        limit: 50,
      );
      
      setState(() {
        _messages.addAll(messages);
        _hasMoreMessages = messages.length == 50;
      });
    } catch (e) {
      _showError('Failed to load messages');
    }
  }
  
  Future<void> _connectToStream() async {
    final client = ref.read(clientProvider);
    
    try {
      await client.openStreamingConnection();
      await client.chat.joinConversation(
        conversationId: widget.conversationId,
      );
      
      setState(() => _connectionState = ConnectionState.connected);
      
      // Listen for new messages
      _messageSubscription = client.chat.messageStream.listen(
        _handleNewMessage,
        onError: (e) => _handleStreamError(e),
      );
      
      // Listen for typing updates
      _typingSubscription = client.chat.typingStream.listen(
        _handleTypingUpdate,
      );
      
      // Monitor connection status
      _connectionSubscription = client.streamingConnectionStatus.listen(
        _handleConnectionChange,
      );
      
    } catch (e) {
      setState(() => _connectionState = ConnectionState.disconnected);
      _showError('Failed to connect');
    }
  }
  
  void _handleNewMessage(ChatMessage message) {
    // Check if we already have this message (optimistic update)
    final existingIndex = _messages.indexWhere(
      (m) => m.localId == message.localId || m.id == message.id,
    );
    
    setState(() {
      if (existingIndex >= 0) {
        // Update existing optimistic message
        _messages[existingIndex] = message;
      } else {
        // Insert new message at the beginning
        _messages.insert(0, message);
        
        // Update unread count if not at bottom
        if (_showScrollToBottom && message.senderId != currentUserId) {
          _unreadCount++;
        }
      }
    });
    
    // Auto-scroll if near bottom
    if (!_showScrollToBottom) {
      _scrollToBottom();
    }
  }
  
  void _handleTypingUpdate(TypingStatus status) {
    setState(() {
      if (status.isTyping) {
        if (!_typingUsers.any((u) => u.id == status.userId.toString())) {
          _typingUsers.add(TypingUser(
            id: status.userId.toString(),
            name: status.userName,
          ));
        }
      } else {
        _typingUsers.removeWhere((u) => u.id == status.userId.toString());
      }
    });
  }
  
  void _handleConnectionChange(StreamingConnectionStatus status) {
    setState(() {
      _connectionState = status == StreamingConnectionStatus.connected
          ? ConnectionState.connected
          : ConnectionState.reconnecting;
    });
    
    if (status == StreamingConnectionStatus.disconnected) {
      // Attempt reconnect
      Future.delayed(Duration(seconds: 2), _connectToStream);
    }
  }
  
  void _onTextChanged() {
    _typingService.onTextChanged(_textController.text);
  }
  
  void _onScroll() {
    // Show scroll-to-bottom button when scrolled up
    final showButton = _scrollController.position.pixels > 200;
    if (showButton != _showScrollToBottom) {
      setState(() {
        _showScrollToBottom = showButton;
        if (!showButton) _unreadCount = 0;
      });
    }
    
    // Load more when near the top (end in reverse mode)
    if (_scrollController.position.pixels >=
        _scrollController.position.maxScrollExtent - 200) {
      _loadMoreMessages();
    }
  }
  
  Future<void> _loadMoreMessages() async {
    if (_isLoadingMore || !_hasMoreMessages || _messages.isEmpty) return;
    
    setState(() => _isLoadingMore = true);
    
    try {
      final oldestMessage = _messages.last;
      final moreMessages = await ref.read(clientProvider).chat.getMessages(
        conversationId: widget.conversationId,
        before: oldestMessage.createdAt,
        limit: 50,
      );
      
      setState(() {
        _messages.addAll(moreMessages);
        _hasMoreMessages = moreMessages.length == 50;
        _isLoadingMore = false;
      });
    } catch (e) {
      setState(() => _isLoadingMore = false);
      _showError('Failed to load more messages');
    }
  }
  
  Future<void> _sendMessage() async {
    final text = _textController.text.trim();
    if (text.isEmpty) return;
    
    _textController.clear();
    _typingService.clearActivity();
    
    // Create optimistic message
    final localId = const Uuid().v4();
    final optimisticMessage = ChatMessage(
      conversationId: widget.conversationId,
      senderId: currentUserId,
      content: text,
      localId: localId,
      createdAt: DateTime.now(),
      status: MessageStatus.sending,
    );
    
    // Add to UI immediately
    setState(() => _messages.insert(0, optimisticMessage));
    _scrollToBottom();
    
    try {
      // Send via stream or REST
      final sent = await ref.read(clientProvider).chat.sendMessage(
        conversationId: widget.conversationId,
        content: text,
        localId: localId,
      );
      
      // Update will come through stream
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
      _showRetrySnackbar(localId, text);
    }
  }
  
  void _scrollToBottom() {
    if (_scrollController.hasClients) {
      _scrollController.animateTo(
        0,
        duration: Duration(milliseconds: 300),
        curve: Curves.easeOut,
      );
    }
  }
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(widget.recipientName),
            _buildConnectionStatus(),
          ],
        ),
      ),
      body: Stack(
        children: [
          Column(
            children: [
              // Message list
              Expanded(
                child: ListView.builder(
                  controller: _scrollController,
                  reverse: true,
                  padding: EdgeInsets.all(16),
                  itemCount: _messages.length + (_isLoadingMore ? 1 : 0),
                  itemBuilder: (context, index) {
                    if (index == _messages.length) {
                      return Center(
                        child: Padding(
                          padding: EdgeInsets.all(16),
                          child: CircularProgressIndicator(),
                        ),
                      );
                    }
                    return MessageBubble(
                      message: _messages[index],
                      showAvatar: _shouldShowAvatar(index),
                    );
                  },
                ),
              ),
              
              // Typing indicator
              if (_typingUsers.isNotEmpty)
                TypingIndicator(typingUsers: _typingUsers),
              
              // Input field
              ChatInputField(
                controller: _textController,
                onSend: _sendMessage,
                enabled: _connectionState == ConnectionState.connected,
              ),
            ],
          ),
          
          // Scroll to bottom FAB
          if (_showScrollToBottom)
            Positioned(
              right: 16,
              bottom: 80,
              child: _ScrollToBottomButton(
                unreadCount: _unreadCount,
                onPressed: () {
                  _scrollToBottom();
                  setState(() => _unreadCount = 0);
                },
              ),
            ),
        ],
      ),
    );
  }
  
  Widget _buildConnectionStatus() {
    switch (_connectionState) {
      case ConnectionState.connected:
        return SizedBox.shrink();
      case ConnectionState.connecting:
        return Text('Connecting...', style: TextStyle(fontSize: 12));
      case ConnectionState.reconnecting:
        return Text('Reconnecting...', style: TextStyle(fontSize: 12, color: Colors.orange));
      case ConnectionState.disconnected:
        return Text('Offline', style: TextStyle(fontSize: 12, color: Colors.red));
    }
  }
  
  bool _shouldShowAvatar(int index) {
    if (index == _messages.length - 1) return true;
    return _messages[index].senderId != _messages[index + 1].senderId;
  }
  
  @override
  void dispose() {
    _messageSubscription?.cancel();
    _typingSubscription?.cancel();
    _connectionSubscription?.cancel();
    _scrollController.dispose();
    _textController.dispose();
    _typingService.dispose();
    super.dispose();
  }
}
```
