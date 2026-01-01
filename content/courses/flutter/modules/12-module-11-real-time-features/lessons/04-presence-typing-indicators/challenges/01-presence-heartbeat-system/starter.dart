// Server-side HeartbeatManager
class HeartbeatManager {
  static const Duration pingInterval = Duration(seconds: 30);
  static const Duration pongTimeout = Duration(seconds: 5);
  static const int maxMissedPings = 3;
  
  // Track: sessionId -> missed ping count
  final Map<String, int> _missedPings = {};
  
  // Track: sessionId -> pending pong timer
  final Map<String, Timer> _pongTimers = {};
  
  Timer? _pingTimer;
  
  void startHeartbeat(List<StreamingSession> activeSessions) {
    // TODO: Start periodic ping timer
    // For each session, send ping and start pong timeout
    throw UnimplementedError();
  }
  
  void handlePong(String sessionId) {
    // TODO: Cancel pong timeout timer
    // Reset missed ping count for this session
    throw UnimplementedError();
  }
  
  void _onPongTimeout(String sessionId) {
    // TODO: Increment missed ping count
    // If exceeded max, disconnect and mark offline
    throw UnimplementedError();
  }
  
  void dispose() {
    // TODO: Clean up all timers
    throw UnimplementedError();
  }
}

// Client-side HeartbeatService
class HeartbeatService {
  final StreamSubscription? _messageSubscription;
  final Function() onDisconnected;
  
  int _reconnectAttempts = 0;
  Timer? _reconnectTimer;
  
  HeartbeatService({
    required this.onDisconnected,
  });
  
  void handlePing() {
    // TODO: Send pong response immediately
    throw UnimplementedError();
  }
  
  void _attemptReconnect() {
    // TODO: Implement exponential backoff
    // delay = min(30 seconds, 2^attempts seconds)
    throw UnimplementedError();
  }
  
  void dispose() {
    // TODO: Clean up
    throw UnimplementedError();
  }
}