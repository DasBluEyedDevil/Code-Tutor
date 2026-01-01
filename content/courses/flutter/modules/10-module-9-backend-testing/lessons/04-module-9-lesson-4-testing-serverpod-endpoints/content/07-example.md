---
type: "EXAMPLE"
title: "Mocking External Dependencies"
---

Here is how to inject mocks for external services:

```dart
// test/unit/notification_service_test.dart
import 'package:test/test.dart';
import 'package:mocktail/mocktail.dart';
import 'package:serverpod_test/serverpod_test.dart';

class MockEmailService extends Mock implements EmailService {}
class MockPushNotificationService extends Mock implements PushNotificationService {}

void main() {
  late TestSession session;
  late MockEmailService mockEmailService;
  late MockPushNotificationService mockPushService;
  late NotificationEndpoint endpoint;

  setUp(() async {
    session = await TestSession.create();
    mockEmailService = MockEmailService();
    mockPushService = MockPushNotificationService();
    
    // Inject mocks into the session
    session.serverpod.registerSingleton<EmailService>(mockEmailService);
    session.serverpod.registerSingleton<PushNotificationService>(mockPushService);
    
    endpoint = NotificationEndpoint();
  });

  tearDown(() async {
    await session.close();
  });

  group('NotificationEndpoint', () {
    test('sendWelcomeEmail sends email to new user', () async {
      // Arrange
      when(() => mockEmailService.send(
        to: any(named: 'to'),
        subject: any(named: 'subject'),
        body: any(named: 'body'),
      )).thenAnswer((_) async => true);

      // Act
      await endpoint.sendWelcomeEmail(
        session,
        userId: 1,
        email: 'newuser@example.com',
      );

      // Assert
      verify(() => mockEmailService.send(
        to: 'newuser@example.com',
        subject: contains('Welcome'),
        body: any(named: 'body'),
      )).called(1);
    });

    test('notifyUser sends push notification when enabled', () async {
      // Arrange
      when(() => mockPushService.sendToUser(
        userId: any(named: 'userId'),
        title: any(named: 'title'),
        message: any(named: 'message'),
      )).thenAnswer((_) async => true);

      // Create user with notifications enabled
      final user = User(
        id: 1,
        name: 'Test User',
        email: 'test@example.com',
        pushNotificationsEnabled: true,
        createdAt: DateTime.now(),
      );
      await User.db.insertRow(session, user);

      // Act
      await endpoint.notifyUser(
        session,
        userId: 1,
        message: 'Hello!',
      );

      // Assert
      verify(() => mockPushService.sendToUser(
        userId: 1,
        title: any(named: 'title'),
        message: 'Hello!',
      )).called(1);
    });

    test('notifyUser skips push when disabled', () async {
      // Create user with notifications disabled
      final user = User(
        id: 1,
        name: 'Test User',
        email: 'test@example.com',
        pushNotificationsEnabled: false,
        createdAt: DateTime.now(),
      );
      await User.db.insertRow(session, user);

      // Act
      await endpoint.notifyUser(
        session,
        userId: 1,
        message: 'Hello!',
      );

      // Assert - push was never called
      verifyNever(() => mockPushService.sendToUser(
        userId: any(named: 'userId'),
        title: any(named: 'title'),
        message: any(named: 'message'),
      ));
    });
  });
}
```
