import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:integration_test/integration_test.dart';
import 'package:my_app/main.dart' as app;

void main() {
  IntegrationTestWidgetsFlutterBinding.ensureInitialized();

  group('login flow', () {
    testWidgets('successful login navigates to home', (tester) async {
      app.main();
      await tester.pumpAndSettle();

      // Verify we're on login screen
      expect(find.text('Login'), findsOneWidget);

      // Enter credentials
      await tester.enterText(
        find.byKey(const Key('email-field')),
        'user@test.com',
      );
      await tester.enterText(
        find.byKey(const Key('password-field')),
        'password123',
      );

      // Tap login
      await tester.tap(find.byType(ElevatedButton));
      await tester.pumpAndSettle();

      // Verify navigation to home
      expect(find.byType(HomeScreen), findsOneWidget);
      expect(find.text('Welcome!'), findsOneWidget);
    });

    testWidgets('invalid credentials show error', (tester) async {
      app.main();
      await tester.pumpAndSettle();

      await tester.enterText(
        find.byKey(const Key('email-field')),
        'wrong@test.com',
      );
      await tester.enterText(
        find.byKey(const Key('password-field')),
        'wrongpass',
      );

      await tester.tap(find.byType(ElevatedButton));
      await tester.pumpAndSettle();

      // Should stay on login and show error
      expect(find.text('Invalid credentials'), findsOneWidget);
    });
  });
}