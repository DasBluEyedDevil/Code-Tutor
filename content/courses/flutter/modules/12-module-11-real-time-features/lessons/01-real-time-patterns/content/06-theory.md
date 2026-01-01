---
type: "THEORY"
title: "Choosing the Right Pattern"
---

Each real-time pattern has its place. The right choice depends on your specific requirements, infrastructure, and constraints.

**Decision Matrix:**

| Factor | Polling | Long-Polling | WebSocket | SSE |
|--------|---------|--------------|-----------|-----|
| Latency | High (interval) | Medium | Low | Low |
| Server Load | High | Medium | Low | Low |
| Bidirectional | No | No | Yes | No |
| Complexity | Low | Medium | High | Medium |
| Scalability | Easy | Medium | Hard | Easy |
| Browser Support | All | All | Modern | Modern |
| Binary Data | Yes | Yes | Yes | No |
| Reconnection | Manual | Manual | Manual | Auto |

**Choose Polling When:**

- Update frequency is low (minutes/hours)
- Simplicity is more important than efficiency
- Supporting legacy systems
- Quick prototyping

**Choose Long-Polling When:**

- Need faster updates than polling
- WebSocket infrastructure unavailable
- Moderate scale requirements

**Choose WebSockets When:**

- Bidirectional communication needed
- High-frequency updates
- Chat, gaming, or collaboration apps
- Client needs to send data frequently

**Choose SSE When:**

- Server-to-client only
- Simpler than WebSocket is acceptable
- Notifications, feeds, or dashboards
- HTTP infrastructure is a requirement

**Serverpod Uses WebSockets:**

Serverpod, the Flutter backend framework, uses WebSockets for its real-time streaming capabilities. This enables bidirectional communication, live method calls, and efficient data streaming between Flutter apps and Serverpod backends.



```dart
// Summary: Pattern selection helper

enum RealTimePattern {
  polling,
  longPolling,
  webSocket,
  serverSentEvents,
}

class RealTimePatternSelector {
  static RealTimePattern recommend({
    required bool needsBidirectional,
    required Duration updateFrequency,
    required int expectedConcurrentUsers,
    required bool hasWebSocketSupport,
  }) {
    // Need bidirectional? WebSocket is the answer
    if (needsBidirectional) {
      return RealTimePattern.webSocket;
    }
    
    // Updates are infrequent? Polling is fine
    if (updateFrequency > Duration(minutes: 1)) {
      return RealTimePattern.polling;
    }
    
    // No WebSocket support? Use long-polling or SSE
    if (!hasWebSocketSupport) {
      return updateFrequency < Duration(seconds: 5)
          ? RealTimePattern.longPolling
          : RealTimePattern.polling;
    }
    
    // Server-to-client streaming? SSE is simpler
    if (expectedConcurrentUsers < 1000) {
      return RealTimePattern.serverSentEvents;
    }
    
    // Default to WebSocket for flexibility
    return RealTimePattern.webSocket;
  }
}

// Example usage
void main() {
  // Chat app: bidirectional, real-time
  final chatPattern = RealTimePatternSelector.recommend(
    needsBidirectional: true,
    updateFrequency: Duration(milliseconds: 100),
    expectedConcurrentUsers: 10000,
    hasWebSocketSupport: true,
  );
  print('Chat app: $chatPattern'); // webSocket
  
  // News feed: server-push only
  final feedPattern = RealTimePatternSelector.recommend(
    needsBidirectional: false,
    updateFrequency: Duration(seconds: 30),
    expectedConcurrentUsers: 500,
    hasWebSocketSupport: true,
  );
  print('News feed: $feedPattern'); // serverSentEvents
  
  // Analytics dashboard: infrequent updates
  final dashPattern = RealTimePatternSelector.recommend(
    needsBidirectional: false,
    updateFrequency: Duration(minutes: 5),
    expectedConcurrentUsers: 50,
    hasWebSocketSupport: true,
  );
  print('Dashboard: $dashPattern'); // polling
}
```
