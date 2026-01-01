---
type: "THEORY"
title: "WebSockets: Full-Duplex Communication"
---

WebSockets provide a persistent, bidirectional connection between client and server. Once established, either side can send messages at any time without the overhead of HTTP requests.

**How WebSockets Work:**

1. Client initiates WebSocket handshake (HTTP upgrade request)
2. Server accepts and upgrades the connection
3. Both sides can now send messages freely
4. Connection stays open until explicitly closed
5. Either side can send data at any time

**The WebSocket Protocol:**

WebSockets use `ws://` (or `wss://` for secure) URLs. After the initial HTTP handshake, communication switches to the WebSocket protocol with minimal framing overhead.

**Advantages:**

- True bidirectional communication
- Minimal latency (no connection overhead per message)
- Efficient for high-frequency updates
- Server can push without client request
- Lower bandwidth than polling
- Supports binary and text messages

**Disadvantages:**

- Requires WebSocket server support
- Stateful connections (harder to scale horizontally)
- Connection management complexity
- May have issues with some proxies/firewalls
- Reconnection logic needed for reliability

**When to Use WebSockets:**

- Chat applications
- Real-time games
- Live collaboration tools
- High-frequency data streams
- Any bidirectional real-time requirement



```dart
import 'package:web_socket_channel/web_socket_channel.dart';
import 'dart:convert';

class WebSocketService {
  WebSocketChannel? _channel;
  bool _isConnected = false;
  
  // Connect to WebSocket server
  Future<void> connect(String url) async {
    try {
      _channel = WebSocketChannel.connect(Uri.parse(url));
      _isConnected = true;
      print('WebSocket connected');
    } catch (e) {
      print('WebSocket connection error: $e');
      _isConnected = false;
    }
  }
  
  // Listen for incoming messages
  Stream<dynamic> get messages {
    return _channel?.stream ?? Stream.empty();
  }
  
  // Send a message to the server
  void sendMessage(Map<String, dynamic> message) {
    if (_isConnected && _channel != null) {
      _channel!.sink.add(jsonEncode(message));
    }
  }
  
  // Close the connection
  void disconnect() {
    _channel?.sink.close();
    _isConnected = false;
  }
}

// Usage in a chat application
class ChatService {
  final WebSocketService _ws = WebSocketService();
  
  Future<void> connectToChat(String roomId) async {
    await _ws.connect('wss://chat.example.com/room/$roomId');
    
    // Listen for incoming messages
    _ws.messages.listen((data) {
      final message = jsonDecode(data);
      print('Received: ${message['text']}');
    });
  }
  
  void sendChatMessage(String text) {
    _ws.sendMessage({
      'type': 'message',
      'text': text,
      'timestamp': DateTime.now().toIso8601String(),
    });
  }
  
  // Send typing indicator
  void sendTypingIndicator(bool isTyping) {
    _ws.sendMessage({
      'type': 'typing',
      'isTyping': isTyping,
    });
  }
}
```
