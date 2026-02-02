---
type: "THEORY"
title: "Server-Sent Events: Simpler Server Push"
---

Server-Sent Events (SSE) provide a unidirectional channel where the server can push updates to the client. Unlike WebSockets, SSE uses standard HTTP and is simpler to implement for server-to-client streaming.

**How SSE Works:**

1. Client opens an HTTP connection with `Accept: text/event-stream`
2. Server keeps the connection open
3. Server sends events as text in a specific format
4. Client receives events through the stream
5. Built-in reconnection with Last-Event-ID header

**SSE Event Format:**

Events are plain text with specific fields:
- `data:` - The event payload
- `event:` - Optional event type name
- `id:` - Optional event ID for reconnection
- `retry:` - Reconnection delay in milliseconds

**Advantages:**

- Simpler than WebSockets
- Uses standard HTTP (better proxy support)
- Automatic reconnection built-in
- Works with HTTP/2 multiplexing
- Text-based, easy to debug
- Native browser support (EventSource API)

**Disadvantages:**

- Unidirectional (server to client only)
- Client must use separate requests to send data
- Text only (no binary data)
- Limited browser connection pool

**When to Use SSE:**

- Notifications and alerts
- Live feeds (news, social, sports)
- Real-time dashboards
- Progress updates for long operations
- Any scenario where client only receives data



```dart
import 'dart:async';
import 'dart:convert';
import 'package:http/http.dart' as http;

class SSEService {
  StreamSubscription? _subscription;
  http.Client? _client;
  
  // Connect to SSE endpoint
  Stream<SSEEvent> connect(String url) async* {
    _client = http.Client();
    
    final request = http.Request('GET', Uri.parse(url));
    request.headers['Accept'] = 'text/event-stream';
    request.headers['Cache-Control'] = 'no-cache';
    
    final response = await _client!.send(request);
    
    String buffer = '';
    
    await for (final chunk in response.stream.transform(utf8.decoder)) {
      buffer += chunk;
      
      // Parse complete events from buffer
      while (buffer.contains('\n\n')) {
        final eventEnd = buffer.indexOf('\n\n');
        final eventStr = buffer.substring(0, eventEnd);
        buffer = buffer.substring(eventEnd + 2);
        
        final event = _parseEvent(eventStr);
        if (event != null) {
          yield event;
        }
      }
    }
  }
  
  SSEEvent? _parseEvent(String eventString) {
    String? data;
    String? event;
    String? id;
    
    for (final line in eventString.split('\n')) {
      if (line.startsWith('data:')) {
        data = line.substring(5).trim();
      } else if (line.startsWith('event:')) {
        event = line.substring(6).trim();
      } else if (line.startsWith('id:')) {
        id = line.substring(3).trim();
      }
    }
    
    if (data != null) {
      return SSEEvent(data: data, event: event, id: id);
    }
    return null;
  }
  
  void disconnect() {
    _client?.close();
  }
}

class SSEEvent {
  final String data;
  final String? event;
  final String? id;
  
  SSEEvent({required this.data, this.event, this.id});
}

// Usage for live notifications
void listenForNotifications() async {
  final sse = SSEService();
  
  await for (final event in sse.connect('https://api.example.com/notifications/stream')) {
    if (event.event == 'notification') {
      final notification = jsonDecode(event.data);
      showNotification(notification);
    } else if (event.event == 'heartbeat') {
      // Server is alive, connection healthy
    }
  }
}
```
