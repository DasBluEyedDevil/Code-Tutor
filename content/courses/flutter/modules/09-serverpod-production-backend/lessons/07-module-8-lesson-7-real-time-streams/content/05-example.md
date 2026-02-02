---
type: "EXAMPLE"
title: "Setting Up a Streaming Endpoint"
---

Let us create a basic streaming endpoint in Serverpod. First, you need to define a message model, then create the endpoint that handles streaming connections.



```dart
// Step 1: Define message models in lib/src/protocol/
// File: chat_message.yaml

// class: ChatMessage
// fields:
//   senderName: String
//   content: String
//   timestamp: DateTime
//   channelId: String

// Step 2: Create the streaming endpoint
// File: lib/src/endpoints/chat_endpoint.dart

import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

// Store connected sessions by channel
// In production, consider using Serverpod's built-in session management
final Map<String, Set<StreamingSession>> _channelSubscribers = {};

class ChatEndpoint extends Endpoint {
  /// Handle streaming connections.
  /// This method is called when a client opens a streaming connection.
  @override
  Future<void> streamOpened(StreamingSession session) async {
    // Log connection for debugging
    session.log('Client connected to chat streaming');
    
    // You can access user info if authenticated
    // final userId = await session.auth.authenticatedUserId;
  }
  
  /// Handle messages from clients.
  /// This is called whenever a client sends a message through the stream.
  @override
  Future<void> handleStreamMessage(
    StreamingSession session,
    SerializableModel message,
  ) async {
    // Determine message type and handle accordingly
    if (message is ChatMessage) {
      await _handleChatMessage(session, message);
    } else if (message is ChannelSubscription) {
      await _handleSubscription(session, message);
    }
  }
  
  /// Clean up when client disconnects.
  @override
  Future<void> streamClosed(StreamingSession session) async {
    session.log('Client disconnected from chat streaming');
    
    // Remove from all channels
    for (final subscribers in _channelSubscribers.values) {
      subscribers.remove(session);
    }
  }
  
  // Handle incoming chat messages
  Future<void> _handleChatMessage(
    StreamingSession session,
    ChatMessage message,
  ) async {
    // Add timestamp if not set
    final messageWithTimestamp = ChatMessage(
      senderName: message.senderName,
      content: message.content,
      timestamp: DateTime.now(),
      channelId: message.channelId,
    );
    
    // Broadcast to all subscribers of this channel
    final subscribers = _channelSubscribers[message.channelId] ?? {};
    for (final subscriber in subscribers) {
      // Send the message to each connected client
      subscriber.sendStreamMessage(messageWithTimestamp);
    }
    
    // Optionally persist to database
    // await ChatMessage.db.insertRow(session, messageWithTimestamp);
  }
  
  // Handle channel subscription requests
  Future<void> _handleSubscription(
    StreamingSession session,
    ChannelSubscription subscription,
  ) async {
    final channelId = subscription.channelId;
    
    // Create channel set if it does not exist
    _channelSubscribers[channelId] ??= {};
    
    if (subscription.subscribe) {
      _channelSubscribers[channelId]!.add(session);
      session.log('Subscribed to channel: $channelId');
    } else {
      _channelSubscribers[channelId]!.remove(session);
      session.log('Unsubscribed from channel: $channelId');
    }
  }
}
```
