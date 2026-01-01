---
type: "EXAMPLE"
title: "Handling Notifications"
---

Notifications need to be handled differently depending on the app state: foreground, background, or terminated. Each state requires specific handling to provide a seamless user experience.

**App States:**

- **Foreground**: App is open and visible - you control display
- **Background**: App is in memory but not visible - system shows notification
- **Terminated**: App is not running - system shows notification

**Deep Linking:**

When users tap a notification, navigate them to the relevant screen. Use data payloads to specify the destination.



```dart
class NotificationHandler {
  static final NotificationHandler instance = NotificationHandler._();
  NotificationHandler._();
  
  final GlobalKey<NavigatorState> navigatorKey = GlobalKey<NavigatorState>();
  
  Future<void> initialize() async {
    // Handle foreground messages
    FirebaseMessaging.onMessage.listen(_handleForegroundMessage);
    
    // Handle background message taps
    FirebaseMessaging.onMessageOpenedApp.listen(_handleNotificationTap);
    
    // Check if app was opened from terminated state via notification
    final initialMessage = await FirebaseMessaging.instance.getInitialMessage();
    if (initialMessage != null) {
      // Delay to ensure app is fully initialized
      Future.delayed(Duration(milliseconds: 500), () {
        _handleNotificationTap(initialMessage);
      });
    }
  }
  
  void _handleForegroundMessage(RemoteMessage message) {
    print('Foreground message: ${message.notification?.title}');
    
    // Show in-app notification
    _showInAppNotification(message);
    
    // Or update UI directly if on relevant screen
    _notifyListeners(message);
  }
  
  void _handleNotificationTap(RemoteMessage message) {
    print('Notification tapped: ${message.data}');
    
    // Extract navigation data
    final type = message.data['type'];
    final id = message.data['id'];
    
    // Navigate based on notification type
    switch (type) {
      case 'chat':
        _navigateTo('/chat/$id');
        break;
      case 'order':
        _navigateTo('/orders/$id');
        break;
      case 'listing':
        _navigateTo('/listings/$id');
        break;
      default:
        _navigateTo('/notifications');
    }
  }
  
  void _showInAppNotification(RemoteMessage message) {
    final context = navigatorKey.currentContext;
    if (context == null) return;
    
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              message.notification?.title ?? 'New notification',
              style: TextStyle(fontWeight: FontWeight.bold),
            ),
            if (message.notification?.body != null)
              Text(message.notification!.body!),
          ],
        ),
        action: SnackBarAction(
          label: 'View',
          onPressed: () => _handleNotificationTap(message),
        ),
        duration: Duration(seconds: 4),
        behavior: SnackBarBehavior.floating,
      ),
    );
  }
  
  void _navigateTo(String route) {
    navigatorKey.currentState?.pushNamed(route);
  }
  
  // Stream for widgets to listen to
  final _notificationController = StreamController<RemoteMessage>.broadcast();
  Stream<RemoteMessage> get notifications => _notificationController.stream;
  
  void _notifyListeners(RemoteMessage message) {
    _notificationController.add(message);
  }
  
  void dispose() {
    _notificationController.close();
  }
}

// Background message handler (must be top-level function)
@pragma('vm:entry-point')
Future<void> _firebaseMessagingBackgroundHandler(RemoteMessage message) async {
  await Firebase.initializeApp();
  print('Background message: ${message.messageId}');
  
  // Process data payload
  // Note: Cannot update UI from here, only process data
  if (message.data['type'] == 'sync') {
    // Trigger background sync
    await BackgroundSync.process(message.data);
  }
}

// Register in main.dart before runApp:
// FirebaseMessaging.onBackgroundMessage(_firebaseMessagingBackgroundHandler);
```
