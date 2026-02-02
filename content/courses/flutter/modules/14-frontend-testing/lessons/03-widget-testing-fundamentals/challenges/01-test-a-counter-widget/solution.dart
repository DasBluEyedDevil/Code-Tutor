void main() {
  testWidgets('displays initial count of 0', (tester) async {
    await tester.pumpWidget(const MaterialApp(home: CounterScreen()));
    expect(find.text('0'), findsOneWidget);
  });

  testWidgets('increments count when + is tapped', (tester) async {
    await tester.pumpWidget(const MaterialApp(home: CounterScreen()));

    await tester.tap(find.byKey(const Key('increment')));
    await tester.pump();

    expect(find.text('1'), findsOneWidget);
  });

  testWidgets('decrements count when - is tapped', (tester) async {
    await tester.pumpWidget(const MaterialApp(home: CounterScreen()));

    await tester.tap(find.byKey(const Key('decrement')));
    await tester.pump();

    expect(find.text('-1'), findsOneWidget);
  });

  testWidgets('multiple taps work correctly', (tester) async {
    await tester.pumpWidget(const MaterialApp(home: CounterScreen()));

    await tester.tap(find.byKey(const Key('increment')));
    await tester.tap(find.byKey(const Key('increment')));
    await tester.tap(find.byKey(const Key('increment')));
    await tester.pump();

    expect(find.text('3'), findsOneWidget);
  });
}