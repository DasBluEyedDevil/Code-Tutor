---
type: "THEORY"
title: "Stream Lifecycle Management"
---

Proper stream lifecycle management is essential to avoid memory leaks. Every stream subscription must be cancelled when it's no longer needed.

**Memory Leak Patterns to Avoid:**

1. Not cancelling subscriptions in dispose()
2. Creating subscriptions without storing references
3. Opening connections without closing them
4. Listeners not removed when widget unmounts

**Safe Patterns:**

- Store all StreamSubscription references
- Cancel in dispose() or when leaving screen
- Use `cancelOnError: true` for one-shot streams
- Consider using `flutter_hooks` for automatic cleanup



```dart
// Proper lifecycle management in a StatefulWidget
class ChatScreen extends StatefulWidget {
  final String roomId;
  
  const ChatScreen({required this.roomId, super.key});
  
  @override
  State<ChatScreen> createState() => _ChatScreenState();
}

class _ChatScreenState extends State<ChatScreen> {
  late final ChatService _chatService;
  StreamSubscription? _messagesSubscription;
  StreamSubscription? _statusSubscription;
  final List<ChatMessage> _messages = [];
  bool _isConnected = false;
  
  @override
  void initState() {
    super.initState();
    _chatService = ChatService(getIt<Client>());
    _connectToChat();
  }
  
  Future<void> _connectToChat() async {
    await _chatService.connect(widget.roomId);
    
    // Store subscription reference for cleanup
    _messagesSubscription = _chatService.messages.listen(
      (message) {
        setState(() {
          _messages.add(message);
        });
      },
      onError: (error) {
        _showError('Message error: $error');
      },
    );
    
    _statusSubscription = _chatService.connectionStatus.listen(
      (isConnected) {
        setState(() {
          _isConnected = isConnected;
        });
      },
    );
  }
  
  @override
  void dispose() {
    // CRITICAL: Cancel all subscriptions
    _messagesSubscription?.cancel();
    _statusSubscription?.cancel();
    
    // Close the connection
    _chatService.disconnect();
    
    super.dispose();
  }
  
  void _showError(String message) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text(message)),
    );
  }
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Chat'),
        actions: [
          // Connection status indicator
          Icon(
            Icons.circle,
            color: _isConnected ? Colors.green : Colors.red,
            size: 12,
          ),
          const SizedBox(width: 16),
        ],
      ),
      body: ListView.builder(
        itemCount: _messages.length,
        itemBuilder: (context, index) {
          final message = _messages[index];
          return ListTile(
            title: Text(message.text),
            subtitle: Text(message.timestamp.toString()),
          );
        },
      ),
    );
  }
}

// Using flutter_hooks for automatic cleanup
class ChatScreenHooks extends HookWidget {
  final String roomId;
  
  const ChatScreenHooks({required this.roomId, super.key});
  
  @override
  Widget build(BuildContext context) {
    final chatService = useMemoized(() => ChatService(getIt<Client>()));
    final messages = useState<List<ChatMessage>>([]);
    final isConnected = useState(false);
    
    // Automatically cleaned up when widget disposes
    useEffect(() {
      chatService.connect(roomId);
      
      final messagesSubscription = chatService.messages.listen((msg) {
        messages.value = [...messages.value, msg];
      });
      
      final statusSubscription = chatService.connectionStatus.listen((status) {
        isConnected.value = status;
      });
      
      // Return cleanup function
      return () {
        messagesSubscription.cancel();
        statusSubscription.cancel();
        chatService.disconnect();
      };
    }, [roomId]);
    
    return Scaffold(
      // ... UI implementation
    );
  }
}
```
