---
type: "THEORY"
title: "Long-Polling: Smarter Polling"
---

Long-polling improves on regular polling by keeping the connection open until the server has new data to send.

**How Long-Polling Works:**

1. Client sends a request to the server
2. Server holds the connection open (doesn't respond immediately)
3. When new data is available, server responds
4. Client immediately sends another request
5. If timeout occurs, server responds with empty data, client reconnects

**Advantages Over Regular Polling:**

- Near-instant updates when data changes
- Less wasted bandwidth (no empty responses)
- Reduced latency compared to fixed intervals
- Still uses standard HTTP (no special protocols)

**Disadvantages:**

- Holds server connections open (resource intensive)
- Timeout handling adds complexity
- Still not true bidirectional communication
- Connection overhead on each new request
- Harder to scale than regular polling

**Server Considerations:**

Long-polling requires servers that can handle many concurrent held connections. Traditional thread-per-request servers may struggle; async/event-driven servers work better.

**When to Use Long-Polling:**

- Need faster updates than regular polling
- WebSocket support is unavailable
- Moderate number of concurrent users
- Server infrastructure supports held connections



```dart
import 'package:http/http.dart' as http;

class LongPollingService {
  bool _isPolling = false;
  final String endpoint;
  final Duration timeout;
  
  LongPollingService({
    required this.endpoint,
    this.timeout = const Duration(seconds: 30),
  });
  
  // Start long-polling loop
  Future<void> startPolling(Function(String) onData) async {
    _isPolling = true;
    
    while (_isPolling) {
      try {
        final response = await http.get(
          Uri.parse(endpoint),
        ).timeout(timeout);
        
        if (response.statusCode == 200) {
          final data = response.body;
          if (data.isNotEmpty) {
            onData(data);
          }
        }
        
        // Immediately reconnect for next update
        // Small delay to prevent tight loop on errors
        await Future.delayed(Duration(milliseconds: 100));
        
      } on TimeoutException {
        // Timeout is normal - server had no updates
        // Immediately reconnect
        continue;
      } catch (e) {
        print('Long-polling error: $e');
        // Wait before retry on error
        await Future.delayed(Duration(seconds: 2));
      }
    }
  }
  
  void stopPolling() {
    _isPolling = false;
  }
}

// Server-side example (Dart/Serverpod-style)
// The server holds the request until data is ready:
// 
// Future<Response> longPollMessages(Request request) async {
//   final lastId = request.queryParams['lastId'];
//   
//   // Wait for new messages (up to 30 seconds)
//   final messages = await messageQueue.waitForNew(
//     afterId: lastId,
//     timeout: Duration(seconds: 30),
//   );
//   
//   return Response.json(messages);
// }
```
