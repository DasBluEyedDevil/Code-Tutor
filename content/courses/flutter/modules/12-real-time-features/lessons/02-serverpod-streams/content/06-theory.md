---
type: "THEORY"
title: "Connection State Management"
---

Managing connection state is crucial for a good user experience. Users should know when they're connected, disconnected, or reconnecting.

**StreamingConnectionStatus:**

Serverpod provides a stream of connection status updates that you can use to update your UI and trigger reconnection logic.

**Status Values:**
- `disconnected`: No active connection
- `connecting`: Connection in progress
- `connected`: Active connection
- `reconnecting`: Lost connection, attempting to reconnect

**Best Practices:**
- Show connection status in UI (green/yellow/red indicator)
- Disable send buttons when disconnected
- Queue messages during brief disconnections
- Show reconnection progress to users



```dart
// Connection state management with Riverpod
import 'package:flutter_riverpod/flutter_riverpod.dart';

enum ConnectionState {
  disconnected,
  connecting,
  connected,
  reconnecting,
}

class ChatNotifier extends StateNotifier<ChatState> {
  final Client _client;
  StreamSubscription? _statusSubscription;
  Timer? _reconnectTimer;
  int _reconnectAttempts = 0;
  static const _maxReconnectAttempts = 5;
  
  ChatNotifier(this._client) : super(ChatState.initial());
  
  Future<void> connect() async {
    state = state.copyWith(connectionState: ConnectionState.connecting);
    
    try {
      await _client.openStreamingConnection();
      
      _statusSubscription = _client.streamingConnectionStatus.listen(
        _handleConnectionStatus,
      );
      
      _reconnectAttempts = 0;
      state = state.copyWith(connectionState: ConnectionState.connected);
      
    } catch (e) {
      state = state.copyWith(
        connectionState: ConnectionState.disconnected,
        error: 'Failed to connect: $e',
      );
      _scheduleReconnect();
    }
  }
  
  void _handleConnectionStatus(StreamingConnectionStatus status) {
    switch (status) {
      case StreamingConnectionStatus.connected:
        _reconnectAttempts = 0;
        state = state.copyWith(
          connectionState: ConnectionState.connected,
          error: null,
        );
        break;
        
      case StreamingConnectionStatus.disconnected:
        state = state.copyWith(
          connectionState: ConnectionState.disconnected,
        );
        _scheduleReconnect();
        break;
    }
  }
  
  void _scheduleReconnect() {
    if (_reconnectAttempts >= _maxReconnectAttempts) {
      state = state.copyWith(
        error: 'Connection failed after $_maxReconnectAttempts attempts',
      );
      return;
    }
    
    _reconnectAttempts++;
    state = state.copyWith(connectionState: ConnectionState.reconnecting);
    
    // Exponential backoff: 1s, 2s, 4s, 8s, 16s
    final delay = Duration(seconds: 1 << (_reconnectAttempts - 1));
    
    _reconnectTimer?.cancel();
    _reconnectTimer = Timer(delay, () {
      connect();
    });
  }
  
  @override
  void dispose() {
    _statusSubscription?.cancel();
    _reconnectTimer?.cancel();
    _client.closeStreamingConnection();
    super.dispose();
  }
}

class ChatState {
  final ConnectionState connectionState;
  final List<ChatMessage> messages;
  final String? error;
  
  const ChatState({
    required this.connectionState,
    required this.messages,
    this.error,
  });
  
  factory ChatState.initial() => const ChatState(
    connectionState: ConnectionState.disconnected,
    messages: [],
  );
  
  ChatState copyWith({
    ConnectionState? connectionState,
    List<ChatMessage>? messages,
    String? error,
  }) {
    return ChatState(
      connectionState: connectionState ?? this.connectionState,
      messages: messages ?? this.messages,
      error: error,
    );
  }
}
```
