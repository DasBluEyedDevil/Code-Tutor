---
type: "THEORY"
title: "Error Handling"
---

Real-time connections can fail in many ways. Robust error handling ensures your app remains usable even when the network is unreliable.

**Common Error Scenarios:**

1. **Network Errors**: WiFi drops, cellular handoff, airplane mode
2. **Server Errors**: Server restarts, deployments, crashes
3. **Authentication Errors**: Token expired, session invalid
4. **Protocol Errors**: Malformed messages, version mismatch

**Error Recovery Patterns:**

- Automatic reconnection with exponential backoff
- Message queuing during disconnection
- User notification for persistent failures
- Graceful degradation to polling



```dart
// Comprehensive error handling for streams
class RobustChatService {
  final Client _client;
  final _messageController = StreamController<ChatMessage>.broadcast();
  final _pendingMessages = <ChatMessage>[];
  
  StreamSubscription? _subscription;
  bool _isConnected = false;
  
  Stream<ChatMessage> get messages => _messageController.stream;
  
  RobustChatService(this._client);
  
  Future<void> connect(String roomId) async {
    try {
      await _client.openStreamingConnection();
      _isConnected = true;
      
      _subscription = _client.chat.stream.listen(
        _handleMessage,
        onError: _handleStreamError,
        onDone: _handleStreamClosed,
        cancelOnError: false, // Don't cancel on error
      );
      
      // Flush pending messages
      await _flushPendingMessages();
      
    } on SocketException catch (e) {
      _handleNetworkError(e);
    } on TimeoutException catch (e) {
      _handleTimeoutError(e);
    } on AuthenticationException catch (e) {
      _handleAuthError(e);
    } catch (e) {
      _handleUnknownError(e);
    }
  }
  
  void _handleMessage(dynamic message) {
    if (message is ChatMessage) {
      _messageController.add(message);
    } else if (message is ErrorMessage) {
      _messageController.addError(
        ChatException(message.code, message.text),
      );
    }
  }
  
  void _handleStreamError(Object error, StackTrace stackTrace) {
    print('Stream error: $error');
    
    if (error is WebSocketException) {
      // WebSocket closed unexpectedly
      _isConnected = false;
      _scheduleReconnect();
    } else {
      // Add error to stream for UI to handle
      _messageController.addError(error, stackTrace);
    }
  }
  
  void _handleStreamClosed() {
    print('Stream closed');
    _isConnected = false;
    // Could be intentional or unintentional
  }
  
  void _handleNetworkError(SocketException e) {
    print('Network error: $e');
    _messageController.addError(
      NetworkException('Unable to connect. Check your internet connection.'),
    );
    _scheduleReconnect();
  }
  
  void _handleTimeoutError(TimeoutException e) {
    print('Timeout: $e');
    _messageController.addError(
      NetworkException('Connection timed out. Retrying...'),
    );
    _scheduleReconnect();
  }
  
  void _handleAuthError(AuthenticationException e) {
    print('Auth error: $e');
    _messageController.addError(
      AuthException('Session expired. Please log in again.'),
    );
    // Don't auto-reconnect for auth errors
  }
  
  void _handleUnknownError(Object e) {
    print('Unknown error: $e');
    _messageController.addError(e);
    _scheduleReconnect();
  }
  
  void sendMessage(ChatMessage message) {
    if (_isConnected) {
      try {
        _client.chat.sendStreamMessage(message);
      } catch (e) {
        // Queue for later if send fails
        _pendingMessages.add(message);
      }
    } else {
      // Queue messages while disconnected
      _pendingMessages.add(message);
    }
  }
  
  Future<void> _flushPendingMessages() async {
    while (_pendingMessages.isNotEmpty && _isConnected) {
      final message = _pendingMessages.removeAt(0);
      try {
        _client.chat.sendStreamMessage(message);
        await Future.delayed(Duration(milliseconds: 50)); // Rate limit
      } catch (e) {
        _pendingMessages.insert(0, message); // Put back at front
        break;
      }
    }
  }
  
  void _scheduleReconnect() {
    // Reconnect logic (use exponential backoff)
  }
  
  void dispose() {
    _subscription?.cancel();
    _messageController.close();
    _client.closeStreamingConnection();
  }
}

// Custom exception types
class NetworkException implements Exception {
  final String message;
  NetworkException(this.message);
}

class AuthException implements Exception {
  final String message;
  AuthException(this.message);
}

class ChatException implements Exception {
  final int code;
  final String message;
  ChatException(this.code, this.message);
}
```
