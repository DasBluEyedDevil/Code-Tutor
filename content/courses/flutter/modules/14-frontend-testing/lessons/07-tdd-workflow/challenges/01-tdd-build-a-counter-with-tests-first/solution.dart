void main() {
  late ProviderContainer container;
  late CounterNotifier notifier;

  setUp(() {
    container = ProviderContainer();
    notifier = container.read(counterProvider.notifier);
  });

  tearDown(() => container.dispose());

  test('initial state is 0', () {
    expect(container.read(counterProvider), 0);
  });

  test('increment increases by 1', () {
    notifier.increment();
    expect(container.read(counterProvider), 1);
  });

  test('decrement decreases by 1', () {
    notifier.increment();
    notifier.decrement();
    expect(container.read(counterProvider), 0);
  });

  test('reset sets state to 0', () {
    notifier.increment();
    notifier.increment();
    notifier.reset();
    expect(container.read(counterProvider), 0);
  });
}

// Implementation (written AFTER tests)
class CounterNotifier extends Notifier<int> {
  @override
  int build() => 0;

  void increment() => state++;
  void decrement() => state--;
  void reset() => state = 0;
}

final counterProvider = NotifierProvider<CounterNotifier, int>(
  CounterNotifier.new,
);