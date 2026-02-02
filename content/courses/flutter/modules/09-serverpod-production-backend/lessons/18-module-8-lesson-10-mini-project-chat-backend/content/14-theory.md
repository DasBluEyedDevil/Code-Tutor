---
type: "THEORY"
title: "Testing Your Chat Backend"
---

Testing a real-time chat backend requires multiple approaches:

**1. Unit Tests:**
Test individual methods in isolation:

```dart
test('sanitizeFileName removes special characters', () {
  expect(
    fileEndpoint.sanitizeFileName('My File (1).pdf'),
    equals('my_file__1_.pdf'),
  );
});
```

**2. Integration Tests:**
Test endpoint behavior with database:

```dart
withServerpod('Chat message flow', (sessionBuilder) {
  test('can send and receive messages', () async {
    final session = sessionBuilder.build();
    
    // Create users and room
    final user1 = await createTestUser(session, 'user1');
    final user2 = await createTestUser(session, 'user2');
    final room = await chatRoomEndpoint.getOrCreateDirectMessage(
      session,
      user2.id!,
    );
    
    // Send message
    final message = await chatMessageEndpoint.sendMessage(
      session,
      chatRoomId: room.id!,
      content: 'Hello!',
    );
    
    expect(message.content, equals('Hello!'));
    expect(message.senderId, equals(user1.id));
  });
});
```

**3. WebSocket Tests:**
Test streaming with multiple clients:

```dart
test('messages broadcast to all subscribers', () async {
  // Connect two clients
  final client1 = await connectStreamingClient(user1Token);
  final client2 = await connectStreamingClient(user2Token);
  
  // Subscribe to same room
  await client1.subscribeToRoom(roomId);
  await client2.subscribeToRoom(roomId);
  
  // Send message from client1
  await client1.sendMessage('Hello from client 1');
  
  // Verify client2 receives it
  final received = await client2.messages.first;
  expect(received.content, equals('Hello from client 1'));
});
```

**4. Load Testing:**
Use tools like k6 or Artillery:

```javascript
// k6 script
import ws from 'k6/ws';

export default function() {
  const url = 'wss://your-server/chat';
  const res = ws.connect(url, {}, function(socket) {
    socket.on('open', () => {
      socket.send(JSON.stringify({type: 'subscribe', roomId: 1}));
    });
    socket.on('message', (data) => {
      // Handle messages
    });
  });
}
```

**5. Manual Testing:**
Run the server and use multiple Flutter apps or a WebSocket client like Postman to test real-time behavior.

