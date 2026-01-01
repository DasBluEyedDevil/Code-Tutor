---
type: "EXAMPLE"
title: "Testing and Polish"
---

A production chat feature needs comprehensive testing and polished error handling. Here we cover integration tests for the chat flow and proper offline queuing.

**Testing Strategy:**

- Unit tests for message formatting and state logic
- Widget tests for UI components
- Integration tests for real-time message flow
- Mock the server for predictable testing

**Offline Queuing:**

When the user sends a message while offline, queue it locally and sync when connectivity returns.



```dart
// Integration test for chat flow
import 'package:flutter_test/flutter_test.dart';
import 'package:integration_test/integration_test.dart';

void main() {
  IntegrationTestWidgetsFlutterBinding.ensureInitialized();
  
  group('Chat Flow Tests', () {
    testWidgets('Send and receive message', (tester) async {
      // Start app with mock server
      await tester.pumpWidget(createTestApp());
      await tester.pumpAndSettle();
      
      // Navigate to chat
      await tester.tap(find.text('Test Conversation'));
      await tester.pumpAndSettle();
      
      // Verify chat screen loaded
      expect(find.byType(ChatScreen), findsOneWidget);
      expect(find.byType(ChatInputField), findsOneWidget);
      
      // Type a message
      await tester.enterText(
        find.byType(TextField),
        'Hello, this is a test message!',
      );
      await tester.pumpAndSettle();
      
      // Tap send
      await tester.tap(find.byIcon(Icons.send));
      await tester.pumpAndSettle();
      
      // Verify optimistic message appears
      expect(
        find.text('Hello, this is a test message!'),
        findsOneWidget,
      );
      
      // Wait for server response
      await tester.pump(Duration(seconds: 1));
      
      // Verify message status updated (no longer 'sending')
      final messageBubble = tester.widget<MessageBubble>(
        find.byType(MessageBubble).first,
      );
      expect(messageBubble.message.status, isNot(MessageStatus.sending));
    });
    
    testWidgets('Shows typing indicator', (tester) async {
      await tester.pumpWidget(createTestApp());
      await tester.pumpAndSettle();
      
      await tester.tap(find.text('Test Conversation'));
      await tester.pumpAndSettle();
      
      // Simulate receiving typing status
      mockServer.emitTypingStatus(
        conversationId: testConversationId,
        userId: otherUserId,
        userName: 'Other User',
        isTyping: true,
      );
      
      await tester.pump(Duration(milliseconds: 100));
      
      // Verify typing indicator appears
      expect(find.byType(TypingIndicator), findsOneWidget);
      expect(find.text('Other User is typing...'), findsOneWidget);
    });
    
    testWidgets('Handles offline gracefully', (tester) async {
      await tester.pumpWidget(createTestApp());
      await tester.pumpAndSettle();
      
      await tester.tap(find.text('Test Conversation'));
      await tester.pumpAndSettle();
      
      // Simulate going offline
      mockServer.disconnect();
      await tester.pump(Duration(seconds: 1));
      
      // Verify reconnecting status
      expect(find.text('Reconnecting...'), findsOneWidget);
      
      // Try to send message while offline
      await tester.enterText(find.byType(TextField), 'Offline message');
      await tester.tap(find.byIcon(Icons.send));
      await tester.pumpAndSettle();
      
      // Message should appear with pending status
      expect(find.text('Offline message'), findsOneWidget);
      
      // Reconnect
      mockServer.reconnect();
      await tester.pump(Duration(seconds: 2));
      
      // Message should sync and status should update
      expect(find.text('Reconnecting...'), findsNothing);
    });
  });
}

// Offline message queue
class OfflineMessageQueue {
  final LocalDatabase _db;
  final ChatService _chatService;
  
  OfflineMessageQueue(this._db, this._chatService);
  
  Future<void> queueMessage(ChatMessage message) async {
    await _db.insertPendingMessage(message);
  }
  
  Future<void> processQueue() async {
    final pending = await _db.getPendingMessages();
    
    for (final message in pending) {
      try {
        await _chatService.sendMessage(
          conversationId: message.conversationId,
          content: message.content,
          localId: message.localId,
        );
        
        await _db.deletePendingMessage(message.localId!);
      } catch (e) {
        // Keep in queue for next attempt
        print('Failed to sync message: $e');
      }
    }
  }
  
  // Call when connectivity changes
  void onConnectivityRestored() {
    processQueue();
  }
}

// Error handling utilities
class ChatErrorHandler {
  static void handleError(BuildContext context, dynamic error) {
    String message;
    
    if (error is SocketException) {
      message = 'No internet connection';
    } else if (error is TimeoutException) {
      message = 'Request timed out. Please try again.';
    } else if (error is UnauthorizedException) {
      message = 'Session expired. Please log in again.';
      // Navigate to login
    } else {
      message = 'Something went wrong';
    }
    
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text(message),
        action: SnackBarAction(
          label: 'Retry',
          onPressed: () => _retryLastAction(context),
        ),
      ),
    );
  }
}
```
