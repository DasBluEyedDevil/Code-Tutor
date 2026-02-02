// Step 1: Write these tests FIRST (they will fail)
void main() {
  late ProviderContainer container;

  setUp(() => container = ProviderContainer());
  tearDown(() => container.dispose());

  // TODO: Write test for initial state = 0
  // TODO: Write test for increment
  // TODO: Write test for decrement
  // TODO: Write test for reset
}

// Step 2: Implement CounterNotifier to make tests pass
// TODO: Create CounterNotifier