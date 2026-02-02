---
type: "EXAMPLE"
title: "Real-Time Message Handling"
---


**Connecting to Serverpod Streams for Real-Time Updates**

This section implements WebSocket connection management, receiving new messages, updating the conversation list, typing indicator display, and connection status handling.



```dart
// lib/features/chat/providers/chat_connection_provider.dart
import 'dart:async';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../core/providers/serverpod_client_provider.dart';

enum ConnectionStatus { disconnected, connecting, connected, reconnecting }

class ChatConnectionNotifier extends Notifier<ConnectionStatus> {
  StreamSubscription? _subscription;
  Timer? _reconnectTimer;
  int _reconnectAttempts = 0;
  static const _maxReconnectAttempts = 5;

  @override
  ConnectionStatus build() {
    ref.onDispose(() {
      _subscription?.cancel();
      _reconnectTimer?.cancel();
    });
    _connect();
    return ConnectionStatus.connecting;
  }

  Future<void> _connect() async {
    state = ConnectionStatus.connecting;

    try {
      final client = ref.read(serverpodClientProvider);
      
      // Connect to the WebSocket stream
      _subscription = client.chat.stream.listen(
        (event) {
          // Handle incoming events
          ref.read(chatEventControllerProvider.notifier).addEvent(event);
        },
        onError: (error) {
          _handleDisconnection();
        },
        onDone: () {
          _handleDisconnection();
        },
      );

      state = ConnectionStatus.connected;
      _reconnectAttempts = 0;
    } catch (e) {
      _handleDisconnection();
    }
  }

  void _handleDisconnection() {
    if (state == ConnectionStatus.disconnected) return;

    _subscription?.cancel();
    _subscription = null;

    if (_reconnectAttempts < _maxReconnectAttempts) {
      state = ConnectionStatus.reconnecting;
      _scheduleReconnect();
    } else {
      state = ConnectionStatus.disconnected;
    }
  }

  void _scheduleReconnect() {
    _reconnectTimer?.cancel();
    
    // Exponential backoff: 1s, 2s, 4s, 8s, 16s
    final delay = Duration(seconds: 1 << _reconnectAttempts);
    _reconnectAttempts++;

    _reconnectTimer = Timer(delay, () {
      _connect();
    });
  }

  void reconnect() {
    _reconnectAttempts = 0;
    _connect();
  }

  void disconnect() {
    _subscription?.cancel();
    _reconnectTimer?.cancel();
    state = ConnectionStatus.disconnected;
  }
}

final chatConnectionProvider =
    NotifierProvider<ChatConnectionNotifier, ConnectionStatus>(() {
  return ChatConnectionNotifier();
});

---

// lib/features/chat/providers/chat_event_provider.dart
import 'dart:async';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../domain/chat_event.dart';

class ChatEventController extends Notifier<ChatEvent?> {
  final _streamController = StreamController<ChatEvent>.broadcast();

  Stream<ChatEvent> get stream => _streamController.stream;

  @override
  ChatEvent? build() {
    ref.onDispose(() {
      _streamController.close();
    });
    return null;
  }

  void addEvent(ChatEvent event) {
    state = event;
    _streamController.add(event);
  }
}

final chatEventControllerProvider =
    NotifierProvider<ChatEventController, ChatEvent?>(() {
  return ChatEventController();
});

final chatStreamProvider = StreamProvider<ChatEvent>((ref) {
  return ref.watch(chatEventControllerProvider.notifier).stream;
});

---

// lib/features/chat/domain/chat_event.dart
import 'package:freezed_annotation/freezed_annotation.dart';
import 'message.dart';

part 'chat_event.freezed.dart';
part 'chat_event.g.dart';

enum ChatEventType {
  newMessage,
  messageRead,
  messageDeleted,
  typingStarted,
  typingStopped,
  userOnline,
  userOffline,
  conversationUpdated,
}

@freezed
class ChatEvent with _$ChatEvent {
  const factory ChatEvent({
    required ChatEventType type,
    String? conversationId,
    String? userId,
    Message? message,
    Map<String, dynamic>? data,
  }) = _ChatEvent;

  factory ChatEvent.fromJson(Map<String, dynamic> json) =>
      _$ChatEventFromJson(json);
}

---

// lib/features/chat/providers/messages_provider.dart
import 'dart:async';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../domain/message.dart';
import '../domain/chat_event.dart';
import '../data/chat_repository.dart';
import 'chat_event_provider.dart';

final messagesProvider = AsyncNotifierProvider.family<
    MessagesNotifier, List<Message>, String>(() {
  return MessagesNotifier();
});

class MessagesNotifier extends FamilyAsyncNotifier<List<Message>, String> {
  String get conversationId => arg;
  int _currentPage = 1;
  bool _hasMore = true;
  StreamSubscription? _eventSubscription;

  @override
  Future<List<Message>> build(String arg) async {
    // Subscribe to real-time events
    _subscribeToEvents();
    ref.onDispose(() {
      _eventSubscription?.cancel();
    });
    return _fetchMessages();
  }

  ChatRepository get _repository => ref.read(chatRepositoryProvider);

  Future<List<Message>> _fetchMessages() async {
    final messages = await _repository.getMessages(
      conversationId: conversationId,
      page: 1,
      limit: 50,
    );
    _currentPage = 1;
    _hasMore = messages.length >= 50;
    return messages;
  }

  void _subscribeToEvents() {
    _eventSubscription = ref.listen(chatStreamProvider, (previous, next) {
      next.whenData((event) => _handleEvent(event));
    });
  }

  void _handleEvent(ChatEvent event) {
    switch (event.type) {
      case ChatEventType.newMessage:
        if (event.conversationId == conversationId && event.message != null) {
          _addMessage(event.message!);
        }
        break;
      case ChatEventType.messageRead:
        if (event.conversationId == conversationId) {
          _markMessagesAsRead();
        }
        break;
      case ChatEventType.messageDeleted:
        if (event.data?['messageId'] != null) {
          _removeMessage(event.data!['messageId']);
        }
        break;
      default:
        break;
    }
  }

  void _addMessage(Message message) {
    state = state.whenData((messages) {
      // Avoid duplicates
      if (messages.any((m) => m.id == message.id)) {
        return messages;
      }
      return [message, ...messages];
    });
  }

  void _removeMessage(String messageId) {
    state = state.whenData((messages) {
      return messages.where((m) => m.id != messageId).toList();
    });
  }

  void _markMessagesAsRead() {
    state = state.whenData((messages) {
      return messages.map((m) {
        if (!m.isFromMe && m.status != MessageStatus.read) {
          return m.copyWith(status: MessageStatus.read);
        }
        return m;
      }).toList();
    });
  }

  Future<void> loadMore() async {
    if (!_hasMore) return;

    try {
      final currentMessages = state.valueOrNull ?? [];
      final nextPage = _currentPage + 1;

      final newMessages = await _repository.getMessages(
        conversationId: conversationId,
        page: nextPage,
        limit: 50,
      );

      if (newMessages.isEmpty) {
        _hasMore = false;
        return;
      }

      _currentPage = nextPage;
      _hasMore = newMessages.length >= 50;

      state = AsyncData([...currentMessages, ...newMessages]);
    } catch (e) {
      // Keep existing messages on error
    }
  }

  Future<void> sendMessage(String content) async {
    final tempId = 'temp_${DateTime.now().millisecondsSinceEpoch}';
    final tempMessage = Message(
      id: tempId,
      conversationId: conversationId,
      senderId: 'currentUserId', // Replace with actual user ID
      senderName: 'Me',
      content: content,
      type: MessageType.text,
      status: MessageStatus.sending,
      createdAt: DateTime.now(),
    );

    // Optimistically add message
    _addMessage(tempMessage);

    try {
      final sentMessage = await _repository.sendMessage(
        conversationId: conversationId,
        content: content,
      );

      // Replace temp message with real one
      state = state.whenData((messages) {
        return messages.map((m) {
          if (m.id == tempId) return sentMessage;
          return m;
        }).toList();
      });
    } catch (e) {
      // Mark as failed
      state = state.whenData((messages) {
        return messages.map((m) {
          if (m.id == tempId) {
            return m.copyWith(status: MessageStatus.failed);
          }
          return m;
        }).toList();
      });
    }
  }

  Future<void> sendImageMessage(String imagePath) async {
    final tempId = 'temp_${DateTime.now().millisecondsSinceEpoch}';
    final tempMessage = Message(
      id: tempId,
      conversationId: conversationId,
      senderId: 'currentUserId',
      senderName: 'Me',
      content: '',
      imageUrl: imagePath, // Local path for preview
      type: MessageType.image,
      status: MessageStatus.sending,
      createdAt: DateTime.now(),
    );

    _addMessage(tempMessage);

    try {
      // Upload image and send message
      final sentMessage = await _repository.sendImageMessage(
        conversationId: conversationId,
        imagePath: imagePath,
      );

      state = state.whenData((messages) {
        return messages.map((m) {
          if (m.id == tempId) return sentMessage;
          return m;
        }).toList();
      });
    } catch (e) {
      state = state.whenData((messages) {
        return messages.map((m) {
          if (m.id == tempId) {
            return m.copyWith(status: MessageStatus.failed);
          }
          return m;
        }).toList();
      });
    }
  }

  Future<void> markAsRead() async {
    try {
      await _repository.markConversationAsRead(conversationId);
    } catch (e) {
      // Silent fail
    }
  }

  Future<void> resendMessage(Message message) async {
    if (!message.isFailed) return;

    // Remove failed message
    _removeMessage(message.id);

    // Resend
    if (message.type == MessageType.image && message.imageUrl != null) {
      await sendImageMessage(message.imageUrl!);
    } else {
      await sendMessage(message.content);
    }
  }
}

---

// lib/features/chat/providers/typing_provider.dart
import 'dart:async';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../domain/chat_event.dart';
import 'chat_event_provider.dart';

final typingUsersProvider =
    NotifierProvider<TypingUsersNotifier, List<String>>(() {
  return TypingUsersNotifier();
});

class TypingUsersNotifier extends Notifier<List<String>> {
  final Map<String, Timer> _typingTimers = {};
  static const _typingTimeout = Duration(seconds: 5);

  @override
  List<String> build() {
    _subscribeToEvents();
    ref.onDispose(() {
      for (final timer in _typingTimers.values) {
        timer.cancel();
      }
    });
    return [];
  }

  void _subscribeToEvents() {
    ref.listen(chatStreamProvider, (previous, next) {
      next.whenData((event) {
        if (event.type == ChatEventType.typingStarted && event.userId != null) {
          _handleTypingStarted(event.userId!, event.data?['userName'] ?? 'Someone');
        } else if (event.type == ChatEventType.typingStopped &&
            event.userId != null) {
          _handleTypingStopped(event.userId!);
        }
      });
    });
  }

  void _handleTypingStarted(String userId, String userName) {
    // Cancel existing timer for this user
    _typingTimers[userId]?.cancel();

    // Add to typing users if not already present
    if (!state.contains(userName)) {
      state = [...state, userName];
    }

    // Set timeout to auto-remove
    _typingTimers[userId] = Timer(_typingTimeout, () {
      _handleTypingStopped(userId);
    });
  }

  void _handleTypingStopped(String userId) {
    _typingTimers[userId]?.cancel();
    _typingTimers.remove(userId);

    // Find and remove the user (we'd need user name mapping in real implementation)
    state = state.where((name) => name != userId).toList();
  }
}

---

// lib/features/chat/data/chat_repository.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../domain/message.dart';
import '../domain/conversation.dart';
import '../../../core/providers/serverpod_client_provider.dart';

final chatRepositoryProvider = Provider<ChatRepository>((ref) {
  return ChatRepository(ref);
});

class ChatRepository {
  final Ref _ref;

  ChatRepository(this._ref);

  Future<List<Conversation>> getConversations() async {
    final client = _ref.read(serverpodClientProvider);
    final response = await client.chat.getConversations();
    return response.map((c) => Conversation.fromJson(c.toJson())).toList();
  }

  Future<List<Message>> getMessages({
    required String conversationId,
    required int page,
    required int limit,
  }) async {
    final client = _ref.read(serverpodClientProvider);
    final response = await client.chat.getMessages(
      conversationId: conversationId,
      page: page,
      limit: limit,
    );
    return response.map((m) => Message.fromJson(m.toJson())).toList();
  }

  Future<Message> sendMessage({
    required String conversationId,
    required String content,
  }) async {
    final client = _ref.read(serverpodClientProvider);
    final response = await client.chat.sendMessage(
      conversationId: conversationId,
      content: content,
    );
    return Message.fromJson(response.toJson());
  }

  Future<Message> sendImageMessage({
    required String conversationId,
    required String imagePath,
  }) async {
    final client = _ref.read(serverpodClientProvider);
    
    // First upload the image
    final imageUrl = await client.upload.uploadFile(imagePath);
    
    // Then send the message
    final response = await client.chat.sendMessage(
      conversationId: conversationId,
      content: '',
      imageUrl: imageUrl,
      type: 'image',
    );
    return Message.fromJson(response.toJson());
  }

  Future<void> markConversationAsRead(String conversationId) async {
    final client = _ref.read(serverpodClientProvider);
    await client.chat.markAsRead(conversationId: conversationId);
  }

  Future<void> sendTypingIndicator(String conversationId) async {
    final client = _ref.read(serverpodClientProvider);
    await client.chat.sendTyping(conversationId: conversationId);
  }
}
```
