---
type: "THEORY"
title: "Subscribing from Flutter"
---

On the Flutter side, you use the generated client to open streaming connections. Serverpod generates type-safe streaming methods that return Dart Streams.

**Opening a Streaming Connection:**

Use `client.openStreamingConnection` to establish a WebSocket connection with the server. Once connected, you can listen to streams and send messages.

**Key Methods:**

- `openStreamingConnection`: Establishes the WebSocket
- `client.chat.stream`: Access the generated stream
- `sendStreamMessage`: Send typed messages to server
- `closeStreamingConnection`: Clean disconnect



```dart
// Flutter client - subscribing to streams
import 'package:your_client/your_client.dart';

class ChatService {
  final Client _client;
  StreamSubscription? _chatSubscription;
  StreamSubscription? _connectionSubscription;
  
  ChatService(this._client);
  
  Future<void> connectToChat(String roomId) async {
    // Open streaming connection
    await _client.openStreamingConnection(
      disconnectOnLostInternetConnection: true,
    );
    
    // Listen for connection state changes
    _connectionSubscription = _client.streamingConnectionStatus.listen(
      (status) {
        print('Connection status: $status');
        if (status == StreamingConnectionStatus.connected) {
          _joinRoom(roomId);
        }
      },
    );
  }
  
  void _joinRoom(String roomId) {
    // Listen for incoming chat messages
    _chatSubscription = _client.chat.stream.listen(
      (message) {
        if (message is ChatMessage) {
          _handleChatMessage(message);
        } else if (message is SystemMessage) {
          _handleSystemMessage(message);
        }
      },
      onError: (error) {
        print('Stream error: $error');
      },
      onDone: () {
        print('Stream closed');
      },
    );
    
    // Send join room message
    _client.chat.sendStreamMessage(
      JoinRoomMessage(roomId: roomId),
    );
  }
  
  void sendMessage(String text, String roomId) {
    _client.chat.sendStreamMessage(
      ChatMessage(
        text: text,
        roomId: roomId,
        timestamp: DateTime.now(),
      ),
    );
  }
  
  void _handleChatMessage(ChatMessage message) {
    // Update UI with new message
    print('Received: ${message.text}');
  }
  
  void _handleSystemMessage(SystemMessage message) {
    print('System: ${message.text}');
  }
}
```
