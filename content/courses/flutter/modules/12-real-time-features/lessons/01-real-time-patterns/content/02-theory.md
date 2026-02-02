---
type: "THEORY"
title: "Polling: The Simplest Approach"
---

Polling is the simplest real-time pattern where the client repeatedly asks the server for updates at fixed intervals.

**How Polling Works:**

1. Client sends a request to the server
2. Server responds with current data
3. Client waits for a fixed interval (e.g., 5 seconds)
4. Client sends another request
5. Repeat indefinitely

**Advantages:**

- Simple to implement with standard HTTP
- Works with any server infrastructure
- No special protocol support needed
- Easy to debug and monitor
- Stateless - each request is independent

**Disadvantages:**

- Wastes bandwidth when no updates exist
- Introduces latency (up to the polling interval)
- Increases server load with many clients
- Battery drain on mobile devices
- Not truly real-time - just periodic updates

**When to Use Polling:**

- Data changes infrequently (hourly updates)
- Simple dashboards with few concurrent users
- Legacy systems without WebSocket support
- Prototyping before implementing proper real-time



```dart
import 'dart:async';
import 'package:http/http.dart' as http;

class PollingService {
  Timer? _timer;
  final Duration interval;
  final String endpoint;
  
  PollingService({
    required this.endpoint,
    this.interval = const Duration(seconds: 5),
  });
  
  // Start polling
  void startPolling(Function(String) onData) {
    // Fetch immediately, then at intervals
    _fetchData(onData);
    
    _timer = Timer.periodic(interval, (_) {
      _fetchData(onData);
    });
  }
  
  Future<void> _fetchData(Function(String) onData) async {
    try {
      final response = await http.get(Uri.parse(endpoint));
      if (response.statusCode == 200) {
        onData(response.body);
      }
    } catch (e) {
      print('Polling error: $e');
    }
  }
  
  // Stop polling
  void stopPolling() {
    _timer?.cancel();
    _timer = null;
  }
}

// Usage
final poller = PollingService(
  endpoint: 'https://api.example.com/messages',
  interval: Duration(seconds: 10),
);

poller.startPolling((data) {
  print('Received: $data');
});
```
