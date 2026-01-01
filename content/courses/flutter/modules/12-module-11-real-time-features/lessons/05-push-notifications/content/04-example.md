---
type: "EXAMPLE"
title: "Sending from Backend"
---

Your Serverpod backend sends push notifications through the FCM HTTP API. This allows you to target specific users, topics, or conditions with customized payloads.

**Serverpod Integration:**

Create a dedicated notification service that handles token storage and message sending.

**Notification Types:**

- **Direct**: Send to specific device tokens
- **Topic**: Send to all subscribers of a topic
- **Condition**: Complex targeting with boolean logic



```dart
// server/lib/src/services/push_notification_service.dart
import 'package:http/http.dart' as http;
import 'dart:convert';

class PushNotificationService {
  static const String _fcmEndpoint = 'https://fcm.googleapis.com/fcm/send';
  final String _serverKey; // From Firebase Console
  
  PushNotificationService(this._serverKey);
  
  // Send to specific device
  Future<bool> sendToDevice({
    required String deviceToken,
    required String title,
    required String body,
    Map<String, String>? data,
    String? imageUrl,
  }) async {
    final payload = {
      'to': deviceToken,
      'notification': {
        'title': title,
        'body': body,
        if (imageUrl != null) 'image': imageUrl,
      },
      'data': data ?? {},
      'priority': 'high',
      'content_available': true, // For iOS background
    };
    
    return _sendNotification(payload);
  }
  
  // Send to multiple devices
  Future<bool> sendToDevices({
    required List<String> deviceTokens,
    required String title,
    required String body,
    Map<String, String>? data,
  }) async {
    final payload = {
      'registration_ids': deviceTokens,
      'notification': {
        'title': title,
        'body': body,
      },
      'data': data ?? {},
      'priority': 'high',
    };
    
    return _sendNotification(payload);
  }
  
  // Send to topic subscribers
  Future<bool> sendToTopic({
    required String topic,
    required String title,
    required String body,
    Map<String, String>? data,
  }) async {
    final payload = {
      'to': '/topics/$topic',
      'notification': {
        'title': title,
        'body': body,
      },
      'data': data ?? {},
    };
    
    return _sendNotification(payload);
  }
  
  Future<bool> _sendNotification(Map<String, dynamic> payload) async {
    try {
      final response = await http.post(
        Uri.parse(_fcmEndpoint),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'key=$_serverKey',
        },
        body: jsonEncode(payload),
      );
      
      if (response.statusCode == 200) {
        final result = jsonDecode(response.body);
        return result['success'] == 1;
      }
      
      print('FCM error: ${response.body}');
      return false;
    } catch (e) {
      print('Failed to send notification: $e');
      return false;
    }
  }
}

// Notification endpoint for common use cases
class NotificationEndpoint extends Endpoint {
  late final PushNotificationService _pushService;
  
  @override
  void initialize(Server server, String name, String? moduleName) {
    super.initialize(server, name, moduleName);
    _pushService = PushNotificationService(
      server.config['fcmServerKey'] as String,
    );
  }
  
  // Register device token
  Future<void> registerDevice(
    Session session, {
    required String token,
    required String platform,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw UnauthorizedException();
    
    await DeviceToken.db.upsert(
      session,
      DeviceToken(
        userId: userId,
        token: token,
        platform: platform,
        updatedAt: DateTime.now(),
      ),
    );
  }
  
  // Send chat notification
  Future<void> notifyNewMessage(
    Session session, {
    required int recipientUserId,
    required String senderName,
    required String messagePreview,
    required String conversationId,
  }) async {
    final tokens = await DeviceToken.db.find(
      session,
      where: (t) => t.userId.equals(recipientUserId),
    );
    
    if (tokens.isEmpty) return;
    
    await _pushService.sendToDevices(
      deviceTokens: tokens.map((t) => t.token).toList(),
      title: senderName,
      body: messagePreview,
      data: {
        'type': 'chat',
        'id': conversationId,
      },
    );
  }
}
```
