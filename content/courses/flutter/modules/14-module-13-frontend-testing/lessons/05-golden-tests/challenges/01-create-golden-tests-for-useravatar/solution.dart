void main() {
  testWidgets('UserAvatar with initials matches golden', (tester) async {
    await tester.pumpWidget(
      const MaterialApp(
        home: Scaffold(
          body: Center(
            child: UserAvatar(initials: 'JD', size: 48),
          ),
        ),
      ),
    );

    await expectLater(
      find.byType(UserAvatar),
      matchesGoldenFile('goldens/user_avatar_initials.png'),
    );
  });

  testWidgets('UserAvatar small size matches golden', (tester) async {
    await tester.pumpWidget(
      const MaterialApp(
        home: Scaffold(
          body: Center(
            child: UserAvatar(initials: 'AB', size: 24),
          ),
        ),
      ),
    );

    await expectLater(
      find.byType(UserAvatar),
      matchesGoldenFile('goldens/user_avatar_small.png'),
    );
  });

  testWidgets('UserAvatar large size matches golden', (tester) async {
    await tester.pumpWidget(
      const MaterialApp(
        home: Scaffold(
          body: Center(
            child: UserAvatar(initials: 'XY', size: 80),
          ),
        ),
      ),
    );

    await expectLater(
      find.byType(UserAvatar),
      matchesGoldenFile('goldens/user_avatar_large.png'),
    );
  });
}