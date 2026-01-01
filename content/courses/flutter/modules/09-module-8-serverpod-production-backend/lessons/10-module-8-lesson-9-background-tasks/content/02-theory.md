---
type: "THEORY"
title: "The Problem with Synchronous Processing"
---

Before diving into solutions, let us understand why synchronous processing fails at scale.

**Scenario: User Registration Endpoint**

A naive implementation might look like this:

```dart
Future<User> register(Session session, String email, String password) async {
  // 1. Create user in database (50ms)
  final user = await User.db.insertRow(session, User(email: email, ...));
  
  // 2. Send welcome email (800ms - external API call)
  await emailService.sendWelcomeEmail(user.email);
  
  // 3. Create default user settings (30ms)
  await UserSettings.db.insertRow(session, UserSettings(userId: user.id!, ...));
  
  // 4. Log to analytics (200ms - external API)
  await analyticsService.trackSignup(user.id!);
  
  // 5. Notify team on Slack (300ms - external API)
  await slackService.notifyNewUser(user.email);
  
  // Total: 1380ms - user waits over a second!
  return user;
}
```

**Problems with this approach:**

1. **Slow Response Time**: User waits 1.4 seconds for what should be instant
2. **Fragile**: If Slack API is down, registration fails entirely
3. **No Retries**: Email fails once? User never gets welcome email
4. **Resource Hogging**: Connection held open during all external calls
5. **Poor UX**: Users abandon slow registration flows

**The Solution: Background Tasks**

```dart
Future<User> register(Session session, String email, String password) async {
  // 1. Create user in database (50ms) - MUST be synchronous
  final user = await User.db.insertRow(session, User(email: email, ...));
  
  // 2. Queue everything else for background processing
  await session.messages.postMessage(
    'user-registered',
    UserRegisteredEvent(userId: user.id!, email: user.email),
  );
  
  // Total: 60ms - user sees success immediately!
  return user;
}

// Background worker handles the rest asynchronously
```

