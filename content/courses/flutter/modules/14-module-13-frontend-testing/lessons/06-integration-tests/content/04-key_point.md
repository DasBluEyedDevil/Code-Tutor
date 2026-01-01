---
type: "KEY_POINT"
title: "Robot Pattern for Readable Tests"
---


The **Robot Pattern** encapsulates UI interactions in reusable classes:

```dart
// robots/login_robot.dart
class LoginRobot {
  final WidgetTester tester;
  LoginRobot(this.tester);

  Future<void> enterEmail(String email) async {
    await tester.enterText(
      find.byKey(const Key('email-field')),
      email,
    );
  }

  Future<void> enterPassword(String password) async {
    await tester.enterText(
      find.byKey(const Key('password-field')),
      password,
    );
  }

  Future<void> tapLogin() async {
    await tester.tap(find.text('Login'));
    await tester.pumpAndSettle();
  }

  Future<void> login(String email, String password) async {
    await enterEmail(email);
    await enterPassword(password);
    await tapLogin();
  }
}

// Usage in test:
final loginRobot = LoginRobot(tester);
await loginRobot.login('user@test.com', 'password123');
```

