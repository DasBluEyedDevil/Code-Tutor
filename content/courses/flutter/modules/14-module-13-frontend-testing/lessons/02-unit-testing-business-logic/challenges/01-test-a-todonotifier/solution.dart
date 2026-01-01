void main() {
  late ProviderContainer container;
  late TodoNotifier notifier;

  setUp(() {
    container = ProviderContainer();
    notifier = container.read(todoProvider.notifier);
  });

  tearDown(() => container.dispose());

  group('TodoNotifier', () {
    test('starts with empty list', () {
      expect(container.read(todoProvider), isEmpty);
    });

    test('addTodo adds a new todo', () {
      notifier.addTodo('Buy milk');
      final todos = container.read(todoProvider);
      expect(todos, hasLength(1));
      expect(todos.first.title, 'Buy milk');
      expect(todos.first.completed, false);
    });

    test('toggleTodo toggles completion', () {
      notifier.addTodo('Test todo');
      final id = container.read(todoProvider).first.id;
      
      notifier.toggleTodo(id);
      expect(container.read(todoProvider).first.completed, true);
      
      notifier.toggleTodo(id);
      expect(container.read(todoProvider).first.completed, false);
    });

    test('removeTodo removes the todo', () {
      notifier.addTodo('To remove');
      final id = container.read(todoProvider).first.id;
      
      notifier.removeTodo(id);
      expect(container.read(todoProvider), isEmpty);
    });
  });
}