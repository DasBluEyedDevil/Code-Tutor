---
type: "EXAMPLE"
title: "Testing Streaming Endpoints"
---

Here is how to test Serverpod streaming endpoints:

```dart
// test/integration/chat_streaming_test.dart
import 'dart:async';
import 'package:test/test.dart';
import 'package:serverpod_test/serverpod_test.dart';
import 'package:my_project_server/src/generated/protocol.dart';
import 'package:my_project_server/src/endpoints/chat_endpoint.dart';

void main() {
  late TestSession session1;
  late TestSession session2;
  late ChatEndpoint chatEndpoint;

  setUpAll(() async {
    await TestServer.start();
  });

  tearDownAll(() async {
    await TestServer.stop();
  });

  setUp(() async {
    // Create two sessions to simulate two users
    session1 = await TestSession.create();
    session2 = await TestSession.create();
    chatEndpoint = ChatEndpoint();

    // Authenticate both sessions
    await authenticateSession(session1, 'user1@test.com', 'User One');
    await authenticateSession(session2, 'user2@test.com', 'User Two');
  });

  tearDown(() async {
    await session1.close();
    await session2.close();
    await TestDatabase.truncateAll();
  });

  group('Chat Streaming', () {
    test('streamOpened is called when user connects', () async {
      var connectionOpened = false;

      // Override the endpoint for testing
      final testEndpoint = TestChatEndpoint(
        onStreamOpened: (session) {
          connectionOpened = true;
        },
      );

      await testEndpoint.streamOpened(session1);

      expect(connectionOpened, isTrue);
    });

    test('messages are broadcast to all connected users', () async {
      final receivedMessages = <ChatMessage>[];

      // Connect both users to the same room
      await chatEndpoint.joinRoom(session1, roomId: 'test-room');
      await chatEndpoint.joinRoom(session2, roomId: 'test-room');

      // Set up listener for session2
      final subscription = session2.messages.listen((message) {
        if (message is ChatMessage) {
          receivedMessages.add(message);
        }
      });

      // User1 sends a message
      await chatEndpoint.sendMessage(
        session1,
        roomId: 'test-room',
        text: 'Hello from User One!',
      );

      // Wait for message to be received
      await Future.delayed(Duration(milliseconds: 100));

      expect(receivedMessages, hasLength(1));
      expect(receivedMessages.first.text, equals('Hello from User One!'));
      expect(receivedMessages.first.senderName, equals('User One'));

      await subscription.cancel();
    });

    test('streamClosed cleans up resources', () async {
      // Join a room
      await chatEndpoint.joinRoom(session1, roomId: 'test-room');

      // Verify user is in room
      var usersInRoom = await chatEndpoint.getUsersInRoom(session1, roomId: 'test-room');
      expect(usersInRoom, contains('User One'));

      // Simulate disconnect
      await chatEndpoint.streamClosed(session1);

      // Verify user is removed from room
      usersInRoom = await chatEndpoint.getUsersInRoom(session2, roomId: 'test-room');
      expect(usersInRoom, isEmpty);
    });

    test('handleStreamMessage processes incoming messages', () async {
      final processedMessages = <String>[];

      // Connect to room
      await chatEndpoint.joinRoom(session1, roomId: 'test-room');

      // Simulate receiving a message from client
      final clientMessage = ChatMessage(
        text: 'Message from client',
        roomId: 'test-room',
        timestamp: DateTime.now(),
      );

      await chatEndpoint.handleStreamMessage(
        session1,
        clientMessage,
      );

      // Verify message was stored in database
      final storedMessages = await ChatMessage.db.find(
        session1,
        where: (t) => t.roomId.equals('test-room'),
      );

      expect(storedMessages, hasLength(1));
      expect(storedMessages.first.text, equals('Message from client'));
    });

    test('user cannot send to room they have not joined', () async {
      // Do not join any room

      expect(
        () => chatEndpoint.sendMessage(
          session1,
          roomId: 'secret-room',
          text: 'Should fail',
        ),
        throwsA(isA<NotInRoomException>()),
      );
    });
  });
}

Future<void> authenticateSession(
  TestSession session,
  String email,
  String name,
) async {
  final userInfo = UserInfo(
    email: email,
    userName: name,
    userIdentifier: email,
    created: DateTime.now(),
  );
  final inserted = await UserInfo.db.insertRow(session, userInfo);
  session.updateAuthentication(AuthenticationInfo(
    userInfo: inserted,
    authId: inserted.id!,
  ));
}
```
