---
type: "EXAMPLE"
title: "Complete Chat UI Integration"
---

Here is how to integrate streaming with a Flutter chat UI using Provider.



```dart
// File: lib/providers/chat_provider.dart

import 'dart:async';
import 'package:flutter/foundation.dart';
import 'package:my_app_client/my_app_client.dart';
import '../services/chat_service.dart';

class ChatProvider extends ChangeNotifier {
  final ChatService _chatService;
  final List<ChatMessage> _messages = [];
  final Map<int, bool> _typingUsers = {};
  
  bool _isConnected = false;
  String? _currentChannel;
  String? _error;
  
  ChatProvider(this._chatService) {
    _initListeners();
  }
  
  // Getters for UI
  List<ChatMessage> get messages => List.unmodifiable(_messages);
  bool get isConnected => _isConnected;
  String? get currentChannel => _currentChannel;
  String? get error => _error;
  List<int> get typingUserIds => 
      _typingUsers.entries.where((e) => e.value).map((e) => e.key).toList();
  
  void _initListeners() {
    // Listen to connection status
    _chatService.connectionStatus.listen((connected) {
      _isConnected = connected;
      _error = connected ? null : 'Disconnected';
      notifyListeners();
    });
    
    // Listen to incoming messages
    _chatService.messages.listen((message) {
      if (message is ChatMessage) {
        _handleChatMessage(message);
      } else if (message is TypingIndicator) {
        _handleTypingIndicator(message);
      }
    });
  }
  
  void _handleChatMessage(ChatMessage message) {
    // Only add if for current channel
    if (message.channelId == _currentChannel) {
      _messages.add(message);
      // Sort by timestamp to handle out-of-order delivery
      _messages.sort((a, b) => a.timestamp.compareTo(b.timestamp));
      notifyListeners();
    }
  }
  
  void _handleTypingIndicator(TypingIndicator indicator) {
    _typingUsers[indicator.userId] = indicator.isTyping;
    notifyListeners();
    
    // Auto-clear after timeout (in case stop event was lost)
    if (indicator.isTyping) {
      Future.delayed(Duration(seconds: 5), () {
        if (_typingUsers[indicator.userId] == true) {
          _typingUsers[indicator.userId] = false;
          notifyListeners();
        }
      });
    }
  }
  
  /// Connect to chat server.
  Future<void> connect() async {
    try {
      _error = null;
      await _chatService.connect();
    } catch (e) {
      _error = 'Failed to connect: $e';
      notifyListeners();
    }
  }
  
  /// Join a chat channel.
  Future<void> joinChannel(String channelId) async {
    if (_currentChannel != null) {
      await _chatService.leaveChannel(_currentChannel!);
    }
    
    _currentChannel = channelId;
    _messages.clear();
    notifyListeners();
    
    await _chatService.joinChannel(channelId);
    
    // Load message history
    await _loadHistory(channelId);
  }
  
  Future<void> _loadHistory(String channelId) async {
    // Fetch recent messages via HTTP endpoint
    // (Streaming is for real-time, HTTP for history)
    // final history = await _client.chat.getHistory(channelId, limit: 50);
    // _messages.addAll(history);
    // notifyListeners();
  }
  
  /// Send a message.
  Future<void> sendMessage(String content, String senderName) async {
    if (_currentChannel == null) return;
    if (content.trim().isEmpty) return;
    
    try {
      await _chatService.sendMessage(
        channelId: _currentChannel!,
        senderName: senderName,
        content: content,
      );
    } catch (e) {
      _error = 'Failed to send: $e';
      notifyListeners();
    }
  }
  
  @override
  void dispose() {
    _chatService.dispose();
    super.dispose();
  }
}

// File: lib/screens/chat_screen.dart

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../providers/chat_provider.dart';

class ChatScreen extends StatefulWidget {
  final String channelId;
  const ChatScreen({super.key, required this.channelId});
  
  @override
  State<ChatScreen> createState() => _ChatScreenState();
}

class _ChatScreenState extends State<ChatScreen> {
  final _messageController = TextEditingController();
  final _scrollController = ScrollController();
  
  @override
  void initState() {
    super.initState();
    // Connect and join channel on screen load
    WidgetsBinding.instance.addPostFrameCallback((_) {
      final provider = context.read<ChatProvider>();
      provider.connect().then((_) {
        provider.joinChannel(widget.channelId);
      });
    });
  }
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Chat: ${widget.channelId}'),
        actions: [
          Consumer<ChatProvider>(
            builder: (context, provider, _) => Icon(
              provider.isConnected ? Icons.cloud_done : Icons.cloud_off,
              color: provider.isConnected ? Colors.green : Colors.red,
            ),
          ),
        ],
      ),
      body: Column(
        children: [
          // Error banner
          Consumer<ChatProvider>(
            builder: (context, provider, _) {
              if (provider.error == null) return const SizedBox.shrink();
              return Container(
                color: Colors.red.shade100,
                padding: const EdgeInsets.all(8),
                child: Text(provider.error!),
              );
            },
          ),
          
          // Message list
          Expanded(
            child: Consumer<ChatProvider>(
              builder: (context, provider, _) {
                return ListView.builder(
                  controller: _scrollController,
                  itemCount: provider.messages.length,
                  itemBuilder: (context, index) {
                    final message = provider.messages[index];
                    return ListTile(
                      title: Text(message.senderName),
                      subtitle: Text(message.content),
                      trailing: Text(
                        '${message.timestamp.hour}:${message.timestamp.minute}',
                        style: Theme.of(context).textTheme.bodySmall,
                      ),
                    );
                  },
                );
              },
            ),
          ),
          
          // Typing indicator
          Consumer<ChatProvider>(
            builder: (context, provider, _) {
              if (provider.typingUserIds.isEmpty) {
                return const SizedBox.shrink();
              }
              return Padding(
                padding: const EdgeInsets.all(8.0),
                child: Text(
                  '${provider.typingUserIds.length} user(s) typing...',
                  style: Theme.of(context).textTheme.bodySmall,
                ),
              );
            },
          ),
          
          // Message input
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              children: [
                Expanded(
                  child: TextField(
                    controller: _messageController,
                    decoration: const InputDecoration(
                      hintText: 'Type a message...',
                      border: OutlineInputBorder(),
                    ),
                    onSubmitted: (_) => _sendMessage(),
                  ),
                ),
                IconButton(
                  icon: const Icon(Icons.send),
                  onPressed: _sendMessage,
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
  
  void _sendMessage() {
    final text = _messageController.text;
    if (text.trim().isEmpty) return;
    
    context.read<ChatProvider>().sendMessage(text, 'CurrentUser');
    _messageController.clear();
    
    // Scroll to bottom
    _scrollController.animateTo(
      _scrollController.position.maxScrollExtent + 50,
      duration: const Duration(milliseconds: 300),
      curve: Curves.easeOut,
    );
  }
  
  @override
  void dispose() {
    _messageController.dispose();
    _scrollController.dispose();
    super.dispose();
  }
}
```
