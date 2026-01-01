class NotificationMessage {
  final String id;
  final String title;
  final String body;
  final DateTime timestamp;
  bool isRead;
  
  NotificationMessage({
    required this.id,
    required this.title,
    required this.body,
    required this.timestamp,
    this.isRead = false,
  });
}

class NotificationService {
  // TODO: Add fields for client, subscriptions, controllers
  
  Stream<List<NotificationMessage>> get notifications {
    // TODO: Return notifications stream
    throw UnimplementedError();
  }
  
  Stream<int> get unreadCount {
    // TODO: Return unread count stream
    throw UnimplementedError();
  }
  
  Future<void> connect() async {
    // TODO: Connect to notification stream
  }
  
  void markAsRead(String id) {
    // TODO: Mark single notification as read
  }
  
  void markAllAsRead() {
    // TODO: Mark all notifications as read
  }
  
  void dispose() {
    // TODO: Clean up resources
  }
}