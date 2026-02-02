---
type: "THEORY"
title: "Creating Server Streams"
---

Streaming endpoints are defined in your Serverpod server project. They use special method signatures that indicate they handle streaming connections.

**Defining a Streaming Endpoint:**

Streaming methods receive a StreamingSession instead of a regular Session. You register message handlers and send data back to the client.

**Key Concepts:**

- `session.messages.addListener`: Register handlers for incoming messages
- `session.messages.postMessage`: Send messages to the connected client
- `sendStreamMessage`: Type-safe way to send protocol messages
- The connection stays open until the client disconnects or the server closes it



```dart
// server/lib/src/endpoints/chat_endpoint.dart
import 'package:serverpod/serverpod.dart';

class ChatEndpoint extends Endpoint {
  // Streaming method for real-time chat
  @override
  Future<void> streamOpened(StreamingSession session) async {
    // Called when a client opens a streaming connection
    print('Client connected: ${session.sessionId}');
    
    // Register handler for incoming messages
    session.messages.addListener('chat', (message) {
      // Handle incoming chat message
      _handleChatMessage(session, message as ChatMessage);
    });
    
    // Send welcome message
    await session.messages.postMessage(
      'system',
      SystemMessage(text: 'Welcome to the chat!'),
    );
  }
  
  @override
  Future<void> streamClosed(StreamingSession session) async {
    // Called when client disconnects
    print('Client disconnected: ${session.sessionId}');
    // Clean up any resources
  }
  
  Future<void> _handleChatMessage(
    StreamingSession session,
    ChatMessage message,
  ) async {
    // Broadcast to all connected clients
    await server.messageCentral.postMessage(
      'chat-room-${message.roomId}',
      message,
    );
  }
}
```
