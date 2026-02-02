enum ConnectionState {
  disconnected,
  connecting,
  connected,
  reconnecting,
}

class StreamingConnectionManager {
  // TODO: Add fields for client, subscriptions, timers
  
  Stream<ConnectionState> get connectionState {
    // TODO: Return connection state stream
    throw UnimplementedError();
  }
  
  Future<void> connect() async {
    // TODO: Implement connection logic
  }
  
  Future<void> reconnect() async {
    // TODO: Implement manual reconnect
  }
  
  void _scheduleReconnect() {
    // TODO: Implement exponential backoff
  }
  
  void dispose() {
    // TODO: Clean up all resources
  }
}