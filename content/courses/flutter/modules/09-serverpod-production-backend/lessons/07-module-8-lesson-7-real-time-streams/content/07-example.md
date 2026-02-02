---
type: "EXAMPLE"
title: "Flutter Client: Connecting to Streams"
---

Now let us see how to connect to the streaming endpoint from your Flutter app. The generated client makes this straightforward.



```dart
// File: lib/services/chat_service.dart

import 'dart:async';
import 'package:my_app_client/my_app_client.dart';

class ChatService {
  final Client _client;
  StreamSubscription? _messageSubscription;
  bool _isConnected = false;
  
  // Stream controller for UI to listen to messages
  final _messageController = StreamController<ChatMessage>.broadcast();
  Stream<ChatMessage> get messages => _messageController.stream;
  
  // Connection status stream
  final _connectionController = StreamController<bool>.broadcast();
  Stream<bool> get connectionStatus => _connectionController.stream;
  
  ChatService(this._client);
  
  /// Connect to the chat streaming endpoint.
  Future<void> connect() async {
    if (_isConnected) return; // Already connected
    
    try {
      // Open streaming connection to the chat endpoint
      // This calls streamOpened on the server
      await _client.openStreamingConnection(
        // Optional: reconnection settings
        disconnectOnLostInternetConnection: false,
      );
      
      // Listen for incoming messages
      _messageSubscription = _client.chat.stream.listen(
        (message) {
          // Route messages by type
          if (message is ChatMessage) {
            _messageController.add(message);
          }
        },
        onError: (error) {
          print('Stream error: $error');
          _handleDisconnection();
        },
        onDone: () {
          print('Stream closed');
          _handleDisconnection();
        },
      );
      
      _isConnected = true;
      _connectionController.add(true);
      print('Connected to chat stream');
      
    } catch (e) {
      print('Failed to connect: $e');
      _connectionController.add(false);
      rethrow;
    }
  }
  
  /// Subscribe to a specific chat channel.
  Future<void> joinChannel(String channelId) async {
    if (!_isConnected) {
      throw StateError('Not connected. Call connect() first.');
    }
    
    // Send subscription request to server
    _client.chat.sendStreamMessage(
      ChannelSubscription(
        channelId: channelId,
        subscribe: true,
      ),
    );
  }
  
  /// Leave a chat channel.
  Future<void> leaveChannel(String channelId) async {
    if (!_isConnected) return;
    
    _client.chat.sendStreamMessage(
      ChannelSubscription(
        channelId: channelId,
        subscribe: false,
      ),
    );
  }
  
  /// Send a message to a channel.
  Future<void> sendMessage({
    required String channelId,
    required String senderName,
    required String content,
  }) async {
    if (!_isConnected) {
      throw StateError('Not connected. Call connect() first.');
    }
    
    _client.chat.sendStreamMessage(
      ChatMessage(
        channelId: channelId,
        senderName: senderName,
        content: content,
        timestamp: DateTime.now(), // Server will override
      ),
    );
  }
  
  void _handleDisconnection() {
    _isConnected = false;
    _connectionController.add(false);
  }
  
  /// Disconnect from streaming.
  Future<void> disconnect() async {
    await _messageSubscription?.cancel();
    await _client.closeStreamingConnection();
    _isConnected = false;
    _connectionController.add(false);
  }
  
  /// Clean up resources.
  void dispose() {
    disconnect();
    _messageController.close();
    _connectionController.close();
  }
}
```
