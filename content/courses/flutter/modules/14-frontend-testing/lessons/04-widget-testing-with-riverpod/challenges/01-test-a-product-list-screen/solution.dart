void main() {
  testWidgets('shows loading indicator', (tester) async {
    await tester.pumpWidget(
      ProviderScope(
        overrides: [
          productsProvider.overrideWith((ref) async {
            await Future.delayed(const Duration(seconds: 10));
            return [];
          }),
        ],
        child: const MaterialApp(home: ProductsScreen()),
      ),
    );

    await tester.pump();
    expect(find.byType(CircularProgressIndicator), findsOneWidget);
  });

  testWidgets('displays products on success', (tester) async {
    await tester.pumpWidget(
      ProviderScope(
        overrides: [
          productsProvider.overrideWith((ref) async => [
            Product(id: '1', name: 'Widget A', price: 9.99),
            Product(id: '2', name: 'Widget B', price: 19.99),
          ]),
        ],
        child: const MaterialApp(home: ProductsScreen()),
      ),
    );

    await tester.pumpAndSettle();

    expect(find.text('Widget A'), findsOneWidget);
    expect(find.text('Widget B'), findsOneWidget);
    expect(find.byType(ListTile), findsNWidgets(2));
  });

  testWidgets('shows error with retry on failure', (tester) async {
    await tester.pumpWidget(
      ProviderScope(
        overrides: [
          productsProvider.overrideWith((ref) async {
            throw Exception('Network error');
          }),
        ],
        child: const MaterialApp(home: ProductsScreen()),
      ),
    );

    await tester.pumpAndSettle();

    expect(find.text('Error loading products'), findsOneWidget);
    expect(find.byType(ElevatedButton), findsOneWidget);
  });
}